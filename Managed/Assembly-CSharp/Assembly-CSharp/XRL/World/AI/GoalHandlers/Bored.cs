using System;
using XRL.Collections;
using XRL.Rules;
using XRL.World.AI.Pathfinding;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B7A RID: 2938
	[Serializable]
	public class Bored : GoalHandler
	{
		// Token: 0x060075A6 RID: 30118 RVA: 0x002C2452 File Offset: 0x002C0652
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x060075A7 RID: 30119 RVA: 0x002C2455 File Offset: 0x002C0655
		public override bool IsBusy()
		{
			return false;
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x002C2458 File Offset: 0x002C0658
		public bool TryMovementAbilities(Cell TargetCell, int Distance = -1, int StandoffDistance = 0)
		{
			if (GameObject.Validate(base.ParentObject) && AICommandList.HandleCommandList(AIGetMovementAbilityListEvent.GetFor(base.ParentObject, null, TargetCell, Distance, StandoffDistance), base.ParentObject, null, TargetCell, 0))
			{
				base.Think("Did a movement ability with a target cell");
				return true;
			}
			return false;
		}

		// Token: 0x060075A9 RID: 30121 RVA: 0x002C2494 File Offset: 0x002C0694
		public bool TryMovementAbilities()
		{
			if (GameObject.Validate(base.ParentObject) && AICommandList.HandleCommandList(AIGetMovementAbilityListEvent.GetFor(base.ParentObject, null, null, -1, 0), base.ParentObject, null, null, 0))
			{
				base.Think("Did a movement ability");
				return true;
			}
			return false;
		}

		// Token: 0x060075AA RID: 30122 RVA: 0x002C24D0 File Offset: 0x002C06D0
		public bool TryPassiveAbilities()
		{
			if (GameObject.Validate(base.ParentObject) && AICommandList.HandleCommandList(AIGetPassiveAbilityListEvent.GetFor(base.ParentObject, null, -1), base.ParentObject, null, null, 0))
			{
				base.Think("Did a passive ability");
				return true;
			}
			return false;
		}

		// Token: 0x060075AB RID: 30123 RVA: 0x002C250A File Offset: 0x002C070A
		public bool TryPassiveItems()
		{
			if (GameObject.Validate(base.ParentObject) && AICommandList.HandleCommandList(AIGetPassiveItemListEvent.GetFor(base.ParentObject, null, -1), base.ParentObject, null, null, 0))
			{
				base.Think("Did a passive item");
				return true;
			}
			return false;
		}

		// Token: 0x060075AC RID: 30124 RVA: 0x002C2544 File Offset: 0x002C0744
		public void TakeActionWithPartyLeader()
		{
			if (base.ParentObject == null || this.ParentBrain == null)
			{
				return;
			}
			GameObject partyLeader = this.ParentBrain.PartyLeader;
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null)
			{
				return;
			}
			if (this.ParentBrain.GoToPartyLeader())
			{
				return;
			}
			Zone parentZone = currentCell.ParentZone;
			if (parentZone == null || !parentZone.IsActive())
			{
				return;
			}
			if (this.ParentBrain.Target == null && this.ParentBrain.CanAcquireTarget())
			{
				if (partyLeader.IsPlayerControlled())
				{
					GameObject target = partyLeader.Target;
					if (target != null && this.ParentBrain.CheckPerceptionOf(target))
					{
						this.ParentBrain.WantToKill(target, "to boredly aid my leader", false);
						if (this.ParentBrain.Target != null)
						{
							return;
						}
					}
				}
				GameObject gameObject = this.ParentBrain.FindProspectiveTarget(currentCell, false);
				if (gameObject != null)
				{
					this.ParentBrain.WantToKill(gameObject, "out of bored hostility while having a party leader", false);
					if (this.ParentBrain.Target != null)
					{
						return;
					}
				}
				else
				{
					base.Think("I boredly looked for a target while having a party leader, but didn't find one.");
				}
			}
			if (this.TryMovementAbilities())
			{
				return;
			}
			if (this.TryPassiveAbilities())
			{
				return;
			}
			if (this.TryPassiveItems())
			{
				return;
			}
			if (!base.ParentObject.IsMobile())
			{
				base.ParentObject.UseEnergy(1000);
				return;
			}
			if (this.ParentBrain.Staying)
			{
				bool flag = false;
				if (!this.ParentBrain.Wanders && !this.ParentBrain.WandersRandomly)
				{
					Brain parentBrain = this.ParentBrain;
					Cell cell;
					if (parentBrain == null)
					{
						cell = null;
					}
					else
					{
						GlobalLocation startingCell = parentBrain.StartingCell;
						cell = ((startingCell != null) ? startingCell.ResolveCell() : null);
					}
					Cell cell2 = cell;
					if (cell2 != null && currentCell != cell2 && cell2.ParentZone == parentZone && base.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
					{
						this.PushChildGoal(new MoveTo(parentZone.ZoneID, cell2.X, cell2.Y, true, false, 0, false, false, false, -1, -1, 95));
						flag = true;
					}
				}
				if (!flag)
				{
					base.ParentObject.UseEnergy(1000);
					return;
				}
			}
			else if (The.Game.TimeTicks > this.NextLeaderPathFind)
			{
				Cell currentCell2 = partyLeader.CurrentCell;
				if (currentCell2 == null || currentCell2.ParentZone == null || currentCell2.OnWorldMap())
				{
					return;
				}
				int @for = GetPartyLeaderFollowDistanceEvent.GetFor(base.ParentObject, partyLeader, null);
				int num = base.ParentObject.DistanceTo(currentCell2);
				if (num > @for)
				{
					if (this.TryMovementAbilities(currentCell2, num, @for))
					{
						return;
					}
					FindPath findPath = new FindPath(currentCell, currentCell2, false, true, base.ParentObject, 95, false, false, false, false, false, null);
					if (findPath.Usable)
					{
						this.NextLeaderPathFind = The.Game.TimeTicks;
						this.PathTowardLeader(findPath, @for);
						return;
					}
					if (base.ParentObject.IsPlayerLed())
					{
						this.NextLeaderPathFind = The.Game.TimeTicks + 2L;
					}
					else
					{
						this.NextLeaderPathFind = The.Game.TimeTicks + (long)Stat.Random(50, 100);
					}
					if (The.Game.TimeTicks > this.NextLeaderAltPathFind)
					{
						Cell closestPassableCellFor = currentCell2.getClosestPassableCellFor(base.ParentObject);
						if (closestPassableCellFor != null && closestPassableCellFor != currentCell2)
						{
							FindPath findPath2 = new FindPath(currentCell, closestPassableCellFor, false, true, base.ParentObject, 95, false, false, false, false, false, null);
							if (findPath2.Usable)
							{
								this.NextLeaderPathFind = The.Game.TimeTicks + 2L;
								this.PathTowardLeader(findPath2, @for);
								return;
							}
							if (base.ParentObject.IsPlayerLed())
							{
								this.NextLeaderAltPathFind = The.Game.TimeTicks + 10L;
								return;
							}
							this.NextLeaderAltPathFind = The.Game.TimeTicks + (long)Stat.Random(100, 200);
							return;
						}
					}
				}
			}
			else
			{
				base.Think("Waiting for party leader");
				base.ParentObject.UseEnergy(1000);
			}
		}

		// Token: 0x060075AD RID: 30125 RVA: 0x002C28F8 File Offset: 0x002C0AF8
		private void PathTowardLeader(FindPath PathFinder, int Distance = 1)
		{
			int i = PathFinder.Steps.Count - Distance - 1;
			while (i >= 1)
			{
				if (PathFinder.Steps[i].IsEmpty())
				{
					if (this.TryMovementAbilities(PathFinder.Steps[i], -1, 0))
					{
						return;
					}
					break;
				}
				else
				{
					i--;
				}
			}
			for (int j = Math.Min(Stat.Random(0, 4), PathFinder.Directions.Count - Distance - 1); j >= 0; j--)
			{
				this.PushChildGoal(new Step(PathFinder.Directions[j], false, false, false, false, null, false));
			}
		}

		// Token: 0x060075AE RID: 30126 RVA: 0x002C298C File Offset: 0x002C0B8C
		public override void TakeAction()
		{
			GameObject parentObject = base.ParentObject;
			Cell cell = ((parentObject != null) ? parentObject.CurrentCell : null);
			if (cell != null)
			{
				GameObject player = The.Player;
				if (((player != null) ? player.CurrentCell : null) != null)
				{
					if (base.ParentObject.IsAflame())
					{
						int num = this.ExtinguishSelfTries + 1;
						this.ExtinguishSelfTries = num;
						if (num < 5)
						{
							this.PushChildGoal(new ExtinguishSelf());
							return;
						}
					}
					base.Think("I'm bored.");
					if (!AIBoredEvent.Check(base.ParentObject))
					{
						return;
					}
					if (base.ParentObject.Energy.Value < 1000)
					{
						return;
					}
					if (this.ParentBrain.PartyLeader != null)
					{
						this.TakeActionWithPartyLeader();
						return;
					}
					if (this.ParentBrain.Target == null)
					{
						GameObject gameObject = this.ParentBrain.FindProspectiveTarget(cell, false);
						if (gameObject != null)
						{
							this.ParentBrain.WantToKill(gameObject, "out of bored hostility", false);
							if (this.ParentBrain.Target != null)
							{
								return;
							}
						}
						else
						{
							base.Think("I boredly looked for a target, but didn't find one.");
						}
					}
					if (this.TryMovementAbilities())
					{
						return;
					}
					if (this.TryPassiveAbilities())
					{
						return;
					}
					if (this.TryPassiveItems())
					{
						return;
					}
					if (base.ParentObject == null)
					{
						return;
					}
					Brain parentBrain = this.ParentBrain;
					Cell cell2;
					if (parentBrain == null)
					{
						cell2 = null;
					}
					else
					{
						GlobalLocation startingCell = parentBrain.StartingCell;
						cell2 = ((startingCell != null) ? startingCell.ResolveCell() : null);
					}
					Cell cell3 = cell2;
					if (cell3 != null && !this.ParentBrain.Wanders && !this.ParentBrain.WandersRandomly && cell != cell3 && base.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
					{
						this.PushChildGoal(new MoveTo(cell3.ParentZone.ZoneID, cell3.X, cell3.Y, true, false, 0, false, false, false, -1, -1, 95));
						return;
					}
					if (!base.ParentObject.InSameZone(The.Player))
					{
						this.PushChildGoal(new Wait(Stat.Random(1, 20), "I'm not in the same zone as the player"));
						return;
					}
					if (base.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
					{
						if (this.ParentBrain.Wanders)
						{
							if ((base.ParentObject.HasTagOrProperty("Restless") && !base.ParentObject.HasTagOrProperty("Social")) || 10.in100())
							{
								this.PushChildGoal(new Wander());
								return;
							}
						}
						else if (this.ParentBrain.WandersRandomly && ((base.ParentObject.HasTagOrProperty("Restless") && !base.ParentObject.HasTagOrProperty("Social")) || 20.in100()))
						{
							this.PushChildGoal(new WanderRandomly(5));
							return;
						}
						if (base.ParentObject.HasTagOrProperty("AllowIdleBehavior") && !base.ParentObject.HasTagOrProperty("PreventIdleBehavior"))
						{
							Rack<GameObject> idleObjects = Bored.IdleObjects;
							Zone parentZone = cell.ParentZone;
							if (Bored.IdleDirty || Bored.IdleZone != parentZone)
							{
								idleObjects.Clear();
								foreach (GameObject gameObject2 in parentZone.IterateObjects())
								{
									if (gameObject2.WantEvent(PooledEvent<IdleQueryEvent>.ID, MinEvent.CascadeLevel))
									{
										Bored.IdleObjects.Add(gameObject2);
									}
								}
								Bored.IdleDirty = false;
							}
							if (!idleObjects.IsNullOrEmpty<GameObject>())
							{
								idleObjects.ShuffleInPlace(null);
								IdleQueryEvent idleQueryEvent = IdleQueryEvent.FromPool(this.ParentBrain.ParentObject);
								for (int i = idleObjects.Count - 1; i >= 0; i--)
								{
									GameObject gameObject3 = idleObjects[i];
									if (!gameObject3.IsValid())
									{
										idleObjects.RemoveAt(i);
									}
									else if (!gameObject3.HandleEvent(idleQueryEvent))
									{
										base.ParentObject.UseEnergy(1000);
										return;
									}
								}
							}
						}
						this.PushChildGoal(new Wait(Stat.Random(1, 10), "I couldn't find anything to do"));
						return;
					}
					this.PushChildGoal(new Wait(Stat.Random(1, 10), "I couldn't perform independent behavior"));
					return;
				}
			}
		}

		// Token: 0x040035DF RID: 13791
		public static Rack<GameObject> IdleObjects = new Rack<GameObject>();

		// Token: 0x040035E0 RID: 13792
		public static Zone IdleZone;

		// Token: 0x040035E1 RID: 13793
		public static bool IdleDirty;

		// Token: 0x040035E2 RID: 13794
		public int ExtinguishSelfTries;

		// Token: 0x040035E3 RID: 13795
		public long NextLeaderPathFind;

		// Token: 0x040035E4 RID: 13796
		public long NextLeaderAltPathFind;
	}
}
