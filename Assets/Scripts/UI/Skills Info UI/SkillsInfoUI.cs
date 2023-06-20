using UnityEngine;
using System;
using System.Collections;

public class SkillsInfoUI : MainCanvasPanel
{
    [Header("Skills Panels")]
    [Space]
    [SerializeField] private GameObject activeSkillsPanel;
    [SerializeField] private GameObject passiveSkillsPanel;

    private float setStartSettingsDelay = 0.5f;

    public event Action<SkillBasicTypes> OnCurrentSkillsInfoPanelChanged;

    private void Start()
    {
        StartCoroutine(SetStartSettingsCoroutine());
    }

    public override void PanelActivated_ExecuteReaction()
    {
        ShowSkillsInfo(SkillBasicTypes.Active);
    }

    public override void PanelDeactivated_ExecuteReaction()
    {

    }

    public void ShowSkillsInfo(SkillBasicTypes skillsType)
    {
        OnCurrentSkillsInfoPanelChanged?.Invoke(skillsType);

        if (skillsType == SkillBasicTypes.Active)
        {
            passiveSkillsPanel.SetActive(false);
            activeSkillsPanel.SetActive(true);
        }
        else
        {
            activeSkillsPanel.SetActive(false);
            passiveSkillsPanel.SetActive(true);
        }
    }

    private IEnumerator SetStartSettingsCoroutine()
    {
        yield return new WaitForSeconds(setStartSettingsDelay);
        ShowSkillsInfo(SkillBasicTypes.Active);
    }
}
