using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F38 RID: 3896
	[Serializable]
	public class CyberneticsNocturnalApex : IPart
	{
		// Token: 0x06009B5A RID: 39770 RVA: 0x00385CBF File Offset: 0x00383EBF
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B5B RID: 39771 RVA: 0x00385CC4 File Offset: 0x00383EC4
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			GameObject parentObject = this.ParentObject;
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(activatedAbilityID, (parentObject != null) ? parentObject.Implantee : null);
			int num = stats.CollectComputePowerAdjustUp(activatedAbilityEntry, "Agility bonus", 6, 1f);
			stats.Set("AgilityBonus", num, num != 6, num - 6);
			int num2 = stats.CollectComputePowerAdjustUp(activatedAbilityEntry, "Move speed bonus", 10, 1f);
			stats.Set("MovespeedBonus", num2, num2 != 10, num2 - 10);
			int num3 = stats.CollectComputePowerAdjustUp(activatedAbilityEntry, "Duration", 100, 1f);
			stats.Set("Duration", num3, num3 != 100, num3 - 100);
			stats.Set("Cooldown", "once per night", false, 0);
		}

		// Token: 0x06009B5C RID: 39772 RVA: 0x00385D84 File Offset: 0x00383F84
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009B5D RID: 39773 RVA: 0x00385DD5 File Offset: 0x00383FD5
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B5E RID: 39774 RVA: 0x00385E0C File Offset: 0x0038400C
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.Used && base.IsNight() && !this.commandId.IsNullOrEmpty() && GameObject.Validate(E.Target) && GameObject.Validate(E.Actor) && E.Actor == this.ParentObject.Implantee && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && (E.Actor.HasTagOrProperty("AIAbilityIgnoreDamage") || !E.Target.isDamaged(0.5, true)) && Stat.Random(1, 10) >= E.Distance)
			{
				if (!E.Actor.HasTagOrProperty("AIAbilityIgnoreCon"))
				{
					int num = Stat.Random(0, 10);
					int? num2 = E.Target.Con(E.Actor, true);
					if (!((num <= num2.GetValueOrDefault()) & (num2 != null)))
					{
						goto IL_0100;
					}
				}
				E.Add(this.commandId, 1, null, false, false, null, null);
			}
			IL_0100:
			return base.HandleEvent(E);
		}

		// Token: 0x06009B5F RID: 39775 RVA: 0x00385F20 File Offset: 0x00384120
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddDynamicCommand(out this.commandId, "CommandToggleNocturnalApex", "Prowl", "Cybernetics", "You gain +6 agility and +10 movespeed for 100 turns. Can only be activated at night.", "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null);
			E.Implantee.RegisterPartEvent(this, this.commandId);
			E.Implantee.RegisterPartEvent(this, "Regenerating2");
			E.Implantee.DisableActivatedAbility(this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B60 RID: 39776 RVA: 0x00385FAB File Offset: 0x003841AB
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, this.commandId);
			E.Implantee.UnregisterPartEvent(this, "Regenerating2");
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B61 RID: 39777 RVA: 0x00385FEC File Offset: 0x003841EC
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.commandId && E.Actor == this.ParentObject.Implantee)
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (((implantee != null) ? implantee.CurrentCell : null) != null)
				{
					int num = 100;
					int num2 = -10;
					int num3 = 6;
					int @for = GetAvailableComputePowerEvent.GetFor(implantee);
					if (@for != 0)
					{
						num = num * (100 + @for) / 100;
						num2 = num2 * (100 + @for) / 100;
						num3 = num3 * (100 + @for) / 100;
					}
					if (implantee != null)
					{
						implantee.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					implantee.ApplyEffect(new NocturnalApexed(num, num2, num3, this.ParentObject), null);
					this.Used = true;
					implantee.DisableActivatedAbility(this.ActivatedAbilityID);
					implantee.CooldownActivatedAbility(this.ActivatedAbilityID, 1200, null, false);
					implantee.SetActivatedAbilityDisabledMessage(this.ActivatedAbilityID, "You've already prowled tonight.");
					this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B62 RID: 39778 RVA: 0x00386110 File Offset: 0x00384310
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			GameObject implantee = this.ParentObject.Implantee;
			if (E.Object == implantee)
			{
				if (!this.Used && base.IsNight())
				{
					if (implantee != null)
					{
						implantee.EnableActivatedAbility(this.ActivatedAbilityID);
					}
				}
				else
				{
					if (this.Used && base.IsDay())
					{
						this.Used = false;
					}
					if (implantee != null)
					{
						implantee.DisableActivatedAbility(this.ActivatedAbilityID);
					}
					if (!this.Used && implantee != null)
					{
						implantee.SetActivatedAbilityDisabledMessage(this.ActivatedAbilityID, "You can only prowl at night.");
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B63 RID: 39779 RVA: 0x0038619F File Offset: 0x0038439F
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009B64 RID: 39780 RVA: 0x003861B9 File Offset: 0x003843B9
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06009B65 RID: 39781 RVA: 0x003861BC File Offset: 0x003843BC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Regenerating2" && base.IsDay())
			{
				float num = GetAvailableComputePowerEvent.AdjustUp(this.ParentObject.Implantee, 1.1f);
				E.SetParameter("Amount", (int)((float)E.GetIntParameter("Amount", 0) * num));
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004076 RID: 16502
		public bool Used;

		// Token: 0x04004077 RID: 16503
		public string commandId = "";

		// Token: 0x04004078 RID: 16504
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
