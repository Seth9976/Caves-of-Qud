using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000895 RID: 2197
	[Serializable]
	public class PsionicSifrahTokenCalmMind : SifrahToken
	{
		// Token: 0x060056DD RID: 22237 RVA: 0x001A35BC File Offset: 0x001A17BC
		public PsionicSifrahTokenCalmMind()
		{
			this.Description = "calm mind";
			this.Tile = "Items/ms_willpower.bmp";
			this.RenderString = "÷";
			this.ColorString = "&Y";
			this.DetailColor = 'Y';
		}
	}
}
