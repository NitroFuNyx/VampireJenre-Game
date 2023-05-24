using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpgradePanel : MonoBehaviour
{
    [Header("Skills Upgrade Panels")]
    [Space]
    [SerializeField] private List<SkillUpgradeDisplayPanel> skillUpgradeDisplayPanelsList = new List<SkillUpgradeDisplayPanel>();
}