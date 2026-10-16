using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F54 RID: 3924
	[Serializable]
	public class TetheredOnboardRecoilerTeleporter : ITeleporter
	{
		// Token: 0x06009C88 RID: 40072 RVA: 0x0038C171 File Offset: 0x0038A371
		public TetheredOnboardRecoilerTeleporter()
		{
			this.ChargeUse = 0;
			this.WorksOnCarrier = false;
			this.WorksOnHolder = false;
			this.WorksOnSelf = true;
			this.NameForStatus = "TeleportationSystem";
		}

		// Token: 0x06009C89 RID: 40073 RVA: 0x0038C1A8 File Offset: 0x0038A3A8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009C8A RID: 40074 RVA: 0x0038C1AB File Offset: 0x0038A3AB
		public override void Initialize()
		{
			this.CheckAbility();
		}

		// Token: 0x06009C8B RID: 40075 RVA: 0x0038C1B3 File Offset: 0x0038A3B3
		public override void Remove()
		{
			base.RemoveMyActivatedAbility(ref this.AbilityID, null);
		}

		// Token: 0x06009C8C RID: 40076 RVA: 0x0038C1C3 File Offset: 0x0038A3C3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == InventoryActionEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009C8D RID: 40077 RVA: 0x0038C1E9 File Offset: 0x0038A3E9
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.AbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C8E RID: 40078 RVA: 0x0038C20C File Offset: 0x0038A40C
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid abilityID = this.AbilityID;
			GameObject parentObject = this.ParentObject;
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(abilityID, (parentObject != null) ? parentObject.Implantee : null);
			int num = stats.CollectComputePowerAdjustDown(activatedAbilityEntry, "Cooldown", this.Cooldown, 1f);
			stats.CollectCooldownTurns(activatedAbilityEntry, num, num - this.Cooldown);
			if (!this.DestinationZone.IsNullOrEmpty())
			{
				stats.Set("Destination", The.ZoneManager.GetZoneBaseDisplayName(this.DestinationZone, false), false, 0);
				stats.Set("TetherStatus", this.ValidateTether() ? "Tethered" : "Broken", false, 0);
				return;
			}
			stats.Set("Destination", "{{R|Uninitialized}}", false, 0);
		}

		// Token: 0x06009C8F RID: 40079 RVA: 0x0038C2BF File Offset: 0x0038A4BF
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandActivateTetheredRecoilerTeleporter")
			{
				this.ActuateTeleport(this.GetActivePartFirstSubject(), E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C90 RID: 40080 RVA: 0x0038C2E8 File Offset: 0x0038A4E8
		private void ActuateTeleport(GameObject Subject, IEvent E)
		{
			if (!Subject.IsActivatedAbilityUsable(this.AbilityID))
			{
				Subject.ShowFailure("You can't recoil yet.");
				return;
			}
			if (!this.ValidateTether())
			{
				GameObject gameObject = GameObjectFactory.Factory.CreateSampleObject(this.TetheredBlueprint, null);
				Subject.ShowFailure("There is no " + gameObject.ShortDisplayNameSingle + " at the destination to receive you.");
				return;
			}
			if (base.AttemptTeleport(Subject, E))
			{
				Subject.UseEnergy(1000, "Recoiler", null, null, false);
				Subject.CooldownActivatedAbility(this.AbilityID, GetAvailableComputePowerEvent.AdjustDown(Subject, this.Cooldown, 2), null, false);
				E.RequestInterfaceExit();
				GameObject partyLeader = Subject.PartyLeader;
				if (partyLeader != null && partyLeader.CurrentZone != Subject.CurrentZone)
				{
					Subject.Brain.Staying = true;
				}
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
		}

		// Token: 0x06009C91 RID: 40081 RVA: 0x0038C3C8 File Offset: 0x0038A5C8
		private void CheckAbility()
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject == null)
			{
				return;
			}
			if (this.DestinationZone.IsNullOrEmpty())
			{
				activePartFirstSubject.RemoveActivatedAbility(ref this.AbilityID);
				return;
			}
			string text = ((The.ZoneManager == null) ? "Recoil" : ("Recoil to " + The.ZoneManager.GetZoneBaseDisplayName(this.DestinationZone, false)));
			if (this.AbilityID == Guid.Empty)
			{
				this.AbilityID = activePartFirstSubject.AddActivatedAbility(text, "CommandActivateTetheredRecoilerTeleporter", "Maneuvers", null, "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
				return;
			}
			activePartFirstSubject.GetActivatedAbility(this.AbilityID).DisplayName = text;
		}

		// Token: 0x06009C92 RID: 40082 RVA: 0x0038C47C File Offset: 0x0038A67C
		private bool ValidateTether()
		{
			if (this.TetheredBlueprint.IsNullOrEmpty())
			{
				return true;
			}
			if (this.DestinationZone.IsNullOrEmpty())
			{
				return false;
			}
			Zone zone = The.ZoneManager.GetZone(this.DestinationZone);
			Cell cell = ((zone != null) ? zone.GetCell(this.DestinationX, this.DestinationY) : null);
			if (cell == null)
			{
				return false;
			}
			GameObject gameObject = cell.FindObject(this.TetheredBlueprint);
			if (gameObject != null)
			{
				return true;
			}
			gameObject = zone.FindObject(this.TetheredBlueprint);
			if (gameObject != null)
			{
				cell = gameObject.CurrentCell;
				this.DestinationX = cell.X;
				this.DestinationY = cell.Y;
				return true;
			}
			return false;
		}

		// Token: 0x06009C93 RID: 40083 RVA: 0x0038C51A File Offset: 0x0038A71A
		public void SetDestination(Cell C)
		{
			this.SetDestination(C.ParentZone.ZoneID, C.X, C.Y);
		}

		// Token: 0x06009C94 RID: 40084 RVA: 0x0038C539 File Offset: 0x0038A739
		public void SetDestination(string ZoneID, int X, int Y)
		{
			this.DestinationZone = ZoneID;
			this.DestinationX = X;
			this.DestinationY = Y;
			this.CheckAbility();
		}

		// Token: 0x040040BF RID: 16575
		public const string ABL_CMD = "CommandActivateTetheredRecoilerTeleporter";

		// Token: 0x040040C0 RID: 16576
		public string TetheredBlueprint;

		// Token: 0x040040C1 RID: 16577
		public int Cooldown = 50;

		// Token: 0x040040C2 RID: 16578
		public Guid AbilityID;
	}
}
