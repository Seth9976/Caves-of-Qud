using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000688 RID: 1672
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeAfterThrownEvent : IActOnItemEvent
	{
		// Token: 0x060047A1 RID: 18337 RVA: 0x0016A029 File Offset: 0x00168229
		public BeforeAfterThrownEvent()
		{
			this.ID = BeforeAfterThrownEvent.ID;
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x0016A03C File Offset: 0x0016823C
		public static int CountPool()
		{
			if (BeforeAfterThrownEvent.Pool != null)
			{
				return BeforeAfterThrownEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0016A051 File Offset: 0x00168251
		public static void ResetPool()
		{
			while (BeforeAfterThrownEvent.PoolCounter > 0)
			{
				BeforeAfterThrownEvent.Pool[--BeforeAfterThrownEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0016A079 File Offset: 0x00168279
		public static void ResetTo(ref BeforeAfterThrownEvent E)
		{
			MinEvent.ResetTo<BeforeAfterThrownEvent>(E, BeforeAfterThrownEvent.Pool, ref BeforeAfterThrownEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0016A08F File Offset: 0x0016828F
		public static BeforeAfterThrownEvent FromPool()
		{
			return MinEvent.FromPool<BeforeAfterThrownEvent>(ref BeforeAfterThrownEvent.Pool, ref BeforeAfterThrownEvent.PoolCounter, 8192);
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x0016A0A5 File Offset: 0x001682A5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x0016A0B9 File Offset: 0x001682B9
		public override void Reset()
		{
			base.Reset();
			this.ApparentTarget = null;
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x0016A0C8 File Offset: 0x001682C8
		public static BeforeAfterThrownEvent FromPool(GameObject Actor, GameObject Item, GameObject ApparentTarget)
		{
			BeforeAfterThrownEvent beforeAfterThrownEvent = BeforeAfterThrownEvent.FromPool();
			beforeAfterThrownEvent.Actor = Actor;
			beforeAfterThrownEvent.Item = Item;
			beforeAfterThrownEvent.ApparentTarget = ApparentTarget;
			return beforeAfterThrownEvent;
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x0016A0E4 File Offset: 0x001682E4
		public static bool Check(GameObject Actor, GameObject Item, GameObject ApparentTarget)
		{
			if (GameObject.Validate(ref Item) && Item.HasRegisteredEvent("BeforeAfterThrown"))
			{
				Event @event = Event.New("BeforeAfterThrown", 0, 0, 0);
				@event.SetParameter("Owner", Actor);
				@event.SetParameter("Object", Item);
				@event.SetParameter("ApparentTarget", ApparentTarget);
				if (!Item.FireEvent(@event))
				{
					return false;
				}
			}
			if (GameObject.Validate(ref Item) && Item.WantEvent(BeforeAfterThrownEvent.ID, MinEvent.CascadeLevel))
			{
				BeforeAfterThrownEvent beforeAfterThrownEvent = BeforeAfterThrownEvent.FromPool(Actor, Item, ApparentTarget);
				if (!Item.HandleEvent(beforeAfterThrownEvent))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002227 RID: 8743
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeAfterThrownEvent), null, new MinEvent.EventPoolCount(BeforeAfterThrownEvent.CountPool), new MinEvent.EventPoolReset(BeforeAfterThrownEvent.ResetPool));

		// Token: 0x04002228 RID: 8744
		private static List<BeforeAfterThrownEvent> Pool;

		// Token: 0x04002229 RID: 8745
		private static int PoolCounter;

		// Token: 0x0400222A RID: 8746
		public GameObject ApparentTarget;
	}
}
