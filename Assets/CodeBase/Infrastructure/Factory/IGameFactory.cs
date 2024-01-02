using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public interface IGameFactory : IService
	{
		public List<ISavedProgressReader> ProgressReaders { get; }
		public List<ISavedProgress> ProgressWriters { get; }

		GameObject CreateHero(GameObject at);
		void CreateHud();
		public void CleanUp();
	}
}