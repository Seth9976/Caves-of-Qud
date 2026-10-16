using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007E8 RID: 2024
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class InductionChargeEvent : IInitialChargeProductionEvent
	{
		// Token: 0x06005155 RID: 20821 RVA: 0x001893C1 File Offset: 0x001875C1
		public InductionChargeEvent()
		{
			this.ID = InductionChargeEvent.ID;
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x001893D4 File Offset: 0x001875D4
		public static int CountPool()
		{
			if (InductionChargeEvent.Pool != null)
			{
				return InductionChargeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x001893E9 File Offset: 0x001875E9
		public static void ResetPool()
		{
			while (InductionChargeEvent.PoolCounter > 0)
			{
				InductionChargeEvent.Pool[--InductionChargeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x00189411 File Offset: 0x00187611
		public static void ResetTo(ref InductionChargeEvent E)
		{
			MinEvent.ResetTo<InductionChargeEvent>(E, InductionChargeEvent.Pool, ref InductionChargeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x00189427 File Offset: 0x00187627
		public static InductionChargeEvent FromPool()
		{
			return MinEvent.FromPool<InductionChargeEvent>(ref InductionChargeEvent.Pool, ref InductionChargeEvent.PoolCounter, 8192);
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x0018943D File Offset: 0x0018763D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x00189454 File Offset: 0x00187654
		public static InductionChargeEvent FromPool(IChargeEvent From)
		{
			InductionChargeEvent inductionChargeEvent = InductionChargeEvent.FromPool();
			inductionChargeEvent.Source = From.Source;
			inductionChargeEvent.Amount = From.Amount;
			inductionChargeEvent.StartingAmount = From.Amount;
			inductionChargeEvent.Multiple = From.Multiple;
			inductionChargeEvent.GridMask = From.GridMask;
			inductionChargeEvent.Forced = From.Forced;
			inductionChargeEvent.LiveOnly = From.LiveOnly;
			inductionChargeEvent.IncludeTransient = From.IncludeTransient;
			inductionChargeEvent.IncludeBiological = From.IncludeBiological;
			return inductionChargeEvent;
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x001894D4 File Offset: 0x001876D4
		public static int Send(GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool IncludeTransient = false, bool IncludeBiological = true)
		{
			if (InductionChargeEvent.Wanted(Object))
			{
				InductionChargeEvent inductionChargeEvent = InductionChargeEvent.FromPool();
				inductionChargeEvent.Source = Source;
				inductionChargeEvent.Amount = Amount;
				inductionChargeEvent.StartingAmount = Amount;
				inductionChargeEvent.Multiple = Multiple;
				inductionChargeEvent.GridMask = GridMask;
				inductionChargeEvent.Forced = Forced;
				inductionChargeEvent.LiveOnly = false;
				inductionChargeEvent.IncludeTransient = IncludeTransient;
				inductionChargeEvent.IncludeBiological = IncludeBiological;
				InductionChargeEvent.Process(Object, inductionChargeEvent);
				return inductionChargeEvent.Used;
			}
			return 0;
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x00189544 File Offset: 0x00187744
		public static int Send(out InductionChargeEvent E, GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool IncludeTransient = false, bool IncludeBiological = true)
		{
			if (InductionChargeEvent.Wanted(Object))
			{
				E = InductionChargeEvent.FromPool();
				E.Source = Source;
				E.Amount = Amount;
				E.StartingAmount = Amount;
				E.Multiple = Multiple;
				E.GridMask = GridMask;
				E.Forced = Forced;
				E.LiveOnly = false;
				E.IncludeTransient = IncludeTransient;
				E.IncludeBiological = IncludeBiological;
				InductionChargeEvent.Process(Object, E);
				return E.Used;
			}
			E = null;
			return 0;
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x001895C2 File Offset: 0x001877C2
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(InductionChargeEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("InductionCharge");
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x001895E3 File Offset: 0x001877E3
		public static bool Process(GameObject Object, InductionChargeEvent E)
		{
			return E.CheckRegisteredEvent(Object, "InductionCharge") && Object.HandleEvent(E);
		}

		// Token: 0x040028BC RID: 10428
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(InductionChargeEvent), null, new MinEvent.EventPoolCount(InductionChargeEvent.CountPool), new MinEvent.EventPoolReset(InductionChargeEvent.ResetPool));

		// Token: 0x040028BD RID: 10429
		private static List<InductionChargeEvent> Pool;

		// Token: 0x040028BE RID: 10430
		private static int PoolCounter;
	}
}
