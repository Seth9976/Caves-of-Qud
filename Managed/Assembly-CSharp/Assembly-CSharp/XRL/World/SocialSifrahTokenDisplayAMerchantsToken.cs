using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008FF RID: 2303
	[Serializable]
	public class SocialSifrahTokenDisplayAMerchantsToken : SifrahPrioritizableToken
	{
		// Token: 0x06005835 RID: 22581 RVA: 0x001ABBBD File Offset: 0x001A9DBD
		public SocialSifrahTokenDisplayAMerchantsToken()
		{
			this.Description = "display a merchant's token";
			this.Tile = "Items/sw_token.bmp";
			this.RenderString = "\t";
			this.ColorString = "&y";
			this.DetailColor = 'B';
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x001ABBF9 File Offset: 0x001A9DF9
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x001ABC0A File Offset: 0x001A9E0A
		public override int GetTiebreakerPriority()
		{
			return int.MaxValue;
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x001ABC11 File Offset: 0x001A9E11
		public bool IsAvailable()
		{
			return The.Player.ContainsBlueprint("Merchant's Token");
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x001ABC22 File Offset: 0x001A9E22
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x001ABC37 File Offset: 0x001A9E37
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail("You do not have a merchant's token.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}
	}
}
