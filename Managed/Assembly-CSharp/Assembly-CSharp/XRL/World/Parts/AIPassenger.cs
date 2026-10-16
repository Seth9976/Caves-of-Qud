using System;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E24 RID: 3620
	[Serializable]
	public class AIPassenger : AIBehaviorPart
	{
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06009340 RID: 37696 RVA: 0x00350B84 File Offset: 0x0034ED84
		public Interior Interior
		{
			get
			{
				Interior interior;
				if ((interior = this._Interior) == null)
				{
					GameObject @object = this.Vehicle.Object;
					interior = (this._Interior = ((@object != null) ? @object.GetPart<Interior>() : null));
				}
				return interior;
			}
		}

		// Token: 0x06009341 RID: 37697 RVA: 0x00350BBB File Offset: 0x0034EDBB
		public AIPassenger()
		{
		}

		// Token: 0x06009342 RID: 37698 RVA: 0x00350BCA File Offset: 0x0034EDCA
		public AIPassenger(GameObject Vehicle, bool Removable = true)
		{
			this.Vehicle = Vehicle.Reference();
			this.Removable = Removable;
		}

		// Token: 0x06009343 RID: 37699 RVA: 0x00350BEC File Offset: 0x0034EDEC
		public bool CheckPassengerSeat()
		{
			if (this.ParentObject.IsBusy())
			{
				return true;
			}
			if (this.ParentObject.IsPlayer() || (this.ParentObject.PartyLeader != null && !this.ParentObject.Brain.Staying))
			{
				return false;
			}
			if (!this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				return false;
			}
			Interior interior = this.Interior;
			if (!GameObject.Validate((interior != null) ? interior.ParentObject : null))
			{
				this._Interior = null;
				return false;
			}
			if (this.Goal == null)
			{
				this.Goal = new DelegateGoal(new Action<GoalHandler>(this.TakeAction), null, null);
			}
			if (!this.ParentObject.Brain.Goals.Contains(this.Goal))
			{
				this.Goal.SetCanFight = new bool?(false);
				this.Goal.SetNonAggressive = new bool?(true);
				this.ParentObject.Brain.PushGoal(this.Goal);
			}
			return true;
		}

		// Token: 0x06009344 RID: 37700 RVA: 0x00350CE4 File Offset: 0x0034EEE4
		public bool IsValidSeat(GameObject Object)
		{
			return Object.HasPart(typeof(Chair)) && Object.CurrentCell.IsPassable(this.ParentObject, true);
		}

		// Token: 0x06009345 RID: 37701 RVA: 0x00350D0C File Offset: 0x0034EF0C
		public void TakeAction(GoalHandler Handler)
		{
			if (!this.End)
			{
				Interior interior = this.Interior;
				if (!GameObject.Validate((interior != null) ? interior.ParentObject : null))
				{
					this.End = true;
				}
			}
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone.ZoneID != this.Interior.ZoneID)
			{
				if (this.End)
				{
					this.ParentObject.Brain.Staying = false;
					Handler.Pop();
					if (this.Removable)
					{
						this.ParentObject.RemovePart(this);
						return;
					}
				}
				else
				{
					Handler.PushChildGoal(new MoveToInterior(this.Vehicle.Object));
				}
				return;
			}
			this.Goal.SetCanFight = null;
			this.Goal.SetNonAggressive = null;
			if (this.End)
			{
				Handler.PushChildGoal(new MoveToExterior());
				return;
			}
			Sitting sitting;
			if (this.ParentObject.TryGetEffect<Sitting>(out sitting))
			{
				if (!GameObject.Validate(sitting.SittingOn) || sitting.SittingOn.DistanceTo(this.ParentObject) >= 1)
				{
					this.ParentObject.RemoveEffect(sitting, true);
					return;
				}
				this.ParentObject.UseEnergy(1000);
				return;
			}
			else
			{
				GameObject gameObject = currentZone.FindClosestObject(this.ParentObject, new Predicate<GameObject>(this.IsValidSeat), false, true, false);
				if (gameObject == null)
				{
					this.End = true;
					Handler.PushChildGoal(new MoveToExterior());
					return;
				}
				if (this.ParentObject.DistanceTo(gameObject) <= 1)
				{
					gameObject.GetPart<Chair>().SitDown(this.ParentObject, null);
					return;
				}
				Handler.PushChildGoal(new MoveTo(gameObject, false, false, 1, false, false, false, -1, -1, 95));
				return;
			}
		}

		// Token: 0x06009346 RID: 37702 RVA: 0x00350EA1 File Offset: 0x0034F0A1
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009347 RID: 37703 RVA: 0x00350EA4 File Offset: 0x0034F0A4
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.ParentObject == null)
			{
				return;
			}
			if (this.ParentObject.IsPlayer() || (this.ParentObject.PartyLeader != null && !this.ParentObject.Brain.Staying))
			{
				this.End = true;
			}
			if (!this.CheckPassengerSeat() && this.Removable)
			{
				GameObject partyLeader = this.ParentObject.Brain.PartyLeader;
				if (GameObject.Validate(ref partyLeader) && this.ParentObject.Brain.Staying && this.ParentObject.InSameZone(partyLeader))
				{
					this.ParentObject.Brain.Staying = false;
				}
				this.ParentObject.RemovePart(this);
			}
		}

		// Token: 0x04003CE2 RID: 15586
		public GameObjectReference Vehicle;

		// Token: 0x04003CE3 RID: 15587
		public bool Removable = true;

		// Token: 0x04003CE4 RID: 15588
		public bool End;

		// Token: 0x04003CE5 RID: 15589
		[NonSerialized]
		public Interior _Interior;

		// Token: 0x04003CE6 RID: 15590
		[NonSerialized]
		private DelegateGoal Goal;
	}
}
