using System;

namespace XRL.World.Parts
{
	// Token: 0x02001212 RID: 4626
	[Serializable]
	public class RespondToEvent : IPoweredPart
	{
		// Token: 0x0600B7E4 RID: 47076 RVA: 0x0042E85D File Offset: 0x0042CA5D
		public RespondToEvent()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B7E5 RID: 47077 RVA: 0x0042E873 File Offset: 0x0042CA73
		public override bool SameAs(IPart p)
		{
			return !((p as RespondToEvent).EventHandled != this.EventHandled) && base.SameAs(p);
		}

		// Token: 0x0600B7E6 RID: 47078 RVA: 0x0042E896 File Offset: 0x0042CA96
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B7E7 RID: 47079 RVA: 0x0042E89C File Offset: 0x0042CA9C
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
		}

		// Token: 0x0600B7E8 RID: 47080 RVA: 0x0042E8CF File Offset: 0x0042CACF
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600B7E9 RID: 47081 RVA: 0x0042E8D2 File Offset: 0x0042CAD2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (!string.IsNullOrEmpty(this.EventHandled))
			{
				Registrar.Register(this.EventHandled);
			}
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B7EA RID: 47082 RVA: 0x0042E8F8 File Offset: 0x0042CAF8
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == this.EventHandled) || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.FireEvent(E);
		}

		// Token: 0x04004A02 RID: 18946
		public string EventHandled;
	}
}
