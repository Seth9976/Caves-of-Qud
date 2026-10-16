using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D00 RID: 3328
	public class CookingDomainRubber_OnProne : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x0600838F RID: 33679 RVA: 0x002FAFBA File Offset: 0x002F91BA
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature go@es prone,";
		}

		// Token: 0x06008390 RID: 33680 RVA: 0x002FAFC1 File Offset: 0x002F91C1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EffectApplied");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008391 RID: 33681 RVA: 0x002FAFD6 File Offset: 0x002F91D6
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EffectApplied" && E.GetParameter("Effect") is Prone)
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}
	}
}
