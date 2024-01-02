using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class SaveLoadService : ISaveLoadService
	{
		private const string ProgressKey = "Progress";

		private IGameFactory _gameFactory { get; }
		private readonly IPersistentProgressService _progressService;

		public SaveLoadService(IPersistentProgressService progressService,IGameFactory gameFactory)
		{
			_progressService = progressService;
			_gameFactory = gameFactory;
		}

		public PlayerProgress LoadProgress() =>
			PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

		public void SaveProgress()
		{
			foreach(var progressWriter in _gameFactory.ProgressWriters)
				progressWriter.UpdateProgress(_progressService.Progress);

			PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
		}
	}
}