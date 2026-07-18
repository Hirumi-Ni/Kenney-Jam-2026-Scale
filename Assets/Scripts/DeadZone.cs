using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject _failScreen;

    void Start()
    {
        _failScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayObject"))
        {
            Debug.Log("You Lose");
            Destroy(other);
            StartCoroutine(LoseGame());
        }
    }

    private IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(2);
        _failScreen.SetActive(true);
    }
}
