using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashscreenPanelSwitcher : MonoBehaviour
{
    
    public GameObject[] panels; 
    public float[] switchInterval; 
    private int currentPanelIndex = 0;

    void Start()
    {        
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentPanelIndex);
        }
                
        StartCoroutine(SwitchPanels());
    }

    private IEnumerator SwitchPanels()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(switchInterval[currentPanelIndex]);

            
            panels[currentPanelIndex].SetActive(false);

           
            currentPanelIndex = (currentPanelIndex + 1) % panels.Length;

            
            panels[currentPanelIndex].SetActive(true);
        }
    }
}
