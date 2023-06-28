using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharactersManager : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private List<PlayerModel> modelsList = new List<PlayerModel>();
    [Header("Current Data")]
    [Space]
    [SerializeField] private PlayerClasses currentClass;

    private float setStartCharacterDelay = 1f;

    public PlayerClasses CurrentClass { get => currentClass; }

    public event Action<Animator> OnPlayerModelChanged;
    public event Action<PlayerClasses> OnCharacterChanged;

    private void Start()
    {
        StartCoroutine(SetStartCharacterCoroutine());
    }

    public void SetPlayCharacterModel(PlayerClasses model)
    {
        for(int i = 0; i < modelsList.Count; i++)
        {
            if(modelsList[i].ModelType == model)
            {
                modelsList[i].gameObject.SetActive(true);
                currentClass = modelsList[i].ModelType;
                Debug.Log($"Character Changed {currentClass}");
                OnPlayerModelChanged?.Invoke(modelsList[i].AnimatorComponent);
                OnCharacterChanged?.Invoke(currentClass);
            }
            else
            {
                modelsList[i].gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator SetStartCharacterCoroutine()
    {
        yield return new WaitForSeconds(setStartCharacterDelay);
        SetPlayCharacterModel(PlayerClasses.Knight);
    }
}
