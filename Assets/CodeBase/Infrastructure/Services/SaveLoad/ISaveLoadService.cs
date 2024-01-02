using CodeBase.Data;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure
{
	public interface ISaveLoadService : IService
	{
		public void SaveProgress();
		public PlayerProgress LoadProgress();
	}
}