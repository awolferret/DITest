namespace GameInfrastructure.GameStateMachine.States
{
    public interface IState : IExitableState
    { 
        void Enter();
    }
}