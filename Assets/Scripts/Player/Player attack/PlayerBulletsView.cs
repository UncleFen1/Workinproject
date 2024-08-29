using GamePlayer;
using UnityEngine;
using Zenject;

public class PlayerBulletsView : MonoBehaviour
{
    private PlayerController playerController;
    private Shooting playerShooting;

    [Inject]
    private void InitBindings(PlayerController pc)
    {
        playerController = pc;
        playerShooting = playerController.shooting;
    }

    void Start()
    {
        playerShooting.OnShoot += OnShoot;
        playerShooting.OnReloadFinished += OnReloadFinished;

        DisplayBullets();
    }

    private void OnShoot()
    {
        DisplayBullets();
    }

    private void OnReloadFinished()
    {
        DisplayBullets();
    }

    void DisplayBullets()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
        for (int i = 0; i < playerShooting.currentBulletsInCartridge; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }
    }
}
