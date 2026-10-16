using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E4 RID: 2276
	[Serializable]
	public class SocialSifrahTokenPayACompliment : SifrahToken
	{
		// Token: 0x060057D7 RID: 22487 RVA: 0x001AA21C File Offset: 0x001A841C
		public SocialSifrahTokenPayACompliment()
		{
			this.Description = "pay a compliment";
			this.Tile = "Items/ms_face_heart.png";
			this.RenderString = "\u0003";
			this.ColorString = "&M";
			this.DetailColor = 'W';
		}
	}
}
