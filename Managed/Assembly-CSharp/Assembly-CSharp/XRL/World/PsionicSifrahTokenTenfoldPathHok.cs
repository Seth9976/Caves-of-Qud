using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200089A RID: 2202
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathHok : SifrahToken
	{
		// Token: 0x060056E2 RID: 22242 RVA: 0x001A3680 File Offset: 0x001A1880
		public PsionicSifrahTokenTenfoldPathHok()
		{
			this.Description = "draw on the creativity of Hok";
			this.Tile = "Items/ms_hok.bmp";
			this.RenderString = ")";
			this.ColorString = "&y";
			this.DetailColor = 'Y';
		}
	}
}
