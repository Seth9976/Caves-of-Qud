using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000700 RID: 1792
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EffectForceAppliedEvent : IActualEffectCheckEvent
	{
		// Token: 0x06004AEA RID: 19178 RVA: 0x0017585C File Offset: 0x00173A5C
		public EffectForceAppliedEvent()
		{
			this.ID = EffectForceAppliedEvent.ID;
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x0017586F File Offset: 0x00173A6F
		public static int CountPool()
		{
			if (EffectForceAppliedEvent.Pool != null)
			{
				return EffectForceAppliedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x00175884 File Offset: 0x00173A84
		public static void ResetPool()
		{
			while (EffectForceAppliedEvent.PoolCounter > 0)
			{
				EffectForceAppliedEvent.Pool[--EffectForceAppliedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x001758AC File Offset: 0x00173AAC
		public static void ResetTo(ref EffectForceAppliedEvent E)
		{
			MinEvent.ResetTo<EffectForceAppliedEvent>(E, EffectForceAppliedEvent.Pool, ref EffectForceAppliedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x001758C2 File Offset: 0x00173AC2
		public static EffectForceAppliedEvent FromPool()
		{
			return MinEvent.FromPool<EffectForceAppliedEvent>(ref EffectForceAppliedEvent.Pool, ref EffectForceAppliedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x001758D8 File Offset: 0x00173AD8
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x001758EC File Offset: 0x00173AEC
		public static EffectForceAppliedEvent FromPool(string Name, Effect Effect, GameObject Actor = null)
		{
			EffectForceAppliedEvent effectForceAppliedEvent = EffectForceAppliedEvent.FromPool();
			effectForceAppliedEvent.Name = Name;
			effectForceAppliedEvent.Effect = Effect;
			effectForceAppliedEvent.Actor = Actor;
			effectForceAppliedEvent.Duration = Effect.Duration;
			return effectForceAppliedEvent;
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00175914 File Offset: 0x00173B14
		public static void Send(GameObject obj, string Name, Effect Effect, GameObject Actor = null)
		{
			if (obj.WantEvent(EffectForceAppliedEvent.ID, IEffectCheckEvent.CascadeLevel))
			{
				obj.HandleEvent(EffectForceAppliedEvent.FromPool(Name, Effect, Actor));
			}
			if (obj.HasRegisteredEvent("EffectForceApplied"))
			{
				obj.FireEvent(Event.New("EffectForceApplied", "Effect", Effect));
			}
		}

		// Token: 0x0400242D RID: 9261
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EffectForceAppliedEvent), null, new MinEvent.EventPoolCount(EffectForceAppliedEvent.CountPool), new MinEvent.EventPoolReset(EffectForceAppliedEvent.ResetPool));

		// Token: 0x0400242E RID: 9262
		private static List<EffectForceAppliedEvent> Pool;

		// Token: 0x0400242F RID: 9263
		private static int PoolCounter;
	}
}
