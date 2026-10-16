using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200076F RID: 1903
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetNamingChanceEvent : IActOnItemEvent
	{
		// Token: 0x06004E4C RID: 20044 RVA: 0x0017FFF5 File Offset: 0x0017E1F5
		public GetNamingChanceEvent()
		{
			this.ID = GetNamingChanceEvent.ID;
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00180008 File Offset: 0x0017E208
		public override int GetCascadeLevel()
		{
			return GetNamingChanceEvent.CascadeLevel;
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x0018000F File Offset: 0x0017E20F
		public static int CountPool()
		{
			if (GetNamingChanceEvent.Pool != null)
			{
				return GetNamingChanceEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00180024 File Offset: 0x0017E224
		public static void ResetPool()
		{
			while (GetNamingChanceEvent.PoolCounter > 0)
			{
				GetNamingChanceEvent.Pool[--GetNamingChanceEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x0018004C File Offset: 0x0017E24C
		public static void ResetTo(ref GetNamingChanceEvent E)
		{
			MinEvent.ResetTo<GetNamingChanceEvent>(E, GetNamingChanceEvent.Pool, ref GetNamingChanceEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00180062 File Offset: 0x0017E262
		public static GetNamingChanceEvent FromPool()
		{
			return MinEvent.FromPool<GetNamingChanceEvent>(ref GetNamingChanceEvent.Pool, ref GetNamingChanceEvent.PoolCounter, 8192);
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x00180078 File Offset: 0x0017E278
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x0018008C File Offset: 0x0017E28C
		public override void Reset()
		{
			base.Reset();
			this.Base = 0.0;
			this.PercentageBonus = 0.0;
			this.LinearBonus = 0.0;
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x001800C1 File Offset: 0x0017E2C1
		public static GetNamingChanceEvent FromPool(GameObject Actor, GameObject Item, double Base, double PercentageBonus = 0.0, double LinearBonus = 0.0)
		{
			GetNamingChanceEvent getNamingChanceEvent = GetNamingChanceEvent.FromPool();
			getNamingChanceEvent.Actor = Actor;
			getNamingChanceEvent.Item = Item;
			getNamingChanceEvent.Base = Base;
			getNamingChanceEvent.PercentageBonus = PercentageBonus;
			getNamingChanceEvent.LinearBonus = LinearBonus;
			return getNamingChanceEvent;
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x001800EC File Offset: 0x0017E2EC
		public static double GetFor(GameObject Actor, GameObject Item, double Base, double PercentageBonus = 0.0, double LinearBonus = 0.0)
		{
			bool flag = Actor != null && Actor.WantEvent(GetNamingChanceEvent.ID, GetNamingChanceEvent.CascadeLevel);
			bool flag2 = Item != null && Item.WantEvent(GetNamingChanceEvent.ID, GetNamingChanceEvent.CascadeLevel);
			double num;
			if (flag || flag2)
			{
				GetNamingChanceEvent getNamingChanceEvent = GetNamingChanceEvent.FromPool(Actor, Item, Base, PercentageBonus, LinearBonus);
				bool flag3 = true;
				if (flag)
				{
					flag3 = Actor.HandleEvent(getNamingChanceEvent);
				}
				if (flag3 && flag2)
				{
					flag3 = Item.HandleEvent(getNamingChanceEvent);
				}
				num = Base * (100.0 + getNamingChanceEvent.PercentageBonus) / 100.0 + getNamingChanceEvent.LinearBonus;
			}
			else
			{
				num = Base * (100.0 + PercentageBonus) / 100.0 + LinearBonus;
			}
			return num;
		}

		// Token: 0x04002639 RID: 9785
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetNamingChanceEvent), null, new MinEvent.EventPoolCount(GetNamingChanceEvent.CountPool), new MinEvent.EventPoolReset(GetNamingChanceEvent.ResetPool));

		// Token: 0x0400263A RID: 9786
		public new static readonly int CascadeLevel = 1;

		// Token: 0x0400263B RID: 9787
		private static List<GetNamingChanceEvent> Pool;

		// Token: 0x0400263C RID: 9788
		private static int PoolCounter;

		// Token: 0x0400263D RID: 9789
		public double Base;

		// Token: 0x0400263E RID: 9790
		public double PercentageBonus;

		// Token: 0x0400263F RID: 9791
		public double LinearBonus;
	}
}
