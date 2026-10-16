using System;

namespace XRL.World.Parts
{
	// Token: 0x020012D7 RID: 4823
	[Serializable]
	public class FungusRiddenIconColor : IIconColorPart
	{
		// Token: 0x0600BDF6 RID: 48630 RVA: 0x0045A254 File Offset: 0x00458454
		public FungusRiddenIconColor()
		{
			this.TextForeground = "&m";
			this.TextForegroundPriority = 90;
			this.TileForeground = "&m";
			this.TileForegroundPriority = 90;
			this.TileDetail = "W";
			this.TileDetailPriority = 90;
		}
	}
}
