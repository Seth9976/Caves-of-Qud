using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000878 RID: 2168
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Pool)]
	public class WantsLiquidCollectionEvent : ILiquidEvent
	{
		// Token: 0x06005611 RID: 22033 RVA: 0x0019A27E File Offset: 0x0019847E
		public WantsLiquidCollectionEvent()
		{
			this.ID = WantsLiquidCollectionEvent.ID;
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x0019A291 File Offset: 0x00198491
		public override int GetCascadeLevel()
		{
			return WantsLiquidCollectionEvent.CascadeLevel;
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x0019A298 File Offset: 0x00198498
		public static int CountPool()
		{
			if (WantsLiquidCollectionEvent.Pool != null)
			{
				return WantsLiquidCollectionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x0019A2AD File Offset: 0x001984AD
		public static void ResetPool()
		{
			while (WantsLiquidCollectionEvent.PoolCounter > 0)
			{
				WantsLiquidCollectionEvent.Pool[--WantsLiquidCollectionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x0019A2D5 File Offset: 0x001984D5
		public static void ResetTo(ref WantsLiquidCollectionEvent E)
		{
			MinEvent.ResetTo<WantsLiquidCollectionEvent>(E, WantsLiquidCollectionEvent.Pool, ref WantsLiquidCollectionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x0019A2EB File Offset: 0x001984EB
		public static WantsLiquidCollectionEvent FromPool()
		{
			return MinEvent.FromPool<WantsLiquidCollectionEvent>(ref WantsLiquidCollectionEvent.Pool, ref WantsLiquidCollectionEvent.PoolCounter, 8192);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x0019A301 File Offset: 0x00198501
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x0019A318 File Offset: 0x00198518
		public static WantsLiquidCollectionEvent FromPool(GameObject Actor, string Liquid = "water")
		{
			WantsLiquidCollectionEvent wantsLiquidCollectionEvent = WantsLiquidCollectionEvent.FromPool();
			wantsLiquidCollectionEvent.Actor = Actor;
			wantsLiquidCollectionEvent.Liquid = Liquid;
			wantsLiquidCollectionEvent.LiquidVolume = null;
			wantsLiquidCollectionEvent.Drams = 0;
			wantsLiquidCollectionEvent.Skip = null;
			wantsLiquidCollectionEvent.SkipList = null;
			wantsLiquidCollectionEvent.Filter = null;
			wantsLiquidCollectionEvent.Auto = false;
			wantsLiquidCollectionEvent.ImpureOkay = false;
			wantsLiquidCollectionEvent.SafeOnly = false;
			return wantsLiquidCollectionEvent;
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x0019A370 File Offset: 0x00198570
		public static bool Check(GameObject Object, GameObject Actor, string Liquid = "water")
		{
			return Object.Understood() && Object.WantEvent(WantsLiquidCollectionEvent.ID, WantsLiquidCollectionEvent.CascadeLevel) && !Object.HandleEvent(WantsLiquidCollectionEvent.FromPool(Actor, Liquid));
		}

		// Token: 0x04002B5B RID: 11099
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(WantsLiquidCollectionEvent), null, new MinEvent.EventPoolCount(WantsLiquidCollectionEvent.CountPool), new MinEvent.EventPoolReset(WantsLiquidCollectionEvent.ResetPool));

		// Token: 0x04002B5C RID: 11100
		public new static readonly int CascadeLevel = 0;

		// Token: 0x04002B5D RID: 11101
		private static List<WantsLiquidCollectionEvent> Pool;

		// Token: 0x04002B5E RID: 11102
		private static int PoolCounter;
	}
}
