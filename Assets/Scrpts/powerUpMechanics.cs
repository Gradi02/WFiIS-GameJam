using System.Runtime.CompilerServices;
using UnityEngine;


public class powerUpMechanics : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.tag=="Player")
        {
             int index = Random.Range(1,4);
            switch (index)
            {
                case 1: //Table

                    Destroy(gameObject);
                    break;

                case 2: //Invincibility
                    Destroy(gameObject);
                    break;

                case 3: //ExtraCash

                    Destroy(gameObject);
                    break;

                case 4: //UNlimited dash

                    Destroy(gameObject);
                    break;
            
            }


        }
    }
}
