using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200076E RID: 1902
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetNamingBestowalChanceEvent : IActOnItemEvent
	{
		// Token: 0x06004E41 RID: 20033 RVA: 0x0017FE4F File Offset: 0x0017E04F
		public GetNamingBestowalChanceEvent()
		{
			this.ID = GetNamingBestowalChanceEvent.ID;
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x0017FE62 File Offset: 0x0017E062
		public override int GetCascadeLevel()
		{
			return GetNamingBestowalChanceEvent.CascadeLevel;
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x0017FE69 File Offset: 0x0017E069
		public static int CountPool()
		{
			if (GetNamingBestowalChanceEvent.Pool != null)
			{
				return GetNamingBestowalChanceEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x0017FE7E File Offset: 0x0017E07E
		public static void ResetPool()
		{
			while (GetNamingBestowalChanceEvent.PoolCounter > 0)
			{
				GetNamingBestowalChanceEvent.Pool[--GetNamingBestowalChanceEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x0017FEA6 File Offset: 0x0017E0A6
		public static void ResetTo(ref GetNamingBestowalChanceEvent E)
		{
			MinEvent.ResetTo<GetNamingBestowalChanceEvent>(E, GetNamingBestowalChanceEvent.Pool, ref GetNamingBestowalChanceEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x0017FEBC File Offset: 0x0017E0BC
		public static GetNamingBestowalChanceEvent FromPool()
		{
			return MinEvent.FromPool<GetNamingBestowalChanceEvent>(ref GetNamingBestowalChanceEvent.Pool, ref GetNamingBestowalChanceEvent.PoolCounter, 8192);
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x0017FED2 File Offset: 0x0017E0D2
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x0017FEE6 File Offset: 0x0017E0E6
		public override void Reset()
		{
			base.Reset();
			this.Base = 0;
			this.PercentageBonus = 0;
			this.LinearBonus = 0;
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x0017FF03 File Offset: 0x0017E103
		public static GetNamingBestowalChanceEvent FromPool(GameObject Actor, GameObject Item, int Base, int PercentageBonus = 0, int LinearBonus = 0)
		{
			GetNamingBestowalChanceEvent getNamingBestowalChanceEvent = GetNamingBestowalChanceEvent.FromPool();
			getNamingBestowalChanceEvent.Actor = Actor;
			getNamingBestowalChanceEvent.Item = Item;
			getNamingBestowalChanceEvent.Base = Base;
			getNamingBestowalChanceEvent.PercentageBonus = PercentageBonus;
			getNamingBestowalChanceEvent.LinearBonus = LinearBonus;
			return getNamingBestowalChanceEvent;
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x0017FF30 File Offset: 0x0017E130
		public static int GetFor(GameObject Actor, GameObject Item, int Base, int PercentageBonus = 0, int LinearBonus = 0)
		{
			bool flag = Actor != null && Actor.WantEvent(GetNamingBestowalChanceEvent.ID, GetNamingBestowalChanceEvent.CascadeLevel);
			bool flag2 = Item != null && Item.WantEvent(GetNamingBestowalChanceEvent.ID, GetNamingBestowalChanceEvent.CascadeLevel);
			int num;
			if (flag || flag2)
			{
				GetNamingBestowalChanceEvent getNamingBestowalChanceEvent = GetNamingBestowalChanceEvent.FromPool(Actor, Item, Base, PercentageBonus, LinearBonus);
				bool flag3 = true;
				if (flag)
				{
					flag3 = Actor.HandleEvent(getNamingBestowalChanceEvent);
				}
				if (flag3 && flag2)
				{
					flag3 = Item.HandleEvent(getNamingBestowalChanceEvent);
				}
				num = Base * (100 + getNamingBestowalChanceEvent.PercentageBonus) / 100 + getNamingBestowalChanceEvent.LinearBonus;
			}
			else
			{
				num = Base * (100 + PercentageBonus) / 100 + LinearBonus;
			}
			return num;
		}

		// Token: 0x04002632 RID: 9778
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetNamingBestowalChanceEvent), null, new MinEvent.EventPoolCount(GetNamingBestowalChanceEvent.CountPool), new MinEvent.EventPoolReset(GetNamingBestowalChanceEvent.ResetPool));

		// Token: 0x04002633 RID: 9779
		public new static readonly int CascadeLevel = 1;

		// Token: 0x04002634 RID: 9780
		private static List<GetNamingBestowalChanceEvent> Pool;

		// Token: 0x04002635 RID: 9781
		private static int PoolCounter;

		// Token: 0x04002636 RID: 9782
		public int Base;

		// Token: 0x04002637 RID: 9783
		public int PercentageBonus;

		// Token: 0x04002638 RID: 9784
		public int LinearBonus;
	}
}
