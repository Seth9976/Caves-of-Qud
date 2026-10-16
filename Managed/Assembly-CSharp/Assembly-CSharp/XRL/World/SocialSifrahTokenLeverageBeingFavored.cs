using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008FA RID: 2298
	[Serializable]
	public class SocialSifrahTokenLeverageBeingFavored : SifrahToken
	{
		// Token: 0x0600581E RID: 22558 RVA: 0x001AB874 File Offset: 0x001A9A74
		public SocialSifrahTokenLeverageBeingFavored()
		{
			this.Description = "leverage being favored";
			this.Tile = "Items/ms_happy_face.png";
			this.RenderString = "\u0001";
			this.ColorString = "&M";
			this.DetailColor = 'Y';
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x001AB8B0 File Offset: 0x001A9AB0
		public SocialSifrahTokenLeverageBeingFavored(GameObject Representative)
			: this()
		{
			this.UseFaction = Representative.GetPrimaryFaction(false);
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x001AB8C5 File Offset: 0x001A9AC5
		public SocialSifrahTokenLeverageBeingFavored(string Faction)
			: this()
		{
			this.UseFaction = Faction;
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x001AB8D4 File Offset: 0x001A9AD4
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.UseFaction.IsNullOrEmpty())
			{
				return this.Description + " by " + Faction.GetFormattedName(this.UseFaction);
			}
			return this.Description;
		}

		// Token: 0x04002C18 RID: 11288
		private string UseFaction;
	}
}
