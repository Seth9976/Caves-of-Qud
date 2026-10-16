using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008DB RID: 2267
	[Serializable]
	public class RitualSifrahTokenEffectShaken : SifrahPrioritizableToken
	{
		// Token: 0x060057B9 RID: 22457 RVA: 0x001A9C64 File Offset: 0x001A7E64
		public RitualSifrahTokenEffectShaken()
		{
			this.Description = "accept becoming shaken";
			this.Tile = "Items/sw_tongue_twist.bmp";
			this.RenderString = "\u001d";
			this.ColorString = "&W";
			this.DetailColor = 'G';
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x001A9CB3 File Offset: 0x001A7EB3
		public RitualSifrahTokenEffectShaken(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming shaken";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x001A9CF3 File Offset: 0x001A7EF3
		public override int GetPriority()
		{
			return 1500 - this.Chance * 2;
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x001A9D03 File Offset: 0x001A7F03
		public override int GetTiebreakerPriority()
		{
			return 3000 - this.Chance * 4;
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x001A9D14 File Offset: 0x001A7F14
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Shaken", this.Chance, null, null, true, false, new Shaken("500-1500".RollCached(), "1d20".RollCached()));
		}

		// Token: 0x04002C0E RID: 11278
		public int Chance = 100;
	}
}
