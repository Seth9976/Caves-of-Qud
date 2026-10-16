using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200089D RID: 2205
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathVur : SifrahToken
	{
		// Token: 0x060056E5 RID: 22245 RVA: 0x001A3734 File Offset: 0x001A1934
		public PsionicSifrahTokenTenfoldPathVur()
		{
			this.Description = "draw on the might of Vur";
			this.Tile = "Items/ms_vur.bmp";
			this.RenderString = "(";
			this.ColorString = "&R";
			this.DetailColor = 'Y';
		}
	}
}
