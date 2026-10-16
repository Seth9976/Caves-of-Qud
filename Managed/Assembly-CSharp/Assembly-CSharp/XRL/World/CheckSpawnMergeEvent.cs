using System;

namespace XRL.World
{
	// Token: 0x020006E3 RID: 1763
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckSpawnMergeEvent : PooledEvent<CheckSpawnMergeEvent>
	{
		// Token: 0x06004A1D RID: 18973 RVA: 0x00173875 File Offset: 0x00171A75
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x0017387E File Offset: 0x00171A7E
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Other = null;
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x00173894 File Offset: 0x00171A94
		public static bool Check(GameObject Object, GameObject Other)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CheckSpawnMerge"))
			{
				Event @event = Event.New("CheckSpawnMerge", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Other", Other);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CheckSpawnMergeEvent>.ID, MinEvent.CascadeLevel))
			{
				CheckSpawnMergeEvent checkSpawnMergeEvent = PooledEvent<CheckSpawnMergeEvent>.FromPool();
				checkSpawnMergeEvent.Object = Object;
				checkSpawnMergeEvent.Other = Other;
				flag = Object.HandleEvent(checkSpawnMergeEvent);
			}
			return flag;
		}

		// Token: 0x040023A8 RID: 9128
		public GameObject Object;

		// Token: 0x040023A9 RID: 9129
		public GameObject Other;
	}
}
