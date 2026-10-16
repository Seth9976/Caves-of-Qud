using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200090F RID: 2319
	[Serializable]
	public class TinkeringSifrahTokenAdvancedToolkit : SifrahPrioritizableToken
	{
		// Token: 0x06005877 RID: 22647 RVA: 0x001AC5A0 File Offset: 0x001AA7A0
		public TinkeringSifrahTokenAdvancedToolkit()
		{
			this.Description = "apply an advanced toolkit";
			this.Tile = "Items/sw_toolbox_large.bmp";
			this.RenderString = "\b";
			this.ColorString = "&c";
			this.DetailColor = 'C';
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x001AC5DC File Offset: 0x001AA7DC
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x001AC5ED File Offset: 0x001AA7ED
		public override int GetTiebreakerPriority()
		{
			return int.MaxValue;
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x001AC5F4 File Offset: 0x001AA7F4
		public bool IsAvailable()
		{
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				Toolbox part = gameObject.GetPart<Toolbox>();
				if (part != null && part.TrackAsToolbox && part.PoweredDisassembleBonus >= 15 && part.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x001AC690 File Offset: 0x001AA890
		public bool IsUnusable()
		{
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				Toolbox part = gameObject.GetPart<Toolbox>();
				if (part != null && part.TrackAsToolbox && part.PoweredDisassembleBonus >= 15 && !part.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x001AC72C File Offset: 0x001AA92C
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x001AC741 File Offset: 0x001AA941
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				if (this.IsUnusable())
				{
					Popup.ShowFail("You do not have a usable advanced toolkit.", true, true, true);
				}
				else
				{
					Popup.ShowFail("You do not have an advanced toolkit.", true, true, true);
				}
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x001AC77C File Offset: 0x001AA97C
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				Toolbox part = gameObject.GetPart<Toolbox>();
				if (part != null && part.TrackAsToolbox && part.PoweredInspectBonus >= 5 && part.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					part.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
					break;
				}
			}
			base.UseToken(Game, Slot, ContextObject);
		}
	}
}
