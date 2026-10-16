using System;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000FDF RID: 4063
	internal class HindrenMysteryExile : IPart
	{
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600A153 RID: 41299 RVA: 0x003AB321 File Offset: 0x003A9521
		private Zone beyLah
		{
			get
			{
				return KithAndKinGameState.Instance.getBeyLahZone();
			}
		}

		// Token: 0x0600A154 RID: 41300 RVA: 0x003AB32D File Offset: 0x003A952D
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == SuspendingEvent.ID;
		}

		// Token: 0x0600A155 RID: 41301 RVA: 0x003AB344 File Offset: 0x003A9544
		public override bool HandleEvent(SuspendingEvent E)
		{
			if (!this.ParentObject.IsPlayerControlled())
			{
				if (this.Destination == null)
				{
					this.ParentObject.Destroy(null, false, false, null);
				}
				else if (this.ParentObject.CurrentZone.ZoneID != this.Destination.ZoneID)
				{
					this.ParentObject.DirectMoveTo(this.Destination, 0, true, true, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A156 RID: 41302 RVA: 0x003AB3B7 File Offset: 0x003A95B7
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A157 RID: 41303 RVA: 0x003AB3BA File Offset: 0x003A95BA
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A158 RID: 41304 RVA: 0x003AB3BD File Offset: 0x003A95BD
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckExile();
		}

		// Token: 0x0600A159 RID: 41305 RVA: 0x003AB3C8 File Offset: 0x003A95C8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneFreezing" && !this.ParentObject.IsPlayerControlled())
			{
				if (this.Destination == null)
				{
					this.ParentObject.Destroy(null, false, false, null);
				}
				else if (this.ParentObject.CurrentZone.ZoneID != this.Destination.ZoneID)
				{
					this.ParentObject.DirectMoveTo(this.Destination, 0, true, true, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A15A RID: 41306 RVA: 0x003AB450 File Offset: 0x003A9650
		public bool CheckExile()
		{
			if (this.ParentObject.IsBusy())
			{
				return false;
			}
			if (!this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				return false;
			}
			if (this.ParentObject.IsPlayerControlled() || this.ParentObject.GetIntProperty("TurnsAsPlayerMinion", 0) > 0)
			{
				this.ParentObject.RemovePart(this);
				return false;
			}
			if (this.ParentObject.CurrentZone.ZoneID == this.beyLah.ZoneID || this.ParentObject.CurrentZone.IsActive())
			{
				if (this.Destination == null)
				{
					this.ParentObject.Brain.PushGoal(new MoveToZone(this.ParentObject.CurrentZone.GetZoneIDFromDirection("W", 1), false, -1, 95));
					this.ParentObject.Brain.StartingCell = null;
				}
				else
				{
					this.ParentObject.Brain.PushGoal(new MoveToGlobal(this.Destination));
					this.ParentObject.Brain.StartingCell = this.Destination;
				}
			}
			else if (this.Destination == null)
			{
				this.ParentObject.Destroy(null, false, false, null);
			}
			else if (this.ParentObject.CurrentZone.ZoneID != this.Destination.ZoneID)
			{
				this.ParentObject.Brain.PushGoal(new MoveToGlobal(this.Destination));
			}
			return true;
		}

		// Token: 0x040042FC RID: 17148
		public GlobalLocation Destination;
	}
}
