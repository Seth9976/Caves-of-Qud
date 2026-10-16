using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006BA RID: 1722
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanBeModdedEvent : IActOnItemEvent
	{
		// Token: 0x06004916 RID: 18710 RVA: 0x00170A10 File Offset: 0x0016EC10
		public CanBeModdedEvent()
		{
			this.ID = CanBeModdedEvent.ID;
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x00170A23 File Offset: 0x0016EC23
		public static int CountPool()
		{
			if (CanBeModdedEvent.Pool != null)
			{
				return CanBeModdedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x00170A38 File Offset: 0x0016EC38
		public static void ResetPool()
		{
			while (CanBeModdedEvent.PoolCounter > 0)
			{
				CanBeModdedEvent.Pool[--CanBeModdedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00170A60 File Offset: 0x0016EC60
		public static void ResetTo(ref CanBeModdedEvent E)
		{
			MinEvent.ResetTo<CanBeModdedEvent>(E, CanBeModdedEvent.Pool, ref CanBeModdedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x00170A76 File Offset: 0x0016EC76
		public static CanBeModdedEvent FromPool()
		{
			return MinEvent.FromPool<CanBeModdedEvent>(ref CanBeModdedEvent.Pool, ref CanBeModdedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00170A8C File Offset: 0x0016EC8C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x00170AA0 File Offset: 0x0016ECA0
		public override void Reset()
		{
			base.Reset();
			this.ModName = null;
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x00170AB0 File Offset: 0x0016ECB0
		public static bool Check(GameObject Actor, GameObject Item, string ModName)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("CanBeModded");
				bool flag3 = GameObject.Validate(ref Item) && Item.HasRegisteredEvent("CanBeModded");
				if (flag2 || flag3)
				{
					Event @event = Event.New("CanBeModded", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Item", Item);
					@event.SetParameter("ModName", ModName);
					flag = (!flag2 || Actor.FireEvent(@event)) && (!flag3 || Item.FireEvent(@event));
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Actor) && Actor.WantEvent(CanBeModdedEvent.ID, MinEvent.CascadeLevel);
				bool flag5 = GameObject.Validate(ref Item) && Item.WantEvent(CanBeModdedEvent.ID, MinEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					CanBeModdedEvent canBeModdedEvent = CanBeModdedEvent.FromPool();
					canBeModdedEvent.Actor = Actor;
					canBeModdedEvent.Item = Item;
					canBeModdedEvent.ModName = ModName;
					flag = (!flag4 || Actor.HandleEvent(canBeModdedEvent)) && (!flag5 || Item.HandleEvent(canBeModdedEvent));
				}
			}
			return flag;
		}

		// Token: 0x0400230F RID: 8975
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanBeModdedEvent), null, new MinEvent.EventPoolCount(CanBeModdedEvent.CountPool), new MinEvent.EventPoolReset(CanBeModdedEvent.ResetPool));

		// Token: 0x04002310 RID: 8976
		private static List<CanBeModdedEvent> Pool;

		// Token: 0x04002311 RID: 8977
		private static int PoolCounter;

		// Token: 0x04002312 RID: 8978
		public string ModName;
	}
}
