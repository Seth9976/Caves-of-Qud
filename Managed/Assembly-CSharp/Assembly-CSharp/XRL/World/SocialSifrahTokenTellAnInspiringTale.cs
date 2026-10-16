using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008EA RID: 2282
	[Serializable]
	public class SocialSifrahTokenTellAnInspiringTale : SifrahToken
	{
		// Token: 0x060057DD RID: 22493 RVA: 0x001AA39C File Offset: 0x001A859C
		public SocialSifrahTokenTellAnInspiringTale()
		{
			this.Description = "tell an inspiring tale";
			this.Tile = "Items/sw_mask3.bmp";
			this.RenderString = "Q";
			this.ColorString = "&b";
			this.TileColor = "&B";
			this.DetailColor = 'M';
		}
	}
}
