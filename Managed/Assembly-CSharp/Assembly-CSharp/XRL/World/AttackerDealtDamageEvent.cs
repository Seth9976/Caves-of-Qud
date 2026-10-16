using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200067E RID: 1662
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AttackerDealtDamageEvent : IDamageEvent
	{
		// Token: 0x06004752 RID: 18258 RVA: 0x0016922F File Offset: 0x0016742F
		public AttackerDealtDamageEvent()
		{
			this.ID = AttackerDealtDamageEvent.ID;
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x00169242 File Offset: 0x00167442
		public static int CountPool()
		{
			if (AttackerDealtDamageEvent.Pool != null)
			{
				return AttackerDealtDamageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00169257 File Offset: 0x00167457
		public static void ResetPool()
		{
			while (AttackerDealtDamageEvent.PoolCounter > 0)
			{
				AttackerDealtDamageEvent.Pool[--AttackerDealtDamageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0016927F File Offset: 0x0016747F
		public static void ResetTo(ref AttackerDealtDamageEvent E)
		{
			MinEvent.ResetTo<AttackerDealtDamageEvent>(E, AttackerDealtDamageEvent.Pool, ref AttackerDealtDamageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x00169295 File Offset: 0x00167495
		public static AttackerDealtDamageEvent FromPool()
		{
			return MinEvent.FromPool<AttackerDealtDamageEvent>(ref AttackerDealtDamageEvent.Pool, ref AttackerDealtDamageEvent.PoolCounter, 8192);
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x001692AB File Offset: 0x001674AB
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x001692C0 File Offset: 0x001674C0
		public static void Send(Damage Damage, GameObject Object, GameObject Actor, GameObject Source = null, GameObject Weapon = null, GameObject Projectile = null, bool Indirect = false, Event ParentEvent = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AttackerDealtDamage"))
			{
				Event @event = Event.New("AttackerDealtDamage", 0, 0, 0);
				@event.SetParameter("Damage", Damage);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Owner", Actor);
				@event.SetParameter("Source", Source);
				@event.SetParameter("Weapon", Weapon);
				@event.SetParameter("Projectile", Projectile);
				@event.SetFlag("Indirect", Indirect);
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(@event);
				}
				flag = Actor.FireEvent(@event, ParentEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(@event);
				}
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AttackerDealtDamageEvent.ID, MinEvent.CascadeLevel))
			{
				AttackerDealtDamageEvent attackerDealtDamageEvent = AttackerDealtDamageEvent.FromPool();
				attackerDealtDamageEvent.Damage = Damage;
				attackerDealtDamageEvent.Object = Object;
				attackerDealtDamageEvent.Actor = Actor;
				attackerDealtDamageEvent.Source = Source;
				attackerDealtDamageEvent.Weapon = Weapon;
				attackerDealtDamageEvent.Projectile = Projectile;
				attackerDealtDamageEvent.Indirect = Indirect;
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(attackerDealtDamageEvent);
				}
				flag = Actor.HandleEvent(attackerDealtDamageEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(attackerDealtDamageEvent);
				}
			}
		}

		// Token: 0x04002202 RID: 8706
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AttackerDealtDamageEvent), null, new MinEvent.EventPoolCount(AttackerDealtDamageEvent.CountPool), new MinEvent.EventPoolReset(AttackerDealtDamageEvent.ResetPool));

		// Token: 0x04002203 RID: 8707
		private static List<AttackerDealtDamageEvent> Pool;

		// Token: 0x04002204 RID: 8708
		private static int PoolCounter;
	}
}
