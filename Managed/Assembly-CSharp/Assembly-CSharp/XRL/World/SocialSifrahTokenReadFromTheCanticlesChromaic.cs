using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008FC RID: 2300
	[Serializable]
	public class SocialSifrahTokenReadFromTheCanticlesChromaic : SifrahPrioritizableToken
	{
		// Token: 0x06005826 RID: 22566 RVA: 0x001AB996 File Offset: 0x001A9B96
		public SocialSifrahTokenReadFromTheCanticlesChromaic()
		{
			this.Description = "read from the Canticles Chromaic";
			this.Tile = "Items/sw_book2.bmp";
			this.RenderString = "ë";
			this.ColorString = "&c";
			this.DetailColor = 'W';
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x001AB9D2 File Offset: 0x001A9BD2
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x001AB9E3 File Offset: 0x001A9BE3
		public override int GetTiebreakerPriority()
		{
			return int.MaxValue;
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x001AB9EA File Offset: 0x001A9BEA
		public bool IsAvailable()
		{
			return The.Player.HasObjectInInventory(SocialSifrahTokenReadFromTheCanticlesChromaic.BLUEPRINT, 1);
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x001AB9FC File Offset: 0x001A9BFC
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x001ABA11 File Offset: 0x001A9C11
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail("You do not have a copy of the Canticles Chromaic.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x001ABA34 File Offset: 0x001A9C34
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			GameObject gameObject = The.Player.FindObjectInInventory(SocialSifrahTokenReadFromTheCanticlesChromaic.BLUEPRINT);
			if (gameObject != null)
			{
				gameObject.ModIntProperty("SifrahActions", 1, false);
				gameObject.SetLongProperty("LastSifrahActionTurn", The.CurrentTurn);
			}
			string text = BookUI.Books["Preacher1"].Pages.GetRandomElement<BookPage>(null).FullText.Replace("\n", " ").Replace("  ", " ").Trim();
			The.Player.ParticleText("{{W|'" + text + "'}}", false);
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x04002C1A RID: 11290
		public static readonly string BLUEPRINT = "Canticles3";
	}
}
