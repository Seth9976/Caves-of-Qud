using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007FC RID: 2044
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class LateBeforeApplyDamageEvent : IDamageEvent
	{
		// Token: 0x060051D7 RID: 20951 RVA: 0x0018AF7F File Offset: 0x0018917F
		public LateBeforeApplyDamageEvent()
		{
			this.ID = LateBeforeApplyDamageEvent.ID;
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x0018AF92 File Offset: 0x00189192
		public static int CountPool()
		{
			if (LateBeforeApplyDamageEvent.Pool != null)
			{
				return LateBeforeApplyDamageEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x0018AFA7 File Offset: 0x001891A7
		public static void ResetPool()
		{
			while (LateBeforeApplyDamageEvent.PoolCounter > 0)
			{
				LateBeforeApplyDamageEvent.Pool[--LateBeforeApplyDamageEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x0018AFCF File Offset: 0x001891CF
		public static void ResetTo(ref LateBeforeApplyDamageEvent E)
		{
			MinEvent.ResetTo<LateBeforeApplyDamageEvent>(E, LateBeforeApplyDamageEvent.Pool, ref LateBeforeApplyDamageEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0018AFE5 File Offset: 0x001891E5
		public static LateBeforeApplyDamageEvent FromPool()
		{
			return MinEvent.FromPool<LateBeforeApplyDamageEvent>(ref LateBeforeApplyDamageEvent.Pool, ref LateBeforeApplyDamageEvent.PoolCounter, 8192);
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0018AFFB File Offset: 0x001891FB
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0018B010 File Offset: 0x00189210
		public static bool Check(Damage Damage, GameObject Object, GameObject Actor, GameObject Source = null, GameObject Weapon = null, GameObject Projectile = null, bool Indirect = false, Event ParentEvent = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("LateBeforeApplyDamage"))
			{
				Event @event = Event.New("LateBeforeApplyDamage", 0, 0, 0);
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
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(LateBeforeApplyDamageEvent.ID, MinEvent.CascadeLevel))
			{
				LateBeforeApplyDamageEvent lateBeforeApplyDamageEvent = LateBeforeApplyDamageEvent.FromPool();
				lateBeforeApplyDamageEvent.Damage = Damage;
				lateBeforeApplyDamageEvent.Object = Object;
				lateBeforeApplyDamageEvent.Actor = Actor;
				lateBeforeApplyDamageEvent.Source = Source;
				lateBeforeApplyDamageEvent.Weapon = Weapon;
				lateBeforeApplyDamageEvent.Projectile = Projectile;
				lateBeforeApplyDamageEvent.Indirect = Indirect;
				if (ParentEvent != null)
				{
					ParentEvent.PreprocessChildEvent(lateBeforeApplyDamageEvent);
				}
				flag = Object.HandleEvent(lateBeforeApplyDamageEvent);
				if (ParentEvent != null)
				{
					ParentEvent.ProcessChildEvent(lateBeforeApplyDamageEvent);
				}
			}
			return flag;
		}

		// Token: 0x04002916 RID: 10518
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(LateBeforeApplyDamageEvent), null, new MinEvent.EventPoolCount(LateBeforeApplyDamageEvent.CountPool), new MinEvent.EventPoolReset(LateBeforeApplyDamageEvent.ResetPool));

		// Token: 0x04002917 RID: 10519
		private static List<LateBeforeApplyDamageEvent> Pool;

		// Token: 0x04002918 RID: 10520
		private static int PoolCounter;
	}
}
