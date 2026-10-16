using System;

namespace XRL.World.Parts
{
	// Token: 0x020012ED RID: 4845
	[Serializable]
	public class SlimewalkerIconColor : IIconColorPart
	{
		// Token: 0x0600BE57 RID: 48727 RVA: 0x0045B997 File Offset: 0x00459B97
		public SlimewalkerIconColor()
		{
			this.TextForeground = "&g";
			this.TextForegroundPriority = 90;
			this.TileForeground = "&g";
			this.TileForegroundPriority = 90;
		}
	}
}
