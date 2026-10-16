using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F2F RID: 3887
	[Serializable]
	public class CyberneticsHighFidelityMatterRecompositer : IPart
	{
		// Token: 0x06009AE8 RID: 39656 RVA: 0x003829C2 File Offset: 0x00380BC2
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AE9 RID: 39657 RVA: 0x003829C5 File Offset: 0x00380BC5
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			GameObject parentObject = this.ParentObject;
			stats.CollectCooldownTurns(base.MyActivatedAbility(activatedAbilityID, (parentObject != null) ? parentObject.Implantee : null), GetAvailableComputePowerEvent.AdjustDown(this.ParentObject, 50, 2), 0);
		}

		// Token: 0x06009AEA RID: 39658 RVA: 0x003829FC File Offset: 0x00380BFC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetMovementAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetRetreatAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009AEB RID: 39659 RVA: 0x00382A5D File Offset: 0x00380C5D
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("High-Fidelity Matter Recompositer", this.commandId, 11000, base.MyActivatedAbility(this.ActivatedAbilityID, null), false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009AEC RID: 39660 RVA: 0x00382A8A File Offset: 0x00380C8A
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject.Implantee))
			{
				E.Add("travel", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AED RID: 39661 RVA: 0x00382AB2 File Offset: 0x00380CB2
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009AEE RID: 39662 RVA: 0x00382AE8 File Offset: 0x00380CE8
		public override bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			if (!this.commandId.IsNullOrEmpty() && GameObject.Validate(E.Actor) && E.TargetCell != null && E.Actor == this.ParentObject.Implantee && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, null, E.Actor, this.ParentObject, null, null, 0))
			{
				if (E.StandoffDistance > 0)
				{
					Cell cell = null;
					int num = 1000;
					int num2 = 1000;
					foreach (Cell cell2 in E.TargetCell.GetLocalAdjacentCellsAtRadius(E.StandoffDistance, true))
					{
						if (cell2.IsEmptyOfSolidFor(E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, cell2, E.Actor, this.ParentObject, null, null, 0))
						{
							int navigationWeightFor = cell2.GetNavigationWeightFor(E.Actor, false, false, false, false, false);
							if (navigationWeightFor < 10)
							{
								int num3 = E.Actor.DistanceTo(cell2);
								if (num3 + navigationWeightFor < num + num2)
								{
									cell = cell2;
									num = num3;
									num2 = navigationWeightFor;
								}
							}
						}
					}
					if (cell != null && Stat.Random(1, 40) <= Math.Max(num - num2, 1))
					{
						E.Add(this.commandId, 1, null, false, false, null, cell);
					}
				}
				else if (!E.TargetCell.IsEmptyOfSolidFor(E.Actor, true) || E.TargetCell.GetNavigationWeightFor(E.Actor, false, false, false, false, false) >= 10)
				{
					Cell randomLocalAdjacentCell = E.TargetCell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyOfSolidFor(E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, E.Actor, this.ParentObject, null, null, 0) && c.GetNavigationWeightFor(E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCell != null && Stat.Random(1, 40) < E.Actor.DistanceTo(randomLocalAdjacentCell))
					{
						E.Add(this.commandId, 1, null, false, false, null, randomLocalAdjacentCell);
					}
				}
				else if (Stat.Random(1, 40) < E.Distance && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, E.TargetCell, E.Actor, this.ParentObject, null, null, 0))
				{
					E.Add(this.commandId, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AEF RID: 39663 RVA: 0x00382DE4 File Offset: 0x00380FE4
		public override bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			CyberneticsHighFidelityMatterRecompositer.<>c__DisplayClass9_0 CS$<>8__locals1 = new CyberneticsHighFidelityMatterRecompositer.<>c__DisplayClass9_0();
			CS$<>8__locals1.E = E;
			CS$<>8__locals1.<>4__this = this;
			if (!this.commandId.IsNullOrEmpty() && GameObject.Validate(CS$<>8__locals1.E.Actor) && CS$<>8__locals1.E.Actor == this.ParentObject.Implantee && CS$<>8__locals1.E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && IComponent<GameObject>.CheckRealityDistortionAdvisability(CS$<>8__locals1.E.Actor, null, CS$<>8__locals1.E.Actor, this.ParentObject, null, null, 0))
			{
				if (CS$<>8__locals1.E.TargetCell == null)
				{
					CyberneticsHighFidelityMatterRecompositer.<>c__DisplayClass9_1 CS$<>8__locals2 = new CyberneticsHighFidelityMatterRecompositer.<>c__DisplayClass9_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CyberneticsHighFidelityMatterRecompositer.<>c__DisplayClass9_1 CS$<>8__locals3 = CS$<>8__locals2;
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
								if (c.IsEmptyOfSolidFor(CS$<>8__locals2.CS$<>8__locals1.E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, CS$<>8__locals2.CS$<>8__locals1.E.Actor, CS$<>8__locals2.CS$<>8__locals1.<>4__this.ParentObject, null, null, 0))
								{
									int navigationWeightFor = c.GetNavigationWeightFor(CS$<>8__locals2.CS$<>8__locals1.E.Actor, false, false, false, false, false);
									if (navigationWeightFor < 10)
									{
										int num = CS$<>8__locals2.avoidCell.DistanceTo(c);
										if (num - navigationWeightFor > CS$<>8__locals2.cellDistance - CS$<>8__locals2.cellWeight)
										{
											CS$<>8__locals2.cell = c;
											CS$<>8__locals2.cellDistance = num;
											CS$<>8__locals2.cellWeight = navigationWeightFor;
										}
									}
								}
							});
						}
						if (CS$<>8__locals2.cell != null)
						{
							CS$<>8__locals2.CS$<>8__locals1.E.Add(this.commandId, 1, null, false, false, null, CS$<>8__locals2.cell);
						}
					}
					else
					{
						Zone currentZone2 = CS$<>8__locals2.CS$<>8__locals1.E.Actor.CurrentZone;
						Cell cell2 = ((currentZone2 != null) ? currentZone2.GetEmptyReachableCells().GetRandomElement<Cell>(null) : null);
						if (cell2 != null && Stat.Random(1, 20) < CS$<>8__locals2.CS$<>8__locals1.E.Actor.DistanceTo(cell2) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, cell2, CS$<>8__locals2.CS$<>8__locals1.E.Actor, this.ParentObject, null, null, 0) && cell2.GetNavigationWeightFor(CS$<>8__locals2.CS$<>8__locals1.E.Actor, false, false, false, false, false) < 10)
						{
							CS$<>8__locals2.CS$<>8__locals1.E.Add(this.commandId, 1, null, false, false, null, cell2);
						}
					}
				}
				else if (CS$<>8__locals1.E.StandoffDistance > 0)
				{
					Cell randomLocalAdjacentCellAtRadius = CS$<>8__locals1.E.TargetCell.GetRandomLocalAdjacentCellAtRadius(CS$<>8__locals1.E.StandoffDistance, (Cell c) => c.IsEmptyOfSolidFor(CS$<>8__locals1.E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, CS$<>8__locals1.E.Actor, CS$<>8__locals1.<>4__this.ParentObject, null, null, 0) && c.GetNavigationWeightFor(CS$<>8__locals1.E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCellAtRadius != null && Stat.Random(1, 20) < CS$<>8__locals1.E.Actor.DistanceTo(randomLocalAdjacentCellAtRadius))
					{
						CS$<>8__locals1.E.Add(this.commandId, 1, null, false, false, null, randomLocalAdjacentCellAtRadius);
					}
				}
				else if (!CS$<>8__locals1.E.TargetCell.IsEmptyOfSolidFor(CS$<>8__locals1.E.Actor, true) || CS$<>8__locals1.E.TargetCell.GetNavigationWeightFor(CS$<>8__locals1.E.Actor, false, false, false, false, false) >= 10)
				{
					Cell randomLocalAdjacentCell = CS$<>8__locals1.E.TargetCell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyOfSolidFor(CS$<>8__locals1.E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, CS$<>8__locals1.E.Actor, CS$<>8__locals1.<>4__this.ParentObject, null, null, 0) && c.GetNavigationWeightFor(CS$<>8__locals1.E.Actor, false, false, false, false, false) < 10);
					if (randomLocalAdjacentCell != null && Stat.Random(1, 20) < CS$<>8__locals1.E.Actor.DistanceTo(randomLocalAdjacentCell))
					{
						CS$<>8__locals1.E.Add(this.commandId, 1, null, false, false, null, randomLocalAdjacentCell);
					}
				}
				else if (Stat.Random(1, 20) < CS$<>8__locals1.E.Distance && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, CS$<>8__locals1.E.TargetCell, CS$<>8__locals1.E.Actor, this.ParentObject, null, null, 0))
				{
					CS$<>8__locals1.E.Add(this.commandId, 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(CS$<>8__locals1.E);
		}

		// Token: 0x06009AF0 RID: 39664 RVA: 0x003831A0 File Offset: 0x003813A0
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.commandId.IsNullOrEmpty() && GameObject.Validate(E.Target) && GameObject.Validate(E.Actor) && E.Actor == this.ParentObject.Implantee && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, null, E.Actor, this.ParentObject, null, null, 0))
			{
				Cell currentCell = E.Target.CurrentCell;
				Cell cell = ((currentCell != null) ? currentCell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyOfSolidFor(E.Actor, true) && IComponent<GameObject>.CheckRealityDistortionAdvisability(null, c, E.Actor, this.ParentObject, null, null, 0) && c.GetNavigationWeightFor(E.Actor, false, false, false, false, false) < 20) : null);
				if (cell != null && Stat.Random(1, 20) < E.Actor.DistanceTo(cell))
				{
					E.Add(this.commandId, 1, null, false, false, null, cell);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AF1 RID: 39665 RVA: 0x003832CC File Offset: 0x003814CC
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddDynamicCommand(out this.commandId, "CommandRecomposite", "Recomposite", "Cybernetics", null, "\a", null, false, false, false, false, true, false, false, false, true, true, false, false, -1, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009AF2 RID: 39666 RVA: 0x0038331E File Offset: 0x0038151E
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009AF3 RID: 39667 RVA: 0x0038333C File Offset: 0x0038153C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.commandId && E.Actor != null && E.Actor == this.ParentObject.Implantee && E.Actor.CurrentCell != null)
			{
				if (E.Actor.OnWorldMap())
				{
					return E.Actor.Fail("You cannot do that on the world map.");
				}
				Cell cell = E.TargetCell;
				if (E.Actor.IsPlayer())
				{
					cell = new Teleportation
					{
						ParentObject = E.Actor,
						Level = 10
					}.PickDestinationCell(9999, AllowVis.OnlyExplored, false, false, false, true, PickTarget.PickStyle.EmptyCell, "Transit", false, null);
				}
				else if (cell == null)
				{
					GameObject gameObject = E.Target ?? E.Actor.Target;
					Cell cell2;
					if (gameObject == null)
					{
						cell2 = null;
					}
					else
					{
						Cell currentCell = gameObject.CurrentCell;
						cell2 = ((currentCell != null) ? currentCell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyOfSolidFor(E.Actor, true)) : null);
					}
					cell = cell2;
				}
				if (cell == null)
				{
					return false;
				}
				if (E.Actor.IsPlayer() && !cell.IsExplored())
				{
					return E.Actor.Fail("You can only teleport to a place you have seen before!");
				}
				if (!cell.IsEmptyOfSolidFor(E.Actor, true))
				{
					return E.Actor.Fail("You may only teleport into an empty square!");
				}
				Event @event = Event.New("InitiateRealityDistortionTransit", "Object", E.Actor, "Device", this.ParentObject, "Cell", cell);
				if (!E.Actor.FireEvent(@event, E) || !cell.FireEvent(@event, E))
				{
					return false;
				}
				E.Actor.TechTeleportSwirlOut(null, "&B", null);
				if (E.Actor.TeleportTo(cell, new int?(0), true, false, false, false, "disappear", "appear"))
				{
					E.Actor.TechTeleportSwirlIn(null, "&C", null);
				}
				IComponent<GameObject>.XDidY(E.Actor, "teleport", null, null, null, null, null, null, false, false, null, null, false, true, false, false, false, null);
				E.Actor.CooldownActivatedAbility(this.ActivatedAbilityID, GetAvailableComputePowerEvent.AdjustDown(E.Actor, 50, 2), null, false);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AF4 RID: 39668 RVA: 0x003835F0 File Offset: 0x003817F0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice reduces this item's cooldown.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009AF5 RID: 39669 RVA: 0x0038360A File Offset: 0x0038180A
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400405B RID: 16475
		public string commandId = "";

		// Token: 0x0400405C RID: 16476
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
