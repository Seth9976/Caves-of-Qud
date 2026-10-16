using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200143E RID: 5182
	[Serializable]
	public class FungicideGeneration : GasGeneration
	{
		// Token: 0x0600CA5C RID: 51804 RVA: 0x0049A560 File Offset: 0x00498760
		public FungicideGeneration()
			: base("FungicideGas")
		{
		}

		// Token: 0x0600CA5D RID: 51805 RVA: 0x0049A56D File Offset: 0x0049876D
		public override int GetReleaseDuration(int Level)
		{
			return (Level + 2) * 2;
		}

		// Token: 0x0600CA5E RID: 51806 RVA: 0x0049A574 File Offset: 0x00498774
		public override int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA5F RID: 51807 RVA: 0x0049A578 File Offset: 0x00498778
		public override string GetReleaseAbilityName()
		{
			return "Release Fungicide";
		}
	}
}
