using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _serices;

		public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_serices = services;

			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(Initial, EnterLoadLevel);
		}

		public void Exit()
		{
			//throw new NotImplementedException();
		}

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadLevelState, string>("Main");

		private void RegisterServices()
		{
			_serices.RegisterSingle<IInputService>(InputService());
			_serices.RegisterSingle<IAssetProvider>(new AssetProvider());
			_serices.RegisterSingle<IGameFactory>(new GameFactory(_serices.Single<IAssetProvider>()));
		}

		private static IInputService InputService()
		{
			if (Application.isEditor)
				return new StandaloneInputService();
			else
				return new MobileInputService();
		}
	}
}

