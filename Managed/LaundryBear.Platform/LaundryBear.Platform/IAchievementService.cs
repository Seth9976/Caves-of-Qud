using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000008 RID: 8
	public interface IAchievementService
	{
		// Token: 0x06000018 RID: 24
		void GetAchievement(IUser user, string achievmentID, OnAchievementGet callback);
	}
}
