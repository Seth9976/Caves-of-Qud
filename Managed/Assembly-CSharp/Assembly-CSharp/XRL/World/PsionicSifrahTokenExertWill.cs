using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000893 RID: 2195
	[Serializable]
	public class PsionicSifrahTokenExertWill : SifrahToken
	{
		// Token: 0x060056DB RID: 22235 RVA: 0x001A3544 File Offset: 0x001A1744
		public PsionicSifrahTokenExertWill()
		{
			this.Description = "exert will";
			this.Tile = "Items/ms_ego.bmp";
			this.RenderString = "\u0004";
			this.ColorString = "&M";
			this.DetailColor = 'Y';
		}
	}
}
