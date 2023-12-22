using System.Collections.Generic;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace CodeBase.Infrastructure
{
	public partial class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SF] private LoadCurtain curtain;

		private Game _game;

		private void Awake()
		{
			_game = new Game(this, curtain);
			_game.StateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
		}
	}
}