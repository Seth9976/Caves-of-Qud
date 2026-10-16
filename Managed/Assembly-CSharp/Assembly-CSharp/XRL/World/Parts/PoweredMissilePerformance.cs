using System;

namespace XRL.World.Parts
{
	// Token: 0x02001120 RID: 4384
	[Serializable]
	public class PoweredMissilePerformance : MissilePerformance
	{
		// Token: 0x0600AD81 RID: 44417 RVA: 0x00403BEA File Offset: 0x00401DEA
		public PoweredMissilePerformance()
		{
			this.ChargeUse = 100;
			this.IsBootSensitive = true;
			this.IsEMPSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "PerformanceEnhancementSystems";
		}
	}
}
