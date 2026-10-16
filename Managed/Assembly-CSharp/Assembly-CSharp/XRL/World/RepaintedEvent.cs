using System;

namespace XRL.World
{
	// Token: 0x02000843 RID: 2115
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class RepaintedEvent : SingletonEvent<RepaintedEvent>
	{
		// Token: 0x060053FC RID: 21500 RVA: 0x00190DFF File Offset: 0x0018EFFF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x00190E08 File Offset: 0x0018F008
		public static void Send(GameObject obj)
		{
			obj.HandleEvent(SingletonEvent<RepaintedEvent>.Instance);
		}
	}
}
