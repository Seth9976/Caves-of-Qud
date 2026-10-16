using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000F49 RID: 3913
	[Serializable]
	public class CyberneticsStasisProjector : IPoweredPart
	{
		// Token: 0x06009C34 RID: 39988 RVA: 0x0038A2A0 File Offset: 0x003884A0
		public CyberneticsStasisProjector()
		{
			this.ChargeUse = 0;
			this.WorksOnImplantee = true;
			this.NameForStatus = "StasisProjector";
		}

		// Token: 0x06009C35 RID: 39989 RVA: 0x0038A2F1 File Offset: 0x003884F1
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009C36 RID: 39990 RVA: 0x0038A2F4 File Offset: 0x003884F4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetRetreatAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009C37 RID: 39991 RVA: 0x0038A348 File Offset: 0x00388548
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			GameObject parentObject = this.ParentObject;
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(activatedAbilityID, (parentObject != null) ? parentObject.Implantee : null);
			int num = stats.CollectComputePowerAdjustUp(activatedAbilityEntry, "Squares of stasis field", this.BaseCoverage, 1f);
			stats.Set("Coverage", num, num != this.BaseCoverage, num - this.BaseCoverage);
			ValueTuple<DieRoll, int> valueTuple = stats.CollectComputePowerAdjustRangeUp(activatedAbilityEntry, "Duration", this.BaseDuration.GetCachedDieRoll(), 1f);
			DieRoll item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			stats.Set("Duration", item.ToString(), item2 != 0, item2);
			int num2 = stats.CollectComputePowerAdjustDown(activatedAbilityEntry, "Cooldown", this.BaseCooldown, 1f);
			stats.CollectCooldownTurns(activatedAbilityEntry, num2, num2 - this.BaseCooldown);
		}

		// Token: 0x06009C38 RID: 39992 RVA: 0x0038A416 File Offset: 0x00388616
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C39 RID: 39993 RVA: 0x0038A44C File Offset: 0x0038864C
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.CommandID.IsNullOrEmpty() && E.Distance == 1 && GameObject.Validate(E.Actor) && GameObject.Validate(E.Target) && this.IsObjectActivePartSubject(E.Actor) && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, E.Target.CurrentCell, E.Actor, this.ParentObject, null, null, 0))
			{
				E.Add(this.CommandID, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C3A RID: 39994 RVA: 0x0038A528 File Offset: 0x00388728
		public override bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			if (!this.CommandID.IsNullOrEmpty() && GameObject.Validate(E.Actor) && this.IsObjectActivePartSubject(E.Actor) && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, null, E.Actor, this.ParentObject, null, null, 0))
			{
				int coverage = this.GetCoverage(E.Actor);
				GameObject target = null;
				int targetDistance = int.MaxValue;
				E.Actor.CurrentZone.ForeachObject(delegate(GameObject obj)
				{
					if (obj.IsCreature && obj != E.Actor && E.Actor.IsHostileTowards(obj) && E.Actor.HasLOSTo(obj, true, false, true, true, null) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, obj.CurrentCell, E.Actor, this.ParentObject, null, null, 0))
					{
						int num = E.Actor.DistanceTo(obj);
						if (num < targetDistance && num < coverage)
						{
							target = obj;
							targetDistance = num;
						}
					}
				});
				if (target != null)
				{
					E.Add(this.CommandID, 1, null, false, false, target, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C3B RID: 39995 RVA: 0x0038A673 File Offset: 0x00388873
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009C3C RID: 39996 RVA: 0x0038A690 File Offset: 0x00388890
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddDynamicCommand(out this.CommandID, "ProjectStasisField", "Project Stasis Field", "Cybernetics", null, "é", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C3D RID: 39997 RVA: 0x0038A6E2 File Offset: 0x003888E2
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C3E RID: 39998 RVA: 0x0038A700 File Offset: 0x00388900
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.CommandID)
			{
				if (!this.IsObjectActivePartSubject(E.Actor))
				{
					return false;
				}
				if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
				if (!E.Actor.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", E.Actor, "Device", this), E))
				{
					return false;
				}
				int coverage = this.GetCoverage(E.Actor);
				List<Cell> list = null;
				if (E.Actor.IsPlayer())
				{
					list = base.PickFieldAdjacent(coverage, E.Actor, "Field", false, false);
				}
				else if (E.Target != null)
				{
					list = new List<Cell>();
					Cell currentCell = E.Target.CurrentCell;
					Cell cell = E.Actor.CurrentCell;
					int num = 0;
					while (list.Count < coverage && cell != null && cell != currentCell)
					{
						if (++num >= 100)
						{
							break;
						}
						string generalDirectionFromCell = cell.GetGeneralDirectionFromCell(currentCell);
						if (!generalDirectionFromCell.IsNullOrEmpty())
						{
							Cell cell2 = cell.GetCellFromDirection(generalDirectionFromCell, true);
							if (cell2 != null)
							{
								if (list.Contains(cell2))
								{
									cell2 = null;
								}
								else
								{
									list.Add(cell2);
								}
							}
							cell = cell2;
						}
					}
				}
				else
				{
					Cell currentCell2 = E.Actor.CurrentCell;
					List<GameObject> list2 = currentCell2.ParentZone.FastCombatSquareVisibility(currentCell2.X, currentCell2.Y, 25, E.Actor, new Predicate<GameObject>(E.Actor.IsHostileTowards), false, false, true, true, null);
					if (list2.Count > 0)
					{
						int num2 = 0;
						List<GameObject> list3 = Event.NewGameObjectList();
						foreach (GameObject gameObject in list2)
						{
							if (gameObject.InAdjacentCellTo(E.Actor))
							{
								list3.Add(gameObject);
							}
						}
						if (list3.Count > 0)
						{
							list = new List<Cell>(list2.Count);
							GameObject randomElement = list3.GetRandomElement<GameObject>(null);
							list2.Remove(randomElement);
							list.Add(randomElement.CurrentCell);
							List<Cell> list4 = new List<Cell>();
							while (list2.Count > 1 && list.Count < coverage && ++num2 < 100)
							{
								Cell cell3 = list[list.Count - 1];
								Cell cell4 = null;
								list4.Clear();
								foreach (Cell cell5 in cell3.GetLocalCardinalAdjacentCells())
								{
									if (!list.Contains(cell5) && !cell5.Objects.Contains(E.Actor))
									{
										list4.Add(cell5);
									}
								}
								list4.ShuffleInPlace<Cell>(null);
								foreach (Cell cell6 in list4)
								{
									foreach (GameObject gameObject2 in list2)
									{
										if (cell6.Objects.Contains(gameObject2))
										{
											cell4 = cell6;
											break;
										}
									}
									if (cell4 != null)
									{
										break;
									}
								}
								if (cell4 == null)
								{
									int num3 = 0;
									foreach (Cell cell7 in list4)
									{
										int num4 = 0;
										foreach (GameObject gameObject3 in list2)
										{
											num4 += 100 - gameObject3.DistanceTo(cell7);
										}
										if (num4 > num3)
										{
											cell4 = cell7;
											num3 = num4;
										}
									}
								}
								if (cell4 == null)
								{
									break;
								}
								list.Add(cell4);
								foreach (GameObject gameObject4 in cell4.Objects)
								{
									list2.Remove(gameObject4);
								}
							}
						}
					}
				}
				if (list == null || list.Count == 0)
				{
					return false;
				}
				E.Actor.UseEnergy(1000, "Cybernetics Stasis Projector", null, null, false);
				E.Actor.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(E.Actor), null, false);
				this.DeployToCells(list, E.Actor);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
				if (E.Actor.IsPlayer())
				{
					IComponent<GameObject>.XDidY(E.Actor, "project", "a stasis field", null, null, null, E.Actor, null, false, false, null, null, false, false, false, false, false, null);
				}
				else
				{
					IComponent<GameObject>.XDidY(E.Actor, "project", "a stasis field", null, null, null, E.Actor, null, false, false, null, null, true, false, false, false, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C3F RID: 39999 RVA: 0x0038AC4C File Offset: 0x00388E4C
		private GameObject DeployToCells(List<Cell> Cells, GameObject Actor)
		{
			GameObject gameObject = null;
			TextConsole textConsole = Look._TextConsole;
			The.Core.RenderBase(true, false);
			textConsole.WaitFrame();
			foreach (Cell cell in Cells)
			{
				if (IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cell, Actor, this.ParentObject, null, 0))
				{
					GameObject gameObject2 = GameObject.Create("Stasisfield");
					gameObject2.GetPart<Forcefield>().Creator = Actor;
					gameObject2.AddPart<Temporary>(new Temporary(this.GetDuration(null) + 1), true, false);
					Phase.carryOver(Actor, gameObject2);
					cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject = gameObject2;
					The.Core.RenderBase(true, false);
				}
				textConsole.WaitFrame();
			}
			return gameObject;
		}

		// Token: 0x06009C40 RID: 40000 RVA: 0x0038AD2C File Offset: 0x00388F2C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009C41 RID: 40001 RVA: 0x0038AD2F File Offset: 0x00388F2F
		public int GetCoverage(GameObject who = null)
		{
			return GetAvailableComputePowerEvent.AdjustUp(who ?? this.ParentObject.Implantee, this.BaseCoverage);
		}

		// Token: 0x06009C42 RID: 40002 RVA: 0x0038AD4C File Offset: 0x00388F4C
		public int GetDuration(GameObject who = null)
		{
			return GetAvailableComputePowerEvent.AdjustUp(who ?? this.ParentObject.Implantee, this.BaseDuration.RollCached());
		}

		// Token: 0x06009C43 RID: 40003 RVA: 0x0038AD6E File Offset: 0x00388F6E
		public int GetCooldown(GameObject who = null)
		{
			return GetAvailableComputePowerEvent.AdjustDown(who ?? this.ParentObject.Implantee, this.BaseCooldown, 2);
		}

		// Token: 0x040040A7 RID: 16551
		public int BaseCoverage = 6;

		// Token: 0x040040A8 RID: 16552
		public int BaseCooldown = 100;

		// Token: 0x040040A9 RID: 16553
		public string BaseDuration = "6-8";

		// Token: 0x040040AA RID: 16554
		public string CommandID;

		// Token: 0x040040AB RID: 16555
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
