using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A8B RID: 2699
	public class BethsaidaColumn : GirshLairMakerBase
	{
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06006EA5 RID: 28325 RVA: 0x0025F636 File Offset: 0x0025D836
		public int CradleLevel
		{
			get
			{
				return ZoneBuilderSandbox.GetOracleIntColumn(this.Zone, 40, 45);
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06006EA6 RID: 28326 RVA: 0x0025F647 File Offset: 0x0025D847
		public override string SecretID
		{
			get
			{
				return "$bethsaidalair";
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06006EA7 RID: 28327 RVA: 0x0025F64E File Offset: 0x0025D84E
		public override string CradleDisplayName
		{
			get
			{
				return "The cradle of Bethsaida";
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06006EA8 RID: 28328 RVA: 0x0025F655 File Offset: 0x0025D855
		public override bool IsCradle
		{
			get
			{
				return this.Zone.Z <= this.CradleLevel && this.Zone.Z > this.CradleLevel - 3;
			}
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x0025F684 File Offset: 0x0025D884
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
				Liquid = "SludgePuddle"
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
									cell.RequireObject("BaseNephilimWall_Bethsaida", null, null, null, null);
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
					Liquid = "ConvalessenceDeepPool"
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
			ZoneTemplateManager.Templates["BethsaidaColumn"].Execute(Z, null);
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
				ZoneBuilderSandbox.PlacePopulationInRegion(Z, list4, "BethsaidaCyst", null, null);
			}
			if (Z.Z == this.CradleLevel && list.Count > 0)
			{
				int num9 = list.Count / 2;
				Z.GetCell(list[num9]).GetCellOrFirstConnectedSpawnLocation(true).AddObject("Bethsaida", null, null, null, null);
			}
			if (Z.Z != this.CradleLevel && list2.Count > 0)
			{
				List<GameObject> objects = Z.GetObjects((GameObject o) => o.HasPart<LiquidVolume>() && o.GetPart<LiquidVolume>().IsOpenVolume());
				int k = 0;
				int num10 = Stat.Random(0, 4);
				while (k < num10)
				{
					using (Pathfinder pathfinder = Z.getPathfinder(null))
					{
						GameObject randomElement = objects.GetRandomElement<GameObject>(null);
						string text = ((randomElement != null) ? randomElement.Blueprint : null);
						Cell currentCell = randomElement.Physics.CurrentCell;
						if (randomElement != null && currentCell != null && pathfinder.FindPath(currentCell.Location, list2.GetRandomElement<Location2D>(null), false, true, 24300, false))
						{
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								Cell cell3 = Z.GetCell(pathfinderNode.X, pathfinderNode.Y);
								GameObject gameObject = GameObject.Create(text);
								cell3.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
					}
					k++;
				}
			}
			return flag;
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x0025FBA4 File Offset: 0x0025DDA4
		public override void MutateObject(GameObject Object)
		{
			if (!Object.HasPart(typeof(SplitOnDeath)))
			{
				Object.AddPart<SplitOnDeath>(new SplitOnDeath
				{
					Blueprint = Object.Blueprint,
					Amount = "2",
					Message = "=subject.T= =verb:split= in two!"
				}, true, false);
				Object.AddPart<NephilimCultistIconColor>(new NephilimCultistIconColor("&W"), true, false);
				Object.RequirePart<DisplayNameAdjectives>(false).RequireAdjective("conjoined");
			}
		}

		// Token: 0x040032A7 RID: 12967
		public const string SECRET_ID = "$bethsaidalair";

		// Token: 0x040032A8 RID: 12968
		public const int Z_LOW = 40;

		// Token: 0x040032A9 RID: 12969
		public const int Z_HIGH = 45;

		// Token: 0x040032AA RID: 12970
		public const int DEPTH = 3;
	}
}
