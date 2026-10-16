using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A8A RID: 2698
	public class AgolgotColumn : GirshLairMakerBase
	{
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06006E9E RID: 28318 RVA: 0x0025EF81 File Offset: 0x0025D181
		public int CradleLevel
		{
			get
			{
				return ZoneBuilderSandbox.GetOracleIntColumn(this.Zone, 40, 45);
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06006E9F RID: 28319 RVA: 0x0025EF92 File Offset: 0x0025D192
		public override string SecretID
		{
			get
			{
				return "$agolgotlair";
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06006EA0 RID: 28320 RVA: 0x0025EF99 File Offset: 0x0025D199
		public override string CradleDisplayName
		{
			get
			{
				return "The cradle of Agolgot";
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06006EA1 RID: 28321 RVA: 0x0025EFA0 File Offset: 0x0025D1A0
		public override bool IsCradle
		{
			get
			{
				return this.Zone.Z <= this.CradleLevel && this.Zone.Z > this.CradleLevel - 3;
			}
		}

		// Token: 0x06006EA2 RID: 28322 RVA: 0x0025EFCC File Offset: 0x0025D1CC
		public override bool BuildLair(Zone Z)
		{
			Pitted pitted = new Pitted
			{
				MinWells = 1,
				MaxWells = 3,
				MinRadius = 2,
				MaxRadius = 5,
				XMargin = 8,
				PitTop = Z.Z,
				PitDepth = 2,
				PitDetailsRandom = true,
				Liquid = "SludgePuddle",
				Lazy = (Z.Z < this.CradleLevel)
			};
			bool isCradle = this.IsCradle;
			bool flag = true;
			List<Location2D> list = new List<Location2D>();
			if (isCradle)
			{
				int num = 2 + Stat.Random(0, 1) + (3 - (this.CradleLevel - Z.Z));
				Location2D location2D = Location2D.Get(10, 12).Wiggle(2, 2);
				int num2 = 0;
				while (num2 < num && !(location2D == null))
				{
					list.Add(location2D);
					int num3 = Stat.Random(6, 8);
					float num4 = (float)Stat.Random(80, 90) / 75f;
					float num5 = (float)Stat.Random(80, 90) / 75f;
					int num6 = (int)((float)num3 * num4);
					int num7 = (int)((float)num3 * num5);
					for (int i = -num6; i <= num6; i++)
					{
						for (int j = -num7; j <= num7; j++)
						{
							Cell cell = Z.GetCell(location2D.X + i, location2D.Y + j);
							if (cell != null)
							{
								int num8 = (int)Math.Sqrt((double)(num4 * (float)Math.Abs(i) * (num4 * (float)Math.Abs(i)) + (float)Math.Abs(j) * num5 * ((float)Math.Abs(j) * num5)));
								if (num8 <= num3 && num8 >= num3 - 2)
								{
									cell.ClearWalls();
									cell.RequireObject("BaseNephilimWall_Agolgot", null, null, null, null);
								}
								if (num8 < 3)
								{
									cell.ClearWalls();
								}
							}
						}
					}
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
					num2++;
				}
				pitted = new Pitted
				{
					MinWells = 8,
					MaxWells = 16,
					MinRadius = 2,
					MaxRadius = 5,
					XMargin = 8,
					PitTop = this.CradleLevel - 3,
					PitDepth = 2,
					Liquid = "SludgePuddle",
					Lazy = true
				};
			}
			if (Z.Z > this.CradleLevel)
			{
				return true;
			}
			List<Location2D> list2 = new List<Location2D>();
			List<Location2D> list3 = new List<Location2D>();
			if (isCradle || Stat.Random(1, 100) <= 70)
			{
				pitted.BuildZone(Z, list2, list3);
			}
			else
			{
				new StairsDown().BuildZone(Z);
			}
			if (Z.Z == this.CradleLevel - 1 && list3.Count > 0)
			{
				ZoneBuilderSandbox.BridgeOver(Z, list2.GetRandomElement<Location2D>(null), ZoneBuilderSandbox.BridgeDirection.Random, "Pit");
			}
			ZoneTemplateManager.Templates["AgolgotColumn"].Execute(Z, null);
			Z.FireEvent("FirmPitEdges");
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, true, 0.5f, null);
			foreach (Location2D location2D2 in list)
			{
				List<Location2D> list4 = new List<Location2D>();
				foreach (Cell cell2 in Z.GetCell(location2D2).GetLocalAdjacentCellsCircular(3, false))
				{
					cell2.ClearWalls();
					list4.Add(cell2.Location);
				}
				ZoneBuilderSandbox.PlacePopulationInRegion(Z, list4, "AgolgotCyst", null, null);
			}
			if (Z.Z == this.CradleLevel && list.Count > 0)
			{
				int num9 = list.Count / 2;
				Z.GetCell(list[num9]).GetCellOrFirstConnectedSpawnLocation(true).AddObject("Agolgot", null, null, null, null);
			}
			if (Z.Z != this.CradleLevel && list2.Count > 0)
			{
				List<GameObject> objects = Z.GetObjects((GameObject o) => o.IsOpenLiquidVolume());
				int k = 0;
				int num10 = Stat.Random(0, 4);
				while (k < num10)
				{
					GameObject randomElement = objects.GetRandomElement<GameObject>(null);
					if (randomElement == null)
					{
						break;
					}
					using (Pathfinder pathfinder = Z.getPathfinder(null))
					{
						Cell currentCell = randomElement.CurrentCell;
						if (currentCell != null && pathfinder.FindPath(currentCell.Location, list2.GetRandomElement<Location2D>(null), false, true, 24300, false))
						{
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								Cell cell3 = Z.GetCell(pathfinderNode.X, pathfinderNode.Y);
								GameObject gameObject = GameObject.Create(randomElement.Blueprint);
								cell3.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
					}
					k++;
				}
			}
			int num11 = 20;
			for (int l = 0; l < Z.Height; l++)
			{
				for (int m = 0; m < Z.Width; m++)
				{
					GameObject openLiquidVolume = Z.GetCell(m, l).GetOpenLiquidVolume();
					LiquidVolume liquidVolume = ((openLiquidVolume != null) ? openLiquidVolume.LiquidVolume : null);
					if (liquidVolume != null && liquidVolume.Volume > 500 && num11.in100() && liquidVolume.IsWaste() && liquidVolume.IsOpenVolume())
					{
						Z.GetCell(m, l).AddObject("EelSpawn", null, null, null, null);
					}
				}
			}
			return flag;
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x0025F590 File Offset: 0x0025D790
		public override void MutateObject(GameObject Object)
		{
			if (!Object.HasPart(typeof(PoisonOnHit)))
			{
				Corpse corpse = Object.RequirePart<Corpse>(false);
				corpse.CorpseBlueprint = "RankPool";
				corpse.CorpseChance = 100;
				Object.AddPart<PoisonOnHit>(new PoisonOnHit
				{
					Chance = 50
				}, true, false);
				Object.AddPart<DiseaseOnHit>(new DiseaseOnHit
				{
					Chance = 50
				}, true, false);
				Object.AddPart<RustOnHit>(new RustOnHit
				{
					Chance = 50
				}, true, false);
				Object.AddPart<NephilimCultistIconColor>(new NephilimCultistIconColor("&g"), true, false);
				Object.RequirePart<DisplayNameAdjectives>(false).RequireAdjective("rank");
			}
		}

		// Token: 0x040032A3 RID: 12963
		public const string SECRET_ID = "$agolgotlair";

		// Token: 0x040032A4 RID: 12964
		public const int Z_LOW = 40;

		// Token: 0x040032A5 RID: 12965
		public const int Z_HIGH = 45;

		// Token: 0x040032A6 RID: 12966
		public const int DEPTH = 3;
	}
}
