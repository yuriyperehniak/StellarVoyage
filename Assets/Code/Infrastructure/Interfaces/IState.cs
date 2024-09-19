namespace Code.Infrastructure.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}