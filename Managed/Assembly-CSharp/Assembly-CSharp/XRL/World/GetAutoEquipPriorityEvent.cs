using System;

namespace XRL.World
{
	// Token: 0x0200072B RID: 1835
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetAutoEquipPriorityEvent : PooledEvent<GetAutoEquipPriorityEvent>
	{
		// Token: 0x06004C1D RID: 19485 RVA: 0x00178C7A File Offset: 0x00176E7A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x00178C83 File Offset: 0x00176E83
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Default = 0;
			this.Priority = 0;
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x00178CA0 File Offset: 0x00176EA0
		public static int GetFor(GameObject Object, int Default = 10)
		{
			int num = Default;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetAutoEquipPriority"))
			{
				Event @event = Event.New("GetAutoEquipPriority", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Default", Default);
				@event.SetParameter("Priority", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Priority", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetAutoEquipPriorityEvent>.ID, MinEvent.CascadeLevel))
			{
				GetAutoEquipPriorityEvent getAutoEquipPriorityEvent = PooledEvent<GetAutoEquipPriorityEvent>.FromPool();
				getAutoEquipPriorityEvent.Object = Object;
				getAutoEquipPriorityEvent.Default = Default;
				getAutoEquipPriorityEvent.Priority = num;
				flag = Object.HandleEvent(getAutoEquipPriorityEvent);
				num = getAutoEquipPriorityEvent.Priority;
			}
			return num;
		}

		// Token: 0x040024D2 RID: 9426
		public GameObject Object;

		// Token: 0x040024D3 RID: 9427
		public int Default;

		// Token: 0x040024D4 RID: 9428
		public int Priority;
	}
}
