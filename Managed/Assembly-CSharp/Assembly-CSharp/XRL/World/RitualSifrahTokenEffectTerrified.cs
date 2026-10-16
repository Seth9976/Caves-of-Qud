using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008DC RID: 2268
	[Serializable]
	public class RitualSifrahTokenEffectTerrified : SifrahPrioritizableToken
	{
		// Token: 0x060057BE RID: 22462 RVA: 0x001A9D50 File Offset: 0x001A7F50
		public RitualSifrahTokenEffectTerrified()
		{
			this.Description = "accept becoming {{W|terrified}}";
			this.Tile = "Items/sw_chimera.bmp";
			this.RenderString = "\u0013";
			this.ColorString = "&g";
			this.DetailColor = 'W';
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x001A9D9F File Offset: 0x001A7F9F
		public RitualSifrahTokenEffectTerrified(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming {{W|terrified}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x001A9DDF File Offset: 0x001A7FDF
		public override int GetPriority()
		{
			return 1600 - this.Chance * 2;
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x001A9DEF File Offset: 0x001A7FEF
		public override int GetTiebreakerPriority()
		{
			return 3200 - this.Chance * 4;
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x001A9E00 File Offset: 0x001A8000
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Terrified terrified;
			if (ContextObject.IsCreature)
			{
				terrified = new Terrified("20-40".RollCached(), ContextObject, false, false);
			}
			else
			{
				terrified = new Terrified("20-40".RollCached(), ContextObject.GetCurrentCell() ?? The.Player.GetCurrentCell());
			}
			Game.AddEffectChance("Terrified", this.Chance, null, null, false, false, terrified);
		}

		// Token: 0x04002C0F RID: 11279
		public int Chance = 100;
	}
}
