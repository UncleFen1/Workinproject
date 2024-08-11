using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallsRendering : MonoBehaviour
{
    private TilemapRenderer spriteRenderer;
    public GameObject object1;
    private TilemapRenderer spriteRenderer1;

    private void Start()
    {
        spriteRenderer = GetComponent<TilemapRenderer>();

        if (object1 != null )
        {
            spriteRenderer1 = object1.GetComponent<TilemapRenderer>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            spriteRenderer.enabled = !spriteRenderer.enabled;

            if (spriteRenderer1 != null)
            {
                spriteRenderer1.enabled = !spriteRenderer.enabled;
            }

            
        }
    }
}