using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000805 RID: 2053
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class MissileTraversingCellEvent : IObjectCellInteractionEvent
	{
		// Token: 0x06005213 RID: 21011 RVA: 0x0018BB86 File Offset: 0x00189D86
		public MissileTraversingCellEvent()
		{
			this.ID = MissileTraversingCellEvent.ID;
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x0018BB99 File Offset: 0x00189D99
		public static int CountPool()
		{
			if (MissileTraversingCellEvent.Pool != null)
			{
				return MissileTraversingCellEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x0018BBAE File Offset: 0x00189DAE
		public static void ResetPool()
		{
			while (MissileTraversingCellEvent.PoolCounter > 0)
			{
				MissileTraversingCellEvent.Pool[--MissileTraversingCellEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x0018BBD6 File Offset: 0x00189DD6
		public static void ResetTo(ref MissileTraversingCellEvent E)
		{
			MinEvent.ResetTo<MissileTraversingCellEvent>(E, MissileTraversingCellEvent.Pool, ref MissileTraversingCellEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x0018BBEC File Offset: 0x00189DEC
		public static MissileTraversingCellEvent FromPool()
		{
			return MinEvent.FromPool<MissileTraversingCellEvent>(ref MissileTraversingCellEvent.Pool, ref MissileTraversingCellEvent.PoolCounter, 8192);
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x0018BC02 File Offset: 0x00189E02
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x0018BC16 File Offset: 0x00189E16
		public override void Reset()
		{
			base.Reset();
			this.Thrown = false;
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x0018BC28 File Offset: 0x00189E28
		public static bool Check(GameObject Object, Cell Cell, string Direction = null, GameObject Actor = null, bool Thrown = false)
		{
			bool flag = true;
			if (flag && Cell != null && Cell.HasObjectWithRegisteredEvent("MissileTraversingCell"))
			{
				Event @event = Event.New("MissileTraversingCell", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Cell", Cell);
				@event.SetParameter("Direction", Direction);
				@event.SetParameter("Actor", Actor);
				@event.SetFlag("Thrown", Thrown);
				flag = Cell.FireEvent(@event);
			}
			if (flag && Cell != null && Cell.WantEvent(MissileTraversingCellEvent.ID, MinEvent.CascadeLevel))
			{
				MissileTraversingCellEvent missileTraversingCellEvent = MissileTraversingCellEvent.FromPool();
				missileTraversingCellEvent.Object = Object;
				missileTraversingCellEvent.Cell = Cell;
				missileTraversingCellEvent.Forced = false;
				missileTraversingCellEvent.System = false;
				missileTraversingCellEvent.IgnoreGravity = true;
				missileTraversingCellEvent.NoStack = true;
				missileTraversingCellEvent.Direction = Direction;
				missileTraversingCellEvent.Type = (Thrown ? "Throw" : "Missile");
				missileTraversingCellEvent.Dragging = null;
				missileTraversingCellEvent.Actor = Actor;
				missileTraversingCellEvent.ForceSwap = null;
				missileTraversingCellEvent.Ignore = null;
				missileTraversingCellEvent.Thrown = Thrown;
				flag = Cell.HandleEvent(missileTraversingCellEvent);
			}
			return flag;
		}

		// Token: 0x04002942 RID: 10562
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(MissileTraversingCellEvent), null, new MinEvent.EventPoolCount(MissileTraversingCellEvent.CountPool), new MinEvent.EventPoolReset(MissileTraversingCellEvent.ResetPool));

		// Token: 0x04002943 RID: 10563
		private static List<MissileTraversingCellEvent> Pool;

		// Token: 0x04002944 RID: 10564
		private static int PoolCounter;

		// Token: 0x04002945 RID: 10565
		public bool Thrown;
	}
}
