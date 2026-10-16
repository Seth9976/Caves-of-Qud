using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000869 RID: 2153
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class TookDamageEvent : IDamageEvent
	{
		// Token: 0x060055A2 RID: 21922 RVA: 0x00198FCA File Offset: 0x001971CA
		public TookDamageEvent()
		{
			this.ID = TookDamageEvent.ID;
		}

		// Token: 0x060055A3 RID: 21923 RVA: 0x00198FDD File Offset: 0x001971DD
		public override int GetCascadeLevel()
		{
			return TookDamageEvent.CascadeLevel;
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x00198FE4 File Offset: 0x001971E4
		public static int CountPool()
		{
			if (TookDamageEvent.Pool != null)
			{
				return TookDamageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x00198FF9 File Offset: 0x001971F9
		public static void ResetPool()
		{
			while (TookDamageEvent.PoolCounter > 0)
			{
				TookDamageEvent.Pool[--TookDamageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x00199021 File Offset: 0x00197221
		public static void ResetTo(ref TookDamageEvent E)
		{
			MinEvent.ResetTo<TookDamageEvent>(E, TookDamageEvent.Pool, ref TookDamageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x00199037 File Offset: 0x00197237
		public static TookDamageEvent FromPool()
		{
			return MinEvent.FromPool<TookDamageEvent>(ref TookDamageEvent.Pool, ref TookDamageEvent.PoolCounter, 8192);
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x0019904D File Offset: 0x0019724D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x00199064 File Offset: 0x00197264
		public static void Send(Damage Damage, GameObject Object, GameObject Actor, GameObject Source = null, GameObject Weapon = null, GameObject Projectile = null, bool Indirect = false, Event ParentEvent = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("TookDamage"))
			{
				Event @event = Event.New("TookDamage", 0, 0, 0);
				@event.SetParameter("Damage", Damage);
				@event.SetParameter("Defender", Object);
				@event.SetParameter("Owner", Actor);
				@event.SetParameter("Attacker", Actor);
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
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(TookDamageEvent.ID, TookDamageEvent.CascadeLevel))
			{
				TookDamageEvent tookDamageEvent = TookDamageEvent.FromPool();
				tookDamageEvent.Damage = Damage;
				tookDamageEvent.Object = Object;
				tookDamageEvent.Actor = Actor;
				tookDamageEvent.Source = Source;
				tookDamageEvent.Weapon = Weapon;
				tookDamageEvent.Projectile = Projectile;
				tookDamageEvent.Indirect = Indirect;
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(tookDamageEvent);
				}
				flag = Object.HandleEvent(tookDamageEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(tookDamageEvent);
				}
			}
		}

		// Token: 0x04002B26 RID: 11046
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(TookDamageEvent), null, new MinEvent.EventPoolCount(TookDamageEvent.CountPool), new MinEvent.EventPoolReset(TookDamageEvent.ResetPool));

		// Token: 0x04002B27 RID: 11047
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002B28 RID: 11048
		private static List<TookDamageEvent> Pool;

		// Token: 0x04002B29 RID: 11049
		private static int PoolCounter;
	}
}
