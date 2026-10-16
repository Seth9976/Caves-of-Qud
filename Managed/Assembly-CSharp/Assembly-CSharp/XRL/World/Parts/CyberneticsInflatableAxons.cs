using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F31 RID: 3889
	[Serializable]
	public class CyberneticsInflatableAxons : IPart
	{
		// Token: 0x06009B04 RID: 39684 RVA: 0x00383AF7 File Offset: 0x00381CF7
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B05 RID: 39685 RVA: 0x00383AFC File Offset: 0x00381CFC
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			GameObject parentObject = this.ParentObject;
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(activatedAbilityID, (parentObject != null) ? parentObject.Implantee : null);
			int num = stats.CollectComputePowerAdjustUp(activatedAbilityEntry, "Quickness bonus", this.Bonus, 1f);
			stats.Set("QuicknessBonus", num, num != this.Bonus, num - this.Bonus);
			int num2 = stats.CollectComputePowerAdjustUp(activatedAbilityEntry, "Duration", this.Duration, 1f);
			stats.Set("QuicknessDuration", num2, num2 != this.Duration, num2 - this.Duration);
			stats.CollectCooldownTurns(activatedAbilityEntry, this.GetCooldown(), 0);
		}

		// Token: 0x06009B06 RID: 39686 RVA: 0x00383BA6 File Offset: 0x00381DA6
		public int GetCooldown()
		{
			return 100;
		}

		// Token: 0x06009B07 RID: 39687 RVA: 0x00383BAA File Offset: 0x00381DAA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009B08 RID: 39688 RVA: 0x00383BE8 File Offset: 0x00381DE8
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B09 RID: 39689 RVA: 0x00383C1C File Offset: 0x00381E1C
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.commandId.IsNullOrEmpty() && GameObject.Validate(E.Target) && GameObject.Validate(E.Actor) && E.Distance <= 1 && E.Actor == this.ParentObject.Implantee && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && (E.Actor.HasTagOrProperty("AIAbilityIgnoreDamage") || !E.Target.isDamaged(0.2, true)))
			{
				if (!E.Actor.HasTagOrProperty("AIAbilityIgnoreCon"))
				{
					int num = Stat.Random(-10, 10);
					int? num2 = E.Target.Con(E.Actor, true);
					if (!((num <= num2.GetValueOrDefault()) & (num2 != null)))
					{
						goto IL_00E4;
					}
				}
				E.Add(this.commandId, 1, null, false, false, null, null);
			}
			IL_00E4:
			return base.HandleEvent(E);
		}

		// Token: 0x06009B0A RID: 39690 RVA: 0x00383D14 File Offset: 0x00381F14
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009B0B RID: 39691 RVA: 0x00383D30 File Offset: 0x00381F30
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddDynamicCommand(out this.commandId, "CommandInflateAxons", "Inflate Axons", "Cybernetics", string.Concat(new string[]
			{
				"You gain +",
				this.Bonus.ToString(),
				" quickness for ",
				this.Duration.ToString(),
				" rounds, then you become sluggish for 10 rounds (-10 quickness)."
			}), "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B0C RID: 39692 RVA: 0x00383DC0 File Offset: 0x00381FC0
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B0D RID: 39693 RVA: 0x00383DDC File Offset: 0x00381FDC
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.commandId && E.Actor != null && E.Actor == this.ParentObject.Implantee && E.Actor.CurrentCell != null)
			{
				int num = this.Duration;
				int num2 = this.Bonus;
				int @for = GetAvailableComputePowerEvent.GetFor(E.Actor);
				if (@for != 0)
				{
					num = num * (100 + @for) / 100;
					num2 = num2 * (100 + @for) / 100;
				}
				E.Actor.ApplyEffect(new AxonsInflated(num, num2, this.ParentObject), null);
				E.Actor.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, false);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B0E RID: 39694 RVA: 0x00383EAC File Offset: 0x003820AC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004061 RID: 16481
		public string commandId = "";

		// Token: 0x04004062 RID: 16482
		public int Bonus = 40;

		// Token: 0x04004063 RID: 16483
		public int Duration = 10;

		// Token: 0x04004064 RID: 16484
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
