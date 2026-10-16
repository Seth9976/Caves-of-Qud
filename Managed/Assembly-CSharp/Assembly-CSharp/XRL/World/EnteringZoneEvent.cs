using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200070A RID: 1802
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 32)]
	public class EnteringZoneEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06004B34 RID: 19252 RVA: 0x001762F5 File Offset: 0x001744F5
		public EnteringZoneEvent()
		{
			this.ID = EnteringZoneEvent.ID;
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x00176308 File Offset: 0x00174508
		public override int GetCascadeLevel()
		{
			return EnteringZoneEvent.CascadeLevel;
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x0017630F File Offset: 0x0017450F
		public static int CountPool()
		{
			if (EnteringZoneEvent.Pool != null)
			{
				return EnteringZoneEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x00176324 File Offset: 0x00174524
		public static void ResetPool()
		{
			while (EnteringZoneEvent.PoolCounter > 0)
			{
				EnteringZoneEvent.Pool[--EnteringZoneEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x0017634C File Offset: 0x0017454C
		public static void ResetTo(ref EnteringZoneEvent E)
		{
			MinEvent.ResetTo<EnteringZoneEvent>(E, EnteringZoneEvent.Pool, ref EnteringZoneEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x00176362 File Offset: 0x00174562
		public static EnteringZoneEvent FromPool()
		{
			return MinEvent.FromPool<EnteringZoneEvent>(ref EnteringZoneEvent.Pool, ref EnteringZoneEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x00176378 File Offset: 0x00174578
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x0017638C File Offset: 0x0017458C
		public override void Reset()
		{
			base.Reset();
			this.Origin = null;
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x0017639C File Offset: 0x0017459C
		public static EnteringZoneEvent FromPool(GameObject Object, Cell Origin, Cell Destination, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			EnteringZoneEvent enteringZoneEvent = EnteringZoneEvent.FromPool();
			enteringZoneEvent.Object = Object;
			enteringZoneEvent.Origin = Origin;
			enteringZoneEvent.Cell = Destination;
			enteringZoneEvent.Forced = Forced;
			enteringZoneEvent.System = System;
			enteringZoneEvent.IgnoreGravity = IgnoreGravity;
			enteringZoneEvent.NoStack = NoStack;
			enteringZoneEvent.Direction = Direction;
			enteringZoneEvent.Type = Type;
			enteringZoneEvent.Dragging = Dragging;
			enteringZoneEvent.Actor = Actor;
			enteringZoneEvent.ForceSwap = ForceSwap;
			enteringZoneEvent.Ignore = Ignore;
			return enteringZoneEvent;
		}

		// Token: 0x0400244B RID: 9291
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EnteringZoneEvent), null, new MinEvent.EventPoolCount(EnteringZoneEvent.CountPool), new MinEvent.EventPoolReset(EnteringZoneEvent.ResetPool));

		// Token: 0x0400244C RID: 9292
		public new static readonly int CascadeLevel = 32;

		// Token: 0x0400244D RID: 9293
		private static List<EnteringZoneEvent> Pool;

		// Token: 0x0400244E RID: 9294
		private static int PoolCounter;

		// Token: 0x0400244F RID: 9295
		public Cell Origin;
	}
}
