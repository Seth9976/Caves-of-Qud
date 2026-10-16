using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Rules;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A8D RID: 2701
	public class QasQonLair : GirshLairMakerBase
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06006ECB RID: 28363 RVA: 0x00261BCB File Offset: 0x0025FDCB
		public override string SecretID
		{
			get
			{
				return "$qasqonlair";
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06006ECC RID: 28364 RVA: 0x00261BD2 File Offset: 0x0025FDD2
		public override string CradleDisplayName
		{
			get
			{
				return "The chuppah of Qas and Qon";
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06006ECD RID: 28365 RVA: 0x00261BD9 File Offset: 0x0025FDD9
		public override int DiscoveryXP
		{
			get
			{
				return 5000;
			}
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x00261BE0 File Offset: 0x0025FDE0
		public void BuildSurface(Zone Z)
		{
			List<Location2D> list = new List<Location2D>();
			List<Location2D> list2 = new List<Location2D>();
			Pitted.BuildPits(Z, 2, 4, 2, 5, 8, 2, list, list2, 10, 2, "DilutedWarmStaticPuddle", true, "CysticPit", "Pit", null, false);
			foreach (Location2D location2D in list2)
			{
				base.CreateCyst(Z, location2D, "BaseNephilimWall_QasQon", 6, 8, null, false, false);
			}
		}

		// Token: 0x06006ECF RID: 28367 RVA: 0x00261C6C File Offset: 0x0025FE6C
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

		// Token: 0x06006ED0 RID: 28368 RVA: 0x00261D78 File Offset: 0x0025FF78
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
				Pitted.BuildPits(Z, num2, num3, num4, num5, num6, num7, new List<Location2D>(), list3, z, 2, "DilutedWarmStaticPuddle", true, "CysticPit", "Pit", list2, false);
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
				base.CreateCyst(Z, location2D2, "BaseNephilimWall_QasQon", 6, 8, null, true, true);
			}
			foreach (Location2D location2D3 in list2)
			{
				base.CreateCyst(Z, location2D3, "BaseNephilimWall_QasQon", 8, 10, null, true, true);
			}
			List<Location2D> list4 = new List<Location2D>();
			foreach (Location2D location2D4 in list2)
			{
				foreach (Cell cell in Z.GetCell(location2D4).GetLocalAdjacentCellsCircular(4, false))
				{
					cell.ClearWalls();
					list4.Add(cell.Location);
				}
				ZoneBuilderSandbox.PlacePopulationInRegion(Z, list4, "QasQonCyst", null, null);
				list4.Clear();
			}
			if (this.Nephal && list2.Count > 0)
			{
				int num10 = list2.Count / 2;
				GameObject gameObject = Z.GetCell(list2[num10]).GetCellOrFirstConnectedSpawnLocation(true).AddObject("Qas", null, null, null, null);
				GameObject gameObject2 = Z.GetCell(list2[num10]).GetCellOrFirstConnectedSpawnLocation(true).AddObject("Qon", null, null, null, null);
				gameObject.ApplyEffect(new Asleep(9999, false, false, true, true), null);
				gameObject2.ApplyEffect(new Asleep(9999, false, false, true, true), null);
			}
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x00262050 File Offset: 0x00260250
		public override void MutateObject(GameObject Object)
		{
			int num = Stat.Random(1, 3);
			bool flag = num <= 2 && this.TryAddPolarized(Object);
			bool flag2 = num >= 2 && this.TryAddCryptic(Object);
			if (!flag)
			{
				if (flag2)
				{
					Object.AddPart<NephilimCultistIconColor>(new NephilimCultistIconColor("&b"), true, false);
					Object.RequirePart<DisplayNameAdjectives>(false).RequireAdjective("cryptic");
				}
				return;
			}
			if (flag2)
			{
				Object.AddPart<NephilimCultistIconColor>(new NephilimCultistIconColor("&C"), true, false);
				Object.RequirePart<DisplayNameAdjectives>(false).RequireAdjective("dazzling");
				return;
			}
			Object.AddPart<NephilimCultistIconColor>(new NephilimCultistIconColor("&c"), true, false);
			Object.RequirePart<DisplayNameAdjectives>(false).RequireAdjective("polarized");
		}

		// Token: 0x06006ED2 RID: 28370 RVA: 0x002620F8 File Offset: 0x002602F8
		public bool TryAddPolarized(GameObject Object)
		{
			if (Object.HasPart(typeof(ForcePylon)))
			{
				return false;
			}
			Object.AddPart<ForcePylon>(new ForcePylon
			{
				Walls = 2,
				Range = 5
			}, true, false);
			return true;
		}

		// Token: 0x06006ED3 RID: 28371 RVA: 0x0026212C File Offset: 0x0026032C
		public bool TryAddCryptic(GameObject Object)
		{
			if (Object.HasPart(typeof(ConfuseOnHit)))
			{
				return false;
			}
			Object.AddPart<ConfuseOnHit>(new ConfuseOnHit
			{
				Chance = 100
			}, true, false);
			if (!Object.HasPart(typeof(BurstOnDeath)))
			{
				Object.AddPart<BurstOnDeath>(new BurstOnDeath
				{
					Blueprint = "ConfusionGas200",
					SkipSolid = true
				}, true, false);
				if (!Object.HasPart(typeof(SoundOnDestroy)))
				{
					Object.AddPart<SoundOnDestroy>(new SoundOnDestroy
					{
						Sounds = "Sounds/Abilities/sfx_ability_gasMutation_passiveRelease",
						Volume = 0.7f
					}, true, false);
				}
			}
			return true;
		}

		// Token: 0x06006ED4 RID: 28372 RVA: 0x002621CC File Offset: 0x002603CC
		public void HoloZone(Zone Z)
		{
			NoiseMap noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 5, 4, 4, 20, 20, 4, 3, 0, 1, null);
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (noiseMap.Noise[j, i] > 1)
					{
						GameObject firstWall = Z.GetCell(j, i).GetFirstWall();
						if (firstWall != null && !firstWall.HasPart(typeof(HologramMaterial)) && !firstWall.HasPart(typeof(ConcealedHologramMaterial)))
						{
							firstWall.RequirePart<HologramMaterial>(false);
							firstWall.Physics.Solid = false;
						}
					}
				}
			}
		}

		// Token: 0x06006ED5 RID: 28373 RVA: 0x00262274 File Offset: 0x00260474
		public void HoloClear(GameObject Object)
		{
			if (Object.IsWall())
			{
				if (!Object.HasPart(typeof(HologramMaterial)) && !Object.HasPart(typeof(ConcealedHologramMaterial)))
				{
					Object.Physics.Solid = false;
					Object.RequirePart<ConcealedHologramMaterial>(false);
					return;
				}
			}
			else
			{
				Object.Obliterate(null, false, null);
			}
		}

		// Token: 0x06006ED6 RID: 28374 RVA: 0x002622CC File Offset: 0x002604CC
		public void HoloCyst(Cell Cell)
		{
			Cell.ClearWalls();
			Cell.RequireObject("BaseNephilimWall_QasQonHolo", null, null, null, null);
		}

		// Token: 0x06006ED7 RID: 28375 RVA: 0x002622E4 File Offset: 0x002604E4
		public override bool BuildLair(Zone Z)
		{
			if (Z.Z == 10)
			{
				this.BuildSurface(Z);
				return true;
			}
			this.BuildChuppah(Z);
			ZoneTemplateManager.Templates["QasQonCradle"].Execute(Z, null);
			this.HoloZone(Z);
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, true, 0.5f, new Action<GameObject>(this.HoloClear));
			return true;
		}

		// Token: 0x040032B9 RID: 12985
		public const string SECRET_ID = "$qasqonlair";

		// Token: 0x040032BA RID: 12986
		public bool Nephal;
	}
}
