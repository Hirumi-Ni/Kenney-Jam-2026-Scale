using UnityEngine;
using System.Collections;
using TMPro;

public class ScaleController : MonoBehaviour
{
    private int _platformCollideCount;
    private Coroutine _startWinCondition;
    [SerializeField] private TMP_Text _winTextCountdown;
    [SerializeField] private GameObject _winScreen;

    void Start()
    {
        _platformCollideCount = 0;
        _winTextCountdown.text = 0.ToString();
    }

    public void CheckScaleCondition()
    {
        if(_platformCollideCount >= 2)
        {
            if (_startWinCondition == null) _startWinCondition = StartCoroutine(StartWinCountdown());
        }
        else
        {
            if (_startWinCondition == null) return;
            StopCoroutine(_startWinCondition);
            _startWinCondition = null;
            _winTextCountdown.text = 0.ToString();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Platform")) _platformCollideCount++;
        Mathf.Clamp(_platformCollideCount, 0, 2);
        Debug.Log($"[Trigger Enter] Platform Count = {_platformCollideCount}");
        CheckScaleCondition();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Platform")) _platformCollideCount--;
        Mathf.Clamp(_platformCollideCount, 0, 2);
        Debug.Log($"[Trigger Exit] Platform Count = {_platformCollideCount}");
        CheckScaleCondition();
    }

    private IEnumerator StartWinCountdown()
    {
        yield return new WaitForSeconds(2);
        _winTextCountdown.text = 1.ToString();
        yield return new WaitForSeconds(2);
        _winTextCountdown.text = 2.ToString();
        yield return new WaitForSeconds(2);
        _winTextCountdown.text = 3.ToString();
        yield return new WaitForSeconds(2);
        _winTextCountdown.text = 4.ToString();
        yield return new WaitForSeconds(2);
        _winTextCountdown.text = 5.ToString();
        yield return new WaitForSeconds(.5f);
        _winScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
