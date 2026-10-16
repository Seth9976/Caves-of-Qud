using System;
using XRL.Language;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000E95 RID: 3733
	[Serializable]
	public class TurretTinker : IPart
	{
		// Token: 0x0600963A RID: 38458 RVA: 0x00366D54 File Offset: 0x00364F54
		public override bool SameAs(IPart p)
		{
			TurretTinker turretTinker = p as TurretTinker;
			return turretTinker.TurretCooldown == this.TurretCooldown && !(turretTinker.Cooldown != this.Cooldown) && !(turretTinker.TurretType != this.TurretType) && turretTinker.MaxTurretsPlaced == this.MaxTurretsPlaced && base.SameAs(p);
		}

		// Token: 0x0600963B RID: 38459 RVA: 0x00366DBC File Offset: 0x00364FBC
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			if (this.TurretType.Contains("Turret"))
			{
				this.TurretType = null;
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(this.TurretType);
				if (blueprint != null)
				{
					foreach (InventoryObject inventoryObject in blueprint.Inventory)
					{
						GameObjectBlueprint blueprint2 = GameObjectFactory.Factory.GetBlueprint(inventoryObject.Blueprint);
						if (blueprint2 != null && blueprint2.DescendsFrom("MissileWeapon"))
						{
							this.TurretType = inventoryObject.Blueprint;
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600963C RID: 38460 RVA: 0x00366E6C File Offset: 0x0036506C
		public void CollectStats(Templates.StatCollector stats)
		{
			string turretReferenceName = this.GetTurretReferenceName();
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.Cooldown);
			stats.Set("TurretName", this.GetTurretReferenceName(), false, 0);
			stats.Set("An", Grammar.IndefiniteArticleShouldBeAn(turretReferenceName) ? "an" : "a", false, 0);
		}

		// Token: 0x0600963D RID: 38461 RVA: 0x00366ED0 File Offset: 0x003650D0
		public string GetTurretReferenceName()
		{
			if (this.TurretType.IsNullOrEmpty())
			{
				return "turret";
			}
			if (this.TurretType[0] == '*')
			{
				return "random turret";
			}
			if (this.TurretType[0] == '@')
			{
				return "dynamic turret";
			}
			return GameObjectFactory.Factory.GetBlueprint(this.GetTurretWeaponBlueprintInstance()).CachedDisplayNameStripped + " turret";
		}

		// Token: 0x0600963E RID: 38462 RVA: 0x00366F3B File Offset: 0x0036513B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600963F RID: 38463 RVA: 0x00366F51 File Offset: 0x00365151
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009640 RID: 38464 RVA: 0x00366F74 File Offset: 0x00365174
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CommandTinkerTurret");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009641 RID: 38465 RVA: 0x00366FA0 File Offset: 0x003651A0
		public string GetRandomTurretWeaponBlueprint()
		{
			string text = "DynamicInheritsTable:MissileWeapon:Tier" + this.ParentObject.GetTier().ToString();
			int num = 0;
			PopulationResult populationResult;
			for (;;)
			{
				populationResult = PopulationManager.RollOneFrom(text, null, null);
				if (populationResult == null)
				{
					break;
				}
				if (++num > 10)
				{
					goto Block_2;
				}
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(populationResult.Blueprint);
				if (blueprint != null && blueprint.GetPartParameter<bool>("MissileWeapon", "FiresManually", true))
				{
					goto Block_4;
				}
			}
			return "Pump Shotgun";
			Block_2:
			return "Pump Shotgun";
			Block_4:
			return populationResult.Blueprint;
		}

		// Token: 0x06009642 RID: 38466 RVA: 0x00367020 File Offset: 0x00365220
		public string GetTurretWeaponBlueprintInstance()
		{
			string text = this.TurretType;
			if (text == "*")
			{
				text = this.GetRandomTurretWeaponBlueprint();
			}
			else if (text[0] == '@')
			{
				string text2 = text.Substring(1);
				if (!text2.Contains(":Tier"))
				{
					text2 = text2 + ":Tier" + this.ParentObject.GetTier().ToString();
				}
				if (text2.Contains("{zonetier}"))
				{
					text2 = text2.Replace("{zonetier}", ZoneManager.zoneGenerationContextTier.ToString());
				}
				PopulationResult populationResult = PopulationManager.RollOneFrom(text2, null, null);
				text = ((populationResult == null) ? null : populationResult.Blueprint);
			}
			return text;
		}

		// Token: 0x06009643 RID: 38467 RVA: 0x003670C4 File Offset: 0x003652C4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.MaxTurretsPlaced > 0)
				{
					if (this.ParentObject.IsPlayer())
					{
						this.TurretCooldown--;
					}
					else if (!base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
					{
						if (!this.ParentObject.IsPlayerControlled())
						{
							this.ParentObject.Brain.PushGoal(new WanderRandomly(5));
						}
					}
					else if (this.TurretCooldown > 0 && this.ParentObject.Brain != null && !this.ParentObject.Brain.HasGoal("PlaceTurretGoal"))
					{
						this.TurretCooldown--;
					}
					else if (this.TurretCooldown <= 0 && !this.ParentObject.Brain.HasGoal("PlaceTurretGoal"))
					{
						int num = 0;
						Cell cell;
						do
						{
							cell = this.ParentObject.CurrentCell.ParentZone.GetEmptyReachableCells().GetRandomElement<Cell>(null);
							if (cell == null)
							{
								break;
							}
							if (cell.HasObjectWithTag("ExcavatoryTerrainFeature"))
							{
								cell = null;
							}
						}
						while (cell == null && ++num < 10);
						if (cell == null)
						{
							this.ParentObject.Brain.PushGoal(new WanderRandomly(5));
						}
						else
						{
							this.ParentObject.Brain.Goals.Clear();
							string turretWeaponBlueprintInstance = this.GetTurretWeaponBlueprintInstance();
							if (!string.IsNullOrEmpty(turretWeaponBlueprintInstance))
							{
								this.ParentObject.Brain.PushGoal(new PlaceTurretGoal(cell.Location, turretWeaponBlueprintInstance));
							}
						}
						this.TurretCooldown = this.Cooldown.RollCached();
					}
				}
			}
			else if (E.ID == "EnteredCell")
			{
				if (this.TurretType.IsNullOrEmpty())
				{
					this.TurretType = this.GetRandomTurretWeaponBlueprint();
				}
				if (this.TurretType != null && this.ActivatedAbilityID == Guid.Empty)
				{
					this.ActivatedAbilityID = base.AddMyActivatedAbility("Tinker Turret  [" + this.MaxTurretsPlaced.ToString() + " remaining]", "CommandTinkerTurret", "Skills", null, "\u0012", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
				}
			}
			else if (E.ID == "CommandTinkerTurret")
			{
				if (base.GetMyActivatedAbilityCooldown(this.ActivatedAbilityID, null) <= 0 && this.MaxTurretsPlaced > 0)
				{
					Cell cell2 = base.PickDirection("Tinker Turret", null);
					if (cell2 != null && cell2.IsEmpty())
					{
						this.MaxTurretsPlaced--;
						GameObject gameObject = IntegratedWeaponHosts.GenerateTurret(GameObject.Create(this.GetTurretWeaponBlueprintInstance()), this.ParentObject, true);
						cell2.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObject.MakeActive(false);
						gameObject.FireEventOnBodyparts(Event.New("GenerateIntegratedHostInitialAmmo", "Host", gameObject));
						CommandReloadEvent.Execute(gameObject, true, false, 0);
						this.ParentObject.UseEnergy(1000, "Tinker Deploy Turret", null, null, false);
						base.SetMyActivatedAbilityDisplayName(this.ActivatedAbilityID, "Tinker Turret  [" + this.MaxTurretsPlaced.ToString() + " remaining]", null);
						base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.Cooldown.RollCached(), null, null, false);
					}
				}
				if (this.MaxTurretsPlaced <= 0)
				{
					this.ParentObject.ShowFailure("You are out of turrets to place.");
					base.DisableMyActivatedAbility(this.ActivatedAbilityID, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003E34 RID: 15924
		public int TurretCooldown;

		// Token: 0x04003E35 RID: 15925
		public string Cooldown = "5-10";

		// Token: 0x04003E36 RID: 15926
		public string TurretType;

		// Token: 0x04003E37 RID: 15927
		public int MaxTurretsPlaced = 10;

		// Token: 0x04003E38 RID: 15928
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
