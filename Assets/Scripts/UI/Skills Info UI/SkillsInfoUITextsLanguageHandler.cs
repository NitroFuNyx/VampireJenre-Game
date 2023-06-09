using UnityEngine;
using Localization;
using TMPro;

public class SkillsInfoUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Buttons Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI activeSkilsText;
    [SerializeField] private TextMeshProUGUI passiveSkilsText;
    [Header("Active Skills Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI singleShotText;
    [SerializeField] private TextMeshProUGUI chainLightningText;
    [SerializeField] private TextMeshProUGUI magicAuraText;
    [SerializeField] private TextMeshProUGUI forceWaveText;
    [SerializeField] private TextMeshProUGUI fireballsText;
    [SerializeField] private TextMeshProUGUI lightningBoltText;
    [SerializeField] private TextMeshProUGUI pulseAuraText;
    [SerializeField] private TextMeshProUGUI allDirectionsShotsText;
    [SerializeField] private TextMeshProUGUI weaponStrikeText;
    [SerializeField] private TextMeshProUGUI meteorText;
    [Header("Passive Skills Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI critPowerText;
    [SerializeField] private TextMeshProUGUI movementSpeedText;
    [SerializeField] private TextMeshProUGUI itemDropChanceText;
    [SerializeField] private TextMeshProUGUI coinsSurplusText;
    [SerializeField] private TextMeshProUGUI decreaseIncomeDamageText;
    [SerializeField] private TextMeshProUGUI regenerationText;
    [SerializeField] private TextMeshProUGUI critChanceText;
    [SerializeField] private TextMeshProUGUI skillRangeText;
    [SerializeField] private TextMeshProUGUI increaseDamageText;
    [SerializeField] private TextMeshProUGUI additionalProjectileText;

    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        activeSkilsText.text = languageHolder.data.skillsInfoUITexts.activeSkillsButtonText;
        passiveSkilsText.text = languageHolder.data.skillsInfoUITexts.passiveSkillsButtonText;

        singleShotText.text = languageHolder.data.skillsInfoUITexts.singleShotText;
        chainLightningText.text = languageHolder.data.skillsInfoUITexts.chainLightningText;
        magicAuraText.text = languageHolder.data.skillsInfoUITexts.magicAuraText;
        forceWaveText.text = languageHolder.data.skillsInfoUITexts.forceWaveText;
        fireballsText.text = languageHolder.data.skillsInfoUITexts.fireballsText;
        lightningBoltText.text = languageHolder.data.skillsInfoUITexts.lightningBoltText;
        pulseAuraText.text = languageHolder.data.skillsInfoUITexts.pulseAuraText;
        allDirectionsShotsText.text = languageHolder.data.skillsInfoUITexts.allDirectionsShotsText;
        weaponStrikeText.text = languageHolder.data.skillsInfoUITexts.weaponStrikeText;
        meteorText.text = languageHolder.data.skillsInfoUITexts.meteorText;

        critPowerText.text = languageHolder.data.skillsInfoUITexts.critPowerText;
        movementSpeedText.text = languageHolder.data.skillsInfoUITexts.movementSpeedText;
        itemDropChanceText.text = languageHolder.data.skillsInfoUITexts.itemDropChanceText;
        coinsSurplusText.text = languageHolder.data.skillsInfoUITexts.coinsSurplusText;
        decreaseIncomeDamageText.text = languageHolder.data.skillsInfoUITexts.decreaseIncomeDamageText;
        regenerationText.text = languageHolder.data.skillsInfoUITexts.regenerationText;
        critChanceText.text = languageHolder.data.skillsInfoUITexts.critChanceText;
        skillRangeText.text = languageHolder.data.skillsInfoUITexts.increaseRangeText;
        increaseDamageText.text = languageHolder.data.skillsInfoUITexts.increaseDamageText;
        additionalProjectileText.text = languageHolder.data.skillsInfoUITexts.increaseProjectilesAmountText;
    }
}
