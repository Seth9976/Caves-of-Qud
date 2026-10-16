using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FE6 RID: 4070
	[Serializable]
	public class IPoweredPart : IActivePart
	{
		// Token: 0x0600A1C6 RID: 41414 RVA: 0x003AFDEC File Offset: 0x003ADFEC
		public IPoweredPart()
		{
			this.ChargeUse = 1;
			this.IsBootSensitive = true;
			this.IsEMPSensitive = true;
			this.IsPowerSwitchSensitive = true;
			base.IsTechScannable = true;
		}
	}
}
