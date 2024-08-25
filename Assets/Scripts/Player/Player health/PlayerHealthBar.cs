using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image[] healthBarImage;

    private Canvas parentCanvas;

    // TODO _j should be done where the Health value is updated, or add events. This code stinks
    void Update()
    {
        if (parentCanvas == null)
        {
            parentCanvas = GetComponentInParent<Canvas>();
        }

        // paused
        if (Time.timeScale == 0)
        {
            if (parentCanvas != null)
            {
                parentCanvas.enabled = false;
            }
        }
        else
        {
            if (parentCanvas != null)
            {
                parentCanvas.enabled = true;
            }
            UpdateHealthBar();
        }
    }
    void UpdateHealthBar()
    {
        if (playerHealth != null)
        {
            int visibleSegments = Mathf.RoundToInt((float)playerHealth.currentPlayerHealth / playerHealth.maxPlayerHealth * healthBarImage.Length);

            for (int i = 0; i < healthBarImage.Length; i++)
            {
                healthBarImage[i].gameObject.SetActive(i < visibleSegments);
            }
           
        }
    }
}
