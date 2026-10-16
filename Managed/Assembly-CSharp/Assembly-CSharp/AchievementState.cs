using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wintellect.PowerCollections;

// Token: 0x0200003C RID: 60
[JsonObject(MemberSerialization.OptIn)]
public class AchievementState
{
	// Token: 0x06000206 RID: 518 RVA: 0x0000A763 File Offset: 0x00008963
	public AchievementState()
	{
		this.Achievements = new Dictionary<string, AchievementInfo>();
		this.Stats = new Dictionary<string, StatInfo>();
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000A781 File Offset: 0x00008981
	public AchievementState(int Capacity)
	{
		this.Achievements = new Dictionary<string, AchievementInfo>(Capacity);
		this.Stats = new Dictionary<string, StatInfo>(Capacity);
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000A7A4 File Offset: 0x000089A4
	public void AssignParents()
	{
		List<AchievementInfo> list = new List<AchievementInfo>(4);
		Comparison<AchievementInfo> comparison = (AchievementInfo a, AchievementInfo b) => a.AchievedAt.CompareTo(b.AchievedAt);
		foreach (KeyValuePair<string, StatInfo> keyValuePair in this.Stats)
		{
			string text;
			StatInfo statInfo;
			keyValuePair.Deconstruct(out text, out statInfo);
			StatInfo statInfo2 = statInfo;
			foreach (KeyValuePair<string, AchievementInfo> keyValuePair2 in this.Achievements)
			{
				AchievementInfo achievementInfo;
				keyValuePair2.Deconstruct(out text, out achievementInfo);
				AchievementInfo achievementInfo2 = achievementInfo;
				if (achievementInfo2.Progress == statInfo2)
				{
					list.Add(achievementInfo2);
				}
			}
			statInfo2.Achievements = list.ToArray();
			Algorithms.StableSortInPlace<AchievementInfo>(statInfo2.Achievements, comparison);
			list.Clear();
		}
	}

	// Token: 0x04000116 RID: 278
	[JsonProperty("Achievements")]
	public Dictionary<string, AchievementInfo> Achievements;

	// Token: 0x04000117 RID: 279
	[JsonProperty("Stats")]
	public Dictionary<string, StatInfo> Stats;
}
