using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000873 RID: 2163
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class UseDramsEvent : ILiquidEvent
	{
		// Token: 0x060055EF RID: 21999 RVA: 0x00199DE7 File Offset: 0x00197FE7
		public UseDramsEvent()
		{
			this.ID = UseDramsEvent.ID;
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x00199DFA File Offset: 0x00197FFA
		public static int CountPool()
		{
			if (UseDramsEvent.Pool != null)
			{
				return UseDramsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x00199E0F File Offset: 0x0019800F
		public static void ResetPool()
		{
			while (UseDramsEvent.PoolCounter > 0)
			{
				UseDramsEvent.Pool[--UseDramsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x00199E37 File Offset: 0x00198037
		public static void ResetTo(ref UseDramsEvent E)
		{
			MinEvent.ResetTo<UseDramsEvent>(E, UseDramsEvent.Pool, ref UseDramsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00199E4D File Offset: 0x0019804D
		public static UseDramsEvent FromPool()
		{
			return MinEvent.FromPool<UseDramsEvent>(ref UseDramsEvent.Pool, ref UseDramsEvent.PoolCounter, 8192);
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x00199E63 File Offset: 0x00198063
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00199E77 File Offset: 0x00198077
		public override void Reset()
		{
			base.Reset();
			this.TrackContainers = null;
			this.Drinking = false;
			this.Pass = 0;
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x00199E94 File Offset: 0x00198094
		public static UseDramsEvent FromPool(GameObject Actor, string Liquid = "water", int Drams = 1, GameObject Skip = null, List<GameObject> SkipList = null, Predicate<GameObject> Filter = null, bool ImpureOkay = false, List<GameObject> TrackContainers = null, bool Drinking = false)
		{
			UseDramsEvent useDramsEvent = UseDramsEvent.FromPool();
			useDramsEvent.Actor = Actor;
			useDramsEvent.Liquid = Liquid;
			useDramsEvent.LiquidVolume = null;
			useDramsEvent.Drams = Drams;
			useDramsEvent.Skip = Skip;
			useDramsEvent.SkipList = SkipList;
			useDramsEvent.Filter = Filter;
			useDramsEvent.Auto = false;
			useDramsEvent.ImpureOkay = ImpureOkay;
			useDramsEvent.SafeOnly = false;
			useDramsEvent.TrackContainers = TrackContainers;
			useDramsEvent.Drinking = Drinking;
			useDramsEvent.Pass = 0;
			return useDramsEvent;
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x00199F08 File Offset: 0x00198108
		public static bool Check(GameObject Actor, string Liquid = "water", int Drams = 1, GameObject Skip = null, List<GameObject> SkipList = null, Predicate<GameObject> Filter = null, bool ImpureOkay = false, List<GameObject> TrackContainers = null, bool Drinking = false)
		{
			if (Actor.WantEvent(UseDramsEvent.ID, ILiquidEvent.CascadeLevel))
			{
				UseDramsEvent useDramsEvent = UseDramsEvent.FromPool(Actor, Liquid, Drams, Skip, SkipList, Filter, ImpureOkay, TrackContainers, Drinking);
				for (int i = 1; i <= 3; i++)
				{
					useDramsEvent.Pass = i;
					if (!Actor.HandleEvent(useDramsEvent))
					{
						return false;
					}
					if (useDramsEvent.Drams <= 0)
					{
						break;
					}
				}
			}
			return true;
		}

		// Token: 0x04002B49 RID: 11081
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(UseDramsEvent), null, new MinEvent.EventPoolCount(UseDramsEvent.CountPool), new MinEvent.EventPoolReset(UseDramsEvent.ResetPool));

		// Token: 0x04002B4A RID: 11082
		private static List<UseDramsEvent> Pool;

		// Token: 0x04002B4B RID: 11083
		private static int PoolCounter;

		// Token: 0x04002B4C RID: 11084
		public const int PASSES = 3;

		// Token: 0x04002B4D RID: 11085
		public List<GameObject> TrackContainers;

		// Token: 0x04002B4E RID: 11086
		public bool Drinking;

		// Token: 0x04002B4F RID: 11087
		public int Pass;
	}
}
