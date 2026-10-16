using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Token: 0x0200003E RID: 62
[JsonObject(MemberSerialization.OptIn)]
public class StatInfo
{
	// Token: 0x06000215 RID: 533 RVA: 0x0000ABE3 File Offset: 0x00008DE3
	public StatInfo()
	{
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000AC01 File Offset: 0x00008E01
	private StatInfo(string ID, int MaxValue = 0)
	{
		this.ID = ID;
		this.MaxValue = MaxValue;
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000AC30 File Offset: 0x00008E30
	public static StatInfo Create(string ID, int MaxValue = 1)
	{
		Dictionary<string, StatInfo> stats = AchievementManager.State.Stats;
		StatInfo statInfo;
		if (stats.TryGetValue(ID, out statInfo))
		{
			statInfo.MaxValue = Math.Max(MaxValue, statInfo.MaxValue);
		}
		else
		{
			statInfo = (stats[ID] = new StatInfo(ID, MaxValue));
		}
		return statInfo;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000AC78 File Offset: 0x00008E78
	public static StatInfo Create(string ID, string[] Consumed)
	{
		Dictionary<string, StatInfo> stats = AchievementManager.State.Stats;
		if (stats.ContainsKey(ID))
		{
			throw new ArgumentException("Attempting to supply new children to existing StatInfo: " + ID);
		}
		int num = Consumed.Length;
		StatInfo statInfo = new StatInfo(ID, num);
		statInfo.Children = new StatInfo[num];
		for (int i = 0; i < num; i++)
		{
			statInfo.Children[i] = StatInfo.Create(Consumed[i], 1);
			statInfo.Children[i].Parent = statInfo;
		}
		stats[ID] = statInfo;
		return statInfo;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000ACF6 File Offset: 0x00008EF6
	public void Increment(int Value = 1)
	{
		this.SetValue(this.Value + Value);
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000AD08 File Offset: 0x00008F08
	public void SetValue(int Value)
	{
		if (!AchievementManager.Enabled)
		{
			return;
		}
		Value = Math.Min(Value, this.MaxValue);
		if (Value == this.Value)
		{
			return;
		}
		this.Value = Value;
		AchievementManager.Write = true;
		PlatformManager.SetStat(this);
		if (this.Parent != null)
		{
			this.Parent.Consume();
		}
		if (this.Achievements.Length != 0)
		{
			bool flag = false;
			int i = 0;
			int num = this.Achievements.Length;
			while (i < num)
			{
				AchievementInfo achievementInfo = this.Achievements[i];
				if (!achievementInfo.Achieved && achievementInfo.AchievedAt <= Value)
				{
					achievementInfo.Unlock();
					flag = true;
				}
				i++;
			}
			if (!flag && (this.MaxValue < 50 || Value % (this.MaxValue / 10) == 0))
			{
				this.Indicate();
			}
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000ADC0 File Offset: 0x00008FC0
	public void Consume()
	{
		if (!AchievementManager.Enabled)
		{
			return;
		}
		int num = this.Children.Length;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			StatInfo statInfo = this.Children[i];
			if (statInfo.Value >= statInfo.MaxValue)
			{
				num2++;
			}
		}
		this.SetValue(num2);
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000AE0E File Offset: 0x0000900E
	public void Indicate()
	{
		if (!AchievementManager.Enabled)
		{
			return;
		}
		AchievementInfo achievementInfo = Array.Find<AchievementInfo>(this.Achievements, (AchievementInfo x) => !x.Achieved);
		if (achievementInfo == null)
		{
			return;
		}
		achievementInfo.Indicate();
	}

	// Token: 0x0400011C RID: 284
	public readonly string ID;

	// Token: 0x0400011D RID: 285
	[JsonProperty]
	public int Value;

	// Token: 0x0400011E RID: 286
	public int MaxValue;

	// Token: 0x0400011F RID: 287
	public StatInfo Parent;

	// Token: 0x04000120 RID: 288
	public AchievementInfo[] Achievements = Array.Empty<AchievementInfo>();

	// Token: 0x04000121 RID: 289
	public StatInfo[] Children = Array.Empty<StatInfo>();
}
