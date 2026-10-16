using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F35 RID: 3893
	[Serializable]
	public class CyberneticsMicromanipulatorArray : IPoweredPart
	{
		// Token: 0x06009B3F RID: 39743 RVA: 0x003854F0 File Offset: 0x003836F0
		public CyberneticsMicromanipulatorArray()
		{
			this.WorksOnImplantee = true;
			this.ChargeUse = 0;
			this.NameForStatus = "ServoMechanisms";
		}

		// Token: 0x06009B40 RID: 39744 RVA: 0x00385511 File Offset: 0x00383711
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B41 RID: 39745 RVA: 0x00385514 File Offset: 0x00383714
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetEnergyCostEvent>.ID || ID == GetTinkeringBonusEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009B42 RID: 39746 RVA: 0x00385542 File Offset: 0x00383742
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "CombatPreventsRepair");
			E.Implantee.RegisterPartEvent(this, "CombatPreventsTinkering");
			return base.HandleEvent(E);
		}

		// Token: 0x06009B43 RID: 39747 RVA: 0x0038556D File Offset: 0x0038376D
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "CombatPreventsRepair");
			E.Implantee.UnregisterPartEvent(this, "CombatPreventsTinkering");
			return base.HandleEvent(E);
		}

		// Token: 0x06009B44 RID: 39748 RVA: 0x00385598 File Offset: 0x00383798
		public override bool HandleEvent(GetEnergyCostEvent E)
		{
			if (base.WasReady() && !E.Type.IsNullOrEmpty() && E.Type.Contains("Skill Tinkering") && !E.Type.Contains("Build") && !E.Type.Contains("Mod") && !E.Type.Contains("Repair") && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.PercentageReduction = Math.Max(Math.Min(40 + GetAvailableComputePowerEvent.GetFor(this.ParentObject.Implantee) / 2, 60), E.PercentageReduction);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B45 RID: 39749 RVA: 0x00385667 File Offset: 0x00383867
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.ForSifrah && E.Actor == this.ParentObject.Implantee)
			{
				E.Bonus++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B46 RID: 39750 RVA: 0x00385699 File Offset: 0x00383899
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06009B47 RID: 39751 RVA: 0x0038569C File Offset: 0x0038389C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CombatPreventsTinkering")
			{
				if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
			}
			else if (E.ID == "CombatPreventsRepair" && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			return base.FireEvent(E);
		}
	}
}
