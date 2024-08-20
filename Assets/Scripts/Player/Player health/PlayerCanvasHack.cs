using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasHack : MonoBehaviour
{
    public Canvas playerCanvas;
    public GameObject triggerImage;

    void Start()
    {
        if (triggerImage != null && !triggerImage.activeSelf)
        {
            playerCanvas.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (triggerImage.activeSelf)
        {
            playerCanvas.gameObject.SetActive(false);
        }
        else
        {
            playerCanvas.gameObject.SetActive(true);
        }
    }
}