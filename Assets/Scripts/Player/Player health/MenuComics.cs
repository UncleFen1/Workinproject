using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuComics : MonoBehaviour

{
    public Sprite[] sprites; 
    public float switchInterval = 0.5f;
    public RectTransform uiImage;

    private Image image; 

    void Start()
    {
        image = GetComponent<Image>();
        if (sprites.Length > 0)
        {
            StartCoroutine(ChangeSprites1());
        }
        else
        {
            Debug.LogWarning("Sprites array is empty!");
        }
    }

    private IEnumerator ChangeSprites1()
    {
        int currentIndex = 0;
        
        while (true)
        {
            image.sprite = sprites[currentIndex];
            currentIndex = (currentIndex + 1) % sprites.Length;
            yield return new WaitForSeconds(switchInterval); 

            uiImage.position = Camera.main.WorldToScreenPoint(transform.position);
        }
                               
    }
}
