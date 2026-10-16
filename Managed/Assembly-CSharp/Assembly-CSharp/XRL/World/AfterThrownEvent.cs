using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000671 RID: 1649
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterThrownEvent : IActOnItemEvent
	{
		// Token: 0x060046EC RID: 18156 RVA: 0x00167FDB File Offset: 0x001661DB
		public AfterThrownEvent()
		{
			this.ID = AfterThrownEvent.ID;
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00167FEE File Offset: 0x001661EE
		public static int CountPool()
		{
			if (AfterThrownEvent.Pool != null)
			{
				return AfterThrownEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x00168003 File Offset: 0x00166203
		public static void ResetPool()
		{
			while (AfterThrownEvent.PoolCounter > 0)
			{
				AfterThrownEvent.Pool[--AfterThrownEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x0016802B File Offset: 0x0016622B
		public static void ResetTo(ref AfterThrownEvent E)
		{
			MinEvent.ResetTo<AfterThrownEvent>(E, AfterThrownEvent.Pool, ref AfterThrownEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00168041 File Offset: 0x00166241
		public static AfterThrownEvent FromPool()
		{
			return MinEvent.FromPool<AfterThrownEvent>(ref AfterThrownEvent.Pool, ref AfterThrownEvent.PoolCounter, 8192);
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x00168057 File Offset: 0x00166257
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x0016806B File Offset: 0x0016626B
		public override void Reset()
		{
			base.Reset();
			this.ApparentTarget = null;
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x0016807A File Offset: 0x0016627A
		public static AfterThrownEvent FromPool(GameObject Actor, GameObject Item, GameObject ApparentTarget)
		{
			AfterThrownEvent afterThrownEvent = AfterThrownEvent.FromPool();
			afterThrownEvent.Actor = Actor;
			afterThrownEvent.Item = Item;
			afterThrownEvent.ApparentTarget = ApparentTarget;
			return afterThrownEvent;
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x00168098 File Offset: 0x00166298
		public static bool Check(GameObject Actor, GameObject Item, GameObject ApparentTarget)
		{
			if (GameObject.Validate(ref Item) && Item.HasRegisteredEvent("AfterThrown"))
			{
				Event @event = Event.New("AfterThrown", 0, 0, 0);
				@event.SetParameter("Owner", Actor);
				@event.SetParameter("Object", Item);
				@event.SetParameter("ApparentTarget", ApparentTarget);
				if (!Item.FireEvent(@event))
				{
					return false;
				}
			}
			if (GameObject.Validate(ref Item) && Item.WantEvent(AfterThrownEvent.ID, MinEvent.CascadeLevel))
			{
				AfterThrownEvent afterThrownEvent = AfterThrownEvent.FromPool(Actor, Item, ApparentTarget);
				if (!Item.HandleEvent(afterThrownEvent))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040021CB RID: 8651
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterThrownEvent), null, new MinEvent.EventPoolCount(AfterThrownEvent.CountPool), new MinEvent.EventPoolReset(AfterThrownEvent.ResetPool));

		// Token: 0x040021CC RID: 8652
		private static List<AfterThrownEvent> Pool;

		// Token: 0x040021CD RID: 8653
		private static int PoolCounter;

		// Token: 0x040021CE RID: 8654
		public GameObject ApparentTarget;
	}
}
