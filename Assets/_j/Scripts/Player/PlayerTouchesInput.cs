using System.Collections;
using Cinemachine;
using Player;
using UnityEngine;

namespace GamePlayer
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerTouchesInput : MonoBehaviour
    {
        private PlayerController playerController;
        private MovePlayer movePlayer;
        private Shooting playerRange;
        private MeleeAttack playerMelee;
        private WeaponSwitcher weaponSwitcher;
        public CinemachineVirtualCamera cinemachineVirtualCamera;

        void Start()
        {
            playerController = GetComponent<PlayerController>();
            movePlayer = playerController.movePlayer;
            playerRange = playerController.shooting;
            playerMelee = playerController.playerMelee;

            StartCoroutine(LateInit());
        }

        IEnumerator LateInit()
        {
            yield return new WaitForSeconds(0.1f);

            cinemachineVirtualCamera = playerController.cinemachineVirtualCamera;
            weaponSwitcher = GetComponent<WeaponSwitcher>();

            if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
            {
                weaponSwitcher.SwitchWeapon();  // set default pistol

                cinemachineVirtualCamera.m_Lens.OrthographicSize *= 1.5f;
            }
        }

        void Update()
        {
            UpdateTouches();
        }

        void UpdateTouches()
        {
            foreach (Touch touch in Input.touches)
            {
                Vector2 v2 = Vector2.zero;
                if (touch.phase == TouchPhase.Moved)
                {
                    // Debug.Log($"touch.fingerId: {touch.fingerId}, TouchPhase.Moved: {touch.deltaPosition}");
                    v2 -= touch.deltaPosition;  // negative is correct
                    movePlayer.ProcessTouchCommands(v2.normalized);
                }

                if (touch.phase == TouchPhase.Began)
                {
                    // Debug.Log($"touch.fingerId: {touch.fingerId}, TouchPhase.Began, touch.position: {touch.position}");
                    if (weaponSwitcher.isMeleeActive)
                    {
                        playerMelee.ProcessTouchCommands(touch.position);
                    }
                    else
                    {
                        var screenVector = new Vector2(Screen.width, Screen.height);
                        playerRange.Attack(touch.position - (screenVector/2f), true);
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    // TODO _j should be something with touchId that it was in Moved status before
                    // to stop moving
                    movePlayer.ProcessTouchCommands(Vector2.zero);
                }
            }

            if (Input.touches.Length >= 3)
            {
                weaponSwitcher.SwitchWeapon();
            }
        }
    }
}
