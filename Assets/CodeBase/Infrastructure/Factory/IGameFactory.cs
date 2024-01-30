using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public interface IGameFactory : IService
	{
		public List<ISavedProgressReader> ProgressReaders { get; }
		public List<ISavedProgress> ProgressWriters { get; }

		GameObject HeroGameObject { get; }

		GameObject CreateHero(GameObject at);
		GameObject CreateHud();
		GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
		public void CleanUp();
		public void Register(ISavedProgressReader progressReader);
	}
}