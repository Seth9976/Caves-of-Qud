using System;

namespace XRL.World.Parts
{
	// Token: 0x020012E0 RID: 4832
	[Serializable]
	public class NephilimCultistIconColor : IIconColorPart
	{
		// Token: 0x0600BE21 RID: 48673 RVA: 0x0045ACF4 File Offset: 0x00458EF4
		public NephilimCultistIconColor()
			: this(null, null)
		{
		}

		// Token: 0x0600BE22 RID: 48674 RVA: 0x0045ACFE File Offset: 0x00458EFE
		public NephilimCultistIconColor(string Foreground)
			: this(Foreground, Foreground)
		{
		}

		// Token: 0x0600BE23 RID: 48675 RVA: 0x0045AD08 File Offset: 0x00458F08
		public NephilimCultistIconColor(string TextForeground, string TileForeground)
		{
			this.TextForeground = TextForeground;
			this.TextForegroundPriority = 95;
			this.TileForeground = TileForeground;
			this.TileForegroundPriority = 95;
		}
	}
}
