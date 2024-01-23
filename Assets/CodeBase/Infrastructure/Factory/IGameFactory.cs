using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public interface IGameFactory : IService
	{
		public List<ISavedProgressReader> ProgressReaders { get; }
		public List<ISavedProgress> ProgressWriters { get; }

		GameObject HeroGameObject { get; }
		event Action HeroCreated;

		GameObject CreateHero(GameObject at);
		GameObject CreateHud();
		public void CleanUp();
		public void Register(ISavedProgressReader progressReader);
	}
}