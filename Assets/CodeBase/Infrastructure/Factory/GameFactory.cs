using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;

		public event Action HeroCreated;

		public List<ISavedProgressReader> ProgressReaders { get; } = new();
		public List<ISavedProgress> ProgressWriters { get; } = new();

		public GameObject HeroGameObject { get; private set; }

		public GameFactory(IAssetProvider assets)
		{
			_assets = assets;
		}

		public GameObject CreateHero(GameObject at)
		{
			HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
			HeroCreated?.Invoke();
			return HeroGameObject;
		}

		public GameObject CreateHud() =>
			InstantiateRegistered(AssetPath.HudPath);

		public void CleanUp()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
		{
			var gameObject = _assets.Instantiate(prefabPath, at);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			var gameObject = _assets.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (var progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

		private void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);

			ProgressReaders.Add(progressReader);
		}
	}
}
