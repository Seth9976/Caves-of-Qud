using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000870 RID: 2160
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class UnequippedEvent : IActOnItemEvent
	{
		// Token: 0x060055D4 RID: 21972 RVA: 0x001998FA File Offset: 0x00197AFA
		public UnequippedEvent()
		{
			this.ID = UnequippedEvent.ID;
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x0019990D File Offset: 0x00197B0D
		public static int CountPool()
		{
			if (UnequippedEvent.Pool != null)
			{
				return UnequippedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x00199922 File Offset: 0x00197B22
		public static void ResetPool()
		{
			while (UnequippedEvent.PoolCounter > 0)
			{
				UnequippedEvent.Pool[--UnequippedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x0019994A File Offset: 0x00197B4A
		public static void ResetTo(ref UnequippedEvent E)
		{
			MinEvent.ResetTo<UnequippedEvent>(E, UnequippedEvent.Pool, ref UnequippedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x00199960 File Offset: 0x00197B60
		public static UnequippedEvent FromPool()
		{
			return MinEvent.FromPool<UnequippedEvent>(ref UnequippedEvent.Pool, ref UnequippedEvent.PoolCounter, 8192);
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x00199976 File Offset: 0x00197B76
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x0019998C File Offset: 0x00197B8C
		public static void Send(GameObject Object, GameObject WasEquippedBy)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("Unequipped"))
			{
				Event @event = new Event("Unequipped");
				@event.SetParameter("UnequippingObject", WasEquippedBy);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", WasEquippedBy);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(UnequippedEvent.ID, MinEvent.CascadeLevel))
			{
				UnequippedEvent unequippedEvent = UnequippedEvent.FromPool();
				unequippedEvent.Actor = WasEquippedBy;
				unequippedEvent.Item = Object;
				flag = Object.HandleEvent(unequippedEvent);
			}
		}

		// Token: 0x04002B3C RID: 11068
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(UnequippedEvent), null, new MinEvent.EventPoolCount(UnequippedEvent.CountPool), new MinEvent.EventPoolReset(UnequippedEvent.ResetPool));

		// Token: 0x04002B3D RID: 11069
		private static List<UnequippedEvent> Pool;

		// Token: 0x04002B3E RID: 11070
		private static int PoolCounter;
	}
}
