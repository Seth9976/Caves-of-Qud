using System;

namespace XRL.World
{
	// Token: 0x0200079B RID: 1947
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class GetTonicCapacityEvent : SingletonEvent<GetTonicCapacityEvent>
	{
		// Token: 0x06004F74 RID: 20340 RVA: 0x00183F4C File Offset: 0x0018214C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x00183F55 File Offset: 0x00182155
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.BaseCapacity = 0;
			this.Capacity = 0;
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x00183F72 File Offset: 0x00182172
		public static int GetFor(GameObject Actor, int BaseCapacity = 1)
		{
			SingletonEvent<GetTonicCapacityEvent>.Instance.Actor = Actor;
			SingletonEvent<GetTonicCapacityEvent>.Instance.BaseCapacity = BaseCapacity;
			SingletonEvent<GetTonicCapacityEvent>.Instance.Capacity = BaseCapacity;
			Actor.HandleEvent(SingletonEvent<GetTonicCapacityEvent>.Instance);
			return SingletonEvent<GetTonicCapacityEvent>.Instance.Capacity;
		}

		// Token: 0x0400272E RID: 10030
		public GameObject Actor;

		// Token: 0x0400272F RID: 10031
		public int BaseCapacity;

		// Token: 0x04002730 RID: 10032
		public int Capacity;
	}
}
