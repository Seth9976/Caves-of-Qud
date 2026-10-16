using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000689 RID: 1673
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeApplyDamageEvent : IDamageEvent
	{
		// Token: 0x060047AB RID: 18347 RVA: 0x0016A1A6 File Offset: 0x001683A6
		public BeforeApplyDamageEvent()
		{
			this.ID = BeforeApplyDamageEvent.ID;
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x0016A1B9 File Offset: 0x001683B9
		public override int GetCascadeLevel()
		{
			return BeforeApplyDamageEvent.CascadeLevel;
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x0016A1C0 File Offset: 0x001683C0
		public static int CountPool()
		{
			if (BeforeApplyDamageEvent.Pool != null)
			{
				return BeforeApplyDamageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x0016A1D5 File Offset: 0x001683D5
		public static void ResetPool()
		{
			while (BeforeApplyDamageEvent.PoolCounter > 0)
			{
				BeforeApplyDamageEvent.Pool[--BeforeApplyDamageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x0016A1FD File Offset: 0x001683FD
		public static void ResetTo(ref BeforeApplyDamageEvent E)
		{
			MinEvent.ResetTo<BeforeApplyDamageEvent>(E, BeforeApplyDamageEvent.Pool, ref BeforeApplyDamageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x0016A213 File Offset: 0x00168413
		public static BeforeApplyDamageEvent FromPool()
		{
			return MinEvent.FromPool<BeforeApplyDamageEvent>(ref BeforeApplyDamageEvent.Pool, ref BeforeApplyDamageEvent.PoolCounter, 8192);
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0016A229 File Offset: 0x00168429
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x0016A240 File Offset: 0x00168440
		public static bool Check(Damage Damage, GameObject Object, GameObject Actor, GameObject Source = null, GameObject Weapon = null, GameObject Projectile = null, bool Indirect = false, Event ParentEvent = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeforeApplyDamage"))
			{
				Event @event = Event.New("BeforeApplyDamage", 0, 0, 0);
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
				flag = Object.FireEvent(@event, ParentEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(@event);
				}
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(BeforeApplyDamageEvent.ID, BeforeApplyDamageEvent.CascadeLevel))
			{
				BeforeApplyDamageEvent beforeApplyDamageEvent = BeforeApplyDamageEvent.FromPool();
				beforeApplyDamageEvent.Damage = Damage;
				beforeApplyDamageEvent.Object = Object;
				beforeApplyDamageEvent.Actor = Actor;
				beforeApplyDamageEvent.Source = Source;
				beforeApplyDamageEvent.Weapon = Weapon;
				beforeApplyDamageEvent.Projectile = Projectile;
				beforeApplyDamageEvent.Indirect = Indirect;
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(beforeApplyDamageEvent);
				}
				flag = Object.HandleEvent(beforeApplyDamageEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(beforeApplyDamageEvent);
				}
			}
			return flag;
		}

		// Token: 0x0400222B RID: 8747
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeApplyDamageEvent), null, new MinEvent.EventPoolCount(BeforeApplyDamageEvent.CountPool), new MinEvent.EventPoolReset(BeforeApplyDamageEvent.ResetPool));

		// Token: 0x0400222C RID: 8748
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400222D RID: 8749
		private static List<BeforeApplyDamageEvent> Pool;

		// Token: 0x0400222E RID: 8750
		private static int PoolCounter;
	}
}
