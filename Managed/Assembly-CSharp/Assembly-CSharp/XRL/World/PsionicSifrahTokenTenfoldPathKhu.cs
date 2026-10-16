using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008A2 RID: 2210
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathKhu : SifrahToken
	{
		// Token: 0x060056EA RID: 22250 RVA: 0x001A3860 File Offset: 0x001A1A60
		public PsionicSifrahTokenTenfoldPathKhu()
		{
			this.Description = "draw on the solidity of Khu";
			this.Tile = "Items/ms_khu.bmp";
			this.RenderString = "*";
			this.ColorString = "&g";
			this.DetailColor = 'Y';
		}
	}
}
