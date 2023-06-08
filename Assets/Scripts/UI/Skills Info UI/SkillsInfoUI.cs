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

    public void ShowSkillsInfo(SkillBasicTypes skillsType)
    {
        if(skillsType == SkillBasicTypes.Active)
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
}
