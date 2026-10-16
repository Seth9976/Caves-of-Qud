using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E7 RID: 2279
	[Serializable]
	public class SocialSifrahTokenInvokeAncientCompacts : SifrahToken
	{
		// Token: 0x060057DA RID: 22490 RVA: 0x001AA2D0 File Offset: 0x001A84D0
		public SocialSifrahTokenInvokeAncientCompacts()
		{
			this.Description = "invoke ancient compacts";
			this.Tile = "Items/sw_scroll2.bmp";
			this.RenderString = "\u0015";
			this.ColorString = "&y";
			this.DetailColor = 'r';
		}
	}
}
