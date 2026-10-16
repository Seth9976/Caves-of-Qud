using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000876 RID: 2166
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class UsingChargeEvent : IChargeConsumptionEvent
	{
		// Token: 0x06005602 RID: 22018 RVA: 0x0019A076 File Offset: 0x00198276
		public UsingChargeEvent()
		{
			this.ID = UsingChargeEvent.ID;
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x0019A089 File Offset: 0x00198289
		public static int CountPool()
		{
			if (UsingChargeEvent.Pool != null)
			{
				return UsingChargeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x0019A09E File Offset: 0x0019829E
		public static void ResetPool()
		{
			while (UsingChargeEvent.PoolCounter > 0)
			{
				UsingChargeEvent.Pool[--UsingChargeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x0019A0C6 File Offset: 0x001982C6
		public static void ResetTo(ref UsingChargeEvent E)
		{
			MinEvent.ResetTo<UsingChargeEvent>(E, UsingChargeEvent.Pool, ref UsingChargeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x0019A0DC File Offset: 0x001982DC
		public static UsingChargeEvent FromPool()
		{
			return MinEvent.FromPool<UsingChargeEvent>(ref UsingChargeEvent.Pool, ref UsingChargeEvent.PoolCounter, 8192);
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x0019A0F2 File Offset: 0x001982F2
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x0019A108 File Offset: 0x00198308
		public static void Send(GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeTransient = true, bool IncludeBiological = true, int PowerLoadLevel = 100)
		{
			if (UsingChargeEvent.Wanted(Object))
			{
				UsingChargeEvent usingChargeEvent = UsingChargeEvent.FromPool();
				usingChargeEvent.Source = Source;
				usingChargeEvent.Amount = Amount;
				usingChargeEvent.StartingAmount = Amount;
				usingChargeEvent.Multiple = Multiple;
				usingChargeEvent.GridMask = GridMask;
				usingChargeEvent.Forced = Forced;
				usingChargeEvent.LiveOnly = LiveOnly;
				usingChargeEvent.IncludeTransient = IncludeTransient;
				usingChargeEvent.IncludeBiological = IncludeBiological;
				usingChargeEvent.PowerLoadLevel = new int?(PowerLoadLevel);
				UsingChargeEvent.Process(Object, usingChargeEvent);
			}
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x0019A17B File Offset: 0x0019837B
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(UsingChargeEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("UsingCharge");
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x0019A19C File Offset: 0x0019839C
		public static void Process(GameObject Object, UsingChargeEvent E)
		{
			if (E.CheckRegisteredEvent(Object, "UsingCharge"))
			{
				Object.HandleEvent(E);
			}
		}

		// Token: 0x04002B56 RID: 11094
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(UsingChargeEvent), null, new MinEvent.EventPoolCount(UsingChargeEvent.CountPool), new MinEvent.EventPoolReset(UsingChargeEvent.ResetPool));

		// Token: 0x04002B57 RID: 11095
		private static List<UsingChargeEvent> Pool;

		// Token: 0x04002B58 RID: 11096
		private static int PoolCounter;
	}
}
