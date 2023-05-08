using UnityEngine;

public class SystemTimeManager : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0.000000001f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
