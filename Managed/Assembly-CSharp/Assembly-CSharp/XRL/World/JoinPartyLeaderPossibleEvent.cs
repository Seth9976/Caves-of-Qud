using System;

namespace XRL.World
{
	// Token: 0x020007F7 RID: 2039
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class JoinPartyLeaderPossibleEvent : PooledEvent<JoinPartyLeaderPossibleEvent>
	{
		// Token: 0x060051B3 RID: 20915 RVA: 0x0018A5A8 File Offset: 0x001887A8
		public override int GetCascadeLevel()
		{
			return JoinPartyLeaderPossibleEvent.CascadeLevel;
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0018A5AF File Offset: 0x001887AF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0018A5B8 File Offset: 0x001887B8
		public override void Reset()
		{
			base.Reset();
			this.Companion = null;
			this.Leader = null;
			this.CurrentCell = null;
			this.TargetCell = null;
			this.IsMobile = false;
			this.Result = false;
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x0018A5EC File Offset: 0x001887EC
		public static bool Check(GameObject Companion, GameObject Leader, Cell CurrentCell, ref Cell TargetCell, bool IsMobile)
		{
			if (!GameObject.Validate(ref Companion))
			{
				return false;
			}
			bool flag = IsMobile;
			if (Companion.HasRegisteredEvent("JoinPartyLeaderPossible"))
			{
				Event @event = Event.New("JoinPartyLeaderPossible", 0, 0, 0);
				@event.SetParameter("Companion", Companion);
				@event.SetParameter("Leader", Leader);
				@event.SetParameter("CurrentCell", CurrentCell);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetFlag("IsMobile", IsMobile);
				@event.SetFlag("Result", flag);
				bool flag2 = Companion.FireEvent(@event);
				TargetCell = @event.GetParameter("TargetCell") as Cell;
				flag = @event.HasFlag("Result");
				if (!flag2)
				{
					return flag;
				}
			}
			if (Companion.WantEvent(PooledEvent<JoinPartyLeaderPossibleEvent>.ID, JoinPartyLeaderPossibleEvent.CascadeLevel))
			{
				JoinPartyLeaderPossibleEvent joinPartyLeaderPossibleEvent = PooledEvent<JoinPartyLeaderPossibleEvent>.FromPool();
				joinPartyLeaderPossibleEvent.Companion = Companion;
				joinPartyLeaderPossibleEvent.Leader = Leader;
				joinPartyLeaderPossibleEvent.CurrentCell = CurrentCell;
				joinPartyLeaderPossibleEvent.TargetCell = TargetCell;
				joinPartyLeaderPossibleEvent.IsMobile = IsMobile;
				joinPartyLeaderPossibleEvent.Result = flag;
				Companion.HandleEvent(joinPartyLeaderPossibleEvent);
				TargetCell = joinPartyLeaderPossibleEvent.TargetCell;
				flag = joinPartyLeaderPossibleEvent.Result;
			}
			return flag;
		}

		// Token: 0x040028F7 RID: 10487
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028F8 RID: 10488
		public GameObject Companion;

		// Token: 0x040028F9 RID: 10489
		public GameObject Leader;

		// Token: 0x040028FA RID: 10490
		public Cell CurrentCell;

		/// <summary>Approximate target cell, usually the leader's current cell. Can be replaced.</summary>
		// Token: 0x040028FB RID: 10491
		public Cell TargetCell;

		// Token: 0x040028FC RID: 10492
		public bool IsMobile;

		// Token: 0x040028FD RID: 10493
		public bool Result;
	}
}
