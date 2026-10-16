using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200068B RID: 1675
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeConsumeEvent : IConsumeEvent
	{
		// Token: 0x060047BA RID: 18362 RVA: 0x0016A44D File Offset: 0x0016864D
		public BeforeConsumeEvent()
		{
			this.ID = BeforeConsumeEvent.ID;
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x0016A460 File Offset: 0x00168660
		public static int CountPool()
		{
			if (BeforeConsumeEvent.Pool != null)
			{
				return BeforeConsumeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x0016A475 File Offset: 0x00168675
		public static void ResetPool()
		{
			while (BeforeConsumeEvent.PoolCounter > 0)
			{
				BeforeConsumeEvent.Pool[--BeforeConsumeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x0016A49D File Offset: 0x0016869D
		public static void ResetTo(ref BeforeConsumeEvent E)
		{
			MinEvent.ResetTo<BeforeConsumeEvent>(E, BeforeConsumeEvent.Pool, ref BeforeConsumeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x0016A4B3 File Offset: 0x001686B3
		public static BeforeConsumeEvent FromPool()
		{
			return MinEvent.FromPool<BeforeConsumeEvent>(ref BeforeConsumeEvent.Pool, ref BeforeConsumeEvent.PoolCounter, 8192);
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x0016A4C9 File Offset: 0x001686C9
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x0016A4E0 File Offset: 0x001686E0
		public static bool Check(GameObject Actor, GameObject Subject, GameObject Object, bool Eat = false, bool Drink = false, bool Inject = false, bool Inhale = false, bool Absorb = false, bool Voluntary = true)
		{
			BeforeConsumeEvent beforeConsumeEvent = BeforeConsumeEvent.FromPool();
			beforeConsumeEvent.Actor = Actor;
			beforeConsumeEvent.Subject = Subject;
			beforeConsumeEvent.Object = Object;
			beforeConsumeEvent.Eat = Eat;
			beforeConsumeEvent.Drink = Drink;
			beforeConsumeEvent.Inject = Inject;
			beforeConsumeEvent.Inhale = Inhale;
			beforeConsumeEvent.Absorb = Absorb;
			beforeConsumeEvent.Voluntary = Voluntary;
			bool flag = IConsumeEvent.DispatchAll<BeforeConsumeEvent>(beforeConsumeEvent);
			BeforeConsumeEvent.ResetTo(ref beforeConsumeEvent);
			return flag;
		}

		// Token: 0x04002232 RID: 8754
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeConsumeEvent), null, new MinEvent.EventPoolCount(BeforeConsumeEvent.CountPool), new MinEvent.EventPoolReset(BeforeConsumeEvent.ResetPool));

		// Token: 0x04002233 RID: 8755
		private static List<BeforeConsumeEvent> Pool;

		// Token: 0x04002234 RID: 8756
		private static int PoolCounter;
	}
}
