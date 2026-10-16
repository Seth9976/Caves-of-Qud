using System;
using System.Collections.Generic;
using Genkit;
using HistoryKit;
using UnityEngine;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A8C RID: 2700
	public abstract class GirshLairMakerBase : ZoneBuilderSandbox
	{
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06006EAC RID: 28332
		public abstract string SecretID { get; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06006EAD RID: 28333
		public abstract string CradleDisplayName { get; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06006EAE RID: 28334 RVA: 0x0025FC1F File Offset: 0x0025DE1F
		public virtual bool IsCradle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06006EAF RID: 28335 RVA: 0x0025FC22 File Offset: 0x0025DE22
		public virtual int DiscoveryXP
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06006EB0 RID: 28336 RVA: 0x0025FC29 File Offset: 0x0025DE29
		public virtual int MutateChance
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x06006EB1 RID: 28337 RVA: 0x0025FC30 File Offset: 0x0025DE30
		public virtual bool BuildZone(Zone Z)
		{
			this.Zone = Z;
			bool isCradle = this.IsCradle;
			if (isCradle)
			{
				this.SetCradleProperties();
				this.AddLocationFinder();
			}
			if (!this.BuildLair(Z))
			{
				return false;
			}
			if (isCradle)
			{
				foreach (GameObject gameObject in Z.IterateObjects())
				{
					if (gameObject.IsCombatObject() && !gameObject.HasProperName && !gameObject.IsPlayerControlled() && !gameObject.IsFactionMember("Girsh") && Stat.Random(1, 100) <= this.MutateChance)
					{
						this.MutateObject(gameObject);
						gameObject.Brain.Allegiance["Girsh"] = 99;
					}
				}
				this.AddHolyPlace();
			}
			return true;
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x0025FCE4 File Offset: 0x0025DEE4
		public virtual void SetCradleProperties()
		{
			this.Zone.SetMusic("Music/of Chrome and How");
			this.Zone.IncludeContextInZoneDisplay = true;
			this.Zone.HasProperName = true;
			this.Zone.BaseDisplayName = this.CradleDisplayName;
		}

		// Token: 0x06006EB3 RID: 28339 RVA: 0x0025FD20 File Offset: 0x0025DF20
		public virtual void AddLocationFinder()
		{
			LocationFinder locationFinder;
			if (this.Zone.GetCell(0, 0).AddObject("LocationFinder", null, null, null, null).TryGetPart<LocationFinder>(out locationFinder))
			{
				locationFinder.ID = this.SecretID;
				locationFinder.Value = this.DiscoveryXP;
			}
		}

		// Token: 0x06006EB4 RID: 28340 RVA: 0x0025FD69 File Offset: 0x0025DF69
		public virtual void AddHolyPlace()
		{
			this.Zone.GetCell(0, 0).RequireObject("HolyPlaceNephilimWidget", null, null, null, null);
		}

		// Token: 0x06006EB5 RID: 28341
		public abstract bool BuildLair(Zone Z);

		// Token: 0x06006EB6 RID: 28342
		public abstract void MutateObject(GameObject Object);

		// Token: 0x06006EB7 RID: 28343 RVA: 0x0025FD87 File Offset: 0x0025DF87
		public int clamp(int n, int low, int high)
		{
			if (n < low)
			{
				return low;
			}
			if (n > high)
			{
				return high;
			}
			return n;
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x0025FD98 File Offset: 0x0025DF98
		public void partition(Rect2D rect, ref int nSegments, List<ISultanDungeonSegment> segments)
		{
			if (nSegments <= 0)
			{
				segments.Add(new SultanRectDungeonSegment(rect));
				return;
			}
			if (Stat.Random(0, 1) == 0)
			{
				int num = rect.y2 - rect.y1;
				int num2 = (rect.y2 - rect.y1) / 2;
				int num3 = (rect.y2 - rect.y1) / 8;
				int num4 = this.clamp((int)Stat.GaussianRandom(0f, (float)num3) + num2, rect.y1 + 4, rect.y2 - 4);
				if (num <= 8)
				{
					segments.Add(new SultanRectDungeonSegment(rect));
					return;
				}
				nSegments--;
				Rect2D rect2D = new Rect2D(rect.x1, rect.y1, rect.x2, num4);
				this.partition(rect2D, ref nSegments, segments);
				nSegments--;
				Rect2D rect2D2 = new Rect2D(rect.x1, num4 + 1, rect.x2, rect.y2);
				this.partition(rect2D2, ref nSegments, segments);
				return;
			}
			else
			{
				int num5 = rect.x2 - rect.x1;
				int num6 = (rect.x2 - rect.x1) / 2;
				int num7 = (rect.x2 - rect.x1) / 8;
				int num8 = this.clamp((int)Stat.GaussianRandom(0f, (float)num7) + num6, rect.x1 + 4, rect.x2 - 4);
				if (num5 <= 16)
				{
					segments.Add(new SultanRectDungeonSegment(rect));
					return;
				}
				nSegments--;
				Rect2D rect2D3 = new Rect2D(rect.x1, rect.y1, num8, rect.y2);
				this.partition(rect2D3, ref nSegments, segments);
				nSegments--;
				Rect2D rect2D4 = new Rect2D(num8 + 1, rect.y1, rect.x2, rect.y2);
				this.partition(rect2D4, ref nSegments, segments);
				return;
			}
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x0025FF40 File Offset: 0x0025E140
		public string ResolveOneObjectFromArgs(List<string> arg, string def = null, int tier = 1)
		{
			if (arg == null || arg.Count == 0)
			{
				return def;
			}
			string randomElement = arg.GetRandomElement<string>(null);
			if (randomElement.Length == 0)
			{
				return def;
			}
			if (randomElement[0] == '$')
			{
				return PopulationManager.RollOneFrom(randomElement.Substring(1), new Dictionary<string, string> { 
				{
					"zonetier",
					tier.ToString()
				} }, "SmallBoulder").Blueprint;
			}
			return randomElement;
		}

		// Token: 0x06006EBA RID: 28346 RVA: 0x0025FFA6 File Offset: 0x0025E1A6
		public List<PopulationResult> ResolveOnePopulationFromArgs(List<string> arg, string def = null, int Tier = 1)
		{
			if (arg == null || arg.Count == 0)
			{
				return PopulationManager.Generate(def, null, null, null, null, null);
			}
			return PopulationManager.Generate(arg.GetRandomElement<string>(null), "zonetier", Tier.ToString(), null, null, null);
		}

		// Token: 0x06006EBB RID: 28347 RVA: 0x0025FFDA File Offset: 0x0025E1DA
		public string ResolveOneTableFromArgs(List<string> arg, string def = null)
		{
			if (arg == null || arg.Count == 0)
			{
				return def;
			}
			return arg.GetRandomElement<string>(null);
		}

		// Token: 0x06006EBC RID: 28348 RVA: 0x0025FFF0 File Offset: 0x0025E1F0
		public bool BuildRandomZoneWithArgs(Zone Z, int relicChance, bool bBuildSurface, string[] args, string stairs, string monstertable = null, string tablePrefix = "SultanDungeons_")
		{
			SultanDungeonArgs sultanDungeonArgs = new SultanDungeonArgs();
			this.monstertable = monstertable;
			this.stairs = stairs;
			HistoricEntitySnapshot historicEntitySnapshot = new HistoricEntitySnapshot(null);
			historicEntitySnapshot.properties.Clear();
			for (int i = 0; i < args.Length; i++)
			{
				historicEntitySnapshot.properties.Add("additionalArg" + Guid.NewGuid().ToString(), args[i]);
			}
			sultanDungeonArgs.UpdateFromEntity(historicEntitySnapshot, tablePrefix);
			bool flag = this.BuildZoneFromArgs(Z, sultanDungeonArgs, false, true);
			if (flag && Stat.Random(1, 100) <= relicChance)
			{
				new PlaceRelicBuilder().BuildZoneWithRelic(Z, RelicGenerator.GenerateRelic(historicEntitySnapshot, Z.NewTier, null, true));
			}
			return flag;
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x0026009B File Offset: 0x0025E29B
		public void LogMem(string step)
		{
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x002600A0 File Offset: 0x0025E2A0
		public bool PointInSegments(List<ISultanDungeonSegment> segments, int x, int y)
		{
			for (int i = 0; i < segments.Count; i++)
			{
				if (segments[i].contains(Location2D.Get(x, y)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x002600D8 File Offset: 0x0025E2D8
		public GameObject sultanDungeonSemanticCreate(string blueprint)
		{
			if (string.IsNullOrEmpty(blueprint) || blueprint[0] != '*')
			{
				return GameObject.Create(blueprint);
			}
			int num = 0;
			string text = "DynamicSemanticTable:" + blueprint.Substring(1) + "::" + this.targetTier.ToString();
			if (!GirshLairMakerBase.staticPerDungeon.TryGetValue(blueprint, out num) || Stat.Random(1, 100) <= num)
			{
				return GameObject.Create(PopulationManager.RollOneFrom(text, null, null).Blueprint);
			}
			if (!base.HasZoneColumnValue(this.targetZone, "sultandungeon_semantic_result_" + blueprint))
			{
				string blueprint2 = PopulationManager.RollOneFrom(text, null, null).Blueprint;
				base.SetZoneColumnValue(this.targetZone, "sultandungeon_semantic_result_" + blueprint, blueprint2);
				return GameObject.Create(blueprint2);
			}
			return GameObject.Create(base.GetZoneColumnValue(this.targetZone, "sultandungeon_semantic_result_" + blueprint, null));
		}

		// Token: 0x06006EC0 RID: 28352 RVA: 0x002601BC File Offset: 0x0025E3BC
		public bool BuildZoneFromArgs(Zone Z, SultanDungeonArgs regionArgs, bool AddSurface, bool Clear = true)
		{
			this.targetZone = Z;
			if (this.targetZone != null)
			{
				this.targetTier = this.targetZone.NewTier;
			}
			if (Z.Z > 10)
			{
				Z.AmbientBed = "Sounds/Ambiences/amb_bed_dungeon";
			}
			this.LogMem("step1");
			string text = "*Default";
			string tablePrefix = regionArgs.tablePrefix;
			string text2 = tablePrefix + "Floor_" + text;
			PopulationResult populationResult = PopulationManager.RollOneFrom(text2, null, "Rocky");
			if (populationResult == null)
			{
				throw new Exception("Got no floor blueprint result from " + text2);
			}
			string blueprint = populationResult.Blueprint;
			HistoricEntity randomElement = The.Game.sultanHistory.GetEntitiesWherePropertyEquals("name", this.locationName).entities.GetRandomElement<HistoricEntity>(null);
			if (randomElement != null)
			{
				randomElement.GetCurrentSnapshot();
			}
			SultanDungeonArgs sultanDungeonArgs = regionArgs.clone();
			string randomElement2 = regionArgs.segmentations.GetRandomElement<string>(null);
			string text3 = this.ResolveOneTableFromArgs(sultanDungeonArgs.greenObjects, null);
			string text4 = this.ResolveOneTableFromArgs(sultanDungeonArgs.yellowObjects, null);
			string text5 = this.ResolveOneTableFromArgs(sultanDungeonArgs.blueObjects, null);
			string text6;
			if (text5 == null)
			{
				text6 = "SaltyWaterPuddle";
			}
			else
			{
				PopulationResult populationResult2 = PopulationManager.RollOneFrom(text5, null, "SaltyWaterPuddle");
				if (populationResult2 == null)
				{
					throw new Exception("Got no blue object result from " + text5);
				}
				text6 = populationResult2.Blueprint;
			}
			List<ISultanDungeonSegment> list = new List<ISultanDungeonSegment>();
			int num = 7;
			int num2 = 72;
			int num3 = 7;
			int num4 = 17;
			foreach (string text7 in randomElement2.Split(';', StringSplitOptions.None))
			{
				if (text7 == "Full")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
				}
				else if (text7.StartsWith("BSP:"))
				{
					int num5 = Convert.ToInt32(text7.Split(':', StringSplitOptions.None)[1]);
					this.partition(new Rect2D(2, 2, 78, 24), ref num5, list);
				}
				else if (text7.StartsWith("Ring:"))
				{
					int num6 = Convert.ToInt32(text7.Split(':', StringSplitOptions.None)[1]);
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
					if (num6 == 2)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(20, 8, 60, 16)));
					}
					if (num6 == 3)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(15, 8, 65, 16)));
						list.Add(new SultanRectDungeonSegment(new Rect2D(25, 10, 55, 14)));
					}
				}
				else if (text7.StartsWith("Blocks"))
				{
					string[] array2 = text7.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					int num7 = array2[0].RollCached();
					for (int j = 0; j < num7; j++)
					{
						int num8 = array2[1].RollCached();
						int num9 = array2[2].RollCached();
						int num10 = Stat.Random(2, 78 - num8);
						int num11 = Stat.Random(2, 23 - num9);
						int num12 = num10 + num8;
						int num13 = num11 + num9;
						if (num < num10)
						{
							num = num10;
						}
						if (num2 > num12)
						{
							num2 = num12;
						}
						if (num3 < num11)
						{
							num3 = num11;
						}
						if (num4 > num13)
						{
							num4 = num13;
						}
						list.Add(new SultanRectDungeonSegment(new Rect2D(num10, num11, num10 + num8, num11 + num9)));
					}
				}
				else if (text7.StartsWith("Circle"))
				{
					string[] array3 = text7.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanTrueCircleDungeonSegment(Location2D.Get(array3[0].RollCached(), array3[1].RollCached()), array3[2].RollCached()));
				}
				else if (text7.StartsWith("Tower"))
				{
					string[] array4 = text7.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanTowerDungeonSegment(Location2D.Get(array4[0].RollCached(), array4[1].RollCached()), array4[2].RollCached(), array4[3].RollCached()));
				}
			}
			ColorOutputMap colorOutputMap = new ColorOutputMap(80, 25);
			for (int k = 0; k < list.Count; k++)
			{
				string text8 = tablePrefix + "PrimaryTemplate_*Default";
				PopulationResult populationResult3 = PopulationManager.RollOneFrom(text8, null, null);
				if (populationResult3 == null)
				{
					throw new Exception("Got no pass 1 blueprint result from " + text8);
				}
				string text9;
				if (k == 0 || Stat.Random(1, 100) <= 75)
				{
					text9 = this.ResolveOneObjectFromArgs(sultanDungeonArgs.primaryTemplate, populationResult3.Blueprint, Z.NewTier);
				}
				else
				{
					text9 = populationResult3.Blueprint;
				}
				text9 = regionArgs.primaryTemplate.GetRandomElement<string>(null);
				int primaryTemplateN = sultanDungeonArgs.primaryTemplateN;
				string text10 = "";
				string text11 = tablePrefix + "SecondaryTemplate_*Default";
				PopulationResult populationResult4 = PopulationManager.RollOneFrom(text11, null, null);
				if (populationResult4 == null)
				{
					throw new Exception("Got no pass 2 blueprint result from " + text11);
				}
				string text12;
				if (k == 0 || Stat.Random(1, 100) <= 75)
				{
					text12 = this.ResolveOneObjectFromArgs(sultanDungeonArgs.secondaryTemplate, populationResult4.Blueprint, Z.NewTier);
				}
				else
				{
					text12 = populationResult4.Blueprint;
				}
				text12 = regionArgs.secondaryTemplate.GetRandomElement<string>(null);
				int secondaryTemplateN = sultanDungeonArgs.secondaryTemplateN;
				if (text9.Contains(","))
				{
					string[] array5 = text9.Split(',', StringSplitOptions.None);
					text9 = array5[0];
					text10 = array5[1];
				}
				if (text12.Contains(","))
				{
					text12 = text12.Split(',', StringSplitOptions.None)[0];
				}
				Cell cell = Z.GetCell(0, 0);
				if (cell == null)
				{
					throw new Exception("No cell at 0, 0");
				}
				if (Z.GetZoneZ() <= 10)
				{
					cell.AddObject("DaylightWidget", null, null, null, null);
				}
				if ((!AddSurface || Z.Z > 10 || string.IsNullOrEmpty(cell.PaintTile)) && !string.IsNullOrEmpty(blueprint))
				{
					cell.AddObject(blueprint, null, null, null, null);
				}
				WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(text9, primaryTemplateN, list[k].width(), list[k].height(), true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				if (!string.IsNullOrEmpty(text10))
				{
					waveCollapseFastModel.ClearColors(text10, "border1");
				}
				waveCollapseFastModel.UpdateSample(text12, secondaryTemplateN, true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				ColorOutputMap colorOutputMap2 = new ColorOutputMap(waveCollapseFastModel);
				colorOutputMap.Paste(colorOutputMap2, list[k].x1, list[k].y1);
				MemoryHelper.GCCollect();
			}
			this.LogMem("step2");
			string defaultWall = base.GetDefaultWall(Z);
			if (Z.Z > 10 && Clear)
			{
				for (int l = 0; l < Z.Height; l++)
				{
					for (int m = 0; m < Z.Width; m++)
					{
						bool flag = false;
						for (int n = 0; n < list.Count; n++)
						{
							if (list[n].contains(m, l))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							Cell cell2 = Z.GetCell(m, l);
							if (cell2 == null)
							{
								throw new Exception("No cell at " + m.ToString() + ", " + l.ToString());
							}
							if (!cell2.HasWall())
							{
								cell2.AddObject(defaultWall, null, null, null, null);
							}
						}
					}
				}
			}
			if (AddSurface && Z.Z == 10)
			{
				string terrain = base.GetTerrain(Z);
				if (!string.IsNullOrEmpty(terrain))
				{
					if (terrain.Contains("Canyon") || terrain.Contains("Hills") || terrain.Contains("Mountains"))
					{
						new Hills().BuildZone(Z);
						Z.GetCell(0, 0).AddObject("Rocky", null, null, null, null);
					}
					else if (terrain.Contains("Wastes") || terrain.Contains("Desert") || terrain.Contains("dunes"))
					{
						new SaltDunes().BuildZone(Z);
					}
					else if (terrain.Contains("marsh") || terrain.Contains("Watervine"))
					{
						new Watervine().BuildZone(Z);
					}
					else if (terrain.Contains("River") || terrain.Contains("Water") || terrain.Contains("Opal") || terrain.Contains("Stream"))
					{
						new OverlandWater().BuildZone(Z);
					}
					else if (terrain.Contains("Ruins"))
					{
						new Ruins().BuildZone(Z);
					}
					else if (terrain.Contains("Flowerf"))
					{
						new Flowerfields().BuildZone(Z);
					}
					else if (terrain.Contains("Banana"))
					{
						new BananaGrove().BuildZone(Z);
					}
					else
					{
						Z.GetCell(0, 0).AddObject(blueprint, null, null, null, null);
					}
				}
			}
			int num14 = 0;
			int num15 = 0;
			for (int num16 = 0; num16 < list.Count; num16++)
			{
				string text13 = this.ResolveOneObjectFromArgs(sultanDungeonArgs.wallTypes, "Fulcrete", Z.NewTier);
				for (int num17 = list[num16].y1; num17 < list[num16].y2; num17++)
				{
					for (int num18 = list[num16].x1; num18 < list[num16].x2; num18++)
					{
						if (list[num16].contains(num18, num17))
						{
							Cell cell3 = Z.GetCell(num18 + num14, num17 + num15);
							if (cell3 == null)
							{
								throw new Exception("No cell at " + (num18 + num14).ToString() + ", " + (num17 + num15).ToString());
							}
							for (int num19 = num16 + 1; num19 < list.Count; num19++)
							{
								if (list[num19].contains(num18, num17))
								{
									goto IL_0CD6;
								}
							}
							Color32 color = colorOutputMap.getPixel(num18, num17);
							if (list[num16].HasCustomColor(num18, num17))
							{
								color = list[num16].GetCustomColor(num18, num17);
							}
							if (WaveCollapseTools.equals(color, ColorOutputMap.BLACK))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								cell3.ClearWalls();
								if (!cell3.HasWall() && !cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									cell3.AddObject(text13, null, null, null, null);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.BLUE))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								if (text6 != null && !cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									cell3.AddObject(text6, null, null, null, null);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.YELLOW))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								PopulationResult populationResult5 = PopulationManager.RollOneFrom(text4, null, "Door");
								string text14 = ((populationResult5 != null) ? populationResult5.Blueprint : null);
								if (GameObjectFactory.Factory.GetBlueprintIfExists(text13).HasTag("UsesGate", false))
								{
									text14 = GameObjectFactory.Factory.GetBlueprintIfExists(text13).GetTag("UsesGate", "");
								}
								if (populationResult5 == null)
								{
									throw new Exception("Got no yellow result from " + text4);
								}
								if (!cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									cell3.AddObject(text14, null, null, null, null);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.GREEN))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								PopulationResult populationResult6 = PopulationManager.RollOneFrom(text3, null, null);
								if (populationResult6 != null && populationResult6.Number > 0 && !cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									cell3.AddObject(populationResult6.Blueprint, null, null, null, null);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.RED) || WaveCollapseTools.equals(color, ColorOutputMap.MAGENTA))
							{
								if (Clear && Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								if (Clear)
								{
									cell3.ClearWalls();
								}
								if (!cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									Rocky.Paint(cell3);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.WHITE))
							{
								if (blueprint == "Rocky")
								{
									Rocky.Paint(cell3);
								}
								if (blueprint == "Grassy")
								{
									Grassy.PaintCell(cell3);
								}
								if (blueprint == "Flowery")
								{
									Flowery.Paint(cell3);
								}
								if (blueprint == "Mushroomy")
								{
									Mushroomy.Paint(cell3);
								}
								if (blueprint == "Trashy")
								{
									Trashy.PaintCell(cell3);
								}
								if (blueprint == "Dirty")
								{
									Dirty.PaintCell(cell3);
								}
								if (blueprint == "UndergroundGrassy")
								{
									UndergroundGrassy.PaintCell(cell3);
								}
								if (blueprint == "GrassPaint")
								{
									GrassPaint.PaintCell(cell3);
								}
								if (blueprint == "UndergroundGrassPaint")
								{
									UndergroundGrassPaint.PaintCell(cell3);
								}
							}
						}
						IL_0CD6:;
					}
				}
			}
			return true;
		}

		// Token: 0x06006EC1 RID: 28353 RVA: 0x00260EEC File Offset: 0x0025F0EC
		public void ConnectRegions(Zone Z, ColorOutputMap Output)
		{
			InfluenceMap influenceMap = new InfluenceMap(Output.width, Output.height);
			for (int i = 0; i < Output.height; i++)
			{
				for (int j = 0; j < Output.width; j++)
				{
					influenceMap.Walls[j, i] = (Z.GetCell(j, i).HasWall() ? 1 : 0);
				}
			}
			influenceMap.SeedAllUnseeded(false, true);
			if (influenceMap.Seeds.Count == 0)
			{
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.SeedAllUnseeded(false, true);
			}
			influenceMap.MoveSeedsToCenters(false);
			int num = 0;
			using (Pathfinder pathfinder = Z.getPathfinder(null))
			{
				for (int k = 0; k < Output.height; k++)
				{
					for (int l = 0; l < Output.width; l++)
					{
						if (influenceMap.Walls[l, k] > 0)
						{
							pathfinder.CurrentNavigationMap[l, k] = 999;
						}
						else
						{
							pathfinder.CurrentNavigationMap[l, k] = 0;
						}
					}
				}
				this.LogMem("step5");
				List<int> list = new List<int>();
				list.Add(num);
				for (int m = 0; m < influenceMap.Seeds.Count; m++)
				{
					if (!list.Contains(m))
					{
						int num2 = influenceMap.FindClosestSeedToInList(m, list);
						if (num2 == -1)
						{
							num2 = num;
						}
						if (pathfinder.FindPath(influenceMap.Seeds[num2], influenceMap.Seeds[m], false, false, 9999, false))
						{
							int num3 = 0;
							int num4 = 0;
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								Cell cell = Z.GetCell(pathfinderNode.X, pathfinderNode.Y);
								if (cell.HasWall())
								{
									if (this.ConnectStep(cell, num3++, num4++))
									{
										pathfinder.CurrentNavigationMap[pathfinderNode.X, pathfinderNode.Y] = 0;
										pathfinderNode.weight = 0;
									}
								}
								else
								{
									num4 = 0;
									if (Z.Z == 10)
									{
										cell.Clear(null, false, false, null);
									}
								}
							}
						}
						list.Add(m);
					}
				}
				foreach (ZoneConnection zoneConnection in Z.EnumerateConnections())
				{
					Location2D location2D = Location2D.Get(zoneConnection.X, zoneConnection.Y);
					int num5 = influenceMap.FindClosestSeedToInList(location2D, list);
					if (num5 == -1)
					{
						num5 = num;
					}
					if (pathfinder.FindPath(influenceMap.Seeds[num5], location2D, false, false, 9999, false))
					{
						int num6 = 0;
						int num7 = 0;
						foreach (PathfinderNode pathfinderNode2 in pathfinder.Steps)
						{
							Cell cell2 = Z.GetCell(pathfinderNode2.X, pathfinderNode2.Y);
							if (cell2.HasWall())
							{
								if (this.ConnectStep(cell2, num6++, num7++))
								{
									pathfinder.CurrentNavigationMap[pathfinderNode2.X, pathfinderNode2.Y] = 0;
									pathfinderNode2.weight = 0;
								}
							}
							else
							{
								num7 = 0;
								if (Z.Z == 10)
								{
									cell2.Clear(null, false, false, null);
								}
							}
						}
					}
				}
				this.LogMem("step6");
				for (int n = 0; n < list.Count; n++)
				{
					if (!Z.IsReachable(influenceMap.Seeds[n].X, influenceMap.Seeds[n].Y))
					{
						Z.BuildReachableMap(influenceMap.Seeds[n].X, influenceMap.Seeds[n].Y);
					}
				}
			}
		}

		// Token: 0x06006EC2 RID: 28354 RVA: 0x00261398 File Offset: 0x0025F598
		public virtual bool ConnectStep(Cell Cell, int Step, int Consecutive)
		{
			Cell.ClearWalls();
			if (Step == 0)
			{
				if (Cell.HasObjectWithTag("UsesGate"))
				{
					string tag = Cell.GetObjectWithTag("UsesGate").GetTag("UsesGate", "Iron Gate");
					Cell.AddObject(tag, null, null, null, null);
				}
				else
				{
					Cell.AddObject("Door", null, null, null, null);
				}
			}
			return true;
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x002613F8 File Offset: 0x0025F5F8
		public void EnrollInCult(GameObject GO, SultanDungeonArgs args)
		{
			if (!string.IsNullOrEmpty(args.cultFaction))
			{
				GO.RequirePart<SocialRoles>(false).RequireRole("member of the " + args.cultName);
				if (GO.Brain != null)
				{
					GO.Brain.FactionFeelings.Clear();
					GO.Brain.Allegiance.Clear();
					GO.Brain.Allegiance.Add(args.cultFaction, 100);
				}
			}
		}

		// Token: 0x06006EC4 RID: 28356 RVA: 0x00261470 File Offset: 0x0025F670
		private void ProcessBlueprintForCultRole(GameObjectBlueprint B)
		{
			string text = "Minion";
			string text2;
			if (B.Tags.TryGetValue("Role", out text2) && !text2.IsNullOrEmpty())
			{
				text = text2;
			}
			if (B.Props.TryGetValue("Role", out text2) && !text2.IsNullOrEmpty())
			{
				text = text2;
			}
			List<string> list;
			if (!this.roleMembers.TryGetValue(text, out list))
			{
				list = new List<string>();
				this.roleMembers.Add(text, list);
			}
			if (!this.memberTier.ContainsKey(B.Name))
			{
				list.Add(B.Name);
				this.allMembers.Add(B.Name);
				this.memberTier.Add(B.Name, B.Tier);
				this.memberHasBody.Add(B.Name, B.HasPart("Body"));
			}
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x00261548 File Offset: 0x0025F748
		public string ResolveCultRole(string Role, SultanDungeonArgs args, int targetTier)
		{
			if (this.roleMembers == null)
			{
				this.roleMembers = new Dictionary<string, List<string>>(16);
				this.allMembers = new List<string>(16);
				this.memberTier = new Dictionary<string, int>(16);
				this.memberHasBody = new Dictionary<string, bool>(16);
				if (args.cultFactions.Count > 0)
				{
					foreach (string text in args.cultFactions)
					{
						foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.GetFactionMembers(text, false, true))
						{
							this.ProcessBlueprintForCultRole(gameObjectBlueprint);
						}
					}
				}
				if (!string.IsNullOrEmpty(this.monstertable))
				{
					foreach (string text2 in PopulationManager.GetEach(this.monstertable, new Dictionary<string, string> { 
					{
						"ZoneTier",
						targetTier.ToString()
					} }, null))
					{
						GameObjectBlueprint gameObjectBlueprint2 = GameObjectFactory.Factory.Blueprints[text2];
						this.ProcessBlueprintForCultRole(gameObjectBlueprint2);
					}
				}
			}
			bool flag = false;
			if (Role == "Hero")
			{
				flag = true;
				if (!this.roleMembers.ContainsKey("Hero"))
				{
					Role = "Leader";
				}
			}
			List<string> list = this.allMembers;
			List<string> list2;
			if (this.roleMembers.TryGetValue(Role, out list2) && list2.Count > 0)
			{
				list = list2;
			}
			int num = 12;
			while (num.in100())
			{
				if (15.in100())
				{
					targetTier--;
				}
				else
				{
					targetTier++;
				}
				num = Math.Max(num - 6, 1);
			}
			Tier.Constrain(ref targetTier);
			List<string> list3 = new List<string>();
			for (int i = 0; i < 8; i++)
			{
				if (flag)
				{
					list3.Clear();
					int j = 0;
					int count = list.Count;
					while (j < count)
					{
						if ((this.memberTier[list[j]] - i == targetTier || this.memberTier[list[j]] + i == targetTier) && this.memberHasBody[list[j]])
						{
							list3.Add(list[j]);
						}
						j++;
					}
					if (list3.Count > 0)
					{
						return list3.GetRandomElement<string>(null);
					}
					list3.Clear();
					int k = 0;
					int count2 = this.allMembers.Count;
					while (k < count2)
					{
						if ((this.memberTier[this.allMembers[k]] - i == targetTier || this.memberTier[this.allMembers[k]] + i == targetTier) && this.memberHasBody[this.allMembers[k]])
						{
							list3.Add(this.allMembers[k]);
						}
						k++;
					}
					if (list3.Count > 0)
					{
						return list3.GetRandomElement<string>(null);
					}
				}
				list3.Clear();
				int l = 0;
				int count3 = list.Count;
				while (l < count3)
				{
					if (this.memberTier[list[l]] - i == targetTier || this.memberTier[list[l]] + i == targetTier)
					{
						list3.Add(list[l]);
					}
					l++;
				}
				if (list3.Count > 0)
				{
					return list3.GetRandomElement<string>(null);
				}
				list3.Clear();
				int m = 0;
				int count4 = this.allMembers.Count;
				while (m < count4)
				{
					if (this.memberTier[this.allMembers[m]] - i == targetTier || this.memberTier[this.allMembers[m]] + i == targetTier)
					{
						list3.Add(this.allMembers[m]);
					}
					m++;
				}
				if (list3.Count > 0)
				{
					return list3.GetRandomElement<string>(null);
				}
			}
			if (list2 != null && list2.Count > 0)
			{
				if (flag)
				{
					list3.Clear();
					int n = 0;
					int count5 = list2.Count;
					while (n < count5)
					{
						if (this.memberHasBody[list2[n]])
						{
							list3.Add(list2[n]);
						}
						n++;
					}
					if (list3.Count > 0)
					{
						return list3.GetRandomElement<string>(null);
					}
				}
				return list2.GetRandomElement<string>(null);
			}
			return null;
		}

		// Token: 0x06006EC6 RID: 28358 RVA: 0x002619EC File Offset: 0x0025FBEC
		public static int GetSultanPeriodFromTier(int tier)
		{
			if (tier == 0 || tier == 1 || tier == 2)
			{
				return 5;
			}
			if (tier == 3 || tier == 4)
			{
				return 4;
			}
			if (tier == 5 || tier == 6)
			{
				return 3;
			}
			if (tier == 7)
			{
				return 2;
			}
			if (tier == 8)
			{
				return 1;
			}
			return 5;
		}

		// Token: 0x06006EC7 RID: 28359 RVA: 0x00261A1C File Offset: 0x0025FC1C
		public static string GetDefaultWallByPeriod(string period)
		{
			PopulationResult populationResult = PopulationManager.RollOneFrom("SultanDungeons_Wall_Default_Period" + period, null, null);
			if (populationResult == null)
			{
				return null;
			}
			return populationResult.Blueprint;
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x00261A3C File Offset: 0x0025FC3C
		public void CreateCyst(Zone Z, Location2D Location, string Blueprint, int MinRadius = 6, int MaxRadius = 8, global::System.Random Random = null, bool FullClear = false, bool Paint = false)
		{
			if (Random == null)
			{
				Random = Stat.Rnd;
			}
			int num = Random.Next(MinRadius, MaxRadius + 1);
			float num2 = (float)Random.Next(80, 91) / 75f;
			float num3 = (float)Random.Next(80, 91) / 75f;
			int num4 = (int)((float)num * num2);
			int num5 = (int)((float)num * num3);
			for (int i = -num4; i <= num4; i++)
			{
				for (int j = -num5; j <= num5; j++)
				{
					Cell cell = Z.GetCell(Location.X + i, Location.Y + j);
					if (cell != null && !cell.HasStairs())
					{
						int num6 = (int)Math.Sqrt((double)(num2 * (float)Math.Abs(i) * (num2 * (float)Math.Abs(i)) + (float)Math.Abs(j) * num3 * ((float)Math.Abs(j) * num3)));
						if (num6 <= num)
						{
							if (FullClear)
							{
								cell.Clear(null, false, false, null);
							}
							else
							{
								cell.ClearWalls();
							}
							if (Paint)
							{
								cell.PaintTile = "Tiles/tile-dirt1.png";
								cell.PaintColorString = "&m";
							}
							if (num6 >= num - 2)
							{
								cell.RequireObject(Blueprint, null, null, null, null);
							}
						}
						else if (num6 < 3)
						{
							if (FullClear)
							{
								cell.Clear(null, false, false, null);
							}
							else
							{
								cell.ClearWalls();
							}
						}
					}
				}
			}
		}

		// Token: 0x040032AB RID: 12971
		public Zone Zone;

		// Token: 0x040032AC RID: 12972
		public string locationName;

		// Token: 0x040032AD RID: 12973
		public string regionName;

		// Token: 0x040032AE RID: 12974
		public string stairs = "";

		// Token: 0x040032AF RID: 12975
		public string monstertable = "";

		// Token: 0x040032B0 RID: 12976
		public const int CHANCE_FOR_SPECIAL_HISTORIC_WALL = 25;

		// Token: 0x040032B1 RID: 12977
		public long lastmem;

		// Token: 0x040032B2 RID: 12978
		private static Dictionary<string, int> staticPerDungeon = new Dictionary<string, int> { { "*LightSource", 20 } };

		// Token: 0x040032B3 RID: 12979
		private int targetTier;

		// Token: 0x040032B4 RID: 12980
		private Zone targetZone;

		// Token: 0x040032B5 RID: 12981
		public Dictionary<string, int> memberTier;

		// Token: 0x040032B6 RID: 12982
		public Dictionary<string, List<string>> roleMembers;

		// Token: 0x040032B7 RID: 12983
		public List<string> allMembers;

		// Token: 0x040032B8 RID: 12984
		public Dictionary<string, bool> memberHasBody;
	}
}
