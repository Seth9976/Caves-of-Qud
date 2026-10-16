using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CA4 RID: 3236
	public class CookingDomainFear_OnFeared : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060081D1 RID: 33233 RVA: 0x002F6CC2 File Offset: 0x002F4EC2
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature become@s afraid,";
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x002F6CC9 File Offset: 0x002F4EC9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("FearApplied");
			base.Register(Object, Registrar);
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x002F6CDE File Offset: 0x002F4EDE
		public override bool FireEvent(Event E)
		{
			if (E.ID == "FearApplied")
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}
	}
}
