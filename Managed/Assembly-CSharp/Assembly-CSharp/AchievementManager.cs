using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Platform.IO;
using UnityEngine;
using XRL;
using XRL.Messages;
using XRL.Wish;

// Token: 0x0200003D RID: 61
public static class AchievementManager
{
	// Token: 0x06000209 RID: 521 RVA: 0x0000A8A8 File Offset: 0x00008AA8
	public static void Awake()
	{
		if (Achievement.DIE == null)
		{
			return;
		}
		AchievementManager.State.AssignParents();
		AchievementManager.Load().Wait();
		AchievementManager.Active = true;
		PlatformManager.Synchronize();
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000A8D1 File Offset: 0x00008AD1
	public static void Update()
	{
		if (AchievementManager.Write)
		{
			AchievementManager.Write = false;
			Task.Run(new Action(AchievementManager.Save));
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000A8F4 File Offset: 0x00008AF4
	public static async Task Load()
	{
		string text = DataManager.SyncedPath("Achievements.json");
		JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
		};
		if (File.Exists(text))
		{
			ObjectLoadResult<AchievementState> objectLoadResult = await Blob.ReadJsonAsync<AchievementState>(text, jsonSerializerSettings);
			if (!objectLoadResult.WasSuccessful())
			{
				MetricsManager.LogError("Error reading local achievement data", objectLoadResult.ToString());
			}
			else
			{
				AchievementState content = objectLoadResult.content;
				foreach (KeyValuePair<string, AchievementInfo> keyValuePair in content.Achievements)
				{
					AchievementInfo achievementInfo;
					if (AchievementManager.State.Achievements.TryGetValue(keyValuePair.Key, out achievementInfo))
					{
						achievementInfo.Achieved = keyValuePair.Value.Achieved;
						achievementInfo.TimeStamp = keyValuePair.Value.TimeStamp;
					}
				}
				foreach (KeyValuePair<string, StatInfo> keyValuePair2 in content.Stats)
				{
					StatInfo statInfo;
					if (AchievementManager.State.Stats.TryGetValue(keyValuePair2.Key, out statInfo))
					{
						statInfo.Value = keyValuePair2.Value.Value;
					}
				}
			}
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000A930 File Offset: 0x00008B30
	public static void Save()
	{
		AchievementState state = AchievementManager.State;
		lock (state)
		{
			string text = DataManager.SyncedPath("Achievements.json");
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				DefaultValueHandling = DefaultValueHandling.Ignore
			};
			Blob.WriteAllJson(text, AchievementManager.State, jsonSerializerSettings).LogIfErrored();
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000A99C File Offset: 0x00008B9C
	public static void Reset()
	{
		foreach (AchievementInfo achievementInfo in AchievementManager.State.Achievements.Values)
		{
			achievementInfo.Achieved = false;
		}
		foreach (StatInfo statInfo in AchievementManager.State.Stats.Values)
		{
			statInfo.Value = 0;
		}
		PlatformManager.ResetAchievements();
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000AA44 File Offset: 0x00008C44
	public static bool GetAchievement(string ID)
	{
		AchievementInfo achievementInfo;
		return AchievementManager.State.Achievements.TryGetValue(ID, out achievementInfo) && achievementInfo.Achieved;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000AA70 File Offset: 0x00008C70
	public static void SetAchievement(string ID)
	{
		if (!AchievementManager.Enabled)
		{
			return;
		}
		AchievementInfo achievementInfo;
		if (!AchievementManager.State.Achievements.TryGetValue(ID, out achievementInfo))
		{
			Debug.LogWarning("Unknown achievement ID: " + ID);
			return;
		}
		achievementInfo.Unlock();
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000AAB0 File Offset: 0x00008CB0
	public static void IncrementAchievement(string ID, int Value = 1)
	{
		if (!AchievementManager.Enabled)
		{
			return;
		}
		AchievementInfo achievementInfo;
		if (!AchievementManager.State.Achievements.TryGetValue(ID, out achievementInfo))
		{
			Debug.LogWarning("Unknown stat ID: " + ID);
			return;
		}
		if (achievementInfo.Progress == null)
		{
			achievementInfo.Unlock();
			return;
		}
		achievementInfo.Progress.Increment(Value);
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000AB08 File Offset: 0x00008D08
	public static int GetStat(string ID)
	{
		StatInfo statInfo;
		if (!AchievementManager.State.Stats.TryGetValue(ID, out statInfo))
		{
			Debug.LogWarning("Unknown stat ID: " + ID);
			return 0;
		}
		return statInfo.Value;
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000AB44 File Offset: 0x00008D44
	public static void SetStat(string ID, int Value)
	{
		if (!AchievementManager.Enabled)
		{
			return;
		}
		StatInfo statInfo;
		if (!AchievementManager.State.Stats.TryGetValue(ID, out statInfo))
		{
			Debug.LogWarning("Unknown stat ID: " + ID);
			return;
		}
		statInfo.SetValue(Value);
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000AB88 File Offset: 0x00008D88
	public static void IncrementStat(string ID)
	{
		if (!AchievementManager.Enabled)
		{
			return;
		}
		StatInfo statInfo;
		if (!AchievementManager.State.Stats.TryGetValue(ID, out statInfo))
		{
			Debug.LogWarning("Unknown stat ID: " + ID);
			return;
		}
		statInfo.Increment(1);
	}

	// Token: 0x04000118 RID: 280
	public static bool Write = false;

	// Token: 0x04000119 RID: 281
	public static bool Enabled = true;

	// Token: 0x0400011A RID: 282
	public static bool Active;

	// Token: 0x0400011B RID: 283
	public static AchievementState State = new AchievementState(100);

	// Token: 0x02001562 RID: 5474
	[HasWishCommand]
	public class AchievementWishes
	{
		// Token: 0x0600D518 RID: 54552 RVA: 0x004F27F8 File Offset: 0x004F09F8
		[WishCommand("unlockanyachievement", null)]
		public static void DebugUnlockAnyAchievement()
		{
			foreach (KeyValuePair<string, AchievementInfo> keyValuePair in AchievementManager.State.Achievements)
			{
				string text;
				AchievementInfo achievementInfo;
				keyValuePair.Deconstruct(out text, out achievementInfo);
				AchievementInfo achievementInfo2 = achievementInfo;
				if (!achievementInfo2.Achieved)
				{
					MessageQueue.AddPlayerMessage("Unlocked Achievement: " + achievementInfo2.ID, null, true);
					achievementInfo2.Unlock();
					return;
				}
			}
			MessageQueue.AddPlayerMessage("All Achievements Already Unlocked", null, true);
		}
	}
}
