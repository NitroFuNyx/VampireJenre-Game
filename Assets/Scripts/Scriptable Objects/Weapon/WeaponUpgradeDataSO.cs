using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgradeDataSO", menuName = "ScriptableObjects/Weapon/WeaponUpgradeDataSO")]
public class WeaponUpgradeDataSO : ScriptableObject
{
    [Header("Weapon Data")]
    [Space]
    [SerializeField] private List<WeaponUpgradeDataStruct> weaponUpgradeDataList = new List<WeaponUpgradeDataStruct>();

    public List<WeaponUpgradeDataStruct> WeaponUpgradeDataList { get => weaponUpgradeDataList; }
}
