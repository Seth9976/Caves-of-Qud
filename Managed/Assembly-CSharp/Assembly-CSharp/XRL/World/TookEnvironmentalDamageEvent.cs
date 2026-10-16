using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200086A RID: 2154
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class TookEnvironmentalDamageEvent : IDamageEvent
	{
		// Token: 0x060055AB RID: 21931 RVA: 0x001991DE File Offset: 0x001973DE
		public TookEnvironmentalDamageEvent()
		{
			this.ID = TookEnvironmentalDamageEvent.ID;
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x001991F1 File Offset: 0x001973F1
		public static int CountPool()
		{
			if (TookEnvironmentalDamageEvent.Pool != null)
			{
				return TookEnvironmentalDamageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x00199206 File Offset: 0x00197406
		public static void ResetPool()
		{
			while (TookEnvironmentalDamageEvent.PoolCounter > 0)
			{
				TookEnvironmentalDamageEvent.Pool[--TookEnvironmentalDamageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x0019922E File Offset: 0x0019742E
		public static void ResetTo(ref TookEnvironmentalDamageEvent E)
		{
			MinEvent.ResetTo<TookEnvironmentalDamageEvent>(E, TookEnvironmentalDamageEvent.Pool, ref TookEnvironmentalDamageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x00199244 File Offset: 0x00197444
		public static TookEnvironmentalDamageEvent FromPool()
		{
			return MinEvent.FromPool<TookEnvironmentalDamageEvent>(ref TookEnvironmentalDamageEvent.Pool, ref TookEnvironmentalDamageEvent.PoolCounter, 8192);
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x0019925A File Offset: 0x0019745A
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x00199270 File Offset: 0x00197470
		public static void Send(Damage Damage, GameObject Object, GameObject Actor, GameObject Source = null, bool Indirect = false, Event ParentEvent = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("TookEnvironmentalDamage"))
			{
				Event @event = Event.New("TookEnvironmentalDamage", 0, 0, 0);
				@event.SetParameter("Damage", Damage);
				@event.SetParameter("Defender", Object);
				@event.SetParameter("Owner", Actor);
				@event.SetParameter("Source", Source);
				@event.SetFlag("Indirect", Indirect);
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(@event);
				}
				flag = Object.FireEvent(@event, ParentEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(@event);
				}
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(TookEnvironmentalDamageEvent.ID, MinEvent.CascadeLevel))
			{
				TookEnvironmentalDamageEvent tookEnvironmentalDamageEvent = TookEnvironmentalDamageEvent.FromPool();
				tookEnvironmentalDamageEvent.Damage = Damage;
				tookEnvironmentalDamageEvent.Object = Object;
				tookEnvironmentalDamageEvent.Actor = Actor;
				tookEnvironmentalDamageEvent.Source = Source;
				tookEnvironmentalDamageEvent.Indirect = Indirect;
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(tookEnvironmentalDamageEvent);
				}
				flag = Object.HandleEvent(tookEnvironmentalDamageEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(tookEnvironmentalDamageEvent);
				}
			}
		}

		// Token: 0x04002B2A RID: 11050
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(TookEnvironmentalDamageEvent), null, new MinEvent.EventPoolCount(TookEnvironmentalDamageEvent.CountPool), new MinEvent.EventPoolReset(TookEnvironmentalDamageEvent.ResetPool));

		// Token: 0x04002B2B RID: 11051
		private static List<TookEnvironmentalDamageEvent> Pool;

		// Token: 0x04002B2C RID: 11052
		private static int PoolCounter;
	}
}
