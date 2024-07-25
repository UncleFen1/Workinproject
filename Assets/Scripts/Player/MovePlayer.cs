using Inputs;
using Scene;
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
        private Vector2 inputDirection, residualDirection, tempInputDirection;
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
            residualDirection = _direction;
            //baseSpeed = moveSpeed;
            //
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
    }
}

