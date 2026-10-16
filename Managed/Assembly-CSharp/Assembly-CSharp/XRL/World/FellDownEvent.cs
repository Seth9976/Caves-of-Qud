using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000713 RID: 1811
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class FellDownEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06004B79 RID: 19321 RVA: 0x00176FE3 File Offset: 0x001751E3
		public FellDownEvent()
		{
			this.ID = FellDownEvent.ID;
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x00176FF6 File Offset: 0x001751F6
		public static int CountPool()
		{
			if (FellDownEvent.Pool != null)
			{
				return FellDownEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x0017700B File Offset: 0x0017520B
		public static void ResetPool()
		{
			while (FellDownEvent.PoolCounter > 0)
			{
				FellDownEvent.Pool[--FellDownEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x00177033 File Offset: 0x00175233
		public static void ResetTo(ref FellDownEvent E)
		{
			MinEvent.ResetTo<FellDownEvent>(E, FellDownEvent.Pool, ref FellDownEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x00177049 File Offset: 0x00175249
		public static FellDownEvent FromPool()
		{
			return MinEvent.FromPool<FellDownEvent>(ref FellDownEvent.Pool, ref FellDownEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x0017705F File Offset: 0x0017525F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x00177073 File Offset: 0x00175273
		public override void Reset()
		{
			base.Reset();
			this.FromCell = null;
			this.Distance = 0;
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x0017708C File Offset: 0x0017528C
		public static FellDownEvent FromPool(GameObject Object, Cell Cell, Cell FromCell, int Distance)
		{
			FellDownEvent fellDownEvent = FellDownEvent.FromPool();
			fellDownEvent.Object = Object;
			fellDownEvent.Cell = Cell;
			fellDownEvent.Forced = false;
			fellDownEvent.System = false;
			fellDownEvent.IgnoreGravity = false;
			fellDownEvent.NoStack = false;
			fellDownEvent.Direction = "D";
			fellDownEvent.Type = null;
			fellDownEvent.Dragging = null;
			fellDownEvent.Actor = null;
			fellDownEvent.ForceSwap = null;
			fellDownEvent.Ignore = null;
			fellDownEvent.Distance = Distance;
			return fellDownEvent;
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00177100 File Offset: 0x00175300
		public static void Send(GameObject Object, Cell Cell, Cell FromCell, int Distance)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("FellDown"))
			{
				Event @event = Event.New("FellDown", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Cell", Cell);
				@event.SetParameter("FromCell", FromCell);
				@event.SetParameter("Distance", Distance);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(FellDownEvent.ID, MinEvent.CascadeLevel))
			{
				flag = Object.HandleEvent(FellDownEvent.FromPool(Object, Cell, FromCell, Distance));
			}
		}

		// Token: 0x0400246E RID: 9326
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(FellDownEvent), null, new MinEvent.EventPoolCount(FellDownEvent.CountPool), new MinEvent.EventPoolReset(FellDownEvent.ResetPool));

		// Token: 0x0400246F RID: 9327
		private static List<FellDownEvent> Pool;

		// Token: 0x04002470 RID: 9328
		private static int PoolCounter;

		// Token: 0x04002471 RID: 9329
		public Cell FromCell;

		// Token: 0x04002472 RID: 9330
		public int Distance;
	}
}
