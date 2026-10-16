using System;

namespace XRL.World
{
	// Token: 0x02000862 RID: 2146
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class SyncMutationLevelsEvent : PooledEvent<SyncMutationLevelsEvent>
	{
		// Token: 0x06005578 RID: 21880 RVA: 0x00198A2C File Offset: 0x00196C2C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x00198A35 File Offset: 0x00196C35
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x00198A44 File Offset: 0x00196C44
		public static void Send(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("SyncMutationLevels"))
			{
				Event @event = Event.New("SyncMutationLevels", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<SyncMutationLevelsEvent>.ID, MinEvent.CascadeLevel))
			{
				SyncMutationLevelsEvent syncMutationLevelsEvent = PooledEvent<SyncMutationLevelsEvent>.FromPool();
				syncMutationLevelsEvent.Object = Object;
				flag = Object.HandleEvent(syncMutationLevelsEvent);
			}
		}

		// Token: 0x04002B16 RID: 11030
		public GameObject Object;
	}
}
