using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008C0 RID: 2240
	[Serializable]
	public class RitualSifrahTokenSingAHistoricalEpic : SifrahToken
	{
		// Token: 0x0600574F RID: 22351 RVA: 0x001A844B File Offset: 0x001A664B
		public RitualSifrahTokenSingAHistoricalEpic()
		{
			this.Description = "sing a historical epic";
			this.Tile = "Items/sw_cherubic.bmp";
			this.RenderString = "\u000e";
			this.ColorString = "&Y";
			this.DetailColor = 'W';
		}
	}
}
