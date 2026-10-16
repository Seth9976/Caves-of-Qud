using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200089F RID: 2207
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathTza : SifrahToken
	{
		// Token: 0x060056E7 RID: 22247 RVA: 0x001A37AC File Offset: 0x001A19AC
		public PsionicSifrahTokenTenfoldPathTza()
		{
			this.Description = "draw on the constancy of Tza";
			this.Tile = "Items/ms_tza.bmp";
			this.RenderString = ")";
			this.ColorString = "&c";
			this.DetailColor = 'Y';
		}
	}
}
