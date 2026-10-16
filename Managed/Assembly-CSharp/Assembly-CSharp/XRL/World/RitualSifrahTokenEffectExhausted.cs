using System;
using XRL.Language;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008D4 RID: 2260
	[Serializable]
	public class RitualSifrahTokenEffectExhausted : SifrahPrioritizableToken
	{
		// Token: 0x06005796 RID: 22422 RVA: 0x001A969C File Offset: 0x001A789C
		public RitualSifrahTokenEffectExhausted()
		{
			this.Description = "accept becoming {{K|exhausted}}";
			this.Tile = "Items/sw_beautiful.bmp";
			this.RenderString = "\u001f";
			this.ColorString = "&K";
			this.DetailColor = 'b';
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x001A96EB File Offset: 0x001A78EB
		public RitualSifrahTokenEffectExhausted(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming {{K|exhausted}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x001A972B File Offset: 0x001A792B
		public override int GetPriority()
		{
			return 600 - this.Chance;
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x001A9739 File Offset: 0x001A7939
		public override int GetTiebreakerPriority()
		{
			return 1000 - this.Chance;
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x001A9747 File Offset: 0x001A7947
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Exhausted", this.Chance, "10-40", null, false, false, null);
		}

		// Token: 0x04002C07 RID: 11271
		public int Chance = 100;
	}
}
