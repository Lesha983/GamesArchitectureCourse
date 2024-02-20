using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
    {
        private const string AndroidGameId = "5556966";
        private const string IOSGameId = "5556967";
        
        private const string RewardedVideoPlacementId = "Rewarded";

        public event Action RewardedVideoIsLoaded;
        public int Reward => 20;

        private string _gameId;
        private Action _onVideoFinished;

        public void Initialize()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameId = AndroidGameId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    break;
                case RuntimePlatform.OSXEditor:
                    _gameId = IOSGameId;
                    break;
                default:
                    Debug.LogError("Unsupported platform for ads");
                    break;
            }
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            Advertisement.Show(RewardedVideoPlacementId, this);
            _onVideoFinished = onVideoFinished;
        }

        public bool AdIsReady() => 
            Advertisement.isSupported;

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"OnUnityAdsAdLoaded");
            
            if(placementId == RewardedVideoPlacementId)
                RewardedVideoIsLoaded?.Invoke();
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log($"OnUnityAdsShowClick");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log($"OnUnityAdsShowStart");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                    Debug.Log($"OnUnityAdsShowComplete - {showCompletionState}");
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    Debug.Log($"OnUnityAdsShowComplete - {showCompletionState}");
                    _onVideoFinished?.Invoke();
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.Log($"OnUnityAdsShowComplete - {showCompletionState}");
                    break;
                default:
                    Debug.Log($"OnUnityAdsShowComplete - {showCompletionState}");
                    break;
            }

            _onVideoFinished = null;
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"OnUnityAdsFailedToLoad");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"OnUnityAdsShowFailure");
        }

        public void OnInitializationComplete()
        {
            Advertisement.Initialize(_gameId, true);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"OnInitializationFailed - {error}; {message}");
        }
    }
}