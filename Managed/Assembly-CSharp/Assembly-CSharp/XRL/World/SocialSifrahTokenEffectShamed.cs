using System;
using XRL.Language;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000903 RID: 2307
	[Serializable]
	public class SocialSifrahTokenEffectShamed : SifrahPrioritizableToken
	{
		// Token: 0x0600584C RID: 22604 RVA: 0x001ABE6C File Offset: 0x001AA06C
		public SocialSifrahTokenEffectShamed()
		{
			this.Description = "accept becoming {{r|shamed}}";
			this.Tile = "Items/sw_mask_null.bmp";
			this.RenderString = "\u0006";
			this.ColorString = "&b";
			this.DetailColor = 'r';
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x001ABEBB File Offset: 0x001AA0BB
		public SocialSifrahTokenEffectShamed(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of becoming {{r|shamed}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x001ABEFB File Offset: 0x001AA0FB
		public override int GetPriority()
		{
			return 500 - this.Chance;
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x001ABF09 File Offset: 0x001AA109
		public override int GetTiebreakerPriority()
		{
			return 2000 - this.Chance;
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x001ABF17 File Offset: 0x001AA117
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Shamed", this.Chance, "100-500", null, true, false, null);
		}

		// Token: 0x04002C1C RID: 11292
		public int Chance = 100;
	}
}
