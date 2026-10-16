using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008FE RID: 2302
	[Serializable]
	public class SocialSifrahTokenDisplayAFarmersToken : SifrahPrioritizableToken
	{
		// Token: 0x0600582F RID: 22575 RVA: 0x001ABB21 File Offset: 0x001A9D21
		public SocialSifrahTokenDisplayAFarmersToken()
		{
			this.Description = "display a farmer's token";
			this.Tile = "Items/sw_token.bmp";
			this.RenderString = "\t";
			this.ColorString = "&y";
			this.DetailColor = 'g';
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x001ABB5D File Offset: 0x001A9D5D
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x001ABB6E File Offset: 0x001A9D6E
		public override int GetTiebreakerPriority()
		{
			return int.MaxValue;
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x001ABB75 File Offset: 0x001A9D75
		public bool IsAvailable()
		{
			return The.Player.ContainsBlueprint("Farmers Token");
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x001ABB86 File Offset: 0x001A9D86
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x001ABB9B File Offset: 0x001A9D9B
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail("You do not have a farmer's token.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}
	}
}
