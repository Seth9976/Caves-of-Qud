using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008D3 RID: 2259
	[Serializable]
	public class RitualSifrahTokenEffectBleeding : SifrahPrioritizableToken
	{
		// Token: 0x06005791 RID: 22417 RVA: 0x001A95BC File Offset: 0x001A77BC
		public RitualSifrahTokenEffectBleeding()
		{
			this.Description = "accept beginning to {{r|bleed}}";
			this.Tile = "Items/ms_droplet.png";
			this.RenderString = "\u00ad";
			this.ColorString = "&r";
			this.DetailColor = 'Y';
		}

		// Token: 0x06005792 RID: 22418 RVA: 0x001A960B File Offset: 0x001A780B
		public RitualSifrahTokenEffectBleeding(int Chance)
			: this()
		{
			if (Chance < 100)
			{
				this.Chance = Math.Max(Chance, 1);
				this.Description = "accept " + Grammar.A(this.Chance, false) + "% chance of beginning to {{r|bleed}}";
				return;
			}
			Chance = 100;
		}

		// Token: 0x06005793 RID: 22419 RVA: 0x001A964B File Offset: 0x001A784B
		public override int GetPriority()
		{
			return 250 - this.Chance;
		}

		// Token: 0x06005794 RID: 22420 RVA: 0x001A9659 File Offset: 0x001A7859
		public override int GetTiebreakerPriority()
		{
			return 400 - this.Chance;
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x001A9668 File Offset: 0x001A7868
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Game.AddEffectChance("CardiacArrest", this.Chance, null, null, true, false, new Bleeding("1d2", 20, null, true, false, false, false));
		}

		// Token: 0x04002C06 RID: 11270
		public int Chance = 100;
	}
}
