using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006FF RID: 1791
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EffectAppliedEvent : IActualEffectCheckEvent
	{
		// Token: 0x06004AE1 RID: 19169 RVA: 0x001756FF File Offset: 0x001738FF
		public EffectAppliedEvent()
		{
			this.ID = EffectAppliedEvent.ID;
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x00175712 File Offset: 0x00173912
		public static int CountPool()
		{
			if (EffectAppliedEvent.Pool != null)
			{
				return EffectAppliedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x00175727 File Offset: 0x00173927
		public static void ResetPool()
		{
			while (EffectAppliedEvent.PoolCounter > 0)
			{
				EffectAppliedEvent.Pool[--EffectAppliedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x0017574F File Offset: 0x0017394F
		public static void ResetTo(ref EffectAppliedEvent E)
		{
			MinEvent.ResetTo<EffectAppliedEvent>(E, EffectAppliedEvent.Pool, ref EffectAppliedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x00175765 File Offset: 0x00173965
		public static EffectAppliedEvent FromPool()
		{
			return MinEvent.FromPool<EffectAppliedEvent>(ref EffectAppliedEvent.Pool, ref EffectAppliedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x0017577B File Offset: 0x0017397B
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x0017578F File Offset: 0x0017398F
		public static EffectAppliedEvent FromPool(string Name, Effect Effect, GameObject Actor = null)
		{
			EffectAppliedEvent effectAppliedEvent = EffectAppliedEvent.FromPool();
			effectAppliedEvent.Name = Name;
			effectAppliedEvent.Effect = Effect;
			effectAppliedEvent.Actor = Actor;
			effectAppliedEvent.Duration = Effect.Duration;
			return effectAppliedEvent;
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x001757B8 File Offset: 0x001739B8
		public static void Send(GameObject obj, string Name, Effect Effect, GameObject Actor = null)
		{
			if (obj.WantEvent(EffectAppliedEvent.ID, IEffectCheckEvent.CascadeLevel))
			{
				obj.HandleEvent(EffectAppliedEvent.FromPool(Name, Effect, Actor));
			}
			if (obj.HasRegisteredEvent("EffectApplied"))
			{
				obj.FireEvent(Event.New("EffectApplied", "Effect", Effect));
			}
			ComponentReflection.ExtensionRecord extensionRecord;
			if (ComponentReflection.EffectExtensions.TryGetValue(Effect.GetType(), out extensionRecord))
			{
				extensionRecord.Add(obj, Effect, true, false);
			}
		}

		// Token: 0x0400242A RID: 9258
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EffectAppliedEvent), null, new MinEvent.EventPoolCount(EffectAppliedEvent.CountPool), new MinEvent.EventPoolReset(EffectAppliedEvent.ResetPool));

		// Token: 0x0400242B RID: 9259
		private static List<EffectAppliedEvent> Pool;

		// Token: 0x0400242C RID: 9260
		private static int PoolCounter;
	}
}
