using TMPro;
using UnityEngine;

public class TimerScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _timerWinText;
    [SerializeField] private string _playerPrefsNameTime;
    private float _elapsedTime = 0f;

    private void OnEnable()
    {
        EventHandler.OnGameWin += BestTimes;
    }

    private void OnDisable()
    {
        EventHandler.OnGameWin -= BestTimes;
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        int _minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int _seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        int _milliseconds = Mathf.FloorToInt((_elapsedTime * 100) % 100);
        _timerText.text = $"{_minutes:00}:{_seconds:00}:{_milliseconds:00}";
    }

    public void BestTimes()
    {
        if (PlayerPrefs.HasKey(_playerPrefsNameTime))
        {
            if (_elapsedTime < PlayerPrefs.GetFloat(_playerPrefsNameTime))
            {
                PlayerPrefs.SetFloat(_playerPrefsNameTime, _elapsedTime);
            }
        }
        else PlayerPrefs.SetFloat(_playerPrefsNameTime, _elapsedTime);

        _timerWinText.text = $"Time Spent: {_timerText.text}";
        Debug.Log(_playerPrefsNameTime);
        Debug.Log(PlayerPrefs.GetFloat(_playerPrefsNameTime));
    }
}
