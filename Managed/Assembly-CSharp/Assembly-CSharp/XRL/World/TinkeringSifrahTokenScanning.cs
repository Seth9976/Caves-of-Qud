using System;
using XRL.World.Capabilities;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200090B RID: 2315
	[Serializable]
	public class TinkeringSifrahTokenScanning : SifrahToken
	{
		// Token: 0x0600586C RID: 22636 RVA: 0x001AC396 File Offset: 0x001AA596
		public TinkeringSifrahTokenScanning()
		{
			this.Description = "scanning";
			this.Tile = "Items/sw_aircurrent.bmp";
			this.RenderString = "å";
			this.ColorString = "&C";
			this.DetailColor = 'W';
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x001AC3D2 File Offset: 0x001AA5D2
		public TinkeringSifrahTokenScanning(Scanning.Scan scan)
			: this()
		{
			this.Description = "read " + Scanning.GetScanSubjectName(scan);
		}
	}
}
