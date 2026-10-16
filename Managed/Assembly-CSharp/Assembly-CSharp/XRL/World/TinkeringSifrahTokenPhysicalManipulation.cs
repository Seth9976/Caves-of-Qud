using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000908 RID: 2312
	[Serializable]
	public class TinkeringSifrahTokenPhysicalManipulation : SifrahToken
	{
		// Token: 0x06005865 RID: 22629 RVA: 0x001AC266 File Offset: 0x001AA466
		public TinkeringSifrahTokenPhysicalManipulation()
		{
			this.Description = "physical manipulation";
			this.Tile = "Items/sw_flexors.bmp";
			this.RenderString = "\u001d";
			this.ColorString = "&y";
			this.DetailColor = 'K';
		}
	}
}
