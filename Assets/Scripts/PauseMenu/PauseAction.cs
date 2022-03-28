// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PauseMenu/PauseAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PauseAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PauseAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PauseAction"",
    ""maps"": [
        {
            ""name"": ""Pause"",
            ""id"": ""db6d72f7-04b3-47dd-a52a-9335aebba3ae"",
            ""actions"": [
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""7c1189c3-b034-43df-aefe-f34d9aacb847"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5cab0912-7789-442b-9dfd-e9ca70d0160d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""f4fd0cc1-84be-44c2-8575-3ede5a3c25e5"",
            ""actions"": [
                {
                    ""name"": ""InteractionAction"",
                    ""type"": ""Button"",
                    ""id"": ""efdf48d8-ce8b-41c8-9b2e-d77a98fa488f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e249c61a-c68e-471d-8e4d-31caeb740303"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractionAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Pause
        m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
        m_Pause_PauseGame = m_Pause.FindAction("PauseGame", throwIfNotFound: true);
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_InteractionAction = m_Interaction.FindAction("InteractionAction", throwIfNotFound: true);
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

    // Pause
    private readonly InputActionMap m_Pause;
    private IPauseActions m_PauseActionsCallbackInterface;
    private readonly InputAction m_Pause_PauseGame;
    public struct PauseActions
    {
        private @PauseAction m_Wrapper;
        public PauseActions(@PauseAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseGame => m_Wrapper.m_Pause_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_Pause; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
        public void SetCallbacks(IPauseActions instance)
        {
            if (m_Wrapper.m_PauseActionsCallbackInterface != null)
            {
                @PauseGame.started -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_PauseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public PauseActions @Pause => new PauseActions(this);

    // Interaction
    private readonly InputActionMap m_Interaction;
    private IInteractionActions m_InteractionActionsCallbackInterface;
    private readonly InputAction m_Interaction_InteractionAction;
    public struct InteractionActions
    {
        private @PauseAction m_Wrapper;
        public InteractionActions(@PauseAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @InteractionAction => m_Wrapper.m_Interaction_InteractionAction;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
            {
                @InteractionAction.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteractionAction;
                @InteractionAction.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteractionAction;
                @InteractionAction.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteractionAction;
            }
            m_Wrapper.m_InteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InteractionAction.started += instance.OnInteractionAction;
                @InteractionAction.performed += instance.OnInteractionAction;
                @InteractionAction.canceled += instance.OnInteractionAction;
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);
    public interface IPauseActions
    {
        void OnPauseGame(InputAction.CallbackContext context);
    }
    public interface IInteractionActions
    {
        void OnInteractionAction(InputAction.CallbackContext context);
    }
}
