using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001400 RID: 5120
	[Serializable]
	public class Teleportation : BaseMutation
	{
		// Token: 0x0600C6AA RID: 50858 RVA: 0x0048689C File Offset: 0x00484A9C
		public Teleportation()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C6AB RID: 50859 RVA: 0x004868AF File Offset: 0x00484AAF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetMovementAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetRetreatAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetMovementCapabilitiesEvent.ID;
		}

		// Token: 0x0600C6AC RID: 50860 RVA: 0x004868ED File Offset: 0x00484AED
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Teleport", Teleportation.COMMAND_NAME, 10000, base.MyActivatedAbility(this.ActivatedAbilityID, null), false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6AD RID: 50861 RVA: 0x0048691C File Offset: 0x00484B1C
		public override bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			if (GameObject.Validate(E.Actor) && E.TargetCell != null && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				int num = this.GetRadius(base.Level) + 1;
				if (E.StandoffDistance > 0)
				{
					Cell cell = null;
					int num2 = 1000;
					int num3 = 1000;
					foreach (Cell cell2 in E.TargetCell.GetLocalAdjacentCellsAtRadius(E.StandoffDistance, true))
					{
						if (cell2.IsEmptyOfSolidFor(E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, cell2, E.Actor, null, this, null, 0))
						{
							int navigationWeightFor = cell2.GetNavigationWeightFor(E.Actor, false, false, false, false, false);
							if (navigationWeightFor < 10)
							{
								int num4 = E.Actor.DistanceTo(cell2);
								if (num4 + navigationWeightFor < num2 + num3)
								{
									cell = cell2;
									num2 = num4;
									num3 = navigationWeightFor;
								}
							}
						}
					}
					if (cell != null && Stat.Random(1, 40) <= Math.Max(num2 - num3, 1))
					{
						E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, cell);
					}
				}
				else if (!E.TargetCell.IsEmptyOfSolidFor(E.Actor, true) || E.TargetCell.GetNavigationWeightFor(E.Actor, false, false, false, false, false) >= 10)
				{
					Cell randomLocalAdjacentCell = E.TargetCell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyOfSolidFor(E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, E.Actor, null, this, null, 0) && c.GetNavigationWeightFor(E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCell != null && E.Actor.DistanceTo(randomLocalAdjacentCell) > num)
					{
						E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, randomLocalAdjacentCell);
					}
				}
				else if (E.Distance > num && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, E.TargetCell, E.Actor, null, this, null, 0))
				{
					E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6AE RID: 50862 RVA: 0x00486BC0 File Offset: 0x00484DC0
		public override bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			Teleportation.<>c__DisplayClass5_0 CS$<>8__locals1 = new Teleportation.<>c__DisplayClass5_0();
			CS$<>8__locals1.E = E;
			CS$<>8__locals1.<>4__this = this;
			if (GameObject.Validate(CS$<>8__locals1.E.Actor) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				CS$<>8__locals1.errorRadius = this.GetRadius(base.Level) + 1;
				if (CS$<>8__locals1.E.TargetCell == null)
				{
					if (CS$<>8__locals1.E.AvoidCell != null || CS$<>8__locals1.E.Target != null)
					{
						Teleportation.<>c__DisplayClass5_1 CS$<>8__locals2 = new Teleportation.<>c__DisplayClass5_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						Teleportation.<>c__DisplayClass5_1 CS$<>8__locals3 = CS$<>8__locals2;
						Cell cell;
						if ((cell = CS$<>8__locals2.CS$<>8__locals1.E.AvoidCell) == null)
						{
							GameObject target = CS$<>8__locals2.CS$<>8__locals1.E.Target;
							cell = ((target != null) ? target.CurrentCell : null);
						}
						CS$<>8__locals3.avoidCell = cell;
						if (CS$<>8__locals2.avoidCell != null)
						{
							CS$<>8__locals2.cell = null;
							CS$<>8__locals2.cellDistance = 0;
							CS$<>8__locals2.cellWeight = 0;
							Zone currentZone = CS$<>8__locals2.CS$<>8__locals1.E.Actor.CurrentZone;
							if (currentZone != null)
							{
								currentZone.ForeachCell(delegate(Cell c)
								{
									try
									{
										if (c != null && c.IsEmptyOfSolidFor(CS$<>8__locals2.CS$<>8__locals1.E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, CS$<>8__locals2.CS$<>8__locals1.E.Actor, null, CS$<>8__locals2.CS$<>8__locals1.<>4__this, null, 0))
										{
											int navigationWeightFor = c.GetNavigationWeightFor(CS$<>8__locals2.CS$<>8__locals1.E.Actor, false, false, false, false, false);
											if (navigationWeightFor < 10)
											{
												int num = CS$<>8__locals2.avoidCell.DistanceTo(c);
												if (num - navigationWeightFor > CS$<>8__locals2.cellDistance - CS$<>8__locals2.cellWeight && CS$<>8__locals2.CS$<>8__locals1.E.Actor.DistanceTo(c) > CS$<>8__locals2.CS$<>8__locals1.errorRadius)
												{
													CS$<>8__locals2.cell = c;
													CS$<>8__locals2.cellDistance = num;
													CS$<>8__locals2.cellWeight = navigationWeightFor;
												}
											}
										}
									}
									catch (Exception ex)
									{
										MetricsManager.LogException("Teleportation::AIGetRetreatAbilityListEvent", ex, "game_exception");
									}
								});
							}
							if (CS$<>8__locals2.cell != null)
							{
								CS$<>8__locals2.CS$<>8__locals1.E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, CS$<>8__locals2.cell);
							}
						}
					}
					else
					{
						Zone currentZone2 = CS$<>8__locals1.E.Actor.CurrentZone;
						Cell cell2 = ((currentZone2 != null) ? currentZone2.GetEmptyReachableCells().GetRandomElement<Cell>(null) : null);
						if (cell2 != null && CS$<>8__locals1.E.Actor.DistanceTo(cell2) > CS$<>8__locals1.errorRadius && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, cell2, CS$<>8__locals1.E.Actor, null, this, null, 0) && cell2.GetNavigationWeightFor(CS$<>8__locals1.E.Actor, false, false, false, false, false) < 10)
						{
							CS$<>8__locals1.E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, cell2);
						}
					}
				}
				else if (CS$<>8__locals1.E.StandoffDistance > 0)
				{
					Cell randomLocalAdjacentCellAtRadius = CS$<>8__locals1.E.TargetCell.GetRandomLocalAdjacentCellAtRadius(CS$<>8__locals1.E.StandoffDistance, (Cell c) => c.IsEmptyOfSolidFor(CS$<>8__locals1.E.Actor, true) && CS$<>8__locals1.E.Actor.DistanceTo(c) > CS$<>8__locals1.errorRadius && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, CS$<>8__locals1.E.Actor, null, CS$<>8__locals1.<>4__this, null, 0) && c.GetNavigationWeightFor(CS$<>8__locals1.E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCellAtRadius != null)
					{
						CS$<>8__locals1.E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, randomLocalAdjacentCellAtRadius);
					}
				}
				else if (!CS$<>8__locals1.E.TargetCell.IsEmptyOfSolidFor(CS$<>8__locals1.E.Actor, true) || CS$<>8__locals1.E.TargetCell.GetNavigationWeightFor(CS$<>8__locals1.E.Actor, false, false, false, false, false) >= 10)
				{
					Cell randomLocalAdjacentCell = CS$<>8__locals1.E.TargetCell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyOfSolidFor(CS$<>8__locals1.E.Actor, true) && CS$<>8__locals1.E.Actor.DistanceTo(c) > CS$<>8__locals1.errorRadius && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, CS$<>8__locals1.E.Actor, null, CS$<>8__locals1.<>4__this, null, 0) && c.GetNavigationWeightFor(CS$<>8__locals1.E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCell != null)
					{
						CS$<>8__locals1.E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, randomLocalAdjacentCell);
					}
				}
				else if (CS$<>8__locals1.E.Distance > CS$<>8__locals1.errorRadius && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, CS$<>8__locals1.E.TargetCell, CS$<>8__locals1.E.Actor, null, this, null, 0))
				{
					CS$<>8__locals1.E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(CS$<>8__locals1.E);
		}

		// Token: 0x0600C6AF RID: 50863 RVA: 0x00486EF8 File Offset: 0x004850F8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (GameObject.Validate(E.Target) && E.Distance > 0 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, E.Target.CurrentCell, E.Actor, null, this, null, 0))
			{
				E.Add(Teleportation.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6B0 RID: 50864 RVA: 0x00486F6C File Offset: 0x0048516C
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6B1 RID: 50865 RVA: 0x00486F94 File Offset: 0x00485194
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == Teleportation.COMMAND_NAME) || Teleportation.Cast(this, "5-6", E, E.TargetCell, null, false, E.StandoffDistance)) && base.HandleEvent(E);
		}

		// Token: 0x0600C6B2 RID: 50866 RVA: 0x00486FCD File Offset: 0x004851CD
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C6B3 RID: 50867 RVA: 0x00486FD0 File Offset: 0x004851D0
		public override string GetDescription()
		{
			return "You teleport to a nearby location.";
		}

		// Token: 0x0600C6B4 RID: 50868 RVA: 0x00486FD7 File Offset: 0x004851D7
		public static int GetCooldown(int Level)
		{
			return Math.Max(103 - 3 * Level, 5);
		}

		// Token: 0x0600C6B5 RID: 50869 RVA: 0x00486FE5 File Offset: 0x004851E5
		public int GetRadius(int Level)
		{
			return Math.Max(13 - Level, 2);
		}

		// Token: 0x0600C6B6 RID: 50870 RVA: 0x00486FF4 File Offset: 0x004851F4
		public override string GetLevelText(int Level)
		{
			return "Teleport to a random location within a designated area.\n" + "Uncertainty radius: {{rules|" + this.GetRadius(Level).ToString() + "}}\n" + "Cooldown: {{rules|" + Teleportation.GetCooldown(Level).ToString() + "}} rounds";
		}

		// Token: 0x0600C6B7 RID: 50871 RVA: 0x00487044 File Offset: 0x00485244
		public static bool Cast(Teleportation Mutation = null, string Level = "5-6", IEvent FromEvent = null, Cell Destination = null, GameObject Subject = null, bool Automatic = false, int WantDistance = 0)
		{
			if (Mutation == null)
			{
				Mutation = new Teleportation();
				Mutation.ParentObject = Subject ?? The.Player;
				Mutation.Level = Level.RollCached();
			}
			Cell cell = null;
			GameObject parentObject = Mutation.ParentObject;
			if (!parentObject.IsRealityDistortionUsable(null, null))
			{
				RealityStabilized.ShowGenericInterdictMessage(parentObject, null);
				return false;
			}
			int num = 0;
			int num2 = Math.Max(Mutation.Level, 1);
			List<Cell> list = null;
			while (cell == null && num2-- > 0)
			{
				if (Destination != null)
				{
					cell = Destination;
					if (WantDistance > 0)
					{
						cell = cell.GetRandomLocalAdjacentCellAtRadius(WantDistance) ?? cell;
					}
					if (num > 0)
					{
						cell = cell.GetLocalAdjacentCells(num, false).GetRandomElement<Cell>(null) ?? cell;
					}
				}
				else
				{
					if (list == null)
					{
						if (parentObject.IsPlayer())
						{
							if (parentObject.OnWorldMap())
							{
								if (!Automatic)
								{
									parentObject.Fail("You may not teleport on the world map.");
								}
								return false;
							}
							list = Mutation.PickCircle(Mutation.GetRadius(Mutation.Level), 9999, false, AllowVis.OnlyExplored, "Teleport");
						}
						else
						{
							GameObject gameObject = parentObject.Target ?? parentObject.PartyLeader;
							if (gameObject == null)
							{
								return false;
							}
							list = gameObject.CurrentCell.GetLocalAdjacentCells(num, false);
						}
						if (list == null)
						{
							return false;
						}
					}
					cell = list.GetRandomElement<Cell>(null);
				}
				if (cell == null)
				{
					break;
				}
				if (!cell.IsEmptyFor(parentObject))
				{
					cell = cell.GetConnectedSpawnLocation(true);
					if (cell != null && parentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You are shunted to another location!", null, true);
					}
				}
			}
			if (cell == null)
			{
				return parentObject.Fail("The teleportation fails!");
			}
			if (parentObject.InActiveZone() && !Options.UseParticleVFX)
			{
				parentObject.ParticleBlip("&C\u000f", 10, 0L, false);
			}
			Event @event = Event.New("InitiateRealityDistortionTransit", "Object", parentObject, "Mutation", Mutation, "Cell", cell);
			if (!parentObject.FireEvent(@event, FromEvent) || !cell.FireEvent(@event, FromEvent))
			{
				return false;
			}
			if (parentObject.InActiveZone())
			{
				parentObject.GetCurrentCell();
				parentObject.TeleportSwirl(null, "&C", true, null, 'ù', true);
				parentObject.TeleportTo(cell, new int?(0), true, false, false, false, "disappear", "appear");
				parentObject.TeleportSwirl(null, "&C", true, null, 'ù', false);
				if (!Options.UseParticleVFX)
				{
					parentObject.ParticleBlip("&C\u000f", 10, 0L, false);
				}
				if (parentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You teleport!", null, true);
				}
			}
			Mutation.UseEnergy(1000, "Mental Mutation Teleportation");
			int cooldown = Teleportation.GetCooldown(Mutation.Level);
			Mutation.CooldownMyActivatedAbility(Mutation.ActivatedAbilityID, cooldown, null, null, false);
			return true;
		}

		// Token: 0x0600C6B8 RID: 50872 RVA: 0x004872AC File Offset: 0x004854AC
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("UncertaintyRadius", this.GetRadius(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Teleportation.GetCooldown(Level), 0);
		}

		// Token: 0x0600C6B9 RID: 50873 RVA: 0x004872F9 File Offset: 0x004854F9
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C6BA RID: 50874 RVA: 0x00487304 File Offset: 0x00485504
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Teleport", Teleportation.COMMAND_NAME, "Mental Mutations", null, "\u001d", null, false, false, false, false, true, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C6BB RID: 50875 RVA: 0x0048734D File Offset: 0x0048554D
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E44 RID: 20036
		public static readonly string COMMAND_NAME = "CommandTeleport";
	}
}
