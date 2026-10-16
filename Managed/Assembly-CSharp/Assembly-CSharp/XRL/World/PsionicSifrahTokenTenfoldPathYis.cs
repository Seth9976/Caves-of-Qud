using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008A1 RID: 2209
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathYis : SifrahToken
	{
		// Token: 0x060056E9 RID: 22249 RVA: 0x001A3824 File Offset: 0x001A1A24
		public PsionicSifrahTokenTenfoldPathYis()
		{
			this.Description = "draw on the depths of Yis";
			this.Tile = "Items/ms_yis.bmp";
			this.RenderString = "*";
			this.ColorString = "&m";
			this.DetailColor = 'Y';
		}
	}
}
