using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x020011D8 RID: 4568
	[Serializable]
	public class PowerSwitch : IPoweredPart, IHackingSifrahHandler
	{
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600B600 RID: 46592 RVA: 0x00426668 File Offset: 0x00424868
		// (set) Token: 0x0600B5FF RID: 46591 RVA: 0x0042665A File Offset: 0x0042485A
		public int SecurityClearance
		{
			get
			{
				return XRL.World.Capabilities.SecurityClearance.GetSecurityClearanceByKeySpecification(this.KeyObject);
			}
			set
			{
				XRL.World.Capabilities.SecurityClearance.HandleSecurityClearanceSpecification(value, ref this.KeyObject);
			}
		}

		// Token: 0x0600B601 RID: 46593 RVA: 0x00426678 File Offset: 0x00424878
		public PowerSwitch()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = false;
			this.IsPowerSwitchSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B602 RID: 46594 RVA: 0x0042678C File Offset: 0x0042498C
		public override bool SameAs(IPart p)
		{
			PowerSwitch powerSwitch = p as PowerSwitch;
			return powerSwitch.Active == this.Active && powerSwitch.Flippable == this.Flippable && powerSwitch.FlippableKinetically == this.FlippableKinetically && powerSwitch.FlippableWithoutUnderstanding == this.FlippableWithoutUnderstanding && powerSwitch.ChanceActive == this.ChanceActive && powerSwitch.ActivateActionPriority == this.ActivateActionPriority && powerSwitch.DeactivateActionPriority == this.DeactivateActionPriority && powerSwitch.EnergyCost == this.EnergyCost && !(powerSwitch.KeyObject != this.KeyObject) && powerSwitch.KeylessActivation == this.KeylessActivation && powerSwitch.KeylessDeactivation == this.KeylessDeactivation && !(powerSwitch.FrequencyCode != this.FrequencyCode) && !(powerSwitch.ActivateVerb != this.ActivateVerb) && !(powerSwitch.ActivatePreposition != this.ActivatePreposition) && !(powerSwitch.ActivateExtra != this.ActivateExtra) && !(powerSwitch.ActivateSuccessMessage != this.ActivateSuccessMessage) && !(powerSwitch.ActivateFailureMessage != this.ActivateFailureMessage) && !(powerSwitch.DeactivateVerb != this.DeactivateVerb) && !(powerSwitch.DeactivatePreposition != this.DeactivatePreposition) && !(powerSwitch.DeactivateExtra != this.DeactivateExtra) && !(powerSwitch.DeactivateSuccessMessage != this.DeactivateSuccessMessage) && !(powerSwitch.DeactivateFailureMessage != this.DeactivateFailureMessage) && !(powerSwitch.PsychometryAccessMessage != this.PsychometryAccessMessage) && !(powerSwitch.AccessFailureMessage != this.AccessFailureMessage) && !(powerSwitch.ActiveAdjective != this.ActiveAdjective) && !(powerSwitch.InactiveAdjective != this.InactiveAdjective) && !(powerSwitch.ActiveDescription != this.ActiveDescription) && !(powerSwitch.InactiveDescription != this.InactiveDescription) && base.SameAs(p);
		}

		// Token: 0x0600B603 RID: 46595 RVA: 0x004269C4 File Offset: 0x00424BC4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == BootSequenceDoneEvent.ID || (ID == CanSmartUseEvent.ID && this.Flippable) || ID == CellChangedEvent.ID || (ID == CommandSmartUseEarlyEvent.ID && this.Flippable) || ID == EffectRemovedEvent.ID || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetHostileWalkRadiusEvent.ID || ID == GetShortDescriptionEvent.ID || ID == GetInventoryActionsAlwaysEvent.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID || ID == ObjectCreatedEvent.ID || ID == PooledEvent<SyncRenderEvent>.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B604 RID: 46596 RVA: 0x00426A8C File Offset: 0x00424C8C
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			this.SyncActivatedAbilityNameAndIcon(null);
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped != null)
			{
				equipped.DescribeActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B605 RID: 46597 RVA: 0x00426AC5 File Offset: 0x00424CC5
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.SetUpActivatedAbility(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B606 RID: 46598 RVA: 0x00426AD5 File Offset: 0x00424CD5
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.SetUpActivatedAbility(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B607 RID: 46599 RVA: 0x00426AE5 File Offset: 0x00424CE5
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.SetUpActivatedAbility(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B608 RID: 46600 RVA: 0x00426AF5 File Offset: 0x00424CF5
		public override bool HandleEvent(EquippedEvent E)
		{
			this.SetUpActivatedAbility(E.Actor);
			E.Actor.RegisterPartEvent(this, this.GetActivatedAbilityCommandName(null));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B609 RID: 46601 RVA: 0x00426B1D File Offset: 0x00424D1D
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, this.GetActivatedAbilityCommandName(E.Actor));
			E.Actor.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B60A RID: 46602 RVA: 0x00426B50 File Offset: 0x00424D50
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			return (E.Object != this.ParentObject || this.Active || this.ParentObject.IsPlayer()) && base.HandleEvent(E);
		}

		// Token: 0x0600B60B RID: 46603 RVA: 0x00426B80 File Offset: 0x00424D80
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == this.ParentObject && !this.Active && !this.ParentObject.HasPart<ArtificialIntelligence>())
			{
				E.Message = this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + this.ParentObject.Is + " utterly unresponsive.";
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B60C RID: 46604 RVA: 0x00426BFE File Offset: 0x00424DFE
		public override bool HandleEvent(GetHostileWalkRadiusEvent E)
		{
			if (!this.Active)
			{
				E.Radius = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B60D RID: 46605 RVA: 0x00426C18 File Offset: 0x00424E18
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.Active)
			{
				if (!this.ActiveAdjective.IsNullOrEmpty() && E.Understood() && !E.Object.HasProperName)
				{
					E.AddAdjective(this.ActiveAdjective, 0);
				}
			}
			else if (!this.InactiveAdjective.IsNullOrEmpty() && E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective(this.InactiveAdjective, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B60E RID: 46606 RVA: 0x00426C94 File Offset: 0x00424E94
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Active)
			{
				if (!this.ActiveDescription.IsNullOrEmpty())
				{
					E.Postfix.Append('\n').Append(GameText.VariableReplace(this.ActiveDescription, this.ParentObject, null, false));
				}
			}
			else if (!this.InactiveDescription.IsNullOrEmpty())
			{
				E.Postfix.Append('\n').Append(GameText.VariableReplace(this.InactiveDescription, this.ParentObject, null, false));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B60F RID: 46607 RVA: 0x00426D18 File Offset: 0x00424F18
		public override bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			if (this.Flippable && (this.FlippableWithoutUnderstanding || this.ParentObject.Understood()) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.Active)
				{
					E.AddAction("Deactivate", "deactivate", "PowerSwitchOff", null, 'a', false, this.DeactivateActionPriority, 0, false, false, this.FlippableKinetically, false, true, null, false);
				}
				else
				{
					E.AddAction("Activate", "activate", "PowerSwitchOn", null, 'a', false, this.ActivateActionPriority, 0, false, false, this.FlippableKinetically, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B610 RID: 46608 RVA: 0x00426DDC File Offset: 0x00424FDC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "PowerSwitchOn")
			{
				this.TryPowerSwitchOn(E.Actor, E);
			}
			else if (E.Command == "PowerSwitchOff")
			{
				this.TryPowerSwitchOff(E.Actor, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B611 RID: 46609 RVA: 0x00426E30 File Offset: 0x00425030
		public override bool HandleEvent(SyncRenderEvent E)
		{
			this.SyncRender();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B612 RID: 46610 RVA: 0x00426E3F File Offset: 0x0042503F
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (!this.Flippable || !E.Actor.IsPlayer() || (!this.FlippableWithoutUnderstanding && !this.ParentObject.Understood())) && base.HandleEvent(E);
		}

		// Token: 0x0600B613 RID: 46611 RVA: 0x00426E74 File Offset: 0x00425074
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			if (this.Flippable && E.Actor.IsPlayer() && (this.FlippableWithoutUnderstanding || this.ParentObject.Understood()) && !this.ParentObject.HasTagOrProperty("SuppressPowerSwitchTwiddle"))
			{
				this.ParentObject.Twiddle(false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B614 RID: 46612 RVA: 0x00426ED3 File Offset: 0x004250D3
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.ChanceActive >= 0)
			{
				this.Active = this.ChanceActive.in100();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B615 RID: 46613 RVA: 0x00426EF6 File Offset: 0x004250F6
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			SyncRenderEvent.Send(this.ParentObject);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B616 RID: 46614 RVA: 0x00426F0A File Offset: 0x0042510A
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600B617 RID: 46615 RVA: 0x00426F0D File Offset: 0x0042510D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LiquidFueledPowerPlantFueled");
			Registrar.Register("PowerSwitchActivate");
			Registrar.Register("PowerSwitchDeactivate");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B618 RID: 46616 RVA: 0x00426F38 File Offset: 0x00425138
		public override bool FireEvent(Event E)
		{
			if (E.ID == "PowerSwitchActivate")
			{
				if ((!this.CanActivate || !base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && !E.HasFlag("Forced"))
				{
					this.ParentObject.FireEvent("PowerSwitchActivateFailed");
					return false;
				}
				this.Active = true;
				PowerSwitchFlippedEvent.Send(E.GetGameObjectParameter("Actor"), this.ParentObject);
				this.ParentObject.FireEvent("PowerSwitchActivated");
				SyncRenderEvent.Send(this.ParentObject);
				this.SyncActivatedAbilityNameAndIcon(null);
			}
			else if (E.ID == "PowerSwitchDeactivate")
			{
				if ((!this.CanDeactivate || !base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && !E.HasFlag("Forced"))
				{
					this.ParentObject.FireEvent("PowerSwitchDeactivateFailed");
					return false;
				}
				this.Active = false;
				PowerSwitchFlippedEvent.Send(E.GetGameObjectParameter("Actor"), this.ParentObject);
				this.ParentObject.FireEvent("PowerSwitchDeactivated");
				SyncRenderEvent.Send(this.ParentObject);
				this.SyncActivatedAbilityNameAndIcon(null);
			}
			else if (E.ID.StartsWith(this.GetActivatedAbilityCommandNamePrefix()) && E.ID == this.GetActivatedAbilityCommandName(null))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				if (this.Active)
				{
					this.TryPowerSwitchOff(gameObjectParameter, E);
				}
				else
				{
					this.TryPowerSwitchOn(gameObjectParameter, E);
				}
				this.SyncActivatedAbilityNameAndIcon(null);
			}
			else if (E.ID == "LiquidFueledPowerPlantFueled")
			{
				this.SetUpActivatedAbility(null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B619 RID: 46617 RVA: 0x0042710C File Offset: 0x0042530C
		public override void SyncRender()
		{
			base.SyncRender();
			if (this.Active)
			{
				if (!this.ActiveColorString.IsNullOrEmpty())
				{
					this.ParentObject.Render.ColorString = this.ActiveColorString;
				}
				if (!this.ActiveDetailColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.DetailColor = this.ActiveDetailColor;
					return;
				}
			}
			else
			{
				if (!this.InactiveColorString.IsNullOrEmpty())
				{
					this.ParentObject.Render.ColorString = this.InactiveColorString;
				}
				if (!this.InactiveDetailColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.DetailColor = this.InactiveDetailColor;
				}
			}
		}

		// Token: 0x0600B61A RID: 46618 RVA: 0x004271B4 File Offset: 0x004253B4
		public bool AccessCheck(GameObject Actor, bool Silent = false, IEvent FromEvent = null)
		{
			if (this.KeyObject.IsNullOrEmpty())
			{
				return true;
			}
			if (Actor == null)
			{
				return false;
			}
			if (this.GrantLeaderAccess)
			{
				if (Actor.IsPlayer())
				{
					if (this.ParentObject.IsPlayerControlled())
					{
						return true;
					}
				}
				else if (Actor == this.ParentObject.PartyLeader)
				{
					return true;
				}
			}
			if (this.GrantAlliesAccess && this.ParentObject.IsAlliedTowards(Actor))
			{
				return true;
			}
			List<string> list = this.KeyObject.CachedCommaExpansion();
			GameObject gameObject = Actor.FindContainedObjectByAnyBlueprint(list);
			if (gameObject != null)
			{
				if (!Silent && !this.KeyObjectAccessMessage.IsNullOrEmpty())
				{
					IComponent<GameObject>.EmitMessage(Actor, GameText.VariableReplace(this.KeyObjectAccessMessage, this.ParentObject, gameObject, false), ' ', true, false, false, null, null);
				}
				return true;
			}
			if (list.Contains("*Psychometry") && base.UsePsychometry(Actor, this.ParentObject))
			{
				if (!Silent && !this.PsychometryAccessMessage.IsNullOrEmpty())
				{
					IComponent<GameObject>.EmitMessage(Actor, GameText.VariableReplace(this.PsychometryAccessMessage, this.ParentObject, null, false), ' ', true, false, false, null, null);
				}
				return true;
			}
			if (Actor.IsPlayer() && Options.SifrahHacking && this.IsHackable() && this.ParentObject.GetIntProperty("SifrahHack", 0) >= 0)
			{
				int num = XRL.World.Capabilities.SecurityClearance.GetSecurityClearanceByKeySpecification(this.KeyObject);
				if (!this.KeyObject.CachedCommaExpansion().Contains("*Psychometry"))
				{
					num += 2;
				}
				HackingSifrah hackingSifrah = new HackingSifrah(this.ParentObject, num, num, Actor.Stat("Intelligence", 0));
				hackingSifrah.HandlerID = this.ParentObject.ID;
				hackingSifrah.HandlerPartName = base.GetType().Name;
				hackingSifrah.Play(this.ParentObject);
				if (hackingSifrah.InterfaceExitRequested && FromEvent != null)
				{
					FromEvent.RequestInterfaceExit();
				}
				if (this.ParentObject.GetIntProperty("SifrahHack", 0) > 0)
				{
					this.ParentObject.ModIntProperty("SifrahHack", -1, true);
					return true;
				}
			}
			if (!Silent && !this.AccessFailureMessage.IsNullOrEmpty())
			{
				IComponent<GameObject>.EmitMessage(Actor, GameText.VariableReplace(this.AccessFailureMessage, this.ParentObject, null, false), ' ', true, false, false, null, null);
			}
			return false;
		}

		// Token: 0x0600B61B RID: 46619 RVA: 0x004273C2 File Offset: 0x004255C2
		public bool IsHackable()
		{
			return !this.KeyObject.IsNullOrEmpty() && XRL.World.Capabilities.SecurityClearance.GetSecurityClearanceByKeySpecification(this.KeyObject) > 0;
		}

		// Token: 0x0600B61C RID: 46620 RVA: 0x004273E4 File Offset: 0x004255E4
		public void HackingResultSuccess(GameObject Actor, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahHack", 1, false);
				if (Actor.IsPlayer())
				{
					Popup.Show("You hack " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x0600B61D RID: 46621 RVA: 0x0042745C File Offset: 0x0042565C
		public void HackingResultExceptionalSuccess(GameObject Actor, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahHack", 1, false);
				if (!this.KeyObject.IsNullOrEmpty())
				{
					List<string> list = this.KeyObject.CachedCommaExpansion();
					int num = 0;
					while (++num < 10)
					{
						try
						{
							if (70.in100())
							{
								string randomBits = BitType.GetRandomBits("2d4".Roll(), obj.GetTechTier());
								if (!randomBits.IsNullOrEmpty())
								{
									Actor.RequirePart<BitLocker>(false).AddBits(randomBits);
									if (Actor.IsPlayer())
									{
										Popup.Show(string.Concat(new string[]
										{
											"You hack ",
											this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" and find tinkering bits <{{|",
											BitType.GetDisplayString(randomBits),
											"}}> in ",
											this.ParentObject.them,
											"!"
										}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
									}
									break;
								}
							}
							else
							{
								GameObject gameObject = GameObject.Create(list.GetRandomElement<string>(null));
								if (gameObject != null)
								{
									if (Actor.IsPlayer())
									{
										Popup.Show(string.Concat(new string[]
										{
											"You hack ",
											this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" and find ",
											gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											" stuck in ",
											this.ParentObject.them,
											"!"
										}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
									}
									Actor.ReceiveObject(gameObject, false, null);
									break;
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x0600B61E RID: 46622 RVA: 0x0042766C File Offset: 0x0042586C
		public void HackingResultPartialSuccess(GameObject Actor, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject && Actor.IsPlayer())
			{
				Popup.Show("You feel like you're making progress on hacking " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600B61F RID: 46623 RVA: 0x004276D0 File Offset: 0x004258D0
		public void HackingResultFailure(GameObject Actor, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahHack", -1, false);
				if (Actor.IsPlayer())
				{
					Popup.Show("You cannot seem to work out how to hack " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x0600B620 RID: 46624 RVA: 0x00427748 File Offset: 0x00425948
		public void HackingResultCriticalFailure(GameObject Actor, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahHack", -1, false);
				if (Actor.HasPart<Dystechnia>())
				{
					Dystechnia.CauseExplosion(this.ParentObject, Actor, null);
					game.RequestInterfaceExit();
					return;
				}
				if (Actor.IsPlayer())
				{
					Popup.Show("Your attempt to hack " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " has gone very wrong.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				List<Cell> localAdjacentCells = this.ParentObject.CurrentCell.GetLocalAdjacentCells();
				Cell currentCell = Actor.CurrentCell;
				this.ParentObject.Discharge((currentCell != null && localAdjacentCells.Contains(currentCell)) ? currentCell : localAdjacentCells.GetRandomElement<Cell>(null), "3d8".Roll(), 0, "2d4", null, Actor, this.ParentObject, null, null, null, null, null, 0, false, false, false);
			}
		}

		// Token: 0x0600B621 RID: 46625 RVA: 0x00427838 File Offset: 0x00425A38
		public void CollectStats(Templates.StatCollector stats)
		{
			Description part = this.ParentObject.GetPart<Description>();
			if (part != null)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				part.GetLongDescription(stringBuilder);
				stats.Set("ItemDescription", stringBuilder.ToString(), false, 0);
			}
		}

		// Token: 0x0600B622 RID: 46626 RVA: 0x00427878 File Offset: 0x00425A78
		public void SetUpActivatedAbility(GameObject Actor = null)
		{
			if (Actor == null)
			{
				Actor = this.ParentObject.Equipped;
			}
			if (Actor != null)
			{
				if (this.Flippable && (this.FlippableWithoutUnderstanding || !Actor.IsPlayer() || this.ParentObject.Understood()) && base.IsReady(false, false, false, true, false, false, true, false, false, false, 1, null, false, 0L, null))
				{
					if (this.ActivatedAbilityID == Guid.Empty)
					{
						string nextAvailableCommandString = Actor.ActivatedAbilities.GetNextAvailableCommandString(this.GetActivatedAbilityCommandNamePrefix());
						this.ActivatedAbilityID = Actor.AddActivatedAbility(this.GetActivatedAbilityName(Actor), nextAvailableCommandString, this.ActivatedAbilityClass ?? ((Actor == this.ParentObject) ? "Maneuvers" : "Items"), null, this.ActivatedAbilityIcon, null, false, false, false, false, false, false, false, false, true, true, false, false, -1, "CommandPowerSwitch", null, null, null, null);
						Actor.DescribeActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats));
					}
					this.SyncActivatedAbilityNameAndIcon(Actor);
					return;
				}
				Actor.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			}
		}

		// Token: 0x0600B623 RID: 46627 RVA: 0x00427994 File Offset: 0x00425B94
		public string GetActivatedAbilityName(GameObject Actor = null)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.Active ? "Deactivate" : "Activate").Append(' ').Append(this.ActivatedAbilityItemName ?? Grammar.MakeTitleCase(this.ParentObject.BaseDisplayNameStripped));
			return stringBuilder.ToString();
		}

		// Token: 0x0600B624 RID: 46628 RVA: 0x004279ED File Offset: 0x00425BED
		[Obsolete("Just proxies to SyncActivatedAbilityNameAndIcon - will remove q3 2024")]
		public void SyncActivatedAbilityName(GameObject Actor = null)
		{
			this.SyncActivatedAbilityNameAndIcon(Actor);
		}

		// Token: 0x0600B625 RID: 46629 RVA: 0x004279F8 File Offset: 0x00425BF8
		public void SyncActivatedAbilityNameAndIcon(GameObject Actor = null)
		{
			if (this.ActivatedAbilityID == Guid.Empty)
			{
				return;
			}
			if (Actor == null)
			{
				Actor = this.ParentObject.Equipped;
			}
			Actor.SetActivatedAbilityDisplayName(this.ActivatedAbilityID, this.GetActivatedAbilityName(Actor));
			RenderEvent renderEvent = this.ParentObject.RenderForUI(null, true);
			ActivatedAbilityEntry activatedAbility = Actor.GetActivatedAbility(this.ActivatedAbilityID);
			if (activatedAbility != null)
			{
				activatedAbility.UITileDefault = new Renderable(renderEvent);
				activatedAbility.UITileCoolingDown = null;
				activatedAbility.UITileDisabled = null;
				activatedAbility.UITileToggleOn = null;
			}
		}

		// Token: 0x0600B626 RID: 46630 RVA: 0x00427A7B File Offset: 0x00425C7B
		public string GetActivatedAbilityCommandNamePrefix()
		{
			return "TogglePowerSwitch";
		}

		// Token: 0x0600B627 RID: 46631 RVA: 0x00427A82 File Offset: 0x00425C82
		public string GetActivatedAbilityCommandName(GameObject From = null)
		{
			GameObject gameObject = From ?? this.ParentObject.Equipped;
			if (gameObject == null)
			{
				return null;
			}
			ActivatedAbilityEntry activatedAbility = gameObject.GetActivatedAbility(this.ActivatedAbilityID);
			if (activatedAbility == null)
			{
				return null;
			}
			return activatedAbility.Command;
		}

		// Token: 0x0600B628 RID: 46632 RVA: 0x00427AB0 File Offset: 0x00425CB0
		public void TryPowerSwitchOn(GameObject Actor, IEvent FromEvent = null)
		{
			if (Actor.CheckFrozen(false, true, false, null))
			{
				this.ParentObject.PlayWorldOrUISound("Sounds/Interact/sfx_interact_powerSwitch_on", null);
				IComponent<GameObject>.XDidYToZ(Actor, this.ActivateVerb, this.ActivatePreposition, this.ParentObject, this.ActivateExtra, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
				if (this.KeylessActivation || this.AccessCheck(Actor, false, FromEvent))
				{
					string text = GameText.VariableReplace(this.ActivateSuccessMessage, this.ParentObject, null, false);
					string text2 = GameText.VariableReplace(this.ActivateFailureMessage, this.ParentObject, null, false);
					if (this.ParentObject.FireEvent(Event.New("PowerSwitchActivate", "Actor", Actor)))
					{
						if (!text.IsNullOrEmpty())
						{
							IComponent<GameObject>.EmitMessage(Actor, text, ' ', true, false, false, null, null);
						}
					}
					else if (!text2.IsNullOrEmpty())
					{
						IComponent<GameObject>.EmitMessage(Actor, text2, ' ', true, false, false, null, null);
					}
				}
				if (this.EnergyCost > 0)
				{
					Actor.UseEnergy(1000, "Item Activate", null, null, false);
					this.SyncActivatedAbilityNameAndIcon(null);
					if (FromEvent != null)
					{
						FromEvent.RequestInterfaceExit();
					}
				}
			}
		}

		// Token: 0x0600B629 RID: 46633 RVA: 0x00427BD4 File Offset: 0x00425DD4
		public void TryPowerSwitchOff(GameObject Actor, IEvent FromEvent = null)
		{
			if (Actor.CheckFrozen(false, true, false, null))
			{
				this.ParentObject.PlayWorldOrUISound("Sounds/Interact/sfx_interact_powerSwitch_off", null);
				IComponent<GameObject>.XDidYToZ(Actor, this.DeactivateVerb, this.DeactivatePreposition, this.ParentObject, this.DeactivateExtra, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
				if (this.KeylessDeactivation || this.AccessCheck(Actor, false, FromEvent))
				{
					string text = GameText.VariableReplace(this.DeactivateSuccessMessage, this.ParentObject, null, false);
					string text2 = GameText.VariableReplace(this.DeactivateFailureMessage, this.ParentObject, null, false);
					if (this.ParentObject.FireEvent(Event.New("PowerSwitchDeactivate", "Actor", Actor)))
					{
						if (!text.IsNullOrEmpty())
						{
							IComponent<GameObject>.EmitMessage(Actor, text, ' ', true, false, false, null, null);
						}
					}
					else if (!text2.IsNullOrEmpty())
					{
						IComponent<GameObject>.EmitMessage(Actor, text2, ' ', true, false, false, null, null);
					}
				}
				if (this.EnergyCost > 0)
				{
					Actor.UseEnergy(1000, "Item Deactivate", null, null, false);
					this.SyncActivatedAbilityNameAndIcon(null);
					if (FromEvent != null)
					{
						FromEvent.RequestInterfaceExit();
					}
				}
			}
		}

		// Token: 0x04004961 RID: 18785
		public bool Active = true;

		// Token: 0x04004962 RID: 18786
		public bool CanActivate = true;

		// Token: 0x04004963 RID: 18787
		public bool CanDeactivate = true;

		// Token: 0x04004964 RID: 18788
		public bool Flippable = true;

		// Token: 0x04004965 RID: 18789
		public bool FlippableKinetically = true;

		// Token: 0x04004966 RID: 18790
		public bool FlippableWithoutUnderstanding;

		// Token: 0x04004967 RID: 18791
		public int ChanceActive = -1;

		// Token: 0x04004968 RID: 18792
		public int ActivateActionPriority = 8;

		// Token: 0x04004969 RID: 18793
		public int DeactivateActionPriority = 4;

		// Token: 0x0400496A RID: 18794
		public int EnergyCost = 1000;

		// Token: 0x0400496B RID: 18795
		public string KeyObject;

		// Token: 0x0400496C RID: 18796
		public bool GrantLeaderAccess = true;

		// Token: 0x0400496D RID: 18797
		public bool GrantAlliesAccess;

		// Token: 0x0400496E RID: 18798
		public bool KeylessActivation;

		// Token: 0x0400496F RID: 18799
		public bool KeylessDeactivation;

		// Token: 0x04004970 RID: 18800
		public string FrequencyCode;

		// Token: 0x04004971 RID: 18801
		public string ActivateVerb = "press";

		// Token: 0x04004972 RID: 18802
		public string ActivatePreposition = "the power button on";

		// Token: 0x04004973 RID: 18803
		public string ActivateExtra;

		// Token: 0x04004974 RID: 18804
		public string ActivateSuccessMessage = "=subject.The==subject.name= =verb:start= up with a hum.";

		// Token: 0x04004975 RID: 18805
		public string ActivateFailureMessage = "Nothing happens.";

		// Token: 0x04004976 RID: 18806
		public string DeactivateVerb = "press";

		// Token: 0x04004977 RID: 18807
		public string DeactivatePreposition = "the power button on";

		// Token: 0x04004978 RID: 18808
		public string DeactivateExtra;

		// Token: 0x04004979 RID: 18809
		public string DeactivateSuccessMessage = "=subject.The==subject.name= =verb:shut= down with a whir.";

		// Token: 0x0400497A RID: 18810
		public string DeactivateFailureMessage = "Nothing happens.";

		// Token: 0x0400497B RID: 18811
		public string PsychometryAccessMessage = "{{g|You touch =subject.the==subject.name= and recall =pronouns.possessive= passcode. =pronouns.Subjective= =verb:beep:afterpronoun= warmly.}}";

		// Token: 0x0400497C RID: 18812
		public string KeyObjectAccessMessage = "=subject.The==subject.name= =verb:recognize= your =object.name=.";

		// Token: 0x0400497D RID: 18813
		public string AccessFailureMessage = "{{r|A loud buzz is emitted. The unauthorized glyph flashes on the display.}}";

		// Token: 0x0400497E RID: 18814
		public string ActiveAdjective;

		// Token: 0x0400497F RID: 18815
		public string InactiveAdjective = "{{K|deactivated}}";

		// Token: 0x04004980 RID: 18816
		public string ActiveDescription;

		// Token: 0x04004981 RID: 18817
		public string InactiveDescription = "{{C|=pronouns.Subjective= =verb:are:afterpronoun= powered off.}}";

		// Token: 0x04004982 RID: 18818
		public string ActiveColorString;

		// Token: 0x04004983 RID: 18819
		public string ActiveDetailColor;

		// Token: 0x04004984 RID: 18820
		public string InactiveColorString;

		// Token: 0x04004985 RID: 18821
		public string InactiveDetailColor;

		// Token: 0x04004986 RID: 18822
		public string ActivatedAbilityItemName;

		// Token: 0x04004987 RID: 18823
		public string ActivatedAbilityClass;

		// Token: 0x04004988 RID: 18824
		public string ActivatedAbilityIcon = "û";

		// Token: 0x04004989 RID: 18825
		public Guid ActivatedAbilityID;
	}
}
