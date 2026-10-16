using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A96 RID: 2710
	public class ShugLair : GirshLairMakerBase
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06006EF0 RID: 28400 RVA: 0x002631CC File Offset: 0x002613CC
		public override string SecretID
		{
			get
			{
				return "$shugruithlair";
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06006EF1 RID: 28401 RVA: 0x002631D3 File Offset: 0x002613D3
		public override string CradleDisplayName
		{
			get
			{
				return "The cradle of Shug'ruith";
			}
		}

		// Token: 0x06006EF2 RID: 28402 RVA: 0x002631DC File Offset: 0x002613DC
		public override bool BuildLair(Zone Z)
		{
			Pitted pitted = new Pitted();
			pitted.MinWells = 1;
			pitted.MaxWells = 1;
			pitted.MinRadius = 2;
			pitted.MaxRadius = 5;
			pitted.XMargin = 8;
			pitted.PitTop = Z.Z;
			pitted.PitDepth = 2;
			pitted.PitDetailsRandom = true;
			pitted.Liquid = "SludgePuddle";
			bool flag = true;
			List<Location2D> list = new List<Location2D>();
			int num = this.l + 1;
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
								cell.RequireObject("BaseNephilimWall_Shug'rith", null, null, null, null);
							}
							if (num8 < 3)
							{
								cell.ClearWalls();
								cell.RequireObject("BaseNephilimWall_Shug'rith", null, null, null, null);
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
			Pitted pitted2 = new Pitted
			{
				MinWells = 1,
				MaxWells = 1,
				MinRadius = 2,
				MaxRadius = 5,
				XMargin = 8,
				PitTop = Z.Z,
				PitDepth = 1,
				Liquid = "SludgePuddle"
			};
			if (this.l < 3)
			{
				List<Location2D> list2 = new List<Location2D>();
				List<Location2D> list3 = new List<Location2D>();
				pitted2.BuildZone(Z, list2, list3);
			}
			ZoneTemplateManager.Templates["ShugruithTunnel"].Execute(Z, null);
			Z.FireEvent("FirmPitEdges");
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, true, 0.5f, null);
			foreach (Location2D location2D2 in list)
			{
				List<Location2D> list4 = new List<Location2D>();
				foreach (Cell cell2 in Z.GetCell(location2D2).GetLocalAdjacentCellsCircular(3, true))
				{
					cell2.ClearWalls();
					list4.Add(cell2.Location);
				}
				ZoneBuilderSandbox.PlacePopulationInRegion(Z, list4, "ShugruithCyst", null, null);
			}
			if (this.l == 3 && list.Count > 0)
			{
				int num9 = list.Count / 2;
				Z.GetCell(list[num9]).AddObject("Shugruith", null, null, null, null);
			}
			return flag;
		}

		// Token: 0x06006EF3 RID: 28403 RVA: 0x00263544 File Offset: 0x00261744
		public override void MutateObject(GameObject Object)
		{
			if (!Object.HasPart(typeof(Burrowing)))
			{
				Object.RequirePart<Mutations>(false).AddMutation("Burrowing", 5);
				Object.AddPart<LeaveTrailWhileHasEffect>(new LeaveTrailWhileHasEffect
				{
					Specification = "BaseNephilimWall_Shug'rith"
				}, true, false);
				Object.AddPart<NephilimCultistIconColor>(new NephilimCultistIconColor("&w"), true, false);
				Object.RequirePart<DisplayNameAdjectives>(false).RequireAdjective("burrowing");
			}
		}

		// Token: 0x040032C0 RID: 12992
		public const string SECRET_ID = "$shugruithlair";

		// Token: 0x040032C1 RID: 12993
		public int l;
	}
}
