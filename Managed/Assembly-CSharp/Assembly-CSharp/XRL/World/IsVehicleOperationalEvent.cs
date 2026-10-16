using System;

namespace XRL.World
{
	// Token: 0x020007F6 RID: 2038
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class IsVehicleOperationalEvent : PooledEvent<IsVehicleOperationalEvent>
	{
		// Token: 0x060051AD RID: 20909 RVA: 0x0018A503 File Offset: 0x00188703
		public override int GetCascadeLevel()
		{
			return IsVehicleOperationalEvent.CascadeLevel;
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x0018A50A File Offset: 0x0018870A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x0018A513 File Offset: 0x00188713
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x0018A524 File Offset: 0x00188724
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (GameObject.Validate(ref Object))
			{
				if (Object.HasRegisteredEvent("IsVehicleOperational"))
				{
					Event @event = Event.New("IsVehicleOperational", 0, 0, 0);
					@event.SetParameter("Object", Object);
					flag = Object.FireEvent(@event);
				}
				if (flag && Object.WantEvent(PooledEvent<IsVehicleOperationalEvent>.ID, IsVehicleOperationalEvent.CascadeLevel))
				{
					IsVehicleOperationalEvent isVehicleOperationalEvent = PooledEvent<IsVehicleOperationalEvent>.FromPool();
					isVehicleOperationalEvent.Object = Object;
					flag = Object.HandleEvent(isVehicleOperationalEvent);
				}
			}
			return flag;
		}

		// Token: 0x040028F5 RID: 10485
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028F6 RID: 10486
		public GameObject Object;
	}
}
