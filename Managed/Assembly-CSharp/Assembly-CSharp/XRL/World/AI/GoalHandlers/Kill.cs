using System;
using System.Collections.Generic;
using XRL.Collections;
using XRL.Rules;
using XRL.World.AI.Pathfinding;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B87 RID: 2951
	[Serializable]
	public class Kill : GoalHandler
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060075F7 RID: 30199 RVA: 0x002C400F File Offset: 0x002C220F
		// (set) Token: 0x060075F8 RID: 30200 RVA: 0x002C4023 File Offset: 0x002C2223
		public GameObject Target
		{
			get
			{
				GameObject.Validate(ref this._Target);
				return this._Target;
			}
			set
			{
				this._Target = value;
			}
		}

		// Token: 0x060075F9 RID: 30201 RVA: 0x002C402C File Offset: 0x002C222C
		public Kill(GameObject Target, bool Directed = false)
		{
			this.Target = Target;
			this.Directed = Directed;
		}

		// Token: 0x060075FA RID: 30202 RVA: 0x002C4044 File Offset: 0x002C2244
		public override void Create()
		{
			if (this.Target != null)
			{
				base.Think("I'm trying to kill someone!");
				if (base.ParentObject.HasRegisteredEvent("AICreateKill"))
				{
					Event @event = Event.New("AICreateKill", "Actor", base.ParentObject, "Target", this.Target);
					if (!base.ParentObject.FireEvent(@event))
					{
						return;
					}
				}
				if (this.Target.HasRegisteredEvent("AITargetCreateKill"))
				{
					Event event2 = Event.New("AITargetCreateKill", "Actor", base.ParentObject, "Target", this.Target);
					this.Target.FireEvent(event2);
					return;
				}
			}
		}

		// Token: 0x060075FB RID: 30203 RVA: 0x002C40EA File Offset: 0x002C22EA
		public override string GetDetails()
		{
			if (this.Target == null)
			{
				return null;
			}
			if (!this._Target.IsPlayer())
			{
				return this._Target.DebugName;
			}
			return "Player";
		}

		// Token: 0x060075FC RID: 30204 RVA: 0x002C4114 File Offset: 0x002C2314
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x060075FD RID: 30205 RVA: 0x002C4118 File Offset: 0x002C2318
		public bool TryThrownWeapon()
		{
			if (base.ParentObject.Body == null)
			{
				return false;
			}
			GameObject target = this.Target;
			Cell currentCell = base.ParentObject.CurrentCell;
			Cell cell = ((target != null) ? target.CurrentCell : null);
			if (currentCell == null || cell == null || currentCell.ParentZone != cell.ParentZone)
			{
				return false;
			}
			if (!base.ParentObject.CanMoveExtremities(null, false, false, true))
			{
				return false;
			}
			bool flag;
			using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
			{
				base.ParentObject.GetThrownWeapons(fromPool, null, null);
				if (fromPool.IsNullOrEmpty<GameObject>())
				{
					flag = false;
				}
				else
				{
					GameObject gameObject = fromPool[0];
					if (gameObject.HasPart<StickyOnHit>() && target.HasEffect<Stuck>())
					{
						flag = false;
					}
					else
					{
						int num;
						int num2;
						int num3;
						bool flag2;
						GetThrowProfileEvent.Process(out num, out num2, out num3, out flag2, base.ParentObject, gameObject, target, cell, base.ParentObject.DistanceTo(target));
						int num4 = base.ParentObject.GetThrowRangeRandomVariance();
						int i = 2;
						int num5 = Math.Min(base.ParentObject.Stat("Intelligence", 0) / 5, 5);
						while (i < num5)
						{
							num4 = Math.Min(num4, base.ParentObject.GetThrowRangeRandomVariance());
							i++;
						}
						List<Point> list = Zone.Line(currentCell.X, currentCell.Y, cell.X, cell.Y, false);
						if (list.Count - 1 > num + num4)
						{
							base.Think("Too far to target for a throw, I'll try a different weapon...");
							flag = false;
						}
						else
						{
							int j = 1;
							int num6 = list.Count - 1;
							while (j < num6)
							{
								Cell cell2 = cell.ParentZone.GetCell(list[j].X, list[j].Y);
								int k = 0;
								int count = cell2.Objects.Count;
								while (k < count)
								{
									GameObject gameObject2 = cell2.Objects[k];
									if (gameObject2.IsCombatObject() && !this.ParentBrain.IsHostileTowards(gameObject2))
									{
										base.Think("Friendly or neutral in the way, I'll try a different weapon...");
										return false;
									}
									if (gameObject2.ConsiderSolidFor(base.ParentObject, false) && !base.ParentObject.ShouldAttackToReachTarget(gameObject2, target))
									{
										base.Think("Solid object in the way, I'll try a different weapon...");
										return false;
									}
									k++;
								}
								if (cell2.IsOccluding())
								{
									int num7 = Stat.Random(1, base.ParentObject.Stat("Intelligence", 0));
									if (num7 < 7)
									{
										base.Think("I can't figure out where my target might be.");
										return false;
									}
									if (num7 < 14 && 75.in100())
									{
										cell = cell.GetRandomLocalAdjacentCell() ?? cell;
									}
								}
								j++;
							}
							base.Think("I'm going to throw!");
							if (!Combat.ThrowWeapon(base.ParentObject, target, cell, null, fromPool))
							{
								base.Think("It didn't work, I'll try a different weapon...");
								flag = false;
							}
							else
							{
								foreach (GameObject gameObject3 in fromPool)
								{
									if (GameObject.Validate(gameObject3))
									{
										AIAfterThrowEvent.Send(gameObject3, base.ParentObject, target);
									}
								}
								flag = true;
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060075FE RID: 30206 RVA: 0x002C445C File Offset: 0x002C265C
		public bool TryMissileReload()
		{
			bool flag = false;
			try
			{
				List<GameObject> missileWeapons = base.ParentObject.GetMissileWeapons(null, null);
				if (missileWeapons != null)
				{
					int i = 0;
					int count = missileWeapons.Count;
					while (i < count)
					{
						GameObject gameObject = missileWeapons[i];
						if (gameObject.GetPart<EnergyCellSocket>() != null)
						{
							EnergyAmmoLoader part = gameObject.GetPart<EnergyAmmoLoader>();
							if (part != null && part.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) == ActivePartStatus.Unpowered)
							{
								flag = CommandReloadEvent.Execute(base.ParentObject, gameObject, null, false, false, part.GetActiveChargeUse(null, null));
								break;
							}
							int @for = QueryDrawEvent.GetFor(gameObject);
							if (@for > 0 && !gameObject.TestCharge(@for, false, 0L, true, true) && !gameObject.HasEffect<ElectromagneticPulsed>())
							{
								flag = CommandReloadEvent.Execute(base.ParentObject, gameObject, null, false, false, @for);
								break;
							}
						}
						i++;
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("TryMissileReload", ex, "game_exception");
			}
			return flag && base.ParentObject.Stat("Energy", 0) < 1000;
		}

		// Token: 0x060075FF RID: 30207 RVA: 0x002C4598 File Offset: 0x002C2798
		public bool TryMissileWeapon()
		{
			GameObject target = this.Target;
			if (target == null)
			{
				return false;
			}
			if (!target.IsCombatObject())
			{
				base.Think("Target can't be targeted with missile weapons, I'll try a different weapon...");
				return false;
			}
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			Zone parentZone = currentCell.ParentZone;
			if (parentZone == null)
			{
				return false;
			}
			Cell cell = target.CurrentCell;
			if (cell == null)
			{
				return false;
			}
			if (parentZone != cell.ParentZone)
			{
				base.Think("Target is in another zone, I'll try a different weapon...");
				return false;
			}
			Body body = base.ParentObject.Body;
			if (body == null)
			{
				base.Think("I'm not a creature that can equip missile weapons, I'll try a different weapon...");
				return false;
			}
			if (base.ParentObject.GetIntProperty("TurretWarmup", 0) > 0 && (target.IsPlayerControlled() || base.Visible()))
			{
				if (base.ParentObject.GetIntProperty("TurretWarmup", 0) == 1)
				{
					if (parentZone.IsActive())
					{
						base.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_turret_chirp", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						if (base.Visible())
						{
							base.ParentObject.Physics.DidX("chirp", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
							if (AutoAct.IsActive(false) && The.Player.IsRelevantHostile(base.ParentObject))
							{
								AutoAct.Interrupt(null, null, base.ParentObject, true);
							}
						}
						else if (base.ParentObject.IsAudible(The.Player, 20))
						{
							GoalHandler.AddPlayerMessage("Something chirps " + The.Player.DescribeDirectionToward(base.ParentObject, false, false) + ".", null, true);
							if (AutoAct.IsActive(false))
							{
								AutoAct.Interrupt(null, null, base.ParentObject, true);
							}
						}
					}
					base.ParentObject.ApplyEffect(new WarmingUp(), null);
					base.ParentObject.UseEnergy(1000, "Warmup", null, null, false);
					base.ParentObject.SetIntProperty("TurretWarmup", 2, false);
					return true;
				}
				if (base.ParentObject.GetIntProperty("TurretWarmup", 0) == 2)
				{
					base.ParentObject.RemoveEffect<WarmingUp>();
					base.ParentObject.SetIntProperty("TurretWarmup", 3, false);
				}
			}
			if (this.ParentBrain.NeedToReload)
			{
				this.ParentBrain.NeedToReload = false;
				if (!CommandReloadEvent.Execute(base.ParentObject, false, false, 0))
				{
					base.Think("Need to reload and can't, I'll try a different weapon...");
					return false;
				}
				if (base.ParentObject.Stat("Energy", 0) < 1000)
				{
					return true;
				}
			}
			List<GameObject> missileWeapons = body.GetMissileWeapons(null, null);
			if (missileWeapons == null || missileWeapons.Count <= 0)
			{
				base.Think("No missile weapons equipped, I'll try a different weapon...");
				return false;
			}
			int num = cell.PathDistanceTo(currentCell);
			if (num > this.ParentBrain.MaxMissileRange)
			{
				base.Think("Too far to target for missile weapons, I'll try a different weapon...");
				return false;
			}
			List<Point> list = null;
			int i = 0;
			int count = missileWeapons.Count;
			while (i < count)
			{
				GameObject gameObject = missileWeapons[i];
				if (AIWantUseWeaponEvent.Check(gameObject, base.ParentObject, target))
				{
					MissileWeapon part = gameObject.GetPart<MissileWeapon>();
					if (part == null || num <= part.MaxRange)
					{
						if (list == null)
						{
							list = Zone.Line(currentCell.X, currentCell.Y, cell.X, cell.Y, true);
						}
						int j = 1;
						int num2 = list.Count - 1;
						while (j < num2)
						{
							Cell cell2 = parentZone.GetCell(list[j].X, list[j].Y);
							int k = 0;
							int count2 = cell2.Objects.Count;
							while (k < count2)
							{
								GameObject gameObject2 = cell2.Objects[k];
								if (gameObject2.IsCombatObject() && !this.ParentBrain.IsHostileTowards(gameObject2))
								{
									base.Think("Friendly or neutral in the way, I'll try a different weapon...");
									return false;
								}
								if (gameObject2.ConsiderSolidFor(base.ParentObject, false) && !base.ParentObject.ShouldAttackToReachTarget(gameObject2, target))
								{
									base.Think("Solid object in the way, I'll try a different weapon...");
									return false;
								}
								k++;
							}
							if (cell2.IsOccluding())
							{
								int num3 = Stat.Random(1, base.ParentObject.Stat("Intelligence", 0));
								if (num3 < 7)
								{
									base.Think("I can't figure out where my target might be.");
									return false;
								}
								if (num3 < 14 && 75.in100())
								{
									cell = cell.GetRandomLocalAdjacentCell() ?? cell;
								}
							}
							j++;
						}
						base.Think("I'm going to fire one or more missile weapons, probably my " + gameObject.Blueprint + "!");
						if (Combat.FireMissileWeapon(base.ParentObject, target, cell, FireType.Normal, null, 0, 0, 90, null, true))
						{
							return true;
						}
						base.Think("It didn't work, I'll try a different weapon...");
					}
				}
				i++;
			}
			base.Think("No missile weapons I can use, I'll try a different weapon...");
			return false;
		}

		// Token: 0x06007600 RID: 30208 RVA: 0x002C4A49 File Offset: 0x002C2C49
		public override void Push(Brain pBrain)
		{
			if (pBrain.ParentObject == this.Target)
			{
				return;
			}
			if (!pBrain.CanFight())
			{
				return;
			}
			base.Push(pBrain);
		}

		// Token: 0x06007601 RID: 30209 RVA: 0x002C4A6C File Offset: 0x002C2C6C
		public bool TryDefensiveItems()
		{
			return (!base.ParentObject.HasStat("Intelligence") || base.ParentObject.Stat("Intelligence", 0) >= 7) && this.Target.InSameZone(base.ParentObject) && AICommandList.HandleCommandList(AIGetDefensiveItemListEvent.GetFor(base.ParentObject, null, -1), base.ParentObject, this.Target, null, 0);
		}

		// Token: 0x06007602 RID: 30210 RVA: 0x002C4AD8 File Offset: 0x002C2CD8
		public bool TryDefensiveAbilities()
		{
			return this.Target != null && this.Target.InSameZone(base.ParentObject) && AICommandList.HandleCommandList(AIGetDefensiveAbilityListEvent.GetFor(base.ParentObject, null, -1), base.ParentObject, this.Target, null, 0);
		}

		// Token: 0x06007603 RID: 30211 RVA: 0x002C4B28 File Offset: 0x002C2D28
		public bool TryItems()
		{
			return this.Target != null && this.Target.InSameZone(base.ParentObject) && AICommandList.HandleCommandList(AIGetOffensiveItemListEvent.GetFor(base.ParentObject, null, -1), base.ParentObject, this.Target, null, 0);
		}

		// Token: 0x06007604 RID: 30212 RVA: 0x002C4B75 File Offset: 0x002C2D75
		public bool TryAbilities()
		{
			return this.Target.InSameZone(base.ParentObject) && AICommandList.HandleCommandList(AIGetOffensiveAbilityListEvent.GetFor(base.ParentObject, null, -1), base.ParentObject, this.Target, null, 0);
		}

		// Token: 0x06007605 RID: 30213 RVA: 0x002C4BB0 File Offset: 0x002C2DB0
		public GameObject FindTargetOfOpportunity(Cell MyCell, Cell TargetCell = null)
		{
			if (TargetCell != null)
			{
				Cell cellFromDirectionOfCell = MyCell.GetCellFromDirectionOfCell(TargetCell, true);
				if (cellFromDirectionOfCell != null)
				{
					GameObject gameObject = cellFromDirectionOfCell.GetCombatTarget(base.ParentObject, false, false, false, 0, null, null, null, null, null, false, false, null);
					if (gameObject != null)
					{
						if (base.ParentObject.IsHostileTowards(gameObject))
						{
							return gameObject;
						}
					}
					else
					{
						gameObject = cellFromDirectionOfCell.GetCombatTarget(base.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null);
						if (gameObject != null && base.ParentObject.ShouldAttackToReachTarget(gameObject, this.Target))
						{
							return gameObject;
						}
					}
				}
			}
			foreach (Cell cell in MyCell.GetLocalAdjacentCells())
			{
				GameObject combatTarget = cell.GetCombatTarget(base.ParentObject, false, false, false, 0, null, null, null, null, null, false, false, null);
				if (combatTarget != null && base.ParentObject.IsHostileTowards(combatTarget))
				{
					return combatTarget;
				}
			}
			return null;
		}

		// Token: 0x06007606 RID: 30214 RVA: 0x002C4C9C File Offset: 0x002C2E9C
		public GameObject FindTargetOfOpportunity(GameObject Target = null)
		{
			return this.FindTargetOfOpportunity(base.ParentObject.CurrentCell, (Target != null) ? Target.CurrentCell : null);
		}

		// Token: 0x06007607 RID: 30215 RVA: 0x002C4CBC File Offset: 0x002C2EBC
		public override void TakeAction()
		{
			if (!base.ParentObject.IsCombatObject())
			{
				base.FailToParent();
				return;
			}
			base.ParentObject.FireEvent("AICombatStart");
			if (this.Target == null)
			{
				base.Think("I don't have a target any more!");
				base.FailToParent();
				return;
			}
			if (this.Target.Render == null || !this.Target.Render.Visible)
			{
				base.Think("I can't see my target any more!");
				base.FailToParent();
				return;
			}
			if (this.Target.IsNowhere())
			{
				base.Think("My target has been destroyed!");
				base.FailToParent();
				return;
			}
			if (this.Target == this.ParentBrain.PartyLeader)
			{
				base.Think("I shouldn't kill my party leader.");
				base.FailToParent();
				return;
			}
			if (this.ParentBrain.IsPlayerLed() && this.Target.IsPlayerLed())
			{
				base.Think("I shouldn't kill other members of the player's party.");
				base.FailToParent();
				return;
			}
			if (!this.Directed && !this.Target.IsRegardedWithHostilityBy(base.ParentObject))
			{
				base.Think("I'm not hostile to my target anymore.");
				base.FailToParent();
				return;
			}
			Cell currentCell = base.ParentObject.CurrentCell;
			Cell currentCell2 = this.Target.GetCurrentCell();
			if (currentCell2 == null || currentCell == null)
			{
				return;
			}
			if (currentCell2 == currentCell)
			{
				base.Think("I should get some distance from my target.");
				this.PushChildGoal(new Flee(this.Target, 1, false, true));
				return;
			}
			Cell cell = currentCell2;
			GameObject parentObject = base.ParentObject;
			bool flag = !this.Target.IsCombat;
			GameObject combatTarget = cell.GetCombatTarget(parentObject, base.ParentObject.IsFlying, false, false, 0, null, null, null, null, null, flag, false, null);
			if (combatTarget != null && combatTarget != this.Target && !this.ParentBrain.IsHostileTowards(combatTarget))
			{
				base.Think("I shouldn't try to kill something if by doing so I would hit somebody else I don't want to.");
				base.FailToParent();
				return;
			}
			int num = currentCell.DistanceToRespectStairs(currentCell2);
			if (currentCell2.ParentZone == null || currentCell2.ParentZone.ZoneID == null || num > 80)
			{
				this.LastSeen++;
			}
			if (this.LastSeen > 5)
			{
				base.Think("I can't find my target...");
				this.Target = null;
				base.FailToParent();
				return;
			}
			if (this.Target.IsInvalid())
			{
				base.Think("My target is dead!");
				this.Target = null;
				base.FailToParent();
				return;
			}
			if (base.ParentObject.IsAflame() && (base.ParentObject.isDamaged(50, true) || base.ParentObject.Physics.Temperature >= base.ParentObject.Physics.FlameTemperature * 2))
			{
				int num2 = this.ExtinguishSelfTries + 1;
				this.ExtinguishSelfTries = num2;
				if (num2 < 5)
				{
					base.Think("I'm on fire!");
					this.PushChildGoal(new ExtinguishSelf());
					return;
				}
			}
			if (!base.ParentObject.FireEvent("AIBeginKill"))
			{
				return;
			}
			if (currentCell2 != null && num == 1)
			{
				if (!base.ParentObject.FireEvent("AIAttackMelee"))
				{
					if (!base.ParentObject.FireEvent("AICanAttackMelee"))
					{
						this.Target = null;
						base.FailToParent();
					}
					return;
				}
				bool isFlying = this.Target.IsFlying;
				bool isFlying2 = base.ParentObject.IsFlying;
				if ((isFlying && !isFlying2) || !base.ParentObject.PhaseMatches(this.Target))
				{
					GameObject gameObject = this.FindTargetOfOpportunity(base.ParentObject.CurrentCell, null);
					if (gameObject != null && base.ParentObject.FireEvent(Event.New("AISwitchToTargetOfOpportunity", "Target", this.Target, "altTarget", gameObject)))
					{
						this.PushChildGoal(new Kill(gameObject, false));
						return;
					}
					this.PushChildGoal(new Flee(this.Target, 2, false, true));
					return;
				}
				else
				{
					List<string> list = Kill.tryMeleeOrder;
					string tagOrStringProperty = base.ParentObject.GetTagOrStringProperty("customMeleeOrder", null);
					if (tagOrStringProperty != null)
					{
						list = tagOrStringProperty.CachedCommaExpansion();
					}
					else
					{
						list.ShuffleInPlace<string>(null);
					}
					base.Think("I'm going to melee my target in melee!");
					foreach (string text in list)
					{
						if (this.Target == null)
						{
							base.Think("I lost my target.");
							base.FailToParent();
							return;
						}
						if (text == "defensiveItems")
						{
							base.Think("I'm going to try my defensive items.");
							int num3 = base.ParentObject.Stat("Energy", 0);
							if (this.TryDefensiveItems())
							{
								if (base.ParentObject.Stat("Energy", 0) == num3)
								{
									base.ParentObject.UseEnergy(1000);
								}
								return;
							}
						}
						else if (text == "missile")
						{
							if ((this.ParentBrain.PointBlankRange || !this.Target.FlightMatches(base.ParentObject)) && this.TryMissileWeapon())
							{
								return;
							}
						}
						else if (text == "abilities")
						{
							if (this.TryAbilities())
							{
								return;
							}
						}
						else if (text == "defensiveAbilities")
						{
							if (this.TryDefensiveAbilities())
							{
								return;
							}
						}
						else if (text == "items" && this.TryItems())
						{
							return;
						}
					}
					if (isFlying && !isFlying2)
					{
						if (this.ParentBrain.PartyLeader == null)
						{
							this.Target = null;
						}
						base.FailToParent();
						return;
					}
					Cell cell2 = currentCell2;
					if (!base.ParentObject.IsFlying || this.Target.IsFlying)
					{
						Combat.AttackCell(base.ParentObject, cell2, null, 0, 0, 0, false);
						return;
					}
					if (base.ParentObject.IsActivatedAbilityAIUsable(Flight.SWOOP_ATTACK_COMMAND_NAME))
					{
						Combat.SwoopAttack(base.ParentObject, currentCell.GetDirectionFromCell(cell2, false));
						return;
					}
				}
			}
			else
			{
				if (!base.ParentObject.FireEvent("AIAttackRange"))
				{
					if (!base.ParentObject.FireEvent("AICanAttackRange"))
					{
						this.Target = null;
						base.FailToParent();
					}
					return;
				}
				base.Think("I'm going to try attacking my target at range!");
				List<string> list2 = Kill.tryMissileOrder;
				string tagOrStringProperty2 = base.ParentObject.GetTagOrStringProperty("customMissileOrder", null);
				if (tagOrStringProperty2 != null)
				{
					list2 = tagOrStringProperty2.CachedCommaExpansion();
				}
				else
				{
					list2.ShuffleInPlace<string>(null);
				}
				foreach (string text2 in list2)
				{
					if (text2 == "defensiveItems")
					{
						int num4 = base.ParentObject.Stat("Energy", 0);
						if (this.TryDefensiveItems())
						{
							if (base.ParentObject.Stat("Energy", 0) == num4)
							{
								base.ParentObject.UseEnergy(1000);
							}
							return;
						}
					}
					else if (text2 == "defensiveAbilities")
					{
						if (this.TryDefensiveAbilities())
						{
							return;
						}
					}
					else if (text2 == "abilities")
					{
						if (this.TryAbilities())
						{
							return;
						}
					}
					else if (text2 == "missile")
					{
						if (this.TryMissileReload())
						{
							return;
						}
						if (this.TryMissileWeapon())
						{
							return;
						}
					}
					else if (text2 == "thrown")
					{
						if (this.TryThrownWeapon())
						{
							return;
						}
					}
					else if (text2 == "items" && this.TryItems())
					{
						return;
					}
				}
				if (!base.ParentObject.FireEvent("AICantAttackRange"))
				{
					return;
				}
				if (base.ParentObject.IsMobile())
				{
					this.LastAttacked++;
					if (this.LastAttacked > 8 && Math.Max(1, 6 - base.ParentObject.StatMod("Intelligence", 0) * 3 / 2).in100())
					{
						base.Think("I'm going to stop pursuing my target.");
						this.ParentBrain.Goals.Clear();
						if (!base.ParentObject.IsPlayerControlled())
						{
							this.ParentBrain.Hibernating = true;
							this.ParentBrain.PushGoal(new Wait(2));
						}
						return;
					}
					if (num <= 1)
					{
						base.ParentObject.UseEnergy(1000);
						base.Think("I'm close enough to my target.");
						return;
					}
					if (!base.ParentObject.FireEvent(Event.New("AIMovingTowardsTarget", "Target", this.Target)))
					{
						base.ParentObject.UseEnergy(1000);
						base.Think("I'm not allowed to move.");
						return;
					}
					base.Think("I'm going to move towards my target.");
					bool flag2 = this.Target.IsPlayer();
					Cell currentCell3 = base.ParentObject.CurrentCell;
					Cell currentCell4 = this.Target.CurrentCell;
					Zone parentZone = currentCell3.ParentZone;
					Zone parentZone2 = currentCell4.ParentZone;
					if (parentZone == null || parentZone2 == null)
					{
						base.Think("Incoherent zone situation trying to reach target.");
						this.Target = null;
						base.FailToParent();
						return;
					}
					if (parentZone2.IsWorldMap())
					{
						base.Think("Target's on the world map, can't follow!");
						this.Target = null;
						base.FailToParent();
						return;
					}
					if (parentZone2 != currentCell3.ParentZone && base.ParentObject.HasTagOrProperty("StaysOnZLevel") && parentZone2 != null && currentCell3.ParentZone != null && parentZone2.Z != currentCell3.ParentZone.Z)
					{
						base.Think("Target's on another Z level, can't follow!");
						this.Target = null;
						base.FailToParent();
						return;
					}
					if (parentZone.ZoneID == null || parentZone2.ZoneID == null)
					{
						this.Target = null;
						base.FailToParent();
						return;
					}
					FindPath findPath = new FindPath(currentCell3, currentCell4, flag2, true, base.ParentObject, 95, false, false, false, false, false, null);
					if (findPath.Usable)
					{
						base.Think("I found a step to take toward my target, I'm going to try it.");
						this.MoveTries = 0;
						if (this.Target != null && this.Target.IsPlayerControlled())
						{
							parentZone.MarkActive(parentZone2);
						}
						this.PushChildGoal(new Step(findPath.Directions[0], false, false, false, false, this.Target, false));
						return;
					}
					GameObject gameObject2 = this.FindTargetOfOpportunity(currentCell3, currentCell4);
					if (gameObject2 != null && base.ParentObject.FireEvent(Event.New("AISwitchToTargetOfOpportunity", "Target", this.Target, "altTarget", gameObject2)))
					{
						if (gameObject2.IsPlayerControlled())
						{
							parentZone.MarkActive(parentZone2);
						}
						this.PushChildGoal(new Kill(gameObject2, false));
						return;
					}
					if (this.ParentBrain.LimitToAquatic())
					{
						this.PushChildGoal(new Flee(this.Target, 10, false, true));
						return;
					}
					base.Think("I can't find a path.");
					if (!base.ParentObject.FireEvent(Event.New("AIFailCombatPathfind", "Target", this.Target)))
					{
						base.FailToParent();
						return;
					}
					int moveTries = this.MoveTries;
					this.MoveTries = moveTries + 1;
					switch (moveTries)
					{
					case 0:
						break;
					case 1:
						this.ParentBrain.FlushLocalNavigationCaches();
						return;
					case 2:
						this.ParentBrain.FlushZoneNavigationCaches();
						return;
					case 3:
						this.ParentBrain.FlushZoneNavigationCaches();
						this.PushChildGoal(new Wait(Stat.Random(1, 3)));
						return;
					default:
						base.FailToParent();
						return;
					}
				}
				else
				{
					base.ParentObject.UseEnergy(1000);
					base.Think("My target is too far and I'm immobile.");
					base.FailToParent();
				}
			}
		}

		// Token: 0x04003601 RID: 13825
		public GameObject _Target;

		// Token: 0x04003602 RID: 13826
		public int ExtinguishSelfTries;

		// Token: 0x04003603 RID: 13827
		public bool Directed;

		// Token: 0x04003604 RID: 13828
		private int LastSeen;

		// Token: 0x04003605 RID: 13829
		private int LastAttacked;

		// Token: 0x04003606 RID: 13830
		private int MoveTries;

		// Token: 0x04003607 RID: 13831
		[NonSerialized]
		public static List<string> tryMeleeOrder = new List<string> { "items", "missile", "abilities", "defensiveItems", "defensiveAbilities" };

		// Token: 0x04003608 RID: 13832
		[NonSerialized]
		public static List<string> tryMissileOrder = new List<string> { "items", "missile", "abilities", "thrown", "defensiveItems", "defensiveAbilities" };
	}
}
