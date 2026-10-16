using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XRL.Collections;

namespace XRL.EditorFormats.Map
{
	// Token: 0x020005C4 RID: 1476
	[HasModSensitiveStaticCache]
	public class MapFile
	{
		// Token: 0x06003EDC RID: 16092 RVA: 0x00134F5C File Offset: 0x0013315C
		[ModSensitiveCacheInit]
		public static void Reset()
		{
			if (MapFile.Initialized)
			{
				MapFile.Repository.Clear();
			}
			if (MapFile.Repository.IsNullOrEmpty<KeyValuePair<string, Rack<DataFile>>>())
			{
				string text = DataManager.FilePath("");
				foreach (FileInfo fileInfo in new DirectoryInfo(text).GetFiles("*.rpm", SearchOption.AllDirectories))
				{
					MapFile.CacheFile(text, fileInfo.FullName, null);
				}
				foreach (KeyValuePair<string, Rack<DataFile>> keyValuePair in MapFile.Repository)
				{
					string text2;
					Rack<DataFile> rack;
					keyValuePair.Deconstruct(out text2, out rack);
					rack.Sort();
				}
			}
			if (ModManager.Initialized)
			{
				foreach (ModInfo modInfo in ModManager.ActiveMods)
				{
					foreach (ModFile modFile in modInfo.Files)
					{
						if (modFile.Type == ModFileType.Map)
						{
							MapFile.CacheFile(modInfo.Directory.FullName, modFile.OriginalName, modInfo);
						}
					}
				}
				foreach (KeyValuePair<string, Rack<DataFile>> keyValuePair in MapFile.Repository)
				{
					string text2;
					Rack<DataFile> rack;
					keyValuePair.Deconstruct(out text2, out rack);
					rack.Sort();
				}
				MapFile.Initialized = true;
			}
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x00135110 File Offset: 0x00133310
		private static void CacheFile(string Root, string Path, ModInfo Mod = null)
		{
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(Path))
				{
					while (xmlReader.Read())
					{
						if (!xmlReader.Name.IsNullOrEmpty() && !(xmlReader.Name == "xml"))
						{
							string attribute = xmlReader.GetAttribute("ID");
							ReadOnlySpan<char> key = MapFile.GetKey(attribute.IsNullOrEmpty() ? Path.GetRelativePath(Root, Path) : attribute);
							Rack<DataFile> rack;
							if (!MapFile.Repository.TryGetValue(key, out rack))
							{
								rack = (MapFile.Repository[key] = new Rack<DataFile>());
							}
							int num2;
							int num = (int.TryParse(xmlReader.GetAttribute("LoadPriority"), out num2) ? num2 : 0);
							rack.Add(new DataFile
							{
								Path = Path,
								Mod = Mod,
								Priority = num
							});
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				string text = ": ";
				Exception ex2 = ex;
				MetricsManager.LogPotentialModError(Mod, Path + text + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x00135230 File Offset: 0x00133430
		private unsafe static ReadOnlySpan<char> GetKey(ReadOnlySpan<char> Base)
		{
			int num = Base.Length;
			char[] array = MapFile.KeyBuffer.GetArray(num);
			for (int i = 0; i < num; i++)
			{
				char c = (char)(*Base[i]);
				if (c == '.')
				{
					num = i;
					break;
				}
				if (c == '/' || c == '\\' || c == ' ' || c == '-')
				{
					array[i] = '_';
				}
				else
				{
					array[i] = char.ToLowerInvariant(c);
				}
			}
			return array.AsSpan<char>(0, num);
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x001352A1 File Offset: 0x001334A1
		public static bool TryGetFiles(string Key, out Rack<DataFile> Files)
		{
			return MapFile.Repository.TryGetValue(MapFile.GetKey(Key), out Files);
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x001352BE File Offset: 0x001334BE
		// (set) Token: 0x06003EE1 RID: 16097 RVA: 0x001352CC File Offset: 0x001334CC
		public int width
		{
			get
			{
				return this.Cells.width;
			}
			set
			{
				MapFileRegion mapFileRegion = new MapFileRegion(value, this.height);
				mapFileRegion.SetRegion(this.Cells, 0, 0);
				this.Cells = mapFileRegion;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x001352FD File Offset: 0x001334FD
		// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x0013530C File Offset: 0x0013350C
		public int height
		{
			get
			{
				return this.Cells.height;
			}
			set
			{
				MapFileRegion mapFileRegion = new MapFileRegion(this.width, value);
				mapFileRegion.SetRegion(this.Cells, 0, 0);
				this.Cells = mapFileRegion;
			}
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x0013533D File Offset: 0x0013353D
		public int RightmostObject()
		{
			return this.Cells.MaxX(true);
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0013534B File Offset: 0x0013354B
		public int BottommostObject()
		{
			return this.Cells.MaxY(true);
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x0013535C File Offset: 0x0013355C
		public static bool TryResolve(string ID, out MapFile Map)
		{
			Rack<DataFile> rack;
			if (MapFile.TryGetFiles(ID, out rack))
			{
				Map = new MapFile(0, 0);
				foreach (DataFile dataFile in rack)
				{
					Map.LoadFile(dataFile.Path);
				}
				Map.Cells.FillEmptyCells();
				return true;
			}
			Map = null;
			return false;
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x001353D8 File Offset: 0x001335D8
		public static MapFile Resolve(string ID, bool Required = true)
		{
			MapFile mapFile;
			if (MapFile.TryResolve(ID, out mapFile))
			{
				return mapFile;
			}
			if (Required)
			{
				throw new KeyNotFoundException("A map by ID '" + ID + "' could not be found.");
			}
			return null;
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0013540B File Offset: 0x0013360B
		[Obsolete("Use MapFile.Resolve")]
		public static MapFile LoadWithMods(string filename)
		{
			return MapFile.Resolve(filename, true);
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x00135414 File Offset: 0x00133614
		public MapFile(int width = 80, int height = 25)
		{
			this.Cells = new MapFileRegion(width, height);
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x0013542C File Offset: 0x0013362C
		public HashSet<string> UsedBlueprints(HashSet<string> result = null)
		{
			if (result == null)
			{
				result = new HashSet<string>();
			}
			foreach (MapFileObjectReference mapFileObjectReference in this.Cells.AllObjects(null))
			{
				result.Add(mapFileObjectReference.blueprint.Name);
			}
			return result;
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x00135498 File Offset: 0x00133698
		public void FlipHorizontal()
		{
			this.Cells = this.Cells.FlippedHorizontal();
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x001354AB File Offset: 0x001336AB
		public void FlipVertical()
		{
			this.Cells = this.Cells.FlippedVertical();
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x001354BE File Offset: 0x001336BE
		public void NewMap(int width = 80, int height = 25)
		{
			this.Cells = new MapFileRegion(width, height);
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x001354CD File Offset: 0x001336CD
		public MapFile Load(XmlTextReader Reader)
		{
			Reader.WhitespaceHandling = WhitespaceHandling.None;
			while (Reader.Read())
			{
				if (Reader.Name == "Map")
				{
					this.LoadMapNode(Reader, false);
				}
			}
			Reader.Close();
			return this;
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x00135501 File Offset: 0x00133701
		public void LoadFile(string FileName)
		{
			this.Load(new XmlTextReader(FileName));
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x00135510 File Offset: 0x00133710
		private void LoadMapNode(XmlTextReader Reader, bool Mod = false)
		{
			if (this.ID == null)
			{
				this.ID = Reader.GetAttribute("ID");
			}
			bool flag = false;
			string text = Reader.GetAttribute("Load");
			if (text != null && text.EqualsNoCase("merge"))
			{
				flag = true;
			}
			text = Reader.GetAttribute("width");
			if (text != null)
			{
				this.width = Convert.ToInt32(text);
			}
			text = Reader.GetAttribute("height");
			if (text != null)
			{
				this.height = Convert.ToInt32(text);
			}
			while (Reader.Read())
			{
				if (Reader.Name == "cell")
				{
					this.LoadCellNode(Reader, flag);
				}
			}
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x001355B0 File Offset: 0x001337B0
		private void LoadCellNode(XmlTextReader Reader, bool Merge = false)
		{
			int num = Convert.ToInt32(Reader.GetAttribute("X"));
			int num2 = Convert.ToInt32(Reader.GetAttribute("Y"));
			if (num + 1 > this.width)
			{
				this.width = num + 1;
			}
			if (num2 + 1 > this.height)
			{
				this.height = num2 + 1;
			}
			MapFileCell orCreateCellAt = this.Cells.GetOrCreateCellAt(num, num2);
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return;
			}
			if (!Merge)
			{
				orCreateCellAt.Objects.Clear();
			}
			orCreateCellAt.Clear = Reader.GetAttribute("Clear").EqualsNoCase("true");
			while (Reader.Read())
			{
				if (Reader.Name == "object")
				{
					orCreateCellAt.Objects.Add(this.LoadObjectNode(Reader));
				}
				if (Reader.NodeType == XmlNodeType.EndElement && (Reader.Name == "" || Reader.Name == "cell"))
				{
					return;
				}
			}
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x001356B0 File Offset: 0x001338B0
		private MapFileObjectBlueprint LoadObjectNode(XmlTextReader Reader)
		{
			MapFileObjectBlueprint mapFileObjectBlueprint = new MapFileObjectBlueprint("", null, null);
			mapFileObjectBlueprint.Name = Reader.GetAttribute("Name");
			mapFileObjectBlueprint.Owner = Reader.GetAttribute("Owner");
			mapFileObjectBlueprint.Part = Reader.GetAttribute("Part");
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return mapFileObjectBlueprint;
			}
			while (Reader.Read())
			{
				if (Reader.Name == "property")
				{
					MapFileObjectBlueprint mapFileObjectBlueprint2 = mapFileObjectBlueprint;
					if (mapFileObjectBlueprint2.Properties == null)
					{
						mapFileObjectBlueprint2.Properties = new Dictionary<string, string>();
					}
					mapFileObjectBlueprint.Properties[Reader.GetAttribute("Name")] = Reader.GetAttribute("Value");
					Reader.SkipToEnd();
				}
				else if (Reader.Name == "intproperty")
				{
					MapFileObjectBlueprint mapFileObjectBlueprint2 = mapFileObjectBlueprint;
					if (mapFileObjectBlueprint2.IntProperties == null)
					{
						mapFileObjectBlueprint2.IntProperties = new Dictionary<string, int>();
					}
					mapFileObjectBlueprint.IntProperties[Reader.GetAttribute("Name")] = int.Parse(Reader.GetAttribute("Value"));
					Reader.SkipToEnd();
				}
				else if (Reader.NodeType == XmlNodeType.EndElement && (Reader.Name == "" || Reader.Name == "object" || Reader.Name == "cell"))
				{
					return mapFileObjectBlueprint;
				}
			}
			return null;
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x0013580C File Offset: 0x00133A0C
		public void Save(string FileName)
		{
			XmlWriter xmlWriter = XmlWriter.Create(FileName, new XmlWriterSettings
			{
				Indent = true
			});
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("Map");
			if (!this.ID.IsNullOrEmpty())
			{
				xmlWriter.WriteAttributeString("ID", this.ID);
			}
			xmlWriter.WriteAttributeString("Width", this.width.ToString());
			xmlWriter.WriteAttributeString("Height", this.height.ToString());
			foreach (MapFileCellReference mapFileCellReference in this.Cells.AllCells(null))
			{
				xmlWriter.WriteStartElement("cell");
				xmlWriter.WriteAttributeString("X", mapFileCellReference.x.ToString());
				xmlWriter.WriteAttributeString("Y", mapFileCellReference.y.ToString());
				if (mapFileCellReference.cell.Clear)
				{
					xmlWriter.WriteAttributeString("Clear", "true");
				}
				foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFileCellReference.cell.Objects)
				{
					xmlWriter.WriteStartElement("object");
					xmlWriter.WriteAttributeString("Name", mapFileObjectBlueprint.Name);
					if (!mapFileObjectBlueprint.Owner.IsNullOrEmpty())
					{
						xmlWriter.WriteAttributeString("Owner", mapFileObjectBlueprint.Owner);
					}
					if (!mapFileObjectBlueprint.Part.IsNullOrEmpty())
					{
						xmlWriter.WriteAttributeString("Part", mapFileObjectBlueprint.Part);
					}
					if (!mapFileObjectBlueprint.Properties.IsNullOrEmpty<KeyValuePair<string, string>>())
					{
						foreach (KeyValuePair<string, string> keyValuePair in mapFileObjectBlueprint.Properties)
						{
							xmlWriter.WriteStartElement("property");
							xmlWriter.WriteAttributeString("Name", keyValuePair.Key);
							xmlWriter.WriteAttributeString("Value", keyValuePair.Value);
							xmlWriter.WriteEndElement();
						}
					}
					if (!mapFileObjectBlueprint.IntProperties.IsNullOrEmpty<KeyValuePair<string, int>>())
					{
						foreach (KeyValuePair<string, int> keyValuePair2 in mapFileObjectBlueprint.IntProperties)
						{
							xmlWriter.WriteStartElement("intproperty");
							xmlWriter.WriteAttributeString("Name", keyValuePair2.Key);
							xmlWriter.WriteAttributeString("Value", keyValuePair2.Value.ToString());
							xmlWriter.WriteEndElement();
						}
					}
					xmlWriter.WriteFullEndElement();
				}
				xmlWriter.WriteFullEndElement();
			}
			xmlWriter.WriteFullEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Flush();
			xmlWriter.Close();
		}

		// Token: 0x04001F11 RID: 7953
		private static StringMap<Rack<DataFile>> Repository = new StringMap<Rack<DataFile>>();

		// Token: 0x04001F12 RID: 7954
		private static Rack<char> KeyBuffer = new Rack<char>(64);

		// Token: 0x04001F13 RID: 7955
		private static bool Initialized;

		// Token: 0x04001F14 RID: 7956
		public string ID;

		// Token: 0x04001F15 RID: 7957
		public MapFileRegion Cells;
	}
}
