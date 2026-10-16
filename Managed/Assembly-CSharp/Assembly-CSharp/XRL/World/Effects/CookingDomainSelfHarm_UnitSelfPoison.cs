using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D07 RID: 3335
	[Serializable]
	public class CookingDomainSelfHarm_UnitSelfPoison : ProceduralCookingEffectUnit
	{
		// Token: 0x060083B4 RID: 33716 RVA: 0x002FB376 File Offset: 0x002F9576
		public override string GetDescription()
		{
			return "Poisons @thisCreature.";
		}

		// Token: 0x060083B5 RID: 33717 RVA: 0x002FB380 File Offset: 0x002F9580
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.ApplyEffect(new Poisoned(Stat.Roll("1d4+4", null), Stat.Roll("1d2+2", null).ToString() + "d2", 10, null, false, false), null);
		}

		// Token: 0x060083B6 RID: 33718 RVA: 0x002FB3C7 File Offset: 0x002F95C7
		public override void Remove(GameObject Object, Effect parent)
		{
		}

		// Token: 0x04003968 RID: 14696
		public GameObject Object;
	}
}
