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
                    ""name"": ""RangedSpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""92603a6c-9afd-4383-bb8f-271ab5818c60"",
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
                },
                {
                    ""name"": ""MeleeSpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""fdecc092-d354-4961-8770-34f98a0f40fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c2d3f063-ce3c-4189-bfa7-0e9d97889d3f"",
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
                    ""id"": ""d2ee713a-9936-44a1-8c34-28cac7f68a94"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                    ""id"": ""266a0dbd-77f5-4296-95e9-50f6cffcefa2"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
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
                    ""path"": ""<Gamepad>/rightStick"",
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
                    ""id"": ""1fb9f983-555b-453f-a329-51f4c350effe"",
                    ""path"": ""<Gamepad>/select"",
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
                    ""id"": ""ec556f91-5cc8-42e5-85b4-9d53acb986b9"",
                    ""path"": ""<Gamepad>/leftShoulder"",
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
                    ""id"": ""1f0b3fb5-8d0e-437c-8b9a-91031fc0e11e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
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
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2858cf38-e166-4b59-a94a-82d1ab406242"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14abaeb0-9d91-4488-b80d-a6979f0022a5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3c15838-d667-44e9-916b-4e1c3f7d6f40"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c82fe1e-2117-4c52-91c5-368978daa630"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RangedSpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e0f8912-2fd9-4d81-a67d-0b0295252c72"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleeSpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""DialogueControl"",
            ""id"": ""d5344e22-e5b5-4592-88a5-98c2d9c7312b"",
            ""actions"": [
                {
                    ""name"": ""Continue"",
                    ""type"": ""Button"",
                    ""id"": ""4613fbdd-4113-4d30-a667-4deeb9825feb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""a1038a93-fae7-4d85-9faa-ea27693c6823"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChoiceA"",
                    ""type"": ""Button"",
                    ""id"": ""8216d786-73e0-4017-bdbd-7de15d33c044"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChoiceB"",
                    ""type"": ""Button"",
                    ""id"": ""c266c97b-4b7e-436c-8afd-d8b64f174995"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChoiceC"",
                    ""type"": ""Button"",
                    ""id"": ""7562e5f4-223f-4efc-95c5-d7eb1e0f1524"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChoiceD"",
                    ""type"": ""Button"",
                    ""id"": ""a4a9ef79-cd96-430b-b5b3-471af067effb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""602b7717-0f25-4126-8d37-c64fa92c95cf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bd9cdd4-e033-4ce3-9381-7f65eae74e7a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f31f4a5-2ac0-4dff-9749-d3def9276995"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChoiceA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""772378c3-5d06-4b71-b25b-01fa2c8702a8"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChoiceB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0080599e-c08e-4b61-9a82-c3ec9b8e3b8d"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChoiceC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2827260b-2d2b-4de6-8bbf-5a2360aec172"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChoiceD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OverlayControl"",
            ""id"": ""a34c2ef1-74fd-42ac-9dd0-85fe8ff5ad5d"",
            ""actions"": [
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""6153356f-3752-4ce1-829a-aaaccefac05c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""72cc7df0-3bfd-4e48-8905-7026fb0b029e"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ArtifactControl"",
            ""id"": ""e262cf76-9971-4506-84aa-34797e11af16"",
            ""actions"": [
                {
                    ""name"": ""ArtContinue"",
                    ""type"": ""Button"",
                    ""id"": ""c8f59fab-6795-4469-bb94-469ac12c0814"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ArtBack"",
                    ""type"": ""Button"",
                    ""id"": ""1abfa750-2e8f-46a1-866c-b02bbbacaf5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c9dc8403-4373-4678-9993-e465e462d58e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArtContinue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca581f40-ed90-4863-a904-6978392e0d49"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArtBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de7e11fd-6a5c-4e0c-88ae-f88de559e421"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArtBack"",
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
        m_CharacterControl_RangedSpecialAttack = m_CharacterControl.FindAction("RangedSpecialAttack", throwIfNotFound: true);
        m_CharacterControl_MeleeAttack = m_CharacterControl.FindAction("MeleeAttack", throwIfNotFound: true);
        m_CharacterControl_MeleeSpecialAttack = m_CharacterControl.FindAction("MeleeSpecialAttack", throwIfNotFound: true);
        m_CharacterControl_Interact = m_CharacterControl.FindAction("Interact", throwIfNotFound: true);
        // DialogueControl
        m_DialogueControl = asset.FindActionMap("DialogueControl", throwIfNotFound: true);
        m_DialogueControl_Continue = m_DialogueControl.FindAction("Continue", throwIfNotFound: true);
        m_DialogueControl_Back = m_DialogueControl.FindAction("Back", throwIfNotFound: true);
        m_DialogueControl_ChoiceA = m_DialogueControl.FindAction("ChoiceA", throwIfNotFound: true);
        m_DialogueControl_ChoiceB = m_DialogueControl.FindAction("ChoiceB", throwIfNotFound: true);
        m_DialogueControl_ChoiceC = m_DialogueControl.FindAction("ChoiceC", throwIfNotFound: true);
        m_DialogueControl_ChoiceD = m_DialogueControl.FindAction("ChoiceD", throwIfNotFound: true);
        // OverlayControl
        m_OverlayControl = asset.FindActionMap("OverlayControl", throwIfNotFound: true);
        m_OverlayControl_Return = m_OverlayControl.FindAction("Return", throwIfNotFound: true);
        // ArtifactControl
        m_ArtifactControl = asset.FindActionMap("ArtifactControl", throwIfNotFound: true);
        m_ArtifactControl_ArtContinue = m_ArtifactControl.FindAction("ArtContinue", throwIfNotFound: true);
        m_ArtifactControl_ArtBack = m_ArtifactControl.FindAction("ArtBack", throwIfNotFound: true);
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
    private readonly InputAction m_CharacterControl_RangedSpecialAttack;
    private readonly InputAction m_CharacterControl_MeleeAttack;
    private readonly InputAction m_CharacterControl_MeleeSpecialAttack;
    private readonly InputAction m_CharacterControl_Interact;
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
        public InputAction @RangedSpecialAttack => m_Wrapper.m_CharacterControl_RangedSpecialAttack;
        public InputAction @MeleeAttack => m_Wrapper.m_CharacterControl_MeleeAttack;
        public InputAction @MeleeSpecialAttack => m_Wrapper.m_CharacterControl_MeleeSpecialAttack;
        public InputAction @Interact => m_Wrapper.m_CharacterControl_Interact;
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
                @RangedSpecialAttack.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRangedSpecialAttack;
                @RangedSpecialAttack.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRangedSpecialAttack;
                @RangedSpecialAttack.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnRangedSpecialAttack;
                @MeleeAttack.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeAttack;
                @MeleeSpecialAttack.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeSpecialAttack;
                @MeleeSpecialAttack.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeSpecialAttack;
                @MeleeSpecialAttack.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnMeleeSpecialAttack;
                @Interact.started -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_CharacterControlActionsCallbackInterface.OnInteract;
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
                @RangedSpecialAttack.started += instance.OnRangedSpecialAttack;
                @RangedSpecialAttack.performed += instance.OnRangedSpecialAttack;
                @RangedSpecialAttack.canceled += instance.OnRangedSpecialAttack;
                @MeleeAttack.started += instance.OnMeleeAttack;
                @MeleeAttack.performed += instance.OnMeleeAttack;
                @MeleeAttack.canceled += instance.OnMeleeAttack;
                @MeleeSpecialAttack.started += instance.OnMeleeSpecialAttack;
                @MeleeSpecialAttack.performed += instance.OnMeleeSpecialAttack;
                @MeleeSpecialAttack.canceled += instance.OnMeleeSpecialAttack;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public CharacterControlActions @CharacterControl => new CharacterControlActions(this);

    // DialogueControl
    private readonly InputActionMap m_DialogueControl;
    private IDialogueControlActions m_DialogueControlActionsCallbackInterface;
    private readonly InputAction m_DialogueControl_Continue;
    private readonly InputAction m_DialogueControl_Back;
    private readonly InputAction m_DialogueControl_ChoiceA;
    private readonly InputAction m_DialogueControl_ChoiceB;
    private readonly InputAction m_DialogueControl_ChoiceC;
    private readonly InputAction m_DialogueControl_ChoiceD;
    public struct DialogueControlActions
    {
        private @PlayerControlls m_Wrapper;
        public DialogueControlActions(@PlayerControlls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Continue => m_Wrapper.m_DialogueControl_Continue;
        public InputAction @Back => m_Wrapper.m_DialogueControl_Back;
        public InputAction @ChoiceA => m_Wrapper.m_DialogueControl_ChoiceA;
        public InputAction @ChoiceB => m_Wrapper.m_DialogueControl_ChoiceB;
        public InputAction @ChoiceC => m_Wrapper.m_DialogueControl_ChoiceC;
        public InputAction @ChoiceD => m_Wrapper.m_DialogueControl_ChoiceD;
        public InputActionMap Get() { return m_Wrapper.m_DialogueControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueControlActions set) { return set.Get(); }
        public void SetCallbacks(IDialogueControlActions instance)
        {
            if (m_Wrapper.m_DialogueControlActionsCallbackInterface != null)
            {
                @Continue.started -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnContinue;
                @Continue.performed -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnContinue;
                @Continue.canceled -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnContinue;
                @Back.started -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnBack;
                @ChoiceA.started -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceA;
                @ChoiceA.performed -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceA;
                @ChoiceA.canceled -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceA;
                @ChoiceB.started -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceB;
                @ChoiceB.performed -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceB;
                @ChoiceB.canceled -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceB;
                @ChoiceC.started -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceC;
                @ChoiceC.performed -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceC;
                @ChoiceC.canceled -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceC;
                @ChoiceD.started -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceD;
                @ChoiceD.performed -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceD;
                @ChoiceD.canceled -= m_Wrapper.m_DialogueControlActionsCallbackInterface.OnChoiceD;
            }
            m_Wrapper.m_DialogueControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Continue.started += instance.OnContinue;
                @Continue.performed += instance.OnContinue;
                @Continue.canceled += instance.OnContinue;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @ChoiceA.started += instance.OnChoiceA;
                @ChoiceA.performed += instance.OnChoiceA;
                @ChoiceA.canceled += instance.OnChoiceA;
                @ChoiceB.started += instance.OnChoiceB;
                @ChoiceB.performed += instance.OnChoiceB;
                @ChoiceB.canceled += instance.OnChoiceB;
                @ChoiceC.started += instance.OnChoiceC;
                @ChoiceC.performed += instance.OnChoiceC;
                @ChoiceC.canceled += instance.OnChoiceC;
                @ChoiceD.started += instance.OnChoiceD;
                @ChoiceD.performed += instance.OnChoiceD;
                @ChoiceD.canceled += instance.OnChoiceD;
            }
        }
    }
    public DialogueControlActions @DialogueControl => new DialogueControlActions(this);

    // OverlayControl
    private readonly InputActionMap m_OverlayControl;
    private IOverlayControlActions m_OverlayControlActionsCallbackInterface;
    private readonly InputAction m_OverlayControl_Return;
    public struct OverlayControlActions
    {
        private @PlayerControlls m_Wrapper;
        public OverlayControlActions(@PlayerControlls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Return => m_Wrapper.m_OverlayControl_Return;
        public InputActionMap Get() { return m_Wrapper.m_OverlayControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OverlayControlActions set) { return set.Get(); }
        public void SetCallbacks(IOverlayControlActions instance)
        {
            if (m_Wrapper.m_OverlayControlActionsCallbackInterface != null)
            {
                @Return.started -= m_Wrapper.m_OverlayControlActionsCallbackInterface.OnReturn;
                @Return.performed -= m_Wrapper.m_OverlayControlActionsCallbackInterface.OnReturn;
                @Return.canceled -= m_Wrapper.m_OverlayControlActionsCallbackInterface.OnReturn;
            }
            m_Wrapper.m_OverlayControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Return.started += instance.OnReturn;
                @Return.performed += instance.OnReturn;
                @Return.canceled += instance.OnReturn;
            }
        }
    }
    public OverlayControlActions @OverlayControl => new OverlayControlActions(this);

    // ArtifactControl
    private readonly InputActionMap m_ArtifactControl;
    private IArtifactControlActions m_ArtifactControlActionsCallbackInterface;
    private readonly InputAction m_ArtifactControl_ArtContinue;
    private readonly InputAction m_ArtifactControl_ArtBack;
    public struct ArtifactControlActions
    {
        private @PlayerControlls m_Wrapper;
        public ArtifactControlActions(@PlayerControlls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ArtContinue => m_Wrapper.m_ArtifactControl_ArtContinue;
        public InputAction @ArtBack => m_Wrapper.m_ArtifactControl_ArtBack;
        public InputActionMap Get() { return m_Wrapper.m_ArtifactControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ArtifactControlActions set) { return set.Get(); }
        public void SetCallbacks(IArtifactControlActions instance)
        {
            if (m_Wrapper.m_ArtifactControlActionsCallbackInterface != null)
            {
                @ArtContinue.started -= m_Wrapper.m_ArtifactControlActionsCallbackInterface.OnArtContinue;
                @ArtContinue.performed -= m_Wrapper.m_ArtifactControlActionsCallbackInterface.OnArtContinue;
                @ArtContinue.canceled -= m_Wrapper.m_ArtifactControlActionsCallbackInterface.OnArtContinue;
                @ArtBack.started -= m_Wrapper.m_ArtifactControlActionsCallbackInterface.OnArtBack;
                @ArtBack.performed -= m_Wrapper.m_ArtifactControlActionsCallbackInterface.OnArtBack;
                @ArtBack.canceled -= m_Wrapper.m_ArtifactControlActionsCallbackInterface.OnArtBack;
            }
            m_Wrapper.m_ArtifactControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ArtContinue.started += instance.OnArtContinue;
                @ArtContinue.performed += instance.OnArtContinue;
                @ArtContinue.canceled += instance.OnArtContinue;
                @ArtBack.started += instance.OnArtBack;
                @ArtBack.performed += instance.OnArtBack;
                @ArtBack.canceled += instance.OnArtBack;
            }
        }
    }
    public ArtifactControlActions @ArtifactControl => new ArtifactControlActions(this);
    public interface ICharacterControlActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnOverlay(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnRangedAttack(InputAction.CallbackContext context);
        void OnRangedSpecialAttack(InputAction.CallbackContext context);
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnMeleeSpecialAttack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IDialogueControlActions
    {
        void OnContinue(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnChoiceA(InputAction.CallbackContext context);
        void OnChoiceB(InputAction.CallbackContext context);
        void OnChoiceC(InputAction.CallbackContext context);
        void OnChoiceD(InputAction.CallbackContext context);
    }
    public interface IOverlayControlActions
    {
        void OnReturn(InputAction.CallbackContext context);
    }
    public interface IArtifactControlActions
    {
        void OnArtContinue(InputAction.CallbackContext context);
        void OnArtBack(InputAction.CallbackContext context);
    }
}
