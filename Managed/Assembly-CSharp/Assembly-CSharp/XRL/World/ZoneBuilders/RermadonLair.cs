using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Rules;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A8E RID: 2702
	public class RermadonLair : GirshLairMakerBase
	{
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06006ED9 RID: 28377 RVA: 0x0026234A File Offset: 0x0026054A
		public override string SecretID
		{
			get
			{
				return "$rermadonlair";
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06006EDA RID: 28378 RVA: 0x00262351 File Offset: 0x00260551
		public override string CradleDisplayName
		{
			get
			{
				return "The cradle of Rermadon";
			}
		}

		// Token: 0x06006EDB RID: 28379 RVA: 0x00262358 File Offset: 0x00260558
		public void BuildSurface(Zone Z)
		{
			List<Location2D> list = new List<Location2D>();
			List<Location2D> list2 = new List<Location2D>();
			Pitted.BuildPits(Z, 2, 4, 2, 5, 8, 2, list, list2, 10, 2, "AlgalWaterDeepPool", true, "CysticPit", "Pit", null, false);
			foreach (Location2D location2D in list2)
			{
				base.CreateCyst(Z, location2D, "BaseNephilimWall_Rermadon", 6, 8, null, false, false);
			}
		}

		// Token: 0x06006EDC RID: 28380 RVA: 0x002623E4 File Offset: 0x002605E4
		public void PopulateCysticPitConnections(Zone Z, List<Location2D> Pits)
		{
			foreach (ZoneConnection zoneConnection in Z.EnumerateConnections())
			{
				if (zoneConnection.Type == "CysticPit")
				{
					Pits.Add(zoneConnection.Loc2D);
				}
			}
			if (Pits.IsNullOrEmpty<Location2D>() && Z.Z > 10)
			{
				Zone zoneFromDirection = Z.GetZoneFromDirection("U");
				if (!zoneFromDirection.Built && !zoneFromDirection.ZoneConnectionCache.IsNullOrEmpty<CachedZoneConnection>())
				{
					foreach (CachedZoneConnection cachedZoneConnection in zoneFromDirection.ZoneConnectionCache)
					{
						if (cachedZoneConnection.TargetDirection == "d" && cachedZoneConnection.Type == "CysticPit")
						{
							Pits.Add(cachedZoneConnection.Loc2D);
						}
					}
				}
			}
		}

		// Token: 0x06006EDD RID: 28381 RVA: 0x002624F0 File Offset: 0x002606F0
		public void BuildChuppah(Zone Z)
		{
			int num = Math.Max(Z.Z - 10, 0);
			List<Location2D> list = new List<Location2D>();
			List<Location2D> list2 = new List<Location2D>();
			this.PopulateCysticPitConnections(Z, list2);
			if (!this.Nephal)
			{
				List<Location2D> list3 = new List<Location2D>();
				int num2 = 2 + num;
				int num3 = 4 + num * 2;
				int num4 = 2;
				int num5 = 5;
				int num6 = 8;
				int num7 = 2;
				int z = Z.Z;
				Pitted.BuildPits(Z, num2, num3, num4, num5, num6, num7, new List<Location2D>(), list3, z, 2, "AlgalWaterDeepPool", true, "CysticPit", "Pit", list2, false);
				list.AddRange(list3);
			}
			int num8 = 2 + Stat.Random(0, 1) + num;
			Location2D location2D = (list.LastOrDefault<Location2D>() ?? Location2D.Get(10, 12)).Wiggle(2, 2);
			int num9 = list2.Count;
			while (num9 < num8 && !(location2D == null))
			{
				list2.Add(location2D);
				location2D += Location2D.Get(6, 0);
				if (location2D == null)
				{
					break;
				}
				location2D = location2D.Wiggle(3, 3);
				if (location2D == null)
				{
					break;
				}
				num9++;
			}
			foreach (Location2D location2D2 in list)
			{
				base.CreateCyst(Z, location2D2, "BaseNephilimWall_Rermadon", 6, 8, null, true, true);
			}
			foreach (Location2D location2D3 in list2)
			{
				base.CreateCyst(Z, location2D3, "BaseNephilimWall_Rermadon", 8, 10, null, true, true);
			}
			List<Location2D> list4 = new List<Location2D>();
			foreach (Location2D location2D4 in list2)
			{
				foreach (Cell cell in Z.GetCell(location2D4).GetLocalAdjacentCellsCircular(4, false))
				{
					cell.ClearWalls();
					list4.Add(cell.Location);
				}
				ZoneBuilderSandbox.PlacePopulationInRegion(Z, list4, "RermadonCyst", null, null);
				list4.Clear();
			}
			if (this.Nephal && list2.Count > 0)
			{
				int num10 = list2.Count / 2;
				Z.GetCell(list2[num10]).GetCellOrFirstConnectedSpawnLocation(true).AddObject("Rermadon", null, null, null, null)
					.ApplyEffect(new Asleep(9999, false, false, true, true), null);
			}
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x0026278C File Offset: 0x0026098C
		public override void MutateObject(GameObject Object)
		{
			Corpse corpse;
			if (Object.TryGetPart<Corpse>(out corpse))
			{
				if (corpse.CorpseBlueprint == "Plasma1000")
				{
					return;
				}
			}
			else
			{
				Object.AddPart<Corpse>(corpse = new Corpse(), true, false);
			}
			corpse.CorpseBlueprint = "Plasma1000";
			corpse.CorpseChance = 100;
			Object.AddPart<NephilimCultistIconColor>(new NephilimCultistIconColor("&G"), true, false);
			Object.RequirePart<DisplayNameAdjectives>(false).RequireAdjective("plasmatic");
		}

		// Token: 0x06006EDF RID: 28383 RVA: 0x00262800 File Offset: 0x00260A00
		public override bool BuildLair(Zone Z)
		{
			if (Z.Z == 10)
			{
				this.BuildSurface(Z);
				return true;
			}
			this.BuildChuppah(Z);
			ZoneTemplateManager.Templates["RermadonCradle"].Execute(Z, null);
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, true, 0.5f, null);
			return true;
		}

		// Token: 0x040032BB RID: 12987
		public const string SECRET_ID = "$rermadonlair";

		// Token: 0x040032BC RID: 12988
		public bool Nephal;
	}
}
