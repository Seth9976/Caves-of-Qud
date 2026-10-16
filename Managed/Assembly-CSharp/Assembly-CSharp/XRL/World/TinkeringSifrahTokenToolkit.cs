using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200090E RID: 2318
	[Serializable]
	public class TinkeringSifrahTokenToolkit : SifrahPrioritizableToken
	{
		// Token: 0x06005870 RID: 22640 RVA: 0x001AC416 File Offset: 0x001AA616
		public TinkeringSifrahTokenToolkit()
		{
			this.Description = "apply a toolkit";
			this.Tile = "Items/sw_toolbox.bmp";
			this.RenderString = "\b";
			this.ColorString = "&c";
			this.DetailColor = 'C';
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x001AC452 File Offset: 0x001AA652
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x001AC463 File Offset: 0x001AA663
		public override int GetTiebreakerPriority()
		{
			return int.MaxValue;
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x001AC46C File Offset: 0x001AA66C
		public bool IsAvailable()
		{
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				Toolbox part = gameObject.GetPart<Toolbox>();
				if (part != null && part.TrackAsToolbox)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x001AC4D4 File Offset: 0x001AA6D4
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x001AC4E9 File Offset: 0x001AA6E9
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail("You do not have a toolkit.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x001AC50C File Offset: 0x001AA70C
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				Toolbox part = gameObject.GetPart<Toolbox>();
				if (part != null && part.TrackAsToolbox)
				{
					part.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
					break;
				}
			}
			base.UseToken(Game, Slot, ContextObject);
		}
	}
}
