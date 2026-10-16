using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200143F RID: 5183
	[Serializable]
	public class ConfusionGasGeneration : GasGeneration
	{
		// Token: 0x0600CA60 RID: 51808 RVA: 0x0049A57F File Offset: 0x0049877F
		public ConfusionGasGeneration()
			: base("ConfusionGas")
		{
		}

		// Token: 0x0600CA61 RID: 51809 RVA: 0x0049A58C File Offset: 0x0049878C
		public override int GetReleaseDuration(int Level)
		{
			return (Level + 2) * 3 / 2;
		}

		// Token: 0x0600CA62 RID: 51810 RVA: 0x0049A595 File Offset: 0x00498795
		public override int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA63 RID: 51811 RVA: 0x0049A599 File Offset: 0x00498799
		public override string GetReleaseAbilityName()
		{
			return "Release Confusion Gas";
		}
	}
}
