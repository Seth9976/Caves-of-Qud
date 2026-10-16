using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000707 RID: 1799
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EnterCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06004B1C RID: 19228 RVA: 0x00175F68 File Offset: 0x00174168
		public EnterCellEvent()
		{
			this.ID = EnterCellEvent.ID;
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x00175F7B File Offset: 0x0017417B
		public static int CountPool()
		{
			if (EnterCellEvent.Pool != null)
			{
				return EnterCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x00175F90 File Offset: 0x00174190
		public static void ResetPool()
		{
			while (EnterCellEvent.PoolCounter > 0)
			{
				EnterCellEvent.Pool[--EnterCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x00175FB8 File Offset: 0x001741B8
		public static void ResetTo(ref EnterCellEvent E)
		{
			MinEvent.ResetTo<EnterCellEvent>(E, EnterCellEvent.Pool, ref EnterCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x00175FCE File Offset: 0x001741CE
		public static EnterCellEvent FromPool()
		{
			return MinEvent.FromPool<EnterCellEvent>(ref EnterCellEvent.Pool, ref EnterCellEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x00175FE4 File Offset: 0x001741E4
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x00175FF8 File Offset: 0x001741F8
		public static EnterCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			EnterCellEvent enterCellEvent = EnterCellEvent.FromPool();
			enterCellEvent.Object = Object;
			enterCellEvent.Cell = Cell;
			enterCellEvent.Forced = Forced;
			enterCellEvent.System = System;
			enterCellEvent.IgnoreGravity = IgnoreGravity;
			enterCellEvent.NoStack = NoStack;
			enterCellEvent.Direction = Direction;
			enterCellEvent.Type = Type;
			enterCellEvent.Dragging = Dragging;
			enterCellEvent.Actor = Actor;
			enterCellEvent.ForceSwap = ForceSwap;
			enterCellEvent.Ignore = Ignore;
			return enterCellEvent;
		}

		// Token: 0x04002442 RID: 9282
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EnterCellEvent), null, new MinEvent.EventPoolCount(EnterCellEvent.CountPool), new MinEvent.EventPoolReset(EnterCellEvent.ResetPool));

		// Token: 0x04002443 RID: 9283
		private static List<EnterCellEvent> Pool;

		// Token: 0x04002444 RID: 9284
		private static int PoolCounter;
	}
}
