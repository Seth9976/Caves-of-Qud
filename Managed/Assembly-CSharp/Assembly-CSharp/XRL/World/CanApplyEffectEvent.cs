using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006B6 RID: 1718
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanApplyEffectEvent : IEffectCheckEvent
	{
		// Token: 0x060048FB RID: 18683 RVA: 0x00170687 File Offset: 0x0016E887
		public CanApplyEffectEvent()
		{
			this.ID = CanApplyEffectEvent.ID;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x0017069A File Offset: 0x0016E89A
		public static int CountPool()
		{
			if (CanApplyEffectEvent.Pool != null)
			{
				return CanApplyEffectEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x001706AF File Offset: 0x0016E8AF
		public static void ResetPool()
		{
			while (CanApplyEffectEvent.PoolCounter > 0)
			{
				CanApplyEffectEvent.Pool[--CanApplyEffectEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x001706D7 File Offset: 0x0016E8D7
		public static void ResetTo(ref CanApplyEffectEvent E)
		{
			MinEvent.ResetTo<CanApplyEffectEvent>(E, CanApplyEffectEvent.Pool, ref CanApplyEffectEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x001706ED File Offset: 0x0016E8ED
		public static CanApplyEffectEvent FromPool()
		{
			return MinEvent.FromPool<CanApplyEffectEvent>(ref CanApplyEffectEvent.Pool, ref CanApplyEffectEvent.PoolCounter, 8192);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00170703 File Offset: 0x0016E903
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x00170717 File Offset: 0x0016E917
		public override void Reset()
		{
			base.Reset();
			this.Type = null;
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x00170728 File Offset: 0x0016E928
		public static bool Check<T>(GameObject Object, int Duration = 0)
		{
			CanApplyEffectEvent canApplyEffectEvent = CanApplyEffectEvent.FromPool();
			canApplyEffectEvent.Name = "T";
			canApplyEffectEvent.Type = typeof(T);
			canApplyEffectEvent.Duration = Duration;
			return Object.HandleEvent(canApplyEffectEvent);
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x00170764 File Offset: 0x0016E964
		public static bool Check(GameObject Object, string Name, int Duration = 0)
		{
			CanApplyEffectEvent canApplyEffectEvent = CanApplyEffectEvent.FromPool();
			canApplyEffectEvent.Name = Name;
			canApplyEffectEvent.Duration = Duration;
			return Object.HandleEvent(canApplyEffectEvent);
		}

		// Token: 0x04002304 RID: 8964
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(CanApplyEffectEvent), null, new MinEvent.EventPoolCount(CanApplyEffectEvent.CountPool), new MinEvent.EventPoolReset(CanApplyEffectEvent.ResetPool));

		// Token: 0x04002305 RID: 8965
		private static List<CanApplyEffectEvent> Pool;

		// Token: 0x04002306 RID: 8966
		private static int PoolCounter;

		// Token: 0x04002307 RID: 8967
		public Type Type;
	}
}
