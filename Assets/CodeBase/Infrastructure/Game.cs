using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class Game
	{
		public static IInputService inputService;

		public Game()
		{
			RegistorInputService();
		}

		private void RegistorInputService()
		{
			if (Application.isEditor)
				inputService = new StandaloneInputService();
			else
				inputService = new MobileInputService();
		}
	}
}
