using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200089C RID: 2204
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathSed : SifrahToken
	{
		// Token: 0x060056E4 RID: 22244 RVA: 0x001A36F8 File Offset: 0x001A18F8
		public PsionicSifrahTokenTenfoldPathSed()
		{
			this.Description = "draw on the grace of Sed";
			this.Tile = "Items/ms_sed.bmp";
			this.RenderString = ")";
			this.ColorString = "&B";
			this.DetailColor = 'Y';
		}
	}
}
