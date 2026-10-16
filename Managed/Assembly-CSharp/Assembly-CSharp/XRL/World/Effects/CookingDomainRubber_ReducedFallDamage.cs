using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D04 RID: 3332
	[Serializable]
	public class CookingDomainRubber_ReducedFallDamage : ProceduralCookingEffectUnit
	{
		// Token: 0x060083A3 RID: 33699 RVA: 0x002FB231 File Offset: 0x002F9431
		public override string GetDescription()
		{
			return "Falling damage @thisCreature take@s is reduced by 50%.";
		}

		// Token: 0x060083A4 RID: 33700 RVA: 0x002FB238 File Offset: 0x002F9438
		public override string GetTemplatedDescription()
		{
			return "Falling damage @thisCreature take@s is reduced by 50%.";
		}

		// Token: 0x060083A5 RID: 33701 RVA: 0x002FB23F File Offset: 0x002F943F
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x060083A6 RID: 33702 RVA: 0x002FB24D File Offset: 0x002F944D
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x060083A7 RID: 33703 RVA: 0x002FB25C File Offset: 0x002F945C
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.HasAttribute("Falling"))
				{
					damage.Amount /= 2;
				}
			}
		}
	}
}
