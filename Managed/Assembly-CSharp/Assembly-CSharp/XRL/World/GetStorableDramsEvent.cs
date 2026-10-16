using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000793 RID: 1939
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetStorableDramsEvent : ILiquidEvent
	{
		// Token: 0x06004F3C RID: 20284 RVA: 0x00182F91 File Offset: 0x00181191
		public GetStorableDramsEvent()
		{
			this.ID = GetStorableDramsEvent.ID;
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x00182FA4 File Offset: 0x001811A4
		public static int CountPool()
		{
			if (GetStorableDramsEvent.Pool != null)
			{
				return GetStorableDramsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x00182FB9 File Offset: 0x001811B9
		public static void ResetPool()
		{
			while (GetStorableDramsEvent.PoolCounter > 0)
			{
				GetStorableDramsEvent.Pool[--GetStorableDramsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x00182FE1 File Offset: 0x001811E1
		public static void ResetTo(ref GetStorableDramsEvent E)
		{
			MinEvent.ResetTo<GetStorableDramsEvent>(E, GetStorableDramsEvent.Pool, ref GetStorableDramsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x00182FF7 File Offset: 0x001811F7
		public static GetStorableDramsEvent FromPool()
		{
			return MinEvent.FromPool<GetStorableDramsEvent>(ref GetStorableDramsEvent.Pool, ref GetStorableDramsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x0018300D File Offset: 0x0018120D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x00183024 File Offset: 0x00181224
		public static GetStorableDramsEvent FromPool(GameObject Actor, string Liquid = "water", GameObject Skip = null, List<GameObject> SkipList = null, Predicate<GameObject> Filter = null, bool SafeOnly = true, LiquidVolume LiquidVolume = null)
		{
			GetStorableDramsEvent getStorableDramsEvent = GetStorableDramsEvent.FromPool();
			getStorableDramsEvent.Actor = Actor;
			getStorableDramsEvent.Liquid = Liquid;
			getStorableDramsEvent.LiquidVolume = LiquidVolume;
			getStorableDramsEvent.Drams = 0;
			getStorableDramsEvent.Skip = Skip;
			getStorableDramsEvent.SkipList = SkipList;
			getStorableDramsEvent.Filter = Filter;
			getStorableDramsEvent.Auto = false;
			getStorableDramsEvent.ImpureOkay = false;
			getStorableDramsEvent.SafeOnly = SafeOnly;
			return getStorableDramsEvent;
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x00183080 File Offset: 0x00181280
		public static int GetFor(GameObject Actor, string Liquid = "water", GameObject Skip = null, List<GameObject> SkipList = null, Predicate<GameObject> Filter = null, bool SafeOnly = true, LiquidVolume LiquidVolume = null)
		{
			if (Actor.WantEvent(GetStorableDramsEvent.ID, ILiquidEvent.CascadeLevel))
			{
				GetStorableDramsEvent getStorableDramsEvent = GetStorableDramsEvent.FromPool(Actor, Liquid, Skip, SkipList, Filter, SafeOnly, LiquidVolume);
				Actor.HandleEvent(getStorableDramsEvent);
				return getStorableDramsEvent.Drams;
			}
			return 0;
		}

		// Token: 0x040026F1 RID: 9969
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetStorableDramsEvent), null, new MinEvent.EventPoolCount(GetStorableDramsEvent.CountPool), new MinEvent.EventPoolReset(GetStorableDramsEvent.ResetPool));

		// Token: 0x040026F2 RID: 9970
		private static List<GetStorableDramsEvent> Pool;

		// Token: 0x040026F3 RID: 9971
		private static int PoolCounter;
	}
}
