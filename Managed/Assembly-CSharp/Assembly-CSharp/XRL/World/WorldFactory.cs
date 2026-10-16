using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using XRL.Core;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x0200098B RID: 2443
	public class WorldFactory
	{
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x002008C0 File Offset: 0x001FEAC0
		public static WorldFactory Factory
		{
			get
			{
				WorldFactory worldFactory;
				if ((worldFactory = WorldFactory._Factory) == null)
				{
					worldFactory = (WorldFactory._Factory = new WorldFactory());
				}
				return worldFactory;
			}
		}

		// Token: 0x0600671F RID: 26399 RVA: 0x002008D8 File Offset: 0x001FEAD8
		public int countWorlds()
		{
			int num = this.Worlds.Keys.Count;
			if (The.Game != null && The.Game.HasObjectGameState("AdditionalWorld"))
			{
				Dictionary<string, WorldBlueprint> dictionary = The.Game.GetObjectGameState("AdditionalWorld") as Dictionary<string, WorldBlueprint>;
				if (dictionary != null)
				{
					num += dictionary.Keys.Count;
				}
			}
			return num;
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x00200938 File Offset: 0x001FEB38
		public bool hasWorld(string ID)
		{
			if (this.Worlds.ContainsKey(ID))
			{
				return true;
			}
			if (The.Game != null && The.Game.HasObjectGameState("AdditionalWorld"))
			{
				Dictionary<string, WorldBlueprint> dictionary = The.Game.GetObjectGameState("AdditionalWorld") as Dictionary<string, WorldBlueprint>;
				if (dictionary != null && dictionary.ContainsKey(ID))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006721 RID: 26401 RVA: 0x00200994 File Offset: 0x001FEB94
		public void addAdditionalWorld(string ID, WorldBlueprint World)
		{
			if (The.Game != null)
			{
				if (!The.Game.HasObjectGameState("AdditionalWorld"))
				{
					The.Game.ObjectGameState.Add("AdditionalWorlds", new Dictionary<string, WorldBlueprint>());
				}
				(The.Game.GetObjectGameState("AdditionalWorld") as Dictionary<string, WorldBlueprint>)[ID] = World;
			}
		}

		// Token: 0x06006722 RID: 26402 RVA: 0x002009F0 File Offset: 0x001FEBF0
		public List<WorldBlueprint> getWorlds()
		{
			this.WorldsScratch.Clear();
			if (The.Game != null && The.Game.HasObjectGameState("AdditionalWorld"))
			{
				Dictionary<string, WorldBlueprint> dictionary = The.Game.GetObjectGameState("AdditionalWorld") as Dictionary<string, WorldBlueprint>;
				if (dictionary != null)
				{
					this.WorldsScratch.AddRange(dictionary.Values);
				}
			}
			this.WorldsScratch.AddRange(this.Worlds.Values);
			return this.WorldsScratch;
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x00200A68 File Offset: 0x001FEC68
		public WorldBlueprint getWorld(string ID)
		{
			WorldBlueprint worldBlueprint;
			if (this.Worlds.TryGetValue(ID, out worldBlueprint))
			{
				return worldBlueprint;
			}
			if (The.Game != null && The.Game.HasObjectGameState("AdditionalWorld"))
			{
				Dictionary<string, WorldBlueprint> dictionary = The.Game.GetObjectGameState("AdditionalWorld") as Dictionary<string, WorldBlueprint>;
				if (dictionary != null && dictionary.TryGetValue(ID, out worldBlueprint))
				{
					return worldBlueprint;
				}
			}
			return null;
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x00200AC8 File Offset: 0x001FECC8
		public void BuildWorlds()
		{
			foreach (string text in this.Worlds.Keys)
			{
				this.BuildWorld(text);
			}
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x00200B20 File Offset: 0x001FED20
		public void BuildWorld(string World)
		{
			if (this.WorldBuilderExtensions == null)
			{
				this.WorldBuilderExtensions = ModManager.GetInstancesWithAttribute<IWorldBuilderExtension>(typeof(WorldBuilderExtension));
			}
			foreach (ZoneBuilderBlueprint zoneBuilderBlueprint in this.Worlds[World].Builders)
			{
				string text = "XRL.World.WorldBuilders." + zoneBuilderBlueprint.Class;
				Type type = ModManager.ResolveType(text, false, false, true);
				if (type == null)
				{
					XRLCore.LogError("Unknown world builder " + text + "!");
					break;
				}
				object newBuilder = Activator.CreateInstance(type);
				foreach (FieldInfo fieldInfo in type.GetFields())
				{
					ZoneBuilderBlueprint zoneBuilderBlueprint2 = zoneBuilderBlueprint;
					if (zoneBuilderBlueprint2.Parameters == null)
					{
						zoneBuilderBlueprint2.Parameters = new Dictionary<string, object>();
					}
					if (zoneBuilderBlueprint.Parameters.ContainsKey(fieldInfo.Name))
					{
						if (fieldInfo.FieldType == typeof(bool))
						{
							fieldInfo.SetValue(newBuilder, Convert.ToBoolean(zoneBuilderBlueprint.Parameters[fieldInfo.Name]));
						}
						else if (fieldInfo.FieldType == typeof(int))
						{
							fieldInfo.SetValue(newBuilder, Convert.ToInt32(zoneBuilderBlueprint.Parameters[fieldInfo.Name]));
						}
						else if (fieldInfo.FieldType == typeof(short))
						{
							fieldInfo.SetValue(newBuilder, Convert.ToInt16(zoneBuilderBlueprint.Parameters[fieldInfo.Name]));
						}
						else
						{
							fieldInfo.SetValue(newBuilder, zoneBuilderBlueprint.Parameters[fieldInfo.Name]);
						}
					}
				}
				this.WorldBuilderExtensions.ForEach(delegate(IWorldBuilderExtension e)
				{
					e.OnBeforeBuild(World, newBuilder);
				});
				The.ZoneManager.AdjustZoneGenerationTierTo(World);
				MethodInfo method = type.GetMethod("BuildWorld");
				if (method != null && !(bool)method.Invoke(newBuilder, new object[] { World }))
				{
					break;
				}
				this.WorldBuilderExtensions.ForEach(delegate(IWorldBuilderExtension e)
				{
					e.OnAfterBuild(World, newBuilder);
				});
				newBuilder = null;
				MemoryHelper.GCCollect();
			}
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x00200DEC File Offset: 0x001FEFEC
		public string ZoneDisplayName(string ID)
		{
			string zoneDisplayName;
			if (this.ZoneIDToDisplay.TryGetValue(ID, out zoneDisplayName))
			{
				return zoneDisplayName;
			}
			zoneDisplayName = The.ZoneManager.GetZoneDisplayName(ID, false, false, true, true);
			this.ZoneIDToDisplay.Add(ID, zoneDisplayName);
			return zoneDisplayName;
		}

		// Token: 0x06006727 RID: 26407 RVA: 0x00200E2C File Offset: 0x001FF02C
		public string ZoneDisplayNameWithIndefiniteArticle(string ID)
		{
			string zoneDisplayName;
			if (this.ZoneIDToDisplayWithIndefiniteArticle.TryGetValue(ID, out zoneDisplayName))
			{
				return zoneDisplayName;
			}
			zoneDisplayName = The.ZoneManager.GetZoneDisplayName(ID, true, false, true, true);
			this.ZoneIDToDisplayWithIndefiniteArticle.Add(ID, zoneDisplayName);
			return zoneDisplayName;
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x00200E6C File Offset: 0x001FF06C
		public void UpdateZoneDisplayName(string ID, string Name)
		{
			string text;
			if (this.ZoneIDToDisplay.TryGetValue(ID, out text))
			{
				this.ZoneIDToDisplay[ID] = Name;
				string text2 = null;
				if (this.ZoneDisplayToID.TryGetValue(text, out text2) && text2 == ID)
				{
					this.ZoneDisplayToID.Remove(text);
					this.ZoneDisplayToID.Set(Name, ID);
				}
			}
			if (this.ZoneIDToDisplayWithIndefiniteArticle.TryGetValue(ID, out text))
			{
				this.ZoneIDToDisplayWithIndefiniteArticle[ID] = Name;
				string text3 = null;
				if (this.ZoneDisplayToIDWithIndefiniteArticle.TryGetValue(text, out text3) && text3 == ID)
				{
					this.ZoneDisplayToIDWithIndefiniteArticle.Remove(text);
					this.ZoneDisplayToIDWithIndefiniteArticle.Set(Name, ID);
				}
			}
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x00200F20 File Offset: 0x001FF120
		public void BuildZoneNameMap()
		{
			this.ZoneIDToDisplay = new Dictionary<string, string>();
			this.ZoneDisplayToID = new Dictionary<string, string>();
			this.ZoneIDToDisplayWithIndefiniteArticle = new Dictionary<string, string>();
			this.ZoneDisplayToIDWithIndefiniteArticle = new Dictionary<string, string>();
			if (Options.EnableWishRegionNames)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(512);
			StringBuilder stringBuilder2 = new StringBuilder(512);
			StringBuilder stringBuilder3 = new StringBuilder(512);
			StringBuilder stringBuilder4 = new StringBuilder(512);
			StringBuilder stringBuilder5 = new StringBuilder(512);
			int num = 0;
			foreach (string text in WorldFactory.Factory.Worlds.Keys)
			{
				num++;
				for (int i = 0; i < 80; i++)
				{
					if (i % 2 == 0)
					{
						if (num == 1)
						{
							WorldCreationProgress.StepProgress("Generating rivers...", false);
						}
						else
						{
							WorldCreationProgress.StepProgress("Generating canyons...", false);
						}
					}
					stringBuilder.Length = 0;
					stringBuilder.Append(text);
					stringBuilder.Append(".");
					stringBuilder.Append(i);
					stringBuilder.Append(".");
					for (int j = 0; j < 25; j++)
					{
						stringBuilder2.Length = 0;
						stringBuilder2.Append(stringBuilder);
						stringBuilder2.Append(j);
						stringBuilder2.Append(".");
						for (int k = 0; k < 3; k++)
						{
							stringBuilder3.Length = 0;
							stringBuilder3.Append(stringBuilder2);
							stringBuilder3.Append(k);
							stringBuilder3.Append(".");
							for (int l = 0; l < 3; l++)
							{
								stringBuilder4.Length = 0;
								stringBuilder4.Append(stringBuilder3);
								stringBuilder4.Append(l);
								stringBuilder4.Append(".");
								for (int m = 10; m <= 10; m++)
								{
									stringBuilder5.Length = 0;
									stringBuilder5.Append(stringBuilder4);
									stringBuilder5.Append(m);
									string text2 = stringBuilder5.ToString();
									string zoneDisplayName = The.ZoneManager.GetZoneDisplayName(text2, text, i, j, k, l, m, false, false, true, true);
									if (!this.ZoneDisplayToID.ContainsKey(zoneDisplayName))
									{
										this.ZoneDisplayToID.Add(zoneDisplayName, text2);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x0020118C File Offset: 0x001FF38C
		public void Init()
		{
			this.Worlds = new Dictionary<string, WorldBlueprint>();
			this.Cells = new Dictionary<string, CellBlueprint>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Worlds", true, true))
			{
				try
				{
					this.LoadWorldsNode(xmlDataHelper);
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(xmlDataHelper.modInfo, ex);
				}
			}
		}

		// Token: 0x0600672B RID: 26411 RVA: 0x00201214 File Offset: 0x001FF414
		public void LoadWorldsNode(XmlTextReader Reader)
		{
			while (Reader.Read())
			{
				if (Reader.Name == "world")
				{
					WorldBlueprint worldBlueprint = this.LoadWorldNode(Reader);
					WorldBlueprint worldBlueprint2;
					if (this.Worlds.TryGetValue(worldBlueprint.Name, out worldBlueprint2))
					{
						using (List<ZoneBuilderBlueprint>.Enumerator enumerator = worldBlueprint.Builders.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ZoneBuilderBlueprint Builder = enumerator.Current;
								if (Builder.Class.StartsWith("-"))
								{
									string builderToRemove = Builder.Class.Substring(1);
									worldBlueprint2.Builders.RemoveAll((ZoneBuilderBlueprint b) => b.Class == builderToRemove);
								}
								else
								{
									worldBlueprint2.Builders.RemoveAll((ZoneBuilderBlueprint b) => b.Class == Builder.Class);
									worldBlueprint2.Builders.Add(Builder);
								}
							}
						}
						using (Dictionary<string, CellBlueprint>.Enumerator enumerator2 = worldBlueprint.CellBlueprintsByName.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<string, CellBlueprint> keyValuePair = enumerator2.Current;
								CellBlueprint cellBlueprint;
								if (worldBlueprint2.CellBlueprintsByName.TryGetValue(keyValuePair.Key, out cellBlueprint) && !cellBlueprint.ApplyTo.IsNullOrEmpty())
								{
									worldBlueprint2.CellBlueprintsByApplication.Remove(cellBlueprint.ApplyTo);
								}
								worldBlueprint2.CellBlueprintsByName[keyValuePair.Key] = keyValuePair.Value;
								if (!keyValuePair.Value.ApplyTo.IsNullOrEmpty())
								{
									worldBlueprint2.CellBlueprintsByApplication[keyValuePair.Value.ApplyTo] = keyValuePair.Value;
								}
							}
							continue;
						}
					}
					this.Worlds.Add(worldBlueprint.Name, worldBlueprint);
				}
			}
		}

		// Token: 0x0600672C RID: 26412 RVA: 0x00201408 File Offset: 0x001FF608
		public WorldBlueprint LoadWorldNode(XmlTextReader Reader)
		{
			this.NewWorld = new WorldBlueprint();
			this.NewWorld.DisplayName = Reader.GetAttribute("DisplayName");
			this.NewWorld.Name = Reader.GetAttribute("Name");
			this.NewWorld.Map = Reader.GetAttribute("Map");
			this.NewWorld.AmbientBed = Reader.GetAttribute("AmbientBed");
			this.NewWorld.ZoneFactory = Reader.GetAttribute("ZoneFactory");
			this.NewWorld.ZoneFactoryRegex = Reader.GetAttribute("ZoneFactoryRegex");
			string attribute = Reader.GetAttribute("Plane");
			if (!attribute.IsNullOrEmpty())
			{
				this.NewWorld.Plane = attribute;
			}
			string attribute2 = Reader.GetAttribute("Protocol");
			if (!attribute2.IsNullOrEmpty())
			{
				this.NewWorld.Protocol = attribute2;
			}
			string attribute3 = Reader.GetAttribute("CustomClock");
			if (!attribute3.IsNullOrEmpty())
			{
				this.NewWorld.CustomClock = attribute3;
			}
			string attribute4 = Reader.GetAttribute("PsychicHunterChance");
			int num;
			if (!attribute4.IsNullOrEmpty() && int.TryParse(attribute4, out num))
			{
				this.NewWorld.PsychicHunterChance = new int?(num);
			}
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return this.NewWorld;
			}
			while (Reader.Read())
			{
				string name = Reader.Name;
				if (name == "builder")
				{
					this.NewWorld.Builders.Add(this.LoadBuilderNode(Reader, "builder", null, 4000).Blueprint);
				}
				else if (name == "cell")
				{
					CellBlueprint cellBlueprint = this.LoadCellNode(Reader, this.NewWorld);
					if (!cellBlueprint.ApplyTo.IsNullOrEmpty())
					{
						this.NewWorld.CellBlueprintsByApplication[cellBlueprint.ApplyTo] = cellBlueprint;
					}
					this.NewWorld.CellBlueprintsByName[cellBlueprint.Name] = cellBlueprint;
					this.Cells[this.NewWorld.Name + cellBlueprint.Name] = cellBlueprint;
				}
				if (Reader.NodeType == XmlNodeType.EndElement && (Reader.Name == "" || Reader.Name == "world"))
				{
					return this.NewWorld;
				}
			}
			return this.NewWorld;
		}

		// Token: 0x0600672D RID: 26413 RVA: 0x0020165C File Offset: 0x001FF85C
		private void ParseRangeSpec(string Spec, out int Start, out int End)
		{
			if (Spec.IndexOf('-') != -1)
			{
				string[] array = Spec.Split('-', StringSplitOptions.None);
				Start = Convert.ToInt32(array[0]);
				End = Convert.ToInt32(array[1]);
				return;
			}
			Start = Convert.ToInt32(Spec);
			End = Convert.ToInt32(Spec);
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x002016A4 File Offset: 0x001FF8A4
		public CellBlueprint LoadCellNode(XmlTextReader Reader, WorldBlueprint World)
		{
			CellBlueprint cellBlueprint = new CellBlueprint();
			cellBlueprint.LandingZone = "1,1";
			cellBlueprint.Name = Reader.GetAttribute("Name");
			cellBlueprint.Inherits = Reader.GetAttribute("Inherits");
			cellBlueprint.ApplyTo = Reader.GetAttribute("ApplyTo");
			string attribute = Reader.GetAttribute("Mutable");
			if (attribute != null && !attribute.EqualsNoCase("true"))
			{
				cellBlueprint.Mutable = false;
			}
			string attribute2 = Reader.GetAttribute("LandingZone");
			if (attribute2 != null)
			{
				cellBlueprint.LandingZone = attribute2;
			}
			bool flag = !Reader.GetAttribute("HasBiomes").EqualsNoCase("false");
			string attribute3 = Reader.GetAttribute("Tier");
			int num;
			if (attribute3.IsNullOrEmpty() || !int.TryParse(attribute3, out num))
			{
				num = 0;
			}
			CellBlueprint cellBlueprint2;
			if (cellBlueprint.Inherits != null && this.Cells.TryGetValue(World.Name + cellBlueprint.Inherits, out cellBlueprint2) && cellBlueprint2 != null)
			{
				cellBlueprint.CopyFrom(cellBlueprint2);
			}
			string attribute4 = Reader.GetAttribute("AmbientBed");
			if (attribute4 != null)
			{
				cellBlueprint.AmbientBed = attribute4;
			}
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return cellBlueprint;
			}
			while (Reader.Read())
			{
				string name = Reader.Name;
				if (name == "zone")
				{
					ZoneBlueprint zoneBlueprint = this.LoadZoneNode(Reader, cellBlueprint);
					if (num > 0 && zoneBlueprint.Tier <= 0)
					{
						zoneBlueprint.Tier = num;
					}
					if (zoneBlueprint.HasBiomes && !flag)
					{
						zoneBlueprint.HasBiomes = false;
					}
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					int num7 = 0;
					this.ParseRangeSpec(zoneBlueprint.Level, out num2, out num3);
					this.ParseRangeSpec(zoneBlueprint.x, out num4, out num5);
					this.ParseRangeSpec(zoneBlueprint.y, out num6, out num7);
					for (int i = num6; i <= num7; i++)
					{
						for (int j = num4; j <= num5; j++)
						{
							for (int k = num2; k <= num3; k++)
							{
								cellBlueprint.LevelBlueprint[j, i, k] = zoneBlueprint;
							}
						}
					}
				}
				else if (name == "property")
				{
					this.LoadPropertyNode<string>(Reader, new Action<string, object>(cellBlueprint.SetProperty));
				}
				else if (name == "intproperty")
				{
					this.LoadPropertyNode<int>(Reader, new Action<string, object>(cellBlueprint.SetProperty));
				}
				else if (name == "boolproperty")
				{
					this.LoadPropertyNode<bool>(Reader, new Action<string, object>(cellBlueprint.SetProperty));
				}
				if (Reader.NodeType == XmlNodeType.EndElement && (name == "" || name == "cell"))
				{
					return cellBlueprint;
				}
			}
			return cellBlueprint;
		}

		// Token: 0x0600672F RID: 26415 RVA: 0x0020194C File Offset: 0x001FFB4C
		public ZoneBlueprint LoadZoneNode(XmlTextReader Reader, CellBlueprint Parent)
		{
			ZoneBlueprint zoneBlueprint = new ZoneBlueprint(null);
			zoneBlueprint.Cell = Parent;
			zoneBlueprint.Level = Reader.GetAttribute("Level");
			zoneBlueprint.x = Reader.GetAttribute("x");
			zoneBlueprint.y = Reader.GetAttribute("y");
			zoneBlueprint.disableForcedConnections = Reader.GetAttribute("DisableForcedConnections").EqualsNoCase("Yes");
			zoneBlueprint.ProperName = Reader.GetAttribute("ProperName").EqualsNoCase("true");
			zoneBlueprint.NameContext = Reader.GetAttribute("NameContext");
			zoneBlueprint.IndefiniteArticle = Reader.GetAttribute("IndefiniteArticle");
			zoneBlueprint.DefiniteArticle = Reader.GetAttribute("DefiniteArticle");
			zoneBlueprint.AmbientBed = Reader.GetAttribute("AmbientBed") ?? Parent.AmbientBed;
			zoneBlueprint.AmbientSounds = Reader.GetAttribute("AmbientSounds");
			int num;
			zoneBlueprint.AmbientVolume = (int.TryParse(Reader.GetAttribute("AmbientVolume"), out num) ? num : (-1));
			zoneBlueprint.IncludeContextInZoneDisplay = (Reader.GetAttribute("IncludeContextInZoneDisplay") ?? "true").EqualsNoCase("true");
			zoneBlueprint.IncludeStratumInZoneDisplay = (Reader.GetAttribute("IncludeStratumInZoneDisplay") ?? "true").EqualsNoCase("true");
			zoneBlueprint.HasWeather = Reader.GetAttribute("HasWeather").EqualsNoCase("true");
			zoneBlueprint.HasBiomes = !Reader.GetAttribute("HasBiomes").EqualsNoCase("false");
			zoneBlueprint.WindSpeed = Reader.GetAttribute("WindSpeed");
			zoneBlueprint.WindDirections = Reader.GetAttribute("WindDirections");
			zoneBlueprint.WindDuration = Reader.GetAttribute("WindDuration");
			string attribute = Reader.GetAttribute("GroundLiquid");
			if (attribute != null)
			{
				zoneBlueprint.GroundLiquid = attribute;
			}
			string attribute2 = Reader.GetAttribute("Name");
			if (attribute2 != null)
			{
				zoneBlueprint.Name = attribute2;
			}
			if (Reader.GetAttribute("DisableForcedConnections").EqualsNoCase("Yes"))
			{
				zoneBlueprint.SetProperty("DisableForcedConnections", "Yes");
			}
			string attribute3 = Reader.GetAttribute("Tier");
			int num2;
			if (!attribute3.IsNullOrEmpty() && int.TryParse(attribute3, out num2))
			{
				zoneBlueprint.Tier = num2;
			}
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return zoneBlueprint;
			}
			while (Reader.Read())
			{
				string name = Reader.Name;
				uint num3 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num3 <= 3072429003U)
				{
					if (num3 <= 2677821396U)
					{
						if (num3 != 2088252948U)
						{
							if (num3 == 2677821396U)
							{
								if (name == "music")
								{
									zoneBlueprint.Builders.Add(this.LoadBuilderNode(Reader, "music", "Music", 4000));
								}
							}
						}
						else if (name == "part")
						{
							ZoneBlueprint zoneBlueprint2 = zoneBlueprint;
							if (zoneBlueprint2.Parts == null)
							{
								zoneBlueprint2.Parts = new ZonePartCollection();
							}
							zoneBlueprint.Parts.Add(this.LoadPartNode(Reader, "part"));
						}
					}
					else if (num3 != 2843749381U)
					{
						if (num3 != 3017706674U)
						{
							if (num3 == 3072429003U)
							{
								if (name == "intproperty")
								{
									this.LoadPropertyNode<int>(Reader, new Action<string, object>(zoneBlueprint.SetProperty));
								}
							}
						}
						else if (name == "population")
						{
							zoneBlueprint.Builders.Add(this.LoadBuilderNode(Reader, "population", "Population", 4000));
						}
					}
					else if (name == "widget")
					{
						zoneBlueprint.Builders.Add(this.LoadBuilderNode(Reader, "widget", "AddWidgetBuilder", 4000));
					}
				}
				else if (num3 <= 3602055880U)
				{
					if (num3 != 3160503262U)
					{
						if (num3 != 3287485344U)
						{
							if (num3 == 3602055880U)
							{
								if (name == "property")
								{
									this.LoadPropertyNode<string>(Reader, new Action<string, object>(zoneBlueprint.SetProperty));
								}
							}
						}
						else if (name == "boolproperty")
						{
							this.LoadPropertyNode<bool>(Reader, new Action<string, object>(zoneBlueprint.SetProperty));
						}
					}
					else if (name == "postbuilder")
					{
						zoneBlueprint.Builders.Add(this.LoadBuilderNode(Reader, "postbuilder", null, 5000));
					}
				}
				else if (num3 != 3751997361U)
				{
					if (num3 != 3828680000U)
					{
						if (num3 == 3919135721U)
						{
							if (name == "prebuilder")
							{
								zoneBlueprint.Builders.Add(this.LoadBuilderNode(Reader, "prebuilder", null, 3999));
							}
						}
					}
					else if (name == "builder")
					{
						zoneBlueprint.Builders.Add(this.LoadBuilderNode(Reader, "builder", null, 4000));
					}
				}
				else if (name == "map")
				{
					zoneBlueprint.Builders.Add(this.LoadBuilderNode(Reader, "map", "MapBuilder", 4000));
				}
				if (Reader.NodeType == XmlNodeType.EndElement && (name == "" || name == "zone"))
				{
					return zoneBlueprint;
				}
			}
			return zoneBlueprint;
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x00201EE0 File Offset: 0x002000E0
		public OrderedBuilderBlueprint LoadBuilderNode(XmlTextReader Reader, string Tag, string DefaultClass = null, int DefaultPriority = 4000)
		{
			string text = Reader.GetAttribute("Class");
			if (text.IsNullOrEmpty())
			{
				text = DefaultClass;
			}
			int num;
			if (!int.TryParse(Reader.GetAttribute("Priority"), out num))
			{
				num = DefaultPriority;
			}
			Dictionary<string, object> parameterBuffer = ZoneBuilderBlueprint.GetParameterBuffer();
			while (Reader.MoveToNextAttribute())
			{
				string name = Reader.Name;
				if (name != "Class" && name != "Priority")
				{
					parameterBuffer[name] = Reader.Value;
				}
			}
			Reader.MoveToElement();
			ZoneBuilderBlueprint zoneBuilderBlueprint = ZoneBuilderBlueprint.Get(text, parameterBuffer);
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return new OrderedBuilderBlueprint(zoneBuilderBlueprint, num);
			}
			while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || (!(Reader.Name == "") && !(Reader.Name == Tag))))
			{
			}
			return new OrderedBuilderBlueprint(zoneBuilderBlueprint, num);
		}

		// Token: 0x06006731 RID: 26417 RVA: 0x00201FC0 File Offset: 0x002001C0
		public ZonePartBlueprint LoadPartNode(XmlTextReader Reader, string Tag)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute.IsNullOrEmpty())
			{
				return null;
			}
			Dictionary<string, object> parameterBuffer = ZonePartBlueprint.GetParameterBuffer();
			while (Reader.MoveToNextAttribute())
			{
				string name = Reader.Name;
				if (name != "Name")
				{
					parameterBuffer[name] = Reader.Value;
				}
			}
			Reader.MoveToElement();
			ZonePartBlueprint zonePartBlueprint = ZonePartBlueprint.Get(attribute, parameterBuffer);
			if (Reader.NodeType != XmlNodeType.EndElement && !Reader.IsEmptyElement)
			{
				while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || (!(Reader.Name == "") && !(Reader.Name == Tag))))
				{
				}
			}
			return zonePartBlueprint;
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x0020206C File Offset: 0x0020026C
		public void LoadPropertyNode<T>(XmlTextReader Reader, Action<string, object> Setter)
		{
			try
			{
				string attribute = Reader.GetAttribute("Name");
				object obj = Convert.ChangeType(Reader.GetAttribute("Value"), typeof(T));
				Setter(attribute, obj);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException(string.Format("Error reading {0} at {1},{2}", Reader.Name, Reader.LineNumber, Reader.LinePosition), ex, "game_exception");
			}
			Reader.SkipToEnd();
		}

		// Token: 0x04002FAE RID: 12206
		public const string ANY_PLANE = "*";

		// Token: 0x04002FAF RID: 12207
		public static WorldFactory _Factory;

		// Token: 0x04002FB0 RID: 12208
		private List<WorldBlueprint> WorldsScratch = new List<WorldBlueprint>();

		// Token: 0x04002FB1 RID: 12209
		private Dictionary<string, WorldBlueprint> Worlds = new Dictionary<string, WorldBlueprint>();

		// Token: 0x04002FB2 RID: 12210
		private Dictionary<string, CellBlueprint> Cells = new Dictionary<string, CellBlueprint>();

		// Token: 0x04002FB3 RID: 12211
		public Dictionary<string, string> ZoneIDToDisplay = new Dictionary<string, string>();

		// Token: 0x04002FB4 RID: 12212
		public Dictionary<string, string> ZoneDisplayToID = new Dictionary<string, string>();

		// Token: 0x04002FB5 RID: 12213
		public Dictionary<string, string> ZoneIDToDisplayWithIndefiniteArticle = new Dictionary<string, string>();

		// Token: 0x04002FB6 RID: 12214
		public Dictionary<string, string> ZoneDisplayToIDWithIndefiniteArticle = new Dictionary<string, string>();

		// Token: 0x04002FB7 RID: 12215
		public List<IWorldBuilderExtension> WorldBuilderExtensions;

		// Token: 0x04002FB8 RID: 12216
		public WorldBlueprint NewWorld;
	}
}
