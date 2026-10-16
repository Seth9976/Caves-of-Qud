using System;
using XRL.Language;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008DA RID: 2266
	[Serializable]
	public class RitualSifrahTokenEffectConfused : SifrahPrioritizableToken
	{
		// Token: 0x060057B4 RID: 22452 RVA: 0x001A9B9C File Offset: 0x001A7D9C
		public RitualSifrahTokenEffectConfused()
		{
			this.Description = "accept becoming disoriented";
			this.Tile = "Mutations/confusion.bmp";
			this.RenderString = "?";
			this.ColorString = "&G";
			this.DetailColor = 'R';
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x001A9BEB File Offset: 0x001A7DEB
		public RitualSifrahTokenEffectConfused(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming confused";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x001A9C2B File Offset: 0x001A7E2B
		public override int GetPriority()
		{
			return 1800 - this.Chance * 2;
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x001A9C3B File Offset: 0x001A7E3B
		public override int GetTiebreakerPriority()
		{
			return 3600 - this.Chance * 4;
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x001A9C4B File Offset: 0x001A7E4B
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Confused", this.Chance, null, null, false, false, null);
		}

		// Token: 0x04002C0D RID: 11277
		public int Chance = 100;
	}
}
