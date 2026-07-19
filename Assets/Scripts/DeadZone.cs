using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject _failScreen;

    void Start()
    {
        _failScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayObject"))
        {
            Debug.Log("You Lose");
            Destroy(collision);
            StartCoroutine(LoseGame());
        }
    }

    private IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(.5f);
        _failScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
