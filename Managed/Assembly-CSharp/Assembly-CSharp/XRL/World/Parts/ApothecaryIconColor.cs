using System;

namespace XRL.World.Parts
{
	// Token: 0x020012D0 RID: 4816
	[Serializable]
	public class ApothecaryIconColor : IIconColorPart
	{
		// Token: 0x0600BDD1 RID: 48593 RVA: 0x004595C3 File Offset: 0x004577C3
		public ApothecaryIconColor()
		{
			this.TextForeground = "&g";
			this.TextForegroundPriority = 100;
			this.TileForeground = "&g";
			this.TileForegroundPriority = 100;
		}
	}
}
