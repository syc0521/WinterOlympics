// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""PlayController"",
            ""id"": ""97b76e75-998f-4334-87d3-1c40cd527947"",
            ""actions"": [
                {
                    ""name"": ""P1_L"",
                    ""type"": ""Button"",
                    ""id"": ""1a60e731-7ed2-401b-b040-49cd2058f694"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""P2_L"",
                    ""type"": ""Button"",
                    ""id"": ""8553c845-eb94-4056-80d2-3d9097bf0adf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""P1_R"",
                    ""type"": ""Button"",
                    ""id"": ""9d8e7d53-969d-405a-baf1-a9867036bc40"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""P2_R"",
                    ""type"": ""Button"",
                    ""id"": ""9dac389c-34ed-40b7-aae6-6ba6885187f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""P1_Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f876fdc6-1584-48fe-a46d-7219f68f6949"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""P2_Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b59115df-2e60-4aa8-bc24-fc1bb686d21f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4c16562b-6180-4c69-a339-e45058ec0ca7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""P1_L"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c3c4794-f211-4798-8080-bac9b13f1a7e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""P1_R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7aa451d-a44e-49c0-9c34-a49a614cc532"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""P2_R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d91297d2-daa3-4732-8505-331e95044d89"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""P2_L"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""236a96a9-210f-42b1-8218-8ea21c46af4f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P1_Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d6b34e3-42c0-4c9d-8637-a342bcbd2153"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P2_Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayController
        m_PlayController = asset.FindActionMap("PlayController", throwIfNotFound: true);
        m_PlayController_P1_L = m_PlayController.FindAction("P1_L", throwIfNotFound: true);
        m_PlayController_P2_L = m_PlayController.FindAction("P2_L", throwIfNotFound: true);
        m_PlayController_P1_R = m_PlayController.FindAction("P1_R", throwIfNotFound: true);
        m_PlayController_P2_R = m_PlayController.FindAction("P2_R", throwIfNotFound: true);
        m_PlayController_P1_Jump = m_PlayController.FindAction("P1_Jump", throwIfNotFound: true);
        m_PlayController_P2_Jump = m_PlayController.FindAction("P2_Jump", throwIfNotFound: true);
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

    // PlayController
    private readonly InputActionMap m_PlayController;
    private IPlayControllerActions m_PlayControllerActionsCallbackInterface;
    private readonly InputAction m_PlayController_P1_L;
    private readonly InputAction m_PlayController_P2_L;
    private readonly InputAction m_PlayController_P1_R;
    private readonly InputAction m_PlayController_P2_R;
    private readonly InputAction m_PlayController_P1_Jump;
    private readonly InputAction m_PlayController_P2_Jump;
    public struct PlayControllerActions
    {
        private @InputMaster m_Wrapper;
        public PlayControllerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @P1_L => m_Wrapper.m_PlayController_P1_L;
        public InputAction @P2_L => m_Wrapper.m_PlayController_P2_L;
        public InputAction @P1_R => m_Wrapper.m_PlayController_P1_R;
        public InputAction @P2_R => m_Wrapper.m_PlayController_P2_R;
        public InputAction @P1_Jump => m_Wrapper.m_PlayController_P1_Jump;
        public InputAction @P2_Jump => m_Wrapper.m_PlayController_P2_Jump;
        public InputActionMap Get() { return m_Wrapper.m_PlayController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayControllerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayControllerActions instance)
        {
            if (m_Wrapper.m_PlayControllerActionsCallbackInterface != null)
            {
                @P1_L.started -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_L;
                @P1_L.performed -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_L;
                @P1_L.canceled -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_L;
                @P2_L.started -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_L;
                @P2_L.performed -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_L;
                @P2_L.canceled -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_L;
                @P1_R.started -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_R;
                @P1_R.performed -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_R;
                @P1_R.canceled -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_R;
                @P2_R.started -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_R;
                @P2_R.performed -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_R;
                @P2_R.canceled -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_R;
                @P1_Jump.started -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_Jump;
                @P1_Jump.performed -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_Jump;
                @P1_Jump.canceled -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP1_Jump;
                @P2_Jump.started -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_Jump;
                @P2_Jump.performed -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_Jump;
                @P2_Jump.canceled -= m_Wrapper.m_PlayControllerActionsCallbackInterface.OnP2_Jump;
            }
            m_Wrapper.m_PlayControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @P1_L.started += instance.OnP1_L;
                @P1_L.performed += instance.OnP1_L;
                @P1_L.canceled += instance.OnP1_L;
                @P2_L.started += instance.OnP2_L;
                @P2_L.performed += instance.OnP2_L;
                @P2_L.canceled += instance.OnP2_L;
                @P1_R.started += instance.OnP1_R;
                @P1_R.performed += instance.OnP1_R;
                @P1_R.canceled += instance.OnP1_R;
                @P2_R.started += instance.OnP2_R;
                @P2_R.performed += instance.OnP2_R;
                @P2_R.canceled += instance.OnP2_R;
                @P1_Jump.started += instance.OnP1_Jump;
                @P1_Jump.performed += instance.OnP1_Jump;
                @P1_Jump.canceled += instance.OnP1_Jump;
                @P2_Jump.started += instance.OnP2_Jump;
                @P2_Jump.performed += instance.OnP2_Jump;
                @P2_Jump.canceled += instance.OnP2_Jump;
            }
        }
    }
    public PlayControllerActions @PlayController => new PlayControllerActions(this);
    private int m_XboxSchemeIndex = -1;
    public InputControlScheme XboxScheme
    {
        get
        {
            if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
            return asset.controlSchemes[m_XboxSchemeIndex];
        }
    }
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IPlayControllerActions
    {
        void OnP1_L(InputAction.CallbackContext context);
        void OnP2_L(InputAction.CallbackContext context);
        void OnP1_R(InputAction.CallbackContext context);
        void OnP2_R(InputAction.CallbackContext context);
        void OnP1_Jump(InputAction.CallbackContext context);
        void OnP2_Jump(InputAction.CallbackContext context);
    }
}
