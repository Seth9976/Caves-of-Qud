using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CD3 RID: 3283
	public class CookingDomainPhase_OnPhaseOut : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008294 RID: 33428 RVA: 0x002F7E94 File Offset: 0x002F6094
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature phase@s out,";
		}

		// Token: 0x06008295 RID: 33429 RVA: 0x002F7E9B File Offset: 0x002F609B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterPhaseOut");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008296 RID: 33430 RVA: 0x002F7EB0 File Offset: 0x002F60B0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterPhaseOut")
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}
	}
}
