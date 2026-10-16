using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200143A RID: 5178
	[Serializable]
	public class CorrosiveGasGeneration : GasGeneration
	{
		// Token: 0x0600CA4C RID: 51788 RVA: 0x0049A4B5 File Offset: 0x004986B5
		public CorrosiveGasGeneration()
			: base("AcidGas")
		{
		}

		// Token: 0x0600CA4D RID: 51789 RVA: 0x0049A4C2 File Offset: 0x004986C2
		public override int GetReleaseDuration(int Level)
		{
			return Level + 2;
		}

		// Token: 0x0600CA4E RID: 51790 RVA: 0x0049A4C7 File Offset: 0x004986C7
		public override int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA4F RID: 51791 RVA: 0x0049A4CB File Offset: 0x004986CB
		public override string GetReleaseAbilityName()
		{
			return "Release Corrosive Gas";
		}

		// Token: 0x0600CA50 RID: 51792 RVA: 0x0049A4D2 File Offset: 0x004986D2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CA51 RID: 51793 RVA: 0x0049A4E8 File Offset: 0x004986E8
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("salt", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}
	}
}
