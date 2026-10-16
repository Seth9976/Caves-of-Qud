using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000DB4 RID: 3508
	public class AdrenalControl2Boosted : Effect
	{
		// Token: 0x06008C10 RID: 35856 RVA: 0x0031B5D9 File Offset: 0x003197D9
		public AdrenalControl2Boosted()
		{
			this.DisplayName = "{{R|adrenaline flowing}}";
		}

		// Token: 0x06008C11 RID: 35857 RVA: 0x0031B5EC File Offset: 0x003197EC
		public AdrenalControl2Boosted(AdrenalControl2 mutation)
			: this()
		{
			this.AppliedSpeedBonus = mutation.GetQuicknessBonus(mutation.Level);
			this.AppliedMutationBonus = mutation.GetMutationBonus(mutation.Level);
			this.Duration = mutation.GetQuicknessDuration(mutation.Level);
		}

		// Token: 0x06008C12 RID: 35858 RVA: 0x0031B62C File Offset: 0x0031982C
		public override void Remove(GameObject Object)
		{
			base.PlayWorldSound("sfx_ability_mutation_physical_generic_deactivate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
		}

		// Token: 0x06008C13 RID: 35859 RVA: 0x0031B664 File Offset: 0x00319864
		public override int GetEffectType()
		{
			return 4;
		}

		// Token: 0x06008C14 RID: 35860 RVA: 0x0031B668 File Offset: 0x00319868
		public override string GetDetails()
		{
			if (this.AppliedMutationBonus == 1)
			{
				return string.Format("+{0} Quickness\n+{1} rank to physical mutations", this.AppliedSpeedBonus, this.AppliedMutationBonus);
			}
			return string.Format("+{0} Quickness\n+{1} ranks to physical mutations", this.AppliedSpeedBonus, this.AppliedMutationBonus);
		}

		// Token: 0x04003AF0 RID: 15088
		public int AppliedSpeedBonus;

		// Token: 0x04003AF1 RID: 15089
		public int AppliedMutationBonus;
	}
}
