using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000F33 RID: 3891
	[Serializable]
	public class CyberneticsMatterRecompositer : IPart
	{
		// Token: 0x06009B17 RID: 39703 RVA: 0x00383FEF File Offset: 0x003821EF
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B18 RID: 39704 RVA: 0x00383FF4 File Offset: 0x003821F4
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			GameObject parentObject = this.ParentObject;
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(activatedAbilityID, (parentObject != null) ? parentObject.Implantee : null);
			int num = stats.CollectComputePowerAdjustDown(activatedAbilityEntry, "Cooldown", this.GetBaseCooldown(), 1f);
			stats.CollectCooldownTurns(activatedAbilityEntry, num, num - this.GetBaseCooldown());
		}

		// Token: 0x06009B19 RID: 39705 RVA: 0x00384048 File Offset: 0x00382248
		public int GetBaseCooldown()
		{
			return 100;
		}

		// Token: 0x06009B1A RID: 39706 RVA: 0x0038404C File Offset: 0x0038224C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetMovementAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetRetreatAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetMovementCapabilitiesEvent.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009B1B RID: 39707 RVA: 0x003840B5 File Offset: 0x003822B5
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Matter Recompositer", this.commandId, 11500, base.MyActivatedAbility(this.ActivatedAbilityID, null), false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B1C RID: 39708 RVA: 0x003840E2 File Offset: 0x003822E2
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject.Implantee))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B1D RID: 39709 RVA: 0x0038410A File Offset: 0x0038230A
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B1E RID: 39710 RVA: 0x00384140 File Offset: 0x00382340
		public override bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			if (E.TargetCell != null && !this.commandId.IsNullOrEmpty() && E.Distance >= 40 && GameObject.Validate(E.Actor) && E.Actor == this.ParentObject.Implantee && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && 10.in100() && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, null, E.Actor, this.ParentObject, null, null, 0))
			{
				E.Add(this.commandId, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B1F RID: 39711 RVA: 0x003841EC File Offset: 0x003823EC
		public override bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			if (!this.commandId.IsNullOrEmpty() && (E.TargetCell == null || E.Distance >= 5) && GameObject.Validate(E.Actor) && E.Actor == this.ParentObject.Implantee && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, null, E.Actor, this.ParentObject, null, null, 0))
			{
				E.Add(this.commandId, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B20 RID: 39712 RVA: 0x00384288 File Offset: 0x00382488
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.commandId.IsNullOrEmpty() && E.Distance >= 40 && GameObject.Validate(E.Actor) && E.Actor == this.ParentObject.Implantee && E.Actor.IsActivatedAbilityAIUsable(this.ActivatedAbilityID) && 5.in100() && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, null, E.Actor, this.ParentObject, null, null, 0))
			{
				E.Add(this.commandId, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B21 RID: 39713 RVA: 0x00384328 File Offset: 0x00382528
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddDynamicCommand(out this.commandId, "CommandEmergencyRecomposite", "Emergency Recomposite", "Cybernetics", null, "\a", null, false, false, false, false, true, false, false, false, true, true, false, false, -1, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B22 RID: 39714 RVA: 0x0038437A File Offset: 0x0038257A
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B23 RID: 39715 RVA: 0x00384398 File Offset: 0x00382598
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.commandId && E.Actor != null && E.Actor == this.ParentObject.Implantee && E.Actor.CurrentCell != null)
			{
				if (E.Actor.OnWorldMap())
				{
					return E.Actor.Fail("You cannot do that on the world map.");
				}
				List<Cell> cells = E.Actor.CurrentZone.GetCells((Cell c) => c.Explored && c.IsEmptyOfSolidFor(E.Actor, true));
				if (cells.Count <= 0)
				{
					return E.Actor.Fail("There are no places to escape to safely!");
				}
				Cell randomElement = cells.GetRandomElement<Cell>(null);
				Event @event = Event.New("InitiateRealityDistortionTransit", "Object", E.Actor, "Device", this.ParentObject, "Cell", randomElement);
				if (!this.ParentObject.FireEvent(@event, E) || !randomElement.FireEvent(@event, E))
				{
					return false;
				}
				E.Actor.TechTeleportSwirlOut(null, "&B", null);
				if (E.Actor.TeleportTo(randomElement, new int?(0), true, false, false, false, "disappear", "appear"))
				{
					E.Actor.TechTeleportSwirlIn(null, "&C", null);
				}
				IComponent<GameObject>.XDidY(E.Actor, "teleport", null, null, null, null, null, null, false, false, null, null, false, true, false, false, false, null);
				int num = GetAvailableComputePowerEvent.AdjustDown(E.Actor, this.GetBaseCooldown(), 2);
				E.Actor.CooldownActivatedAbility(this.ActivatedAbilityID, num, null, false);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B24 RID: 39716 RVA: 0x0038459C File Offset: 0x0038279C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice reduces this item's cooldown.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009B25 RID: 39717 RVA: 0x003845B6 File Offset: 0x003827B6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004065 RID: 16485
		public string commandId = "";

		// Token: 0x04004066 RID: 16486
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
