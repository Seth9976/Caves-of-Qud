using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000902 RID: 2306
	[Serializable]
	public class SocialSifrahTokenEffectLovesick : SifrahPrioritizableToken
	{
		// Token: 0x06005847 RID: 22599 RVA: 0x001ABD94 File Offset: 0x001A9F94
		public SocialSifrahTokenEffectLovesick()
		{
			this.Description = "accept becoming {{lovesickness|lovesick}}";
			this.Tile = "Items/sw_heart.bmp";
			this.RenderString = "\u0003";
			this.ColorString = "&R";
			this.DetailColor = 'Y';
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x001ABDE3 File Offset: 0x001A9FE3
		public SocialSifrahTokenEffectLovesick(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(Chance, false) + "% chance of becoming {{lovesickness|lovesick}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x001ABE1E File Offset: 0x001AA01E
		public override int GetPriority()
		{
			return 300 - this.Chance;
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x001ABE2C File Offset: 0x001AA02C
		public override int GetTiebreakerPriority()
		{
			return 1000 - this.Chance;
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x001ABE3C File Offset: 0x001AA03C
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("Lovesick", this.Chance, "3000-3600", null, false, false, new Lovesick(1, ContextObject));
		}

		// Token: 0x04002C1B RID: 11291
		public int Chance = 100;
	}
}
