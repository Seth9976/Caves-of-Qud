using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E1 RID: 2273
	[Serializable]
	public class SocialSifrahTokenSociableChat : SifrahToken
	{
		// Token: 0x060057D4 RID: 22484 RVA: 0x001AA168 File Offset: 0x001A8368
		public SocialSifrahTokenSociableChat()
		{
			this.Description = "sociable chat";
			this.Tile = "Items/sw_cherubic.bmp";
			this.RenderString = "\u0002";
			this.ColorString = "&w";
			this.DetailColor = 'W';
		}
	}
}
