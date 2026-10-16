using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008FD RID: 2301
	[Serializable]
	public class SocialSifrahTokenLeverageBeingTrueKin : SifrahToken
	{
		// Token: 0x0600582E RID: 22574 RVA: 0x001ABAE5 File Offset: 0x001A9CE5
		public SocialSifrahTokenLeverageBeingTrueKin()
		{
			this.Description = "leverage being True Kin";
			this.Tile = "Items/ms_happy_face.png";
			this.RenderString = "\u0002";
			this.ColorString = "&Y";
			this.DetailColor = 'B';
		}
	}
}
