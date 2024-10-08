using Inputs;
using Roulettes;
using OldSceneNamespace;
using UnityEngine;
using Zenject;
using GamePlayer;

namespace Player
{
    public class MovePlayer : MonoBehaviour
    {
        [SerializeField][Range(0, 10)] private float moveSpeed = 5f;
        //[SerializeField][Range(0, 1f)] private float defaultSpeed = 0.5f;
        [SerializeField][Range(0, 100)] private float speedTurn = 10f;

        [Header("Звуки эффектов")]
        [SerializeField] private AudioClip footstepsEffectClip;
        private AudioSource effectAudioSource;

        bool isMovementStarted = false;

        private Rigidbody2D rbThisObject;
        private Vector3 moveDirection;
        private Quaternion deltaRotation, directionRotation;
        private Vector2 inputDirection, tempInputDirection;
        //
        private bool isStopClass = false, isRun = false;
        //
        private ISceneExecutor scenes;
        private IInputPlayerExecutor inputs;

        [Inject]
        public void Init(ISceneExecutor _scenes, IInputPlayerExecutor _inputs)
        {
            scenes = _scenes;
            inputs = _inputs;
        }

        private Animator playerWalkAnimator;

        private PlayerRoulette playerRoulette;
        [Inject]
        private void InitBindings(PlayerRoulette pr, PlayerController pc) {
            playerRoulette = pr;
            ApplyRouletteModifiers();

            playerWalkAnimator = pc.walkAnimator;
        }
        void ApplyRouletteModifiers()
        {
            var mod = playerRoulette.playerKindsMap[PlayerKind.MovementSpeed].modifier;
            switch (mod)
            {
                case PlayerModifier.Unchanged:
                    break;
                case PlayerModifier.Increased:
                    moveSpeed *= 2;
                    break;
                case PlayerModifier.Decreased:
                    moveSpeed /= 2;
                    break;
                default:
                    Debug.LogWarning("_j unknown modifier");
                    break;
            }
        }

        private void OnEnable()
        {
            scenes.OnPauseGame += PauseGame;
            if (!(Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform))
            {
                inputs.Enable();
                inputs.OnMoveButton += MoveButton;
            }
        }
        private void PauseGame(bool _isRun)
        {
            isStopClass = _isRun;
        }
        private void MoveButton(InputButtonData data)
        {
            inputDirection = data.WASD;
        }
        private void EscButtonPressed()
        {
            var gameUIPanel = FindObjectOfType<UI.LvlGndPanel>();
            var menuUIPanel = FindObjectOfType<UI.LvlButtonPanel>();
            var settingsUIPanel = FindObjectOfType<UI.LvlSettingsPanel>();

            if (gameUIPanel.IsButtonActiveAndEnabled())
            {
                gameUIPanel.PressPauseButton();
            }
            else if (menuUIPanel.IsContinueButtonActiveAndEnabled())
            {
                menuUIPanel.PressContinueLevelButton();
            }
            else if (settingsUIPanel.IsReturnButtonActiveAndEnabled())
            {
                settingsUIPanel.PressReturnButton();
            }
        }

        void Start()
        {
            SetupAudio();

            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                rbThisObject = GetComponent<Rigidbody2D>();
                if (!(rbThisObject is Rigidbody2D)) { this.gameObject.AddComponent<Rigidbody2D>(); }
                isRun = true;
            }
        }
        void FixedUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            // TestyWASD();
            Move();
            RunUpdate();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EscButtonPressed();
            }
        }

        void TestyWASD()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            if (Mathf.Abs(h) > 1e-6 || Mathf.Abs(v) > 1e-6)
            {
                var v2 = new Vector2(h, v);
                // MoveExecutor(v2);
                inputDirection = v2.normalized;
            }
        }

        void SetupAudio()
        {
            if (effectAudioSource == null)
            {
                effectAudioSource = gameObject.AddComponent<AudioSource>();
            }

            effectAudioSource.clip = footstepsEffectClip;
            effectAudioSource.loop = true;

            effectAudioSource.volume = 1f;
            Debug.LogWarning("footsteps volume set to MAX due to source quite level");
            // scenes.OnSetSettingsAudioScene += (SettingsScene settingsScene) => {
            //     effectAudioSource.volume = settingsScene.EffectValum;   // VALUM!!!
            // };
        }

        private void Move()
        {
            if (inputDirection.x == 0 && inputDirection.y == 0)
            {
                tempInputDirection.x = 0;
                tempInputDirection.y = 0;
                MoveExecutor(tempInputDirection);
            }

            if (inputDirection.x != 0 && inputDirection.y == 0)
            {
                tempInputDirection.x = inputDirection.x;
                tempInputDirection.y = 0;
                MoveExecutor(tempInputDirection);
            }

            if (inputDirection.x == 0 && inputDirection.y != 0)
            {
                tempInputDirection.x = 0;
                tempInputDirection.y = inputDirection.y;
                MoveExecutor(tempInputDirection);
            }

            if (inputDirection.x > 0 && inputDirection.y > 0)
            {
                tempInputDirection.x = 0.71f;
                tempInputDirection.y = 0.71f;
                MoveExecutor(tempInputDirection);
            }

            if (inputDirection.x > 0 && inputDirection.y < 0)
            {
                tempInputDirection.x = 0.71f;
                tempInputDirection.y = -0.71f;
                MoveExecutor(tempInputDirection);
            }

            if (inputDirection.x < 0 && inputDirection.y > 0)
            {
                tempInputDirection.x = -0.71f;
                tempInputDirection.y = 0.71f;
                MoveExecutor(tempInputDirection);
            }

            if (inputDirection.x < 0 && inputDirection.y < 0)
            {
                tempInputDirection.x = -0.71f;
                tempInputDirection.y = -0.71f;
                MoveExecutor(tempInputDirection);
            }
        }

        private void MoveExecutor(Vector2 _direction)
        {
            rbThisObject.velocity = _direction * moveSpeed;
            moveDirection = rbThisObject.velocity;
            if (moveDirection.sqrMagnitude > 0)
            {
                deltaRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                
                if (!isMovementStarted)
                {
                    OnMovementStart();
                }
            }
            else
            {
                if (isMovementStarted)
                {
                    OnMovementStop();
                }
            }
            directionRotation = Quaternion.RotateTowards(transform.rotation, deltaRotation, speedTurn);
            rbThisObject.MoveRotation(directionRotation);
        }

        void OnMovementStart()
        {
            isMovementStarted = true;
            effectAudioSource.Play();
            playerWalkAnimator.SetBool("Stop", false);
        }

        void OnMovementStop()
        {
            isMovementStarted = false;
            effectAudioSource.Stop();
            playerWalkAnimator.SetBool("Stop", true);
        }

        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }

        public float GetMovementSpeed() {
            return moveSpeed;
        }

        public void SetMovementSpeed(float speed) {
            moveSpeed = speed;
        }

        public void ProcessTouchCommands(Vector2 v2)
        {
            // MoveExecutor(v2);
            inputDirection = v2;
        }
    }
}
