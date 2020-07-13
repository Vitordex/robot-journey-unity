// GENERATED AUTOMATICALLY FROM 'Assets/Runtime/GameInput/MainActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Runtime.GameInput
{
    public class @MainActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MainActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainActions"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""762db8d5-95ec-4d36-a448-ea42f94cade9"",
            ""actions"": [
                {
                    ""name"": ""TurnPlatforms"",
                    ""type"": ""Button"",
                    ""id"": ""573ee60a-abfe-4974-aab8-68092db27b42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""PassThrough"",
                    ""id"": ""49f23f89-e00e-43d1-abcf-3d3d8c87a18e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""2646c4d0-cf49-4257-bf66-d55fa21c8e6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""101396fd-f47d-4c01-8c31-84e5edcce87b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnPlatforms"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5785ab84-776b-497d-a7df-ae24855fff2c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""89c20a58-c915-4184-8bc0-365237bf8c77"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e57736d3-23f9-4ac8-b7ca-01f724976760"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a9bb8f07-441e-4b42-806c-20e17d61a054"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4d3e708d-480b-49f3-97cb-327518583ab4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""43e06af0-7169-4476-a4d1-a4dfb1fe7aa7"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Game
            m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
            m_Game_TurnPlatforms = m_Game.FindAction("TurnPlatforms", throwIfNotFound: true);
            m_Game_Walk = m_Game.FindAction("Walk", throwIfNotFound: true);
            m_Game_Interact = m_Game.FindAction("Interact", throwIfNotFound: true);
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

        // Game
        private readonly InputActionMap m_Game;
        private IGameActions m_GameActionsCallbackInterface;
        private readonly InputAction m_Game_TurnPlatforms;
        private readonly InputAction m_Game_Walk;
        private readonly InputAction m_Game_Interact;
        public struct GameActions
        {
            private @MainActions m_Wrapper;
            public GameActions(@MainActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @TurnPlatforms => m_Wrapper.m_Game_TurnPlatforms;
            public InputAction @Walk => m_Wrapper.m_Game_Walk;
            public InputAction @Interact => m_Wrapper.m_Game_Interact;
            public InputActionMap Get() { return m_Wrapper.m_Game; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
            public void SetCallbacks(IGameActions instance)
            {
                if (m_Wrapper.m_GameActionsCallbackInterface != null)
                {
                    @TurnPlatforms.started -= m_Wrapper.m_GameActionsCallbackInterface.OnTurnPlatforms;
                    @TurnPlatforms.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnTurnPlatforms;
                    @TurnPlatforms.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnTurnPlatforms;
                    @Walk.started -= m_Wrapper.m_GameActionsCallbackInterface.OnWalk;
                    @Walk.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnWalk;
                    @Walk.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnWalk;
                    @Interact.started -= m_Wrapper.m_GameActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnInteract;
                }
                m_Wrapper.m_GameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @TurnPlatforms.started += instance.OnTurnPlatforms;
                    @TurnPlatforms.performed += instance.OnTurnPlatforms;
                    @TurnPlatforms.canceled += instance.OnTurnPlatforms;
                    @Walk.started += instance.OnWalk;
                    @Walk.performed += instance.OnWalk;
                    @Walk.canceled += instance.OnWalk;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                }
            }
        }
        public GameActions @Game => new GameActions(this);
        public interface IGameActions
        {
            void OnTurnPlatforms(InputAction.CallbackContext context);
            void OnWalk(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
        }
    }
}
