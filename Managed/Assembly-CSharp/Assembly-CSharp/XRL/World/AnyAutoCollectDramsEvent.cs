using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000679 RID: 1657
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AnyAutoCollectDramsEvent : ILiquidEvent
	{
		// Token: 0x0600472B RID: 18219 RVA: 0x00168988 File Offset: 0x00166B88
		public AnyAutoCollectDramsEvent()
		{
			this.ID = AnyAutoCollectDramsEvent.ID;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x0016899B File Offset: 0x00166B9B
		public static int CountPool()
		{
			if (AnyAutoCollectDramsEvent.Pool != null)
			{
				return AnyAutoCollectDramsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x001689B0 File Offset: 0x00166BB0
		public static void ResetPool()
		{
			while (AnyAutoCollectDramsEvent.PoolCounter > 0)
			{
				AnyAutoCollectDramsEvent.Pool[--AnyAutoCollectDramsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x001689D8 File Offset: 0x00166BD8
		public static void ResetTo(ref AnyAutoCollectDramsEvent E)
		{
			MinEvent.ResetTo<AnyAutoCollectDramsEvent>(E, AnyAutoCollectDramsEvent.Pool, ref AnyAutoCollectDramsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x001689EE File Offset: 0x00166BEE
		public static AnyAutoCollectDramsEvent FromPool()
		{
			return MinEvent.FromPool<AnyAutoCollectDramsEvent>(ref AnyAutoCollectDramsEvent.Pool, ref AnyAutoCollectDramsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x00168A04 File Offset: 0x00166C04
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x00168A18 File Offset: 0x00166C18
		public static bool Check(GameObject Actor, string Liquid = "water", GameObject Skip = null, List<GameObject> SkipList = null)
		{
			if (Actor.WantEvent(AnyAutoCollectDramsEvent.ID, ILiquidEvent.CascadeLevel))
			{
				AnyAutoCollectDramsEvent anyAutoCollectDramsEvent = AnyAutoCollectDramsEvent.FromPool();
				anyAutoCollectDramsEvent.Actor = Actor;
				anyAutoCollectDramsEvent.Liquid = Liquid;
				anyAutoCollectDramsEvent.LiquidVolume = null;
				anyAutoCollectDramsEvent.Drams = 0;
				anyAutoCollectDramsEvent.Skip = Skip;
				anyAutoCollectDramsEvent.SkipList = SkipList;
				anyAutoCollectDramsEvent.Filter = null;
				anyAutoCollectDramsEvent.Auto = true;
				anyAutoCollectDramsEvent.ImpureOkay = false;
				anyAutoCollectDramsEvent.SafeOnly = false;
				return !Actor.HandleEvent(anyAutoCollectDramsEvent);
			}
			return false;
		}

		// Token: 0x040021EC RID: 8684
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AnyAutoCollectDramsEvent), null, new MinEvent.EventPoolCount(AnyAutoCollectDramsEvent.CountPool), new MinEvent.EventPoolReset(AnyAutoCollectDramsEvent.ResetPool));

		// Token: 0x040021ED RID: 8685
		private static List<AnyAutoCollectDramsEvent> Pool;

		// Token: 0x040021EE RID: 8686
		private static int PoolCounter;
	}
}
