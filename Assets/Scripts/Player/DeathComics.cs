using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathComics : MonoBehaviour

{
    public Sprite[] sprites; 
    public float switchInterval = 0.5f; 

    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            StartCoroutine(ChangeSprites());
        }
        else
        {
            Debug.LogWarning("Sprites array is empty!");
        }
    }

    private IEnumerator ChangeSprites()
    {
        for (int currentIndex = 0; currentIndex < sprites.Length; currentIndex++) 
        {
            spriteRenderer.sprite = sprites[currentIndex]; 
            yield return new WaitForSeconds(switchInterval); 
        }
                
        spriteRenderer.sprite = null;

        // spriteRenderer.sprite = sprites[sprites.Length - 1]; - to stay on last sprite

    }
}
