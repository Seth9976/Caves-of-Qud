using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000866 RID: 2150
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class TakenEvent : IActOnItemEvent
	{
		// Token: 0x0600558B RID: 21899 RVA: 0x00198C27 File Offset: 0x00196E27
		public TakenEvent()
		{
			this.ID = TakenEvent.ID;
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x00198C3A File Offset: 0x00196E3A
		public static int CountPool()
		{
			if (TakenEvent.Pool != null)
			{
				return TakenEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x00198C4F File Offset: 0x00196E4F
		public static void ResetPool()
		{
			while (TakenEvent.PoolCounter > 0)
			{
				TakenEvent.Pool[--TakenEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00198C77 File Offset: 0x00196E77
		public static void ResetTo(ref TakenEvent E)
		{
			MinEvent.ResetTo<TakenEvent>(E, TakenEvent.Pool, ref TakenEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x00198C8D File Offset: 0x00196E8D
		public static TakenEvent FromPool()
		{
			return MinEvent.FromPool<TakenEvent>(ref TakenEvent.Pool, ref TakenEvent.PoolCounter, 8192);
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x00198CA3 File Offset: 0x00196EA3
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x00198CB7 File Offset: 0x00196EB7
		public override void Reset()
		{
			base.Reset();
			this.Context = null;
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x00198CC8 File Offset: 0x00196EC8
		public static void Send(GameObject Item, GameObject Actor, string Context = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Item) && Item.HasRegisteredEvent("Taken"))
			{
				Event @event = Event.New("Taken", 0, 0, 0);
				@event.SetParameter("Item", Item);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("TakingObject", Actor);
				@event.SetParameter("Context", Context);
				flag = Item.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Item) && Item.WantEvent(TakenEvent.ID, MinEvent.CascadeLevel))
			{
				TakenEvent takenEvent = TakenEvent.FromPool();
				takenEvent.Item = Item;
				takenEvent.Actor = Actor;
				takenEvent.Context = Context;
				flag = Item.HandleEvent(takenEvent);
			}
		}

		// Token: 0x04002B1D RID: 11037
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(TakenEvent), null, new MinEvent.EventPoolCount(TakenEvent.CountPool), new MinEvent.EventPoolReset(TakenEvent.ResetPool));

		// Token: 0x04002B1E RID: 11038
		private static List<TakenEvent> Pool;

		// Token: 0x04002B1F RID: 11039
		private static int PoolCounter;

		// Token: 0x04002B20 RID: 11040
		public string Context;
	}
}
