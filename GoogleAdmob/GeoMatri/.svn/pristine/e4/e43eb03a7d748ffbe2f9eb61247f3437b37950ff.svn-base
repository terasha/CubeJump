
using UnityEngine.Advertisements;
using UnityEngine;
using System;

public class UnityAdsManager : MonoSinglton<UnityAdsManager> {

    private Action<ShowResult> EndEvent;

    protected override void OnInit()
    {
        if (Advertisement.isSupported)
            Advertisement.Initialize("1506469");        
    }

    public void ShowRewardedAd(Action<ShowResult> _callBack)
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = _callBack };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
