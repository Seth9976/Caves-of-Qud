using System;
using XRL.Language;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008D9 RID: 2265
	[Serializable]
	public class RitualSifrahTokenEffectDisoriented : SifrahPrioritizableToken
	{
		// Token: 0x060057AF RID: 22447 RVA: 0x001A9AD4 File Offset: 0x001A7CD4
		public RitualSifrahTokenEffectDisoriented()
		{
			this.Description = "accept becoming disoriented";
			this.Tile = "Mutations/confusion.bmp";
			this.RenderString = "?";
			this.ColorString = "&b";
			this.DetailColor = 'W';
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x001A9B23 File Offset: 0x001A7D23
		public RitualSifrahTokenEffectDisoriented(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming disoriented";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x001A9B63 File Offset: 0x001A7D63
		public override int GetPriority()
		{
			return 1800 - this.Chance * 2;
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x001A9B73 File Offset: 0x001A7D73
		public override int GetTiebreakerPriority()
		{
			return 3600 - this.Chance * 4;
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x001A9B83 File Offset: 0x001A7D83
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Disoriented", this.Chance, null, null, false, false, null);
		}

		// Token: 0x04002C0C RID: 11276
		public int Chance = 100;
	}
}
