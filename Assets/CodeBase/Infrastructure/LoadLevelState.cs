using CodeBase.CameraLogic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string _initialPointTag = "InitialPoint";
		private const string _heroPath = "Hero/hero";
		private const string _hudPath = "Hud/Hud";
		private GameStateMachine _stateMachine;
		private SceneLoader _sceneLoader;
		private readonly LoadCurtain _curtain;

		public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadCurtain curtain)
		{
			_stateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_curtain = curtain;
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
			var hero = Instantiate(_heroPath, initialPoint.transform.position);

			Instantiate(_hudPath);

			CameraFollow(hero);

			_stateMachine.Enter<GameLoopState>();
		}

		private static GameObject Instantiate(string path)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab);
		}

		private static GameObject Instantiate(string path, Vector3 at)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, at, Quaternion.identity);
		}

		private void CameraFollow(GameObject hero) =>
			Camera.main
			.GetComponent<CameraFollow>()
			.Follow(hero.transform);
	}
}