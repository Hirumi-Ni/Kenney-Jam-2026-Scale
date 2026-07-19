using TMPro;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _failScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TMP_Text _winTextCountdown;

    private void OnEnable()
    {
        EventHandler.OnGameLose += OpenLoseScreen;    
        EventHandler.OnGameWin += OpenWinScreen;    
        EventHandler.OnWinCountdown += ChangeWinCountdownText;    
    }

    private void OnDisable()
    { 
        EventHandler.OnGameLose -= OpenLoseScreen;    
        EventHandler.OnGameWin -= OpenWinScreen;    
        EventHandler.OnWinCountdown -= ChangeWinCountdownText;
    }

    void Start()
    {
        _failScreen.SetActive(false);
        _winTextCountdown.text = 0.ToString();
    }

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

    public void SelectGameLevel(string sceneName)
    {
        GameManager.instance.ChangeScene(sceneName);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        GameManager.instance.ChangeToNextScene();
    }

    public void ReturnToLevelSelect()
    {
        Time.timeScale = 1f;
        GameManager.instance.ChangeScene("LevelSelect");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.instance.ChangeScene("MainMenu");
    }

    public void SwapBlockButton()
    {
        SpawnManager.instance.SwapBlock();
    }

    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OpenLoseScreen()
    {
        AudioManager.instance.PlayAudio(SoundType.Fail);
        Time.timeScale = 0f;
        _failScreen.SetActive(true);
    }

    private void OpenWinScreen()
    {
        AudioManager.instance.PlayAudio(SoundType.Win);
        Time.timeScale = 0f;
        _winScreen.SetActive(true);
    }

    private void ChangeWinCountdownText(int num)
    {
        _winTextCountdown.text = num.ToString();
    }
    
    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
