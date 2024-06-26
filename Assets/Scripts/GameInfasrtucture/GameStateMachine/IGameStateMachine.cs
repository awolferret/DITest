using GameInfasrtucture.GameStateMachine.States;
using GameInfasrtucture.Services;

namespace GameInfasrtucture.GameStateMachine
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}