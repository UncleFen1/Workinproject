using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasHack : MonoBehaviour
{
    public Canvas playerCanvas;
    public Button hideButton;
    public Button showButton;

    void Start()
    {
        playerCanvas.gameObject.SetActive(true);

        if (hideButton != null)
        {
            hideButton.onClick.AddListener(HidePlayerCanvas);
        }  
        
        if (showButton != null)
        {
            showButton.onClick.AddListener(ShowPlayerCanvas);
        }
    }

    void OnDestroy()
    {
        if (hideButton != null)
        {
            hideButton.onClick.RemoveListener(HidePlayerCanvas);
        }

        if (showButton != null)
        {
            showButton.onClick.RemoveListener(ShowPlayerCanvas);
        }
    }

    private void HidePlayerCanvas()
    {
        playerCanvas.gameObject.SetActive(false);
    }

    private void ShowPlayerCanvas()
    {
        playerCanvas.gameObject.SetActive(true);
    }
}