using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CED RID: 3309
	public class CookingDomainReflect_OnReflectedDamageHighTier : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x0600833C RID: 33596 RVA: 0x002FA649 File Offset: 0x002F8849
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature reflect@s damage, there's a 100% chance";
		}

		// Token: 0x0600833D RID: 33597 RVA: 0x002FA650 File Offset: 0x002F8850
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ReflectedDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600833E RID: 33598 RVA: 0x002FA665 File Offset: 0x002F8865
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.CurrentlyEnflamed = false;
		}

		// Token: 0x0600833F RID: 33599 RVA: 0x002FA675 File Offset: 0x002F8875
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ReflectedDamage")
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003962 RID: 14690
		public bool CurrentlyEnflamed;
	}
}
