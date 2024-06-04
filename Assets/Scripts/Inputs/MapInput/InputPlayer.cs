//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Inputs/MapInput/InputPlayer.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Inputs
{
    public partial class @InputActions: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputPlayer"",
    ""maps"": [
        {
            ""name"": ""KeyMap"",
            ""id"": ""568ffe23-1b0c-4a99-b77f-81f251671ce7"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""563c4a4b-4ce2-4a85-b706-fc7f89d7d8e6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseLeftButton"",
                    ""type"": ""Button"",
                    ""id"": ""6d4ea16a-7d43-4b6b-85fb-264788987ddf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseMiddleButton"",
                    ""type"": ""Button"",
                    ""id"": ""59d77ace-75bb-4068-b0ae-40b218775ce5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseRightButton"",
                    ""type"": ""Button"",
                    ""id"": ""1bcb9a40-41a7-4ea0-8315-aaeba551e8b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WASD"",
                    ""type"": ""Value"",
                    ""id"": ""f182d8cb-cc41-43a5-89d1-03637540bfc7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""6beaac2e-8c46-43e5-a2f7-f018cbe33a19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""101ad3bb-61ff-42ff-bae7-f9f19726a1ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""E"",
                    ""type"": ""Button"",
                    ""id"": ""b6e5b87f-98d4-48f3-93b6-1ad4fc800582"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""F"",
                    ""type"": ""Button"",
                    ""id"": ""2fd314b5-5ed4-4c3e-b5d7-eeef597bbeb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""R"",
                    ""type"": ""Button"",
                    ""id"": ""f0a08ef6-6585-4524-b9b9-901e433c6704"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""a84b2544-f3a3-4407-935c-1453de89bbdc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5388052d-93d2-4c6c-9e91-71c35da12a2b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a4e5300-1ef5-490b-bddc-ac6478909e5b"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""516363a4-18c9-4579-b5d7-22f7c8eaa07c"",
                    ""path"": ""<Joystick>/{Hatswitch}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4582dd2d-2e25-4c52-985d-9589dfd9bba2"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMiddleButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e50a5853-8f67-4c97-9739-5f0388243231"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRightButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5031799c-c795-499d-b006-c282f4454527"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f902cca-d128-409f-a1f8-e295c9423e64"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4571859b-5f63-4d3f-9e77-3865ec81a3bc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db303c6a-54d3-4d7a-a052-4fed68a5cad6"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""97ee8106-e916-477b-b6e6-cf02ac1163b5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""93058474-6e71-4fe4-ad85-e6739f2a0d40"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f378f23c-9bd6-4c47-aac2-d75310f0f9d1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""11a36356-0c43-4bc9-9c08-df896ec8f7e3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d74df8dd-e286-4ba4-a5c0-d0e2c0fce48f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f003026d-c162-477d-91fa-d8b58c392e25"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4199e8c5-4e81-4cd3-8ffc-b9edc05cfa72"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0694361-7696-4cb0-9055-917fb02d1bd7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3dc778fa-cab8-41ef-9115-d34a71567390"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeftButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIMap"",
            ""id"": ""6a26a4f4-dc1f-4018-ab00-52a50643f357"",
            ""actions"": [
                {
                    ""name"": ""WASDUI"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cb50cfb1-8249-4ea3-a520-9addc6f901c2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClickUI"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6fae29f9-5b67-471e-bc48-ac560f2fee5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ac666f55-8e67-4b40-989b-1dc453a50aee"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""2b747a40-87b9-40e1-8912-8e057d6d4017"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""40672866-1b5c-4315-bd06-309ef777fe2b"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3489b5a5-de55-46c8-856a-cbaf4ba8a371"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""50806e66-b60c-418c-b7dd-2cf4de03c2a7"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ce67f8c1-493b-4bd8-be82-988bf3697dd7"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2aa3afa7-d882-4772-a23b-14d4ff42fa4a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3cbd234d-4e7e-4652-bf20-31c4c2f9c145"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""871ff09a-9337-4f46-a7a7-9678d1db6b5a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fc7ac157-221c-4436-8029-5c1915387063"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""45be0538-5a71-481e-9a0a-7fada72e7466"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ce8eb05-c0bf-486f-9fa8-72f959138042"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85b9dbbe-f7c0-4f08-b989-110754738011"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01c26eba-c0e3-4db9-adb4-56902bf4df93"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b11ce26c-b771-4bd9-912c-7278d253860b"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51c9500f-333f-449d-8209-da822e803f6a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7066c7c-1a01-4b4f-9f58-c745e2d09edd"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0723cb24-3f40-4d2d-ab96-04aba5ccfabd"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // KeyMap
            m_KeyMap = asset.FindActionMap("KeyMap", throwIfNotFound: true);
            m_KeyMap_MousePosition = m_KeyMap.FindAction("MousePosition", throwIfNotFound: true);
            m_KeyMap_MouseLeftButton = m_KeyMap.FindAction("MouseLeftButton", throwIfNotFound: true);
            m_KeyMap_MouseMiddleButton = m_KeyMap.FindAction("MouseMiddleButton", throwIfNotFound: true);
            m_KeyMap_MouseRightButton = m_KeyMap.FindAction("MouseRightButton", throwIfNotFound: true);
            m_KeyMap_WASD = m_KeyMap.FindAction("WASD", throwIfNotFound: true);
            m_KeyMap_Tab = m_KeyMap.FindAction("Tab", throwIfNotFound: true);
            m_KeyMap_Esc = m_KeyMap.FindAction("Esc", throwIfNotFound: true);
            m_KeyMap_E = m_KeyMap.FindAction("E", throwIfNotFound: true);
            m_KeyMap_F = m_KeyMap.FindAction("F", throwIfNotFound: true);
            m_KeyMap_R = m_KeyMap.FindAction("R", throwIfNotFound: true);
            m_KeyMap_Space = m_KeyMap.FindAction("Space", throwIfNotFound: true);
            // UIMap
            m_UIMap = asset.FindActionMap("UIMap", throwIfNotFound: true);
            m_UIMap_WASDUI = m_UIMap.FindAction("WASDUI", throwIfNotFound: true);
            m_UIMap_ClickUI = m_UIMap.FindAction("ClickUI", throwIfNotFound: true);
            m_UIMap_Point = m_UIMap.FindAction("Point", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // KeyMap
        private readonly InputActionMap m_KeyMap;
        private List<IKeyMapActions> m_KeyMapActionsCallbackInterfaces = new List<IKeyMapActions>();
        private readonly InputAction m_KeyMap_MousePosition;
        private readonly InputAction m_KeyMap_MouseLeftButton;
        private readonly InputAction m_KeyMap_MouseMiddleButton;
        private readonly InputAction m_KeyMap_MouseRightButton;
        private readonly InputAction m_KeyMap_WASD;
        private readonly InputAction m_KeyMap_Tab;
        private readonly InputAction m_KeyMap_Esc;
        private readonly InputAction m_KeyMap_E;
        private readonly InputAction m_KeyMap_F;
        private readonly InputAction m_KeyMap_R;
        private readonly InputAction m_KeyMap_Space;
        public struct KeyMapActions
        {
            private @InputActions m_Wrapper;
            public KeyMapActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @MousePosition => m_Wrapper.m_KeyMap_MousePosition;
            public InputAction @MouseLeftButton => m_Wrapper.m_KeyMap_MouseLeftButton;
            public InputAction @MouseMiddleButton => m_Wrapper.m_KeyMap_MouseMiddleButton;
            public InputAction @MouseRightButton => m_Wrapper.m_KeyMap_MouseRightButton;
            public InputAction @WASD => m_Wrapper.m_KeyMap_WASD;
            public InputAction @Tab => m_Wrapper.m_KeyMap_Tab;
            public InputAction @Esc => m_Wrapper.m_KeyMap_Esc;
            public InputAction @E => m_Wrapper.m_KeyMap_E;
            public InputAction @F => m_Wrapper.m_KeyMap_F;
            public InputAction @R => m_Wrapper.m_KeyMap_R;
            public InputAction @Space => m_Wrapper.m_KeyMap_Space;
            public InputActionMap Get() { return m_Wrapper.m_KeyMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(KeyMapActions set) { return set.Get(); }
            public void AddCallbacks(IKeyMapActions instance)
            {
                if (instance == null || m_Wrapper.m_KeyMapActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_KeyMapActionsCallbackInterfaces.Add(instance);
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseLeftButton.started += instance.OnMouseLeftButton;
                @MouseLeftButton.performed += instance.OnMouseLeftButton;
                @MouseLeftButton.canceled += instance.OnMouseLeftButton;
                @MouseMiddleButton.started += instance.OnMouseMiddleButton;
                @MouseMiddleButton.performed += instance.OnMouseMiddleButton;
                @MouseMiddleButton.canceled += instance.OnMouseMiddleButton;
                @MouseRightButton.started += instance.OnMouseRightButton;
                @MouseRightButton.performed += instance.OnMouseRightButton;
                @MouseRightButton.canceled += instance.OnMouseRightButton;
                @WASD.started += instance.OnWASD;
                @WASD.performed += instance.OnWASD;
                @WASD.canceled += instance.OnWASD;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
                @E.started += instance.OnE;
                @E.performed += instance.OnE;
                @E.canceled += instance.OnE;
                @F.started += instance.OnF;
                @F.performed += instance.OnF;
                @F.canceled += instance.OnF;
                @R.started += instance.OnR;
                @R.performed += instance.OnR;
                @R.canceled += instance.OnR;
                @Space.started += instance.OnSpace;
                @Space.performed += instance.OnSpace;
                @Space.canceled += instance.OnSpace;
            }

            private void UnregisterCallbacks(IKeyMapActions instance)
            {
                @MousePosition.started -= instance.OnMousePosition;
                @MousePosition.performed -= instance.OnMousePosition;
                @MousePosition.canceled -= instance.OnMousePosition;
                @MouseLeftButton.started -= instance.OnMouseLeftButton;
                @MouseLeftButton.performed -= instance.OnMouseLeftButton;
                @MouseLeftButton.canceled -= instance.OnMouseLeftButton;
                @MouseMiddleButton.started -= instance.OnMouseMiddleButton;
                @MouseMiddleButton.performed -= instance.OnMouseMiddleButton;
                @MouseMiddleButton.canceled -= instance.OnMouseMiddleButton;
                @MouseRightButton.started -= instance.OnMouseRightButton;
                @MouseRightButton.performed -= instance.OnMouseRightButton;
                @MouseRightButton.canceled -= instance.OnMouseRightButton;
                @WASD.started -= instance.OnWASD;
                @WASD.performed -= instance.OnWASD;
                @WASD.canceled -= instance.OnWASD;
                @Tab.started -= instance.OnTab;
                @Tab.performed -= instance.OnTab;
                @Tab.canceled -= instance.OnTab;
                @Esc.started -= instance.OnEsc;
                @Esc.performed -= instance.OnEsc;
                @Esc.canceled -= instance.OnEsc;
                @E.started -= instance.OnE;
                @E.performed -= instance.OnE;
                @E.canceled -= instance.OnE;
                @F.started -= instance.OnF;
                @F.performed -= instance.OnF;
                @F.canceled -= instance.OnF;
                @R.started -= instance.OnR;
                @R.performed -= instance.OnR;
                @R.canceled -= instance.OnR;
                @Space.started -= instance.OnSpace;
                @Space.performed -= instance.OnSpace;
                @Space.canceled -= instance.OnSpace;
            }

            public void RemoveCallbacks(IKeyMapActions instance)
            {
                if (m_Wrapper.m_KeyMapActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IKeyMapActions instance)
            {
                foreach (var item in m_Wrapper.m_KeyMapActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_KeyMapActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public KeyMapActions @KeyMap => new KeyMapActions(this);

        // UIMap
        private readonly InputActionMap m_UIMap;
        private List<IUIMapActions> m_UIMapActionsCallbackInterfaces = new List<IUIMapActions>();
        private readonly InputAction m_UIMap_WASDUI;
        private readonly InputAction m_UIMap_ClickUI;
        private readonly InputAction m_UIMap_Point;
        public struct UIMapActions
        {
            private @InputActions m_Wrapper;
            public UIMapActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @WASDUI => m_Wrapper.m_UIMap_WASDUI;
            public InputAction @ClickUI => m_Wrapper.m_UIMap_ClickUI;
            public InputAction @Point => m_Wrapper.m_UIMap_Point;
            public InputActionMap Get() { return m_Wrapper.m_UIMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIMapActions set) { return set.Get(); }
            public void AddCallbacks(IUIMapActions instance)
            {
                if (instance == null || m_Wrapper.m_UIMapActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_UIMapActionsCallbackInterfaces.Add(instance);
                @WASDUI.started += instance.OnWASDUI;
                @WASDUI.performed += instance.OnWASDUI;
                @WASDUI.canceled += instance.OnWASDUI;
                @ClickUI.started += instance.OnClickUI;
                @ClickUI.performed += instance.OnClickUI;
                @ClickUI.canceled += instance.OnClickUI;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
            }

            private void UnregisterCallbacks(IUIMapActions instance)
            {
                @WASDUI.started -= instance.OnWASDUI;
                @WASDUI.performed -= instance.OnWASDUI;
                @WASDUI.canceled -= instance.OnWASDUI;
                @ClickUI.started -= instance.OnClickUI;
                @ClickUI.performed -= instance.OnClickUI;
                @ClickUI.canceled -= instance.OnClickUI;
                @Point.started -= instance.OnPoint;
                @Point.performed -= instance.OnPoint;
                @Point.canceled -= instance.OnPoint;
            }

            public void RemoveCallbacks(IUIMapActions instance)
            {
                if (m_Wrapper.m_UIMapActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IUIMapActions instance)
            {
                foreach (var item in m_Wrapper.m_UIMapActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_UIMapActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public UIMapActions @UIMap => new UIMapActions(this);
        public interface IKeyMapActions
        {
            void OnMousePosition(InputAction.CallbackContext context);
            void OnMouseLeftButton(InputAction.CallbackContext context);
            void OnMouseMiddleButton(InputAction.CallbackContext context);
            void OnMouseRightButton(InputAction.CallbackContext context);
            void OnWASD(InputAction.CallbackContext context);
            void OnTab(InputAction.CallbackContext context);
            void OnEsc(InputAction.CallbackContext context);
            void OnE(InputAction.CallbackContext context);
            void OnF(InputAction.CallbackContext context);
            void OnR(InputAction.CallbackContext context);
            void OnSpace(InputAction.CallbackContext context);
        }
        public interface IUIMapActions
        {
            void OnWASDUI(InputAction.CallbackContext context);
            void OnClickUI(InputAction.CallbackContext context);
            void OnPoint(InputAction.CallbackContext context);
        }
    }
}
