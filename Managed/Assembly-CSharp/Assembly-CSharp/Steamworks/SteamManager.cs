using System;
using Qud.UI;
using UnityEngine;
using XRL.Collections;

namespace Steamworks
{
	// Token: 0x020004AA RID: 1194
	public class SteamManager
	{
		// Token: 0x06002DC0 RID: 11712 RVA: 0x000FA47F File Offset: 0x000F867F
		public void ResetAchievements()
		{
			Debug.Log("STEAM - Resetting achievements.");
			SteamUserStats.ResetAllStats(true);
			this.Store = true;
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000FA49C File Offset: 0x000F869C
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

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000FA5A8 File Offset: 0x000F87A8
		public bool TryShowVirtualKeyboard(VirtualKeyboardType Type = VirtualKeyboardType.SingleLine, int X = 0, int Y = 0, int Width = 80, int Height = 1)
		{
			try
			{
				EFloatingGamepadTextInputMode efloatingGamepadTextInputMode;
				if (Type != VirtualKeyboardType.MultiLine)
				{
					if (Type != VirtualKeyboardType.Numeric)
					{
						efloatingGamepadTextInputMode = EFloatingGamepadTextInputMode.k_EFloatingGamepadTextInputModeModeSingleLine;
					}
					else
					{
						efloatingGamepadTextInputMode = EFloatingGamepadTextInputMode.k_EFloatingGamepadTextInputModeModeNumeric;
					}
				}
				else
				{
					efloatingGamepadTextInputMode = EFloatingGamepadTextInputMode.k_EFloatingGamepadTextInputModeModeMultipleLines;
				}
				return SteamUtils.ShowFloatingGamepadTextInput(efloatingGamepadTextInputMode, X, Y, Width, Height);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Showing onscreen keyboard", ex, "game_exception");
			}
			return false;
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000FA600 File Offset: 0x000F8800
		public PlatformControlType GetControlType()
		{
			if (this.Deck)
			{
				return PlatformControlType.Deck;
			}
			return PlatformControlType.Undefined;
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000FA60D File Offset: 0x000F880D
		public ControlManager.ControllerFontType GetFontType()
		{
			return ControlManager.ControllerFontType.Default;
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000FA610 File Offset: 0x000F8810
		public bool GetAchievement(string ID)
		{
			bool flag;
			return SteamUserStats.GetAchievement(ID, out flag) && flag;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000FA62A File Offset: 0x000F882A
		public bool SetAchievement(string ID)
		{
			if (this.Active && SteamUserStats.SetAchievement(ID))
			{
				this.Store = true;
				return true;
			}
			return false;
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000FA648 File Offset: 0x000F8848
		public bool UpdateAchievement(string ID, ref bool Achieved, ref DateTime TimeStamp)
		{
			if (!this.Initialized)
			{
				return false;
			}
			bool flag;
			uint num;
			if (!SteamUserStats.GetAchievementAndUnlockTime(ID, out flag, out num))
			{
				return false;
			}
			bool flag2 = false;
			if (num > 0U && TimeStamp == DateTime.MinValue)
			{
				TimeStamp = DateTimeOffset.FromUnixTimeSeconds((long)((ulong)num)).DateTime;
				flag2 = true;
			}
			if (Achieved && !flag)
			{
				SteamUserStats.SetAchievement(ID);
				this.Store = true;
				return flag2;
			}
			if (!Achieved && flag)
			{
				Achieved = true;
				return true;
			}
			return flag2;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000FA6C4 File Offset: 0x000F88C4
		public int GetStat(string ID)
		{
			int num;
			if (SteamUserStats.GetStat(ID, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000FA6DE File Offset: 0x000F88DE
		public void SetStat(string ID, int Value)
		{
			this.Store = SteamUserStats.SetStat(ID, Value);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000FA6F0 File Offset: 0x000F88F0
		public bool UpdateStat(string ID, ref int Value)
		{
			int num;
			if (!SteamUserStats.GetStat(ID, out num))
			{
				return false;
			}
			if (Value > num)
			{
				this.SetStat(ID, Value);
				return false;
			}
			if (Value < num)
			{
				Value = num;
				return true;
			}
			return false;
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000FA724 File Offset: 0x000F8924
		public bool TryShowAchievementProgress(string ID, int Value, int Max)
		{
			return SteamUtils.IsOverlayEnabled() && SteamUserStats.IndicateAchievementProgress(ID, (uint)Value, (uint)Max);
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000FA738 File Offset: 0x000F8938
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
				if (commandLineArgs[i].EqualsNoCase("STEAM:NO"))
				{
					Debug.Log("STEAM - Disabled via command line argument.");
					return;
				}
				i++;
			}
			if (!Packsize.Test())
			{
				Debug.LogError("STEAM - Packsize test failed, the wrong version of Steamworks.NET is being run on this platform.");
			}
			if (!DllCheck.Test())
			{
				Debug.LogError("STEAM - Dll test failed, one or more of the Steamworks binaries seems to be the wrong version.");
			}
			string text;
			ESteamAPIInitResult esteamAPIInitResult = SteamAPI.InitEx(out text);
			this.Initialized = esteamAPIInitResult == ESteamAPIInitResult.k_ESteamAPIInitResult_OK;
			if (!this.Initialized)
			{
				Debug.Log(string.Format("STEAM - could not be initalized: {0} ({1})", esteamAPIInitResult, text));
				return;
			}
			this.Deck = SteamUtils.IsSteamRunningOnSteamDeck();
			Debug.Log("STEAM - Requesting user stats.");
			this.Disposables.Add(Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived)));
			SteamUserStats.RequestCurrentStats();
			this.CheckEntitlements();
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000FA81C File Offset: 0x000F8A1C
		public void CheckEntitlements()
		{
			MetricsManager.LogInfo("Checking entitlements");
			uint num = 3252660U;
			uint num2 = 1247760U;
			bool flag = SteamApps.BIsDlcInstalled(new AppId_t(num)) || SteamApps.BIsSubscribedApp(new AppId_t(num)) || SteamApps.BIsAppInstalled(new AppId_t(num));
			MetricsManager.LogInfo(flag ? "has Pets Pack 1" : "does not have Pets Pack 1");
			bool flag2 = SteamApps.BIsDlcInstalled(new AppId_t(num2)) || SteamApps.BIsAppInstalled(new AppId_t(num2)) || SteamApps.BIsAppInstalled(new AppId_t(num2));
			MetricsManager.LogInfo(flag2 ? "has OST" : "does not have OST");
			if (flag && flag2)
			{
				MainMenu.HasDromadDeluxeEntitlement = true;
			}
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000FA8C4 File Offset: 0x000F8AC4
		private void OnUserStatsReceived(UserStatsReceived_t Callback)
		{
			if (Callback.m_eResult == EResult.k_EResultOK)
			{
				Debug.Log("STEAM - User stats retrieved.");
				this.Active = true;
				this.Synchronize();
				return;
			}
			Debug.Log(string.Format("STEAM - Failed to retrieve user stats ({0}).", Callback.m_eResult));
			this.Active = false;
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000FA914 File Offset: 0x000F8B14
		public void Shutdown()
		{
			if (!this.Initialized)
			{
				return;
			}
			Debug.Log("STEAM - Shutting down.");
			this.Initialized = false;
			this.Active = false;
			foreach (IDisposable disposable in this.Disposables)
			{
				disposable.Dispose();
			}
			this.Disposables.Clear();
			SteamAPI.Shutdown();
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000FA998 File Offset: 0x000F8B98
		public void Update()
		{
			SteamAPI.RunCallbacks();
			if (this.Store)
			{
				SteamUserStats.StoreStats();
				this.Store = false;
			}
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000FA9B4 File Offset: 0x000F8BB4
		public string GetOptionDefault(string OptionID)
		{
			if (this.Deck)
			{
				if (OptionID == "OptionBugDeckKeyboardShift")
				{
					return "Yes";
				}
				if (OptionID == "OptionsPrereleaseInputManager")
				{
					return "Yes";
				}
				if (OptionID == "OptionPrereleaseStageScale")
				{
					return "1.25";
				}
				if (OptionID == "OptionDisplayFramerate")
				{
					return "120";
				}
			}
			return null;
		}

		// Token: 0x04001A00 RID: 6656
		public const uint APP_ID = 333640U;

		// Token: 0x04001A01 RID: 6657
		public bool Active;

		// Token: 0x04001A02 RID: 6658
		public bool Initialized;

		// Token: 0x04001A03 RID: 6659
		public bool Synchronized;

		// Token: 0x04001A04 RID: 6660
		private bool Deck;

		// Token: 0x04001A05 RID: 6661
		private bool Store = true;

		// Token: 0x04001A06 RID: 6662
		internal Rack<IDisposable> Disposables = new Rack<IDisposable>();
	}
}
