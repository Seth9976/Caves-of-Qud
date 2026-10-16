using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200086F RID: 2159
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class TryRemoveFromContextEvent : IRemoveFromContextEvent
	{
		// Token: 0x060055CC RID: 21964 RVA: 0x001997FE File Offset: 0x001979FE
		public TryRemoveFromContextEvent()
		{
			this.ID = TryRemoveFromContextEvent.ID;
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x00199811 File Offset: 0x00197A11
		public static int CountPool()
		{
			if (TryRemoveFromContextEvent.Pool != null)
			{
				return TryRemoveFromContextEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x00199826 File Offset: 0x00197A26
		public static void ResetPool()
		{
			while (TryRemoveFromContextEvent.PoolCounter > 0)
			{
				TryRemoveFromContextEvent.Pool[--TryRemoveFromContextEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x0019984E File Offset: 0x00197A4E
		public static void ResetTo(ref TryRemoveFromContextEvent E)
		{
			MinEvent.ResetTo<TryRemoveFromContextEvent>(E, TryRemoveFromContextEvent.Pool, ref TryRemoveFromContextEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x00199864 File Offset: 0x00197A64
		public static TryRemoveFromContextEvent FromPool()
		{
			return MinEvent.FromPool<TryRemoveFromContextEvent>(ref TryRemoveFromContextEvent.Pool, ref TryRemoveFromContextEvent.PoolCounter, 8192);
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x0019987A File Offset: 0x00197A7A
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x00199890 File Offset: 0x00197A90
		public static bool Check(GameObject Object, IEvent ParentEvent = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object))
			{
				TryRemoveFromContextEvent tryRemoveFromContextEvent = TryRemoveFromContextEvent.FromPool();
				tryRemoveFromContextEvent.Object = Object;
				flag = Object.HandleEvent(tryRemoveFromContextEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(tryRemoveFromContextEvent);
				}
			}
			return flag;
		}

		// Token: 0x04002B39 RID: 11065
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(TryRemoveFromContextEvent), null, new MinEvent.EventPoolCount(TryRemoveFromContextEvent.CountPool), new MinEvent.EventPoolReset(TryRemoveFromContextEvent.ResetPool));

		// Token: 0x04002B3A RID: 11066
		private static List<TryRemoveFromContextEvent> Pool;

		// Token: 0x04002B3B RID: 11067
		private static int PoolCounter;
	}
}
