using System;
using CodeBase.CameraLogic;
using CodeBase.Hero;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string _initialPointTag = "InitialPoint";
		private const string _enemySpawnerTag = "EnemySpawner";
		private GameStateMachine _stateMachine;
		private SceneLoader _sceneLoader;
		private readonly LoadCurtain _curtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;

		public LoadLevelState(
			GameStateMachine gameStateMachine,
			SceneLoader sceneLoader,
			LoadCurtain curtain,
			IGameFactory gameFactory,
			IPersistentProgressService progressService)
		{
			_stateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_curtain = curtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
		}

		public void Enter(string sceneName)
		{
			_curtain.Show();
			_gameFactory.CleanUp();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_curtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();
			InformProgressReaders();
			InitHud(_gameFactory.HeroGameObject);

			_stateMachine.Enter<GameLoopState>();
		}

		private void InformProgressReaders()
		{
			foreach(var progressReader in _gameFactory.ProgressReaders)
				progressReader.LoadProgress(_progressService.Progress);
		}

		private void InitGameWorld()
		{
			InitSpawners();
			var initialPoint = GameObject.FindWithTag(_initialPointTag);
			GameObject hero = _gameFactory.CreateHero(initialPoint);

			CameraFollow(hero);
		}

		private void InitSpawners()
		{
			foreach (var spawnerObject in GameObject.FindGameObjectsWithTag(_enemySpawnerTag))
			{
				var spawner = spawnerObject.GetComponent<EnemySpawner>();
				_gameFactory.Register(spawner);
			}
		}

		private void InitHud(GameObject hero)
		{
			var hud = _gameFactory.CreateHud();

			hud.GetComponentInChildren<ActorUI>()
				.Constract(hero.GetComponent<IHealth>());
		}

		private void CameraFollow(GameObject hero) =>
			Camera.main
			.GetComponent<CameraFollow>()
			.Follow(hero.transform);
	}
}