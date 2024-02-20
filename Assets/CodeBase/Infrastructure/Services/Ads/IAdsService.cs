using System;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        int Reward { get; }
        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
        public bool AdIsReady();
        event Action RewardedVideoIsLoaded;
    }
}