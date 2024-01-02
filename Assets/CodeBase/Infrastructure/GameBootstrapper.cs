using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace CodeBase.Infrastructure
{
	public partial class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SF] private LoadCurtain curtainPrefab;

		private Game _game;

		private void Awake()
		{
			_game = new Game(this, Instantiate(curtainPrefab));
			_game.StateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
		}
	}
}