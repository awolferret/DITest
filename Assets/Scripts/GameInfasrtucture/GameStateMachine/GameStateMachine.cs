using System;
using System.Collections.Generic;
using GameInfrastructure.Factory;
using GameInfrastructure.GameStateMachine.States;
using GameInfrastructure.Services;
using GameInfrastructure.Services.PersistentProgress;
using GameInfrastructure.Services.PersistentProgress.SaveLoad;
using GameInfrastructure.UI.Services.UIFactory;
using Logic;
using UnityEngine;

namespace GameInfrastructure.GameStateMachine
{
    public class GameStateMachine : MonoBehaviour, IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices allServices)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, allServices),
                [typeof(LoadLevelState)] =
                    new LoadLevelState(this, sceneLoader, loadingScreen, allServices.Single<IGameFactory>(),
                        allServices.Single<IPersistentProgressService>(), allServices.Single<IStaticDataService>(),
                        allServices.Single<IUIFactory>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    allServices.Single<IPersistentProgressService>(), allServices.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}