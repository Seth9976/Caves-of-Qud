using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000890 RID: 2192
	public abstract class PsionicSifrah : SifrahGame
	{
		// Token: 0x060056D4 RID: 22228 RVA: 0x001A34AF File Offset: 0x001A16AF
		public PsionicSifrah()
		{
			this.CorrectTokenSound = "Sounds/StatusEffects/sfx_statusEffect_spacetimeWeirdness";
			this.CorrectTokenSoundDelay = 400;
			this.IncorrectTokenSound = "Spark2";
			this.IncorrectTokenSoundDelay = 300;
		}

		// Token: 0x060056D5 RID: 22229 RVA: 0x001A34E3 File Offset: 0x001A16E3
		public override string GetSifrahCategory()
		{
			return PsionicSifrah.CATEGORY;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060056D6 RID: 22230 RVA: 0x001A34EA File Offset: 0x001A16EA
		public static bool AnyEnabled
		{
			get
			{
				return Options.SifrahRealityDistortion || Options.SifrahPsychicCombat;
			}
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x001A34FA File Offset: 0x001A16FA
		public static void AwardInsight()
		{
			if (PsionicSifrah.AnyEnabled)
			{
				SifrahGame.AwardInsight(PsionicSifrah.CATEGORY, "psionics");
			}
		}

		// Token: 0x04002BD0 RID: 11216
		public static readonly string CATEGORY = "Psionic";
	}
}
