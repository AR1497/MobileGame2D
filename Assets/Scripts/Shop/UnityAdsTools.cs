using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Tools.Ads
{
    public class UnityAdsTools : MonoBehaviour, IUnityAdsListener, IAdsShower, IUnityAdsInitializationListener
    {
        private const string InterstitialPlacement = "Interstitial_IOS";
        private const string RewardedPlacement = "Rewarded_IOS";
        private const string GameId = "4703504";

        private Action _onRewardedFinish;
        private bool _isReady = false;

        public void Start()
        {
            Advertisement.Initialize(GameId, true);
        }

        public void OnUnityAdsReady(string placementId)
        {
            _isReady = true;
        }

        public void OnUnityAdsDidError(string message)
        {
            _onRewardedFinish = null;
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished)
            {
                _onRewardedFinish?.Invoke();
            }
            _onRewardedFinish = null;
        }

        public void ShowInterstitial()
        {
            _onRewardedFinish = null;
            Advertisement.Show(InterstitialPlacement);
        }

        public void ShowVideo(Action successShow)
        {
            Advertisement.Show(RewardedPlacement);
            _onRewardedFinish = successShow;
        }

        public void OnInitializationComplete()
        {
            
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"Error {message} :: {error.ToString()}");
        }
    }
}
