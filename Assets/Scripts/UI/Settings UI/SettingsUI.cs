using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MainCanvasPanel
{
    private List<SettingsPanel> panelsList = new List<SettingsPanel>();
    private Dictionary<SettingsPanels, SettingsPanel> panelsDictionary = new Dictionary<SettingsPanels, SettingsPanel>();

    private void Start()
    {
        FillPanelsListAndDictionary();
        SetStartSettings();
    }

    public override void PanelActivated_ExecuteReaction()
    {
        panelsDictionary[SettingsPanels.MainSettings].ShowPanel();
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
        
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

    private void SetStartSettings()
    {
        for(int i = 0; i < panelsList.Count; i++)
        {
            panelsList[i].HidePanel();
        }
    }
}
