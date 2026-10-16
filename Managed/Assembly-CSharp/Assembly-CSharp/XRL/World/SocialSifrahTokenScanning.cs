using System;
using XRL.World.Capabilities;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F1 RID: 2289
	[Serializable]
	public class SocialSifrahTokenScanning : SifrahToken
	{
		// Token: 0x060057E6 RID: 22502 RVA: 0x001AA5B0 File Offset: 0x001A87B0
		public SocialSifrahTokenScanning()
		{
			this.Description = "scanning";
			this.Tile = "Items/sw_aircurrent.bmp";
			this.RenderString = "å";
			this.ColorString = "&C";
			this.DetailColor = 'W';
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x001AA5EC File Offset: 0x001A87EC
		public SocialSifrahTokenScanning(Scanning.Scan scan)
			: this()
		{
			this.Description = "interpret " + Scanning.GetScanSubjectName(scan);
		}
	}
}
