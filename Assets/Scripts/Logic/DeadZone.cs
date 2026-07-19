using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
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
        EventHandler.WhenGameLose();
    }
}
