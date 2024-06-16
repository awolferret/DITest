namespace GameInfasrtucture.GameStateMachine.States
{
    public interface IPayloadedState<TPayload> : IExitableState
    { 
        void Enter(TPayload payload);
    }
}