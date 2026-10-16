using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008EC RID: 2284
	[Serializable]
	public class SocialSifrahTokenRailAgainstInjustice : SifrahToken
	{
		// Token: 0x060057DF RID: 22495 RVA: 0x001AA42C File Offset: 0x001A862C
		public SocialSifrahTokenRailAgainstInjustice()
		{
			this.Description = "rail against injustice";
			this.Tile = "Items/sw_gianthands.bmp";
			this.RenderString = "í";
			this.ColorString = "&w";
			this.TileColor = "&w";
			this.DetailColor = 'r';
		}
	}
}
