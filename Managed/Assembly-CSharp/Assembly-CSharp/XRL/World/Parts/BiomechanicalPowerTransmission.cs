using System;

namespace XRL.World.Parts
{
	// Token: 0x020011B7 RID: 4535
	[Serializable]
	public class BiomechanicalPowerTransmission : IPowerTransmission
	{
		// Token: 0x0600B40A RID: 46090 RVA: 0x0041AEE8 File Offset: 0x004190E8
		public BiomechanicalPowerTransmission()
		{
			this.IsEMPSensitive = false;
			this.ChargeRate = 200;
			this.ChanceBreakConnectedOnDestroy = 30;
			this.ChanceBreakConnectedOnMove = 0;
			this.ChanceBreakOnMove = 0;
			this.Substance = "power";
			this.Activity = "carrying";
			this.Constituent = "biomachinery";
			this.Assembly = "biomechanical transmission system";
			this.Unit = "joule";
			this.UnitFactor = 0.1;
		}

		// Token: 0x0600B40B RID: 46091 RVA: 0x0041AF69 File Offset: 0x00419169
		public override string GetPowerTransmissionType()
		{
			return "biomechanical";
		}
	}
}
