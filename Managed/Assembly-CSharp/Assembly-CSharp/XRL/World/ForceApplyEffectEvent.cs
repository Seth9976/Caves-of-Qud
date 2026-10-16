using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000718 RID: 1816
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ForceApplyEffectEvent : IActualEffectCheckEvent
	{
		// Token: 0x06004BA5 RID: 19365 RVA: 0x001776F7 File Offset: 0x001758F7
		public ForceApplyEffectEvent()
		{
			this.ID = ForceApplyEffectEvent.ID;
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0017770A File Offset: 0x0017590A
		public static int CountPool()
		{
			if (ForceApplyEffectEvent.Pool != null)
			{
				return ForceApplyEffectEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0017771F File Offset: 0x0017591F
		public static void ResetPool()
		{
			while (ForceApplyEffectEvent.PoolCounter > 0)
			{
				ForceApplyEffectEvent.Pool[--ForceApplyEffectEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x00177747 File Offset: 0x00175947
		public static void ResetTo(ref ForceApplyEffectEvent E)
		{
			MinEvent.ResetTo<ForceApplyEffectEvent>(E, ForceApplyEffectEvent.Pool, ref ForceApplyEffectEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x0017775D File Offset: 0x0017595D
		public static ForceApplyEffectEvent FromPool()
		{
			return MinEvent.FromPool<ForceApplyEffectEvent>(ref ForceApplyEffectEvent.Pool, ref ForceApplyEffectEvent.PoolCounter, 8192);
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x00177773 File Offset: 0x00175973
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x00177787 File Offset: 0x00175987
		public static ForceApplyEffectEvent FromPool(string Name, Effect Effect, GameObject Actor = null)
		{
			ForceApplyEffectEvent forceApplyEffectEvent = ForceApplyEffectEvent.FromPool();
			forceApplyEffectEvent.Name = Name;
			forceApplyEffectEvent.Effect = Effect;
			forceApplyEffectEvent.Actor = Actor;
			forceApplyEffectEvent.Duration = Effect.Duration;
			return forceApplyEffectEvent;
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x001777B0 File Offset: 0x001759B0
		public static bool Check(GameObject obj, string Name, Effect Effect, GameObject Actor = null)
		{
			return Effect.CanBeAppliedTo(obj) && (!obj.WantEvent(ForceApplyEffectEvent.ID, IEffectCheckEvent.CascadeLevel) || obj.HandleEvent(ForceApplyEffectEvent.FromPool(Name, Effect, Actor))) && (!obj.HasRegisteredEvent("ForceApplyEffect") || obj.FireEvent(Event.New("ForceApplyEffect", "Object", obj, "Effect", Effect, "Owner", Actor)));
		}

		// Token: 0x0400247D RID: 9341
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ForceApplyEffectEvent), null, new MinEvent.EventPoolCount(ForceApplyEffectEvent.CountPool), new MinEvent.EventPoolReset(ForceApplyEffectEvent.ResetPool));

		// Token: 0x0400247E RID: 9342
		private static List<ForceApplyEffectEvent> Pool;

		// Token: 0x0400247F RID: 9343
		private static int PoolCounter;
	}
}
