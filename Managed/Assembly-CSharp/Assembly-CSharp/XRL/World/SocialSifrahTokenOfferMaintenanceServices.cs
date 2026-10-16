using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008EE RID: 2286
	[Serializable]
	public class SocialSifrahTokenOfferMaintenanceServices : SifrahToken
	{
		// Token: 0x060057E1 RID: 22497 RVA: 0x001AA4BA File Offset: 0x001A86BA
		public SocialSifrahTokenOfferMaintenanceServices()
		{
			this.Description = "offer maintenance services";
			this.Tile = "Items/sw_toolbox.bmp";
			this.RenderString = "÷";
			this.ColorString = "&c";
			this.DetailColor = 'C';
		}
	}
}
