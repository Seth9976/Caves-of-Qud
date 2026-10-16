using System;
using System.Threading.Tasks;
using Galaxy;
using Steamworks;

// Token: 0x02000069 RID: 105
public static class PlatformManager
{
	// Token: 0x06000399 RID: 921 RVA: 0x000179C0 File Offset: 0x00015BC0
	public static bool AreLeaderboardsOffline()
	{
		return PlatformManager.m_inOfflineMode;
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000179C8 File Offset: 0x00015BC8
	public static async Task<PlatformManager.TryGetNetworkResult> TryGetLeaderboardsReady()
	{
		return PlatformManager.Steam.Initialized ? PlatformManager.TryGetNetworkResult.Success : PlatformManager.TryGetNetworkResult.Failure;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00017A03 File Offset: 0x00015C03
	public static void TryGetLeaderboardsReady(Action<bool> onComplete)
	{
		if (onComplete != null)
		{
			onComplete(PlatformManager.Steam.Initialized);
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00017A18 File Offset: 0x00015C18
	public static void SetOfflineMode(bool isOffline)
	{
		PlatformManager.m_inOfflineMode = isOffline;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00017A20 File Offset: 0x00015C20
	public static bool IsInitialized()
	{
		return PlatformManager.Steam.Initialized;
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00017A2C File Offset: 0x00015C2C
	public static void Awake()
	{
		PlatformManager.Steam.Awake();
		PlatformManager.Galaxy.Awake();
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00017A44 File Offset: 0x00015C44
	public static void UpdateAchievement(AchievementInfo Achievement)
	{
		if (PlatformManager.Steam.Active)
		{
			PlatformManager.Steam.UpdateAchievement(Achievement.ID, ref Achievement.Achieved, ref Achievement.TimeStamp);
		}
		if (PlatformManager.Galaxy.Active)
		{
			PlatformManager.Galaxy.UpdateAchievement(Achievement.ID, ref Achievement.Achieved, ref Achievement.TimeStamp);
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00017AA4 File Offset: 0x00015CA4
	public static void UpdateStat(StatInfo Stat)
	{
		if (PlatformManager.Steam.Active)
		{
			PlatformManager.Steam.UpdateStat(Stat.ID, ref Stat.Value);
		}
		if (PlatformManager.Galaxy.Active)
		{
			PlatformManager.Galaxy.UpdateStat(Stat.ID, ref Stat.Value);
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00017AF7 File Offset: 0x00015CF7
	public static bool GetAchievement(AchievementInfo Achievement)
	{
		if (PlatformManager.Steam.Active)
		{
			return PlatformManager.Steam.GetAchievement(Achievement.ID);
		}
		return PlatformManager.Galaxy.Active && PlatformManager.Galaxy.GetAchievement(Achievement.ID);
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00017B34 File Offset: 0x00015D34
	public static bool TrySetAchievement(AchievementInfo Achievement)
	{
		if (PlatformManager.Steam.Active && PlatformManager.Steam.SetAchievement(Achievement.ID))
		{
			return true;
		}
		if (PlatformManager.Galaxy.Active)
		{
			PlatformManager.Galaxy.SetAchievement(Achievement.ID);
			return true;
		}
		return false;
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00017B80 File Offset: 0x00015D80
	public static int GetStat(StatInfo Stat)
	{
		if (PlatformManager.Steam.Active)
		{
			return PlatformManager.Steam.GetStat(Stat.ID);
		}
		if (PlatformManager.Galaxy.Active)
		{
			return PlatformManager.Galaxy.GetStat(Stat.ID);
		}
		return 0;
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00017BC0 File Offset: 0x00015DC0
	public static void SetStat(StatInfo Stat)
	{
		if (PlatformManager.Steam.Active)
		{
			PlatformManager.Steam.SetStat(Stat.ID, Stat.Value);
		}
		if (PlatformManager.Galaxy.Active)
		{
			PlatformManager.Galaxy.SetStat(Stat.ID, Stat.Value);
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00017C11 File Offset: 0x00015E11
	public static void Synchronize()
	{
		if (PlatformManager.Steam.Active)
		{
			PlatformManager.Steam.Synchronize();
		}
		if (PlatformManager.Galaxy.Active)
		{
			PlatformManager.Galaxy.Synchronize();
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00017C3F File Offset: 0x00015E3F
	public static void Update()
	{
		if (PlatformManager.Steam.Initialized)
		{
			PlatformManager.Steam.Update();
		}
		if (PlatformManager.Galaxy.Initialized)
		{
			PlatformManager.Galaxy.Update();
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00017C6D File Offset: 0x00015E6D
	public static void Shutdown()
	{
		MetricsManager.LogInfo("Platform - Shutdown Starting");
		PlatformManager.Steam.Shutdown();
		PlatformManager.Galaxy.Shutdown();
		MetricsManager.LogInfo("Platform - Shutdown Complete");
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00017C97 File Offset: 0x00015E97
	public static void ResetAchievements()
	{
		if (PlatformManager.Steam.Active)
		{
			PlatformManager.Steam.ResetAchievements();
		}
		if (PlatformManager.Galaxy.Active)
		{
			PlatformManager.Galaxy.ResetAchievements();
		}
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00017CC8 File Offset: 0x00015EC8
	public static bool TryShowAchievementProgress(AchievementInfo Achievement)
	{
		if (Achievement.Progress == null)
		{
			return false;
		}
		int value = Achievement.Progress.Value;
		int achievedAt = Achievement.AchievedAt;
		return PlatformManager.Steam.Active && PlatformManager.Steam.TryShowAchievementProgress(Achievement.ID, value, achievedAt);
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00017D15 File Offset: 0x00015F15
	public static bool IsOverlayEnabled()
	{
		return false;
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00017D18 File Offset: 0x00015F18
	public static PlatformControlType GetControlType()
	{
		if (PlatformManager.Steam.Initialized)
		{
			return PlatformManager.Steam.GetControlType();
		}
		return PlatformControlType.Undefined;
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00017D34 File Offset: 0x00015F34
	public static string GetOptionDefault(string OptionID)
	{
		string text = null;
		if (PlatformManager.Steam.Initialized)
		{
			text = PlatformManager.Steam.GetOptionDefault(OptionID);
		}
		return text;
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00017D5C File Offset: 0x00015F5C
	public static bool TryShowVirtualKeyboard(VirtualKeyboardType Type = VirtualKeyboardType.SingleLine, int X = 0, int Y = 0, int Width = 80, int Height = 1)
	{
		return PlatformManager.Steam.Initialized && PlatformManager.Steam.TryShowVirtualKeyboard(Type, X, Y, Width, Height);
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00017D7F File Offset: 0x00015F7F
	public static ControlManager.ControllerFontType GetFontType()
	{
		if (PlatformManager.Steam.Initialized)
		{
			return PlatformManager.Steam.GetFontType();
		}
		return ControlManager.ControllerFontType.Default;
	}

	// Token: 0x040002C6 RID: 710
	public static bool m_inOfflineMode = false;

	// Token: 0x040002C7 RID: 711
	public static readonly SteamManager Steam = new SteamManager();

	// Token: 0x040002C8 RID: 712
	public static readonly GalaxyManager Galaxy = new GalaxyManager();

	// Token: 0x02001584 RID: 5508
	public enum TryGetNetworkResult
	{
		// Token: 0x0400569D RID: 22173
		Success,
		// Token: 0x0400569E RID: 22174
		Failure,
		// Token: 0x0400569F RID: 22175
		Cancelled
	}
}
