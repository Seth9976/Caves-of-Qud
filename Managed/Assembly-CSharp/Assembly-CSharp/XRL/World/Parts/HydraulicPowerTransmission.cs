using System;

namespace XRL.World.Parts
{
	// Token: 0x020011CB RID: 4555
	[Serializable]
	public class HydraulicPowerTransmission : IPowerTransmission
	{
		// Token: 0x0600B514 RID: 46356 RVA: 0x00421574 File Offset: 0x0041F774
		public HydraulicPowerTransmission()
		{
			this.IsEMPSensitive = false;
			this.ChargeRate = 2000;
			this.ChanceBreakConnectedOnDestroy = 100;
			this.Substance = "power";
			this.Activity = "carrying";
			this.Constituent = "plumbing";
			this.Assembly = "hydraulic transmission system";
			this.Unit = "joule";
			this.UnitFactor = 0.1;
			this.DependsOnLiquid = "water";
			this.WrongLiquidFactor = 0.2;
		}

		// Token: 0x0600B515 RID: 46357 RVA: 0x00421601 File Offset: 0x0041F801
		public override string GetPowerTransmissionType()
		{
			return "hydraulic";
		}
	}
}
