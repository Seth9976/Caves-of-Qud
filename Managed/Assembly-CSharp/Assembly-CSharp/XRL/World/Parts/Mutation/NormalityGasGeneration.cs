using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001440 RID: 5184
	[Serializable]
	public class NormalityGasGeneration : GasGeneration
	{
		// Token: 0x0600CA64 RID: 51812 RVA: 0x0049A5A0 File Offset: 0x004987A0
		public NormalityGasGeneration()
			: base("NormalityGas")
		{
		}

		// Token: 0x0600CA65 RID: 51813 RVA: 0x0049A5AD File Offset: 0x004987AD
		public override int GetReleaseDuration(int Level)
		{
			return (Level + 2) * 3 / 2;
		}

		// Token: 0x0600CA66 RID: 51814 RVA: 0x0049A5B6 File Offset: 0x004987B6
		public override int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA67 RID: 51815 RVA: 0x0049A5BA File Offset: 0x004987BA
		public override string GetReleaseAbilityName()
		{
			return "Release Normality Gas";
		}
	}
}
