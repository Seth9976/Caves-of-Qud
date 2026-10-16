using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000048 RID: 72
	[Obsolete("UDP support will be removed in the next major update of In-App Purchasing. Right now, the UDP SDK will still function normally in tandem with IAP.")]
	public class UDP
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005868 File Offset: 0x00003A68
		public static string Name
		{
			get
			{
				string text;
				try
				{
					text = StoreServiceInterface.GetName() ?? "UDP";
				}
				catch (Exception)
				{
					text = "UDP";
				}
				return text;
			}
		}
	}
}
