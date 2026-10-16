using System;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;
using XRL.UI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000E6A RID: 3690
	[Serializable]
	public class Cloneling : IPart
	{
		// Token: 0x06009514 RID: 38164 RVA: 0x0035EA40 File Offset: 0x0035CC40
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CanDrinkEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x06009515 RID: 38165 RVA: 0x0035EA91 File Offset: 0x0035CC91
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.ConsiderCloning();
			return base.HandleEvent(E);
		}

		// Token: 0x06009516 RID: 38166 RVA: 0x0035EAA1 File Offset: 0x0035CCA1
		public override bool HandleEvent(CanDrinkEvent E)
		{
			if (E.Liquid.MilliAmount("cloning") * 40 / 1000 > 0)
			{
				E.CanDrinkThis = true;
			}
			else
			{
				E.CouldDrinkOther = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009517 RID: 38167 RVA: 0x0035EAD6 File Offset: 0x0035CCD6
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Cloneling.COMMAND_NAME)
			{
				this.AttemptCloning();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009518 RID: 38168 RVA: 0x0035EAF8 File Offset: 0x0035CCF8
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ClonesLeft = "10-40".Roll();
			this.SyncAbility();
			return base.HandleEvent(E);
		}

		// Token: 0x06009519 RID: 38169 RVA: 0x0035EB18 File Offset: 0x0035CD18
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (20.in100() && this.ClonesLeft.in100())
			{
				IInventory dropInventory = this.ParentObject.GetDropInventory();
				if (dropInventory != null)
				{
					GameObject gameObject = GameObject.Create("Phial");
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					if (liquidVolume != null)
					{
						liquidVolume.InitialLiquid = "cloning";
						liquidVolume.Volume = 1;
					}
					dropInventory.AddObjectToInventory(gameObject, this.ParentObject, false, false, true, null, E);
					DroppedEvent.Send(this.ParentObject, gameObject, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600951A RID: 38170 RVA: 0x0035EB98 File Offset: 0x0035CD98
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.ParentObject.IsHostileTowards(E.Actor))
			{
				E.AddAction("ResupplyForCloning", "resupply [1 dram of " + LiquidVolume.GetLiquid("cloning").GetName().Strip() + "]", "ResupplyForCloning", null, 'r', false, -1, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600951B RID: 38171 RVA: 0x0035EC08 File Offset: 0x0035CE08
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ResupplyForCloning")
			{
				if (E.Actor.UseDrams(1, "cloning", null, null, null, null, false))
				{
					this.ClonesLeft = 40;
					this.SyncAbility();
					IComponent<GameObject>.XDidYToZ(E.Actor, "resupply", this.ParentObject, "with " + LiquidVolume.GetLiquid("cloning").GetName(), null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					base.PlayWorldSound("Sounds/Interact/sfx_interact_robot_resupply_liquid", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					E.Actor.UseEnergy(1000, "Action Resupply", null, null, false);
					E.RequestInterfaceExit();
				}
				else if (E.Actor.IsPlayer())
				{
					Popup.ShowFail("You do not have 1 dram of " + LiquidVolume.GetLiquid("cloning").GetName() + ".", true, true, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600951C RID: 38172 RVA: 0x0035ED30 File Offset: 0x0035CF30
		public override void Initialize()
		{
			base.Initialize();
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Clone", Cloneling.COMMAND_NAME, "Onboard Systems", null, "\u0013", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, Renderable.UITile("Abilities/abil_clone.bmp", 'r', 'W', " ", '\0'), null, null, null);
		}

		// Token: 0x0600951D RID: 38173 RVA: 0x0035ED8A File Offset: 0x0035CF8A
		public override void Remove()
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.Remove();
		}

		// Token: 0x0600951E RID: 38174 RVA: 0x0035EDA0 File Offset: 0x0035CFA0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600951F RID: 38175 RVA: 0x0035EDA3 File Offset: 0x0035CFA3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DrinkingFrom");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009520 RID: 38176 RVA: 0x0035EDB8 File Offset: 0x0035CFB8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DrinkingFrom")
			{
				int num = Math.Min(E.GetGameObjectParameter("Container").LiquidVolume.MilliAmount("cloning") * 40 / 1000, 40);
				if (num > this.ClonesLeft)
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.Show("Your cloning capacity is refreshed.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					this.ClonesLeft = num;
					this.SyncAbility();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009521 RID: 38177 RVA: 0x0035EE44 File Offset: 0x0035D044
		public void SyncAbility()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Clone [").Append(this.ClonesLeft).Append(" left]");
			base.SetMyActivatedAbilityDisplayName(this.ActivatedAbilityID, stringBuilder.ToString(), null);
			if (this.ClonesLeft <= 0)
			{
				base.DisableMyActivatedAbility(this.ActivatedAbilityID, null);
				return;
			}
			base.EnableMyActivatedAbility(this.ActivatedAbilityID, null);
		}

		// Token: 0x06009522 RID: 38178 RVA: 0x0035EEB3 File Offset: 0x0035D0B3
		public bool ConsiderWandering()
		{
			if (this.ParentObject.IsPlayerControlled())
			{
				return false;
			}
			if (!this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				return false;
			}
			this.ParentObject.Brain.PushGoal(new WanderRandomly(5));
			return true;
		}

		// Token: 0x06009523 RID: 38179 RVA: 0x0035EEF0 File Offset: 0x0035D0F0
		public bool ConsiderCloning()
		{
			if (this.ParentObject.IsPlayer())
			{
				return false;
			}
			if (this.ClonesLeft <= 0)
			{
				this.ConsiderWandering();
				return false;
			}
			if (!base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				this.ConsiderWandering();
				return false;
			}
			if (!this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				return false;
			}
			for (int i = 0; i < this.ParentObject.Brain.Goals.Items.Count; i++)
			{
				if (this.ParentObject.Brain.Goals.Items[i].GetType().FullName.Contains("Cloneling"))
				{
					return false;
				}
			}
			GameObject randomElement = this.ParentObject.CurrentZone.FastCombatSquareVisibility(this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y, 12, this.ParentObject, new Predicate<GameObject>(this.CanBeCloned), false, false, true, true, null).GetRandomElement<GameObject>(null);
			if (randomElement == null)
			{
				this.ConsiderWandering();
				return false;
			}
			if (this.ParentObject.Brain.Staying && this.ParentObject.DistanceTo(randomElement) > 1)
			{
				return false;
			}
			this.ParentObject.Brain.Goals.Clear();
			this.ParentObject.Brain.PushGoal(new ClonelingGoal(randomElement));
			return true;
		}

		// Token: 0x06009524 RID: 38180 RVA: 0x0035F04E File Offset: 0x0035D24E
		public bool CanBeCloned(GameObject Target)
		{
			return Cloning.CanBeCloned(Target, this.ParentObject, "Cloneling", false) && Target.PhaseAndFlightMatches(this.ParentObject);
		}

		// Token: 0x06009525 RID: 38181 RVA: 0x0035F078 File Offset: 0x0035D278
		public bool PerformCloning(GameObject Target)
		{
			if (this.ClonesLeft <= 0)
			{
				return false;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			Cell randomElement = currentCell.GetLocalEmptyAdjacentCells().GetRandomElement<Cell>(null);
			if (randomElement == null)
			{
				return false;
			}
			string geneID = Target.GeneID;
			GameObject gameObject = Target.DeepCopy(false, false, null);
			gameObject.StripContents(true, true);
			Statistic stat = gameObject.GetStat("XPValue");
			if (stat != null)
			{
				stat.BaseValue /= 2;
			}
			gameObject.RestorePristineHealth(false, false);
			gameObject.RemoveIntProperty("ProperNoun");
			gameObject.RemoveIntProperty("Renamed");
			gameObject.ModIntProperty("IsClone", 1, false);
			gameObject.ModIntProperty("IsClonelingClone", 1, false);
			gameObject.SetStringProperty("CloneOf", Target.ID, false);
			gameObject.SetStringProperty("CloneOfGenes", geneID, false);
			if (gameObject.Render != null && !Target.HasPropertyOrTag("CloneNoNameChange") && !Target.BaseDisplayName.Contains("clone of"))
			{
				if (Target.HasProperName)
				{
					gameObject.Render.DisplayName = "clone of " + Target.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, false, false, true, false, null, false);
				}
				else
				{
					string text = gameObject.GetBlueprint(true).DisplayName();
					if (!text.IsNullOrEmpty() && !text.Contains("["))
					{
						gameObject.Render.DisplayName = "clone of " + Grammar.A(text, false);
					}
					else
					{
						gameObject.Render.DisplayName = "clone of " + Target.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, false, false, true, false, null, false);
					}
				}
			}
			Brain brain = gameObject.Brain;
			if (brain != null)
			{
				brain.Mindwipe();
			}
			if (!Achievement.CLONES_30.Achieved && Target.IsPlayer())
			{
				Cloning.QueueAchievementCheck();
			}
			randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			gameObject.MakeActive(false);
			WasReplicatedEvent.Send(Target, this.ParentObject, gameObject, "Cloneling", false);
			ReplicaCreatedEvent.Send(gameObject, this.ParentObject, Target, "Cloneling", false);
			gameObject.Bloodsplatter(false);
			this.ParentObject.UseEnergy(1000, "Ability Clone", null, null, false);
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, "2d10".Roll(), null, null, false);
			this.ClonesLeft--;
			this.SyncAbility();
			this.ParentObject.UseEnergy(1000, "Ability Clone", null, null, false);
			Messaging.XDidYToZ(this.ParentObject, "produce", "a clone of", Target, "in a flurry of {{C|flashing chrome}} and {{cloning|spurting liquid}}", null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			if (gameObject != null)
			{
				gameObject.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_budding_clone_complete", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x06009526 RID: 38182 RVA: 0x0035F364 File Offset: 0x0035D564
		public bool AttemptCloning()
		{
			if (this.ClonesLeft <= 0)
			{
				return this.ParentObject.Fail("Your onboard systems are out of " + LiquidVolume.GetLiquid("cloning").GetName() + ".");
			}
			if (!base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null))
			{
				return this.ParentObject.Fail("Your onboard cloning systems are offline.");
			}
			Cell cell = base.PickDirection("Clone", null);
			if (cell == null)
			{
				return false;
			}
			GameObject combatTarget = cell.GetCombatTarget(this.ParentObject, true, false, false, 5, null, null, null, null, null, true, false, null);
			if (combatTarget == null)
			{
				return this.ParentObject.Fail("There is nothing there for you to clone.");
			}
			if (!this.CanBeCloned(combatTarget))
			{
				if (this.ParentObject.IsPlayer())
				{
					if (!combatTarget.FlightMatches(this.ParentObject))
					{
						this.ParentObject.Fail("You cannot reach " + combatTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					}
					else if (!combatTarget.PhaseMatches(this.ParentObject))
					{
						this.ParentObject.Fail("You cannot make contact with " + combatTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					}
					else if (Cloning.CanBeCloned(combatTarget, null, null, false))
					{
						this.ParentObject.Fail("You cannot clone " + combatTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					}
					else
					{
						this.ParentObject.Fail(combatTarget.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " cannot be cloned.");
					}
				}
				return false;
			}
			return !this.PerformCloning(combatTarget) && this.ParentObject.Fail("You fail to clone " + combatTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
		}

		// Token: 0x04003DC8 RID: 15816
		public const int CLONES_PER_DRAM = 40;

		// Token: 0x04003DC9 RID: 15817
		public const string INITIAL_CLONES = "10-40";

		// Token: 0x04003DCA RID: 15818
		public const string REPLICATION_CONTEXT = "Cloneling";

		// Token: 0x04003DCB RID: 15819
		public static readonly string COMMAND_NAME = "CommandClone";

		// Token: 0x04003DCC RID: 15820
		public int ClonesLeft;

		// Token: 0x04003DCD RID: 15821
		public Guid ActivatedAbilityID;
	}
}
