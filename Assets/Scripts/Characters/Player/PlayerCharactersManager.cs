using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharactersManager : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private List<PlayerModel> modelsList = new List<PlayerModel>();

    public event Action<Animator> OnPlayerModelChanged;
    public event Action<PlayersCharactersTypes> OnCharacterChanged;

    private void Start()
    {
        SetPlayCharacterModel(PlayersCharactersTypes.Knight);
    }

    public void SetPlayCharacterModel(PlayersCharactersTypes model)
    {
        for(int i = 0; i < modelsList.Count; i++)
        {
            if(modelsList[i].ModelType == model)
            {
                modelsList[i].gameObject.SetActive(true);
                OnPlayerModelChanged?.Invoke(modelsList[i].AnimatorComponent);
                OnCharacterChanged?.Invoke(modelsList[i].ModelType);
            }
            else
            {
                modelsList[i].gameObject.SetActive(false);
            }
        }
    }
}
