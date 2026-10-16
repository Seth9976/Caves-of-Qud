using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200067D RID: 1661
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AttackerDealingDamageEvent : IDamageEvent
	{
		// Token: 0x0600474A RID: 18250 RVA: 0x00169035 File Offset: 0x00167235
		public AttackerDealingDamageEvent()
		{
			this.ID = AttackerDealingDamageEvent.ID;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00169048 File Offset: 0x00167248
		public static int CountPool()
		{
			if (AttackerDealingDamageEvent.Pool != null)
			{
				return AttackerDealingDamageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x0016905D File Offset: 0x0016725D
		public static void ResetPool()
		{
			while (AttackerDealingDamageEvent.PoolCounter > 0)
			{
				AttackerDealingDamageEvent.Pool[--AttackerDealingDamageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x00169085 File Offset: 0x00167285
		public static void ResetTo(ref AttackerDealingDamageEvent E)
		{
			MinEvent.ResetTo<AttackerDealingDamageEvent>(E, AttackerDealingDamageEvent.Pool, ref AttackerDealingDamageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x0016909B File Offset: 0x0016729B
		public static AttackerDealingDamageEvent FromPool()
		{
			return MinEvent.FromPool<AttackerDealingDamageEvent>(ref AttackerDealingDamageEvent.Pool, ref AttackerDealingDamageEvent.PoolCounter, 8192);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x001690B1 File Offset: 0x001672B1
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x001690C8 File Offset: 0x001672C8
		public static bool Check(Damage Damage, GameObject Object, GameObject Actor, GameObject Source = null, GameObject Weapon = null, GameObject Projectile = null, bool Indirect = false, Event ParentEvent = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("AttackerDealingDamage"))
			{
				Event @event = Event.New("AttackerDealingDamage", 0, 0, 0);
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
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(AttackerDealingDamageEvent.ID, MinEvent.CascadeLevel))
			{
				AttackerDealingDamageEvent attackerDealingDamageEvent = AttackerDealingDamageEvent.FromPool();
				attackerDealingDamageEvent.Damage = Damage;
				attackerDealingDamageEvent.Object = Object;
				attackerDealingDamageEvent.Actor = Actor;
				attackerDealingDamageEvent.Source = Source;
				attackerDealingDamageEvent.Weapon = Weapon;
				attackerDealingDamageEvent.Projectile = Projectile;
				attackerDealingDamageEvent.Indirect = Indirect;
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(attackerDealingDamageEvent);
				}
				flag = Actor.HandleEvent(attackerDealingDamageEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(attackerDealingDamageEvent);
				}
			}
			return flag;
		}

		// Token: 0x040021FF RID: 8703
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AttackerDealingDamageEvent), null, new MinEvent.EventPoolCount(AttackerDealingDamageEvent.CountPool), new MinEvent.EventPoolReset(AttackerDealingDamageEvent.ResetPool));

		// Token: 0x04002200 RID: 8704
		private static List<AttackerDealingDamageEvent> Pool;

		// Token: 0x04002201 RID: 8705
		private static int PoolCounter;
	}
}
