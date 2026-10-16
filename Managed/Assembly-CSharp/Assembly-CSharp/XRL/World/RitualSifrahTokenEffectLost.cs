using System;
using XRL.Language;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008D8 RID: 2264
	[Serializable]
	public class RitualSifrahTokenEffectLost : SifrahPrioritizableToken
	{
		// Token: 0x060057AA RID: 22442 RVA: 0x001A9A0C File Offset: 0x001A7C0C
		public RitualSifrahTokenEffectLost()
		{
			this.Description = "accept becoming lost";
			this.Tile = "Items/sw_campfire.bmp";
			this.RenderString = "?";
			this.ColorString = "&w";
			this.DetailColor = 'W';
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x001A9A5B File Offset: 0x001A7C5B
		public RitualSifrahTokenEffectLost(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming lost";
				return;
			}
			Chance = 100;
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x001A9A9B File Offset: 0x001A7C9B
		public override int GetPriority()
		{
			return 2000 - this.Chance * 2;
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x001A9AAB File Offset: 0x001A7CAB
		public override int GetTiebreakerPriority()
		{
			return 4000 - this.Chance * 4;
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x001A9ABB File Offset: 0x001A7CBB
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Lost", this.Chance, null, null, false, false, null);
		}

		// Token: 0x04002C0B RID: 11275
		public int Chance = 100;
	}
}
