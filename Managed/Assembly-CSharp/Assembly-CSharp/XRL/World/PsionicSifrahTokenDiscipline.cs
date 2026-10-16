using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000898 RID: 2200
	[Serializable]
	public class PsionicSifrahTokenDiscipline : SifrahToken
	{
		// Token: 0x060056E0 RID: 22240 RVA: 0x001A3608 File Offset: 0x001A1808
		public PsionicSifrahTokenDiscipline()
		{
			this.Description = "draw on reserves of self-discipline";
			this.Tile = "Items/sw_gem.bmp";
			this.RenderString = "è";
			this.ColorString = "&K";
			this.DetailColor = 'Y';
		}
	}
}
