using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200089B RID: 2203
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathBin : SifrahToken
	{
		// Token: 0x060056E3 RID: 22243 RVA: 0x001A36BC File Offset: 0x001A18BC
		public PsionicSifrahTokenTenfoldPathBin()
		{
			this.Description = "draw on the insights of Bin";
			this.Tile = "Items/ms_bin.bmp";
			this.RenderString = "(";
			this.ColorString = "&K";
			this.DetailColor = 'Y';
		}
	}
}
