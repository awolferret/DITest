namespace GameInfasrtucture.GameStateMachine.States
{
    public interface IState : IExitableState
    { 
        void Enter();
    }
}