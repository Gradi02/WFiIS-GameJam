using UnityEngine;

public class trophyManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameLoopManager.instance.PlayerReachEnd(collision.gameObject);
        }
    }
}
