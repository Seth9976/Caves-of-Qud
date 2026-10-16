using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D05 RID: 3333
	[Serializable]
	public class CookingDomainRubber_ExtraReducedFallDamage : ProceduralCookingEffectUnit
	{
		// Token: 0x060083A9 RID: 33705 RVA: 0x002FB2B2 File Offset: 0x002F94B2
		public override string GetDescription()
		{
			return "Falling damage @thisCreature take@s is reduced by 90%.";
		}

		// Token: 0x060083AA RID: 33706 RVA: 0x002FB2B9 File Offset: 0x002F94B9
		public override string GetTemplatedDescription()
		{
			return "Falling damage @thisCreature take@s is reduced by 90%.";
		}

		// Token: 0x060083AB RID: 33707 RVA: 0x002FB2C0 File Offset: 0x002F94C0
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x060083AC RID: 33708 RVA: 0x002FB2CE File Offset: 0x002F94CE
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x060083AD RID: 33709 RVA: 0x002FB2DC File Offset: 0x002F94DC
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.HasAttribute("Falling"))
				{
					damage.Amount /= 10;
				}
			}
		}
	}
}
