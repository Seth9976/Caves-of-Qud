using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC7 RID: 2759
	public class PossibleCryotube : ZoneBuilderSandbox
	{
		// Token: 0x06006F96 RID: 28566 RVA: 0x00272290 File Offset: 0x00270490
		public bool BuildZone(Zone Z)
		{
			if (Stat.Random(1, 10000) > this.Chance + this.ExtraChancePerLevel * (Z.Z - 10))
			{
				return true;
			}
			int num = 1;
			while (Stat.Random(1, 10000) <= this.ChanceOfAdditionalTubes)
			{
				num++;
			}
			for (int i = 0; i < num; i++)
			{
				Cell cellWithEmptyBorder = Z.GetCellWithEmptyBorder(2);
				if (cellWithEmptyBorder != null)
				{
					string text = PopulationManager.RollOneFrom("CryotubeContents", null, null).Blueprint;
					int x = cellWithEmptyBorder.X;
					int y = cellWithEmptyBorder.Y;
					for (int j = y - 2; j <= y + 2; j++)
					{
						for (int k = x - 2; k <= x + 2; k++)
						{
							if (Z.GetCell(k, j).HasObjectWithTag("Stairs"))
							{
								text = "*Destroyed";
							}
							string zoneIDFromDirection = Z.GetZoneIDFromDirection("U", 1);
							if (The.ZoneManager.IsZoneLive(zoneIDFromDirection) && Z.GetZoneFromDirection("U").GetCell(k, j).HasObjectWithTag("Stairs"))
							{
								text = "*Destroyed";
							}
							string zoneIDFromDirection2 = Z.GetZoneIDFromDirection("D", 1);
							if (The.ZoneManager.IsZoneLive(zoneIDFromDirection2) && Z.GetZoneFromDirection("D").GetCell(k, j).HasObjectWithTag("Stairs"))
							{
								text = "*Destroyed";
							}
						}
					}
					Cryobarrio2.MakeCryochamber(cellWithEmptyBorder.X, cellWithEmptyBorder.Y, Z, text, null);
					if (text == "*Destroyed")
					{
						Physics.LegacyApplyExplosion(cellWithEmptyBorder, new List<Cell>(), new List<GameObject>(), 35000, true, false, null, null, 1, 1f);
						for (int l = y - 1; l <= y + 1; l++)
						{
							for (int m = x - 1; m <= x + 1; m++)
							{
								if (5.in100())
								{
									Z.GetCell(m, l).AddObject("ConvalessencePuddle", null, null, null, null);
								}
							}
						}
						Z.GetCell(x - 2, y - 2).Clear(null, false, false, null);
						Z.GetCell(x + 2, y - 2).Clear(null, false, false, null);
						Z.GetCell(x - 2, y + 2).Clear(null, false, false, null);
						Z.GetCell(x + 2, y + 2).Clear(null, false, false, null);
						Z.GetCell(x - 2, y - 2).AddObject("CryochamberWallBroken", null, null, null, null);
						Z.GetCell(x + 2, y - 2).AddObject("CryochamberWallBroken", null, null, null, null);
						Z.GetCell(x - 2, y + 2).AddObject("CryochamberWallBroken", null, null, null, null);
						Z.GetCell(x + 2, y + 2).AddObject("CryochamberWallBroken", null, null, null, null);
					}
				}
			}
			return true;
		}

		// Token: 0x04003335 RID: 13109
		public int Chance = 20;

		// Token: 0x04003336 RID: 13110
		public int ExtraChancePerLevel = 10;

		// Token: 0x04003337 RID: 13111
		public int ChanceOfAdditionalTubes = 10;
	}
}
