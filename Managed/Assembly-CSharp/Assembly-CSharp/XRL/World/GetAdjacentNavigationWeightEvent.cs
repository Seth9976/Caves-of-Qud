using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000724 RID: 1828
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetAdjacentNavigationWeightEvent : IAdjacentNavigationWeightEvent
	{
		// Token: 0x06004BEC RID: 19436 RVA: 0x00178316 File Offset: 0x00176516
		public GetAdjacentNavigationWeightEvent()
		{
			this.ID = GetAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x00178329 File Offset: 0x00176529
		public static int CountPool()
		{
			if (GetAdjacentNavigationWeightEvent.Pool != null)
			{
				return GetAdjacentNavigationWeightEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0017833E File Offset: 0x0017653E
		public static void ResetPool()
		{
			while (GetAdjacentNavigationWeightEvent.PoolCounter > 0)
			{
				GetAdjacentNavigationWeightEvent.Pool[--GetAdjacentNavigationWeightEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x00178366 File Offset: 0x00176566
		public static void ResetTo(ref GetAdjacentNavigationWeightEvent E)
		{
			MinEvent.ResetTo<GetAdjacentNavigationWeightEvent>(E, GetAdjacentNavigationWeightEvent.Pool, ref GetAdjacentNavigationWeightEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x0017837C File Offset: 0x0017657C
		public static GetAdjacentNavigationWeightEvent FromPool()
		{
			return MinEvent.FromPool<GetAdjacentNavigationWeightEvent>(ref GetAdjacentNavigationWeightEvent.Pool, ref GetAdjacentNavigationWeightEvent.PoolCounter, 8192);
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x00178392 File Offset: 0x00176592
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x001783A8 File Offset: 0x001765A8
		public static void Process(INavigationWeightEvent Parent)
		{
			int num = Parent.Weight;
			if (num >= 100 || Parent.Cell == null)
			{
				return;
			}
			GetAdjacentNavigationWeightEvent getAdjacentNavigationWeightEvent = GetAdjacentNavigationWeightEvent.FromPool();
			Parent.ApplyTo(getAdjacentNavigationWeightEvent);
			List<Cell> localAdjacentCells = getAdjacentNavigationWeightEvent.Cell.GetLocalAdjacentCells();
			int i = 0;
			int count = localAdjacentCells.Count;
			while (i < count)
			{
				Cell cell = localAdjacentCells[i];
				GameObject[] array = cell.Objects.GetArray();
				int j = 0;
				int count2 = cell.Objects.Count;
				while (j < count2)
				{
					GameObject gameObject = array[j];
					int num2;
					if (gameObject.IntProperty.TryGetValue("AdjacentNavigationWeight", out num2) && num2 > num)
					{
						getAdjacentNavigationWeightEvent.Weight = num2;
					}
					getAdjacentNavigationWeightEvent.Object = gameObject;
					getAdjacentNavigationWeightEvent.PriorWeight = num;
					getAdjacentNavigationWeightEvent.AdjacentCell = cell;
					gameObject.HandleEvent(getAdjacentNavigationWeightEvent);
					num = getAdjacentNavigationWeightEvent.Weight;
					if (num >= 100)
					{
						i = count;
						break;
					}
					j++;
				}
				i++;
			}
			getAdjacentNavigationWeightEvent.ApplyTo(Parent);
			GetAdjacentNavigationWeightEvent.ResetTo(ref getAdjacentNavigationWeightEvent);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x0017849C File Offset: 0x0017669C
		public static int GetFor(Cell Cell, GameObject Actor, ref bool Uncacheable, int Weight = 0, int Nav = 0)
		{
			if (Weight >= 100)
			{
				return Weight;
			}
			if (Cell != null)
			{
				GetAdjacentNavigationWeightEvent getAdjacentNavigationWeightEvent = null;
				List<Cell> localAdjacentCells = Cell.GetLocalAdjacentCells();
				int i = 0;
				int count = localAdjacentCells.Count;
				while (i < count)
				{
					Cell cell = localAdjacentCells[i];
					int j = 0;
					int count2 = cell.Objects.Count;
					while (j < count2)
					{
						int num = Weight;
						GameObject gameObject = cell.Objects[j];
						int intProperty = gameObject.GetIntProperty("AdjacentNavigationWeight", 0);
						if (intProperty > Weight)
						{
							Weight = intProperty;
						}
						if (gameObject.WantEvent(GetAdjacentNavigationWeightEvent.ID, MinEvent.CascadeLevel))
						{
							if (getAdjacentNavigationWeightEvent == null)
							{
								getAdjacentNavigationWeightEvent = GetAdjacentNavigationWeightEvent.FromPool();
								getAdjacentNavigationWeightEvent.Cell = Cell;
								getAdjacentNavigationWeightEvent.Actor = Actor;
								getAdjacentNavigationWeightEvent.Uncacheable = Uncacheable;
								getAdjacentNavigationWeightEvent.Weight = 0;
								getAdjacentNavigationWeightEvent.PriorWeight = 0;
								getAdjacentNavigationWeightEvent.Nav = Nav;
							}
							getAdjacentNavigationWeightEvent.Object = gameObject;
							getAdjacentNavigationWeightEvent.Weight = Weight;
							getAdjacentNavigationWeightEvent.PriorWeight = num;
							getAdjacentNavigationWeightEvent.AdjacentCell = cell;
							gameObject.HandleEvent(getAdjacentNavigationWeightEvent);
							Weight = getAdjacentNavigationWeightEvent.Weight;
							Uncacheable = getAdjacentNavigationWeightEvent.Uncacheable;
						}
						if (Weight >= 100)
						{
							return Weight;
						}
						j++;
					}
					i++;
				}
			}
			return Weight;
		}

		// Token: 0x040024B9 RID: 9401
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetAdjacentNavigationWeightEvent), null, new MinEvent.EventPoolCount(GetAdjacentNavigationWeightEvent.CountPool), new MinEvent.EventPoolReset(GetAdjacentNavigationWeightEvent.ResetPool));

		// Token: 0x040024BA RID: 9402
		private static List<GetAdjacentNavigationWeightEvent> Pool;

		// Token: 0x040024BB RID: 9403
		private static int PoolCounter;
	}
}
