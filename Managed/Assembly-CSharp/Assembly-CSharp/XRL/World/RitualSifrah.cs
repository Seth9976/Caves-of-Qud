using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008BB RID: 2235
	public abstract class RitualSifrah : SifrahGame
	{
		// Token: 0x06005746 RID: 22342 RVA: 0x001A8352 File Offset: 0x001A6552
		public RitualSifrah()
		{
			this.CorrectTokenSound = "Sounds/Interact/sfx_artifact_examination_success_total";
			this.CorrectTokenSoundDelay = 500;
			this.IncorrectTokenSound = "Sounds/StatusEffects/sfx_statusEffect_frozen";
			this.IncorrectTokenSoundDelay = 200;
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x001A8386 File Offset: 0x001A6586
		public override string GetSifrahCategory()
		{
			return RitualSifrah.CATEGORY;
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06005748 RID: 22344 RVA: 0x001A838D File Offset: 0x001A658D
		public static bool AnyEnabled
		{
			get
			{
				return Options.SifrahItemNaming;
			}
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x001A8394 File Offset: 0x001A6594
		public static void AwardInsight()
		{
			if (RitualSifrah.AnyEnabled)
			{
				SifrahGame.AwardInsight(RitualSifrah.CATEGORY, "ritual");
			}
		}

		// Token: 0x04002C01 RID: 11265
		public static readonly string CATEGORY = "Ritual";
	}
}
