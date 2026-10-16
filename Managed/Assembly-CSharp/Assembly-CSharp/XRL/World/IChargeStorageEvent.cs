using System;

namespace XRL.World
{
	// Token: 0x020007C1 RID: 1985
	[GameEvent(Base = true)]
	public abstract class IChargeStorageEvent : IChargeEvent
	{
		// Token: 0x06005058 RID: 20568 RVA: 0x00186720 File Offset: 0x00184920
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x00186734 File Offset: 0x00184934
		public override void Reset()
		{
			base.Reset();
			this.Transient = 0;
			this.UnlimitedTransient = false;
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x0018674A File Offset: 0x0018494A
		public override Event GenerateRegisteredEvent(string ID)
		{
			Event @event = base.GenerateRegisteredEvent(ID);
			@event.SetParameter("Transient", this.Transient);
			@event.SetFlag("UnlimitedTransient", this.UnlimitedTransient);
			return @event;
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00186777 File Offset: 0x00184977
		public override void SyncFromRegisteredEvent(Event E, bool AllFields = false)
		{
			base.SyncFromRegisteredEvent(E, AllFields);
			this.Transient = E.GetIntParameter("Transient", 0);
			this.UnlimitedTransient = E.HasFlag("UnlimitedTransient");
		}

		// Token: 0x040027CE RID: 10190
		public int Transient;

		// Token: 0x040027CF RID: 10191
		public bool UnlimitedTransient;
	}
}
