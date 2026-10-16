using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AE6 RID: 2790
	public class Connecter
	{
		// Token: 0x06007004 RID: 28676 RVA: 0x00279AA0 File Offset: 0x00277CA0
		public bool BuildZone(Zone Z)
		{
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type == "StairsUp" || zoneConnection.Type == "StairsDown")
				{
					Z.GetCell(zoneConnection.X, zoneConnection.Y).ClearWalls();
					Z.GetCell(zoneConnection.X, zoneConnection.Y).ClearImpassableObjects(The.Player, false);
					Z.BuildReachableMap(zoneConnection.X, zoneConnection.Y);
					break;
				}
			}
			return true;
		}
	}
}
