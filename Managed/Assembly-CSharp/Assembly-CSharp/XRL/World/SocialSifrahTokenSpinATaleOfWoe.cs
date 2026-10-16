using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E9 RID: 2281
	[Serializable]
	public class SocialSifrahTokenSpinATaleOfWoe : SifrahToken
	{
		// Token: 0x060057DC RID: 22492 RVA: 0x001AA348 File Offset: 0x001A8548
		public SocialSifrahTokenSpinATaleOfWoe()
		{
			this.Description = "spin a tale of woe";
			this.Tile = "Items/sw_mask3.bmp";
			this.RenderString = "Q";
			this.ColorString = "&b";
			this.TileColor = "&B";
			this.DetailColor = 'b';
		}
	}
}
