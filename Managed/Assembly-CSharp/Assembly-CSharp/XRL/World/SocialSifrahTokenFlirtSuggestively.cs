using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E8 RID: 2280
	[Serializable]
	public class SocialSifrahTokenFlirtSuggestively : SifrahToken
	{
		// Token: 0x060057DB RID: 22491 RVA: 0x001AA30C File Offset: 0x001A850C
		public SocialSifrahTokenFlirtSuggestively()
		{
			this.Description = "flirt suggestively";
			this.Tile = "Items/sw_banana.bmp";
			this.RenderString = "õ";
			this.ColorString = "&W";
			this.DetailColor = 'Y';
		}
	}
}
