using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200081A RID: 2074
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class OnDestroyObjectEvent : IDestroyObjectEvent
	{
		// Token: 0x060052AE RID: 21166 RVA: 0x0018D8CB File Offset: 0x0018BACB
		public OnDestroyObjectEvent()
		{
			this.ID = OnDestroyObjectEvent.ID;
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x0018D8DE File Offset: 0x0018BADE
		public static int CountPool()
		{
			if (OnDestroyObjectEvent.Pool != null)
			{
				return OnDestroyObjectEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x0018D8F3 File Offset: 0x0018BAF3
		public static void ResetPool()
		{
			while (OnDestroyObjectEvent.PoolCounter > 0)
			{
				OnDestroyObjectEvent.Pool[--OnDestroyObjectEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0018D91B File Offset: 0x0018BB1B
		public static void ResetTo(ref OnDestroyObjectEvent E)
		{
			MinEvent.ResetTo<OnDestroyObjectEvent>(E, OnDestroyObjectEvent.Pool, ref OnDestroyObjectEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0018D931 File Offset: 0x0018BB31
		public static OnDestroyObjectEvent FromPool()
		{
			return MinEvent.FromPool<OnDestroyObjectEvent>(ref OnDestroyObjectEvent.Pool, ref OnDestroyObjectEvent.PoolCounter, 8192);
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x0018D947 File Offset: 0x0018BB47
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x0018D95B File Offset: 0x0018BB5B
		public static OnDestroyObjectEvent FromPool(GameObject Object, bool Obliterate = false, bool Silent = false, string Reason = null, string ThirdPersonReason = null)
		{
			OnDestroyObjectEvent onDestroyObjectEvent = OnDestroyObjectEvent.FromPool();
			onDestroyObjectEvent.Object = Object;
			onDestroyObjectEvent.Obliterate = Obliterate;
			onDestroyObjectEvent.Silent = Silent;
			onDestroyObjectEvent.Reason = Reason;
			onDestroyObjectEvent.ThirdPersonReason = ThirdPersonReason;
			return onDestroyObjectEvent;
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0018D988 File Offset: 0x0018BB88
		public static void Send(GameObject Object, bool Obliterate = false, bool Silent = false, string Reason = null, string ThirdPersonReason = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(Object) && Object.HasRegisteredEvent("OnDestroyObject"))
				{
					Event @event = Event.New("OnDestroyObject", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetFlag("Obliterate", Obliterate);
					@event.SetSilent(Silent);
					@event.SetParameter("Reason", Reason);
					@event.SetParameter("ThirdPersonReason", ThirdPersonReason);
					flag = Object.FireEvent(@event);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("OnDestroyObject registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(Object) && Object.WantEvent(OnDestroyObjectEvent.ID, MinEvent.CascadeLevel))
				{
					OnDestroyObjectEvent onDestroyObjectEvent = OnDestroyObjectEvent.FromPool(Object, Obliterate, Silent, Reason, ThirdPersonReason);
					flag = Object.HandleEvent(onDestroyObjectEvent);
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("OnDestroyObject MinEvent handling", ex2);
			}
		}

		// Token: 0x0400298C RID: 10636
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(OnDestroyObjectEvent), null, new MinEvent.EventPoolCount(OnDestroyObjectEvent.CountPool), new MinEvent.EventPoolReset(OnDestroyObjectEvent.ResetPool));

		// Token: 0x0400298D RID: 10637
		private static List<OnDestroyObjectEvent> Pool;

		// Token: 0x0400298E RID: 10638
		private static int PoolCounter;
	}
}
