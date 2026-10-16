using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000915 RID: 2325
	[Serializable]
	public class TinkeringSifrahTokenComputePower : SifrahPrioritizableToken
	{
		// Token: 0x060058AD RID: 22701 RVA: 0x001AD5D4 File Offset: 0x001AB7D4
		public TinkeringSifrahTokenComputePower()
		{
			this.Description = "apply compute power";
			this.Tile = "Items/sw_skillsoft.bmp";
			this.RenderString = "ä";
			this.ColorString = "&C";
			this.DetailColor = 'B';
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x001AD624 File Offset: 0x001AB824
		public TinkeringSifrahTokenComputePower(int Amount)
			: this()
		{
			if (Amount < 1)
			{
				Amount = 1;
			}
			this.Amount = Amount;
			this.Description = string.Concat(new string[]
			{
				"apply {{C|",
				Amount.ToString(),
				"}} ",
				(Amount == 1) ? "unit" : "units",
				" of compute power"
			});
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x001AD68B File Offset: 0x001AB88B
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x001AD69C File Offset: 0x001AB89C
		public override int GetTiebreakerPriority()
		{
			if (!this.IsAvailable())
			{
				return -1;
			}
			return int.MaxValue;
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x001AD6AD File Offset: 0x001AB8AD
		public bool IsAvailable()
		{
			return GetAvailableComputePowerEvent.GetFor(The.Player) >= this.Amount;
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x001AD6C4 File Offset: 0x001AB8C4
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x001AD6DC File Offset: 0x001AB8DC
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail(string.Concat(new string[]
				{
					"You do not have ",
					this.Amount.ToString(),
					" ",
					(this.Amount == 1) ? "unit" : "units",
					" of compute power available on the local lattice."
				}), true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x04002C23 RID: 11299
		public int Amount = 1;
	}
}
