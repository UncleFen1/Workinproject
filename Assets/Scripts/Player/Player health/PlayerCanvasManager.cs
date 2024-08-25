using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasManager : MonoBehaviour
{
    public static PlayerCanvasManager Instance;

    public Canvas playerCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPlayerCanvas()
    {
        playerCanvas.gameObject.SetActive(true);
    }

    public void HidePlayerCanvas()
    {
        playerCanvas.gameObject.SetActive(false);
    }
}
