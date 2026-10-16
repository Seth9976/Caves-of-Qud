using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020007AE RID: 1966
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GiveDramsEvent : ILiquidEvent
	{
		// Token: 0x06004FF5 RID: 20469 RVA: 0x00185A08 File Offset: 0x00183C08
		public GiveDramsEvent()
		{
			this.ID = GiveDramsEvent.ID;
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x00185A26 File Offset: 0x00183C26
		public static int CountPool()
		{
			if (GiveDramsEvent.Pool != null)
			{
				return GiveDramsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x00185A3B File Offset: 0x00183C3B
		public static void ResetPool()
		{
			while (GiveDramsEvent.PoolCounter > 0)
			{
				GiveDramsEvent.Pool[--GiveDramsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x00185A63 File Offset: 0x00183C63
		public static void ResetTo(ref GiveDramsEvent E)
		{
			MinEvent.ResetTo<GiveDramsEvent>(E, GiveDramsEvent.Pool, ref GiveDramsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x00185A79 File Offset: 0x00183C79
		public static GiveDramsEvent FromPool()
		{
			return MinEvent.FromPool<GiveDramsEvent>(ref GiveDramsEvent.Pool, ref GiveDramsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x00185A8F File Offset: 0x00183C8F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x00185AA3 File Offset: 0x00183CA3
		public override void Reset()
		{
			base.Reset();
			this.Pass = 0;
			this.StoredIn.Clear();
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x00185AC0 File Offset: 0x00183CC0
		public static GiveDramsEvent FromPool(GameObject Actor, string Liquid = "water", int Drams = 1, GameObject Skip = null, List<GameObject> SkipList = null, Predicate<GameObject> Filter = null, bool Auto = false, bool SafeOnly = true, LiquidVolume LiquidVolume = null)
		{
			GiveDramsEvent giveDramsEvent = GiveDramsEvent.FromPool();
			giveDramsEvent.Actor = Actor;
			giveDramsEvent.Liquid = Liquid;
			giveDramsEvent.LiquidVolume = LiquidVolume;
			giveDramsEvent.Drams = Drams;
			giveDramsEvent.Skip = Skip;
			giveDramsEvent.SkipList = SkipList;
			giveDramsEvent.Filter = Filter;
			giveDramsEvent.Auto = Auto;
			giveDramsEvent.ImpureOkay = false;
			giveDramsEvent.SafeOnly = SafeOnly;
			giveDramsEvent.Pass = 0;
			giveDramsEvent.StoredIn.Clear();
			return giveDramsEvent;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x00185B30 File Offset: 0x00183D30
		public static bool Check(GameObject Actor, string Liquid = "water", int Drams = 1, GameObject Skip = null, List<GameObject> SkipList = null, Predicate<GameObject> Filter = null, bool Auto = false, List<GameObject> StoredIn = null, bool SafeOnly = true, LiquidVolume LiquidVolume = null)
		{
			bool flag = true;
			if (Actor.WantEvent(GiveDramsEvent.ID, ILiquidEvent.CascadeLevel))
			{
				GiveDramsEvent giveDramsEvent = GiveDramsEvent.FromPool(Actor, Liquid, Drams, Skip, SkipList, Filter, Auto, SafeOnly, LiquidVolume);
				for (int i = 1; i <= 5; i++)
				{
					giveDramsEvent.Pass = i;
					if (!Actor.HandleEvent(giveDramsEvent))
					{
						flag = false;
						break;
					}
					if (giveDramsEvent.Drams <= 0)
					{
						break;
					}
				}
				if (StoredIn != null)
				{
					StoredIn.AddRange(giveDramsEvent.StoredIn);
				}
			}
			return flag;
		}

		// Token: 0x04002796 RID: 10134
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GiveDramsEvent), null, new MinEvent.EventPoolCount(GiveDramsEvent.CountPool), new MinEvent.EventPoolReset(GiveDramsEvent.ResetPool));

		// Token: 0x04002797 RID: 10135
		private static List<GiveDramsEvent> Pool;

		// Token: 0x04002798 RID: 10136
		private static int PoolCounter;

		// Token: 0x04002799 RID: 10137
		public const int PASSES = 5;

		// Token: 0x0400279A RID: 10138
		public int Pass;

		// Token: 0x0400279B RID: 10139
		public List<GameObject> StoredIn = new List<GameObject>();
	}
}
