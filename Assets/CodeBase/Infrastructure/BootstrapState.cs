using System;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
		{
			_stateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter()
		{
			RegisterServices();
			_sceneLoader.Load(Initial, EnterLoadLevel);
		}

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadLevelState, string>("Main");

		private void RegisterServices()
		{
			Game.inputService = RegistorInputService();
		}

		public void Exit()
		{
			//throw new NotImplementedException();
		}

		private static IInputService RegistorInputService()
		{
			if (Application.isEditor)
				return new StandaloneInputService();
			else
				return new MobileInputService();
		}
	}
}

