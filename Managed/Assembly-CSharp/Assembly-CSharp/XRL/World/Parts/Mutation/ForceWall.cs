using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013EA RID: 5098
	[Serializable]
	public class ForceWall : BaseMutation
	{
		// Token: 0x0600C541 RID: 50497 RVA: 0x0047D172 File Offset: 0x0047B372
		public ForceWall()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C542 RID: 50498 RVA: 0x0047D185 File Offset: 0x0047B385
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetRetreatAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C543 RID: 50499 RVA: 0x0047D1AC File Offset: 0x0047B3AC
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				GameObject actor = E.Actor;
				Cell cell = ((actor != null) ? actor.CurrentCell : null);
				if (cell != null && cell.ParentZone != null)
				{
					GameObject gameObject = cell.ParentZone.FastSquareVisibilityFirst(cell.X, cell.Y, 25, "ForceWallTarget", E.Actor, false, false, null, 9999999, int.MinValue);
					if (gameObject != null && E.Distance < E.Actor.DistanceTo(gameObject))
					{
						E.Add("CommandForceWall", 1, null, false, false, null, null);
					}
					else if (GameObject.Validate(E.Target))
					{
						Cell currentCell = E.Target.CurrentCell;
						if (currentCell != null && currentCell.ParentZone != null && !currentCell.ParentZone.FastSquareVisibilityAny(currentCell.X, currentCell.Y, 5, "Forcefield", E.Actor, false, false, null))
						{
							E.Add("CommandForceWall", 1, null, false, false, null, null);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C544 RID: 50500 RVA: 0x0047D2CC File Offset: 0x0047B4CC
		public override bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				GameObject target = null;
				int targetDistance = int.MaxValue;
				E.Actor.CurrentZone.ForeachObject(delegate(GameObject obj)
				{
					if (obj.IsCreature && obj != E.Actor && E.Actor.IsHostileTowards(obj) && E.Actor.HasLOSTo(obj, true, false, true, true, null) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, obj.CurrentCell, E.Actor, null, this, null, 0))
					{
						int num = E.Actor.DistanceTo(obj);
						if (num < targetDistance)
						{
							target = obj;
							targetDistance = num;
						}
					}
				});
				if (target != null)
				{
					E.Add("CommandForceWall", 1, null, false, false, target, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C545 RID: 50501 RVA: 0x0047D378 File Offset: 0x0047B578
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandForceWall")
			{
				if (this.ParentObject.OnWorldMap())
				{
					this.ParentObject.Fail("You cannot do that on the world map.");
					return false;
				}
				if (!this.ParentObject.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Mutation", this), E))
				{
					return false;
				}
				List<Cell> list = null;
				if (this.ParentObject.IsPlayer())
				{
					list = base.PickField(9, null, null, false, false);
				}
				else
				{
					Cell currentCell = this.ParentObject.CurrentCell;
					List<GameObject> list2 = currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 25, "ForceWallTarget", this.ParentObject, false, false, null);
					if (list2.Count > 0)
					{
						list = new List<Cell>(list2.Count);
						using (List<GameObject>.Enumerator enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								GameObject gameObject = enumerator.Current;
								list.Add(gameObject.CurrentCell);
							}
							goto IL_0253;
						}
					}
					GameObject gameObject2 = E.Target ?? this.ParentObject.Target;
					if (gameObject2 == null)
					{
						return false;
					}
					if (base.DoIHaveAMissileWeapon() || this.ParentObject.HasGoal("Retreat") || this.ParentObject.HasGoal("Flee") || this.ParentObject.HasGoal("FleeLocation") || this.ParentObject.HasPart<Cryokinesis>() || this.ParentObject.HasPart<Pyrokinesis>() || this.ParentObject.HasPart(typeof(FlamingRay)) || this.ParentObject.HasPart(typeof(FreezingRay)))
					{
						list = gameObject2.CurrentCell.GetLocalAdjacentCells();
					}
					else
					{
						string directionFromCell = currentCell.GetDirectionFromCell(gameObject2.CurrentCell, false);
						Cell cellFromDirection = gameObject2.CurrentCell.GetCellFromDirection(directionFromCell, true);
						if (cellFromDirection != null)
						{
							list = new List<Cell>(9) { cellFromDirection };
							string[] orthogonalDirections = Directions.GetOrthogonalDirections(directionFromCell);
							Cell cell = cellFromDirection;
							Cell cell2 = cellFromDirection;
							for (int i = 0; i < 4; i++)
							{
								cell = ((cell == null) ? null : cell.GetCellFromDirection(orthogonalDirections[0], true));
								cell2 = ((cell2 == null) ? null : cell2.GetCellFromDirection(orthogonalDirections[1], true));
								if (cell != null)
								{
									list.Add(cell);
								}
								if (cell2 != null)
								{
									list.Add(cell2);
								}
							}
						}
					}
				}
				IL_0253:
				if (list == null || list.Count == 0)
				{
					return false;
				}
				base.UseEnergy(1000, "Mental Mutation ForceWall");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
				int num = this.GetDuration(base.Level) + 1;
				foreach (Cell cell3 in list)
				{
					if (IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cell3, this.ParentObject, null, this, 0))
					{
						GameObject gameObject3 = GameObject.Create("Forcefield");
						Forcefield part = gameObject3.GetPart<Forcefield>();
						part.Creator = this.ParentObject;
						part.RejectOwner = false;
						gameObject3.AddPart<Temporary>(new Temporary(num), true, false);
						Phase.carryOver(this.ParentObject, gameObject3);
						cell3.AddObject(gameObject3, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
				base.DidX("conjure", "a {{B|wall of force}}", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C546 RID: 50502 RVA: 0x0047D708 File Offset: 0x0047B908
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C547 RID: 50503 RVA: 0x0047D70B File Offset: 0x0047B90B
		public override string GetDescription()
		{
			return "You generate a wall of force that protects you from your enemies.";
		}

		// Token: 0x0600C548 RID: 50504 RVA: 0x0047D712 File Offset: 0x0047B912
		public int GetCooldown(int Level)
		{
			return 100;
		}

		// Token: 0x0600C549 RID: 50505 RVA: 0x0047D716 File Offset: 0x0047B916
		public int GetDuration(int Level)
		{
			return 14 + Level * 2;
		}

		// Token: 0x0600C54A RID: 50506 RVA: 0x0047D720 File Offset: 0x0047B920
		public override string GetLevelText(int Level)
		{
			string text = "Creates 9 contiguous squares of immobile forcefield.\n";
			int cooldown = this.GetCooldown(Level);
			return text + "Duration: {{rules|" + this.GetDuration(Level).ToString() + "}} rounds\n" + "Cooldown: " + cooldown.ToString() + " rounds\n" + "You may fire missile weapons through the forcefield.";
		}

		// Token: 0x0600C54B RID: 50507 RVA: 0x0047D778 File Offset: 0x0047B978
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Duration", this.GetDuration(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C54C RID: 50508 RVA: 0x0047D7C6 File Offset: 0x0047B9C6
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C54D RID: 50509 RVA: 0x0047D7D0 File Offset: 0x0047B9D0
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Force Wall", "CommandForceWall", "Mental Mutations", null, "°", null, false, false, false, false, true, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C54E RID: 50510 RVA: 0x0047D819 File Offset: 0x0047BA19
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
