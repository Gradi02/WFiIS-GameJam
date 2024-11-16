using System.Runtime.CompilerServices;
using UnityEngine;


public class powerUpMechanics : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();

        if(hitInfo.gameObject.tag=="Player")
        {
            int index = Random.Range(1,3);
            switch (index)
            {
                case 1: //Table
                    if(player.isPlayerOne==true)
                    {
                        player.isTable = true;
                    } else {
                        player.isTable = true;
                    }
                    Destroy(gameObject);
                    break;

                case 2: //Invincibility
                    if (player.isPlayerOne == true)
                    {
                        player.isInvicible = true;  
                    } else {
                        player.isInvicible = true;
                    }
                    Destroy(gameObject);
                    break;

                case 3: //ExtraCash
                    if(player.isPlayerOne==true)
                    {
                        player.GLM.player1Cash += 10;
                    } else {
                        player.GLM.player2Cash += 10;
                    }
                    Destroy(gameObject);
                    break;

                case 4: //Unlimited dash
                    if (player.isPlayerOne == true)
                    {

                    } else {

                    }
                    Destroy(gameObject);
                    break;
            
            }


        }
        

    }
}
