using System;
using CodeBase.Data;

namespace CodeBase.Infrastructure
{
	internal class LoadProgressState : IState
	{
		private GameStateMachine _gameStateMachine;
		private readonly IPersistentProgressService _progressService;
		private ISaveLoadService _saveLoadService;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadService = saveLoadService;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();
			_gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
		}

		public void Exit()
		{
		}

		private void LoadProgressOrInitNew()
		{
			_progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
		}

		private PlayerProgress NewProgress() =>
			new PlayerProgress("Main");
	}
}