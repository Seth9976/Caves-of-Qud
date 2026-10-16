using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000007 RID: 7
	public interface IAchievement
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18
		IUser User { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19
		bool IsSecret { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20
		string LockedDescription { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21
		string UnlockedDescription { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22
		int Progress { get; }

		// Token: 0x06000017 RID: 23
		void SetProgress(int progress, OnAchievementSet onComplete);
	}
}
