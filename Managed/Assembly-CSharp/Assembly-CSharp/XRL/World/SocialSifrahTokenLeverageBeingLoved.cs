using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008FB RID: 2299
	[Serializable]
	public class SocialSifrahTokenLeverageBeingLoved : SifrahToken
	{
		// Token: 0x06005822 RID: 22562 RVA: 0x001AB905 File Offset: 0x001A9B05
		public SocialSifrahTokenLeverageBeingLoved()
		{
			this.Description = "leverage being loved";
			this.Tile = "Items/ms_heart.png";
			this.RenderString = "\u0003";
			this.ColorString = "&R";
			this.DetailColor = 'Y';
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x001AB941 File Offset: 0x001A9B41
		public SocialSifrahTokenLeverageBeingLoved(GameObject Representative)
			: this()
		{
			this.UseFaction = Representative.GetPrimaryFaction(false);
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x001AB956 File Offset: 0x001A9B56
		public SocialSifrahTokenLeverageBeingLoved(string Faction)
			: this()
		{
			this.UseFaction = Faction;
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x001AB965 File Offset: 0x001A9B65
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.UseFaction.IsNullOrEmpty())
			{
				return this.Description + " by " + Faction.GetFormattedName(this.UseFaction);
			}
			return this.Description;
		}

		// Token: 0x04002C19 RID: 11289
		private string UseFaction;
	}
}
