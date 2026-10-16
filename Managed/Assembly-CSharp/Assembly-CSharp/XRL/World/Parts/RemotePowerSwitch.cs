using System;

namespace XRL.World.Parts
{
	// Token: 0x020011D9 RID: 4569
	[Serializable]
	public class RemotePowerSwitch : IPoweredPart
	{
		// Token: 0x0600B62A RID: 46634 RVA: 0x00427CF5 File Offset: 0x00425EF5
		public RemotePowerSwitch()
		{
			this.ChargeUse = 0;
			this.IsPowerSwitchSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B62B RID: 46635 RVA: 0x00427D12 File Offset: 0x00425F12
		public override bool SameAs(IPart p)
		{
			return !((p as RemotePowerSwitch).FrequencyCode != this.FrequencyCode) && base.SameAs(p);
		}

		// Token: 0x0600B62C RID: 46636 RVA: 0x00427D35 File Offset: 0x00425F35
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600B62D RID: 46637 RVA: 0x00427D54 File Offset: 0x00425F54
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			PowerSwitch powerSwitch = this.Connected();
			if (powerSwitch != null && this.ParentObject.Understood() && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (powerSwitch.Active)
				{
					E.AddAction("Deactivate", "deactivate", "RemotePowerSwitchOff", null, 'a', false, powerSwitch.DeactivateActionPriority, 0, false, false, powerSwitch.FlippableKinetically, false, true, null, false);
				}
				else
				{
					E.AddAction("Activate", "activate", "RemotePowerSwitchOn", null, 'a', false, powerSwitch.ActivateActionPriority, 0, false, false, powerSwitch.FlippableKinetically, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B62E RID: 46638 RVA: 0x00427E14 File Offset: 0x00426014
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "RemotePowerSwitchOn")
			{
				PowerSwitch powerSwitch = this.Connected();
				if (powerSwitch != null && E.Actor.CheckFrozen(false, true, false, null))
				{
					IComponent<GameObject>.XDidYToZ(E.Actor, powerSwitch.ActivateVerb, powerSwitch.ActivatePreposition, this.ParentObject, powerSwitch.ActivateExtra, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
					if (powerSwitch.KeylessActivation || powerSwitch.AccessCheck(E.Actor, false, null))
					{
						string text = GameText.VariableReplace(powerSwitch.ActivateSuccessMessage, this.ParentObject, null, E.Actor.IsPlayer());
						string text2 = GameText.VariableReplace(powerSwitch.ActivateFailureMessage, this.ParentObject, null, E.Actor.IsPlayer());
						if (this.ParentObject.FireEvent(Event.New("RemotePowerSwitchActivate", "Actor", E.Actor)))
						{
							if (!string.IsNullOrEmpty(text))
							{
								IComponent<GameObject>.EmitMessage(E.Actor, text, ' ', true, false, false, null, null);
							}
						}
						else if (!string.IsNullOrEmpty(text2))
						{
							IComponent<GameObject>.EmitMessage(E.Actor, text2, ' ', true, false, false, null, null);
						}
					}
					E.Actor.UseEnergy(1000, "Item Activate", null, null, false);
					E.RequestInterfaceExit();
				}
			}
			else if (E.Command == "RemotePowerSwitchOff")
			{
				PowerSwitch powerSwitch2 = this.Connected();
				if (powerSwitch2 != null && E.Actor.CheckFrozen(false, true, false, null))
				{
					IComponent<GameObject>.XDidYToZ(E.Actor, powerSwitch2.DeactivateVerb, powerSwitch2.DeactivatePreposition, this.ParentObject, powerSwitch2.DeactivateExtra, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
					if (powerSwitch2.KeylessDeactivation || powerSwitch2.AccessCheck(E.Actor, false, null))
					{
						string text3 = GameText.VariableReplace(powerSwitch2.DeactivateSuccessMessage, this.ParentObject, null, E.Actor.IsPlayer());
						string text4 = GameText.VariableReplace(powerSwitch2.DeactivateFailureMessage, this.ParentObject, null, E.Actor.IsPlayer());
						if (this.ParentObject.FireEvent(Event.New("RemotePowerSwitchDeactivate", "Actor", E.Actor)))
						{
							if (!string.IsNullOrEmpty(text3))
							{
								IComponent<GameObject>.EmitMessage(E.Actor, text3, ' ', true, false, false, null, null);
							}
						}
						else if (!string.IsNullOrEmpty(text4))
						{
							IComponent<GameObject>.EmitMessage(E.Actor, text4, ' ', true, false, false, null, null);
						}
					}
					E.Actor.UseEnergy(1000, "Item Deactivate", null, null, false);
					E.RequestInterfaceExit();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B62F RID: 46639 RVA: 0x004280C4 File Offset: 0x004262C4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUseEarly");
			Registrar.Register("RemotePowerSwitchActivate");
			Registrar.Register("RemotePowerSwitchDeactivate");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B630 RID: 46640 RVA: 0x004280FC File Offset: 0x004262FC
		public PowerSwitch Connected()
		{
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null)
			{
				return null;
			}
			Zone parentZone = currentCell.ParentZone;
			if (parentZone == null)
			{
				return null;
			}
			for (int i = 0; i < parentZone.Height; i++)
			{
				for (int j = 0; j < parentZone.Width; j++)
				{
					Cell cell = parentZone.GetCell(j, i);
					if (cell != null)
					{
						int k = 0;
						int count = cell.Objects.Count;
						while (k < count)
						{
							PowerSwitch powerSwitch;
							if (cell.Objects[k].TryGetPart<PowerSwitch>(out powerSwitch) && powerSwitch.FrequencyCode == this.FrequencyCode)
							{
								return powerSwitch;
							}
							k++;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600B631 RID: 46641 RVA: 0x004281A4 File Offset: 0x004263A4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				if (this.Connected() != null && E.GetGameObjectParameter("User").IsPlayer() && this.ParentObject.Understood())
				{
					return false;
				}
			}
			else if (E.ID == "CommandSmartUseEarly")
			{
				if (this.Connected() != null && E.GetGameObjectParameter("User").IsPlayer() && this.ParentObject.Understood())
				{
					this.ParentObject.Twiddle(false);
					return false;
				}
			}
			else if (E.ID == "RemotePowerSwitchActivate")
			{
				PowerSwitch powerSwitch = this.Connected();
				if (powerSwitch == null)
				{
					return false;
				}
				if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
				Event @event = Event.New("PowerSwitchActivate", 0, 0, 0);
				@event.SetParameter("Actor", E.GetGameObjectParameter("Actor"));
				@event.SetFlag("Forced", E.HasFlag("Forced"));
				if (!powerSwitch.FireEvent(@event))
				{
					return false;
				}
			}
			else if (E.ID == "RemotePowerSwitchDeactivate")
			{
				PowerSwitch powerSwitch2 = this.Connected();
				if (powerSwitch2 == null)
				{
					return false;
				}
				if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
				Event event2 = Event.New("PowerSwitchDectivate", 0, 0, 0);
				event2.SetParameter("Actor", E.GetGameObjectParameter("Actor"));
				event2.SetFlag("Forced", E.HasFlag("Forced"));
				if (!powerSwitch2.FireEvent(event2))
				{
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400498A RID: 18826
		public string FrequencyCode;
	}
}
