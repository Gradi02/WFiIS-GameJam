using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    private Color originalColor;
    private List<Collider2D> collidersInContact = new List<Collider2D>();
    private bool isPlayerOne;

    private void Awake()
    {
        originalColor = GetComponent<SpriteRenderer>().material.color;
        GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    private void Start()
    {
        isPlayerOne = GetComponent<ObstacleMovement>().isPlayerOne;   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled)
        {
            if (!collidersInContact.Contains(other))
            {
                collidersInContact.Add(other);
                GetComponent<SpriteRenderer>().material.color = Color.red;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (enabled)
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
    }


    private void Update()
    {
        bool input = (isPlayerOne == true ? Input.GetKeyDown(KeyCode.LeftShift) : Input.GetKeyDown(KeyCode.RightShift));

        if (input && collidersInContact.Count == 0)
        {
            GetComponent<ObstacleMovement>().enabled = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            enabled = false;
        }
    }
}