using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// This part is not used in the base game.
	/// </remarks>
	// Token: 0x02000F47 RID: 3911
	[Serializable]
	public class CyberneticsStasisArena : IPoweredPart
	{
		// Token: 0x06009C07 RID: 39943 RVA: 0x00389284 File Offset: 0x00387484
		public CyberneticsStasisArena()
		{
			this.ChargeUse = 0;
			this.WorksOnImplantee = true;
			this.NameForStatus = "StasisArena";
		}

		// Token: 0x06009C08 RID: 39944 RVA: 0x003892DD File Offset: 0x003874DD
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009C09 RID: 39945 RVA: 0x003892E0 File Offset: 0x003874E0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009C0A RID: 39946 RVA: 0x00389310 File Offset: 0x00387510
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (this.CommandID == null)
			{
				this.CommandID = Guid.NewGuid().ToString();
			}
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Stasis Arena", this.CommandID, "Cybernetics", null, "é", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C0B RID: 39947 RVA: 0x0038937F File Offset: 0x0038757F
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C0C RID: 39948 RVA: 0x0038939A File Offset: 0x0038759A
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == this.CommandID) || this.ActivateStasisArena(E.Actor, E.Target, E)) && base.HandleEvent(E);
		}

		// Token: 0x06009C0D RID: 39949 RVA: 0x003893D0 File Offset: 0x003875D0
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			int @for = GetAvailableComputePowerEvent.GetFor(this);
			int exclusionSize = this.GetExclusionSize(@for);
			int cooldown = this.GetCooldown(@for);
			int minDuration = this.GetMinDuration(@for);
			int maxDuration = this.GetMaxDuration(@for);
			E.Description = string.Concat(new string[]
			{
				"Activated. Cooldown ",
				cooldown.ToString(),
				".\nPick an exclusion zone of up to ",
				exclusionSize.Things("square", null),
				"; the rest of the zone, other than the square you are in, is enveloped in stasis fields that last ",
				(minDuration == maxDuration) ? minDuration.Things("turn", null) : (minDuration.ToString() + "-" + maxDuration.ToString() + " turns"),
				".\nCompute power on the local lattice increases this implant's effectiveness."
			});
			return base.HandleEvent(E);
		}

		// Token: 0x06009C0E RID: 39950 RVA: 0x00389489 File Offset: 0x00387689
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009C0F RID: 39951 RVA: 0x0038948C File Offset: 0x0038768C
		public bool ActivateStasisArena(GameObject Actor, GameObject Target = null, IEvent FromEvent = null)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return false;
			}
			if (!this.IsObjectActivePartSubject(Actor))
			{
				return false;
			}
			if (Actor.OnWorldMap())
			{
				return Actor.Fail("You cannot do that on the world map.");
			}
			Zone currentZone = Actor.CurrentZone;
			if (currentZone == null)
			{
				return false;
			}
			if (!Actor.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				return false;
			}
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + ".");
			}
			int @for = GetAvailableComputePowerEvent.GetFor(this);
			int realityStabilizationPenetration = this.GetRealityStabilizationPenetration(@for);
			Event @event = Event.New("InitiateRealityDistortionLocal", 0, 0, 0);
			@event.SetParameter("Object", Actor);
			@event.SetParameter("Device", this.ParentObject);
			@event.SetParameter("RealityStabilizationPenetration", realityStabilizationPenetration);
			if (!Actor.FireEvent(@event, FromEvent))
			{
				return false;
			}
			Cell currentCell = Actor.CurrentCell;
			int exclusionSize = this.GetExclusionSize(@for);
			List<Cell> list = null;
			if (Actor.IsPlayer())
			{
				list = base.PickFieldAdjacent(exclusionSize, Actor, "Exclusion", false, false);
			}
			else
			{
				GameObject gameObject;
				if ((gameObject = Target) == null)
				{
					gameObject = (Target = Actor.Target);
				}
				if (gameObject != null)
				{
					list = new List<Cell>();
					Cell currentCell2 = Target.CurrentCell;
					int num = 0;
					Cell cell = currentCell;
					while (list.Count < exclusionSize && cell != null && cell != currentCell2)
					{
						if (++num >= 100)
						{
							break;
						}
						string generalDirectionFromCell = cell.GetGeneralDirectionFromCell(currentCell2);
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
					List<GameObject> list2 = currentZone.FastCombatSquareVisibility(currentCell.X, currentCell.Y, 25, Actor, new Predicate<GameObject>(Actor.IsHostileTowards), false, false, true, true, null);
					if (list2.Count > 0)
					{
						int num2 = 0;
						List<GameObject> list3 = Event.NewGameObjectList();
						foreach (GameObject gameObject2 in list2)
						{
							if (gameObject2.InAdjacentCellTo(Actor))
							{
								list3.Add(gameObject2);
							}
						}
						if (list3.Count > 0)
						{
							list = new List<Cell>(list2.Count);
							GameObject randomElement = list3.GetRandomElement<GameObject>(null);
							list2.Remove(randomElement);
							list.Add(randomElement.CurrentCell);
							List<Cell> list4 = new List<Cell>();
							while (list2.Count > 1 && list.Count < exclusionSize && ++num2 < 100)
							{
								Cell cell3 = list[list.Count - 1];
								Cell cell4 = null;
								list4.Clear();
								foreach (Cell cell5 in cell3.GetLocalCardinalAdjacentCells())
								{
									if (!list.Contains(cell5) && !cell5.Objects.Contains(Actor))
									{
										list4.Add(cell5);
									}
								}
								list4.ShuffleInPlace<Cell>(null);
								foreach (Cell cell6 in list4)
								{
									foreach (GameObject gameObject3 in list2)
									{
										if (cell6.Objects.Contains(gameObject3))
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
										foreach (GameObject gameObject4 in list2)
										{
											num4 += 100 - gameObject4.DistanceTo(cell7);
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
								foreach (GameObject gameObject5 in cell4.Objects)
								{
									list2.Remove(gameObject5);
								}
							}
						}
					}
				}
			}
			if (list == null)
			{
				return false;
			}
			if (!list.Contains(currentCell))
			{
				list.Add(currentCell);
			}
			Actor.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(@for), null, false);
			Actor.UseEnergy(1000, "Cybernetics StasisArena", null, null, false);
			this.DeployToCells(currentZone, list, Actor, @for, realityStabilizationPenetration);
			if (FromEvent != null)
			{
				FromEvent.RequestInterfaceExit();
			}
			return true;
		}

		// Token: 0x06009C10 RID: 39952 RVA: 0x003899DC File Offset: 0x00387BDC
		private GameObject DeployToCells(Zone Zone, List<Cell> ExclusionCells, GameObject Actor, int ComputePower, int RealityStabilizationPenetration)
		{
			GameObject gameObject = null;
			Phased phased;
			Omniphase omniphase;
			Phase.carryOverPrep(Actor, out phased, out omniphase);
			TextConsole textConsole = Look._TextConsole;
			The.Core.RenderBase(true, false);
			for (int i = 0; i < Zone.Height; i++)
			{
				for (int j = 0; j < Zone.Width; j++)
				{
					Cell cell = Zone.GetCell(j, i);
					if (!ExclusionCells.Contains(cell) && IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cell, Actor, this.ParentObject, null, RealityStabilizationPenetration))
					{
						GameObject gameObject2 = GameObject.Create("Stasisfield");
						gameObject2.GetPart<Forcefield>().Creator = Actor;
						gameObject2.AddPart<Temporary>(new Temporary(this.GetDuration(ComputePower) + 1), true, false);
						Phase.carryOver(Actor, gameObject2, phased, omniphase);
						cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObject = gameObject2;
						if (cell.IsVisible())
						{
							The.Core.RenderBase(true, false);
						}
					}
				}
			}
			return gameObject;
		}

		// Token: 0x06009C11 RID: 39953 RVA: 0x00389AD3 File Offset: 0x00387CD3
		public int GetExclusionSize(int ComputePower)
		{
			return this.BaseExclusionSize + ComputePower / 5;
		}

		// Token: 0x06009C12 RID: 39954 RVA: 0x00389ADF File Offset: 0x00387CDF
		public int GetExclusionSize()
		{
			return this.GetExclusionSize(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C13 RID: 39955 RVA: 0x00389AF0 File Offset: 0x00387CF0
		public int GetCooldown(int ComputePower)
		{
			int num = this.BaseCooldown;
			if (ComputePower != 0)
			{
				num = Math.Max(num * (100 - ComputePower / 2) / 100, num / 2);
			}
			return num;
		}

		// Token: 0x06009C14 RID: 39956 RVA: 0x00389B1C File Offset: 0x00387D1C
		public int GetCooldown()
		{
			return this.GetCooldown(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C15 RID: 39957 RVA: 0x00389B2C File Offset: 0x00387D2C
		public int GetDuration(int ComputePower)
		{
			int num = this.BaseDuration.RollCached() + ComputePower / 3;
			return num + Stat.Random(-num / 5, num / 5);
		}

		// Token: 0x06009C16 RID: 39958 RVA: 0x00389B59 File Offset: 0x00387D59
		public int GetDuration()
		{
			return this.GetDuration(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C17 RID: 39959 RVA: 0x00389B67 File Offset: 0x00387D67
		public int GetMinDuration(int ComputePower)
		{
			int num = this.BaseDuration.RollMinCached() + ComputePower / 3;
			return num - num / 5;
		}

		// Token: 0x06009C18 RID: 39960 RVA: 0x00389B7C File Offset: 0x00387D7C
		public int GetMinDuration()
		{
			return this.GetMinDuration(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C19 RID: 39961 RVA: 0x00389B8A File Offset: 0x00387D8A
		public int GetMaxDuration(int ComputePower)
		{
			int num = this.BaseDuration.RollMaxCached() + ComputePower / 3;
			return num + num / 5;
		}

		// Token: 0x06009C1A RID: 39962 RVA: 0x00389B9F File Offset: 0x00387D9F
		public int GetMaxDuration()
		{
			return this.GetMaxDuration(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C1B RID: 39963 RVA: 0x00389BAD File Offset: 0x00387DAD
		public int GetRealityStabilizationPenetration(int ComputePower)
		{
			return this.BaseRealityStabilizationPenetration + ComputePower / 3;
		}

		// Token: 0x06009C1C RID: 39964 RVA: 0x00389BB9 File Offset: 0x00387DB9
		public int GetRealityStabilizationPenetration()
		{
			return this.GetRealityStabilizationPenetration(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x0400409C RID: 16540
		public int BaseCooldown = 60;

		// Token: 0x0400409D RID: 16541
		public int BaseExclusionSize = 5;

		// Token: 0x0400409E RID: 16542
		public int BaseRealityStabilizationPenetration = 30;

		// Token: 0x0400409F RID: 16543
		public string BaseDuration = "10";

		// Token: 0x040040A0 RID: 16544
		public string CommandID;

		// Token: 0x040040A1 RID: 16545
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
