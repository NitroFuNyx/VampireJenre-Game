using UnityEngine;
using System.Collections.Generic;

public class MenuButtonsUI : MainCanvasPanel
{
    [Header("Buttons")]
    [Space]
    [SerializeField] private List<MenuPanelButton> buttonsList;

    public override void PanelActivated_ExecuteReaction()
    {

    }

    public override void PanelDeactivated_ExecuteReaction()
    {

    }

    public void ResetButtonsSprites()
    {
        for(int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].SetStandartButtonSprite();
        }
    }
}
