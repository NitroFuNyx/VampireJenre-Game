using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MapsManager : MonoBehaviour
{
    [Header("Current Map Data")]
    [Space]
    [SerializeField] private LevelMaps currentLevelMap;

    private List<LevelMaps> levelMapsList = new List<LevelMaps>();

    public event Action<LevelMaps> OnMapChanged;

    private void Awake()
    {
        levelMapsList.Add(LevelMaps.Cementary);
        levelMapsList.Add(LevelMaps.Castle);
    }

    private void Start()
    {
        StartCoroutine(SetStartMapCoroutine());
    }

    public void PressButton_ChangeMap(SelectionArrowTypes buttonArrowType)
    {
        int newMapIndex = levelMapsList.IndexOf(currentLevelMap);

        if (buttonArrowType == SelectionArrowTypes.Right)
        {
            newMapIndex += 1;

            if (newMapIndex == levelMapsList.Count)
            {
                newMapIndex = 0;
            }
        }
        else
        {
            newMapIndex -= 1;

            if (newMapIndex < 0)
            {
                newMapIndex = levelMapsList.Count - 1;
            }
        }

        SetLevelMap(levelMapsList[newMapIndex]);
    }

    private void SetLevelMap(LevelMaps map)
    {
        currentLevelMap = map;
        OnMapChanged?.Invoke(currentLevelMap);
    }

    private IEnumerator SetStartMapCoroutine()
    {
        yield return null;
        yield return null;
        SetLevelMap(LevelMaps.Cementary);
    }
}
