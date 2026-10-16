using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000901 RID: 2305
	[Serializable]
	public class SocialSifrahTokenDisplayABarathrumiteToken : SifrahPrioritizableToken
	{
		// Token: 0x06005841 RID: 22593 RVA: 0x001ABCF5 File Offset: 0x001A9EF5
		public SocialSifrahTokenDisplayABarathrumiteToken()
		{
			this.Description = "display a Barathrumite token";
			this.Tile = "Items/sw_token.bmp";
			this.RenderString = "\t";
			this.ColorString = "&y";
			this.DetailColor = 'c';
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x001ABD31 File Offset: 0x001A9F31
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x001ABD42 File Offset: 0x001A9F42
		public override int GetTiebreakerPriority()
		{
			return int.MaxValue;
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x001ABD49 File Offset: 0x001A9F49
		public bool IsAvailable()
		{
			return The.Player.ContainsBlueprint("Barathrumite Token");
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x001ABD5A File Offset: 0x001A9F5A
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x001ABD6F File Offset: 0x001A9F6F
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail("You do not have a Barathrumite token.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}
	}
}
