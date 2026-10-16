using System;

namespace XRL.World.Parts
{
	// Token: 0x020012D8 RID: 4824
	[Serializable]
	public class HeroIconColor : IIconColorPart
	{
		// Token: 0x0600BDF7 RID: 48631 RVA: 0x0045A2A0 File Offset: 0x004584A0
		public HeroIconColor()
		{
			this.TextForeground = "&M";
			this.TextForegroundPriority = 100;
			this.TileForeground = "&M";
			this.TileForegroundPriority = 100;
		}
	}
}
