using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008D6 RID: 2262
	[Serializable]
	public class RitualSifrahTokenEffectIll : SifrahPrioritizableToken
	{
		// Token: 0x060057A0 RID: 22432 RVA: 0x001A982C File Offset: 0x001A7A2C
		public RitualSifrahTokenEffectIll()
		{
			this.Description = "accept becoming {{g|ill}}";
			this.Tile = "Items/sw_cherubic.bmp";
			this.RenderString = "á";
			this.ColorString = "&g";
			this.DetailColor = 'G';
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x001A987B File Offset: 0x001A7A7B
		public RitualSifrahTokenEffectIll(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming {{g|ill}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057A2 RID: 22434 RVA: 0x001A98BB File Offset: 0x001A7ABB
		public override int GetPriority()
		{
			return 800 - this.Chance;
		}

		// Token: 0x060057A3 RID: 22435 RVA: 0x001A98C9 File Offset: 0x001A7AC9
		public override int GetTiebreakerPriority()
		{
			return 1000 - this.Chance;
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x001A98D8 File Offset: 0x001A7AD8
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Ill", this.Chance, "1000-2000", null, false, false, new Ill(1, 1, "You feel nauseous.", false, false));
		}

		// Token: 0x04002C09 RID: 11273
		public int Chance = 100;
	}
}
