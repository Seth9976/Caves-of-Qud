using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Platform.IO;

namespace XRL
{
	// Token: 0x02000500 RID: 1280
	public static class BuildLibrary
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06003720 RID: 14112 RVA: 0x0010AE2F File Offset: 0x0010902F
		public static List<BuildEntry> BuildEntries
		{
			get
			{
				if (BuildLibrary._BuildEntries == null)
				{
					BuildLibrary.Init();
				}
				return BuildLibrary._BuildEntries;
			}
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x0010AE44 File Offset: 0x00109044
		public static void Init()
		{
			if (BuildLibrary._BuildEntries != null)
			{
				return;
			}
			string text = DataManager.SyncedPath("BuildLibrary.json");
			BuildLibrary._BuildEntries = new List<BuildEntry>();
			if (File.Exists(text))
			{
				BuildLibrary._BuildEntries = JsonConvert.DeserializeObject<List<BuildEntry>>(File.ReadAllText(text));
			}
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x0010AE86 File Offset: 0x00109086
		private static void Save()
		{
			if (BuildLibrary.BuildEntries == null)
			{
				return;
			}
			File.WriteAllText(DataManager.SyncedPath("BuildLibrary.json"), JsonConvert.SerializeObject(BuildLibrary.BuildEntries));
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x0010AEAC File Offset: 0x001090AC
		public static bool AddBuild(string Code, string Name, string Tile = null, string Foreground = null, string Detail = null)
		{
			if (string.IsNullOrEmpty(Name))
			{
				return false;
			}
			if (BuildLibrary.BuildEntries == null)
			{
				BuildLibrary.Init();
			}
			if (BuildLibrary.HasBuild(Code))
			{
				return false;
			}
			BuildEntry buildEntry = new BuildEntry();
			buildEntry.Code = Code;
			buildEntry.Name = Name;
			buildEntry.Tile = Tile;
			buildEntry.Foreground = Foreground;
			buildEntry.Detail = Detail;
			BuildLibrary.BuildEntries.Add(buildEntry);
			BuildLibrary.Save();
			return true;
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x0010AF14 File Offset: 0x00109114
		public static BuildEntry GetBuild(string Code)
		{
			if (BuildLibrary.BuildEntries == null)
			{
				BuildLibrary.Init();
			}
			for (int i = 0; i < BuildLibrary.BuildEntries.Count; i++)
			{
				if (BuildLibrary.BuildEntries[i].Code == Code)
				{
					return BuildLibrary.BuildEntries[i];
				}
			}
			return null;
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x0010AF68 File Offset: 0x00109168
		public static bool HasBuild(string Code)
		{
			if (BuildLibrary.BuildEntries == null)
			{
				BuildLibrary.Init();
			}
			for (int i = 0; i < BuildLibrary.BuildEntries.Count; i++)
			{
				if (BuildLibrary.BuildEntries[i].Code == Code)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x0010AFB4 File Offset: 0x001091B4
		public static void UpdateBuild(BuildEntry Info)
		{
			if (BuildLibrary.BuildEntries == null)
			{
				BuildLibrary.Init();
			}
			for (int i = 0; i < BuildLibrary.BuildEntries.Count; i++)
			{
				if (BuildLibrary.BuildEntries[i].Code == Info.Code)
				{
					BuildLibrary.BuildEntries[i].Name = Info.Name;
					BuildLibrary.BuildEntries[i].Tile = Info.Tile;
					BuildLibrary.BuildEntries[i].Foreground = Info.Foreground;
					BuildLibrary.BuildEntries[i].Detail = Info.Detail;
					BuildLibrary.Save();
					return;
				}
			}
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x0010B060 File Offset: 0x00109260
		public static void DeleteBuild(string Code)
		{
			if (BuildLibrary.BuildEntries == null)
			{
				BuildLibrary.Init();
			}
			for (int i = 0; i < BuildLibrary.BuildEntries.Count; i++)
			{
				if (BuildLibrary.BuildEntries[i].Code == Code)
				{
					BuildLibrary.BuildEntries.RemoveAt(i);
					BuildLibrary.Save();
					return;
				}
			}
		}

		// Token: 0x04001B72 RID: 7026
		public static List<BuildEntry> _BuildEntries;
	}
}
