using System;
using XRL.Language;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008D2 RID: 2258
	[Serializable]
	public class RitualSifrahTokenEffectCardiacArrest : SifrahPrioritizableToken
	{
		// Token: 0x0600578C RID: 22412 RVA: 0x001A94F8 File Offset: 0x001A76F8
		public RitualSifrahTokenEffectCardiacArrest()
		{
			this.Description = "accept going into {{W|cardiac arrest}}";
			this.Tile = "Items/ms_broken_heart.png";
			this.RenderString = "\u0003";
			this.ColorString = "&K";
			this.DetailColor = 'r';
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x001A9547 File Offset: 0x001A7747
		public RitualSifrahTokenEffectCardiacArrest(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of going into {{W|cardiac arrest}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x001A9587 File Offset: 0x001A7787
		public override int GetPriority()
		{
			return 150 - this.Chance;
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x001A9595 File Offset: 0x001A7795
		public override int GetTiebreakerPriority()
		{
			return 300 - this.Chance;
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x001A95A3 File Offset: 0x001A77A3
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("CardiacArrest", this.Chance, null, null, true, false, null);
		}

		// Token: 0x04002C05 RID: 11269
		public int Chance = 100;
	}
}
