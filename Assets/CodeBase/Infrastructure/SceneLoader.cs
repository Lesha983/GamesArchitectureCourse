using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
	public class SceneLoader
	{
		private readonly ICoroutineRunner _coroutineRunner;

		public SceneLoader(ICoroutineRunner coroutineRunner) =>
			_coroutineRunner = coroutineRunner;

		public void Load(string name, Action callback = null) =>
			_coroutineRunner.StartCoroutine(LoadScene(name, callback));

		private IEnumerator LoadScene(string name, Action callback = null)
		{
			if (SceneManager.GetActiveScene().name == name)
			{
				callback?.Invoke();
				yield break;
			}

			var waitNextScene = SceneManager.LoadSceneAsync(name);

			while (!waitNextScene.isDone)
				yield return null;

			callback?.Invoke();
		}
	}
}