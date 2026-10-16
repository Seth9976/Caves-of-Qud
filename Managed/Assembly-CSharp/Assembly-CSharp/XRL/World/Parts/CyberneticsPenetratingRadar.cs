using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F3D RID: 3901
	[Serializable]
	public class CyberneticsPenetratingRadar : IPoweredPart
	{
		// Token: 0x06009B92 RID: 39826 RVA: 0x00386AF4 File Offset: 0x00384CF4
		public CyberneticsPenetratingRadar()
		{
			this.ChargeUse = 0;
			this.WorksOnImplantee = true;
			this.NameForStatus = "PhasedRadarArray";
		}

		// Token: 0x06009B93 RID: 39827 RVA: 0x00386B28 File Offset: 0x00384D28
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B94 RID: 39828 RVA: 0x00386B2C File Offset: 0x00384D2C
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			GameObject parentObject = this.ParentObject;
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(activatedAbilityID, (parentObject != null) ? parentObject.Implantee : null);
			int num = stats.CollectComputePowerAdjustUp(activatedAbilityEntry, "Radius", this.Radius, 1f);
			stats.Set("Radius", num, num != this.Radius, num - this.Radius);
		}

		// Token: 0x06009B95 RID: 39829 RVA: 0x00386B90 File Offset: 0x00384D90
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009B96 RID: 39830 RVA: 0x00386BCE File Offset: 0x00384DCE
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B97 RID: 39831 RVA: 0x00386C04 File Offset: 0x00384E04
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee != null && implantee.IsPlayer() && base.WasReady() && implantee.IsActivatedAbilityUsable(this.ActivatedAbilityID) && implantee.IsActivatedAbilityToggledOn(this.ActivatedAbilityID))
			{
				Cell currentCell = implantee.CurrentCell;
				if (currentCell != null && !currentCell.OnWorldMap())
				{
					int num = GetAvailableComputePowerEvent.AdjustUp(implantee, this.Radius);
					Zone parentZone = currentCell.ParentZone;
					if (parentZone != null)
					{
						parentZone.AddLight(currentCell.X, currentCell.Y, num, LightLevel.Radar, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B98 RID: 39832 RVA: 0x00386C96 File Offset: 0x00384E96
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's range.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009B99 RID: 39833 RVA: 0x00386CB0 File Offset: 0x00384EB0
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Penetrating Radar", CyberneticsPenetratingRadar.COMMAND_NAME, "Cybernetics", null, "\a", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B9A RID: 39834 RVA: 0x00386D24 File Offset: 0x00384F24
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B9B RID: 39835 RVA: 0x00386D74 File Offset: 0x00384F74
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == CyberneticsPenetratingRadar.COMMAND_NAME && E.Actor == this.ParentObject.Implantee)
			{
				this.ParentObject.Implantee.ToggleActivatedAbility(this.ActivatedAbilityID, false, null);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
				base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B9C RID: 39836 RVA: 0x00386E08 File Offset: 0x00385008
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009B9D RID: 39837 RVA: 0x00386E0C File Offset: 0x0038500C
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, true, null);
		}

		// Token: 0x04004080 RID: 16512
		public static readonly string COMMAND_NAME = "CommandTogglePenetratingRadar";

		// Token: 0x04004081 RID: 16513
		public int Radius = 10;

		// Token: 0x04004082 RID: 16514
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
