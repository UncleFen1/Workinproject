using GamePlayer;
using UnityEngine;
using Zenject;

public class PlayerBulletsView : MonoBehaviour
{
    public RectTransform bulletsHolder;
    public RectTransform reloadingHolder;

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
        if (bulletsHolder == null) Debug.LogWarning("no RectTransform for bulletsHolder is set");
        if (reloadingHolder == null) Debug.LogWarning("no RectTransform for reloadingHolder is set");

        playerShooting.OnShoot += OnShoot;
        playerShooting.OnReloadStarted += OnReloadStarted;
        playerShooting.OnReloadFinished += OnReloadFinished;

        playerWeaponSwitcher.OnWeaponSwitch += OnWeaponSwitch;

        OnWeaponSwitch();
    }

    private void OnShoot()
    {
        UpdateBulletsDisplay();
    }

    private void OnReloadStarted()
    {
        HideBullets();
        ShowReload();
    }
    private void OnReloadFinished()
    {
        ShowBullets();
        HideReload();
        UpdateBulletsDisplay();
    }
    private void OnWeaponSwitch()
    {
        if (playerWeaponSwitcher.isMeleeActive)
        {
            HideBullets();
            HideReload();
        }
        else
        {
            ShowBullets();
            HideReload();
            UpdateBulletsDisplay();
        }
    }

    private void HideBullets()
    {
        bulletsHolder.gameObject.SetActive(false);
    }
    private void ShowBullets()
    {
        bulletsHolder.gameObject.SetActive(true);
    }

    private void HideReload()
    {
        reloadingHolder.gameObject.SetActive(false);
    }
    private void ShowReload()
    {
        reloadingHolder.gameObject.SetActive(true);
    }

    void UpdateBulletsDisplay()
    {
        for (int i = 0; i < bulletsHolder.childCount; i++)
        {
            var child = bulletsHolder.GetChild(i);

            if (i < playerShooting.currentBulletsInCartridge)
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
