using System;

namespace XRL.UI.Framework
{
	// Token: 0x020014EA RID: 5354
	public class AchievementInfoData : FrameworkDataElement
	{
		// Token: 0x0600D142 RID: 53570 RVA: 0x004DABD3 File Offset: 0x004D8DD3
		public AchievementInfoData(AchievementInfo Achievement)
		{
			this.Achievement = Achievement;
			this.Id = Achievement.ID;
			this.Description = Achievement.Name;
		}

		// Token: 0x0400528C RID: 21132
		public AchievementInfo Achievement;
	}
}
