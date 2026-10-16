using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008EB RID: 2283
	[Serializable]
	public class SocialSifrahTokenBoastOfAccomplishments : SifrahToken
	{
		// Token: 0x060057DE RID: 22494 RVA: 0x001AA3EE File Offset: 0x001A85EE
		public SocialSifrahTokenBoastOfAccomplishments()
		{
			this.Description = "boast of my accomplishments";
			this.Tile = "Items/sw_armlocks.bmp";
			this.RenderString = "\u0013";
			this.ColorString = "&M";
			this.DetailColor = 'm';
		}
	}
}
