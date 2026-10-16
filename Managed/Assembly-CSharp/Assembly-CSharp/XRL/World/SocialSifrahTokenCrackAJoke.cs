using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E3 RID: 2275
	[Serializable]
	public class SocialSifrahTokenCrackAJoke : SifrahToken
	{
		// Token: 0x060057D6 RID: 22486 RVA: 0x001AA1E0 File Offset: 0x001A83E0
		public SocialSifrahTokenCrackAJoke()
		{
			this.Description = "crack a joke";
			this.Tile = "Items/sw_mask.bmp";
			this.RenderString = "\u0001";
			this.ColorString = "&C";
			this.DetailColor = 'W';
		}
	}
}
