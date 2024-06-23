using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public enum Mode
    {
        Mode1,
        Mode2,
        Mode3,
    }
    public class InputPlayerExecutor : IInputPlayerExecutor
    {
        public Camera Camera { get { return camera; }set { camera = value; } }
        private Camera camera;
        private Vector2 tempRaycastHit;
        private Mode[] modes;
        private int countMode = 0;
        private bool isTrigerClick = true;
        private InputButtonData inputButtonData;
        private InputMouseData inputMouseData;
        private MausExecutor mausExecutor;
        private InputActions inputActions;

        public Action<InputMouseData> OnMoveMouse { get { return onMoveMouse; } set { onMoveMouse = value; } }
        private Action<InputMouseData> onMoveMouse;
        public Action<InputMouseData> OnMousePoint { get { return onMousePoint; } set { onMousePoint = value; } }
        private Action<InputMouseData> onMousePoint;
        public Action<InputMouseData> OnStartPressMouse { get { return onStartPressMouse; } set { onStartPressMouse = value; } }
        private Action<InputMouseData> onStartPressMouse;
        public Action<InputMouseData> OnCancelPressMouse { get { return onCancelPressMouse; } set { onCancelPressMouse = value; } }
        private Action<InputMouseData> onCancelPressMouse;
        public Action<InputButtonData> OnMoveButton { get { return onMoveButton; } set { onMoveButton = value; } }
        private Action<InputButtonData> onMoveButton;
        public Action<InputButtonData> OnStartPressButton { get { return onStartPressButton; } set { onStartPressButton = value; } }
        private Action<InputButtonData> onStartPressButton;
        public Action<InputButtonData> OnCancelPressButton { get { return onCancelPressButton; } set { onCancelPressButton = value; } }
        private Action<InputButtonData> onCancelPressButton;

        public void Enable()
        {
            inputButtonData = new InputButtonData();
            inputMouseData = new InputMouseData();

            inputButtonData.Modes = modes;
            inputActions = new InputActions();
            mausExecutor = new MausExecutor();

            if (inputActions != null)
            {
                EnableMouse();
                EnableButton();
                //����� UI
                {
                    inputActions.UIMap.WASDUI.started += contex => inputButtonData.WASD = contex.ReadValue<Vector2>();
                    inputActions.UIMap.WASDUI.performed += contex => inputButtonData.WASD = contex.ReadValue<Vector2>();
                    inputActions.UIMap.WASDUI.canceled += contex => inputButtonData.WASD = contex.ReadValue<Vector2>();
                }

                inputActions.Enable();
            }
        }
        public void OnDisable()
        {
            inputActions.Disable();
        }
        private void SelectMoveMode()
        {
            if (inputButtonData.Tab != 0)
            {
                if (isTrigerClick)
                {
                    isTrigerClick = false;
                    countMode++;
                    if (countMode >= modes.Length) { countMode = 0; }

                    for (int i = 0; i < modes.Length; i++)
                    {
                        if ((int)modes[i] == countMode)
                        {
                            inputButtonData.ModeAction = (Mode)countMode;
                        }
                    }
                    isTrigerClick = true;
                }
            }
        }
        #region Mouse
        private void EnableMouse()
        {
            //Key Mouse
            inputActions.KeyMap.MousePosition.started += contex => { inputMouseData.MouseAxes = contex.ReadValue<Vector2>(); inputMouseData.MousePosition = Mouse.current.position.ReadValue(); MoveMouse(inputMouseData); };
            inputActions.KeyMap.MousePosition.performed += contex => { inputMouseData.MouseAxes = contex.ReadValue<Vector2>(); inputMouseData.MousePosition = Mouse.current.position.ReadValue(); };
            inputActions.KeyMap.MousePosition.canceled += contex => { inputMouseData.MouseAxes = contex.ReadValue<Vector2>(); inputMouseData.MousePosition = Mouse.current.position.ReadValue(); };

            inputActions.KeyMap.MouseLeftButton.started += context => { inputMouseData.MouseLeftButton = context.ReadValue<float>(); StartPressMouse(inputMouseData); };
            inputActions.KeyMap.MouseLeftButton.performed += context => { inputMouseData.MouseLeftButton = context.ReadValue<float>(); };
            inputActions.KeyMap.MouseLeftButton.canceled += context => { inputMouseData.MouseLeftButton = context.ReadValue<float>(); CancelPressMouse(inputMouseData); };

            inputActions.KeyMap.MouseMiddleButton.started += context => { inputMouseData.MouseMiddleButton = context.ReadValue<float>(); StartPressMouse(inputMouseData); };
            inputActions.KeyMap.MouseMiddleButton.performed += context => { inputMouseData.MouseMiddleButton = context.ReadValue<float>(); };
            inputActions.KeyMap.MouseMiddleButton.canceled += context => { inputMouseData.MouseMiddleButton = context.ReadValue<float>(); CancelPressMouse(inputMouseData); };

            inputActions.KeyMap.MouseRightButton.started += context => { inputMouseData.MouseRightButton = context.ReadValue<float>(); StartPressMouse(inputMouseData); };
            inputActions.KeyMap.MouseRightButton.performed += context => { inputMouseData.MouseRightButton = context.ReadValue<float>(); };
            inputActions.KeyMap.MouseRightButton.canceled += context => { inputMouseData.MouseRightButton = context.ReadValue<float>(); CancelPressMouse(inputMouseData); };
        }
        private void MoveMouse(InputMouseData inputMouseData)
        {
            if (camera != null)
            {
                inputMouseData =mausExecutor.MausMoveControl(inputMouseData, camera);
                onMousePoint?.Invoke(inputMouseData);
            }

            onMoveMouse?.Invoke(inputMouseData);
        }
        private void StartPressMouse(InputMouseData inputMouseData)
        {
            onStartPressMouse?.Invoke(inputMouseData);
        }
        private void CancelPressMouse(InputMouseData inputMouseData)
        {
            onCancelPressMouse?.Invoke(inputMouseData);
        }
        #endregion
        #region Button
        private void EnableButton()
        {
            //����� Key Button

            inputActions.KeyMap.WASD.started += contex => { inputButtonData.WASD = contex.ReadValue<Vector2>(); MoveButton(inputButtonData); };
            inputActions.KeyMap.WASD.performed += contex => { inputButtonData.WASD = contex.ReadValue<Vector2>(); MoveButton(inputButtonData); };
            inputActions.KeyMap.WASD.canceled += contex => { inputButtonData.WASD = contex.ReadValue<Vector2>(); MoveButton(inputButtonData); };

            inputActions.KeyMap.Tab.started += context => { inputButtonData.Tab = context.ReadValue<float>(); SelectMoveMode(); };
            inputActions.KeyMap.Tab.performed += context => { inputButtonData.Tab = context.ReadValue<float>(); };
            inputActions.KeyMap.Tab.canceled += context => { inputButtonData.Tab = context.ReadValue<float>(); };

            inputActions.KeyMap.Esc.started += context => { inputButtonData.Esc = context.ReadValue<float>(); StartPressButton(inputButtonData); };
            inputActions.KeyMap.Esc.performed += context => { inputButtonData.Esc = context.ReadValue<float>(); };
            inputActions.KeyMap.Esc.canceled += context => { inputButtonData.Esc = context.ReadValue<float>(); CancelPressButton(inputButtonData); };

            inputActions.KeyMap.E.started += context => { inputButtonData.E = context.ReadValue<float>(); StartPressButton(inputButtonData); };
            inputActions.KeyMap.E.performed += context => { inputButtonData.E = context.ReadValue<float>(); };
            inputActions.KeyMap.E.canceled += context => { inputButtonData.E = context.ReadValue<float>(); CancelPressButton(inputButtonData); };

            inputActions.KeyMap.F.started += context => { inputButtonData.F = context.ReadValue<float>(); StartPressButton(inputButtonData); };
            inputActions.KeyMap.F.performed += context => { inputButtonData.F = context.ReadValue<float>(); };
            inputActions.KeyMap.F.canceled += context => { inputButtonData.F = context.ReadValue<float>(); CancelPressButton(inputButtonData); };

            inputActions.KeyMap.R.started += context => { inputButtonData.R = context.ReadValue<float>(); StartPressButton(inputButtonData); };
            inputActions.KeyMap.R.performed += context => { inputButtonData.R = context.ReadValue<float>(); };
            inputActions.KeyMap.R.canceled += context => { inputButtonData.R = context.ReadValue<float>(); CancelPressButton(inputButtonData); };

            inputActions.KeyMap.Space.started += context => { inputButtonData.Space = context.ReadValue<float>(); StartPressButton(inputButtonData); };
            inputActions.KeyMap.Space.performed += context => { inputButtonData.Space = context.ReadValue<float>(); };
            inputActions.KeyMap.Space.canceled += context => { inputButtonData.Space = context.ReadValue<float>(); CancelPressButton(inputButtonData); };
        }
        private void MoveButton(InputButtonData inputData)
        {
            onMoveButton?.Invoke(inputData);
        }
        private void StartPressButton(InputButtonData inputData)
        {
            onStartPressButton?.Invoke(inputData);
        }
        private void CancelPressButton(InputButtonData inputData)
        {
            onCancelPressButton?.Invoke(inputData);
        }
        #endregion
    }
}

