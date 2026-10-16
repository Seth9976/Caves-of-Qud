using System;
using XRL.Core;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AE7 RID: 2791
	public class Spire
	{
		// Token: 0x06007006 RID: 28678 RVA: 0x00279B70 File Offset: 0x00277D70
		public bool BuildZone(Zone Z)
		{
			int num = 0;
			SpireZoneTemplate spireZoneTemplate;
			Ruiner ruiner;
			for (;;)
			{
				num++;
				ZoneManager zoneManager = XRLCore.Core.Game.ZoneManager;
				Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("Dirty"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				if (zoneManager.GetZoneColumnProperty(Z.ZoneID, "Builder.Ruins.SpireZoneTemplate", null) != null)
				{
					try
					{
						spireZoneTemplate = ((SpireZoneTemplate)zoneManager.GetZoneColumnProperty(Z.ZoneID, "Builder.Ruins.SpireZoneTemplate", null)).Copy();
						goto IL_00BF;
					}
					catch (Exception)
					{
					}
					goto IL_0078;
				}
				goto IL_0078;
				IL_00BF:
				ruiner = new Ruiner();
				Z.ClearReachableMap();
				spireZoneTemplate.GenerateRooms(Z.GetZoneZ() - 10);
				if (spireZoneTemplate.EnsureConnections(Z) || num >= 20)
				{
					break;
				}
				continue;
				IL_0078:
				spireZoneTemplate = new SpireZoneTemplate();
				spireZoneTemplate.New(Z.Width, Z.Height, this.ZonesWide.RollCached(), this.ZonesHigh.RollCached());
				zoneManager.SetZoneColumnProperty(Z.ZoneID, "Builder.Ruins.SpireZoneTemplate", spireZoneTemplate);
				spireZoneTemplate = spireZoneTemplate.Copy();
				goto IL_00BF;
			}
			spireZoneTemplate.BuildZone(Z, Z.GetZoneZ() > 10);
			this.RuinLevel = 50 - Z.GetZoneZ();
			if (this.RuinLevel > 0)
			{
				ruiner.RuinZone(Z, 50 - Z.GetZoneZ(), Z.GetZoneZ() > 10, 100000, 500000);
			}
			bool flag = false;
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type == "StairsUp" || zoneConnection.Type == "StairsDown")
				{
					flag = true;
					Z.BuildReachableMap(zoneConnection.X, zoneConnection.Y);
					break;
				}
			}
			if (!flag)
			{
				for (int i = 0; i < Z.Height; i++)
				{
					for (int j = 0; j < Z.Width; j++)
					{
						if (!Z.GetCell(j, i).IsSolid())
						{
							Z.BuildReachableMap(j, i);
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x04003386 RID: 13190
		public int RuinLevel;

		// Token: 0x04003387 RID: 13191
		public string ZonesWide = "1";

		// Token: 0x04003388 RID: 13192
		public string ZonesHigh = "1";
	}
}
