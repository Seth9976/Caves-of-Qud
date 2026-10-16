using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is set to
	/// true, which it is not by default, temperature changes are increased
	/// in magnitude by a percentage equal to ((power load - 100) / 10), i.e.
	/// 30% for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x020010B0 RID: 4272
	[Serializable]
	public class TemperatureController : IPoweredPart
	{
		// Token: 0x0600AA06 RID: 43526 RVA: 0x003E2CCC File Offset: 0x003E0ECC
		public TemperatureController()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600AA07 RID: 43527 RVA: 0x003E2D20 File Offset: 0x003E0F20
		public override bool SameAs(IPart p)
		{
			TemperatureController temperatureController = p as TemperatureController;
			return temperatureController.TemperatureAmountPercentage == this.TemperatureAmountPercentage && temperatureController.TemperatureAmountFloor == this.TemperatureAmountFloor && temperatureController.TemperatureAmountChargeUsePercentage == this.TemperatureAmountChargeUsePercentage && temperatureController.TemperatureTarget == this.TemperatureTarget && temperatureController.ConfigureEnergy == this.ConfigureEnergy && !(temperatureController.BehaviorDescription != this.BehaviorDescription) && !(temperatureController.EquippedTargetReachedMessage != this.EquippedTargetReachedMessage) && temperatureController.InactiveOnWorldMap == this.InactiveOnWorldMap && temperatureController.TargetReached == this.TargetReached && temperatureController.IgnoreResistance == this.IgnoreResistance && !(temperatureController.ActivatedAbilityID != this.ActivatedAbilityID) && base.SameAs(p);
		}

		// Token: 0x0600AA08 RID: 43528 RVA: 0x003E2DFC File Offset: 0x003E0FFC
		public override bool GetActivePartLocallyDefinedFailure()
		{
			if (this.InactiveOnWorldMap)
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.OnWorldMap())
							{
								return true;
							}
						}
						return false;
					}
				}
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (activePartFirstSubject != null && activePartFirstSubject.OnWorldMap())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600AA09 RID: 43529 RVA: 0x003E2E7C File Offset: 0x003E107C
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "Inactive";
		}

		// Token: 0x0600AA0A RID: 43530 RVA: 0x003E2E84 File Offset: 0x003E1084
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("TemperatureChange", "roughly " + this.TemperatureAmountPercentage.ToString() + "%", false, 0);
			stats.Set("ChargeUse", "Temperature change x " + ((float)this.TemperatureAmountChargeUsePercentage / 100f).ToString(), false, 0);
		}

		// Token: 0x0600AA0B RID: 43531 RVA: 0x003E2EE4 File Offset: 0x003E10E4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeTemperatureChangeEvent>.ID || ID == BootSequenceDoneEvent.ID || ID == PooledEvent<CanTemperatureReturnToAmbientEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<ExamineSuccessEvent>.ID || ID == GetInventoryActionsEvent.ID || (ID == GetShortDescriptionEvent.ID && !string.IsNullOrEmpty(this.BehaviorDescription)) || ID == InventoryActionEvent.ID || (ID == SingletonEvent<RadiatesHeatAdjacentEvent>.ID && this.WorksOnAdjacentCellContents) || (ID == SingletonEvent<RadiatesHeatEvent>.ID && this.WorksOnCellContents) || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || (ID == ObjectCreatedEvent.ID && this.WorksOnSelf) || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600AA0C RID: 43532 RVA: 0x003E2F9B File Offset: 0x003E119B
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA0D RID: 43533 RVA: 0x003E2FC8 File Offset: 0x003E11C8
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Object == this.ParentObject && this.IsObjectActivePartSubject(this.ParentObject.Equipped))
			{
				this.SetUpActivatedAbility(this.ParentObject.Equipped);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA0E RID: 43534 RVA: 0x003E3003 File Offset: 0x003E1203
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "ConfigureTemperatureController")
			{
				this.ConfigureTemperatureController(E.Actor, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA0F RID: 43535 RVA: 0x003E302C File Offset: 0x003E122C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood())
			{
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (activePartFirstSubject != null && activePartFirstSubject.IsPlayer())
				{
					E.AddAction("ConfigureTemperatureController", "set target temperature", "ConfigureTemperatureController", null, 's', false, 1, 0, false, false, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA10 RID: 43536 RVA: 0x003E3082 File Offset: 0x003E1282
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ConfigureTemperatureController")
			{
				this.ConfigureTemperatureController(E.Actor, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA11 RID: 43537 RVA: 0x003E30AA File Offset: 0x003E12AA
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.WorksOnSelf)
			{
				this.SetUpActivatedAbility(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA12 RID: 43538 RVA: 0x003E30C8 File Offset: 0x003E12C8
		public override bool HandleEvent(CanTemperatureReturnToAmbientEvent E)
		{
			return (!this.IsObjectActivePartSubject(E.Object) || !base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, new int?(Math.Max(Math.Abs(E.Amount) * this.TemperatureAmountChargeUsePercentage / 100, 1)), false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA13 RID: 43539 RVA: 0x003E312C File Offset: 0x003E132C
		public override bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			return (!E.Radiant || E.IgnoreResistance || E.Object.Physics.Temperature != this.TemperatureTarget || Math.Abs(E.Amount) > this.TemperatureAmountFloor || !this.IsObjectActivePartSubject(E.Object) || !base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, new int?(Math.Max(Math.Abs(E.Amount) * this.TemperatureAmountChargeUsePercentage / 100, 1)), false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA14 RID: 43540 RVA: 0x003E31CD File Offset: 0x003E13CD
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.Understood() && this.IsObjectActivePartSubject(E.Actor))
			{
				this.SetUpActivatedAbility(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA15 RID: 43541 RVA: 0x003E31FD File Offset: 0x003E13FD
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA16 RID: 43542 RVA: 0x003E3218 File Offset: 0x003E1418
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.BehaviorDescription))
			{
				E.Postfix.AppendRules(this.BehaviorDescription, base.GetEventSensitiveAddStatusSummary(E));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA17 RID: 43543 RVA: 0x003E3248 File Offset: 0x003E1448
		public override bool HandleEvent(RadiatesHeatAdjacentEvent E)
		{
			return (!this.WorksOnAdjacentCellContents || this.TemperatureAmountFloor <= 0 || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA18 RID: 43544 RVA: 0x003E3298 File Offset: 0x003E1498
		public override bool HandleEvent(RadiatesHeatEvent E)
		{
			return (!this.WorksOnCellContents || this.TemperatureAmountFloor <= 0 || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA19 RID: 43545 RVA: 0x003E32E7 File Offset: 0x003E14E7
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			if (!this.ShouldResetMessaging())
			{
				this.TargetReached = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA1A RID: 43546 RVA: 0x003E32FF File Offset: 0x003E14FF
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600AA1B RID: 43547 RVA: 0x003E3304 File Offset: 0x003E1504
		public override void TurnTick(long TimeTick, int Amount)
		{
			int num = this.MyPowerLoadLevel();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GameObject gameObject = enumerator.Current;
							this.AdjustTemperature(gameObject, num);
						}
						return;
					}
				}
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (activePartFirstSubject != null)
				{
					this.AdjustTemperature(activePartFirstSubject, num);
				}
			}
		}

		// Token: 0x0600AA1C RID: 43548 RVA: 0x003E33A8 File Offset: 0x003E15A8
		public bool ShouldAdjustTemperature(GameObject obj)
		{
			return obj != null && obj.Physics != null && obj.Physics.SpecificHeat != 0f && obj.Physics.Temperature != this.TemperatureTarget;
		}

		// Token: 0x0600AA1D RID: 43549 RVA: 0x003E33E4 File Offset: 0x003E15E4
		public bool ShouldAdjustTemperature()
		{
			if (this.ActivePartHasMultipleSubjects())
			{
				foreach (GameObject gameObject in this.GetActivePartSubjects())
				{
					if (this.ShouldAdjustTemperature(gameObject))
					{
						return true;
					}
				}
				return false;
			}
			return this.ShouldAdjustTemperature(this.GetActivePartFirstSubject());
		}

		// Token: 0x0600AA1E RID: 43550 RVA: 0x003E3458 File Offset: 0x003E1658
		public bool ShouldResetMessaging(GameObject obj)
		{
			return obj != null && obj.Physics != null && obj.Physics.SpecificHeat != 0f && obj.Physics.Temperature != this.TemperatureTarget && Math.Abs(obj.Physics.Temperature - this.TemperatureTarget) >= TemperatureController.MIN_TARGET_REACHED_MESSAGE_RESET_DIFFERENCE;
		}

		// Token: 0x0600AA1F RID: 43551 RVA: 0x003E34C0 File Offset: 0x003E16C0
		public bool ShouldResetMessaging()
		{
			if (this.ActivePartHasMultipleSubjects())
			{
				foreach (GameObject gameObject in this.GetActivePartSubjects())
				{
					if (this.ShouldResetMessaging(gameObject))
					{
						return true;
					}
				}
				return false;
			}
			return this.ShouldResetMessaging(this.GetActivePartFirstSubject());
		}

		// Token: 0x0600AA20 RID: 43552 RVA: 0x003E3534 File Offset: 0x003E1734
		public bool AdjustTemperature(GameObject obj, int PowerLoad = -2147483648)
		{
			if (!this.ShouldAdjustTemperature(obj))
			{
				this.CheckMessaging(obj);
				return false;
			}
			if (this.ShouldResetMessaging(obj))
			{
				this.TargetReached = false;
			}
			int num = obj.Physics.Temperature - this.TemperatureTarget;
			int num2 = Math.Max(Math.Abs(num) * this.TemperatureAmountPercentage / 100, this.TemperatureAmountFloor);
			if (this.IsPowerLoadSensitive)
			{
				if (PowerLoad == -2147483648)
				{
					PowerLoad = this.MyPowerLoadLevel();
				}
				int num3 = this.MyPowerLoadBonus(PowerLoad, 100, 10);
				if (num3 != 0)
				{
					num2 = num2 * (100 + num3) / 100;
				}
			}
			if (Math.Abs(num) < num2)
			{
				num2 = Math.Abs(num);
			}
			num2 = (int)((float)num2 * obj.Physics.SpecificHeat);
			num2 = Math.Max(1, num2);
			int num4 = Math.Max(1, num2 * this.TemperatureAmountChargeUsePercentage / 100);
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, new int?(num4), false, 0L, new int?(PowerLoad)))
			{
				return false;
			}
			if (num > 0)
			{
				num2 = -num2;
			}
			int num5 = num2;
			GameObject gameObject;
			if ((gameObject = obj.Equipped) == null)
			{
				gameObject = obj.Implantee ?? obj;
			}
			obj.TemperatureChange(num5, gameObject, false, false, false, this.IgnoreResistance, 0, null, null);
			this.CheckMessaging(obj);
			return true;
		}

		// Token: 0x0600AA21 RID: 43553 RVA: 0x003E366C File Offset: 0x003E186C
		public void ConfigureTemperatureController(GameObject Actor, bool FreeAction = false)
		{
			SoundManager.PreloadClipSet("Sounds/Abilities/sfx_ability_dial_activated");
			if (Actor.IsPlayer())
			{
				int? num = Popup.AskNumber("Set target temperature.", "Sounds/UI/ui_notification", "", this.TemperatureTarget, TemperatureController.MIN_TEMPERATURE, TemperatureController.MAX_TEMPERATURE);
				int? num2;
				if (num != null)
				{
					num2 = num;
					int temperatureTarget = this.TemperatureTarget;
					if (!((num2.GetValueOrDefault() == temperatureTarget) & (num2 != null)))
					{
						this.TemperatureTarget = num.Value;
						if (this.ConfigureEnergy > 0 && !FreeAction)
						{
							Actor.UseEnergy(this.ConfigureEnergy, "Item Temperature Controller", null, null, false);
						}
					}
				}
				IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_thermostat_change", 1f, false, SoundRequest.SoundEffectType.None);
				string text = "You set a target temperature of ";
				num2 = num;
				IComponent<GameObject>.AddPlayerMessage(text + num2.ToString() + ".", null, true);
				return;
			}
			if (Actor != null)
			{
				int num3 = GetIdealTemperatureEvent.GetFor(Actor, 25);
				num3 = Math.Max(num3, TemperatureController.MIN_TEMPERATURE);
				num3 = Math.Min(num3, TemperatureController.MAX_TEMPERATURE);
				if (num3 != this.TemperatureTarget)
				{
					this.TemperatureTarget = num3;
					if (this.ConfigureEnergy > 0 && !FreeAction)
					{
						if (Actor != null)
						{
							Actor.PlayWorldSound("Sounds/Abilities/sfx_ability_dial_activated", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
						Actor.UseEnergy(this.ConfigureEnergy, "Item Temperature Controller", null, null, false);
					}
				}
			}
		}

		// Token: 0x0600AA22 RID: 43554 RVA: 0x003E37D4 File Offset: 0x003E19D4
		public void SetUpActivatedAbility(GameObject who)
		{
			if (who != null)
			{
				this.ActivatedAbilityID = who.AddActivatedAbility("Set Target Temperature", "ConfigureTemperatureController", (who == this.ParentObject) ? "Maneuvers" : "Items", null, "è", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
				if (!who.IsPlayer() && !who.IsPlayerControlled())
				{
					this.ConfigureTemperatureController(who, true);
				}
			}
		}

		// Token: 0x0600AA23 RID: 43555 RVA: 0x003E3840 File Offset: 0x003E1A40
		private void CheckMessaging(GameObject Object)
		{
			if (this.TargetReached)
			{
				return;
			}
			if (Object.Physics == null)
			{
				return;
			}
			if (Object.Physics.Temperature != this.TemperatureTarget)
			{
				return;
			}
			this.TargetReached = true;
			if (this.EquippedTargetReachedMessage.IsNullOrEmpty())
			{
				return;
			}
			if (!Object.IsPlayer())
			{
				return;
			}
			if (Object != this.ParentObject.Equipped)
			{
				return;
			}
			if (TemperatureController.LastTargetReachedMessage > XRLCore.CurrentTurn - (long)TemperatureController.MIN_TARGET_REACHED_MESSAGE_TURNS)
			{
				return;
			}
			string text = GameText.VariableReplace(this.EquippedTargetReachedMessage, Object, this.ParentObject, false);
			if (!text.IsNullOrEmpty())
			{
				IComponent<GameObject>.AddPlayerMessage(text, null, true);
				TemperatureController.LastTargetReachedMessage = XRLCore.CurrentTurn;
			}
		}

		// Token: 0x0400460F RID: 17935
		public static readonly int MIN_TEMPERATURE = -8000;

		// Token: 0x04004610 RID: 17936
		public static readonly int MAX_TEMPERATURE = 8000;

		// Token: 0x04004611 RID: 17937
		public static readonly int MIN_TARGET_REACHED_MESSAGE_TURNS = 2;

		// Token: 0x04004612 RID: 17938
		public static readonly int MIN_TARGET_REACHED_MESSAGE_RESET_DIFFERENCE = 6;

		// Token: 0x04004613 RID: 17939
		public int TemperatureAmountPercentage = 10;

		// Token: 0x04004614 RID: 17940
		public int TemperatureAmountFloor = 5;

		// Token: 0x04004615 RID: 17941
		public int TemperatureAmountChargeUsePercentage = 100;

		// Token: 0x04004616 RID: 17942
		public int TemperatureTarget = 100;

		// Token: 0x04004617 RID: 17943
		public int ConfigureEnergy = 100;

		// Token: 0x04004618 RID: 17944
		public string BehaviorDescription;

		// Token: 0x04004619 RID: 17945
		public string EquippedTargetReachedMessage = "=object.T= =object.verb:have= reached the target temperature.";

		// Token: 0x0400461A RID: 17946
		public bool InactiveOnWorldMap;

		// Token: 0x0400461B RID: 17947
		public bool TargetReached;

		// Token: 0x0400461C RID: 17948
		public bool IgnoreResistance = true;

		// Token: 0x0400461D RID: 17949
		public Guid ActivatedAbilityID;

		// Token: 0x0400461E RID: 17950
		public static long LastTargetReachedMessage;
	}
}
