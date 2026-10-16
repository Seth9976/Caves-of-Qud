using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200143C RID: 5180
	[Serializable]
	public class PoisonGasGeneration : GasGeneration
	{
		// Token: 0x0600CA54 RID: 51796 RVA: 0x0049A524 File Offset: 0x00498724
		public PoisonGasGeneration()
			: base("PoisonGas")
		{
		}

		// Token: 0x0600CA55 RID: 51797 RVA: 0x0049A531 File Offset: 0x00498731
		public override int GetReleaseDuration(int Level)
		{
			return Level + 2;
		}

		// Token: 0x0600CA56 RID: 51798 RVA: 0x0049A536 File Offset: 0x00498736
		public override int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA57 RID: 51799 RVA: 0x0049A53A File Offset: 0x0049873A
		public override string GetReleaseAbilityName()
		{
			return "Release Poison Gas";
		}
	}
}
