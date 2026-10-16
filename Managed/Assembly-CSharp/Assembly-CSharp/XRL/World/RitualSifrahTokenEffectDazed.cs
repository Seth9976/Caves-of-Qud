using System;
using XRL.Language;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008DE RID: 2270
	[Serializable]
	public class RitualSifrahTokenEffectDazed : SifrahPrioritizableToken
	{
		// Token: 0x060057C8 RID: 22472 RVA: 0x001A9F54 File Offset: 0x001A8154
		public RitualSifrahTokenEffectDazed()
		{
			this.Description = "accept becoming {{C|dazed}}";
			this.Tile = "Items/sw_crunch1.bmp";
			this.RenderString = "*";
			this.ColorString = "&C";
			this.DetailColor = 'W';
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x001A9FA3 File Offset: 0x001A81A3
		public RitualSifrahTokenEffectDazed(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming {{C|dazed}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x001A9FE3 File Offset: 0x001A81E3
		public override int GetPriority()
		{
			return 4000 - this.Chance * 4;
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x001A9FF3 File Offset: 0x001A81F3
		public override int GetTiebreakerPriority()
		{
			return 8000 - this.Chance * 8;
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x001AA003 File Offset: 0x001A8203
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Dazed", this.Chance, null, null, true, false, null);
		}

		// Token: 0x04002C11 RID: 11281
		public int Chance = 100;
	}
}
