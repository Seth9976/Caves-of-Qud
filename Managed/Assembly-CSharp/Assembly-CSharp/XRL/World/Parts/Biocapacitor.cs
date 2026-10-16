using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020011B6 RID: 4534
	[Serializable]
	public class Biocapacitor : Capacitor
	{
		// Token: 0x0600B408 RID: 46088 RVA: 0x0041AE68 File Offset: 0x00419068
		public Biocapacitor()
		{
			this.ChargeRate = 3;
			this.MaxCharge = 5000;
			this.MinimumChargeToExplode = 2500;
			this.Description = "biocapacitor";
			this.ChargeDisplayStyle = "bio";
			this.AltChargeDisplayStyle = "percentage";
			this.AltChargeDisplayProperty = Scanning.GetScanPropertyName(Scanning.Scan.Bio);
			this.ChargeLossDisable = false;
			base.IsBioScannable = true;
		}

		// Token: 0x0600B409 RID: 46089 RVA: 0x0041AED3 File Offset: 0x004190D3
		public override bool InteractWithChargeEvent(IChargeEvent E)
		{
			return E.IncludeBiological && base.InteractWithChargeEvent(E);
		}
	}
}
