using UnityEngine;
using System;
using System.Collections.Generic;
using Zenject;

public class AudioManager : MonoBehaviour, IDataPersistance
{
    [Header("Audio Sources")]
    [Space]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource sfxAdditionalAudioSource;
    [SerializeField] private AudioSource voicesAudioSource;
    [Header("Music Clips")]
    [Space]
    [SerializeField] private AudioClip mainScreenClip;
    [SerializeField] private AudioClip battleClip;
    [SerializeField] private AudioClip bossClip;
    [Header("SFX Clips")]
    [Space]
    [SerializeField] private AudioClip uiButtonClip;
    [SerializeField] private AudioClip victoryClip;
    [SerializeField] private AudioClip defeatClip;
    [SerializeField] private AudioClip talentSlotChangeClip;
    [SerializeField] private AudioClip talentGrantedClip;
    //[SerializeField] private AudioClip rewardWheelSlotChangeClip;
    [SerializeField] private AudioClip pickUpItemClip;
    [SerializeField] private AudioClip pickUpResourceClip;
    [SerializeField] private AudioClip buyItemClip;

    private DataPersistanceManager _dataPersistanceManager;

    private List<AudioSource> audioSourcesList = new List<AudioSource>();

    private bool audioMuted = false;

    #region Events Declaration
    public event Action<bool> OnAudioMuteStateChanged;
    #endregion Events Declaration

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        SetStartSettings();
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    #region Save/Load Methods
    public void LoadData(GameData data)
    {
        audioMuted = data.soundMuted;
        SetAudioSourcesState();
    }

    public void SaveData(GameData data)
    {
        data.soundMuted = audioMuted;
    }
    #endregion Save/Load Methods

    public void ChangeMuteState(bool muted)
    {
        audioMuted = muted;

        SetAudioSourcesState();
        _dataPersistanceManager.SaveGame();
    }

    private void SetStartSettings()
    {
        FillAudioSourcesList();
        PlayMusic_MainScreen_Loader();
    }

    private void FillAudioSourcesList()
    {
        audioSourcesList.Add(musicAudioSource);
        audioSourcesList.Add(sfxAudioSource);
        audioSourcesList.Add(sfxAdditionalAudioSource);
        audioSourcesList.Add(voicesAudioSource);
    }

    private void SetAudioSourcesState()
    {
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            audioSourcesList[i].mute = audioMuted;
        }

        OnAudioMuteStateChanged?.Invoke(audioMuted);
    }

    #region Music Methods
    public void PlayMusic_MainScreen_Loader()
    {
        musicAudioSource.clip = mainScreenClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayMusic_Battle()
    {
        musicAudioSource.clip = battleClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayMusic_Boss()
    {
        musicAudioSource.clip = bossClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void StopMusicAudio()
    {
        musicAudioSource.Stop();
    }
    #endregion Music Methods

    #region SFX Methods
    public void PlaySFXSound_PressButtonUI()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = uiButtonClip;
        source.Play();
    }

    public void PlaySFXSound_Victory()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = victoryClip;
        source.Play();
    }

    public void PlaySFXSound_Defeat()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = defeatClip;
        source.Play();
    }

    public void PlaySFXSound_TalentSlotChange()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = talentSlotChangeClip;
        source.Play();
    }

    public void PlaySFXSound_TalentGranted()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = talentGrantedClip;
        source.Play();
    }

    //public void PlaySFXSound_RewardWheelSlotChange()
    //{
    //    AudioSource source = GetSFXAudioSource();
    //    source.clip = rewardWheelSlotChangeClip;
    //    source.Play();
    //}

    public void PlaySFXSound_PickUpItem()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = pickUpItemClip;
        source.Play();
    }

    public void PlaySFXSound_PickUpResource()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = pickUpResourceClip;
        source.Play();
    }

    public void PlaySFXSound_BuyItem()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = buyItemClip;
        source.Play();
    }

    public void StopSFXAudio()
    {
        sfxAudioSource.Stop();
        sfxAdditionalAudioSource.Stop();
    }

    private AudioSource GetSFXAudioSource()
    {
        AudioSource source = sfxAudioSource;

        if (sfxAudioSource.isPlaying)
        {
            source = sfxAdditionalAudioSource;
        }

        return source;
    }
    #endregion SFX Methods

    #region Voices Methods
    public void PlayVoicesAudio_ZombieScream()
    {
        voicesAudioSource.clip = mainScreenClip;
        voicesAudioSource.loop = true;
        voicesAudioSource.Play();
    }

    public void StopVoicesAudio()
    {
        voicesAudioSource.Stop();
    }
    #endregion Voices Methods
}
