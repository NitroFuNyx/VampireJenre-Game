using UnityEngine;

public class SystemTimeManager : MonoBehaviour
{
    private readonly float pauseGameSpeed = 0.000000001f;
    private readonly float normalGameSpeed = 1f;

    public void PauseGame()
    {
        Time.timeScale = pauseGameSpeed;
    }

    public void ResumeGame()
    {
        Time.timeScale = normalGameSpeed;
    }
}
