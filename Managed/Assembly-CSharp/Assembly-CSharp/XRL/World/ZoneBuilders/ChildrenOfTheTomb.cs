using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AFE RID: 2814
	public class ChildrenOfTheTomb : ZoneBuilderSandbox
	{
		// Token: 0x06007083 RID: 28803 RVA: 0x00283534 File Offset: 0x00281734
		public void BuildZone(Zone zone)
		{
			The.Game.RequireGameState<Dictionary<int, string>>("ChildrenOfTheTombZoneCache", () => new Dictionary<int, string>());
			List<string> list = new List<string>();
			list.Add(string.Concat(new string[]
			{
				zone.ZoneWorld,
				".",
				zone.wX.ToString(),
				".",
				zone.wY.ToString(),
				".0.0.",
				zone.Z.ToString()
			}));
			list.Add(string.Concat(new string[]
			{
				zone.ZoneWorld,
				".",
				zone.wX.ToString(),
				".",
				zone.wY.ToString(),
				".1.0.",
				zone.Z.ToString()
			}));
			list.Add(string.Concat(new string[]
			{
				zone.ZoneWorld,
				".",
				zone.wX.ToString(),
				".",
				zone.wY.ToString(),
				".2.0.",
				zone.Z.ToString()
			}));
			list.Add(string.Concat(new string[]
			{
				zone.ZoneWorld,
				".",
				zone.wX.ToString(),
				".",
				zone.wY.ToString(),
				".0.1.",
				zone.Z.ToString()
			}));
			list.Add(string.Concat(new string[]
			{
				zone.ZoneWorld,
				".",
				zone.wX.ToString(),
				".",
				zone.wY.ToString(),
				".2.1.",
				zone.Z.ToString()
			}));
			list.Add(string.Concat(new string[]
			{
				zone.ZoneWorld,
				".",
				zone.wX.ToString(),
				".",
				zone.wY.ToString(),
				".0.2.",
				zone.Z.ToString()
			}));
			list.Add(string.Concat(new string[]
			{
				zone.ZoneWorld,
				".",
				zone.wX.ToString(),
				".",
				zone.wY.ToString(),
				".1.2.",
				zone.Z.ToString()
			}));
			list.Add(string.Concat(new string[]
			{
				zone.ZoneWorld,
				".",
				zone.wX.ToString(),
				".",
				zone.wY.ToString(),
				".2.2.",
				zone.Z.ToString()
			}));
			List<string> list2 = The.Game.RequireGameState<List<string>>("ListOfUnplacedChildren", () => new List<string> { "Nacham,Doyoba", "Vaam,Dadogom", "Dagasha,Yona", "Kah,Gyamyo" });
			if (base.GetOracleListEntryFromInt<string>(zone.Z, list) == zone.ZoneID)
			{
				string text = list2.RemoveRandomElement<string>(null);
				zone.GetSpawnCell().AddObject(text.Split(',', StringSplitOptions.None)[0], null, null, null, null);
				Cell cell = zone.GetCellsWithObject("AnchorRoomTile").GetRandomElement<Cell>(null);
				if (cell == null)
				{
					cell = zone.GetSpawnCell();
				}
				cell.AddObject(text.Split(',', StringSplitOptions.None)[1], null, null, null, null);
			}
		}
	}
}
