using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ACA RID: 2762
	public class PulldownLocation
	{
		// Token: 0x06006FAB RID: 28587 RVA: 0x002738F5 File Offset: 0x00271AF5
		public bool BuildZone(Zone Z)
		{
			ZoneManager.instance.SetZoneProperty(Z.ZoneID, "PullDownLocation", this.x.ToString() + "," + this.y.ToString());
			return true;
		}

		// Token: 0x04003355 RID: 13141
		public int x;

		// Token: 0x04003356 RID: 13142
		public int y;
	}
}
