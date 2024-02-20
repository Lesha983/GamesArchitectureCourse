using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Ads;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
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


    public void CleanUp() => 
      _adsService.RewardedVideoIsLoaded -= RefreshAvailableAd;

    private void OnShowAdClicked() =>
      _adsService.ShowRewardedVideo(OnVideoFinished);

    private void OnVideoFinished() => 
      _progressService.Progress.WorldData.LootData.Add(_adsService.Reward);

    private void RefreshAvailableAd()
    {
      bool videoReady = _adsService.AdIsReady();

      foreach (var adActiveObject in AdActiveObjects)
      {
        adActiveObject.SetActive(videoReady);
      }

      foreach (var adInactiveObject in AdInactiveObjects)
      {
        adInactiveObject.SetActive(!videoReady);
      }
    }
  }
}