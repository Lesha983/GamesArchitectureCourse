using CodeBase.Data;

namespace CodeBase.Infrastructure
{
	public interface ISavedProgress : ISavedProgressReader
	{
		public void UpdateProgress(PlayerProgress progress);
	}
}
