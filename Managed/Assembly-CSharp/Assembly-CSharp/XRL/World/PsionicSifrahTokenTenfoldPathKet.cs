using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000899 RID: 2201
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathKet : SifrahToken
	{
		// Token: 0x060056E1 RID: 22241 RVA: 0x001A3644 File Offset: 0x001A1844
		public PsionicSifrahTokenTenfoldPathKet()
		{
			this.Description = "draw on the authority of Ket";
			this.Tile = "Items/ms_ket.bmp";
			this.RenderString = "^";
			this.ColorString = "&Y";
			this.DetailColor = 'Y';
		}
	}
}
