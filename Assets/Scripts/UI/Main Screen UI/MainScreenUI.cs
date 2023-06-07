using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class MainScreenUI : MainCanvasPanel
{
    [Header("Images")]
    [Space]
    [SerializeField] private List<Image> chaptersProgressImagesList = new List<Image>();

    private ChaptersProgressManager _chaptersProgressManager;

    private void Start()
    {
        ChangeProgressImages(_chaptersProgressManager.FinishedChaptersCounter);

        _chaptersProgressManager.OnChaptersProgressUpdated += ChaptersProgressManager_OnChaptersProgressUpdated_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _chaptersProgressManager.OnChaptersProgressUpdated -= ChaptersProgressManager_OnChaptersProgressUpdated_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(ChaptersProgressManager chaptersProgressManager)
    {
        _chaptersProgressManager = chaptersProgressManager;
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
}
