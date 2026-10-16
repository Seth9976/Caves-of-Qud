using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000658 RID: 1624
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterConsumeEvent : IConsumeEvent
	{
		// Token: 0x0600463D RID: 17981 RVA: 0x0016603E File Offset: 0x0016423E
		public AfterConsumeEvent()
		{
			this.ID = AfterConsumeEvent.ID;
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x00166051 File Offset: 0x00164251
		public static int CountPool()
		{
			if (AfterConsumeEvent.Pool != null)
			{
				return AfterConsumeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x00166066 File Offset: 0x00164266
		public static void ResetPool()
		{
			while (AfterConsumeEvent.PoolCounter > 0)
			{
				AfterConsumeEvent.Pool[--AfterConsumeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x0016608E File Offset: 0x0016428E
		public static void ResetTo(ref AfterConsumeEvent E)
		{
			MinEvent.ResetTo<AfterConsumeEvent>(E, AfterConsumeEvent.Pool, ref AfterConsumeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x001660A4 File Offset: 0x001642A4
		public static AfterConsumeEvent FromPool()
		{
			return MinEvent.FromPool<AfterConsumeEvent>(ref AfterConsumeEvent.Pool, ref AfterConsumeEvent.PoolCounter, 8192);
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x001660BA File Offset: 0x001642BA
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x001660D0 File Offset: 0x001642D0
		public static void Send(GameObject Actor, GameObject Subject, GameObject Object, bool Eat = false, bool Drink = false, bool Inject = false, bool Inhale = false, bool Absorb = false, bool Voluntary = true)
		{
			AfterConsumeEvent afterConsumeEvent = AfterConsumeEvent.FromPool();
			afterConsumeEvent.Actor = Actor;
			afterConsumeEvent.Subject = Subject;
			afterConsumeEvent.Object = Object;
			afterConsumeEvent.Eat = Eat;
			afterConsumeEvent.Drink = Drink;
			afterConsumeEvent.Inject = Inject;
			afterConsumeEvent.Inhale = Inhale;
			afterConsumeEvent.Absorb = Absorb;
			afterConsumeEvent.Voluntary = Voluntary;
			IConsumeEvent.DispatchAll<AfterConsumeEvent>(afterConsumeEvent);
			AfterConsumeEvent.ResetTo(ref afterConsumeEvent);
		}

		// Token: 0x0400215B RID: 8539
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterConsumeEvent), null, new MinEvent.EventPoolCount(AfterConsumeEvent.CountPool), new MinEvent.EventPoolReset(AfterConsumeEvent.ResetPool));

		// Token: 0x0400215C RID: 8540
		private static List<AfterConsumeEvent> Pool;

		// Token: 0x0400215D RID: 8541
		private static int PoolCounter;
	}
}
