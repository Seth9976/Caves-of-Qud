using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008DD RID: 2269
	[Serializable]
	public class RitualSifrahTokenEffectShatterMentalArmor : SifrahPrioritizableToken
	{
		// Token: 0x060057C3 RID: 22467 RVA: 0x001A9E64 File Offset: 0x001A8064
		public RitualSifrahTokenEffectShatterMentalArmor()
		{
			this.Description = "accept becoming {{psionic|psionically cleaved}}";
			this.Tile = "Items/sw_esper.bmp";
			this.RenderString = "û";
			this.ColorString = "&m";
			this.DetailColor = 'R';
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x001A9EB3 File Offset: 0x001A80B3
		public RitualSifrahTokenEffectShatterMentalArmor(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming {{psionic|psionically cleaved}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x001A9EF3 File Offset: 0x001A80F3
		public override int GetPriority()
		{
			return 700 - this.Chance * 3 / 2;
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x001A9F05 File Offset: 0x001A8105
		public override int GetTiebreakerPriority()
		{
			return 1300 - this.Chance * 3;
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x001A9F18 File Offset: 0x001A8118
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("ShatterMentalArmor", this.Chance, null, null, true, false, new ShatterMentalArmor("100-500".RollCached(), null, "1-2".RollCached()));
		}

		// Token: 0x04002C10 RID: 11280
		public int Chance = 100;
	}
}
