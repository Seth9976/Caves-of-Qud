using System;

namespace XRL.World
{
	// Token: 0x0200080C RID: 2060
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class MutationsSubjectToEMPEvent : PooledEvent<MutationsSubjectToEMPEvent>
	{
		// Token: 0x06005249 RID: 21065 RVA: 0x0018C413 File Offset: 0x0018A613
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x0018C41C File Offset: 0x0018A61C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x0018C42C File Offset: 0x0018A62C
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("MutationsSubjectToEMP"))
			{
				Event @event = Event.New("MutationsSubjectToEMP", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<MutationsSubjectToEMPEvent>.ID, MinEvent.CascadeLevel))
			{
				MutationsSubjectToEMPEvent mutationsSubjectToEMPEvent = PooledEvent<MutationsSubjectToEMPEvent>.FromPool();
				mutationsSubjectToEMPEvent.Object = Object;
				flag = Object.HandleEvent(mutationsSubjectToEMPEvent);
			}
			return !flag;
		}

		// Token: 0x04002959 RID: 10585
		public GameObject Object;
	}
}
