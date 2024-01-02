using CodeBase.Data;

namespace CodeBase.Infrastructure
{
	public interface ISavedProgressReader
	{
		public void LoadProgress(PlayerProgress progress);
	}
}

