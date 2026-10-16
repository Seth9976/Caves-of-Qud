using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008A4 RID: 2212
	[Serializable]
	public class PsionicSifrahTokenApplyAncientLore : SifrahToken
	{
		// Token: 0x060056EE RID: 22254 RVA: 0x001A38B6 File Offset: 0x001A1AB6
		public PsionicSifrahTokenApplyAncientLore()
		{
			this.Description = "apply ancient lore";
			this.Tile = "Items/sw_scroll1.bmp";
			this.RenderString = "ë";
			this.ColorString = "&w";
			this.DetailColor = 'W';
		}
	}
}
