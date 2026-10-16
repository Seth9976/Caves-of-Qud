using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000799 RID: 1945
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class GetTinkeringBonusEvent : IActOnItemEvent
	{
		// Token: 0x06004F60 RID: 20320 RVA: 0x00183953 File Offset: 0x00181B53
		public GetTinkeringBonusEvent()
		{
			this.ID = GetTinkeringBonusEvent.ID;
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x00183966 File Offset: 0x00181B66
		public override int GetCascadeLevel()
		{
			return GetTinkeringBonusEvent.CascadeLevel;
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x0018396D File Offset: 0x00181B6D
		public static int CountPool()
		{
			if (GetTinkeringBonusEvent.Pool != null)
			{
				return GetTinkeringBonusEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x00183982 File Offset: 0x00181B82
		public static void ResetPool()
		{
			while (GetTinkeringBonusEvent.PoolCounter > 0)
			{
				GetTinkeringBonusEvent.Pool[--GetTinkeringBonusEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x001839AA File Offset: 0x00181BAA
		public static void ResetTo(ref GetTinkeringBonusEvent E)
		{
			MinEvent.ResetTo<GetTinkeringBonusEvent>(E, GetTinkeringBonusEvent.Pool, ref GetTinkeringBonusEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x001839C0 File Offset: 0x00181BC0
		public static GetTinkeringBonusEvent FromPool()
		{
			return MinEvent.FromPool<GetTinkeringBonusEvent>(ref GetTinkeringBonusEvent.Pool, ref GetTinkeringBonusEvent.PoolCounter, 8192);
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x001839D6 File Offset: 0x00181BD6
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x001839EA File Offset: 0x00181BEA
		public override void Reset()
		{
			base.Reset();
			this.Type = null;
			this.BaseRating = 0;
			this.Bonus = 0;
			this.SecondaryBonus = 0;
			this.ToolboxBonus = 0;
			this.PsychometryApplied = false;
			this.Interruptable = false;
			this.ForSifrah = false;
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x00183A2C File Offset: 0x00181C2C
		public static int GetFor(GameObject Actor, GameObject Item, string Type, int BaseRating, int Bonus, ref int SecondaryBonus, ref bool Interrupt, ref bool PsychometryApplied, bool Interruptable = true, bool ForSifrah = false)
		{
			if (Actor.WantEvent(GetTinkeringBonusEvent.ID, GetTinkeringBonusEvent.CascadeLevel))
			{
				GetTinkeringBonusEvent getTinkeringBonusEvent = GetTinkeringBonusEvent.FromPool();
				getTinkeringBonusEvent.Actor = Actor;
				getTinkeringBonusEvent.Item = Item;
				getTinkeringBonusEvent.Type = Type;
				getTinkeringBonusEvent.BaseRating = BaseRating;
				getTinkeringBonusEvent.Bonus = Bonus;
				getTinkeringBonusEvent.SecondaryBonus = SecondaryBonus;
				getTinkeringBonusEvent.ToolboxBonus = 0;
				getTinkeringBonusEvent.PsychometryApplied = PsychometryApplied;
				getTinkeringBonusEvent.Interruptable = Interruptable;
				getTinkeringBonusEvent.ForSifrah = ForSifrah;
				if (!Actor.HandleEvent(getTinkeringBonusEvent))
				{
					Interrupt = true;
				}
				Bonus = getTinkeringBonusEvent.Bonus;
				PsychometryApplied = getTinkeringBonusEvent.PsychometryApplied;
				SecondaryBonus = getTinkeringBonusEvent.SecondaryBonus;
			}
			return Bonus;
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00183AC8 File Offset: 0x00181CC8
		public static int GetFor(GameObject Actor, GameObject Item, string Type, int BaseRating, int Bonus, ref bool Interrupt, ref bool PsychometryApplied, bool Interruptable = true, bool ForSifrah = false)
		{
			int num = 0;
			return GetTinkeringBonusEvent.GetFor(Actor, Item, Type, BaseRating, Bonus, ref num, ref Interrupt, ref PsychometryApplied, Interruptable, ForSifrah);
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x00183AEC File Offset: 0x00181CEC
		public static int GetFor(GameObject Actor, GameObject Item, string Type, int BaseRating, int Bonus, ref bool Interrupt, bool Interruptable = true, bool ForSifrah = false)
		{
			int num = 0;
			bool flag = false;
			return GetTinkeringBonusEvent.GetFor(Actor, Item, Type, BaseRating, Bonus, ref num, ref Interrupt, ref flag, Interruptable, ForSifrah);
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x00183B14 File Offset: 0x00181D14
		public static int GetFor(GameObject Actor, GameObject Item, string Type, int BaseRating, int Bonus, bool Interruptable = true, bool ForSifrah = false)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			return GetTinkeringBonusEvent.GetFor(Actor, Item, Type, BaseRating, Bonus, ref num, ref flag, ref flag2, Interruptable, ForSifrah);
		}

		// Token: 0x04002715 RID: 10005
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetTinkeringBonusEvent), null, new MinEvent.EventPoolCount(GetTinkeringBonusEvent.CountPool), new MinEvent.EventPoolReset(GetTinkeringBonusEvent.ResetPool));

		// Token: 0x04002716 RID: 10006
		public new static readonly int CascadeLevel = 3;

		// Token: 0x04002717 RID: 10007
		private static List<GetTinkeringBonusEvent> Pool;

		// Token: 0x04002718 RID: 10008
		private static int PoolCounter;

		// Token: 0x04002719 RID: 10009
		public string Type;

		// Token: 0x0400271A RID: 10010
		public int BaseRating;

		// Token: 0x0400271B RID: 10011
		public int Bonus;

		// Token: 0x0400271C RID: 10012
		public int SecondaryBonus;

		// Token: 0x0400271D RID: 10013
		public int ToolboxBonus;

		// Token: 0x0400271E RID: 10014
		public bool PsychometryApplied;

		// Token: 0x0400271F RID: 10015
		public bool Interruptable;

		// Token: 0x04002720 RID: 10016
		public bool ForSifrah;
	}
}
