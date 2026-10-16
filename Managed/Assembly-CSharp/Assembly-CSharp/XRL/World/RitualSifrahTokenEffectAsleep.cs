using System;
using XRL.Language;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008D5 RID: 2261
	[Serializable]
	public class RitualSifrahTokenEffectAsleep : SifrahPrioritizableToken
	{
		// Token: 0x0600579B RID: 22427 RVA: 0x001A9764 File Offset: 0x001A7964
		public RitualSifrahTokenEffectAsleep()
		{
			this.Description = "accept falling {{c|asleep}}";
			this.Tile = "Items/sw_bed.bmp";
			this.RenderString = "\u001c";
			this.ColorString = "&w";
			this.DetailColor = 'y';
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x001A97B3 File Offset: 0x001A79B3
		public RitualSifrahTokenEffectAsleep(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of falling {{c|asleep}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x001A97F3 File Offset: 0x001A79F3
		public override int GetPriority()
		{
			return 1000 - this.Chance;
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x001A9801 File Offset: 0x001A7A01
		public override int GetTiebreakerPriority()
		{
			return 2000 - this.Chance;
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x001A980F File Offset: 0x001A7A0F
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Asleep", this.Chance, "2000-4000", null, false, false, null);
		}

		// Token: 0x04002C08 RID: 11272
		public int Chance = 100;
	}
}
