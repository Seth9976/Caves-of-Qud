using System;

namespace XRL.World.Parts
{
	// Token: 0x020012D6 RID: 4822
	[Serializable]
	public class FungiFriendIconColor : IIconColorPart
	{
		// Token: 0x0600BDF5 RID: 48629 RVA: 0x0045A208 File Offset: 0x00458408
		public FungiFriendIconColor()
		{
			this.TextForeground = "&m";
			this.TextForegroundPriority = 90;
			this.TileForeground = "&m";
			this.TileForegroundPriority = 90;
			this.TileDetail = "w";
			this.TileDetailPriority = 90;
		}
	}
}
