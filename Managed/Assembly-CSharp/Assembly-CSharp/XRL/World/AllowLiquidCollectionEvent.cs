using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000675 RID: 1653
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Pool)]
	public class AllowLiquidCollectionEvent : ILiquidEvent
	{
		// Token: 0x0600470E RID: 18190 RVA: 0x001684C3 File Offset: 0x001666C3
		public AllowLiquidCollectionEvent()
		{
			this.ID = AllowLiquidCollectionEvent.ID;
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x001684D6 File Offset: 0x001666D6
		public override int GetCascadeLevel()
		{
			return AllowLiquidCollectionEvent.CascadeLevel;
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x001684DD File Offset: 0x001666DD
		public static int CountPool()
		{
			if (AllowLiquidCollectionEvent.Pool != null)
			{
				return AllowLiquidCollectionEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x001684F2 File Offset: 0x001666F2
		public static void ResetPool()
		{
			while (AllowLiquidCollectionEvent.PoolCounter > 0)
			{
				AllowLiquidCollectionEvent.Pool[--AllowLiquidCollectionEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x0016851A File Offset: 0x0016671A
		public static void ResetTo(ref AllowLiquidCollectionEvent E)
		{
			MinEvent.ResetTo<AllowLiquidCollectionEvent>(E, AllowLiquidCollectionEvent.Pool, ref AllowLiquidCollectionEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x00168530 File Offset: 0x00166730
		public static AllowLiquidCollectionEvent FromPool()
		{
			return MinEvent.FromPool<AllowLiquidCollectionEvent>(ref AllowLiquidCollectionEvent.Pool, ref AllowLiquidCollectionEvent.PoolCounter, 8192);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00168546 File Offset: 0x00166746
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x0016855C File Offset: 0x0016675C
		public static AllowLiquidCollectionEvent FromPool(GameObject Actor, string Liquid = "water")
		{
			AllowLiquidCollectionEvent allowLiquidCollectionEvent = AllowLiquidCollectionEvent.FromPool();
			allowLiquidCollectionEvent.Actor = Actor;
			allowLiquidCollectionEvent.Liquid = Liquid;
			allowLiquidCollectionEvent.LiquidVolume = null;
			allowLiquidCollectionEvent.Drams = 0;
			allowLiquidCollectionEvent.Skip = null;
			allowLiquidCollectionEvent.SkipList = null;
			allowLiquidCollectionEvent.Filter = null;
			allowLiquidCollectionEvent.Auto = false;
			allowLiquidCollectionEvent.ImpureOkay = false;
			allowLiquidCollectionEvent.SafeOnly = false;
			return allowLiquidCollectionEvent;
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x001685B4 File Offset: 0x001667B4
		public static bool Check(GameObject Object, GameObject Actor, string Liquid = "water")
		{
			return Object.Understood() && (!Object.WantEvent(AllowLiquidCollectionEvent.ID, AllowLiquidCollectionEvent.CascadeLevel) || Object.HandleEvent(AllowLiquidCollectionEvent.FromPool(Actor, Liquid)));
		}

		// Token: 0x040021DB RID: 8667
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AllowLiquidCollectionEvent), null, new MinEvent.EventPoolCount(AllowLiquidCollectionEvent.CountPool), new MinEvent.EventPoolReset(AllowLiquidCollectionEvent.ResetPool));

		// Token: 0x040021DC RID: 8668
		public new static readonly int CascadeLevel = 0;

		// Token: 0x040021DD RID: 8669
		private static List<AllowLiquidCollectionEvent> Pool;

		// Token: 0x040021DE RID: 8670
		private static int PoolCounter;
	}
}
