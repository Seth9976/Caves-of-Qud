using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000867 RID: 2151
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class TestChargeEvent : IChargeConsumptionEvent
	{
		// Token: 0x06005594 RID: 21908 RVA: 0x00198DAA File Offset: 0x00196FAA
		public TestChargeEvent()
		{
			this.ID = TestChargeEvent.ID;
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x00198DBD File Offset: 0x00196FBD
		public static int CountPool()
		{
			if (TestChargeEvent.Pool != null)
			{
				return TestChargeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x00198DD2 File Offset: 0x00196FD2
		public static void ResetPool()
		{
			while (TestChargeEvent.PoolCounter > 0)
			{
				TestChargeEvent.Pool[--TestChargeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x00198DFA File Offset: 0x00196FFA
		public static void ResetTo(ref TestChargeEvent E)
		{
			MinEvent.ResetTo<TestChargeEvent>(E, TestChargeEvent.Pool, ref TestChargeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x00198E10 File Offset: 0x00197010
		public static TestChargeEvent FromPool()
		{
			return MinEvent.FromPool<TestChargeEvent>(ref TestChargeEvent.Pool, ref TestChargeEvent.PoolCounter, 8192);
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x00198E26 File Offset: 0x00197026
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x00198E3C File Offset: 0x0019703C
		public static bool Check(GameObject Object, GameObject Source, int Amount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeTransient = true, bool IncludeBiological = true)
		{
			if (TestChargeEvent.Wanted(Object))
			{
				TestChargeEvent testChargeEvent = TestChargeEvent.FromPool();
				testChargeEvent.Source = Source;
				testChargeEvent.Amount = Amount;
				testChargeEvent.StartingAmount = Amount;
				testChargeEvent.Multiple = Multiple;
				testChargeEvent.GridMask = GridMask;
				testChargeEvent.Forced = Forced;
				testChargeEvent.LiveOnly = LiveOnly;
				testChargeEvent.IncludeTransient = IncludeTransient;
				testChargeEvent.IncludeBiological = IncludeBiological;
				return !TestChargeEvent.Process(Object, testChargeEvent);
			}
			return false;
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x00198EA7 File Offset: 0x001970A7
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(TestChargeEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("TestCharge");
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x00198EC8 File Offset: 0x001970C8
		public static bool Process(GameObject Object, TestChargeEvent E)
		{
			return E.CheckRegisteredEvent(Object, "TestCharge") && Object.HandleEvent(E);
		}

		// Token: 0x04002B21 RID: 11041
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(TestChargeEvent), null, new MinEvent.EventPoolCount(TestChargeEvent.CountPool), new MinEvent.EventPoolReset(TestChargeEvent.ResetPool));

		// Token: 0x04002B22 RID: 11042
		private static List<TestChargeEvent> Pool;

		// Token: 0x04002B23 RID: 11043
		private static int PoolCounter;
	}
}
