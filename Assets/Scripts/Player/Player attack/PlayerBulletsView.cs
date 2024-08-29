using GamePlayer;
using UnityEngine;
using Zenject;

public class PlayerBulletsView : MonoBehaviour
{
    private PlayerController playerController;
    private Shooting playerShooting;
    private WeaponSwitcher playerWeaponSwitcher;

    [Inject]
    private void InitBindings(PlayerController pc)
    {
        playerController = pc;
        playerShooting = playerController.shooting;
        playerWeaponSwitcher = playerController.weaponSwitcher;
    }

    void Start()
    {
        playerShooting.OnShoot += OnShoot;
        playerShooting.OnReloadFinished += OnReloadFinished;

        playerWeaponSwitcher.OnWeaponSwitch += () => { DisplayBullets(); };

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

            if (i < playerShooting.currentBulletsInCartridge
                && playerWeaponSwitcher.isMeleeActive == false)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
