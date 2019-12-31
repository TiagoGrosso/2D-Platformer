// GENERATED AUTOMATICALLY FROM 'Assets/Configs/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""b4b944db-d8f4-4561-847e-ae1dcd594e0a"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2ee9a04b-6db2-484e-b856-4aa073623b94"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c043e5c0-fe55-4bed-91e6-3e97da62131f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slide"",
                    ""type"": ""Button"",
                    ""id"": ""42659938-913e-47ba-9fc1-d0fe28bf60d3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc2f6dca-2a11-4c22-8fc4-216ae285c574"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""88974467-6580-4c44-a917-97a26692e40d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cded8ef4-f35a-458f-a969-768256265be2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1bc4712d-a556-4d49-86a5-543181a17404"",
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
                    ""id"": ""b3351438-a988-4c18-8c6b-8341b4d314a5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Abilities"",
            ""id"": ""626e1cef-69c0-4766-b8ed-ce939928c591"",
            ""actions"": [
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""381ad28a-cef3-49c4-be4f-aa81759d5516"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1869241c-d8a2-49cb-bb24-a225703b261a"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
        m_Movement_Slide = m_Movement.FindAction("Slide", throwIfNotFound: true);
        // Abilities
        m_Abilities = asset.FindActionMap("Abilities", throwIfNotFound: true);
        m_Abilities_Dash = m_Abilities.FindAction("Dash", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Jump;
    private readonly InputAction m_Movement_Move;
    private readonly InputAction m_Movement_Slide;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputAction @Slide => m_Wrapper.m_Movement_Slide;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Slide.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlide;
                @Slide.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlide;
                @Slide.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlide;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Slide.started += instance.OnSlide;
                @Slide.performed += instance.OnSlide;
                @Slide.canceled += instance.OnSlide;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Abilities
    private readonly InputActionMap m_Abilities;
    private IAbilitiesActions m_AbilitiesActionsCallbackInterface;
    private readonly InputAction m_Abilities_Dash;
    public struct AbilitiesActions
    {
        private @PlayerControls m_Wrapper;
        public AbilitiesActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Dash => m_Wrapper.m_Abilities_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Abilities; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AbilitiesActions set) { return set.Get(); }
        public void SetCallbacks(IAbilitiesActions instance)
        {
            if (m_Wrapper.m_AbilitiesActionsCallbackInterface != null)
            {
                @Dash.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_AbilitiesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public AbilitiesActions @Abilities => new AbilitiesActions(this);
    public interface IMovementActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
    }
    public interface IAbilitiesActions
    {
        void OnDash(InputAction.CallbackContext context);
    }
}
