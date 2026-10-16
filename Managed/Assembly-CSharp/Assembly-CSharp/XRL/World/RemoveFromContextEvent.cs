using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000842 RID: 2114
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class RemoveFromContextEvent : IRemoveFromContextEvent
	{
		// Token: 0x060053F4 RID: 21492 RVA: 0x00190D04 File Offset: 0x0018EF04
		public RemoveFromContextEvent()
		{
			this.ID = RemoveFromContextEvent.ID;
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x00190D17 File Offset: 0x0018EF17
		public static int CountPool()
		{
			if (RemoveFromContextEvent.Pool != null)
			{
				return RemoveFromContextEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x00190D2C File Offset: 0x0018EF2C
		public static void ResetPool()
		{
			while (RemoveFromContextEvent.PoolCounter > 0)
			{
				RemoveFromContextEvent.Pool[--RemoveFromContextEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x00190D54 File Offset: 0x0018EF54
		public static void ResetTo(ref RemoveFromContextEvent E)
		{
			MinEvent.ResetTo<RemoveFromContextEvent>(E, RemoveFromContextEvent.Pool, ref RemoveFromContextEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x00190D6A File Offset: 0x0018EF6A
		public static RemoveFromContextEvent FromPool()
		{
			return MinEvent.FromPool<RemoveFromContextEvent>(ref RemoveFromContextEvent.Pool, ref RemoveFromContextEvent.PoolCounter, 8192);
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x00190D80 File Offset: 0x0018EF80
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x00190D94 File Offset: 0x0018EF94
		public static void Send(GameObject Object, IEvent ParentEvent = null)
		{
			if (GameObject.Validate(ref Object))
			{
				RemoveFromContextEvent removeFromContextEvent = RemoveFromContextEvent.FromPool();
				removeFromContextEvent.Object = Object;
				Object.HandleEvent(removeFromContextEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(removeFromContextEvent);
				}
				RemoveFromContextEvent.ResetTo(ref removeFromContextEvent);
			}
		}

		// Token: 0x04002A30 RID: 10800
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(RemoveFromContextEvent), null, new MinEvent.EventPoolCount(RemoveFromContextEvent.CountPool), new MinEvent.EventPoolReset(RemoveFromContextEvent.ResetPool));

		// Token: 0x04002A31 RID: 10801
		private static List<RemoveFromContextEvent> Pool;

		// Token: 0x04002A32 RID: 10802
		private static int PoolCounter;
	}
}
