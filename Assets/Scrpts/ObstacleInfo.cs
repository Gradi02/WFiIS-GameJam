using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    private Color originalColor;
    private List<Collider2D> collidersInContact = new List<Collider2D>();
    private bool isPlayerOne;

    public ObstacleMovement movement;
    public BoxCollider2D colliderO;
    public SpriteRenderer rendererO;
    private void Awake()
    {
        originalColor = rendererO.material.color;
        rendererO.sortingOrder = 1;
    }

    private void Start()
    {
        isPlayerOne = movement.isPlayerOne;   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled)
        {
            if (!collidersInContact.Contains(other))
            {
                collidersInContact.Add(other);
                rendererO.material.color = Color.red;
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
                rendererO.material.color = originalColor;
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
            rendererO.color = Color.white;
            colliderO.isTrigger = false;
            rendererO.sortingOrder = 0;
            enabled = false;
        }
    }
}