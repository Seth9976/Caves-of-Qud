using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CD2 RID: 3282
	public class CookingDomainPhase_OnPhaseIn : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008290 RID: 33424 RVA: 0x002F7E4F File Offset: 0x002F604F
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature phase@s in,";
		}

		// Token: 0x06008291 RID: 33425 RVA: 0x002F7E56 File Offset: 0x002F6056
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterPhaseIn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008292 RID: 33426 RVA: 0x002F7E6B File Offset: 0x002F606B
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterPhaseIn")
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}
	}
}
