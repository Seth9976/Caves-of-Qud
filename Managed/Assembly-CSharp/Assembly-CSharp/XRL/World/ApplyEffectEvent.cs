using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200067B RID: 1659
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ApplyEffectEvent : IActualEffectCheckEvent
	{
		// Token: 0x0600473B RID: 18235 RVA: 0x00168D08 File Offset: 0x00166F08
		public ApplyEffectEvent()
		{
			this.ID = ApplyEffectEvent.ID;
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x00168D1B File Offset: 0x00166F1B
		public static int CountPool()
		{
			if (ApplyEffectEvent.Pool != null)
			{
				return ApplyEffectEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x00168D30 File Offset: 0x00166F30
		public static void ResetPool()
		{
			while (ApplyEffectEvent.PoolCounter > 0)
			{
				ApplyEffectEvent.Pool[--ApplyEffectEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x00168D58 File Offset: 0x00166F58
		public static void ResetTo(ref ApplyEffectEvent E)
		{
			MinEvent.ResetTo<ApplyEffectEvent>(E, ApplyEffectEvent.Pool, ref ApplyEffectEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00168D6E File Offset: 0x00166F6E
		public static ApplyEffectEvent FromPool()
		{
			return MinEvent.FromPool<ApplyEffectEvent>(ref ApplyEffectEvent.Pool, ref ApplyEffectEvent.PoolCounter, 8192);
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x00168D84 File Offset: 0x00166F84
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x00168D98 File Offset: 0x00166F98
		public static ApplyEffectEvent FromPool(string Name, Effect Effect, GameObject Actor = null)
		{
			ApplyEffectEvent applyEffectEvent = ApplyEffectEvent.FromPool();
			applyEffectEvent.Name = Name;
			applyEffectEvent.Effect = Effect;
			applyEffectEvent.Actor = Actor;
			applyEffectEvent.Duration = Effect.Duration;
			return applyEffectEvent;
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x00168DC0 File Offset: 0x00166FC0
		public static bool Check(GameObject obj, string Name, Effect Effect, GameObject Actor = null)
		{
			return Effect.CanBeAppliedTo(obj) && (!obj.WantEvent(ApplyEffectEvent.ID, IEffectCheckEvent.CascadeLevel) || obj.HandleEvent(ApplyEffectEvent.FromPool(Name, Effect, Actor))) && (!obj.HasRegisteredEvent("ApplyEffect") || obj.FireEvent(Event.New("ApplyEffect", "Object", obj, "Effect", Effect, "Owner", Actor)));
		}

		// Token: 0x040021F2 RID: 8690
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ApplyEffectEvent), null, new MinEvent.EventPoolCount(ApplyEffectEvent.CountPool), new MinEvent.EventPoolReset(ApplyEffectEvent.ResetPool));

		// Token: 0x040021F3 RID: 8691
		private static List<ApplyEffectEvent> Pool;

		// Token: 0x040021F4 RID: 8692
		private static int PoolCounter;
	}
}
