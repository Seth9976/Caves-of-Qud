using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E6 RID: 2278
	[Serializable]
	public class SocialSifrahTokenDebateRationally : SifrahToken
	{
		// Token: 0x060057D9 RID: 22489 RVA: 0x001AA294 File Offset: 0x001A8494
		public SocialSifrahTokenDebateRationally()
		{
			this.Description = "debate rationally";
			this.Tile = "Items/sw_book1.bmp";
			this.RenderString = "\u0014";
			this.ColorString = "&w";
			this.DetailColor = 'W';
		}
	}
}
