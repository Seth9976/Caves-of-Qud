using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E2 RID: 2274
	[Serializable]
	public class SocialSifrahTokenListenSympathetically : SifrahToken
	{
		// Token: 0x060057D5 RID: 22485 RVA: 0x001AA1A4 File Offset: 0x001A83A4
		public SocialSifrahTokenListenSympathetically()
		{
			this.Description = "listen sympathetically";
			this.Tile = "Items/sw_heightenedhearing.bmp";
			this.RenderString = "@";
			this.ColorString = "&y";
			this.DetailColor = 'w';
		}
	}
}
