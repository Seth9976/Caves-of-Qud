using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D13 RID: 3347
	public class CookingDomainTeleport_OnTeleport : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060083EA RID: 33770 RVA: 0x002FBB10 File Offset: 0x002F9D10
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature teleport@s,";
		}

		// Token: 0x060083EB RID: 33771 RVA: 0x002FBB17 File Offset: 0x002F9D17
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterTeleport");
			base.Register(Object, Registrar);
		}

		// Token: 0x060083EC RID: 33772 RVA: 0x002FBB2C File Offset: 0x002F9D2C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterTeleport")
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400396E RID: 14702
		public bool CurrentlyEnflamed;
	}
}
