using System;
using XRL.Rules;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001205 RID: 4613
	[HasWishCommand]
	[Serializable]
	public class AIBarathrumShuttle : AIBehaviorPart
	{
		// Token: 0x0600B780 RID: 46976 RVA: 0x0042C6A6 File Offset: 0x0042A8A6
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == PooledEvent<AIBoredEvent>.ID || ID == GetZoneSuspendabilityEvent.ID || ID == BeginMentalDefendEvent.ID;
		}

		// Token: 0x0600B781 RID: 46977 RVA: 0x0042C6C2 File Offset: 0x0042A8C2
		public override bool HandleEvent(AIBoredEvent E)
		{
			return !this.PushGoal(E.Actor) && base.HandleEvent(E);
		}

		// Token: 0x0600B782 RID: 46978 RVA: 0x0042C6DB File Offset: 0x0042A8DB
		public override bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			E.Suspendability = Suspendability.Pinned;
			return false;
		}

		// Token: 0x0600B783 RID: 46979 RVA: 0x0042C6E5 File Offset: 0x0042A8E5
		public override bool HandleEvent(BeginMentalDefendEvent E)
		{
			return (E.Defender != this.ParentObject || !(E.Command == "Proselytize")) && base.HandleEvent(E);
		}

		// Token: 0x0600B784 RID: 46980 RVA: 0x0042C710 File Offset: 0x0042A910
		public bool PushGoal(GameObject Actor)
		{
			if (Actor == this.ParentObject)
			{
				if (this.Stage >= 0 && Actor.PartyLeader != null)
				{
					return false;
				}
				if (this.Goal == null || !Actor.Brain.Goals.Contains(this.Goal))
				{
					if (this.Goal == null)
					{
						this.Goal = new DelegateGoal(new Action<GoalHandler>(this.Action), null, null);
					}
					Actor.Brain.PushGoal(this.Goal);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B785 RID: 46981 RVA: 0x0042C790 File Offset: 0x0042A990
		public void Action(GoalHandler Handler)
		{
			if (this.Stage == -1)
			{
				this.ActionIdle(Handler);
				return;
			}
			if (this.Stage == 0)
			{
				this.ActionMoverCentral(Handler);
				return;
			}
			if (this.Stage == 1)
			{
				this.ActionTravelQuay(Handler);
				return;
			}
			if (this.Stage == 2)
			{
				this.ActionShipQuay(Handler);
				return;
			}
			if (this.Stage == 3)
			{
				this.ActionShipLaunch(Handler);
			}
		}

		// Token: 0x0600B786 RID: 46982 RVA: 0x0042C7F0 File Offset: 0x0042A9F0
		public void ActionIdle(GoalHandler Handler)
		{
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
				GameObject gameObject = this.ParentObject.CurrentZone.FindClosestObject(this.ParentObject, new Predicate<GameObject>(this.IsValidSeat), false, true, false);
				if (gameObject == null)
				{
					Handler.PushChildGoal(new WanderRandomly(5));
					Handler.PushChildGoal(new Wait(5));
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

		// Token: 0x0600B787 RID: 46983 RVA: 0x0042C8C8 File Offset: 0x0042AAC8
		public void ActionMoverCentral(GoalHandler Handler)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			InteriorZone interiorZone = currentZone as InteriorZone;
			if (interiorZone != null)
			{
				GameObject parentObject = interiorZone.ParentObject;
				GameObject gameObject = interiorZone.FindObject((GameObject x) => x.HasPart<VehicleControl>());
				VehicleControl vehicleControl;
				Vehicle vehicle;
				if (parentObject == null || gameObject == null || parentObject.Blueprint != "Mover" || !gameObject.TryGetPart<VehicleControl>(out vehicleControl) || !parentObject.TryGetPart<Vehicle>(out vehicle) || (vehicle.PilotID != null && vehicle.PilotID != this.ParentObject.ID))
				{
					Handler.PushChildGoal(new MoveToExterior());
					return;
				}
				if (this.ParentObject.DistanceTo(gameObject) > 1)
				{
					Handler.PushChildGoal(new MoveTo(gameObject, false, false, 1, false, false, false, -1, -1, 95));
					return;
				}
				if (vehicle.PilotID == this.ParentObject.ID)
				{
					Handler.PushChildGoal(new Wait(10));
					return;
				}
				if (vehicleControl.AttemptPilot(this.ParentObject))
				{
					this.Stage = 1;
					this.StartTravel(parentObject);
					return;
				}
			}
			else
			{
				GameObject gameObject2 = currentZone.FindObject(new Predicate<GameObject>(AIBarathrumShuttle.IsFreeMover));
				if (gameObject2 != null)
				{
					Handler.PushChildGoal(new MoveToInterior(gameObject2));
					return;
				}
				if (currentZone.ZoneID != "NorthSheva.39.11.2.2.8")
				{
					Handler.PushChildGoal(new MoveToZone("NorthSheva.39.11.2.2.8", false, -1, 95));
					return;
				}
			}
			Handler.PushChildGoal(new Wait(Stat.Random(5, 25)));
		}

		// Token: 0x0600B788 RID: 46984 RVA: 0x0042CA40 File Offset: 0x0042AC40
		public void ActionTravelQuay(GoalHandler Handler)
		{
			if (this.ParentObject.HasEffect<Piloting>())
			{
				Handler.PushChildGoal(new Wait(Stat.Random(5, 25)));
				return;
			}
			this.ActionIdle(Handler);
		}

		// Token: 0x0600B789 RID: 46985 RVA: 0x0042CA6C File Offset: 0x0042AC6C
		public void ActionShipQuay(GoalHandler Handler)
		{
			Zone parentZone = this.ParentObject.CurrentCell.ParentZone;
			if (parentZone is InteriorZone)
			{
				if (parentZone.ZoneID.Contains("ShevaStarship"))
				{
					this.Stage = 3;
					return;
				}
				Handler.PushChildGoal(new MoveToExterior());
				return;
			}
			else
			{
				if (parentZone.ZoneID != "NorthSheva.66.13.0.1.10")
				{
					Handler.PushChildGoal(new MoveToZone("NorthSheva.66.13.0.1.10", false, -1, 95));
					return;
				}
				GameObject gameObject = parentZone.FindFirstObject(delegate(GameObject x)
				{
					Interior interior;
					return x.TryGetPart<Interior>(out interior) && interior.Cell == "ShevaStarship";
				});
				if (gameObject != null)
				{
					Handler.PushChildGoal(new MoveToInterior(gameObject));
					return;
				}
				Handler.PushChildGoal(new Wait(Stat.Random(5, 25)));
				return;
			}
		}

		// Token: 0x0600B78A RID: 46986 RVA: 0x0042CB28 File Offset: 0x0042AD28
		public void ActionShipLaunch(GoalHandler Handler)
		{
			InteriorZone interiorZone = this.ParentObject.CurrentCell.ParentZone as InteriorZone;
			if (interiorZone == null || !interiorZone.ZoneID.Contains("ShevaStarship"))
			{
				this.Stage = 2;
				return;
			}
			GameObject gameObject = interiorZone.FindFirstObject((GameObject x) => x.HasPart(typeof(ShevaStarshipControl)));
			if (gameObject == null)
			{
				Handler.PushGoal(new Wait(5));
				return;
			}
			if (gameObject.DistanceTo(this.ParentObject) > 1)
			{
				Handler.PushGoal(new MoveTo(gameObject, false, false, 1, false, false, false, -1, -1, 95));
				return;
			}
			this.ParentObject.UseEnergy(1000);
			ShevaStarshipControl shevaStarshipControl;
			if (gameObject.TryGetPart<ShevaStarshipControl>(out shevaStarshipControl) && !shevaStarshipControl.IsStarted && !shevaStarshipControl.IsFinished)
			{
				base.DidXToY("start", "interfacing with", gameObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, true, null);
				if (!shevaStarshipControl.AttemptLaunch(this.ParentObject))
				{
					Handler.PushGoal(new Wait(20));
				}
			}
		}

		// Token: 0x0600B78B RID: 46987 RVA: 0x0042CC30 File Offset: 0x0042AE30
		public void StartTravel(GameObject Object)
		{
			Zone currentZone = Object.CurrentZone;
			if (!currentZone.IsWorldMap())
			{
				Cell worldCell = currentZone.GetWorldCell();
				if (worldCell != null)
				{
					GameObject terrainObject = currentZone.GetTerrainObject();
					if (((terrainObject != null) ? terrainObject.Blueprint : null) != "TerrainStarfarersQuayStop" && !worldCell.ParentZone.Stale)
					{
						Object.SystemMoveTo(worldCell, null, false, true, false, false);
						if (!Object.HasPart<AIWorldMapTravel>())
						{
							AIWorldMapTravel aiworldMapTravel = new AIWorldMapTravel();
							if (aiworldMapTravel.SetTerrain(worldCell.ParentZone, "TerrainStarfarersQuayStop"))
							{
								aiworldMapTravel.Cardinal = true;
								aiworldMapTravel.Pinned = true;
								Object.AddPart<AIWorldMapTravel>(aiworldMapTravel, true, false);
								Cell cell;
								if (!The.Player.OnWorldMap())
								{
									GameObject terrainObject2 = The.ActiveZone.GetTerrainObject();
									cell = ((terrainObject2 != null) ? terrainObject2.CurrentCell : null);
								}
								else
								{
									cell = The.Player.CurrentCell;
								}
								Cell cell2 = cell;
								if (cell2 != null)
								{
									int num = 0;
									while (cell2.ManhattanDistanceTo(Object.CurrentCell) < 2 && num++ < 20)
									{
										aiworldMapTravel.TakeAction();
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600B78C RID: 46988 RVA: 0x0042CD38 File Offset: 0x0042AF38
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B78D RID: 46989 RVA: 0x0042CD3C File Offset: 0x0042AF3C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Stage == 1)
			{
				InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
				if (interiorZone != null)
				{
					GameObject parentObject = interiorZone.ParentObject;
					Zone zone = ((parentObject != null) ? parentObject.CurrentZone : null);
					if (zone != null && !zone.IsWorldMap())
					{
						GameObject terrainObject = zone.GetTerrainObject();
						if (((terrainObject != null) ? terrainObject.Blueprint : null) == "TerrainStarfarersQuayStop")
						{
							Piloting piloting;
							if (this.ParentObject.TryGetEffect<Piloting>(out piloting))
							{
								piloting.Stop();
							}
							this.Stage = 2;
						}
					}
				}
			}
		}

		// Token: 0x0600B78E RID: 46990 RVA: 0x0042CDC0 File Offset: 0x0042AFC0
		public static bool IsFreeMover(GameObject Object)
		{
			Vehicle vehicle;
			Interior interior;
			return Object.Blueprint == "Mover" && Object.TryGetPart<Vehicle>(out vehicle) && vehicle.PilotID == null && Object.TryGetPart<Interior>(out interior) && (!interior.IsZoneLive || !interior.Zone.IsActive());
		}

		// Token: 0x0600B78F RID: 46991 RVA: 0x0042CE15 File Offset: 0x0042B015
		public bool IsValidSeat(GameObject Object)
		{
			return Object.HasPart(typeof(Chair)) && Object.CurrentCell.IsPassable(this.ParentObject, true);
		}

		// Token: 0x0600B790 RID: 46992 RVA: 0x0042CE40 File Offset: 0x0042B040
		[WishCommand("launchbarathrum", null)]
		public static void WishBarathrumLaunch()
		{
			GameObject gameObject = GameObject.Create("Starship 1 Platform N");
			GameObject gameObject2 = GameObject.Create("Barathrum");
			The.ActiveZone.GetCell(0, 0).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			The.ActiveZone.GetCell(0, 0).AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Interior part = gameObject.GetPart<Interior>();
			part.TryEnter(The.Player, true, true);
			part.TryEnter(gameObject2, true, true);
			gameObject2.RequirePart<AIBarathrumShuttle>(false).Stage = 3;
		}

		// Token: 0x040049CA RID: 18890
		public const string ID_MOVER_CENTRAL = "NorthSheva.39.11.2.2.8";

		// Token: 0x040049CB RID: 18891
		public const string ID_SHIP_QUAY = "NorthSheva.66.13.0.1.10";

		// Token: 0x040049CC RID: 18892
		public const string ID_SHIP_CELL = "ShevaStarship";

		// Token: 0x040049CD RID: 18893
		public const string TERRAIN_QUAY = "TerrainStarfarersQuayStop";

		// Token: 0x040049CE RID: 18894
		public const int STAGE_ASCENDING = -1;

		// Token: 0x040049CF RID: 18895
		public const int STAGE_MOVER_CENTRAL = 0;

		// Token: 0x040049D0 RID: 18896
		public const int STAGE_TRAVELING_QUAY = 1;

		// Token: 0x040049D1 RID: 18897
		public const int STAGE_SHIP_QUAY = 2;

		// Token: 0x040049D2 RID: 18898
		public const int STAGE_SHIP_LAUNCH = 3;

		// Token: 0x040049D3 RID: 18899
		public int Stage = -1;

		// Token: 0x040049D4 RID: 18900
		public int Timer;

		// Token: 0x040049D5 RID: 18901
		[NonSerialized]
		private DelegateGoal Goal;
	}
}
