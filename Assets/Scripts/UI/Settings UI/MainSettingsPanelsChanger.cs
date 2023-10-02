using UnityEngine;

public class MainSettingsPanelsChanger : MonoBehaviour
{
    [Header("Panels")]
    [Space]
    [SerializeField] private GameObject androidPanel;
    [SerializeField] private GameObject IosPanel;

    private void Start()
    {
        if(IsAndroid())
        {
            androidPanel.SetActive(true);
            IosPanel.SetActive(false);
        }
        else
        {
            androidPanel.SetActive(false);
            IosPanel.SetActive(true);
        }
    }

    private bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
}
