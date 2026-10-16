using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008A6 RID: 2214
	[Serializable]
	public class PsionicSifrahTokenEffectNosebleed : SifrahPrioritizableToken
	{
		// Token: 0x060056F0 RID: 22256 RVA: 0x001A38FC File Offset: 0x001A1AFC
		public PsionicSifrahTokenEffectNosebleed()
		{
			this.Description = "accept a {{r|nosebleed}} starting";
			this.Tile = "Items/ms_droplet.png";
			this.RenderString = "\u00ad";
			this.ColorString = "&r";
			this.DetailColor = 'W';
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x001A394B File Offset: 0x001A1B4B
		public PsionicSifrahTokenEffectNosebleed(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of a {{r|nosebleed}} starting";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x001A398B File Offset: 0x001A1B8B
		public override int GetPriority()
		{
			return 200 - this.Chance;
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x001A3999 File Offset: 0x001A1B99
		public override int GetTiebreakerPriority()
		{
			return 350 - this.Chance;
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x001A39A8 File Offset: 0x001A1BA8
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Nosebleed", this.Chance, null, null, true, false, new Nosebleed("1d2", 20, null, true, false, false));
		}

		// Token: 0x04002BD1 RID: 11217
		public int Chance = 100;
	}
}
