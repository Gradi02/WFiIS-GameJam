using System.Runtime.CompilerServices;
using UnityEngine;


public class powerUpMechanics : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();

        if(hitInfo.gameObject.tag=="Player")
        {
            int index = Random.Range(1,4);
            switch (index)
            {
                case 1: //Table
                    GameLoopManager.instance.AddEffect(player.isPlayerOne, GameLoopManager.Types.table);
                    Destroy(gameObject);
                    break;

                case 2: //Invincibility
                    GameLoopManager.instance.AddEffect(player.isPlayerOne, GameLoopManager.Types.invc);
                    Destroy(gameObject);
                    break;

                case 3: //ExtraCash
                    GameLoopManager.instance.AddEffect(player.isPlayerOne, GameLoopManager.Types.cash);
                    Destroy(gameObject);
                    break;
            
            }


        }
        

    }
}
