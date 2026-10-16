using System;
using Galaxy.Api;
using UnityEngine;

namespace Galaxy
{
	// Token: 0x020004AB RID: 1195
	public class GalaxyManager
	{
		// Token: 0x06002DD3 RID: 11731 RVA: 0x000FAA32 File Offset: 0x000F8C32
		internal GalaxyManager()
		{
			if (GalaxyManager.Manager != null)
			{
				throw new InvalidOperationException();
			}
			GalaxyManager.Manager = this;
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000FAA50 File Offset: 0x000F8C50
		public void Awake()
		{
			this.Active = false;
			this.Initialized = false;
			this.Synchronized = false;
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			int i = 0;
			int num = commandLineArgs.Length;
			while (i < num)
			{
				if (commandLineArgs[i].EqualsNoCase("GALAXY:NO"))
				{
					Debug.Log("GALAXY - Disabled via command line argument.");
					return;
				}
				i++;
			}
			try
			{
				Debug.Log("GALAXY - Initializing.");
				this.Initialized = true;
				GalaxyInstance.Init(new InitParams("50877364037825147", "1d281dffae098f06a520daeff1fb1f9e9ebdb346b5c397b4f98e8ddb8d978f4e"));
				this.Stats = GalaxyInstance.Stats();
				Debug.Log("GALAXY - Authenticating user.");
				GalaxyManager.OperationalStateListener.Register();
				GalaxyManager.AchievementChangeListener.Register();
				GalaxyInstance.User().SignInGalaxy(false, new GalaxyManager.AuthenticationListener());
			}
			catch (Exception ex)
			{
				string text = "GALAXY - Error initializing: ";
				Exception ex2 = ex;
				Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null));
				this.Shutdown();
			}
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000FAB30 File Offset: 0x000F8D30
		public void Shutdown()
		{
			if (!this.Initialized)
			{
				return;
			}
			try
			{
				Debug.Log("GALAXY - Shutting down.");
				this.Initialized = false;
				this.Active = false;
				this.Stats = null;
				GalaxyManager.OperationalStateListener.Unregister();
				GalaxyManager.UserStatsRetrieveListener.Unregister();
				GalaxyManager.AchievementChangeListener.Unregister();
				GalaxyInstance.Shutdown(true);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("GalaxyManager::Shutdown", ex, "game_exception");
			}
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000FABA0 File Offset: 0x000F8DA0
		public void Update()
		{
			GalaxyInstance.ProcessData();
			if (this.Store)
			{
				this.Stats.StoreStatsAndAchievements();
				this.Store = false;
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000FABC4 File Offset: 0x000F8DC4
		public bool UpdateAchievement(string ID, ref bool Achieved, ref DateTime TimeStamp)
		{
			bool flag = false;
			uint num = 0U;
			try
			{
				this.Stats.GetAchievement(ID, ref flag, ref num);
			}
			catch (Exception ex)
			{
				Debug.Log(string.Format("GALAXY - Error getting achievement '{0}':\n{1}", ID, ex));
				return false;
			}
			if (num > 0U && TimeStamp == DateTime.MinValue)
			{
				TimeStamp = DateTimeOffset.FromUnixTimeSeconds((long)((ulong)num)).DateTime;
			}
			if (Achieved && !flag)
			{
				this.SetAchievement(ID);
				return false;
			}
			if (!Achieved && flag)
			{
				Achieved = true;
				return true;
			}
			return false;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000FAC5C File Offset: 0x000F8E5C
		public bool GetAchievement(string ID)
		{
			bool flag = false;
			uint num = 0U;
			try
			{
				this.Stats.GetAchievement(ID, ref flag, ref num);
			}
			catch (Exception ex)
			{
				Debug.Log(string.Format("GALAXY - Error getting achievement '{0}':\n{1}", ID, ex));
				return false;
			}
			return flag;
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000FACA8 File Offset: 0x000F8EA8
		public void SetAchievement(string ID)
		{
			try
			{
				this.Stats.SetAchievement(ID);
				this.Store = true;
			}
			catch (Exception ex)
			{
				Debug.Log(string.Format("GALAXY - Error setting achievement '{0}':\n{1}", ID, ex));
			}
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000FACF0 File Offset: 0x000F8EF0
		public int GetStat(string ID)
		{
			int num;
			try
			{
				num = this.Stats.GetStatInt(ID);
			}
			catch (Exception ex)
			{
				Debug.Log(string.Format("GALAXY - Error getting stat '{0}':\n{1}", ID, ex));
				num = 0;
			}
			return num;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000FAD34 File Offset: 0x000F8F34
		public void SetStat(string ID, int Value)
		{
			try
			{
				this.Stats.SetStatInt(ID, Value);
				this.Store = true;
			}
			catch (Exception ex)
			{
				Debug.Log(string.Format("GALAXY - Error setting stat '{0}':\n{1}", ID, ex));
			}
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000FAD7C File Offset: 0x000F8F7C
		public bool UpdateStat(string ID, ref int Value)
		{
			if (!this.Active)
			{
				return false;
			}
			int stat = this.GetStat(ID);
			if (Value > stat)
			{
				this.SetStat(ID, Value);
				return false;
			}
			if (Value < stat)
			{
				Value = stat;
				return true;
			}
			return false;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000FADB8 File Offset: 0x000F8FB8
		public void Synchronize()
		{
			if (!AchievementManager.Active)
			{
				return;
			}
			if (this.Synchronized)
			{
				return;
			}
			this.Synchronized = true;
			AchievementState state = AchievementManager.State;
			bool flag = false;
			foreach (AchievementInfo achievementInfo in state.Achievements.Values)
			{
				if (!achievementInfo.ID.IsNullOrEmpty())
				{
					flag |= this.UpdateAchievement(achievementInfo.ID, ref achievementInfo.Achieved, ref achievementInfo.TimeStamp);
				}
			}
			foreach (StatInfo statInfo in state.Stats.Values)
			{
				if (!statInfo.ID.IsNullOrEmpty())
				{
					flag |= this.UpdateStat(statInfo.ID, ref statInfo.Value);
				}
			}
			AchievementManager.Write = AchievementManager.Write || flag;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000FAEC4 File Offset: 0x000F90C4
		public void ResetAchievements()
		{
			this.Stats.ResetStatsAndAchievements();
		}

		// Token: 0x04001A07 RID: 6663
		private const string CLIENT_ID = "50877364037825147";

		// Token: 0x04001A08 RID: 6664
		private const string CLIENT_SECRET = "1d281dffae098f06a520daeff1fb1f9e9ebdb346b5c397b4f98e8ddb8d978f4e";

		// Token: 0x04001A09 RID: 6665
		private static GalaxyManager Manager;

		// Token: 0x04001A0A RID: 6666
		public bool Active;

		// Token: 0x04001A0B RID: 6667
		public bool Initialized;

		// Token: 0x04001A0C RID: 6668
		public bool Synchronized;

		// Token: 0x04001A0D RID: 6669
		private bool Store;

		// Token: 0x04001A0E RID: 6670
		private IStats Stats;

		// Token: 0x02001913 RID: 6419
		private class AuthenticationListener : IAuthListener
		{
			// Token: 0x0600E238 RID: 57912 RVA: 0x00521BCF File Offset: 0x0051FDCF
			public override void OnAuthSuccess()
			{
				Debug.Log("GALAXY - Authentication succeeded, requesting user stats.");
				GalaxyManager.UserStatsRetrieveListener.Register();
				GalaxyManager.Manager.Stats.RequestUserStatsAndAchievements();
			}

			// Token: 0x0600E239 RID: 57913 RVA: 0x00521BEF File Offset: 0x0051FDEF
			public override void OnAuthFailure(IAuthListener.FailureReason FailureReason)
			{
				Debug.Log(string.Format("GALAXY - Authentication failed ({0}).", FailureReason));
				GalaxyManager.Manager.Shutdown();
			}

			// Token: 0x0600E23A RID: 57914 RVA: 0x00521C10 File Offset: 0x0051FE10
			public override void OnAuthLost()
			{
				Debug.Log("GALAXY - Connection lost.");
			}
		}

		// Token: 0x02001914 RID: 6420
		private class OperationalStateListener : IOperationalStateChangeListener
		{
			// Token: 0x0600E23C RID: 57916 RVA: 0x00521C24 File Offset: 0x0051FE24
			public static void Register()
			{
				if (!GalaxyManager.Manager.Initialized)
				{
					return;
				}
				if (GalaxyManager.OperationalStateListener.Listener == null)
				{
					GalaxyManager.OperationalStateListener.Listener = new GalaxyManager.OperationalStateListener();
				}
				GalaxyInstance.ListenerRegistrar().Register(ListenerType.OPERATIONAL_STATE_CHANGE, GalaxyManager.OperationalStateListener.Listener);
			}

			// Token: 0x0600E23D RID: 57917 RVA: 0x00521C55 File Offset: 0x0051FE55
			public static void Unregister()
			{
				GalaxyManager.OperationalStateListener listener = GalaxyManager.OperationalStateListener.Listener;
				if (listener != null)
				{
					listener.Dispose();
				}
				GalaxyManager.OperationalStateListener.Listener = null;
			}

			// Token: 0x0600E23E RID: 57918 RVA: 0x00521C6D File Offset: 0x0051FE6D
			public override void OnOperationalStateChanged(uint State)
			{
				if (State == 0U)
				{
					GalaxyManager.Manager.Shutdown();
					return;
				}
				GalaxyManager.Manager.Active &= State.HasBit(1U);
			}

			// Token: 0x040064A5 RID: 25765
			private const uint SIGNED_OUT = 0U;

			// Token: 0x040064A6 RID: 25766
			private const uint SIGNED_IN = 1U;

			// Token: 0x040064A7 RID: 25767
			private const uint LOGGED_ON = 2U;

			// Token: 0x040064A8 RID: 25768
			public static GalaxyManager.OperationalStateListener Listener;
		}

		// Token: 0x02001915 RID: 6421
		private class UserStatsRetrieveListener : IUserStatsAndAchievementsRetrieveListener
		{
			// Token: 0x0600E240 RID: 57920 RVA: 0x00521C9D File Offset: 0x0051FE9D
			public static void Register()
			{
				if (!GalaxyManager.Manager.Initialized)
				{
					return;
				}
				if (GalaxyManager.UserStatsRetrieveListener.Listener == null)
				{
					GalaxyManager.UserStatsRetrieveListener.Listener = new GalaxyManager.UserStatsRetrieveListener();
				}
				GalaxyInstance.ListenerRegistrar().Register(ListenerType.USER_STATS_AND_ACHIEVEMENTS_RETRIEVE, GalaxyManager.UserStatsRetrieveListener.Listener);
			}

			// Token: 0x0600E241 RID: 57921 RVA: 0x00521CCE File Offset: 0x0051FECE
			public static void Unregister()
			{
				GalaxyManager.UserStatsRetrieveListener listener = GalaxyManager.UserStatsRetrieveListener.Listener;
				if (listener != null)
				{
					listener.Dispose();
				}
				GalaxyManager.UserStatsRetrieveListener.Listener = null;
			}

			// Token: 0x0600E242 RID: 57922 RVA: 0x00521CE6 File Offset: 0x0051FEE6
			public override void OnUserStatsAndAchievementsRetrieveSuccess(GalaxyID UserID)
			{
				Debug.Log("GALAXY - User stats retrieved.");
				GalaxyManager.Manager.Active = true;
				GalaxyManager.Manager.Synchronize();
			}

			// Token: 0x0600E243 RID: 57923 RVA: 0x00521D07 File Offset: 0x0051FF07
			public override void OnUserStatsAndAchievementsRetrieveFailure(GalaxyID UserID, IUserStatsAndAchievementsRetrieveListener.FailureReason FailureReason)
			{
				Debug.Log(string.Format("GALAXY - Error to retrieve user stats ({0}).", FailureReason));
				GalaxyManager.Manager.Active = false;
			}

			// Token: 0x040064A9 RID: 25769
			public static GalaxyManager.UserStatsRetrieveListener Listener;
		}

		// Token: 0x02001916 RID: 6422
		private class AchievementChangeListener : IAchievementChangeListener
		{
			// Token: 0x0600E245 RID: 57925 RVA: 0x00521D31 File Offset: 0x0051FF31
			public static void Register()
			{
				if (!GalaxyManager.Manager.Initialized)
				{
					return;
				}
				if (GalaxyManager.AchievementChangeListener.Listener == null)
				{
					GalaxyManager.AchievementChangeListener.Listener = new GalaxyManager.AchievementChangeListener();
				}
				GalaxyInstance.ListenerRegistrar().Register(ListenerType.ACHIEVEMENT_CHANGE, GalaxyManager.AchievementChangeListener.Listener);
			}

			// Token: 0x0600E246 RID: 57926 RVA: 0x00521D62 File Offset: 0x0051FF62
			public static void Unregister()
			{
				GalaxyManager.AchievementChangeListener listener = GalaxyManager.AchievementChangeListener.Listener;
				if (listener != null)
				{
					listener.Dispose();
				}
				GalaxyManager.AchievementChangeListener.Listener = null;
			}

			// Token: 0x0600E247 RID: 57927 RVA: 0x00521D7A File Offset: 0x0051FF7A
			public override void OnAchievementUnlocked(string Name)
			{
				Debug.Log("GALAXY - Unlocked achievement: " + Name + ".");
			}

			// Token: 0x040064AA RID: 25770
			public static GalaxyManager.AchievementChangeListener Listener;
		}
	}
}
