using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CFA RID: 3322
	public class CookingDomainRegenLowtier_OnSalve : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008372 RID: 33650 RVA: 0x002FAD54 File Offset: 0x002F8F54
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature use@s a salve or ubernostrum injector,";
		}

		// Token: 0x06008373 RID: 33651 RVA: 0x002FAD5B File Offset: 0x002F8F5B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyingSalve");
			Registrar.Register("ApplyingUbernostrum");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008374 RID: 33652 RVA: 0x002FAD7B File Offset: 0x002F8F7B
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyingSalve" || E.ID == "ApplyingUbernostrum")
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}
	}
}
