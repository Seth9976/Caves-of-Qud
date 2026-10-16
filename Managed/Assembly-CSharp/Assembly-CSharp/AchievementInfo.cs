using System;
using Newtonsoft.Json;
using Qud.UI;
using UnityEngine;
using XRL;

// Token: 0x0200003B RID: 59
[JsonObject(MemberSerialization.OptIn)]
public class AchievementInfo
{
	// Token: 0x060001FE RID: 510 RVA: 0x0000A557 File Offset: 0x00008757
	public AchievementInfo()
	{
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000A560 File Offset: 0x00008760
	public AchievementInfo(string ID, string Name, string Icon, string Description, bool Hidden = false)
	{
		this.ID = ID;
		this.Name = Name;
		this.Description = Description;
		this.Hidden = Hidden;
		this.IconUnlocked = (Icon.Contains('/') ? Icon : ("UI/Achievements/" + Icon));
		this.IconLocked = this.IconUnlocked.Replace(".", "_u.");
		AchievementManager.State.Achievements.Add(ID, this);
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000A5DB File Offset: 0x000087DB
	public AchievementInfo(string ID, string Name, string Icon, string Description, string Progress, int Value, int MaxValue = 2147483647, bool Hidden = false)
		: this(ID, Name, Icon, Description, Hidden)
	{
		this.AchievedAt = Value;
		this.Progress = StatInfo.Create(Progress, MaxValue);
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000A601 File Offset: 0x00008801
	public AchievementInfo(string ID, string Name, string Icon, string Description, string Progress, string[] Consumed, bool Hidden = false)
		: this(ID, Name, Icon, Description, Hidden)
	{
		this.AchievedAt = Consumed.Length;
		this.Progress = StatInfo.Create(Progress, Consumed);
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000A629 File Offset: 0x00008829
	public void Unlock()
	{
		if (this.Achieved || !AchievementManager.Enabled)
		{
			return;
		}
		this.Achieved = true;
		this.TimeStamp = DateTime.Now;
		AchievementManager.Write = true;
		if (!PlatformManager.TrySetAchievement(this))
		{
			this.NotifyUnlock();
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000A661 File Offset: 0x00008861
	public void Indicate()
	{
		if (this.Achieved || !AchievementManager.Enabled)
		{
			return;
		}
		if (!PlatformManager.TryShowAchievementProgress(this))
		{
			this.NotifyProgress();
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0000A684 File Offset: 0x00008884
	public void NotifyProgress()
	{
		string name = this.Name;
		string text = "Achievement progress.";
		string iconLocked = this.IconLocked;
		string text2 = null;
		StatInfo progress = this.Progress;
		int? num = ((progress != null) ? new int?(progress.Value) : null);
		int? num2 = new int?(this.AchievedAt);
		Color? color = new Color?(The.Color.DarkCyan);
		Color? color2 = new Color?(The.Color.Black);
		Notification.Enqueue(name, text, iconLocked, text2, num, num2, color, null, color2);
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000A6FC File Offset: 0x000088FC
	public void NotifyUnlock()
	{
		string name = this.Name;
		string text = "Achievement unlocked!";
		string iconUnlocked = this.IconUnlocked;
		string text2 = "Sounds/UI/ui_achievement_unlock";
		Color? color = new Color?(The.Color.Yellow);
		Color? color2 = new Color?(The.Color.Yellow);
		Notification.Enqueue(name, text, iconUnlocked, text2, null, null, color, null, color2);
	}

	// Token: 0x0400010C RID: 268
	public readonly string ID;

	// Token: 0x0400010D RID: 269
	public readonly string Name;

	// Token: 0x0400010E RID: 270
	public readonly string Description;

	// Token: 0x0400010F RID: 271
	public readonly string IconUnlocked;

	// Token: 0x04000110 RID: 272
	public readonly string IconLocked;

	// Token: 0x04000111 RID: 273
	public readonly int AchievedAt;

	// Token: 0x04000112 RID: 274
	public readonly bool Hidden;

	// Token: 0x04000113 RID: 275
	public readonly StatInfo Progress;

	// Token: 0x04000114 RID: 276
	[JsonProperty]
	public bool Achieved;

	// Token: 0x04000115 RID: 277
	[JsonProperty]
	public DateTime TimeStamp;
}
