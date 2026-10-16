using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E9A RID: 3738
	[Serializable]
	public class DisabledNaturalHealing : IPart
	{
		// Token: 0x06009655 RID: 38485 RVA: 0x00367B54 File Offset: 0x00365D54
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009656 RID: 38486 RVA: 0x00367B57 File Offset: 0x00365D57
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Regenerating");
		}

		// Token: 0x06009657 RID: 38487 RVA: 0x00367B64 File Offset: 0x00365D64
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Regenerating")
			{
				E.SetParameter("Amount", 0);
			}
			return base.FireEvent(E);
		}
	}
}
