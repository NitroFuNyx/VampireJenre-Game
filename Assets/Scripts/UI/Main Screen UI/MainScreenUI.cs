using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class MainScreenUI : MainCanvasPanel
{
    [Header("Images")]
    [Space]
    [SerializeField] private List<Image> chaptersProgressImagesList = new List<Image>();
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite cementaryMainScreenSprite;
    [SerializeField] private Sprite castleMainScreenSprite;

    private ChaptersProgressManager _chaptersProgressManager;
    private MapsManager _mapsManager;

    private void Start()
    {
        ChangeProgressImages(_chaptersProgressManager.FinishedChaptersCounter);

        _chaptersProgressManager.OnChaptersProgressUpdated += ChaptersProgressManager_OnChaptersProgressUpdated_ExecuteReaction;
        _mapsManager.OnMapChanged += MapsManager_OnMapChanged_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _chaptersProgressManager.OnChaptersProgressUpdated -= ChaptersProgressManager_OnChaptersProgressUpdated_ExecuteReaction;
        _mapsManager.OnMapChanged -= MapsManager_OnMapChanged_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(ChaptersProgressManager chaptersProgressManager, MapsManager mapsManager)
    {
        _chaptersProgressManager = chaptersProgressManager;
        _mapsManager = mapsManager;
    }
    #endregion Zenject

    public override void PanelActivated_ExecuteReaction()
    {
        ChangeProgressImages(_chaptersProgressManager.FinishedChaptersCounter);
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
        
    }

    private void ChangeProgressImages(int finishedChaptersCounter)
    {
        for(int i = 0; i < chaptersProgressImagesList.Count; i++)
        {
            if(i <= finishedChaptersCounter)
            {
                chaptersProgressImagesList[i].gameObject.SetActive(true);
            }
            else
            {
                chaptersProgressImagesList[i].gameObject.SetActive(false);
            }
        }
    }

    private void ChaptersProgressManager_OnChaptersProgressUpdated_ExecuteReaction(int finishedChaptersCounter)
    {
        ChangeProgressImages(finishedChaptersCounter);
    }

    private void MapsManager_OnMapChanged_ExecuteReaction(LevelMaps map)
    {
        if(map == LevelMaps.Cementary)
        {
            chaptersProgressImagesList[0].sprite = cementaryMainScreenSprite;
            ChangeProgressImages(_chaptersProgressManager.FinishedChaptersCounter);
        }
        else
        {
            chaptersProgressImagesList[0].sprite = castleMainScreenSprite;
            ChangeProgressImages(0);
        }
    }
}
