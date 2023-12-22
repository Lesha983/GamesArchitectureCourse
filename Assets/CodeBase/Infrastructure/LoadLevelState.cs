using CodeBase.CameraLogic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string _initialPointTag = "InitialPoint";
		private GameStateMachine _stateMachine;
		private SceneLoader _sceneLoader;
		private readonly LoadCurtain _curtain;
		private readonly IGameFactory _gameFactory;

		public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadCurtain curtain, IGameFactory gameFactory)
		{
			_stateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_curtain = curtain;
			_gameFactory = gameFactory;
		}

		public void Enter(string sceneName)
		{
			_curtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_curtain.Hide();

		private void OnLoaded()
		{
			var initialPoint = GameObject.FindWithTag(_initialPointTag);
			GameObject hero = _gameFactory.CreateHero(initialPoint);

			_gameFactory.CreateHud();

			CameraFollow(hero);

			_stateMachine.Enter<GameLoopState>();
		}

		private void CameraFollow(GameObject hero) =>
			Camera.main
			.GetComponent<CameraFollow>()
			.Follow(hero.transform);
	}
}