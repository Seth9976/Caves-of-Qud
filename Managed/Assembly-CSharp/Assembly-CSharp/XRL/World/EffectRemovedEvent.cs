using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000701 RID: 1793
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EffectRemovedEvent : IActualEffectCheckEvent
	{
		// Token: 0x06004AF3 RID: 19187 RVA: 0x00175995 File Offset: 0x00173B95
		public EffectRemovedEvent()
		{
			this.ID = EffectRemovedEvent.ID;
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x001759A8 File Offset: 0x00173BA8
		public static int CountPool()
		{
			if (EffectRemovedEvent.Pool != null)
			{
				return EffectRemovedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x001759BD File Offset: 0x00173BBD
		public static void ResetPool()
		{
			while (EffectRemovedEvent.PoolCounter > 0)
			{
				EffectRemovedEvent.Pool[--EffectRemovedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x001759E5 File Offset: 0x00173BE5
		public static void ResetTo(ref EffectRemovedEvent E)
		{
			MinEvent.ResetTo<EffectRemovedEvent>(E, EffectRemovedEvent.Pool, ref EffectRemovedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x001759FB File Offset: 0x00173BFB
		public static EffectRemovedEvent FromPool()
		{
			return MinEvent.FromPool<EffectRemovedEvent>(ref EffectRemovedEvent.Pool, ref EffectRemovedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00175A11 File Offset: 0x00173C11
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x00175A25 File Offset: 0x00173C25
		public static EffectRemovedEvent FromPool(string Name, Effect Effect, GameObject Actor = null)
		{
			EffectRemovedEvent effectRemovedEvent = EffectRemovedEvent.FromPool();
			effectRemovedEvent.Name = Name;
			effectRemovedEvent.Effect = Effect;
			effectRemovedEvent.Actor = Actor;
			effectRemovedEvent.Duration = Effect.Duration;
			return effectRemovedEvent;
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x00175A50 File Offset: 0x00173C50
		public static void Send(GameObject obj, string Name, Effect Effect, GameObject Actor = null)
		{
			EffectRemovedEvent effectRemovedEvent = EffectRemovedEvent.FromPool();
			effectRemovedEvent.Name = Name;
			effectRemovedEvent.Effect = Effect;
			effectRemovedEvent.Actor = Actor;
			effectRemovedEvent.Duration = Effect.Duration;
			obj.HandleEvent(effectRemovedEvent);
		}

		// Token: 0x04002430 RID: 9264
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EffectRemovedEvent), null, new MinEvent.EventPoolCount(EffectRemovedEvent.CountPool), new MinEvent.EventPoolReset(EffectRemovedEvent.ResetPool));

		// Token: 0x04002431 RID: 9265
		private static List<EffectRemovedEvent> Pool;

		// Token: 0x04002432 RID: 9266
		private static int PoolCounter;
	}
}
