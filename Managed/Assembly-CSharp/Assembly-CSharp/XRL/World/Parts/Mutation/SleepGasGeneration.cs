using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001439 RID: 5177
	[Serializable]
	public class SleepGasGeneration : GasGeneration
	{
		// Token: 0x0600CA48 RID: 51784 RVA: 0x0049A498 File Offset: 0x00498698
		public SleepGasGeneration()
			: base("SleepGas")
		{
		}

		// Token: 0x0600CA49 RID: 51785 RVA: 0x0049A4A5 File Offset: 0x004986A5
		public override int GetReleaseDuration(int Level)
		{
			return Level + 2;
		}

		// Token: 0x0600CA4A RID: 51786 RVA: 0x0049A4AA File Offset: 0x004986AA
		public override int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA4B RID: 51787 RVA: 0x0049A4AE File Offset: 0x004986AE
		public override string GetReleaseAbilityName()
		{
			return "Release Sleep Gas";
		}
	}
}
