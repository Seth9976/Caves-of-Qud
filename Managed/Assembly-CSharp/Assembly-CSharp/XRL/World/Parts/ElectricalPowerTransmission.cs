using System;

namespace XRL.World.Parts
{
	// Token: 0x020011C0 RID: 4544
	[Serializable]
	public class ElectricalPowerTransmission : IPowerTransmission
	{
		// Token: 0x0600B482 RID: 46210 RVA: 0x0041D580 File Offset: 0x0041B780
		public ElectricalPowerTransmission()
		{
			this.ChargeRate = 500;
			this.ChanceBreakConnectedOnDestroy = 100;
			this.Substance = "charge";
			this.Activity = "conducting";
			this.Constituent = "wiring";
			this.Assembly = "power grid";
			this.Unit = "amp";
			this.UnitFactor = 0.1;
			this.SparkWhenBrokenAndPowered = true;
		}

		// Token: 0x0600B483 RID: 46211 RVA: 0x0041D5F3 File Offset: 0x0041B7F3
		public override string GetPowerTransmissionType()
		{
			return "electrical";
		}

		// Token: 0x0600B484 RID: 46212 RVA: 0x0041D5FA File Offset: 0x0041B7FA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetElectricalConductivityEvent>.ID;
		}

		// Token: 0x0600B485 RID: 46213 RVA: 0x0041D610 File Offset: 0x0041B810
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}
	}
}
