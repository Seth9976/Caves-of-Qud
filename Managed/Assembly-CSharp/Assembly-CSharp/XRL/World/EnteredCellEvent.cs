using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000708 RID: 1800
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EnteredCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06004B24 RID: 19236 RVA: 0x00176095 File Offset: 0x00174295
		public EnteredCellEvent()
		{
			this.ID = EnteredCellEvent.ID;
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x001760A8 File Offset: 0x001742A8
		public static int CountPool()
		{
			if (EnteredCellEvent.Pool != null)
			{
				return EnteredCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x001760BD File Offset: 0x001742BD
		public static void ResetPool()
		{
			while (EnteredCellEvent.PoolCounter > 0)
			{
				EnteredCellEvent.Pool[--EnteredCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x001760E5 File Offset: 0x001742E5
		public static void ResetTo(ref EnteredCellEvent E)
		{
			MinEvent.ResetTo<EnteredCellEvent>(E, EnteredCellEvent.Pool, ref EnteredCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x001760FB File Offset: 0x001742FB
		public static EnteredCellEvent FromPool()
		{
			return MinEvent.FromPool<EnteredCellEvent>(ref EnteredCellEvent.Pool, ref EnteredCellEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x00176111 File Offset: 0x00174311
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x00176128 File Offset: 0x00174328
		public static EnteredCellEvent FromPool(GameObject Object, Cell Cell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			EnteredCellEvent enteredCellEvent = EnteredCellEvent.FromPool();
			enteredCellEvent.Object = Object;
			enteredCellEvent.Cell = Cell;
			enteredCellEvent.Forced = Forced;
			enteredCellEvent.System = System;
			enteredCellEvent.IgnoreGravity = IgnoreGravity;
			enteredCellEvent.NoStack = NoStack;
			enteredCellEvent.Direction = Direction;
			enteredCellEvent.Type = Type;
			enteredCellEvent.Dragging = Dragging;
			enteredCellEvent.Actor = Actor;
			enteredCellEvent.ForceSwap = ForceSwap;
			enteredCellEvent.Ignore = Ignore;
			return enteredCellEvent;
		}

		// Token: 0x04002445 RID: 9285
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EnteredCellEvent), null, new MinEvent.EventPoolCount(EnteredCellEvent.CountPool), new MinEvent.EventPoolReset(EnteredCellEvent.ResetPool));

		// Token: 0x04002446 RID: 9286
		private static List<EnteredCellEvent> Pool;

		// Token: 0x04002447 RID: 9287
		private static int PoolCounter;
	}
}
