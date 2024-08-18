using Cinemachine;
using Player;
using Roulettes;
using UnityEngine;
using Zenject;

namespace GamePlayer
{
    public class PlayerController : MonoBehaviour
    {
        public MovePlayer movePlayer;
        public Shooting shooting;
        public MeleeAttack playerMelee;
        public PlayerHealth playerHealth;
        public CinemachineVirtualCamera cinemachineVirtualCamera;

        private PlayerRoulette playerRoulette;
        [Inject]
        private void InitBindings(PlayerRoulette pr)
        {
            playerRoulette = pr;
            ApplyRouletteModifiers();
        }

        void Start()
        {
            if (movePlayer == null) movePlayer = gameObject.GetComponentInChildren<MovePlayer>(true);
            if (movePlayer == null) Debug.LogError("MovePlayer component isn't found");

            if (shooting == null) shooting = gameObject.GetComponentInChildren<Shooting>(true);
            if (shooting == null) Debug.LogError("Shooting component isn't found");

            if (playerMelee == null) playerMelee = gameObject.GetComponentInChildren<MeleeAttack>(true);
            if (playerMelee == null) Debug.LogError("MeleeAttack component isn't found");

            if (playerHealth == null) playerHealth = gameObject.GetComponentInChildren<PlayerHealth>(true);
            if (playerHealth == null) Debug.LogError("PlayerHealth component isn't found");

            if (cinemachineVirtualCamera == null) cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>(true);
            if (cinemachineVirtualCamera == null) Debug.LogError("CinemachineVirtualCamera component isn't found");

            ApplyRouletteModifiers();
        }

        void ApplyRouletteModifiers()
        {
            if (cinemachineVirtualCamera)
            {
                // TODO _j for some reason it's launched twice, one is before FindObjectOfType()...
                var mod = playerRoulette.playerKindsMap[PlayerKind.CameraZoom].modifier;
                switch (mod)
                {
                    case PlayerModifier.Unchanged:
                        break;
                    case PlayerModifier.Increased:
                        cinemachineVirtualCamera.m_Lens.OrthographicSize = 13;
                        break;
                    case PlayerModifier.Decreased:
                        cinemachineVirtualCamera.m_Lens.OrthographicSize = 5;
                        break;
                    default:
                        Debug.LogWarning("_j unknown modifier");
                        break;
                }
            }

            // TODO _j other modifiers should be applied here, but there are more changes that can afford now
        }
    }
}
