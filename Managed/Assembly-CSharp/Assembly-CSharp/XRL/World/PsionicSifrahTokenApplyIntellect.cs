using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000894 RID: 2196
	[Serializable]
	public class PsionicSifrahTokenApplyIntellect : SifrahToken
	{
		// Token: 0x060056DC RID: 22236 RVA: 0x001A3580 File Offset: 0x001A1780
		public PsionicSifrahTokenApplyIntellect()
		{
			this.Description = "apply intellect";
			this.Tile = "Items/ms_intelligence.bmp";
			this.RenderString = "§";
			this.ColorString = "&B";
			this.DetailColor = 'Y';
		}
	}
}
