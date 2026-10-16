using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008BF RID: 2239
	[Serializable]
	public class RitualSifrahTokenSingHymn : SifrahToken
	{
		// Token: 0x0600574E RID: 22350 RVA: 0x001A840F File Offset: 0x001A660F
		public RitualSifrahTokenSingHymn()
		{
			this.Description = "sing a hymn";
			this.Tile = "Items/sw_cherubic.bmp";
			this.RenderString = "\u000e";
			this.ColorString = "&Y";
			this.DetailColor = 'W';
		}
	}
}
