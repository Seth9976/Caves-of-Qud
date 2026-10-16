using System;

namespace XRL.World.Parts
{
	// Token: 0x020012F5 RID: 4853
	[Serializable]
	public class WardenIconColor : IIconColorPart
	{
		// Token: 0x0600BE71 RID: 48753 RVA: 0x0045BF08 File Offset: 0x0045A108
		public WardenIconColor()
		{
			this.TextForeground = "&B";
			this.TextForegroundPriority = 100;
			this.TileForeground = "&B";
			this.TileForegroundPriority = 100;
		}
	}
}
