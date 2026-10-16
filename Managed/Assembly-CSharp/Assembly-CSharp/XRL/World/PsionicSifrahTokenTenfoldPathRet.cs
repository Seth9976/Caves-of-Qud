using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200089E RID: 2206
	[Serializable]
	public class PsionicSifrahTokenTenfoldPathRet : SifrahToken
	{
		// Token: 0x060056E6 RID: 22246 RVA: 0x001A3770 File Offset: 0x001A1970
		public PsionicSifrahTokenTenfoldPathRet()
		{
			this.Description = "draw on the beauty of Ret";
			this.Tile = "Items/ms_ret.bmp";
			this.RenderString = "*";
			this.ColorString = "&w";
			this.DetailColor = 'Y';
		}
	}
}
