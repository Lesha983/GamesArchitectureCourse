namespace CodeBase.Infrastructure.Services
{
    public interface IRandomService : IService
    {
        public int Next(int min, int max);
    }
}