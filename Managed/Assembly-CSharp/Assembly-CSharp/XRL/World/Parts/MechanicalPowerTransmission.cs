using System;

namespace XRL.World.Parts
{
	// Token: 0x020011D6 RID: 4566
	[Serializable]
	public class MechanicalPowerTransmission : IPowerTransmission
	{
		// Token: 0x0600B5F6 RID: 46582 RVA: 0x00426428 File Offset: 0x00424628
		public MechanicalPowerTransmission()
		{
			this.IsEMPSensitive = false;
			this.ChargeRate = 100;
			this.ChanceBreakConnectedOnDestroy = 100;
			this.Substance = "power";
			this.Activity = "carrying";
			this.Constituent = "machinery";
			this.Assembly = "mechanical transmission system";
			this.Unit = "joule";
			this.UnitFactor = 0.1;
		}

		// Token: 0x0600B5F7 RID: 46583 RVA: 0x00426498 File Offset: 0x00424698
		public override string GetPowerTransmissionType()
		{
			return "mechanical";
		}
	}
}
