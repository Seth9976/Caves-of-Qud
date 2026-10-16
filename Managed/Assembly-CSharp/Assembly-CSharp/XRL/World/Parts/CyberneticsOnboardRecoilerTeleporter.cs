using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F3A RID: 3898
	[Serializable]
	public class CyberneticsOnboardRecoilerTeleporter : ITeleporter
	{
		// Token: 0x06009B76 RID: 39798 RVA: 0x0038666C File Offset: 0x0038486C
		public CyberneticsOnboardRecoilerTeleporter()
		{
			this.ChargeUse = 0;
			this.WorksOnCarrier = false;
			this.WorksOnHolder = false;
			this.WorksOnImplantee = true;
			this.NameForStatus = "TeleportationSystem";
		}

		// Token: 0x06009B77 RID: 39799 RVA: 0x003866A6 File Offset: 0x003848A6
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B78 RID: 39800 RVA: 0x003866AC File Offset: 0x003848AC
		public string GetName()
		{
			string destinationZone = this.DestinationZone;
			if (string.IsNullOrEmpty(destinationZone))
			{
				return null;
			}
			string zoneDisplayName = The.ZoneManager.GetZoneDisplayName(destinationZone, true, false, true, true);
			if (string.IsNullOrEmpty(zoneDisplayName))
			{
				return null;
			}
			return zoneDisplayName;
		}

		// Token: 0x06009B79 RID: 39801 RVA: 0x003866E8 File Offset: 0x003848E8
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			GameObject parentObject = this.ParentObject;
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(activatedAbilityID, (parentObject != null) ? parentObject.Implantee : null);
			string name = this.GetName();
			if (name.IsNullOrEmpty())
			{
				stats.Set("Location", "none", false, 0);
			}
			else
			{
				stats.Set("Location", name, false, 0);
			}
			int num = stats.CollectComputePowerAdjustDown(activatedAbilityEntry, "Cooldown", this.GetBaseCooldown(), 1f);
			stats.CollectCooldownTurns(activatedAbilityEntry, num, num - this.GetBaseCooldown());
		}

		// Token: 0x06009B7A RID: 39802 RVA: 0x0038676D File Offset: 0x0038496D
		public int GetBaseCooldown()
		{
			return 50;
		}

		// Token: 0x06009B7B RID: 39803 RVA: 0x00386774 File Offset: 0x00384974
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == InventoryActionEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009B7C RID: 39804 RVA: 0x003867C5 File Offset: 0x003849C5
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B7D RID: 39805 RVA: 0x003867FC File Offset: 0x003849FC
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddDynamicCommand(out this.CommandId, "CommandActivateOnboardRecoilerTeleporter", "Recoil", "Cybernetics", null, "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B7E RID: 39806 RVA: 0x0038684E File Offset: 0x00384A4E
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B7F RID: 39807 RVA: 0x0038686C File Offset: 0x00384A6C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command != null && E.Command == this.CommandId && E.Actor == this.ParentObject.Implantee)
			{
				this.ActuateTeleport(IComponent<GameObject>.ThePlayer, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B80 RID: 39808 RVA: 0x003868BA File Offset: 0x00384ABA
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice reduces this item's cooldown.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009B81 RID: 39809 RVA: 0x003868D4 File Offset: 0x00384AD4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsObjectActivePartSubject(IComponent<GameObject>.ThePlayer))
			{
				E.AddAction("Activate", "activate", "ActivateRecoilerTeleporter", null, 'a', false, 100, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B82 RID: 39810 RVA: 0x00386919 File Offset: 0x00384B19
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateRecoilerTeleporter")
			{
				this.ActuateTeleport(E.Actor, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B83 RID: 39811 RVA: 0x00386941 File Offset: 0x00384B41
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009B84 RID: 39812 RVA: 0x00386944 File Offset: 0x00384B44
		public override bool VisibleInRecoilerList()
		{
			return base.VisibleInRecoilerList() && this.ParentObject.Implantee != null;
		}

		// Token: 0x06009B85 RID: 39813 RVA: 0x00386960 File Offset: 0x00384B60
		private void ActuateTeleport(GameObject who, IEvent E)
		{
			if (who.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				if (base.AttemptTeleport(who, E))
				{
					who.UseEnergy(1000, "Cybernetics Recoiler", null, null, false);
					int num = GetAvailableComputePowerEvent.AdjustDown(who, 50, 2);
					who.CooldownActivatedAbility(this.ActivatedAbilityID, num, null, false);
					E.RequestInterfaceExit();
					this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
					return;
				}
			}
			else if (who.IsPlayer())
			{
				Popup.ShowFail("You can't recoil yet.", true, true, true);
			}
		}

		// Token: 0x0400407D RID: 16509
		public const int COOLDOWN = 50;

		// Token: 0x0400407E RID: 16510
		public string CommandId;

		// Token: 0x0400407F RID: 16511
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
