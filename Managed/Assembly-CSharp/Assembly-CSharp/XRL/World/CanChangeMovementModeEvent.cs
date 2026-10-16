using System;

namespace XRL.World
{
	// Token: 0x020006C0 RID: 1728
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanChangeMovementModeEvent : PooledEvent<CanChangeMovementModeEvent>
	{
		// Token: 0x06004943 RID: 18755 RVA: 0x00171279 File Offset: 0x0016F479
		public override int GetCascadeLevel()
		{
			return CanChangeMovementModeEvent.CascadeLevel;
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x00171280 File Offset: 0x0016F480
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x00171289 File Offset: 0x0016F489
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.To = null;
			this.Involuntary = false;
			this.ShowMessage = false;
			this.AllowTelekinetic = false;
			this.FrozenOkay = false;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x001712BC File Offset: 0x0016F4BC
		public static bool Check(GameObject Object, string To = null, bool Involuntary = false, bool ShowMessage = false, bool AllowTelekinetic = false, bool FrozenOkay = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanChangeMovementMode"))
			{
				Event @event = Event.New("CanChangeMovementMode", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("To", To);
				@event.SetFlag("Involuntary", Involuntary);
				@event.SetFlag("ShowMessage", ShowMessage);
				@event.SetFlag("AllowTelekinetic", AllowTelekinetic);
				@event.SetFlag("FrozenOkay", FrozenOkay);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CanChangeMovementModeEvent>.ID, CanChangeMovementModeEvent.CascadeLevel))
			{
				CanChangeMovementModeEvent canChangeMovementModeEvent = PooledEvent<CanChangeMovementModeEvent>.FromPool();
				canChangeMovementModeEvent.Object = Object;
				canChangeMovementModeEvent.To = To;
				canChangeMovementModeEvent.Involuntary = Involuntary;
				canChangeMovementModeEvent.ShowMessage = ShowMessage;
				canChangeMovementModeEvent.AllowTelekinetic = AllowTelekinetic;
				canChangeMovementModeEvent.FrozenOkay = FrozenOkay;
				flag = Object.HandleEvent(canChangeMovementModeEvent);
			}
			return flag;
		}

		// Token: 0x04002327 RID: 8999
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002328 RID: 9000
		public GameObject Object;

		// Token: 0x04002329 RID: 9001
		public string To;

		// Token: 0x0400232A RID: 9002
		public bool Involuntary;

		// Token: 0x0400232B RID: 9003
		public bool ShowMessage;

		// Token: 0x0400232C RID: 9004
		public bool AllowTelekinetic;

		// Token: 0x0400232D RID: 9005
		public bool FrozenOkay;
	}
}
