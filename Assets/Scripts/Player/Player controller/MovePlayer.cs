using Inputs;
using Roulettes;
using OldSceneNamespace;
using UnityEngine;
using Zenject;

namespace Player
{
    public class MovePlayer : MonoBehaviour
    {
        [SerializeField][Range(0, 10)] private float moveSpeed = 5f;
        //[SerializeField][Range(0, 1f)] private float defaultSpeed = 0.5f;
        [SerializeField][Range(0, 100)] private float speedTurn = 10f;
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

        private PlayerRoulette playerRoulette;
        [Inject]
        private void InitBindings(PlayerRoulette pr) {
            playerRoulette = pr;
            ApplyRouletteModifiers();
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
            inputs.Enable();
            inputs.OnMoveButton += MoveButton;
        }
        private void PauseGame(bool _isRun)
        {
            isStopClass = _isRun;
        }
        private void MoveButton(InputButtonData data)
        {
            inputDirection = data.WASD;
        }
        void Start()
        {
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
            Move();
            RunUpdate();
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
            if (moveDirection.sqrMagnitude > 0) { 
                deltaRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            }
            directionRotation = Quaternion.RotateTowards(transform.rotation, deltaRotation, speedTurn);
            rbThisObject.MoveRotation(directionRotation);
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

        // TODO _j not sure if needed, but public methods added to demonstrate interaction with environment
        public float GetMovementSpeed() {
            return moveSpeed;
        }

        public void SetMovementSpeed(float speed) {
            moveSpeed = speed;
        }
    }
}

