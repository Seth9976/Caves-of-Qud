using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F3 RID: 2291
	[Serializable]
	public class SocialSifrahTokenHookah : SifrahPrioritizableToken
	{
		// Token: 0x060057E9 RID: 22505 RVA: 0x001AA61D File Offset: 0x001A881D
		public SocialSifrahTokenHookah()
		{
			this.Description = "offer a puff on a hookah";
			this.Tile = "Items/sw_hookah.bmp";
			this.RenderString = "ë";
			this.ColorString = "&R";
			this.DetailColor = 'w';
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x001AA659 File Offset: 0x001A8859
		public override int GetPriority()
		{
			if (!this.IsPotentiallyAvailable())
			{
				return 0;
			}
			if (!this.IsAvailable())
			{
				return 1879048185;
			}
			return int.MaxValue;
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x001AA678 File Offset: 0x001A8878
		public override int GetTiebreakerPriority()
		{
			return int.MaxValue;
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x001AA67F File Offset: 0x001A887F
		public bool IsPotentiallyAvailable()
		{
			return The.Player.HasObjectInInventory(SocialSifrahTokenHookah.BLUEPRINT, 1);
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x001AA694 File Offset: 0x001A8894
		public bool IsAvailable()
		{
			GameObject gameObject = The.Player.FindObjectInInventory(SocialSifrahTokenHookah.BLUEPRINT);
			if (gameObject == null)
			{
				return false;
			}
			Hookah part = gameObject.GetPart<Hookah>();
			return part != null && part.CanPuff();
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x001AA6C8 File Offset: 0x001A88C8
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x001AA6DD File Offset: 0x001A88DD
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsPotentiallyAvailable())
			{
				Popup.ShowFail("You do not have a hookah.", true, true, true);
				return false;
			}
			if (!this.IsAvailable())
			{
				Popup.ShowFail("Your hookah is not filled with water.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x001AA718 File Offset: 0x001A8918
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			GameObject gameObject = The.Player.FindObjectInInventory(SocialSifrahTokenHookah.BLUEPRINT);
			if (gameObject != null)
			{
				gameObject.ModIntProperty("SifrahActions", 1, false);
				gameObject.SetLongProperty("LastSifrahActionTurn", The.CurrentTurn);
			}
			for (int i = 2; i < 5; i++)
			{
				ParticleFX.Smoke(this.X, this.Y, 150, 180);
			}
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x04002C13 RID: 11283
		public static readonly string BLUEPRINT = "Hookah";
	}
}
