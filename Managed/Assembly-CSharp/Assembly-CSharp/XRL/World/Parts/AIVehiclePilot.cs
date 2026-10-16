using System;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020012AA RID: 4778
	[Serializable]
	public class AIVehiclePilot : AIBehaviorPart
	{
		// Token: 0x0600BCB8 RID: 48312 RVA: 0x0045152C File Offset: 0x0044F72C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BCB9 RID: 48313 RVA: 0x0045152F File Offset: 0x0044F72F
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!this.ParentObject.IsBusy() && this.ParentObject.Target == null && !this.ParentObject.IsPlayer())
			{
				this.TryPilotVehicle(false);
			}
		}

		// Token: 0x0600BCBA RID: 48314 RVA: 0x00451560 File Offset: 0x0044F760
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BCBB RID: 48315 RVA: 0x00451563 File Offset: 0x0044F763
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (this.OnCombatStart)
			{
				Registrar.Register("AICreateKill");
			}
		}

		// Token: 0x0600BCBC RID: 48316 RVA: 0x00451578 File Offset: 0x0044F778
		public GameObject FindVehicle(bool Search = false)
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			if (interiorZone != null && this.IsValidVehicle(interiorZone.ParentObject))
			{
				return interiorZone.ParentObject;
			}
			if (Search)
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.YieldAdjacentCells(this.SearchRadius, true, true))
				{
					foreach (GameObject gameObject in cell.Objects)
					{
						if (this.IsValidVehicle(gameObject))
						{
							return gameObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600BCBD RID: 48317 RVA: 0x00451648 File Offset: 0x0044F848
		public bool TryPilotVehicle(bool Search = false)
		{
			this.Vehicle = this.FindVehicle(Search);
			GameObject vehicle = this.Vehicle;
			this.Interior = ((vehicle != null) ? vehicle.GetPart<Interior>() : null);
			if (this.Vehicle != null && this.Interior != null)
			{
				if (this.Goal == null)
				{
					this.Goal = new DelegateGoal(new Action<GoalHandler>(this.TakeAction), null, null);
				}
				this.Goal.SetCanFight = new bool?(false);
				this.ParentObject.Brain.Goals.Clear();
				this.ParentObject.Brain.PushGoal(this.Goal);
				return true;
			}
			return false;
		}

		// Token: 0x0600BCBE RID: 48318 RVA: 0x004516EB File Offset: 0x0044F8EB
		public override bool FireEvent(Event E)
		{
			return (!this.OnCombatStart || !(E.ID == "AICreateKill") || this.ParentObject.AreHostilesAdjacent(true) || !this.TryPilotVehicle(true)) && base.FireEvent(E);
		}

		// Token: 0x0600BCBF RID: 48319 RVA: 0x00451728 File Offset: 0x0044F928
		public bool IsValidVehicle(GameObject Object)
		{
			Vehicle vehicle = ((Object != null) ? Object.GetPart<Vehicle>() : null);
			return vehicle != null && vehicle.PilotID == null && vehicle.CanBePilotedBy(this.ParentObject) && ((!this.Blueprint.IsNullOrEmpty() && Object.Blueprint == this.Blueprint) || (!this.Tag.IsNullOrEmpty() && Object.HasTagOrProperty(this.Tag)) || (!this.Type.IsNullOrEmpty() && vehicle.Type == this.Type));
		}

		// Token: 0x0600BCC0 RID: 48320 RVA: 0x004517C1 File Offset: 0x0044F9C1
		public bool IsValidSeat(GameObject Object)
		{
			return Object.HasPart(typeof(VehicleSeat)) && Object.CurrentCell.IsPassable(this.ParentObject, true);
		}

		// Token: 0x0600BCC1 RID: 48321 RVA: 0x004517EC File Offset: 0x0044F9EC
		public void TakeAction(GoalHandler Handler)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (this.ParentObject.IsPlayer())
			{
				Handler.Pop();
				return;
			}
			if (currentZone.ZoneID != this.Interior.ZoneID)
			{
				Handler.PushChildGoal(new MoveToInterior(this.Vehicle));
				return;
			}
			Sitting sitting;
			if (this.ParentObject.TryGetEffect<Sitting>(out sitting))
			{
				if (!GameObject.Validate(sitting.SittingOn) || sitting.SittingOn.DistanceTo(this.ParentObject) >= 1 || !this.ParentObject.HasEffect(typeof(Piloting)))
				{
					this.ParentObject.RemoveEffect(sitting, true);
					return;
				}
			}
			else
			{
				currentZone.MarkActive();
				currentZone.Activated();
				GameObject gameObject = currentZone.FindClosestObject(this.ParentObject, new Predicate<GameObject>(this.IsValidSeat), false, true, false);
				if (gameObject != null)
				{
					if (this.ParentObject.DistanceTo(gameObject) <= 1)
					{
						gameObject.GetPart<Chair>().SitDown(this.ParentObject, null);
						return;
					}
					Handler.PushChildGoal(new MoveTo(gameObject, false, false, 1, false, false, false, -1, -1, 95));
					return;
				}
				else
				{
					Handler.PushChildGoal(new MoveToExterior());
				}
			}
		}

		// Token: 0x04004BA7 RID: 19367
		public string Blueprint;

		// Token: 0x04004BA8 RID: 19368
		public string Tag;

		// Token: 0x04004BA9 RID: 19369
		public string Type;

		// Token: 0x04004BAA RID: 19370
		public int SearchRadius = 15;

		// Token: 0x04004BAB RID: 19371
		public bool OnCombatStart = true;

		// Token: 0x04004BAC RID: 19372
		[NonSerialized]
		private DelegateGoal Goal;

		// Token: 0x04004BAD RID: 19373
		[NonSerialized]
		private GameObject Vehicle;

		// Token: 0x04004BAE RID: 19374
		[NonSerialized]
		private Interior Interior;
	}
}
