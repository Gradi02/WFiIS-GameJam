using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameLoopManager.instance.ResetPlayer(collision.gameObject);
        }
    }
}
