using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LoginManager : MonoSinglton<LoginManager> {

    private string m_strLeaderBoard = "CgkI8eXKhZ0REAIQAA"; // 리더보드 ID..
    private Action<bool> LoginEvent;
    public bool IsLogin { get; set; }

    /// <summary>
    /// GPGS를 초기화 합니다.
    /// </summary>
    public void InitializeGPGS()
    {
        Debug.LogWarning("초기화");
        IsLogin = false;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        LoginGPGS();
        //GoogleDriveInit();
    }

    /// <summary>
    /// GPGS를 로그인 합니다.
    /// </summary>
    public void LoginGPGS()
    {
        // 로그인이 안되어 있으면
        if (!Social.localUser.authenticated)
        {
            if (LoginEvent == null)
                Social.localUser.Authenticate(LoginCallBackGPGS);
            else
                Social.localUser.Authenticate(LoginEvent);
        }
    }

    /// <summary>
    /// GPGS Login Callback
    /// </summary>
    /// <param name="result"> 결과 </param>
    public void LoginCallBackGPGS(bool result)
    {
        IsLogin = result;
        LoginEvent = null;
    }

    /// <summary>
    /// GPGS를 로그아웃 합니다.
    /// </summary>
    public void LogoutGPGS()
    {
        // 로그인이 되어 있으면
        if (Social.localUser.authenticated)
        {
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
            IsLogin = false;
        }
    }

    /// <summary>
    /// GPGS에서 자신의 프로필 이미지를 가져옵니다.
    /// </summary>
    /// <returns> Texture 2D 이미지 </returns>
    public Texture2D GetImageGPGS()
    {
        if (Social.localUser.authenticated)
            return Social.localUser.image;
        else
            return null;
    }

    /// <summary>
    /// GPGS 에서 사용자 이름을 가져옵니다.
    /// </summary>
    /// <returns> 이름 </returns>
    public string GetNameGPGS()
    {
        if (Social.localUser.authenticated)
            return Social.localUser.userName;
        else
            return null;
    }

    //public void SendBoardScore(int Score)
    //{
    //    if (IsLogin)
    //    {
    //        Social.ReportScore(Score, GPGS.leaderboard_ranking, (bool success) =>
    //        {
    //            if (success == true)
    //            {
    //                Debug.LogError("등록 성공");
    //            }
    //            else
    //            {
    //                Debug.LogError("등록 실패");
    //            }
    //            // handle success or failure
    //        });
    //    }
    //}

    public void SendLeaderBoard(string key, int data)
    {
        if (IsLogin)
        {
            Social.ReportScore(data, key, (bool success) =>
            {
                if (success == true)
                {
                    Debug.LogError("등록 성공");
                }
                else
                {
                    Debug.LogError("등록 실패");
                }
                // handle success or failure
            });
        }
    }


    public void GetBoardValue(string BoardID , string SaveKey)
    {
        Social.LoadScores(BoardID , (result)=> {
            PlayerPrefs.SetInt(SaveKey, (int)result[0].value);
        });
       
    }

    public void ShowLeaderBoard()
    {
        if (IsLogin)
            Social.ShowLeaderboardUI();
        else
        {
            LoginEvent = (result) => 
            {
                IsLogin = result;
                if(result)
                {
                    ShowLeaderBoard();
                }
                LoginEvent = null;
            };
            LoginGPGS();
        }
    }

    public void ShowAchiveBoard()
    {
        if (IsLogin)
            Social.ShowAchievementsUI();
        else
        {
            LoginEvent = (result) =>
            {
                IsLogin = result;
                if (result)
                {
                    ShowAchiveBoard();
                }
                LoginEvent = null;
            };
            LoginGPGS();
        }

    }



    public void SpecialShowLeaderBoard()
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(m_strLeaderBoard);
    }

    //public void ShowRankingBoard()
    //{
    //    if (!Social.localUser.authenticated)
    //    {
    //        LoginEvent = (result) => {
    //            if (result == true) {
    //                ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGS.leaderboard_ranking);
    //                CheckData();
    //            }
    //            LoginEvent = null;
    //        };
    //        LoginGPGS();
    //    }else
    //        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGS.leaderboard_ranking);

    //}

    public void ShowLeaderBoard(string boardID)
    {
        if (!Social.localUser.authenticated)
        {
            LoginEvent = (result) =>
            {
                if (result == true)
                {
                    ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(boardID);
                }
                LoginEvent = null;
            };
            LoginGPGS();
        }
        else
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(boardID);
    }



    public void UnlockAchievement(string Key)
    {
        if(!Social.localUser.authenticated)
        {
            LoginEvent = (result) => {
                if (result == true)
                {
                    ((PlayGamesPlatform)Social.Active).UnlockAchievement(Key, (result01) =>
                    {

                    });
                }
                LoginEvent = null;
            };
            LoginGPGS();
        }
        else
        {
            ((PlayGamesPlatform)Social.Active).UnlockAchievement(Key , (result)=> 
            {
                
            });
        }
    }
}
