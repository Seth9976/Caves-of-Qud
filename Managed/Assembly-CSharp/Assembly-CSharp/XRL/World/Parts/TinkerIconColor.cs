using System;

namespace XRL.World.Parts
{
	// Token: 0x020012EF RID: 4847
	[Serializable]
	public class TinkerIconColor : IIconColorPart
	{
		// Token: 0x0600BE60 RID: 48736 RVA: 0x0045BC68 File Offset: 0x00459E68
		public TinkerIconColor()
		{
			this.TextForeground = "&c";
			this.TextForegroundPriority = 100;
			this.TileForeground = "&c";
			this.TileForegroundPriority = 100;
		}
	}
}
