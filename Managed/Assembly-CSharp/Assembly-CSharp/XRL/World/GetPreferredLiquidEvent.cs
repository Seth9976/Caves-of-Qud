using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000777 RID: 1911
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Pool)]
	public class GetPreferredLiquidEvent : ILiquidEvent
	{
		// Token: 0x06004E88 RID: 20104 RVA: 0x00180D0B File Offset: 0x0017EF0B
		public GetPreferredLiquidEvent()
		{
			this.ID = GetPreferredLiquidEvent.ID;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x00180D1E File Offset: 0x0017EF1E
		public override int GetCascadeLevel()
		{
			return GetPreferredLiquidEvent.CascadeLevel;
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x00180D25 File Offset: 0x0017EF25
		public static int CountPool()
		{
			if (GetPreferredLiquidEvent.Pool != null)
			{
				return GetPreferredLiquidEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x00180D3A File Offset: 0x0017EF3A
		public static void ResetPool()
		{
			while (GetPreferredLiquidEvent.PoolCounter > 0)
			{
				GetPreferredLiquidEvent.Pool[--GetPreferredLiquidEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x00180D62 File Offset: 0x0017EF62
		public static void ResetTo(ref GetPreferredLiquidEvent E)
		{
			MinEvent.ResetTo<GetPreferredLiquidEvent>(E, GetPreferredLiquidEvent.Pool, ref GetPreferredLiquidEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x00180D78 File Offset: 0x0017EF78
		public static GetPreferredLiquidEvent FromPool()
		{
			return MinEvent.FromPool<GetPreferredLiquidEvent>(ref GetPreferredLiquidEvent.Pool, ref GetPreferredLiquidEvent.PoolCounter, 8192);
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x00180D8E File Offset: 0x0017EF8E
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00180DA2 File Offset: 0x0017EFA2
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x00180DB4 File Offset: 0x0017EFB4
		public static string GetFor(GameObject Object, GameObject Actor = null)
		{
			if (!Object.Understood())
			{
				return null;
			}
			if (Object.WantEvent(GetPreferredLiquidEvent.ID, GetPreferredLiquidEvent.CascadeLevel))
			{
				GetPreferredLiquidEvent getPreferredLiquidEvent = GetPreferredLiquidEvent.FromPool();
				getPreferredLiquidEvent.Object = Object;
				getPreferredLiquidEvent.Actor = Actor;
				getPreferredLiquidEvent.Liquid = null;
				getPreferredLiquidEvent.LiquidVolume = null;
				getPreferredLiquidEvent.Drams = 0;
				getPreferredLiquidEvent.Skip = null;
				getPreferredLiquidEvent.SkipList = null;
				getPreferredLiquidEvent.Filter = null;
				getPreferredLiquidEvent.Auto = false;
				getPreferredLiquidEvent.ImpureOkay = false;
				getPreferredLiquidEvent.SafeOnly = false;
				if (Object.HandleEvent(getPreferredLiquidEvent))
				{
					return getPreferredLiquidEvent.Liquid;
				}
			}
			return null;
		}

		// Token: 0x04002656 RID: 9814
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetPreferredLiquidEvent), null, new MinEvent.EventPoolCount(GetPreferredLiquidEvent.CountPool), new MinEvent.EventPoolReset(GetPreferredLiquidEvent.ResetPool));

		// Token: 0x04002657 RID: 9815
		public new static readonly int CascadeLevel = 0;

		// Token: 0x04002658 RID: 9816
		private static List<GetPreferredLiquidEvent> Pool;

		// Token: 0x04002659 RID: 9817
		private static int PoolCounter;

		// Token: 0x0400265A RID: 9818
		public GameObject Object;
	}
}
