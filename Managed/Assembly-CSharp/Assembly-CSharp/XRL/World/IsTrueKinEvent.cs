using System;

namespace XRL.World
{
	// Token: 0x020007F5 RID: 2037
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class IsTrueKinEvent : PooledEvent<IsTrueKinEvent>
	{
		// Token: 0x060051A8 RID: 20904 RVA: 0x0018A3F1 File Offset: 0x001885F1
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x0018A3FA File Offset: 0x001885FA
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.IsTrueKin = false;
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x0018A410 File Offset: 0x00188610
		public static IsTrueKinEvent FromPool(GameObject Object, bool IsTrueKin)
		{
			IsTrueKinEvent isTrueKinEvent = PooledEvent<IsTrueKinEvent>.FromPool();
			isTrueKinEvent.Object = Object;
			isTrueKinEvent.IsTrueKin = IsTrueKin;
			return isTrueKinEvent;
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0018A428 File Offset: 0x00188628
		public static bool Check(GameObject Object)
		{
			bool? flag;
			if (Object == null)
			{
				flag = null;
			}
			else
			{
				GenotypeEntry genotypeEntry = Object.genotypeEntry;
				flag = ((genotypeEntry != null) ? new bool?(genotypeEntry.IsTrueKin) : null);
			}
			bool? flag2 = flag;
			bool flag3 = flag2.GetValueOrDefault();
			bool flag4 = true;
			if (flag4 && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("IsTrueKin"))
			{
				Event @event = Event.New("IsTrueKin", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetFlag("IsTrueKin", flag3);
				flag4 = Object.FireEvent(@event);
				flag3 = @event.HasFlag("IsTrueKin");
			}
			if (flag4 && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<IsTrueKinEvent>.ID, MinEvent.CascadeLevel))
			{
				IsTrueKinEvent isTrueKinEvent = IsTrueKinEvent.FromPool(Object, flag3);
				flag4 = Object.HandleEvent(isTrueKinEvent);
				flag3 = isTrueKinEvent.IsTrueKin;
			}
			return flag3;
		}

		// Token: 0x040028F3 RID: 10483
		public GameObject Object;

		// Token: 0x040028F4 RID: 10484
		public bool IsTrueKin;
	}
}
