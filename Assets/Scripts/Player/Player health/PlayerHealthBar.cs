using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image[] healthBarImage;

    void Update()
    {
        UpdateHealthBar();
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
