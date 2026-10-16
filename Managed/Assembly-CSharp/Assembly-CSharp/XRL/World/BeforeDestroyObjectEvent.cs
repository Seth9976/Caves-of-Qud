using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200068E RID: 1678
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeDestroyObjectEvent : IDestroyObjectEvent
	{
		// Token: 0x060047D2 RID: 18386 RVA: 0x0016A973 File Offset: 0x00168B73
		public BeforeDestroyObjectEvent()
		{
			this.ID = BeforeDestroyObjectEvent.ID;
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0016A986 File Offset: 0x00168B86
		public static int CountPool()
		{
			if (BeforeDestroyObjectEvent.Pool != null)
			{
				return BeforeDestroyObjectEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x0016A99B File Offset: 0x00168B9B
		public static void ResetPool()
		{
			while (BeforeDestroyObjectEvent.PoolCounter > 0)
			{
				BeforeDestroyObjectEvent.Pool[--BeforeDestroyObjectEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x0016A9C3 File Offset: 0x00168BC3
		public static void ResetTo(ref BeforeDestroyObjectEvent E)
		{
			MinEvent.ResetTo<BeforeDestroyObjectEvent>(E, BeforeDestroyObjectEvent.Pool, ref BeforeDestroyObjectEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x0016A9D9 File Offset: 0x00168BD9
		public static BeforeDestroyObjectEvent FromPool()
		{
			return MinEvent.FromPool<BeforeDestroyObjectEvent>(ref BeforeDestroyObjectEvent.Pool, ref BeforeDestroyObjectEvent.PoolCounter, 8192);
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x0016A9EF File Offset: 0x00168BEF
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x0016AA03 File Offset: 0x00168C03
		public static BeforeDestroyObjectEvent FromPool(GameObject Object, bool Obliterate = false, bool Silent = false, string Reason = null, string ThirdPersonReason = null)
		{
			BeforeDestroyObjectEvent beforeDestroyObjectEvent = BeforeDestroyObjectEvent.FromPool();
			beforeDestroyObjectEvent.Object = Object;
			beforeDestroyObjectEvent.Obliterate = Obliterate;
			beforeDestroyObjectEvent.Silent = Silent;
			beforeDestroyObjectEvent.Reason = Reason;
			beforeDestroyObjectEvent.ThirdPersonReason = ThirdPersonReason;
			return beforeDestroyObjectEvent;
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x0016AA30 File Offset: 0x00168C30
		public static bool Check(GameObject Object, bool Obliterate = false, bool Silent = false, string Reason = null, string ThirdPersonReason = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeforeDestroyObject"))
				{
					Event @event = Event.New("BeforeDestroyObject", 0, 0, 0);
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
				MetricsManager.LogError("BeforeDestroyObject registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(ref Object) && Object.WantEvent(BeforeDestroyObjectEvent.ID, MinEvent.CascadeLevel))
				{
					BeforeDestroyObjectEvent beforeDestroyObjectEvent = BeforeDestroyObjectEvent.FromPool(Object, Obliterate, Silent, Reason, ThirdPersonReason);
					flag = Object.HandleEvent(beforeDestroyObjectEvent);
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("BeforeDestroyObject MinEvent handling", ex2);
			}
			return flag;
		}

		// Token: 0x0400223B RID: 8763
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeDestroyObjectEvent), null, new MinEvent.EventPoolCount(BeforeDestroyObjectEvent.CountPool), new MinEvent.EventPoolReset(BeforeDestroyObjectEvent.ResetPool));

		// Token: 0x0400223C RID: 8764
		private static List<BeforeDestroyObjectEvent> Pool;

		// Token: 0x0400223D RID: 8765
		private static int PoolCounter;
	}
}
