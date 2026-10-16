using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008A0 RID: 2208
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathHod : SifrahToken
	{
		// Token: 0x060056E8 RID: 22248 RVA: 0x001A37E8 File Offset: 0x001A19E8
		public PsionicSifrahTokenTenfoldPathHod()
		{
			this.Description = "draw on the majesty of Hod";
			this.Tile = "Items/ms_hod.bmp";
			this.RenderString = "(";
			this.ColorString = "&O";
			this.DetailColor = 'Y';
		}
	}
}
