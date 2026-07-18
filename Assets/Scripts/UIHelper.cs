using UnityEngine;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;

    public void Pause()
    {
        Time.timeScale = 0f;
        _pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _pauseScreen.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GameManager.instance.RestartScene();
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        GameManager.instance.ChangeToNextScene();
    }

    public void ReturnToLevelSelect()
    {
        Time.timeScale = 1f;
        GameManager.instance.ChangeScene("MainMenu");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.instance.ChangeScene("MainMenu");
    }
}
