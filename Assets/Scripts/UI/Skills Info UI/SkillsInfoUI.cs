using UnityEngine;

public class SkillsInfoUI : MainCanvasPanel
{
    [Header("Skills Panels")]
    [Space]
    [SerializeField] private GameObject activeSkillsPanel;
    [SerializeField] private GameObject passiveSkillsPanel;
 
    public override void PanelActivated_ExecuteReaction()
    {
        activeSkillsPanel.SetActive(true);
        passiveSkillsPanel.SetActive(false);
    }

    public override void PanelDeactivated_ExecuteReaction()
    {

    }

    public void ShowActiveSkillsInfo()
    {
        passiveSkillsPanel.SetActive(false);
        activeSkillsPanel.SetActive(true);
    }

    public void ShowPassiveSkillsInfo()
    {
        activeSkillsPanel.SetActive(false);
        passiveSkillsPanel.SetActive(true);
    }
}
