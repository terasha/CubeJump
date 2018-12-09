using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class ADMobManager : MonoSinglton<ADMobManager> {

    InterstitialAd mInterstitialAD = null;  //전면광고 변수
    public string mInterstitialAD_Key; //전면 배너 Key
    BannerView mBannerView = null; // 배너 출력
    public string mBannerView_Key = "ca-app-pub-8769932161527011/8610536289"; // 배너 Key

    RewardBasedVideoAd mVideoAD = null; //비디오 광고
    private string mVideo_Key = "ca-app-pub-8769932161527011/8750137081";           //비디오 키

    public string SEE_YOUT_LOGCAT_TO_GET_YOUR_DEVICE_ID = "0E461943693A4F7C45E7A1A20A5713FF";
    public string SEE_YOUT_LOGCAT_TO_GET_YOUR_DEVICE_ID2 = string.Empty;

    private AdRequest request;
    protected override void OnInit()
    {
        //mInterstitialAD = new InterstitialAd(mInterstitialAD_Key);
        mVideoAD = RewardBasedVideoAd.Instance;
        mBannerView = new BannerView(mBannerView_Key, AdSize.SmartBanner, AdPosition.Bottom);

        //테스트용 코드
        request = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)           //Simulator
            .AddTestDevice(SEE_YOUT_LOGCAT_TO_GET_YOUR_DEVICE_ID)   //My test device
            .AddTestDevice(SEE_YOUT_LOGCAT_TO_GET_YOUR_DEVICE_ID2)
            .Build();

        //실제 빌드 코드
        //request = new AdRequest.Builder().Build();

        mBannerView.LoadAd(request);
        //mInterstitialAD.LoadAd(request);
        mVideoAD.LoadAd(request, mVideo_Key);
    }

    //전면배너
    public void FowardShowBanner()
    {
        if (mInterstitialAD.IsLoaded())
            mInterstitialAD.Show();
    }

    private void AddFowardBannerEvent()
    {
        mInterstitialAD.OnAdClosed += FowardOnClose;
    }

    private void FowardOnClose(object sender, EventArgs args)
    {
        mInterstitialAD.Destroy();
        mInterstitialAD = new InterstitialAd(mInterstitialAD_Key);
        AddFowardBannerEvent();
        mInterstitialAD.LoadAd(request);
    }

    //항상 떠있는 배너
    public void ShowBanner()
    {
        mBannerView.Show();
    }

    public bool IsReadyVideo()
    {
        return mVideoAD.IsLoaded();
    }

    public void ShowRewardedVideo(EventHandler<Reward> Event)
    {
        if (mVideoAD.IsLoaded())
        {
            mVideoAD.OnAdRewarded += Event;
            mVideoAD.Show();
        }
    }

}
