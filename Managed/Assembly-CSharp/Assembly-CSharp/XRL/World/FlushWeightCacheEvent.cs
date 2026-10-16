using System;

namespace XRL.World
{
	// Token: 0x02000717 RID: 1815
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class FlushWeightCacheEvent : SingletonEvent<FlushWeightCacheEvent>
	{
		// Token: 0x06004BA2 RID: 19362 RVA: 0x001776BE File Offset: 0x001758BE
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x001776C7 File Offset: 0x001758C7
		public static void Send(GameObject Object)
		{
			if (GameObject.Validate(Object) && Object.WantEvent(SingletonEvent<FlushWeightCacheEvent>.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(SingletonEvent<FlushWeightCacheEvent>.Instance);
			}
		}
	}
}
