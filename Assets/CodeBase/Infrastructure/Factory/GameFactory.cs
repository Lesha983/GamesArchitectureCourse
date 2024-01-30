using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;
		private IStaticDataService _staticData;

		public List<ISavedProgressReader> ProgressReaders { get; } = new();
		public List<ISavedProgress> ProgressWriters { get; } = new();

		public GameObject HeroGameObject { get; private set; }

		public GameFactory(IAssetProvider assets, IStaticDataService staticData)
		{
			_assets = assets;
			_staticData = staticData;
		}

		public GameObject CreateHero(GameObject at)
		{
			HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
			return HeroGameObject;
		}

		public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
		{
			var monsterData = _staticData.ForMonster(typeId);
			var monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);
			
			var health = monster.GetComponent<IHealth>();
			health.Current = monsterData.Hp;
			health.Max = monsterData.Hp;
			
			monster.GetComponent<ActorUI>().Constract(health);
			monster.GetComponent<AgentMoveToHero>().Constract(HeroGameObject.transform);
			monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

			var attack = monster.GetComponent<Attack>();
			attack.Constract(HeroGameObject.transform);
			attack.Damage = monsterData.Damage;
			attack.Cleavage = monsterData.Cleavage;
			attack.EffectiveDistance = monsterData.EffectiveDistance;
			
			monster.GetComponent<RotateToHero>()?.Constract(HeroGameObject.transform);
			
			return monster;
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

		public void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);

			ProgressReaders.Add(progressReader);
		}
	}
}
