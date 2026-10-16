using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E5 RID: 2277
	[Serializable]
	public class SocialSifrahTokenFlatterInsincerely : SifrahToken
	{
		// Token: 0x060057D8 RID: 22488 RVA: 0x001AA258 File Offset: 0x001A8458
		public SocialSifrahTokenFlatterInsincerely()
		{
			this.Description = "flatter insincerely";
			this.Tile = "Items/sw_twohearted.bmp";
			this.RenderString = "\u0006";
			this.ColorString = "&K";
			this.DetailColor = 'W';
		}
	}
}
