using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001441 RID: 5185
	[Serializable]
	public class GlitterDustGeneration : GasGeneration
	{
		// Token: 0x0600CA68 RID: 51816 RVA: 0x0049A5C1 File Offset: 0x004987C1
		public GlitterDustGeneration()
			: base("GlitterGas")
		{
		}

		// Token: 0x0600CA69 RID: 51817 RVA: 0x0049A5CE File Offset: 0x004987CE
		public override int GetReleaseDuration(int Level)
		{
			return (Level + 2) * 3 / 2;
		}

		// Token: 0x0600CA6A RID: 51818 RVA: 0x0049A5D7 File Offset: 0x004987D7
		public override int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA6B RID: 51819 RVA: 0x0049A5DB File Offset: 0x004987DB
		public override string GetReleaseAbilityName()
		{
			return "Release Glitter Dust";
		}
	}
}
