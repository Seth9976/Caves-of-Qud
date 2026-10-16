using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F39 RID: 3897
	[Serializable]
	public class CyberneticsOnboardRecoilerImprinting : IProgrammableRecoiler
	{
		// Token: 0x06009B67 RID: 39783 RVA: 0x0038623C File Offset: 0x0038443C
		public CyberneticsOnboardRecoilerImprinting()
		{
			this.ChargeUse = 0;
			base.WorksOn(false, false, false, false, false, false, true, false, false, false);
			this.NameForStatus = "GeospatialCore";
			this.Reprogrammable = true;
		}

		// Token: 0x06009B68 RID: 39784 RVA: 0x00386283 File Offset: 0x00384483
		public override void ProgrammedForLocation(Zone Z, Cell C)
		{
			this.UpdateName();
		}

		// Token: 0x06009B69 RID: 39785 RVA: 0x0038628B File Offset: 0x0038448B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B6A RID: 39786 RVA: 0x00386290 File Offset: 0x00384490
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
			stats.CollectCooldownTurns(activatedAbilityEntry, 100, 0);
		}

		// Token: 0x06009B6B RID: 39787 RVA: 0x003862F8 File Offset: 0x003844F8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == ImplantedEvent.ID || ID == InventoryActionEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009B6C RID: 39788 RVA: 0x00386349 File Offset: 0x00384549
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B6D RID: 39789 RVA: 0x00386380 File Offset: 0x00384580
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddDynamicCommand(out this.CommandId, "CommandActivateOnboardRecoilerImprinting", "Imprint with current location", "Cybernetics", null, "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B6E RID: 39790 RVA: 0x003863D2 File Offset: 0x003845D2
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B6F RID: 39791 RVA: 0x003863F0 File Offset: 0x003845F0
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command != null && E.Command == this.CommandId && E.Actor == this.ParentObject.Implantee && base.ProgramRecoiler(E.Actor, E))
			{
				E.Actor.UseEnergy(1000, "Cybernetics Recoiler Imprint", null, null, false);
				E.Actor.CooldownActivatedAbility(this.ActivatedAbilityID, 100, null, false);
				E.RequestInterfaceExit();
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B70 RID: 39792 RVA: 0x00386494 File Offset: 0x00384694
		public void UpdateName()
		{
			CyberneticsOnboardRecoilerTeleporter cyberneticsOnboardRecoilerTeleporter;
			if (this.ParentObject.TryGetPart<CyberneticsOnboardRecoilerTeleporter>(out cyberneticsOnboardRecoilerTeleporter))
			{
				GameObject implantee = this.ParentObject.Implantee;
				ActivatedAbilityEntry activatedAbilityEntry = ((implantee != null) ? implantee.GetActivatedAbility(cyberneticsOnboardRecoilerTeleporter.ActivatedAbilityID) : null);
				if (activatedAbilityEntry != null)
				{
					string name = this.GetName();
					activatedAbilityEntry.DisplayName = (name.IsNullOrEmpty() ? "Recoil" : ("Recoil to " + name));
				}
			}
			this.NameTick = The.ZoneManager.NameUpdateTick;
		}

		// Token: 0x06009B71 RID: 39793 RVA: 0x00386508 File Offset: 0x00384708
		public string GetName()
		{
			CyberneticsOnboardRecoilerTeleporter part = this.ParentObject.GetPart<CyberneticsOnboardRecoilerTeleporter>();
			if (part != null)
			{
				string destinationZone = part.DestinationZone;
				if (!string.IsNullOrEmpty(destinationZone))
				{
					string zoneDisplayName = The.ZoneManager.GetZoneDisplayName(destinationZone, true, false, true, true);
					if (string.IsNullOrEmpty(zoneDisplayName))
					{
						return null;
					}
					return zoneDisplayName;
				}
			}
			return null;
		}

		// Token: 0x06009B72 RID: 39794 RVA: 0x00386550 File Offset: 0x00384750
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (The.Game.ZoneManager != null && The.ZoneManager.NameUpdateTick > this.NameTick)
			{
				this.UpdateName();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B73 RID: 39795 RVA: 0x00386580 File Offset: 0x00384780
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsObjectActivePartSubject(IComponent<GameObject>.ThePlayer))
			{
				E.AddAction("Imprint", "imprint", "ImprintRecoiler", null, 'i', false, 100, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B74 RID: 39796 RVA: 0x003865C8 File Offset: 0x003847C8
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ImprintRecoiler")
			{
				if (!E.Actor.IsActivatedAbilityUsable(this.ActivatedAbilityID))
				{
					if (E.Actor.IsPlayer())
					{
						Popup.ShowFail("You can't imprint yet.", true, true, true);
					}
				}
				else if (base.ProgramRecoiler(E.Actor, E))
				{
					E.Actor.UseEnergy(1000, "Cybernetics Recoiler Imprint", null, null, false);
					E.Actor.CooldownActivatedAbility(this.ActivatedAbilityID, 100, null, false);
					E.RequestInterfaceExit();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B75 RID: 39797 RVA: 0x00386669 File Offset: 0x00384869
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004079 RID: 16505
		public const int COOLDOWN = 100;

		// Token: 0x0400407A RID: 16506
		[NonSerialized]
		private int NameTick;

		// Token: 0x0400407B RID: 16507
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x0400407C RID: 16508
		public string CommandId;
	}
}
