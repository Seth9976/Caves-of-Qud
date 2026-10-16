using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008D7 RID: 2263
	[Serializable]
	public class RitualSifrahTokenEffectPoisoned : SifrahPrioritizableToken
	{
		// Token: 0x060057A5 RID: 22437 RVA: 0x001A990C File Offset: 0x001A7B0C
		public RitualSifrahTokenEffectPoisoned()
		{
			this.Description = "accept becoming {{g|ill}}";
			this.Tile = "Items/sw_stinger.bmp";
			this.RenderString = "ô";
			this.ColorString = "&w";
			this.DetailColor = 'G';
		}

		// Token: 0x060057A6 RID: 22438 RVA: 0x001A995B File Offset: 0x001A7B5B
		public RitualSifrahTokenEffectPoisoned(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming {{G|poisoned}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057A7 RID: 22439 RVA: 0x001A999B File Offset: 0x001A7B9B
		public override int GetPriority()
		{
			return 350 - this.Chance;
		}

		// Token: 0x060057A8 RID: 22440 RVA: 0x001A99A9 File Offset: 0x001A7BA9
		public override int GetTiebreakerPriority()
		{
			return 450 - this.Chance;
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x001A99B8 File Offset: 0x001A7BB8
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Poisoned", this.Chance, null, null, true, false, new Poisoned("12-36".RollCached(), "1d2+2".RollCached().ToString() + "d2", 10, null, false, false));
		}

		// Token: 0x04002C0A RID: 11274
		public int Chance = 100;
	}
}
