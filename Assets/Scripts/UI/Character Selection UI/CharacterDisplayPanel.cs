using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterDisplayPanel : MonoBehaviour
{
    [Header("Character Data")]
    [Space]
    [SerializeField] private PlayerClasses characterClass;
    [Header("Images")]
    [Space]
    [SerializeField] private Image characterImage;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI classText;
    [SerializeField] private TextMeshProUGUI characterNameText;

    public void SetCharacterData(PlayerClasses character, int level)
    {

    }
}
