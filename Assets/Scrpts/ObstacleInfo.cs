using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    private Color originalColor;
    private List<Collider2D> collidersInContact = new List<Collider2D>();

    private void Awake()
    {
        originalColor = GetComponent<SpriteRenderer>().material.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!collidersInContact.Contains(other))
        {
            collidersInContact.Add(other);
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (collidersInContact.Contains(other))
        {
            collidersInContact.Remove(other);
        }

        if (collidersInContact.Count == 0)
        {
            GetComponent<SpriteRenderer>().material.color = originalColor;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && collidersInContact.Count == 0)
        {
            //DISABLE MOVEMENT;
            Debug.Log("PLACED");
            enabled = false;
        }
    }
}