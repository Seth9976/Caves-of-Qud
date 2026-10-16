using System;
using XRL.Core;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A59 RID: 2649
	public class CanyonReacher
	{
		// Token: 0x06006E28 RID: 28200 RVA: 0x0025A0E4 File Offset: 0x002582E4
		public bool BuildZone(Zone Z)
		{
			Z.ClearReachableMap();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type.Contains("Canyon"))
				{
					Z.BuildReachableMap(zoneConnection.X, zoneConnection.Y, false);
				}
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.Type.Contains("Canyon") && cachedZoneConnection.TargetDirection == "-")
				{
					Z.BuildReachableMap(cachedZoneConnection.X, cachedZoneConnection.Y, false);
				}
			}
			return true;
		}
	}
}
