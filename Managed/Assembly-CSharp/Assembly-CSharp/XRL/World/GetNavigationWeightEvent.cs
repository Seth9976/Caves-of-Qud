using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000770 RID: 1904
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetNavigationWeightEvent : INavigationWeightEvent
	{
		// Token: 0x06004E57 RID: 20055 RVA: 0x001801CD File Offset: 0x0017E3CD
		public GetNavigationWeightEvent()
		{
			this.ID = GetNavigationWeightEvent.ID;
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x001801E0 File Offset: 0x0017E3E0
		public static int CountPool()
		{
			if (GetNavigationWeightEvent.Pool != null)
			{
				return GetNavigationWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x001801F5 File Offset: 0x0017E3F5
		public static void ResetPool()
		{
			while (GetNavigationWeightEvent.PoolCounter > 0)
			{
				GetNavigationWeightEvent.Pool[--GetNavigationWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x0018021D File Offset: 0x0017E41D
		public static void ResetTo(ref GetNavigationWeightEvent E)
		{
			MinEvent.ResetTo<GetNavigationWeightEvent>(E, GetNavigationWeightEvent.Pool, ref GetNavigationWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x00180233 File Offset: 0x0017E433
		public static GetNavigationWeightEvent FromPool()
		{
			return MinEvent.FromPool<GetNavigationWeightEvent>(ref GetNavigationWeightEvent.Pool, ref GetNavigationWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x00180249 File Offset: 0x0017E449
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x00180260 File Offset: 0x0017E460
		public static GetNavigationWeightEvent HandleFor(Cell Cell, GameObject Actor, bool Uncacheable, int Weight = 0, int Nav = 0)
		{
			GetNavigationWeightEvent getNavigationWeightEvent = GetNavigationWeightEvent.FromPool();
			getNavigationWeightEvent.Actor = Actor;
			getNavigationWeightEvent.Cell = Cell;
			getNavigationWeightEvent.Weight = Weight;
			getNavigationWeightEvent.Nav = Nav;
			getNavigationWeightEvent.Uncacheable = Uncacheable;
			if (Weight >= 100 || Cell == null)
			{
				return getNavigationWeightEvent;
			}
			GameObject[] array = Cell.Objects.GetArray();
			int i = 0;
			int count = Cell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = array[i];
				int num;
				if (gameObject.IntProperty.TryGetValue("NavigationWeight", out num) && num > Weight)
				{
					getNavigationWeightEvent.Weight = num;
				}
				getNavigationWeightEvent.Object = gameObject;
				getNavigationWeightEvent.PriorWeight = Weight;
				gameObject.HandleEvent(getNavigationWeightEvent);
				Weight = getNavigationWeightEvent.Weight;
				if (Weight >= 100)
				{
					break;
				}
				i++;
			}
			return getNavigationWeightEvent;
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x00180314 File Offset: 0x0017E514
		public static int GetFor(Cell Cell, GameObject Actor, ref bool Uncacheable, int Weight = 0, int Nav = 0)
		{
			if (Weight >= 100)
			{
				return Weight;
			}
			if (Cell != null)
			{
				GetNavigationWeightEvent getNavigationWeightEvent = null;
				int i = 0;
				int count = Cell.Objects.Count;
				while (i < count)
				{
					int num = Weight;
					GameObject gameObject = Cell.Objects[i];
					int intProperty = gameObject.GetIntProperty("NavigationWeight", 0);
					if (intProperty > Weight)
					{
						Weight = intProperty;
					}
					if (gameObject.WantEvent(GetNavigationWeightEvent.ID, MinEvent.CascadeLevel))
					{
						if (getNavigationWeightEvent == null)
						{
							getNavigationWeightEvent = GetNavigationWeightEvent.FromPool();
							getNavigationWeightEvent.Cell = Cell;
							getNavigationWeightEvent.Actor = Actor;
							getNavigationWeightEvent.Uncacheable = Uncacheable;
							getNavigationWeightEvent.Weight = 0;
							getNavigationWeightEvent.PriorWeight = 0;
							getNavigationWeightEvent.Nav = Nav;
						}
						getNavigationWeightEvent.Object = gameObject;
						getNavigationWeightEvent.Weight = Weight;
						getNavigationWeightEvent.PriorWeight = num;
						gameObject.HandleEvent(getNavigationWeightEvent);
						Weight = getNavigationWeightEvent.Weight;
						Uncacheable = getNavigationWeightEvent.Uncacheable;
					}
					if (Weight >= 100)
					{
						return Weight;
					}
					i++;
				}
			}
			return Weight;
		}

		// Token: 0x04002640 RID: 9792
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetNavigationWeightEvent), null, new MinEvent.EventPoolCount(GetNavigationWeightEvent.CountPool), new MinEvent.EventPoolReset(GetNavigationWeightEvent.ResetPool));

		// Token: 0x04002641 RID: 9793
		private static List<GetNavigationWeightEvent> Pool;

		// Token: 0x04002642 RID: 9794
		private static int PoolCounter;
	}
}
