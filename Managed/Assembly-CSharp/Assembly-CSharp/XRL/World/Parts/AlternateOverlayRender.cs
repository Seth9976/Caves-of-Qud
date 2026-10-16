using System;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02001249 RID: 4681
	[Serializable]
	public class AlternateOverlayRender : IPart
	{
		// Token: 0x0600BA1B RID: 47643 RVA: 0x0043F893 File Offset: 0x0043DA93
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA1C RID: 47644 RVA: 0x0043F898 File Offset: 0x0043DA98
		public override bool OverlayRender(RenderEvent E)
		{
			if (!string.IsNullOrEmpty(this.ColorString) && Globals.RenderMode == RenderModeType.Tiles)
			{
				E.ColorString = this.ColorString;
			}
			if (!string.IsNullOrEmpty(this.DetailColor) && Globals.RenderMode == RenderModeType.Tiles)
			{
				E.DetailColor = this.DetailColor;
			}
			return true;
		}

		// Token: 0x04004ACA RID: 19146
		public string ColorString;

		// Token: 0x04004ACB RID: 19147
		public string DetailColor;
	}
}
