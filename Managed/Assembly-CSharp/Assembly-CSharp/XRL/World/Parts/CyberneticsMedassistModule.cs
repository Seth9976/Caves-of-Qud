using System;
using System.Collections.Generic;
using System.Text;
using XRL.Collections;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F34 RID: 3892
	[Serializable]
	public class CyberneticsMedassistModule : IPoweredPart
	{
		// Token: 0x06009B27 RID: 39719 RVA: 0x003845D7 File Offset: 0x003827D7
		public CyberneticsMedassistModule()
		{
			this.ChargeUse = 0;
			this.WorksOnImplantee = true;
			this.NameForStatus = "DeploymentExpertSystem";
		}

		// Token: 0x06009B28 RID: 39720 RVA: 0x0038460A File Offset: 0x0038280A
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Loadout", this.GetCurrentLoadout().ToString(), false, 0);
		}

		// Token: 0x06009B29 RID: 39721 RVA: 0x00384624 File Offset: 0x00382824
		public StringBuilder GetCurrentLoadout()
		{
			Inventory inventory = this.ParentObject.Inventory;
			if (inventory != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("\n{{c|Current loadout:}}");
				if (inventory.Objects.Count > 0)
				{
					using (List<GameObject>.Enumerator enumerator = inventory.Objects.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GameObject gameObject = enumerator.Current;
							stringBuilder.Append("\n ").Append('ú').Append(' ')
								.Append(gameObject.DisplayName);
						}
						return stringBuilder;
					}
				}
				stringBuilder.Append(" {{y|no injectors}}");
				return stringBuilder;
			}
			return null;
		}

		// Token: 0x06009B2A RID: 39722 RVA: 0x003846DC File Offset: 0x003828DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == InventoryActionEvent.ID || ID == OwnerGetInventoryActionsEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009B2B RID: 39723 RVA: 0x00384735 File Offset: 0x00382935
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B2C RID: 39724 RVA: 0x0038476C File Offset: 0x0038296C
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "BeforeApplyDamage");
			E.Implantee.RegisterPartEvent(this, "CommandToggleMedassistModule");
			E.Implantee.RegisterPartEvent(this, "OwnerGetInventoryActions");
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Medassist Module", CyberneticsMedassistModule.COMMAND_NAME, "Cybernetics", null, "\a", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B2D RID: 39725 RVA: 0x003847EC File Offset: 0x003829EC
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "BeforeApplyDamage");
			E.Implantee.UnregisterPartEvent(this, "CommandToggleMedassistModule");
			E.Implantee.UnregisterPartEvent(this, "OwnerGetInventoryActions");
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B2E RID: 39726 RVA: 0x00384848 File Offset: 0x00382A48
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == CyberneticsMedassistModule.COMMAND_NAME && E.Actor == this.ParentObject.Implantee)
			{
				this.ParentObject.Implantee.ToggleActivatedAbility(this.ActivatedAbilityID, false, null);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B2F RID: 39727 RVA: 0x003848B5 File Offset: 0x00382AB5
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Infix = this.GetCurrentLoadout();
			return base.HandleEvent(E);
		}

		// Token: 0x06009B30 RID: 39728 RVA: 0x003848CC File Offset: 0x00382ACC
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Load Tonics", "load tonics", "LoadTonics", null, 'o', false, 0, 0, false, false, false, false, true, null, false);
			E.AddAction("Eject Tonics", "eject tonics", "EjectTonics", null, 'j', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B31 RID: 39729 RVA: 0x00384928 File Offset: 0x00382B28
		public override bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			if (CyberneticsMedassistModule.CanBeLoaded(E.Object))
			{
				E.AddAction("Load Into Medassist Module", "load into medassist module", "LoadTonic", null, 'm', false, 0, 0, false, false, false, false, true, this.ParentObject, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B32 RID: 39730 RVA: 0x00384974 File Offset: 0x00382B74
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "LoadTonic")
			{
				Inventory inventory = this.ParentObject.Inventory;
				if (inventory == null)
				{
					throw new Exception("inventory missing from " + this.ParentObject.DebugName);
				}
				if (!CyberneticsMedassistModule.CanBeLoaded(E.Item))
				{
					throw new Exception("received invalid item " + E.Item.DebugName);
				}
				if (this.GetLoadedCount() >= this.TonicCapacity)
				{
					E.Actor.Fail(E.Actor.Poss(this.ParentObject, true, null) + this.ParentObject.Is + " full.");
				}
				else
				{
					E.Item.PlayWorldSound("Sounds/Interact/sfx_interact_medassistModule_tonic_load", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					GameObject gameObject = E.Item.RemoveOne();
					gameObject.RemoveFromContext(null);
					inventory.AddObject(gameObject, null, false, false, true, null);
					if (E.Actor.IsPlayer())
					{
						Popup.Show(string.Concat(new string[]
						{
							"You slot ",
							gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							" into ",
							E.Actor.poss(this.ParentObject, true, null),
							"."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					E.Actor.UseEnergy(1000);
					E.RequestInterfaceExit();
				}
			}
			else
			{
				if (E.Command == "LoadTonics")
				{
					Inventory inventory2 = this.ParentObject.Inventory;
					if (inventory2 == null)
					{
						throw new Exception("inventory missing from " + this.ParentObject.DebugName);
					}
					if (this.GetLoadedCount() >= this.TonicCapacity)
					{
						E.Actor.Fail(E.Actor.Poss(this.ParentObject, true, null) + this.ParentObject.Is + " full.");
						goto IL_05ED;
					}
					using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
					{
						using (ScopeDisposedList<GameObject> fromPool2 = ScopeDisposedList<GameObject>.GetFromPool())
						{
							E.Actor.GetContents(fromPool2);
							foreach (GameObject gameObject2 in fromPool2)
							{
								if (CyberneticsMedassistModule.CanBeLoaded(gameObject2))
								{
									BodyPart bodyPart;
									int num;
									IContextRelationManager contextRelationManager;
									GameObject objectContext = gameObject2.GetObjectContext(out bodyPart, out num, out contextRelationManager);
									if (objectContext != null && objectContext != this.ParentObject && num != 4 && num != 6)
									{
										fromPool.Add(gameObject2);
									}
								}
							}
							if (fromPool.Count <= 0)
							{
								E.Actor.Fail("You have no tonics to load.");
								goto IL_05ED;
							}
							GameObject gameObject3 = PickItem.ShowPicker(fromPool, null, PickItem.PickItemDialogStyle.SelectItemDialog, E.Actor, null, null, null, false, null, true, true, false);
							if (gameObject3 != null)
							{
								E.Item.PlayWorldSound("Sounds/Interact/sfx_interact_medassistModule_tonic_load", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
								gameObject3.SplitFromStack();
								gameObject3.RemoveFromContext(null);
								inventory2.AddObject(gameObject3, null, false, false, true, null);
								if (E.Actor.IsPlayer())
								{
									Popup.Show(string.Concat(new string[]
									{
										"You slot ",
										gameObject3.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
										" into ",
										E.Actor.poss(this.ParentObject, true, null),
										"."
									}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
								}
								E.Actor.UseEnergy(1000);
								E.RequestInterfaceExit();
							}
							goto IL_05ED;
						}
					}
				}
				if (E.Command == "EjectTonics")
				{
					Inventory inventory3 = this.ParentObject.Inventory;
					if (inventory3 == null)
					{
						throw new Exception("inventory missing from " + this.ParentObject.DebugName);
					}
					int num2 = 0;
					foreach (GameObject gameObject4 in inventory3.Objects)
					{
						num2 += gameObject4.Count;
					}
					if (num2 <= 0)
					{
						E.Actor.Fail(E.Actor.Poss(this.ParentObject, true, null) + this.ParentObject.Is + " empty.");
					}
					else
					{
						E.Item.PlayWorldSound("Sounds/Interact/sfx_interact_medassistModule_tonic_eject", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						int num3 = this.TonicCapacity * 3;
						GameObject gameObject5 = null;
						while (inventory3.Objects.Count > 0 && --num3 > 0)
						{
							gameObject5 = inventory3.Objects[0];
							E.Actor.TakeObject(gameObject5, false, false, new int?(0), null, null);
						}
						if (E.Actor.IsPlayer())
						{
							Popup.Show(string.Concat(new string[]
							{
								"You eject ",
								(num2 == 1) ? gameObject5.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) : "the injectors",
								" from ",
								E.Actor.poss(this.ParentObject, true, null),
								"."
							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							E.Actor.UseEnergy(1000);
							E.RequestInterfaceExit();
						}
					}
				}
			}
			IL_05ED:
			return base.HandleEvent(E);
		}

		// Token: 0x06009B33 RID: 39731 RVA: 0x00384FDC File Offset: 0x003831DC
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B34 RID: 39732 RVA: 0x00384FDF File Offset: 0x003831DF
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009B35 RID: 39733 RVA: 0x00384FE2 File Offset: 0x003831E2
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.AttemptMedicalAssistance(null);
		}

		// Token: 0x06009B36 RID: 39734 RVA: 0x00384FEB File Offset: 0x003831EB
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return !this.ParentObject.Implantee.IsActivatedAbilityToggledOn(this.ActivatedAbilityID);
		}

		// Token: 0x06009B37 RID: 39735 RVA: 0x00385006 File Offset: 0x00383206
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "Deactivated";
		}

		// Token: 0x06009B38 RID: 39736 RVA: 0x00385010 File Offset: 0x00383210
		public void AttemptMedicalAssistance(Damage damage = null)
		{
			if (base.IsDisabled(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return;
			}
			Inventory inventory = this.ParentObject.Inventory;
			if (inventory == null || inventory.Objects.Count <= 0)
			{
				return;
			}
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee.GetTonicEffectCount() >= implantee.GetTonicCapacity())
			{
				return;
			}
			int num = 0;
			GameObject gameObject = null;
			int i = 0;
			int count = inventory.Objects.Count;
			while (i < count)
			{
				GameObject gameObject2 = inventory.Objects[i];
				if (!gameObject2.IsBroken() && !gameObject2.IsRusted())
				{
					int utilityScore = CyberneticsMedassistModule.GetUtilityScore(implantee, gameObject2, damage);
					if (utilityScore > num)
					{
						gameObject = gameObject2;
						num = utilityScore;
					}
				}
				i++;
			}
			if (gameObject == null)
			{
				return;
			}
			if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return;
			}
			int @for = GetTonicDosageEvent.GetFor(gameObject, implantee, this.ParentObject, 1);
			if (@for <= 0)
			{
				return;
			}
			List<Effect> tonicEffects = implantee.GetTonicEffects();
			if (implantee.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("inject", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " you with " + gameObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", null, true);
			}
			Event @event = Event.New("ApplyingTonic", 0, 0, 0);
			@event.SetParameter("Subject", implantee);
			@event.SetParameter("Actor", this.ParentObject);
			@event.SetParameter("Tonic", gameObject);
			@event.SetParameter("Dosage", @for);
			@event.SetFlag("External", false);
			@event.SetFlag("Involuntary", true);
			if (!implantee.FireEvent(@event))
			{
				if (implantee.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("The injection fails.", null, true);
				}
				return;
			}
			Event event2 = Event.New("ApplyTonic", 0, 0, 0);
			event2.SetParameter("Owner", this.ParentObject);
			event2.SetParameter("Target", implantee);
			event2.SetParameter("Actor", this.ParentObject);
			event2.SetParameter("Subject", implantee);
			event2.SetParameter("Attacker", null);
			event2.SetParameter("Overdose", "No");
			event2.SetParameter("Dosage", @for);
			event2.SetFlag("External", false);
			event2.SetFlag("Involuntary", true);
			if (!gameObject.FireEvent(event2))
			{
				if (implantee.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("The injection fails.", null, true);
				}
				return;
			}
			gameObject.Destroy(null, false, false, null);
			Event event3 = Event.New("TonicAutoApplied", 0, 0, 0);
			event3.SetParameter("Subject", implantee);
			event3.SetParameter("By", this.ParentObject);
			event3.SetParameter("Damage", damage);
			foreach (Effect effect in implantee.GetTonicEffects())
			{
				if (!tonicEffects.Contains(effect))
				{
					effect.FireEvent(event3);
				}
			}
		}

		// Token: 0x06009B39 RID: 39737 RVA: 0x00385380 File Offset: 0x00383580
		public static int GetUtilityScore(GameObject who, GameObject tonic, Damage damage = null)
		{
			return GetUtilityScoreEvent.GetFor(who, tonic, damage, false);
		}

		// Token: 0x06009B3A RID: 39738 RVA: 0x0038538B File Offset: 0x0038358B
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06009B3B RID: 39739 RVA: 0x00385390 File Offset: 0x00383590
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null)
				{
					GameObject implantee = this.ParentObject.Implantee;
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					if (E.HasParameter("Phase"))
					{
						int intParameter = E.GetIntParameter("Phase", 0);
						bool flag = implantee.PhaseMatches(intParameter);
						this.AttemptMedicalAssistance(damage);
						if (flag && !implantee.PhaseMatches(intParameter))
						{
							return false;
						}
					}
					else if (gameObjectParameter != null)
					{
						bool flag2 = implantee.PhaseMatches(gameObjectParameter);
						this.AttemptMedicalAssistance(damage);
						if (flag2 && !implantee.PhaseMatches(gameObjectParameter))
						{
							return false;
						}
					}
					else
					{
						int phase = implantee.GetPhase();
						this.AttemptMedicalAssistance(damage);
						if (!implantee.PhaseMatches(phase))
						{
							return false;
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009B3C RID: 39740 RVA: 0x00385458 File Offset: 0x00383658
		public static bool CanBeLoaded(GameObject obj)
		{
			Tonic part = obj.GetPart<Tonic>();
			return part != null && !part.Eat;
		}

		// Token: 0x06009B3D RID: 39741 RVA: 0x0038547C File Offset: 0x0038367C
		public int GetLoadedCount()
		{
			int num = 0;
			Inventory inventory = this.ParentObject.Inventory;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.Objects)
				{
					num += gameObject.Count;
				}
			}
			return num;
		}

		// Token: 0x04004067 RID: 16487
		public static readonly string COMMAND_NAME = "CommandToggleMedassistModule";

		// Token: 0x04004068 RID: 16488
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004069 RID: 16489
		public int TonicCapacity = 8;
	}
}
