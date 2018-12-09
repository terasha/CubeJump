﻿/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Class to help us to manager the score in the game
/// </summary>
namespace NNest.GeometryJump
{
    [Serializable]
    public class Score 
	{
		private int point;
		private int last;
		private int best;
		private bool lastIsBest;

		public Score()
		{
			this.point = 0;
            this.last = 0;
            this.best = 0;
			this.lastIsBest = false;
		}

		//return current score
		public int AddPoint(int p)
		{
			this.point += p;
			return GetPoint();
		}

		public int SetPoint(int p)
		{
			this.point = p;
			return this.point;
		}

		public int GetPoint()
		{
			return this.point;
		}

		//return true if best
		public bool Save()
		{
            if (this.best < this.point)
			{
				this.lastIsBest = true;
                best = point;
                LoginManager.instnace.SendLeaderBoard(GPGSIds.leaderboard_ranking, best);
                return true;
			}
            this.last = this.point;
			this.lastIsBest = false;
            
			return false;
		}

		public int GetLast()
		{
			return this.last;
		}

		public int GetBest()
		{
			return this.best;
		}

		public bool GetLastIsBest()
		{
			return this.lastIsBest;
		}
	}
}