using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008BE RID: 2238
	[Serializable]
	public class RitualSifrahTokenPrayHumbly : SifrahToken
	{
		// Token: 0x0600574D RID: 22349 RVA: 0x001A83D3 File Offset: 0x001A65D3
		public RitualSifrahTokenPrayHumbly()
		{
			this.Description = "pray humbly";
			this.Tile = "Items/sw_gianthands.bmp";
			this.RenderString = "\u0014";
			this.ColorString = "&y";
			this.DetailColor = 'W';
		}
	}
}
