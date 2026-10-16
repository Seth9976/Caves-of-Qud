using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x02000165 RID: 357
	public class IStats : IDisposable
	{
		// Token: 0x06000CFD RID: 3325 RVA: 0x0001A558 File Offset: 0x00018758
		internal IStats(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0001A574 File Offset: 0x00018774
		internal static HandleRef getCPtr(IStats obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0001A594 File Offset: 0x00018794
		~IStats()
		{
			this.Dispose();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0001A5C4 File Offset: 0x000187C4
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IStats(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0001A644 File Offset: 0x00018844
		public virtual void RequestUserStatsAndAchievements(GalaxyID userID, IUserStatsAndAchievementsRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IUserStatsAndAchievementsRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0001A66D File Offset: 0x0001886D
		public virtual void RequestUserStatsAndAchievements(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0001A690 File Offset: 0x00018890
		public virtual void RequestUserStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0001A6B0 File Offset: 0x000188B0
		public virtual int GetStatInt(string name, GalaxyID userID)
		{
			int num = GalaxyInstancePINVOKE.IStats_GetStatInt__SWIG_0(this.swigCPtr, name, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0001A6E4 File Offset: 0x000188E4
		public virtual int GetStatInt(string name)
		{
			int num = GalaxyInstancePINVOKE.IStats_GetStatInt__SWIG_1(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0001A710 File Offset: 0x00018910
		public virtual float GetStatFloat(string name, GalaxyID userID)
		{
			float num = GalaxyInstancePINVOKE.IStats_GetStatFloat__SWIG_0(this.swigCPtr, name, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0001A744 File Offset: 0x00018944
		public virtual float GetStatFloat(string name)
		{
			float num = GalaxyInstancePINVOKE.IStats_GetStatFloat__SWIG_1(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0001A76F File Offset: 0x0001896F
		public virtual void SetStatInt(string name, int value)
		{
			GalaxyInstancePINVOKE.IStats_SetStatInt(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0001A78E File Offset: 0x0001898E
		public virtual void SetStatFloat(string name, float value)
		{
			GalaxyInstancePINVOKE.IStats_SetStatFloat(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0001A7AD File Offset: 0x000189AD
		public virtual void UpdateAvgRateStat(string name, float countThisSession, double sessionLength)
		{
			GalaxyInstancePINVOKE.IStats_UpdateAvgRateStat(this.swigCPtr, name, countThisSession, sessionLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0001A7CD File Offset: 0x000189CD
		public virtual void GetAchievement(string name, ref bool unlocked, ref uint unlockTime, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetAchievement__SWIG_0(this.swigCPtr, name, ref unlocked, ref unlockTime, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0001A7F4 File Offset: 0x000189F4
		public virtual void GetAchievement(string name, ref bool unlocked, ref uint unlockTime)
		{
			GalaxyInstancePINVOKE.IStats_GetAchievement__SWIG_1(this.swigCPtr, name, ref unlocked, ref unlockTime);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0001A814 File Offset: 0x00018A14
		public virtual void SetAchievement(string name)
		{
			GalaxyInstancePINVOKE.IStats_SetAchievement(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0001A832 File Offset: 0x00018A32
		public virtual void ClearAchievement(string name)
		{
			GalaxyInstancePINVOKE.IStats_ClearAchievement(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0001A850 File Offset: 0x00018A50
		public virtual void StoreStatsAndAchievements(IStatsAndAchievementsStoreListener listener)
		{
			GalaxyInstancePINVOKE.IStats_StoreStatsAndAchievements__SWIG_0(this.swigCPtr, IStatsAndAchievementsStoreListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0001A873 File Offset: 0x00018A73
		public virtual void StoreStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_StoreStatsAndAchievements__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0001A890 File Offset: 0x00018A90
		public virtual void ResetStatsAndAchievements(IStatsAndAchievementsStoreListener listener)
		{
			GalaxyInstancePINVOKE.IStats_ResetStatsAndAchievements__SWIG_0(this.swigCPtr, IStatsAndAchievementsStoreListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0001A8B3 File Offset: 0x00018AB3
		public virtual void ResetStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_ResetStatsAndAchievements__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0001A8D0 File Offset: 0x00018AD0
		public virtual string GetAchievementDisplayName(string name)
		{
			string text = GalaxyInstancePINVOKE.IStats_GetAchievementDisplayName(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0001A8FC File Offset: 0x00018AFC
		public virtual void GetAchievementDisplayNameCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetAchievementDisplayNameCopy(this.swigCPtr, name, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0001A954 File Offset: 0x00018B54
		public virtual string GetAchievementDescription(string name)
		{
			string text = GalaxyInstancePINVOKE.IStats_GetAchievementDescription(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0001A980 File Offset: 0x00018B80
		public virtual void GetAchievementDescriptionCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetAchievementDescriptionCopy(this.swigCPtr, name, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		public virtual bool IsAchievementVisible(string name)
		{
			bool flag = GalaxyInstancePINVOKE.IStats_IsAchievementVisible(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0001AA04 File Offset: 0x00018C04
		public virtual bool IsAchievementVisibleWhileLocked(string name)
		{
			bool flag = GalaxyInstancePINVOKE.IStats_IsAchievementVisibleWhileLocked(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0001AA2F File Offset: 0x00018C2F
		public virtual void RequestLeaderboards(ILeaderboardsRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboards__SWIG_0(this.swigCPtr, ILeaderboardsRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0001AA52 File Offset: 0x00018C52
		public virtual void RequestLeaderboards()
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboards__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0001AA70 File Offset: 0x00018C70
		public virtual string GetLeaderboardDisplayName(string name)
		{
			string text = GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayName(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0001AA9C File Offset: 0x00018C9C
		public virtual void GetLeaderboardDisplayNameCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayNameCopy(this.swigCPtr, name, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		public virtual LeaderboardSortMethod GetLeaderboardSortMethod(string name)
		{
			LeaderboardSortMethod leaderboardSortMethod = (LeaderboardSortMethod)GalaxyInstancePINVOKE.IStats_GetLeaderboardSortMethod(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return leaderboardSortMethod;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0001AB20 File Offset: 0x00018D20
		public virtual LeaderboardDisplayType GetLeaderboardDisplayType(string name)
		{
			LeaderboardDisplayType leaderboardDisplayType = (LeaderboardDisplayType)GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayType(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return leaderboardDisplayType;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0001AB4B File Offset: 0x00018D4B
		public virtual void RequestLeaderboardEntriesGlobal(string name, uint rangeStart, uint rangeEnd, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesGlobal__SWIG_0(this.swigCPtr, name, rangeStart, rangeEnd, ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0001AB72 File Offset: 0x00018D72
		public virtual void RequestLeaderboardEntriesGlobal(string name, uint rangeStart, uint rangeEnd)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesGlobal__SWIG_1(this.swigCPtr, name, rangeStart, rangeEnd);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0001AB92 File Offset: 0x00018D92
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter, GalaxyID userID, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_0(this.swigCPtr, name, countBefore, countAfter, GalaxyID.getCPtr(userID), ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_1(this.swigCPtr, name, countBefore, countAfter, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0001ABE7 File Offset: 0x00018DE7
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_2(this.swigCPtr, name, countBefore, countAfter);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0001AC08 File Offset: 0x00018E08
		public virtual void RequestLeaderboardEntriesForUsers(string name, ref GalaxyID[] userArray, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesForUsers__SWIG_0(this.swigCPtr, name, Array.ConvertAll<GalaxyID, ulong>(userArray, (GalaxyID id) => id.ToUint64()), (uint)userArray.LongLength, ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0001AC64 File Offset: 0x00018E64
		public virtual void RequestLeaderboardEntriesForUsers(string name, ref GalaxyID[] userArray)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesForUsers__SWIG_1(this.swigCPtr, name, Array.ConvertAll<GalaxyID, ulong>(userArray, (GalaxyID id) => id.ToUint64()), (uint)userArray.LongLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0001ACB9 File Offset: 0x00018EB9
		public virtual void GetRequestedLeaderboardEntry(uint index, ref uint rank, ref int score, ref GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetRequestedLeaderboardEntry(this.swigCPtr, index, ref rank, ref score, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0001ACE1 File Offset: 0x00018EE1
		public virtual void GetRequestedLeaderboardEntryWithDetails(uint index, ref uint rank, ref int score, byte[] details, uint detailsSize, ref uint outDetailsSize, ref GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetRequestedLeaderboardEntryWithDetails(this.swigCPtr, index, ref rank, ref score, details, detailsSize, ref outDetailsSize, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0001AD0F File Offset: 0x00018F0F
		public virtual void SetLeaderboardScore(string name, int score, bool forceUpdate, ILeaderboardScoreUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_0(this.swigCPtr, name, score, forceUpdate, ILeaderboardScoreUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0001AD36 File Offset: 0x00018F36
		public virtual void SetLeaderboardScore(string name, int score, bool forceUpdate)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_1(this.swigCPtr, name, score, forceUpdate);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0001AD56 File Offset: 0x00018F56
		public virtual void SetLeaderboardScore(string name, int score)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_2(this.swigCPtr, name, score);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0001AD75 File Offset: 0x00018F75
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize, bool forceUpdate, ILeaderboardScoreUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_0(this.swigCPtr, name, score, details, detailsSize, forceUpdate, ILeaderboardScoreUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0001ADA0 File Offset: 0x00018FA0
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize, bool forceUpdate)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_1(this.swigCPtr, name, score, details, detailsSize, forceUpdate);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0001ADC4 File Offset: 0x00018FC4
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_2(this.swigCPtr, name, score, details, detailsSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		public virtual uint GetLeaderboardEntryCount(string name)
		{
			uint num = GalaxyInstancePINVOKE.IStats_GetLeaderboardEntryCount(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0001AE13 File Offset: 0x00019013
		public virtual void FindLeaderboard(string name, ILeaderboardRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_FindLeaderboard__SWIG_0(this.swigCPtr, name, ILeaderboardRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0001AE37 File Offset: 0x00019037
		public virtual void FindLeaderboard(string name)
		{
			GalaxyInstancePINVOKE.IStats_FindLeaderboard__SWIG_1(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0001AE55 File Offset: 0x00019055
		public virtual void FindOrCreateLeaderboard(string name, string displayName, LeaderboardSortMethod sortMethod, LeaderboardDisplayType displayType, ILeaderboardRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_FindOrCreateLeaderboard__SWIG_0(this.swigCPtr, name, displayName, (int)sortMethod, (int)displayType, ILeaderboardRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0001AE7E File Offset: 0x0001907E
		public virtual void FindOrCreateLeaderboard(string name, string displayName, LeaderboardSortMethod sortMethod, LeaderboardDisplayType displayType)
		{
			GalaxyInstancePINVOKE.IStats_FindOrCreateLeaderboard__SWIG_1(this.swigCPtr, name, displayName, (int)sortMethod, (int)displayType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0001AEA0 File Offset: 0x000190A0
		public virtual void RequestUserTimePlayed(GalaxyID userID, IUserTimePlayedRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IUserTimePlayedRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0001AEC9 File Offset: 0x000190C9
		public virtual void RequestUserTimePlayed(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0001AEEC File Offset: 0x000190EC
		public virtual void RequestUserTimePlayed()
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0001AF0C File Offset: 0x0001910C
		public virtual uint GetUserTimePlayed(GalaxyID userID)
		{
			uint num = GalaxyInstancePINVOKE.IStats_GetUserTimePlayed__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0001AF3C File Offset: 0x0001913C
		public virtual uint GetUserTimePlayed()
		{
			uint num = GalaxyInstancePINVOKE.IStats_GetUserTimePlayed__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x040002A3 RID: 675
		private HandleRef swigCPtr;

		// Token: 0x040002A4 RID: 676
		protected bool swigCMemOwn;
	}
}
