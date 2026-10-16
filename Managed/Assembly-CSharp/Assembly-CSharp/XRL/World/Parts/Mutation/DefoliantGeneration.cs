using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200143D RID: 5181
	[Serializable]
	public class DefoliantGeneration : GasGeneration
	{
		// Token: 0x0600CA58 RID: 51800 RVA: 0x0049A541 File Offset: 0x00498741
		public DefoliantGeneration()
			: base("DefoliantGas")
		{
		}

		// Token: 0x0600CA59 RID: 51801 RVA: 0x0049A54E File Offset: 0x0049874E
		public override int GetReleaseDuration(int Level)
		{
			return (Level + 2) * 2;
		}

		// Token: 0x0600CA5A RID: 51802 RVA: 0x0049A555 File Offset: 0x00498755
		public override int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA5B RID: 51803 RVA: 0x0049A559 File Offset: 0x00498759
		public override string GetReleaseAbilityName()
		{
			return "Release Defoliant";
		}
	}
}
