using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AA2 RID: 2722
	public class IsCheckpoint
	{
		// Token: 0x06006F1F RID: 28447 RVA: 0x002682A1 File Offset: 0x002664A1
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).RequireObject("CheckpointWidget", null, null, null, null).SetStringProperty("CheckpointKey", this.Key, false);
			return true;
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x002682CB File Offset: 0x002664CB
		public bool BuildZoneWithKey(Zone Z, string key)
		{
			Z.GetCell(0, 0).RequireObject("CheckpointWidget", null, null, null, null).SetStringProperty("CheckpointKey", key, false);
			return true;
		}

		// Token: 0x040032EF RID: 13039
		public string Key;
	}
}
