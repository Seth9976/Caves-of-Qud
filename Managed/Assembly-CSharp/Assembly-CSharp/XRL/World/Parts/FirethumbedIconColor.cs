using System;

namespace XRL.World.Parts
{
	// Token: 0x020012D5 RID: 4821
	[Serializable]
	public class FirethumbedIconColor : IIconColorPart
	{
		// Token: 0x0600BDF4 RID: 48628 RVA: 0x0045A1D7 File Offset: 0x004583D7
		public FirethumbedIconColor()
		{
			this.TextForeground = "&R";
			this.TextForegroundPriority = 90;
			this.TileForeground = "&R";
			this.TileForegroundPriority = 90;
		}
	}
}
