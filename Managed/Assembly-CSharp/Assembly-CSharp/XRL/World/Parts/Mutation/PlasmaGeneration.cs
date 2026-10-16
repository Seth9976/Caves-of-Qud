using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200143B RID: 5179
	[Serializable]
	public class PlasmaGeneration : GasGeneration
	{
		// Token: 0x0600CA52 RID: 51794 RVA: 0x0049A510 File Offset: 0x00498710
		public PlasmaGeneration()
			: base("Plasma")
		{
		}

		// Token: 0x0600CA53 RID: 51795 RVA: 0x0049A51D File Offset: 0x0049871D
		public override string GetReleaseAbilityName()
		{
			return "Release Plasma";
		}
	}
}
