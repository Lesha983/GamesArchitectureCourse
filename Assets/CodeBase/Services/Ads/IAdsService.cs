using System;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        event Action RewardedVideoIsLoaded;
        int Reward { get; }
        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
        public bool AdIsReady();
    }
}