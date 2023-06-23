using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharactersManager : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private List<PlayerModel> modelsList = new List<PlayerModel>();

    public event Action<Animator> OnPlayerModelChanged;

    private void Start()
    {
        SetPlayCharacterModel(PlayerModels.Knight);
    }

    public void SetPlayCharacterModel(PlayerModels model)
    {
        for(int i = 0; i < modelsList.Count; i++)
        {
            if(modelsList[i].ModelType == model)
            {
                modelsList[i].gameObject.SetActive(true);
                OnPlayerModelChanged?.Invoke(modelsList[i].AnimatorComponent);
            }
            else
            {
                modelsList[i].gameObject.SetActive(false);
            }
        }
    }
}
