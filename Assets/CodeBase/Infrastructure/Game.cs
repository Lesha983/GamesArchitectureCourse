using CodeBase.Infrastructure.Services;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class Game
	{
		public GameStateMachine StateMachine;

		public Game(ICoroutineRunner coroutineRunner, LoadCurtain curtain)
		{
			StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
		}
	}
}
