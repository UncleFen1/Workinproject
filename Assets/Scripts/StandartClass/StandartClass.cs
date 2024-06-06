using UnityEngine;

public class StandartClass : MonoBehaviour
{
    private bool isStopClass = false, isRun = false;
    private void OnEnable()
    {

    }
    void Start()
    {
        SetClass();
    }
    private void SetClass()
    {
        if (!isRun)
        {
            isRun = true;
        }
    }
    void FixedUpdate()
    {
        if (isStopClass) { return; }
        if (!isRun) { SetClass(); }
        RunUpdate();
    }
    void Update()
    {

    }
    private void RunUpdate()
    {

    }
    private void OnDisable()
    {

    }
}
