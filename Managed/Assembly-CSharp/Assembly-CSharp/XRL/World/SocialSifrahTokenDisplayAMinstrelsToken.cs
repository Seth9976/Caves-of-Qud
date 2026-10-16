using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000900 RID: 2304
	[Serializable]
	public class SocialSifrahTokenDisplayAMinstrelsToken : SifrahPrioritizableToken
	{
		// Token: 0x0600583B RID: 22587 RVA: 0x001ABC59 File Offset: 0x001A9E59
		public SocialSifrahTokenDisplayAMinstrelsToken()
		{
			this.Description = "display a minstrel's token";
			this.Tile = "Items/sw_token.bmp";
			this.RenderString = "\t";
			this.ColorString = "&y";
			this.DetailColor = 'm';
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x001ABC95 File Offset: 0x001A9E95
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x001ABCA6 File Offset: 0x001A9EA6
		public override int GetTiebreakerPriority()
		{
			return int.MaxValue;
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x001ABCAD File Offset: 0x001A9EAD
		public bool IsAvailable()
		{
			return The.Player.ContainsBlueprint("Minstrel's Token");
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x001ABCBE File Offset: 0x001A9EBE
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x001ABCD3 File Offset: 0x001A9ED3
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail("You do not have a minstrel's token.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}
	}
}
