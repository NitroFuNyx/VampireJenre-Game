using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Save_Load_System
{
    [Serializable]
    public class PlayerStatisticsManager : MonoBehaviour, IDataPersistance
    {
        private PlayerStatsData _playerStatsData;
        private GameProcessManager _gameProcessManager;
        private TimersManager _timersManager;
        private DataPersistanceManager _dataPersistanceManager;
        private SpawnEnemiesManager _spawnEnemiesManager;
        private SkillsManager _skillsManager;
        private Dictionary<int, int> pickedSkillsDictionary;

        public PlayerStatsData PlayerStatsData => _playerStatsData;

        #region UnityEvents

        private void Awake()
        {
            _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
        }

        private void Start()
        {
            pickedSkillsDictionary = new Dictionary<int, int>();
            _playerStatsData = new PlayerStatsData();
        }

        #endregion

        #region Zenject

        [Inject]
        private void InjectDependencies(GameProcessManager gameProcessManager, TimersManager timersManager,
            DataPersistanceManager dataPersistanceManager, SpawnEnemiesManager spawnEnemiesManager,
            SkillsManager skillsManager)
        {
            _skillsManager = skillsManager;
            _spawnEnemiesManager = spawnEnemiesManager;
            _dataPersistanceManager = dataPersistanceManager;
            _timersManager = timersManager;
            _gameProcessManager = gameProcessManager;
        }

        #endregion

        #region EventSubscription

        private void OnEnable()
        {
            _gameProcessManager.OnPlayerLost += GameProcessManager_OnPlayerLost_ExecuteReaction;
            _gameProcessManager.OnPlayerWon += GameProcessManager_OnPlayerWon_ExecuteReaction;

            _timersManager.OnStopwatchTimeStop += AddTimePlayed;
            _timersManager.OnStopwatchTimeStop += CalculateLongestTimePlayed;

            _skillsManager.OnSkillToUpgradeDefined += IncreaseChooseMostPickableSkillDictionary;
        }

        private void OnDisable()
        {
            _gameProcessManager.OnPlayerLost -= GameProcessManager_OnPlayerLost_ExecuteReaction;
            _gameProcessManager.OnPlayerWon -= GameProcessManager_OnPlayerWon_ExecuteReaction;

            _timersManager.OnStopwatchTimeStop -= AddTimePlayed;
            _timersManager.OnStopwatchTimeStop -= CalculateLongestTimePlayed;

            _skillsManager.OnSkillToUpgradeDefined -= IncreaseChooseMostPickableSkillDictionary;
        }

        #endregion

        #region Save/Load

        public void LoadData(GameData data)
        {
            _playerStatsData = data.playerStatsData;
            ConvertListsToDictionaries();
        }

        public void SaveData(GameData data)
        {
            CalculateAverageTimeSurviving();
            ConvertDictionariesToLists();
            data.playerStatsData = PlayerStatsData;
        }

        #endregion

        #region Converters

        public void ConvertDictionariesToLists()
        {
            PlayerStatsData.pickedSkillsKeys = new List<int>(pickedSkillsDictionary.Keys);
            PlayerStatsData.pickedSkillsValues = new List<int>(pickedSkillsDictionary.Values);
        }

        public void ConvertListsToDictionaries()
        {
            if (PlayerStatsData.pickedSkillsKeys.Count != PlayerStatsData.pickedSkillsValues.Count)
            {
                Debug.LogError("The number of keys and values in the lists does not match!");
                return;
            }

            for (int i = 0; i < PlayerStatsData.pickedSkillsKeys.Count; i++)
            {
                pickedSkillsDictionary[PlayerStatsData.pickedSkillsKeys[i]] = PlayerStatsData.pickedSkillsValues[i];
            }
        }
    
        private int TimeToMinutes(TimeHolder time)
        {
            return time.hours * 60 + time.minutes;
        }

        #endregion

        #region Reactions

        private void GameProcessManager_OnPlayerWon_ExecuteReaction()
        {
            IncreaseMatchesPlayed(_gameProcessManager.CurrentGameMode);
        }

        private void GameProcessManager_OnPlayerLost_ExecuteReaction(GameModes obj)
        {
            IncreaseMatchesPlayed(obj);
        }

        #endregion

        #region TimeStuff
        public void CalculateAverageTimeSurviving()
        {
            TimeHolder averageTime = new TimeHolder();

            int totalMinutesPlayed = TimeToMinutes(PlayerStatsData.allTimePlayedStandardGamemode);

            int averageMinutesPlayed = totalMinutesPlayed / (PlayerStatsData.matchesPlayed==0?1:PlayerStatsData.matchesPlayed);

            averageTime.hours = averageMinutesPlayed / 60;
            averageTime.minutes = averageMinutesPlayed % 60;
            PlayerStatsData.averageTimeSurviving = averageTime;
        }
        private TimeHolder SumGameTime(string time, TimeHolder timeToSum)
        {
            TimeHolder givenTime = new TimeHolder(time);

            int newTotalMinutesPlayed = TimeToMinutes(givenTime);

            int totalMinutesPlayed = TimeToMinutes(timeToSum);

            int updatedTotalMinutesPlayed = totalMinutesPlayed + newTotalMinutesPlayed;

            int updatedHours = updatedTotalMinutesPlayed / 60;
            int updatedMinutes = updatedTotalMinutesPlayed % 60;

            TimeHolder updatedAllTimePlayed = new TimeHolder {hours = updatedHours, minutes = updatedMinutes};
            return updatedAllTimePlayed;
        }
        public void AddTimePlayed(string time)
        {
            if (_gameProcessManager.CurrentGameMode == GameModes.Standart)
                PlayerStatsData.allTimePlayedStandardGamemode =
                    SumGameTime(time, PlayerStatsData.allTimePlayedStandardGamemode);
            PlayerStatsData.allTimePlayed = SumGameTime(time, PlayerStatsData.allTimePlayed);
            
        }
        public void CalculateLongestTimePlayed(string timePlayed)
        {
            if (_gameProcessManager.CurrentGameMode == GameModes.Standart) return;
            TimeHolder time = new TimeHolder(timePlayed);
            int totalMinutesPlayed = time.hours * 60 + time.minutes;

            int longestTotalMinutesPlayed = TimeToMinutes(PlayerStatsData.longestTimePlayed);

            if (totalMinutesPlayed > longestTotalMinutesPlayed)
            {
                PlayerStatsData.longestTimePlayed = time;
            }

            _dataPersistanceManager.SaveGame();
        }

        #endregion
        private void IncreaseMonstersKilledOverall()
        {
            PlayerStatsData.killedMonstersAmount += _spawnEnemiesManager.DefeatedEnemiesCounter;
            CalculateAverageMonstersKilled();
        }

        public void CalculateAverageMonstersKilled()
        {
            PlayerStatsData.killedMonstersAverage = PlayerStatsData.killedMonstersAmount / PlayerStatsData.matchesPlayed;
        }

        private void IncreaseChooseMostPickableSkillDictionary(int skillCategory, int pickedSkill)
        {
            SkillBasicTypes category = (SkillBasicTypes) skillCategory;
            int skill = pickedSkill;
            if (category == SkillBasicTypes.Active)
            {
                if (!pickedSkillsDictionary.ContainsKey(skill)) pickedSkillsDictionary.Add(skill, 0);

                pickedSkillsDictionary[skill] += 1;
            }

            ChooseMostPickableSkill();
        }

        private void ChooseMostPickableSkill()
        {
            int maxSkillValue = int.MinValue;
            int maxSkill = 0;

            foreach (KeyValuePair<int, int> skill in pickedSkillsDictionary)
            {
                if (skill.Value > maxSkillValue)
                {
                    maxSkillValue = skill.Value;
                    maxSkill = skill.Key;
                }
            }

            PlayerStatsData.mostPickableSkill = (ActiveSkills) maxSkill;
        }
        
        public void IncreaseMatchesPlayed(GameModes _)
        {
            ++PlayerStatsData.matchesPlayed;
            IncreaseMonstersKilledOverall();
        }

    }
}