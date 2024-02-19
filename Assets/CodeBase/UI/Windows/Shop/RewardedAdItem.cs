using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Ads;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class RewardedAdItem : MonoBehaviour
    {
        public Button ShowAdButton;
        public GameObject[] AdActiveObjects;
        public GameObject[] AdInactiveObjects;

        private IAdsService _adsService;
        private IPersistentProgressService _progressService;
        
        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }
        public void Initialize()
        {
            ShowAdButton.onClick.AddListener(OnShowAdClicked);
            RefreshAvailableAd();
        }

        public void Subscribe() => 
            _adsService.RewardedVideoIsLoaded += RefreshAvailableAd;

        public void Cleanup() => 
            _adsService.RewardedVideoIsLoaded -= RefreshAvailableAd;

        private void OnShowAdClicked() => 
            _adsService.ShowRewardedVideo(OnVideoFinished);

        private void OnVideoFinished() => 
            _progressService.Progress.WorldData.LootData.Add(_adsService.Reward);

        private void RefreshAvailableAd()
        {
            var videoReady = _adsService.AdIsReady();
            Debug.Log($"RefreshAvailableAd() - videoReady = {videoReady}");

            foreach (var activeObject in AdActiveObjects)
            {
                activeObject.SetActive(videoReady);
            }
            
            foreach (var inactiveObject in AdInactiveObjects)
            {
                inactiveObject.SetActive(!videoReady);
            }
        }
    }
}