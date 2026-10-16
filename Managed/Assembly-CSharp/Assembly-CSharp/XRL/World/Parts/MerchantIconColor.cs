using System;

namespace XRL.World.Parts
{
	// Token: 0x020012DD RID: 4829
	[Serializable]
	public class MerchantIconColor : IIconColorPart
	{
		// Token: 0x0600BE1C RID: 48668 RVA: 0x0045ABC8 File Offset: 0x00458DC8
		public MerchantIconColor()
		{
			this.TextForeground = "&W";
			this.TextForegroundPriority = 100;
			this.TileForeground = "&W";
			this.TileForegroundPriority = 100;
		}
	}
}
