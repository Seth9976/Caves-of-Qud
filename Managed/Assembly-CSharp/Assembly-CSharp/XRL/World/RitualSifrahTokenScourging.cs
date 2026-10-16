using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008C7 RID: 2247
	[Serializable]
	public class RitualSifrahTokenScourging : SifrahPrioritizableToken
	{
		// Token: 0x06005758 RID: 22360 RVA: 0x001A84D4 File Offset: 0x001A66D4
		public RitualSifrahTokenScourging()
		{
			this.Description = "scourge myself with a leather whip";
			this.Tile = "Items/sw_whip_1.bmp";
			this.RenderString = "\u00a8";
			this.ColorString = "&w";
			this.DetailColor = 'K';
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x001A8510 File Offset: 0x001A6710
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return The.Player.Stat("Hitpoints", 0);
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x001A852C File Offset: 0x001A672C
		public override int GetTiebreakerPriority()
		{
			return 0;
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x001A852F File Offset: 0x001A672F
		public bool IsAvailable()
		{
			return The.Player.HasObjectInInventory(RitualSifrahTokenScourging.BLUEPRINT, 1);
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x001A8541 File Offset: 0x001A6741
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail("You do not have a leather whip.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x001A8563 File Offset: 0x001A6763
		public override int GetPowerup(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (Slot.CurrentMove == Slot.Token)
			{
				return 1;
			}
			return base.GetPowerup(Game, Slot, ContextObject);
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x001A8580 File Offset: 0x001A6780
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			GameObject gameObject = The.Player.FindObjectInInventory(RitualSifrahTokenScourging.BLUEPRINT);
			if (gameObject != null)
			{
				gameObject.ModIntProperty("SifrahActions", 1, false);
				gameObject.SetLongProperty("LastSifrahActionTurn", The.CurrentTurn);
			}
			SoundManager.PlaySound("Sounds/Damage/sfx_damage_stone", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			The.Player.TakeDamage(Stat.Random(1, 4), "from scourging yourself.", null, "You scourged yourself to death.", null, The.Player, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x04002C02 RID: 11266
		public static readonly string BLUEPRINT = "Leather Whip";
	}
}
