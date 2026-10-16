using System;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000911 RID: 2321
	[Serializable]
	public class TinkeringSifrahTokenBit : SifrahPrioritizableToken
	{
		// Token: 0x0600588A RID: 22666 RVA: 0x001ACA6F File Offset: 0x001AAC6F
		public TinkeringSifrahTokenBit()
		{
			this.Description = "use bit";
			this.RenderString = "?";
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x001ACA90 File Offset: 0x001AAC90
		public TinkeringSifrahTokenBit(BitType bitType)
			: this()
		{
			this.Description = "use " + bitType.Description;
			this.RenderString = BitType.TranslateBit(bitType.Color);
			this.ColorString = "&" + bitType.Color.ToString();
			this.Color = bitType.Color;
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x001ACAF4 File Offset: 0x001AACF4
		public override int GetPriority()
		{
			BitType bitType = BitType.BitMap[this.Color];
			int num = BitLocker.GetBitCount(The.Player, this.Color);
			if (num > 0 && bitType.Level > 0)
			{
				num = Math.Max(num * (10 - bitType.Level) / 10, 1);
			}
			return num;
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x001ACB46 File Offset: 0x001AAD46
		public override int GetTiebreakerPriority()
		{
			return 30 - BitType.GetBitSortOrder(this.Color);
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x001ACB56 File Offset: 0x001AAD56
		public int GetNumberAvailable(int Chosen = 0)
		{
			return BitLocker.GetBitCount(The.Player, this.Color) - Chosen;
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x001ACB6C File Offset: 0x001AAD6C
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return this.Description + " [have {{C|" + this.GetNumberAvailable(Game.GetTimesChosen(this, Slot)).ToString() + "}}]";
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x001ACBA4 File Offset: 0x001AADA4
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return this.GetNumberAvailable(Game.GetTimesChosen(this, Slot)) <= 0 || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x001ACBC4 File Offset: 0x001AADC4
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			int timesChosen = Game.GetTimesChosen(this, Slot);
			if (this.GetNumberAvailable(timesChosen) <= 0)
			{
				Popup.ShowFail("You do not have any " + ((timesChosen > 0) ? "more " : "") + BitType.BitMap[this.Color].Description + ".", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x001ACC2B File Offset: 0x001AAE2B
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			BitLocker.UseBits(The.Player, this.Color, 1);
		}

		// Token: 0x04002C20 RID: 11296
		public char Color;
	}
}
