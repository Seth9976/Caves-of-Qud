using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000906 RID: 2310
	public abstract class TinkeringSifrah : SifrahGame
	{
		// Token: 0x0600585F RID: 22623 RVA: 0x001AC1A6 File Offset: 0x001AA3A6
		public TinkeringSifrah()
		{
			this.CorrectTokenSound = "compartment_close";
			this.CorrectTokenSoundDelay = 500;
			this.IncorrectTokenSound = "Chop2";
			this.IncorrectTokenSoundDelay = 200;
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x001AC1DA File Offset: 0x001AA3DA
		public override string GetSifrahCategory()
		{
			return TinkeringSifrah.CATEGORY;
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06005861 RID: 22625 RVA: 0x001AC1E1 File Offset: 0x001AA3E1
		public static bool AnyEnabled
		{
			get
			{
				return Options.SifrahExamine || Options.SifrahRepair || Options.SifrahReverseEngineer || Options.SifrahDisarming || Options.SifrahHacking;
			}
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x001AC206 File Offset: 0x001AA406
		public static void AwardInsight()
		{
			if (TinkeringSifrah.AnyEnabled)
			{
				SifrahGame.AwardInsight(TinkeringSifrah.CATEGORY, "tinkering");
			}
		}

		// Token: 0x04002C1D RID: 11293
		public static readonly string CATEGORY = "Tinkering";
	}
}
