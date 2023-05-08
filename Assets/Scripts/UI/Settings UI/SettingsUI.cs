using System.Collections.Generic;

public class SettingsUI : MainCanvasPanel
{
    private List<SettingsPanel> panelsList = new List<SettingsPanel>();
    private Dictionary<SettingsPanels, SettingsPanel> panelsDictionary = new Dictionary<SettingsPanels, SettingsPanel>();

    private void Start()
    {
        FillPanelsListAndDictionary();
        ShowSettingsPanel(SettingsPanels.MainSettings);
    }

    public override void PanelActivated_ExecuteReaction()
    {
        panelsDictionary[SettingsPanels.MainSettings].ShowPanel();
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
        
    }

    public void ShowInfoPanel()
    {
        ShowSettingsPanel(SettingsPanels.InfoPanel);
    }

    public void ShowMainSettingsPanel()
    {
        ShowSettingsPanel(SettingsPanels.MainSettings);
    }

    private void FillPanelsListAndDictionary()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out SettingsPanel settingsPanel))
            {
                panelsList.Add(settingsPanel);
                panelsDictionary.Add(settingsPanel.PanelType, settingsPanel);
            }
        }
    }

    private void HidePanels()
    {
        for(int i = 0; i < panelsList.Count; i++)
        {
            panelsList[i].HidePanel();
        }
    }

    private void ShowSettingsPanel(SettingsPanels settingsPanel)
    {
        for (int i = 0; i < panelsList.Count; i++)
        {
            if(settingsPanel != panelsList[i].PanelType)
            {
                panelsList[i].HidePanel();
            }
            else
            {
                panelsList[i].ShowPanel();
            }
        }
    }
}
