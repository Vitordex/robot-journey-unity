// GENERATED AUTOMATICALLY FROM 'Assets/Input/MainActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace RobotJourney.Input
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
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""584b167a-f0e3-4168-854f-86d9fbfc21fa"",
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
                    ""name"": """",
                    ""id"": ""37f4d613-1bd2-4dbd-a3a5-1cafd92c92ab"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
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
            m_Game_Test = m_Game.FindAction("Test", throwIfNotFound: true);
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
        private readonly InputAction m_Game_Test;
        public struct GameActions
        {
            private @MainActions m_Wrapper;
            public GameActions(@MainActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @TurnPlatforms => m_Wrapper.m_Game_TurnPlatforms;
            public InputAction @Test => m_Wrapper.m_Game_Test;
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
                    @Test.started -= m_Wrapper.m_GameActionsCallbackInterface.OnTest;
                    @Test.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnTest;
                    @Test.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnTest;
                }
                m_Wrapper.m_GameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @TurnPlatforms.started += instance.OnTurnPlatforms;
                    @TurnPlatforms.performed += instance.OnTurnPlatforms;
                    @TurnPlatforms.canceled += instance.OnTurnPlatforms;
                    @Test.started += instance.OnTest;
                    @Test.performed += instance.OnTest;
                    @Test.canceled += instance.OnTest;
                }
            }
        }
        public GameActions @Game => new GameActions(this);
        public interface IGameActions
        {
            void OnTurnPlatforms(InputAction.CallbackContext context);
            void OnTest(InputAction.CallbackContext context);
        }
    }
}
