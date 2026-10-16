using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A67 RID: 2663
	public class FungalTrailKlanqHut : ZoneBuilderSandbox
	{
		// Token: 0x06006E47 RID: 28231 RVA: 0x0025B114 File Offset: 0x00259314
		public bool BuildZone(Zone Z)
		{
			int num = Stat.Random(20, 50);
			int num2 = Stat.Random(10, 15);
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (!(zoneConnection.Type != "FungalTrailStart"))
				{
					num = zoneConnection.X;
					num2 = zoneConnection.Y;
					break;
				}
			}
			Rect2D rect2D = new Rect2D(num - 3, num2 - 4, num + 3, num2 + 4);
			ZoneBuilderSandbox.PlaceObjectInRect(Z, rect2D.ReduceBy(1, 1), "PaxKlanq", null, 0, null);
			base.PlaceHut(Z, rect2D, "FungalTrailBrick", "GodshroomWall", "PaxKlanqHut", true);
			return true;
		}
	}
}
