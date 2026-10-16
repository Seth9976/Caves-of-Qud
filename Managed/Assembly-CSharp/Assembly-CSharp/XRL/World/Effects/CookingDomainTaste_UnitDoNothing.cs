using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D10 RID: 3344
	[Serializable]
	public class CookingDomainTaste_UnitDoNothing : ProceduralCookingEffectUnit
	{
		// Token: 0x060083DF RID: 33759 RVA: 0x002FBA2C File Offset: 0x002F9C2C
		public override string GetDescription()
		{
			return "Mmmm.";
		}

		// Token: 0x060083E0 RID: 33760 RVA: 0x002FBA33 File Offset: 0x002F9C33
		public override string GetTemplatedDescription()
		{
			return "Guaranteed to be tasty if eaten while hungry.";
		}

		// Token: 0x060083E1 RID: 33761 RVA: 0x002FBA3A File Offset: 0x002F9C3A
		public override void Init(GameObject target)
		{
		}

		// Token: 0x060083E2 RID: 33762 RVA: 0x002FBA3C File Offset: 0x002F9C3C
		public override void Apply(GameObject Object, Effect parent)
		{
		}

		// Token: 0x060083E3 RID: 33763 RVA: 0x002FBA3E File Offset: 0x002F9C3E
		public override void Remove(GameObject Object, Effect parent)
		{
		}
	}
}
