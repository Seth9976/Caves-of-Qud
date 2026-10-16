using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CEC RID: 3308
	public class CookingDomainReflect_OnReflectedDamage : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008337 RID: 33591 RVA: 0x002FA5E8 File Offset: 0x002F87E8
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature reflect@s damage, there's a 50% chance";
		}

		// Token: 0x06008338 RID: 33592 RVA: 0x002FA5EF File Offset: 0x002F87EF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ReflectedDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008339 RID: 33593 RVA: 0x002FA604 File Offset: 0x002F8804
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.CurrentlyEnflamed = false;
		}

		// Token: 0x0600833A RID: 33594 RVA: 0x002FA614 File Offset: 0x002F8814
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ReflectedDamage" && Stat.Random(1, 100) <= 50)
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003961 RID: 14689
		public bool CurrentlyEnflamed;
	}
}
