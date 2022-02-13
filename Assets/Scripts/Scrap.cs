using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    public int Amount
    {
        get => amount;
        set
        {
            amount = value;

            UpdateSprite();
        }
    }

    [SerializeField]
    private Sprite small;
    [SerializeField]
    private Sprite medium;
    [SerializeField]
    private Sprite large;
    [SerializeField]
    private int smallThreshold = 5;
    [SerializeField]
    private int mediumThreshold = 20;
    [SerializeField]
    private int amount;

    private void Start()
    {
        if (amount <= 0)
        {
            Destroy(gameObject);
        }

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (amount < smallThreshold)
        {
            spriteRenderer.sprite = small;
        }
        else if (amount < mediumThreshold)
        {
            spriteRenderer.sprite = medium;
        }
        else
        {
            spriteRenderer.sprite = large;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("OnScrapPickup", amount);
            Destroy(gameObject);
        }
    }
}
