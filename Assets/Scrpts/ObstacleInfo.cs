using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    private Color originalColor;
    private List<Collider2D> collidersInContact = new List<Collider2D>();
    private bool isPlayerOne;


    public GameObject[] elements;

    public ObstacleMovement movement;
    public BoxCollider2D colliderO;
    public SpriteRenderer rendererO;
    private void Awake()
    {
        rendererO.sortingOrder = 1;
    }

    private void Start()
    {
        isPlayerOne = movement.isPlayerOne;
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].GetComponent<SpriteRenderer>().material.color = Color.green;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled)
        {
            if (!collidersInContact.Contains(other))
            {
                collidersInContact.Add(other);

                for(int i = 0; i<elements.Length; i++)
                {
                    elements[i].GetComponent<SpriteRenderer>().material.color = Color.red;
                }
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
                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i].GetComponent<SpriteRenderer>().material.color = Color.green;
                }
            }
        }
    }


    private void Update()
    {
        bool input = (isPlayerOne == true ? Input.GetKeyDown(KeyCode.LeftShift) : Input.GetKeyDown(KeyCode.RightShift));

        if (input && collidersInContact.Count == 0)
        {
            movement.Stop();
            movement.enabled = false;
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i].GetComponent<SpriteRenderer>().material.color = Color.white;
                elements[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            //colliderO.isTrigger = false;
            //rendererO.sortingOrder = 0;
            GameLoopManager.instance.BuyB(isPlayerOne);
            enabled = false;
        }
    }
}