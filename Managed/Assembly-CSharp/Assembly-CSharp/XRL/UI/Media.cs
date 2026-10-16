using System;
using Qud.UI;
using UnityEngine;

namespace XRL.UI
{
	// Token: 0x020014BB RID: 5307
	public static class Media
	{
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600D052 RID: 53330 RVA: 0x004CDFF2 File Offset: 0x004CC1F2
		public static Media.SizeClass sizeClass
		{
			get
			{
				if ((float)Screen.width / UIManager.scale < 1536f)
				{
					return Media.SizeClass.Small;
				}
				return Media.SizeClass.Medium;
			}
		}

		// Token: 0x040051C3 RID: 20931
		public static string WEB_COLOR_YELLOW = "858951";

		// Token: 0x02001DCA RID: 7626
		public enum SizeClass
		{
			// Token: 0x0400741A RID: 29722
			Unset = -2147483648,
			// Token: 0x0400741B RID: 29723
			Small = 0,
			// Token: 0x0400741C RID: 29724
			Medium = 100,
			// Token: 0x0400741D RID: 29725
			Large = 200
		}
	}
}
