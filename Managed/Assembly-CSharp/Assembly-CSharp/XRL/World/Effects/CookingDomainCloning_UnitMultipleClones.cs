using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C84 RID: 3204
	[Serializable]
	public class CookingDomainCloning_UnitMultipleClones : ProceduralCookingEffectUnit
	{
		// Token: 0x0600815B RID: 33115 RVA: 0x002F62C6 File Offset: 0x002F44C6
		public override string GetDescription()
		{
			return "Causes @thisCreature to multiply 1 to 3 times.";
		}

		// Token: 0x0600815C RID: 33116 RVA: 0x002F62D0 File Offset: 0x002F44D0
		public override void Apply(GameObject Object, Effect parent)
		{
			int num = Stat.Random(1, 3);
			int num2 = num;
			Object.ApplyEffect(new Budding(Object, num2, "Budding"), null);
		}

		// Token: 0x0600815D RID: 33117 RVA: 0x002F62FB File Offset: 0x002F44FB
		public override void Remove(GameObject Object, Effect parent)
		{
		}

		// Token: 0x0400391B RID: 14619
		public GameObject Object;
	}
}
