using GamePlayer;
using UnityEngine;
using Zenject;

public class PlayerBulletsView : MonoBehaviour
{
    public RectTransform bulletsHolder;
    public RectTransform reloadingHolder;
    public RectTransform weaponJammedHolder;
    public RectTransform weaponJamFixingHolder;

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
        if (weaponJammedHolder == null) Debug.LogWarning("no RectTransform for weaponJammedHolder is set");
        if (weaponJamFixingHolder == null) Debug.LogWarning("no RectTransform for weaponJamFixingHolder is set");

        playerShooting.OnShoot += OnShoot;
        playerShooting.OnReloadStarted += OnReloadStarted;
        playerShooting.OnReloadFinished += OnReloadFinished;
        playerShooting.OnJammed += OnJammed;
        playerShooting.OnJamFixingStarted += OnJamFixingStarted;
        playerShooting.OnJamFixingFinished += OnJamFixingFinished;

        playerWeaponSwitcher.OnWeaponSwitch += OnWeaponSwitch;

        OnWeaponSwitch();
    }

    private void OnShoot()
    {
        ShowBullets();
    }

    private void OnReloadStarted()
    {
        HideAll();
        ShowReload();
    }
    private void OnReloadFinished()
    {
        HideAll();
        ShowBullets();
    }
    
    private void OnJammed()
    {
        HideAll();
        ShowWeaponJammed();
    }

    private void OnWeaponSwitch()
    {
        if (playerWeaponSwitcher.isMeleeActive)
        {
            HideAll();
        }
        else
        {
            HideAll();
            if (playerShooting.IsJammed)
            {
                ShowWeaponJammed();
            }
            else
            {
                ShowBullets();
            }
        }
    }

    private void OnJamFixingStarted()
    {
        HideAll();
        ShowWeaponJamFixing();
    }
    private void OnJamFixingFinished()
    {
        HideAll();
        ShowBullets();
    }

    private void HideAll()
    {
        HideBullets();
        HideReload();
        HideWeaponJammed();
        HideWeaponJamFixing();
    }

    private void HideBullets()
    {
        bulletsHolder.gameObject.SetActive(false);
    }
    private void ShowBullets()
    {
        bulletsHolder.gameObject.SetActive(true);
        UpdateBulletsDisplay();
    }

    private void HideReload()
    {
        reloadingHolder.gameObject.SetActive(false);
    }
    private void ShowReload()
    {
        reloadingHolder.gameObject.SetActive(true);
    }
    
    private void HideWeaponJammed()
    {
        weaponJammedHolder.gameObject.SetActive(false);
    }
    private void ShowWeaponJammed()
    {
        weaponJammedHolder.gameObject.SetActive(true);
    }

    private void HideWeaponJamFixing()
    {
        weaponJamFixingHolder.gameObject.SetActive(false);
    }
    private void ShowWeaponJamFixing()
    {
        weaponJamFixingHolder.gameObject.SetActive(true);
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
