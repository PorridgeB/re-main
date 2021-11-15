// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerControlls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControlls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControlls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControlls"",
    ""maps"": [
        {
            ""name"": ""CharacterControl"",
            ""id"": ""8fd9704e-159d-4258-a222-b19c3036a94c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""fc6b7952-01da-430d-b687-0fd7d6143a21"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""Button"",
                    ""id"": ""1b181894-6e97-43f0-8282-6ce51950db7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""4f563b1d-b78c-4233-9f63-5df3b8f63d16"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Overlay"",
                    ""type"": ""Button"",
                    ""id"": ""f905319d-0523-496d-8b06-2db0d7b2c2ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""9da343ab-20e8-4718-8a2a-217afa7638ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RangedAttack"",
                    ""type"": ""Button"",
                    ""id"": ""975e0ad3-4bf9-4da3-9d5c-49d9a4cf0838"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MeleeAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b7fd527a-652d-4386-90dc-46b8766e6d59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1c80100c-8e38-442b-901e-633a19d271cf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""4b969d97-01cf-4322-9ddc-9c1a63ffdfc2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""73e117b3-0c23-4fee-82b6-6e271787a651"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""e205a22c-9ada-41c6-94e4-39dd577edc99"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""b9d6d96c-2188-48fe-be57-355f55a1aece"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""134e21ff-9c8c-4b2e-ab45-f590a19ecacd"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf33aed7-0a6b-40d7-9f3e-e8efc79098c0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""550fe667-6741-488d-a2fd-0fa14f38d231"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3dccc992-9616-427b-a74d-18df201c045f"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Overlay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b64ddf8b-43bd-4dca-b040-79f966100f51"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cecdaac-4ecc-4040-a465-e4eb2ee6c3f1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RangedAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da456867-81e8-4b49-aef5-2ab49b965be5"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControl
        m_CharacterControl = asset.FindActionMap("CharacterControl", throwIfNotFound: true);
        m_CharacterControl_Move = m_CharacterControl.FindAction("Move", throwIfNotFound: true);
        m_CharacterControl_Walk = m_CharacterControl.FindAction("Walk", throwIfNotFound: true);
        m_CharacterControl_Aim = m_CharacterControl.FindAction("Aim", throwIfNotFound: true);
        m_CharacterControl_Overlay = m_CharacterControl.FindAction("Overlay", throwIfNotFound: true);
        m_CharacterControl_Dash = m_CharacterControl.FindAction("Dash", throwIfNotFound: true);
        m_CharacterControl_RangedAttack = m_CharacterControl.FindAction("RangedAttack", throwIfNotFound: true);
        m_CharacterControl_MeleeAttack = m_CharacterControl.FindAction("MeleeAttack", throwIfNotFound: true);
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

    // CharacterControl
    private readonly InputActionMap m_CharacterControl;
    private ICharacterControlActions m_CharacterControlActionsCallbackInterface;
    private readonly InputAction m_CharacterControl_Move;
    private readonly InputAction m_CharacterControl_Walk;
    private readonly InputAction m_CharacterControl_Aim;
    private readonly InputAction m_CharacterControl_Overlay;
    private readonly InputAction m_CharacterControl_Dash;
    private readonly InputAction m_CharacterControl_RangedAttack;
    private readonly InputAction m_CharacterControl_MeleeAttack;
    public struct CharacterControlActions
    {
        private @PlayerControlls m_Wrapper;
        public CharacterControlActions(@PlayerControlls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterControl_Move;
        public InputAction @Walk => m_Wrapper.m_CharacterControl_Walk;
        public InputAction @Aim => m_Wrapper.m_CharacterControl_Aim;
        public InputAction @Overlay => m_Wrapper.m_CharacterControl_Overlay;
        public InputAction @Dash => m_Wrapper.m_CharacterControl_Dash;
        public InputAction @RangedAttack => m_Wrapper.m_CharacterControl_RangedAttack;
        public InputAction @MeleeAttack => m_Wrapper.m_CharacterControl_MeleeAttack;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlActions instance)
        {
            if (m_Wrapper.m_CharacterControlActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMove;
                @Walk.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnWalk;
                @Aim.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnAim;
                @Overlay.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnOverlay;
                @Overlay.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnOverlay;
                @Overlay.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnOverlay;
                @Dash.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnDash;
                @RangedAttack.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRangedAttack;
                @RangedAttack.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRangedAttack;
                @RangedAttack.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRangedAttack;
                @MeleeAttack.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeAttack;
            }
            m_Wrapper.m_CharacterControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Overlay.started += instance.OnOverlay;
                @Overlay.performed += instance.OnOverlay;
                @Overlay.canceled += instance.OnOverlay;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @RangedAttack.started += instance.OnRangedAttack;
                @RangedAttack.performed += instance.OnRangedAttack;
                @RangedAttack.canceled += instance.OnRangedAttack;
                @MeleeAttack.started += instance.OnMeleeAttack;
                @MeleeAttack.performed += instance.OnMeleeAttack;
                @MeleeAttack.canceled += instance.OnMeleeAttack;
            }
        }
    }
    public CharacterControlActions @CharacterControl => new CharacterControlActions(this);
    public interface ICharacterControlActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnOverlay(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnRangedAttack(InputAction.CallbackContext context);
        void OnMeleeAttack(InputAction.CallbackContext context);
    }
}
