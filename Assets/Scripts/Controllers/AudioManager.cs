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
    [Header("SFX Clips")]
    [Space]
    [SerializeField] private AudioClip uiButtonClip;

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

    private void SetStartSettings()
    {
        FillAudioSourcesList();
    }

    private void FillAudioSourcesList()
    {
        audioSourcesList.Add(musicAudioSource);
        audioSourcesList.Add(sfxAudioSource);
        audioSourcesList.Add(sfxAdditionalAudioSource);
    }

    private void SetAudioSourcesState()
    {
        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            audioSourcesList[i].mute = audioMuted;
        }

        OnAudioMuteStateChanged?.Invoke(audioMuted);
    }

    #region SFX Methods
    public void PlaySFXSound_PressButtonUI()
    {
        AudioSource source = GetSFXAudioSource();
        source.clip = uiButtonClip;
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
}
