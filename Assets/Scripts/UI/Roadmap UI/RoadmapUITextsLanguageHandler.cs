using UnityEngine;
using Localization;
using TMPro;

public class RoadmapUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI panelTitleText;
    [Space]
    [SerializeField] private TextMeshProUGUI shopTitleText;
    [SerializeField] private TextMeshProUGUI shopDescribtionText;
    [Space]
    [SerializeField] private TextMeshProUGUI charactersTitleText;
    [SerializeField] private TextMeshProUGUI charactersDescribtionText;
    [Space]
    [SerializeField] private TextMeshProUGUI mapsTitleText;
    [SerializeField] private TextMeshProUGUI mapsDescribtionText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        panelTitleText.text = languageHolder.data.roadmapUITexts.titleText;

        shopTitleText.text = languageHolder.data.roadmapUITexts.shopPanelTitleText;
        shopDescribtionText.text = languageHolder.data.roadmapUITexts.shopPanelDescriptionText;

        charactersTitleText.text = languageHolder.data.roadmapUITexts.charactersPanelTitleText;
        charactersDescribtionText.text = languageHolder.data.roadmapUITexts.charactersPanelDescriptionText;

        mapsTitleText.text = languageHolder.data.roadmapUITexts.mapsPanelTitleText;
        mapsDescribtionText.text = languageHolder.data.roadmapUITexts.mapsPanelDescriptionText;
    }
}
