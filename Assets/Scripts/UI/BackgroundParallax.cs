using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour

    //script working just for main menu background, so i hope u will not curse me
{
    public float parallaxFactor;
    private RectTransform rectTransform;
    private Vector3 previousMousePosition;

    public float minX;
    public float maxX;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        previousMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        float deltaX = currentMousePosition.x - previousMousePosition.x;

        float newX = rectTransform.anchoredPosition.x + deltaX * parallaxFactor;

        newX = Mathf.Clamp(newX, minX, maxX);

        rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);

        previousMousePosition = currentMousePosition;
    }
}