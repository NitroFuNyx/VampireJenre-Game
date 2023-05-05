using UnityEngine;
using System;
using Zenject;

public class HapticManager : MonoBehaviour, IDataPersistance
{
    private DataPersistanceManager _dataPersistanceManager;

    private bool canVibrate = true;

    #region Events Declaration
    public event Action<bool> OnHapticStateChanged;
    #endregion Events Declaration

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
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
        canVibrate = data.canVibrate;
        OnHapticStateChanged?.Invoke(canVibrate);
    }

    public void SaveData(GameData data)
    {
        data.canVibrate = canVibrate;
    }
    #endregion Save/Load Methods

    public void ChangeHapticState(bool canVibrateState)
    {
        canVibrate = canVibrateState;

        OnHapticStateChanged?.Invoke(canVibrate);
        _dataPersistanceManager.SaveGame();
    }

    [ContextMenu("Vibrate")]
    public void Vibrate()
    {
        if(canVibrate)
        {
            Debug.Log($"Vibrates");
            Haptic.Vibrate();
        }    
    }
}
