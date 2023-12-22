namespace CodeBase.Infrastructure
{
	public class GameLoopState : IState
	{
		public GameStateMachine GameStateMachine { get; }

		public GameLoopState(GameStateMachine gameStateMachine)
		{
			GameStateMachine = gameStateMachine;
		}

		public void Enter()
		{
		}

		public void Exit()
		{
		}
	}
}