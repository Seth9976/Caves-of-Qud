using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007B0 RID: 1968
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GravitationEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06005004 RID: 20484 RVA: 0x00185C09 File Offset: 0x00183E09
		public GravitationEvent()
		{
			this.ID = GravitationEvent.ID;
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x00185C1C File Offset: 0x00183E1C
		public static int CountPool()
		{
			if (GravitationEvent.Pool != null)
			{
				return GravitationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x00185C31 File Offset: 0x00183E31
		public static void ResetPool()
		{
			while (GravitationEvent.PoolCounter > 0)
			{
				GravitationEvent.Pool[--GravitationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x00185C59 File Offset: 0x00183E59
		public static void ResetTo(ref GravitationEvent E)
		{
			MinEvent.ResetTo<GravitationEvent>(E, GravitationEvent.Pool, ref GravitationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x00185C6F File Offset: 0x00183E6F
		public static GravitationEvent FromPool()
		{
			return MinEvent.FromPool<GravitationEvent>(ref GravitationEvent.Pool, ref GravitationEvent.PoolCounter, 8192);
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x00185C85 File Offset: 0x00183E85
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x00185C9C File Offset: 0x00183E9C
		public static GravitationEvent FromPool(GameObject Object, Cell Cell)
		{
			GravitationEvent gravitationEvent = GravitationEvent.FromPool();
			gravitationEvent.Object = Object;
			gravitationEvent.Cell = Cell;
			gravitationEvent.Forced = true;
			gravitationEvent.System = false;
			gravitationEvent.IgnoreGravity = false;
			gravitationEvent.NoStack = false;
			gravitationEvent.Direction = "D";
			gravitationEvent.Type = "Gravitation";
			gravitationEvent.Dragging = null;
			gravitationEvent.Actor = null;
			gravitationEvent.ForceSwap = null;
			gravitationEvent.Ignore = null;
			return gravitationEvent;
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x00185D0A File Offset: 0x00183F0A
		public static void Check(GameObject Object, Cell Cell)
		{
			if (Cell != null && GameObject.Validate(ref Object) && Cell.WantEvent(GravitationEvent.ID, MinEvent.CascadeLevel))
			{
				Cell.HandleEvent(GravitationEvent.FromPool(Object, Cell));
			}
		}

		// Token: 0x0400279D RID: 10141
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GravitationEvent), null, new MinEvent.EventPoolCount(GravitationEvent.CountPool), new MinEvent.EventPoolReset(GravitationEvent.ResetPool));

		// Token: 0x0400279E RID: 10142
		private static List<GravitationEvent> Pool;

		// Token: 0x0400279F RID: 10143
		private static int PoolCounter;
	}
}
