using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006A4 RID: 1700
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeTookDamageEvent : IDamageEvent
	{
		// Token: 0x0600486E RID: 18542 RVA: 0x0016C4F0 File Offset: 0x0016A6F0
		public BeforeTookDamageEvent()
		{
			this.ID = BeforeTookDamageEvent.ID;
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x0016C503 File Offset: 0x0016A703
		public static int CountPool()
		{
			if (BeforeTookDamageEvent.Pool != null)
			{
				return BeforeTookDamageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x0016C518 File Offset: 0x0016A718
		public static void ResetPool()
		{
			while (BeforeTookDamageEvent.PoolCounter > 0)
			{
				BeforeTookDamageEvent.Pool[--BeforeTookDamageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x0016C540 File Offset: 0x0016A740
		public static void ResetTo(ref BeforeTookDamageEvent E)
		{
			MinEvent.ResetTo<BeforeTookDamageEvent>(E, BeforeTookDamageEvent.Pool, ref BeforeTookDamageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x0016C556 File Offset: 0x0016A756
		public static BeforeTookDamageEvent FromPool()
		{
			return MinEvent.FromPool<BeforeTookDamageEvent>(ref BeforeTookDamageEvent.Pool, ref BeforeTookDamageEvent.PoolCounter, 8192);
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x0016C56C File Offset: 0x0016A76C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x0016C580 File Offset: 0x0016A780
		public static void Send(Damage Damage, GameObject Object, GameObject Actor, GameObject Source = null, GameObject Weapon = null, GameObject Projectile = null, bool Indirect = false, Event ParentEvent = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeforeTookDamage"))
			{
				Event @event = Event.New("BeforeTookDamage", 0, 0, 0);
				@event.SetParameter("Damage", Damage);
				@event.SetParameter("Defender", Object);
				@event.SetParameter("Owner", Actor);
				@event.SetParameter("Source", Source);
				@event.SetParameter("Weapon", Weapon);
				@event.SetParameter("Projectile", Projectile);
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
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(BeforeTookDamageEvent.ID, MinEvent.CascadeLevel))
			{
				BeforeTookDamageEvent beforeTookDamageEvent = BeforeTookDamageEvent.FromPool();
				beforeTookDamageEvent.Damage = Damage;
				beforeTookDamageEvent.Object = Object;
				beforeTookDamageEvent.Actor = Actor;
				beforeTookDamageEvent.Source = Source;
				beforeTookDamageEvent.Weapon = Weapon;
				beforeTookDamageEvent.Projectile = Projectile;
				beforeTookDamageEvent.Indirect = Indirect;
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(beforeTookDamageEvent);
				}
				flag = Object.HandleEvent(beforeTookDamageEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(beforeTookDamageEvent);
				}
			}
		}

		// Token: 0x040022AE RID: 8878
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeTookDamageEvent), null, new MinEvent.EventPoolCount(BeforeTookDamageEvent.CountPool), new MinEvent.EventPoolReset(BeforeTookDamageEvent.ResetPool));

		// Token: 0x040022AF RID: 8879
		private static List<BeforeTookDamageEvent> Pool;

		// Token: 0x040022B0 RID: 8880
		private static int PoolCounter;
	}
}
