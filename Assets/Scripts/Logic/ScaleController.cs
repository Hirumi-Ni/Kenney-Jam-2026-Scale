using UnityEngine;
using System.Collections;

public class ScaleController : MonoBehaviour
{
    private int _platformCollideCount;
    private Coroutine _startWinCondition;


    void Start()
    {
        _platformCollideCount = 0;
    }

    public void CheckScaleCondition()
    {
        if(_platformCollideCount >= 2 && !SpawnManager.instance.CheckListLength())
        {
            if (_startWinCondition == null) _startWinCondition = StartCoroutine(StartWinCountdown());
        }
        else
        {
            if (_startWinCondition == null) return;
            StopCoroutine(_startWinCondition);
            _startWinCondition = null;
            EventHandler.WhenWinCountdown(0);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            _platformCollideCount++;
            _platformCollideCount = Mathf.Clamp(_platformCollideCount, 0, 2);
            Debug.Log($"[Trigger Enter] Platform Count = {_platformCollideCount}");
            CheckScaleCondition();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            _platformCollideCount--;
            _platformCollideCount = Mathf.Clamp(_platformCollideCount, 0, 2);
            Debug.Log($"[Trigger Exit] Platform Count = {_platformCollideCount}");
            CheckScaleCondition();
        }
    }

    private IEnumerator StartWinCountdown()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
            EventHandler.WhenWinCountdown(i+1);
        }
        yield return new WaitForSeconds(.5f);
        EventHandler.WhenGameWin();
    }
}
