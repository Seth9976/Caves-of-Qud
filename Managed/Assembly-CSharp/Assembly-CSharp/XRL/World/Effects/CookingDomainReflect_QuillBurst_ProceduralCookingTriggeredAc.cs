using System;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CE7 RID: 3303
	[Serializable]
	public class CookingDomainReflect_QuillBurst_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008323 RID: 33571 RVA: 0x002FA142 File Offset: 0x002F8342
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(8, 9);
			base.Init(target);
		}

		// Token: 0x06008324 RID: 33572 RVA: 0x002FA159 File Offset: 0x002F8359
		public override string GetDescription()
		{
			return "@they expel quills per the Quills mutation at level " + this.Tier.ToString() + ".";
		}

		// Token: 0x06008325 RID: 33573 RVA: 0x002FA175 File Offset: 0x002F8375
		public override string GetTemplatedDescription()
		{
			return "@they expel quills per the Quills mutation at level 8-9.";
		}

		// Token: 0x06008326 RID: 33574 RVA: 0x002FA17C File Offset: 0x002F837C
		public override string GetNotification()
		{
			return "@they expel a blast of quills.";
		}

		// Token: 0x06008327 RID: 33575 RVA: 0x002FA184 File Offset: 0x002F8384
		public override void Apply(GameObject go)
		{
			go.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_quills_expel", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			foreach (Cell cell in go.Physics.CurrentCell.GetLocalAdjacentCells())
			{
				new Quills
				{
					Level = this.Tier,
					ParentObject = go
				}.QuillFling(cell, Stat.Roll("1d4+1", null), false, false, null);
			}
		}

		// Token: 0x0400395F RID: 14687
		public int Tier;
	}
}
