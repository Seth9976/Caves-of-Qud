using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using ConsoleLib.Console;
using Genkit;
using HistoryKit;
using Mono.Data.Sqlite;
using Qud.API;
using UnityEngine;
using XRL.Collections;
using XRL.Core;
using XRL.Language;
using XRL.Liquids;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.World.Biomes;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.ZoneBuilders;

namespace XRL.World
{
	// Token: 0x02000996 RID: 2454
	[HasGameBasedStaticCache]
	[Serializable]
	public class ZoneManager
	{
		// Token: 0x06006902 RID: 26882 RVA: 0x0020F4AC File Offset: 0x0020D6AC
		public ZoneManager()
		{
		}

		// Token: 0x06006903 RID: 26883 RVA: 0x0020F534 File Offset: 0x0020D734
		public ZoneManager(XRLGame Game)
		{
			this.Game = Game;
		}

		// Token: 0x06006904 RID: 26884 RVA: 0x0020F5C4 File Offset: 0x0020D7C4
		public GameObject FindObjectByID(string ID)
		{
			GameObject gameObject = this.ActiveZone.FindObjectByID(ID);
			if (gameObject != null)
			{
				return gameObject;
			}
			foreach (Zone zone in this.CachedZones.Values)
			{
				if (zone != this.ActiveZone)
				{
					gameObject = zone.FindObjectByID(ID);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x0020F644 File Offset: 0x0020D844
		public GameObject FindObjectByID(int ID)
		{
			GameObject gameObject = this.ActiveZone.FindObjectByID(ID);
			if (gameObject != null)
			{
				return gameObject;
			}
			foreach (Zone zone in this.CachedZones.Values)
			{
				if (zone != this.ActiveZone)
				{
					gameObject = zone.FindObjectByID(ID);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x0020F6C4 File Offset: 0x0020D8C4
		public void FindObjects(List<GameObject> Store, Predicate<GameObject> Filter = null)
		{
			if (this.ActiveZone == null)
			{
				return;
			}
			this.ActiveZone.FindObjects(Store, Filter);
			foreach (Zone zone in this.CachedZones.Values)
			{
				if (zone != this.ActiveZone)
				{
					zone.FindObjects(Store, Filter);
				}
			}
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x0020F73C File Offset: 0x0020D93C
		public List<GameObject> FindObjects(Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = new List<GameObject>();
			this.FindObjects(list, Filter);
			return list;
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x0020F758 File Offset: 0x0020D958
		public List<GameObject> FindObjectsReadonly(Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.FindObjects(list, Filter);
			return list;
		}

		// Token: 0x06006909 RID: 26889 RVA: 0x0020F774 File Offset: 0x0020D974
		public void Release()
		{
			foreach (KeyValuePair<string, Zone> keyValuePair in this.CachedZones)
			{
				keyValuePair.Value.Release();
			}
			if (!this.TerrainSamples.IsNullOrEmpty<KeyValuePair<string, GameObject>>())
			{
				foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.TerrainSamples)
				{
					keyValuePair2.Value.Pool();
				}
				this.TerrainSamples.Clear();
			}
			this.Graveyard.ReleaseObjects();
			Zone activeZone = this.ActiveZone;
			if (activeZone != null)
			{
				activeZone.Release();
			}
			this.CachedZones.Clear();
			this.ActiveZone = null;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600690A RID: 26890 RVA: 0x0020F85C File Offset: 0x0020DA5C
		public static ZoneManager instance
		{
			get
			{
				return The.Game.ZoneManager;
			}
		}

		// Token: 0x0600690B RID: 26891 RVA: 0x0020F868 File Offset: 0x0020DA68
		public bool CachedZonesContains(string ZoneID)
		{
			if (this.CachedZones == null)
			{
				return false;
			}
			foreach (KeyValuePair<string, Zone> keyValuePair in this.CachedZones)
			{
				string key = keyValuePair.Key;
				if (ZoneID == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600690C RID: 26892 RVA: 0x0020F8D0 File Offset: 0x0020DAD0
		public void AddCachedZone(Zone Zone)
		{
			this.CachedZones[Zone.ZoneID] = Zone;
			Zone.LastCached = XRLCore.CurrentTurn;
		}

		// Token: 0x0600690D RID: 26893 RVA: 0x0020F8F0 File Offset: 0x0020DAF0
		public GameObject peekCachedObject(string ID)
		{
			GameObject gameObject;
			if (ID != null && this.CachedObjects.TryGetValue(ID, out gameObject))
			{
				return gameObject;
			}
			return null;
		}

		// Token: 0x0600690E RID: 26894 RVA: 0x0020F914 File Offset: 0x0020DB14
		public GameObject PullCachedObject(string ID, bool DeepCopy = true)
		{
			GameObject gameObject;
			if (ID != null && this.CachedObjects.TryGetValue(ID, out gameObject))
			{
				GameObject gameObject2 = (DeepCopy ? gameObject.DeepCopy(false, true, null) : gameObject);
				this.CachedObjects.Remove(ID);
				return gameObject2;
			}
			return null;
		}

		// Token: 0x0600690F RID: 26895 RVA: 0x0020F952 File Offset: 0x0020DB52
		public void UncacheObject(string ID)
		{
			this.CachedObjects.Remove(ID);
		}

		// Token: 0x06006910 RID: 26896 RVA: 0x0020F964 File Offset: 0x0020DB64
		public GameObject GetCachedObjects(string ID)
		{
			if (ID == null || !this.CachedObjects.ContainsKey(ID))
			{
				GameObject gameObject = GameObject.Create("PhysicalObject");
				gameObject.Render.DisplayName = "INVALID CACHE OBJECT: " + ID;
				gameObject.Render.ColorString = "&M^W";
				gameObject.Render.DetailColor = "W";
				return gameObject;
			}
			this.CachedObjectsToRemoveAfterZoneBuild.Add(ID);
			return this.CachedObjects[ID].DeepCopy(false, true, null);
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x0020F9E4 File Offset: 0x0020DBE4
		public string CacheObject(GameObject GO, bool cacheTwiceOk = false, bool replaceIfAlreadyCached = false)
		{
			if (!this.CachedObjects.ContainsKey(GO.ID))
			{
				this.CachedObjects.Add(GO.ID, GO);
				return GO.ID;
			}
			if (replaceIfAlreadyCached)
			{
				this.CachedObjects[GO.ID] = GO;
				return GO.ID;
			}
			if (!cacheTwiceOk)
			{
				global::UnityEngine.Debug.LogWarning("Did you mean to cache " + GO.DisplayName + " twice?");
			}
			return GO.ID;
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x0020FA5C File Offset: 0x0020DC5C
		public static string GetObjectTypeForZone(string zoneId)
		{
			string text;
			int num;
			int num2;
			if (!ZoneID.Parse(zoneId, out text, out num, out num2))
			{
				return "";
			}
			Cell cell = The.Game.ZoneManager.GetZone(text).GetCell(num, num2);
			if (cell == null)
			{
				return "";
			}
			GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("TerrainTravel");
			if (firstObjectWithPart == null)
			{
				return "";
			}
			return firstObjectWithPart.Blueprint;
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x0020FABC File Offset: 0x0020DCBC
		public static string GetObjectTypeForZone(int wx, int wy, string World)
		{
			Cell cell = The.Game.ZoneManager.GetZone(World).GetCell(wx, wy);
			if (cell == null)
			{
				return "";
			}
			GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("TerrainTravel");
			if (firstObjectWithPart == null)
			{
				return "";
			}
			return firstObjectWithPart.Blueprint;
		}

		// Token: 0x06006914 RID: 26900 RVA: 0x0020FB05 File Offset: 0x0020DD05
		public static GameObject GetTerrainObjectForZone(int wx, int wy, string World)
		{
			Zone zone = The.Game.ZoneManager.GetZone(World);
			if (zone == null)
			{
				return null;
			}
			Cell cell = zone.GetCell(wx, wy);
			if (cell == null)
			{
				return null;
			}
			return cell.GetFirstObjectWithPart("TerrainTravel");
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x0020FB34 File Offset: 0x0020DD34
		public GameObject GetTerrainSample(string Terrain)
		{
			if (this.TerrainSamples != null)
			{
				GameObject gameObject;
				if (this.TerrainSamples.TryGetValue(Terrain, out gameObject))
				{
					return gameObject;
				}
			}
			else
			{
				this.TerrainSamples = new Dictionary<string, GameObject>();
			}
			GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint("Terrain" + Terrain);
			if (blueprint != null)
			{
				GameObject gameObject2 = GameObjectFactory.Factory.CreateSampleObject(blueprint, null);
				this.TerrainSamples[Terrain] = gameObject2;
				return gameObject2;
			}
			return null;
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x0020FB9C File Offset: 0x0020DD9C
		public static Location2D GetWorldMapLocationForZoneID(string zoneID)
		{
			if (zoneID == null)
			{
				return Location2D.Invalid;
			}
			string text;
			int num;
			int num2;
			ZoneID.Parse(zoneID, out text, out num, out num2);
			return Location2D.Get(num, num2);
		}

		// Token: 0x06006917 RID: 26903 RVA: 0x0020FBC8 File Offset: 0x0020DDC8
		public static string GetRegionForZone(Zone z)
		{
			Cell cell = The.Game.ZoneManager.GetZone(z.GetZoneWorld()).GetCell(z.wX, z.wY);
			if (cell == null)
			{
				return null;
			}
			GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("TerrainTravel");
			if (firstObjectWithPart == null)
			{
				return null;
			}
			return firstObjectWithPart.GetTag("Terrain", firstObjectWithPart.Blueprint);
		}

		// Token: 0x06006918 RID: 26904 RVA: 0x0020FC23 File Offset: 0x0020DE23
		public IEnumerable<ZoneBuilderBlueprint> GetBuildersFor(Zone Z)
		{
			return this.GetBuildersFor(Z.ZoneID);
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x0020FC31 File Offset: 0x0020DE31
		public IEnumerable<ZoneBuilderBlueprint> GetBuildersFor(string ZoneID)
		{
			return this.GetBuilderCollection(ZoneID);
		}

		// Token: 0x0600691A RID: 26906 RVA: 0x0020FC3C File Offset: 0x0020DE3C
		public int CountPartsFor(string ZoneID, Predicate<ZonePartBlueprint> Filter = null)
		{
			int num = 0;
			ZonePartCollection zonePartCollection;
			if (this.ZoneParts.TryGetValue(ZoneID, out zonePartCollection))
			{
				num = ((Filter == null) ? zonePartCollection.Count : zonePartCollection.Count<ZonePartBlueprint>(new Func<ZonePartBlueprint, bool>(Filter.Invoke)));
			}
			return num;
		}

		// Token: 0x0600691B RID: 26907 RVA: 0x0020FC7C File Offset: 0x0020DE7C
		public int CountBuildersFor(string ZoneID, Predicate<ZoneBuilderBlueprint> Filter = null)
		{
			int num = 0;
			ZoneBuilderCollection zoneBuilderCollection;
			if (this.ZoneBuilders.TryGetValue(ZoneID, out zoneBuilderCollection))
			{
				num = ((Filter == null) ? zoneBuilderCollection.Count : zoneBuilderCollection.Count<ZoneBuilderBlueprint>(new Func<ZoneBuilderBlueprint, bool>(Filter.Invoke)));
			}
			return num;
		}

		// Token: 0x0600691C RID: 26908 RVA: 0x0020FCBC File Offset: 0x0020DEBC
		public bool ZoneHasPart(string ZoneID, string Name)
		{
			ZonePartCollection zonePartCollection;
			return this.ZoneParts.TryGetValue(ZoneID, out zonePartCollection) && zonePartCollection.Any<ZonePartBlueprint>((ZonePartBlueprint x) => x.Name == Name);
		}

		// Token: 0x0600691D RID: 26909 RVA: 0x0020FCFC File Offset: 0x0020DEFC
		public bool ZoneHasBuilder(string ZoneID, string Class)
		{
			ZoneBuilderCollection zoneBuilderCollection;
			return this.ZoneBuilders.TryGetValue(ZoneID, out zoneBuilderCollection) && zoneBuilderCollection.Any<ZoneBuilderBlueprint>((ZoneBuilderBlueprint x) => x.Class == Class);
		}

		// Token: 0x0600691E RID: 26910 RVA: 0x0020FD3C File Offset: 0x0020DF3C
		public void Save(SerializationWriter Writer)
		{
			Writer.WriteFields<ZoneManager>(this, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
			Writer.Write(this.CachedZones.Count);
			foreach (KeyValuePair<string, Zone> keyValuePair in this.CachedZones)
			{
				Zone.Save(Writer, keyValuePair.Value);
			}
			Writer.Write(this.LastZoneTransition);
			Writer.Write(ZoneManager.FrozenZones.Count);
			foreach (string text in ZoneManager.FrozenZones)
			{
				Writer.WriteOptimized(text);
			}
			Writer.Write(this.PinnedZones.Count);
			foreach (string text2 in this.PinnedZones)
			{
				Writer.WriteOptimized(text2);
			}
			Writer.Write(this.VisitedTime.Count);
			foreach (KeyValuePair<string, long> keyValuePair2 in this.VisitedTime)
			{
				Writer.WriteOptimized(keyValuePair2.Key);
				Writer.WriteOptimized(keyValuePair2.Value);
			}
			Writer.Write(ZoneManager.Ticker);
			Writer.Write(this.ZoneConnections.Count);
			foreach (KeyValuePair<string, List<ZoneConnection>> keyValuePair3 in this.ZoneConnections)
			{
				Writer.WriteOptimized(keyValuePair3.Key);
				Writer.WriteOptimized(keyValuePair3.Value.Count);
				foreach (ZoneConnection zoneConnection in keyValuePair3.Value)
				{
					Writer.WriteOptimized(zoneConnection.Object);
					Writer.WriteOptimized(zoneConnection.Type);
					Writer.WriteOptimized(zoneConnection.X);
					Writer.WriteOptimized(zoneConnection.Y);
				}
			}
			Writer.Write(this.ZoneProperties.Count);
			foreach (KeyValuePair<string, Dictionary<string, object>> keyValuePair4 in this.ZoneProperties)
			{
				Writer.WriteOptimized(keyValuePair4.Key);
				Writer.WriteOptimized(keyValuePair4.Value.Count);
				foreach (KeyValuePair<string, object> keyValuePair5 in keyValuePair4.Value)
				{
					Writer.WriteOptimized(keyValuePair5.Key);
					Writer.WriteObject(keyValuePair5.Value);
				}
			}
			Writer.Write(this.ActiveZone.ZoneID);
			Writer.Write(this.CachedObjects.Count);
			foreach (KeyValuePair<string, GameObject> keyValuePair6 in this.CachedObjects)
			{
				Writer.WriteOptimized(keyValuePair6.Key);
				if (keyValuePair6.Value.Physics != null)
				{
					keyValuePair6.Value.Physics._CurrentCell = null;
					keyValuePair6.Value.Physics._Equipped = null;
					keyValuePair6.Value.Physics._InInventory = null;
				}
				Writer.WriteGameObject(keyValuePair6.Value);
			}
			Hills.Save(Writer);
			BananaGrove.Save(Writer);
			Watervine.Save(Writer);
			Cave.Save(Writer);
			this.SaveZoneParts(Writer);
			this.SaveZoneBuilders(Writer);
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x00210168 File Offset: 0x0020E368
		public void SaveZoneParts(SerializationWriter Writer)
		{
			try
			{
				Writer.Write(this.ZoneParts.Count);
				foreach (ZonePartCollection zonePartCollection in this.ZoneParts.Values)
				{
					ZonePartCollection.Save(Writer, zonePartCollection);
				}
			}
			finally
			{
				ZonePartBlueprint.ClearTokens();
			}
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x002101E4 File Offset: 0x0020E3E4
		public void SaveZoneBuilders(SerializationWriter Writer)
		{
			try
			{
				Writer.Write(this.ZoneBuilders.Count);
				foreach (ZoneBuilderCollection zoneBuilderCollection in this.ZoneBuilders.Values)
				{
					ZoneBuilderCollection.Save(Writer, zoneBuilderCollection);
				}
			}
			finally
			{
				ZoneBuilderBlueprint.ClearTokens();
			}
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x00210260 File Offset: 0x0020E460
		public void LoadZoneParts(SerializationReader Reader)
		{
			try
			{
				int num = Reader.ReadInt32();
				List<ZonePartBlueprint> list = new List<ZonePartBlueprint>(num);
				if (this.ZoneParts == null)
				{
					this.ZoneParts = new Dictionary<string, ZonePartCollection>(num);
				}
				this.ZoneParts.EnsureCapacity(num);
				for (int i = 0; i < num; i++)
				{
					ZonePartCollection zonePartCollection = ZonePartCollection.Load(Reader, list);
					this.ZoneParts[zonePartCollection.ZoneID] = zonePartCollection;
				}
			}
			finally
			{
				ZonePartBlueprint.ClearOrphaned();
				ZonePartBlueprint.ClearTokens();
			}
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x002102E0 File Offset: 0x0020E4E0
		public void LoadZoneBuilders(SerializationReader Reader)
		{
			try
			{
				int num = Reader.ReadInt32();
				List<ZoneBuilderBlueprint> list = new List<ZoneBuilderBlueprint>(num);
				if (this.ZoneBuilders == null)
				{
					this.ZoneBuilders = new Dictionary<string, ZoneBuilderCollection>(num);
				}
				this.ZoneBuilders.EnsureCapacity(num);
				for (int i = 0; i < num; i++)
				{
					ZoneBuilderCollection zoneBuilderCollection = ZoneBuilderCollection.Load(Reader, list);
					this.ZoneBuilders[zoneBuilderCollection.ZoneID] = zoneBuilderCollection;
				}
			}
			finally
			{
				ZoneBuilderBlueprint.ClearOrphaned();
				ZoneBuilderBlueprint.ClearTokens();
			}
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x00210360 File Offset: 0x0020E560
		public static ZoneManager Load(XRLGame Game, SerializationReader Reader)
		{
			ZoneManager zoneManager = Reader.ReadInstanceFields<ZoneManager>(BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
			int num = Reader.ReadInt32();
			zoneManager.Game = Game;
			zoneManager.CachedZones.Clear();
			zoneManager.CachedZones.EnsureCapacity(num);
			for (int i = 0; i < num; i++)
			{
				Zone zone = Zone.Load(Reader, null);
				if (!zoneManager.CachedZones.TryAdd(zone.ZoneID, zone))
				{
					MetricsManager.LogWarning("Duplicate zone ID found on load: " + zone.ZoneID);
				}
			}
			zoneManager.LastZoneTransition = Reader.ReadInt64();
			num = Reader.ReadInt32();
			ZoneManager.FrozenZones.Clear();
			ZoneManager.FrozenZones.EnsureCapacity(num);
			for (int j = 0; j < num; j++)
			{
				ZoneManager.FrozenZones.Add(Reader.ReadOptimizedString());
			}
			num = Reader.ReadInt32();
			zoneManager.PinnedZones.Clear();
			zoneManager.PinnedZones.EnsureCapacity<string>(num);
			for (int k = 0; k < num; k++)
			{
				zoneManager.PinnedZones.Add(Reader.ReadOptimizedString());
			}
			num = Reader.ReadInt32();
			zoneManager.VisitedTime.Clear();
			zoneManager.VisitedTime.EnsureCapacity(num);
			for (int l = 0; l < num; l++)
			{
				zoneManager.VisitedTime[Reader.ReadOptimizedString()] = Reader.ReadOptimizedInt64();
			}
			ZoneManager.Ticker = Reader.ReadInt64();
			num = Reader.ReadInt32();
			zoneManager.ZoneConnections.Clear();
			zoneManager.ZoneConnections.EnsureCapacity(num);
			for (int m = 0; m < num; m++)
			{
				string text = Reader.ReadOptimizedString();
				int num2 = Reader.ReadOptimizedInt32();
				List<ZoneConnection> list = new List<ZoneConnection>(num2);
				for (int n = 0; n < num2; n++)
				{
					list.Add(new ZoneConnection
					{
						Object = Reader.ReadOptimizedString(),
						Type = Reader.ReadOptimizedString(),
						X = Reader.ReadOptimizedInt32(),
						Y = Reader.ReadOptimizedInt32()
					});
				}
				zoneManager.ZoneConnections.Add(text, list);
			}
			num = Reader.ReadInt32();
			zoneManager.ZoneProperties.Clear();
			zoneManager.ZoneProperties.EnsureCapacity(num);
			for (int num3 = 0; num3 < num; num3++)
			{
				string text2 = Reader.ReadOptimizedString();
				int num4 = Reader.ReadOptimizedInt32();
				Dictionary<string, object> dictionary = new Dictionary<string, object>(num4);
				for (int num5 = 0; num5 < num4; num5++)
				{
					dictionary[Reader.ReadOptimizedString()] = Reader.ReadObject();
				}
				zoneManager.ZoneProperties[text2] = dictionary;
			}
			zoneManager.ActiveZone = zoneManager.GetZone(Reader.ReadOptimizedString());
			num = Reader.ReadInt32();
			zoneManager.CachedObjects.Clear();
			zoneManager.CachedObjects.EnsureCapacity(num);
			for (int num6 = 0; num6 < num; num6++)
			{
				string text3 = Reader.ReadOptimizedString();
				GameObject gameObject = Reader.ReadGameObject("zone cache");
				zoneManager.CachedObjects[text3] = gameObject;
				if (gameObject.Physics != null)
				{
					gameObject.Physics._CurrentCell = null;
					gameObject.Physics._Equipped = null;
					gameObject.Physics._InInventory = null;
				}
			}
			Hills.Load(Reader);
			BananaGrove.Load(Reader);
			Watervine.Load(Reader);
			Cave.Load(Reader);
			zoneManager.LoadZoneParts(Reader);
			zoneManager.LoadZoneBuilders(Reader);
			return zoneManager;
		}

		// Token: 0x06006924 RID: 26916 RVA: 0x002106A4 File Offset: 0x0020E8A4
		public static long CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[2048];
			long num = 0L;
			int num2;
			while ((num2 = source.Read(array, 0, array.Length)) > 0)
			{
				destination.Write(array, 0, num2);
				num += (long)num2;
			}
			return num;
		}

		// Token: 0x06006925 RID: 26917 RVA: 0x002106E0 File Offset: 0x0020E8E0
		public static void ForceCollect()
		{
			MemoryHelper.GCCollectMax();
		}

		// Token: 0x06006926 RID: 26918 RVA: 0x002106E7 File Offset: 0x0020E8E7
		public void ForceTryThawZone(string ZoneID, out Zone Zone)
		{
			if (!ZoneManager.FrozenZones.Contains(ZoneID))
			{
				ZoneManager.FrozenZones.Add(ZoneID);
			}
			this.TryThawZone(ZoneID, out Zone);
		}

		// Token: 0x06006927 RID: 26919 RVA: 0x0021070C File Offset: 0x0020E90C
		public bool TryThawZone(string ZoneID, out Zone Zone)
		{
			bool flag = ZoneManager.FrozenZones.Contains(ZoneID);
			if (!flag && !Options.DisableZoneThawRestrict)
			{
				Zone = null;
				return false;
			}
			if (!ZoneManager.ProcessingZones.Add(ZoneID))
			{
				MetricsManager.LogError("Attempting to thaw reserved zone.");
				Zone = null;
				return false;
			}
			Loading.Status status = Loading.StartTask("Thawing zone...", "Thawing " + ZoneID, true);
			bool flag2;
			try
			{
				SerializationReader serializationReader = SerializationReader.Get();
				long num = -1L;
				try
				{
					using (PooledList<byte> pooledList = new PooledList<byte>(2048))
					{
						byte[] array = ArrayPool<byte>.Shared.Rent(4096);
						DataManager.CacheOperation cacheOperation = DataManager.StartCacheOperation(false);
						try
						{
							cacheOperation.Command.CommandText = "SELECT Data, FrozenTick FROM FrozenZone WHERE ZoneID = @ZoneID LIMIT 1";
							cacheOperation.Command.Parameters.AddWithValue("ZoneID", ZoneID);
							using (SqliteDataReader sqliteDataReader = cacheOperation.Command.ExecuteReader())
							{
								if (!sqliteDataReader.HasRows)
								{
									if (flag)
									{
										MetricsManager.LogWarning("ZoneCacheMiss: No row by ZoneID '" + ZoneID + "' in database.");
									}
									Zone = null;
									return false;
								}
								if (sqliteDataReader.Read())
								{
									if (sqliteDataReader[1].GetType() != typeof(DBNull))
									{
										num = sqliteDataReader.GetInt64(1);
									}
									int num2 = 0;
									int num3;
									do
									{
										num3 = (int)sqliteDataReader.GetBytes(0, (long)num2, array, 0, array.Length);
										pooledList.AddRange(array.AsSpan<byte>(0, num3));
										num2 += num3;
									}
									while ((long)num3 > 0L);
								}
							}
						}
						finally
						{
							ArrayPool<byte>.Shared.Return(array, true);
							cacheOperation.Dispose();
						}
						int num4 = BitConverter.ToInt32(pooledList.AsSpan(0, 4));
						serializationReader.Stream.SetLength((long)num4);
						array = serializationReader.Stream.GetBuffer();
						try
						{
							int num5;
							if (!BrotliDecoder.TryDecompress(pooledList.AsSpan(4), array, ref num5) || num5 != num4)
							{
								throw new InvalidDataException("Decoded brotli chunk not correct length.");
							}
						}
						catch (ArgumentOutOfRangeException ex)
						{
							MetricsManager.LogError(string.Format("Invalid brotli arguments:: Length: {0}, Buffer: {1}, List: {2}", num4, array.Length, pooledList.Count));
							ExceptionDispatchInfo.Throw(ex);
						}
					}
					serializationReader.Start(false);
					int num6 = serializationReader.ReadInt32();
					if (num6 != 123457)
					{
						MetricsManager.LogError(string.Format("Token mismatch, wanted 123457, got {0}", num6));
					}
					serializationReader.ReadString();
					Zone = Zone.Load(serializationReader, ZoneID);
					this.AddCachedZone(Zone);
					serializationReader.FinalizeRead();
					ZoneManager.ForceCollect();
					if (serializationReader.Errors > 0)
					{
						Popup.DisplayLoadError(serializationReader, "zone", serializationReader.Errors);
					}
				}
				catch (Exception ex2)
				{
					string text = "ZoneManager::TryThawZone::";
					if (ex2 is FileNotFoundException)
					{
						text += "CacheMiss::";
					}
					else
					{
						text += "ReadError::";
					}
					ModInfo modInfo;
					StackFrame stackFrame;
					if (ModManager.TryGetStackMod(ex2, out modInfo, out stackFrame))
					{
						MethodBase method = stackFrame.GetMethod();
						ModInfo modInfo2 = modInfo;
						string[] array2 = new string[5];
						int num7 = 0;
						Type declaringType = method.DeclaringType;
						array2[num7] = ((declaringType != null) ? declaringType.FullName : null);
						array2[1] = "::";
						array2[2] = method.Name;
						array2[3] = "::";
						int num8 = 4;
						Exception ex3 = ex2;
						array2[num8] = ((ex3 != null) ? ex3.ToString() : null);
						modInfo2.Error(string.Concat(array2));
					}
					else
					{
						MetricsManager.LogException(text, ex2, "serialization_error");
					}
					MessageQueue.AddPlayerMessage("ThawZone exception", 'W', true);
					Zone = null;
					this.CachedZones.Remove(ZoneID);
				}
				finally
				{
					SerializationReader.Release(serializationReader);
				}
				if (Zone == null)
				{
					MetricsManager.LogError("Thaw error: " + ZoneID);
					flag2 = false;
				}
				else
				{
					ZoneManager.PaintWalls(Zone, 0, 0, -1, -1);
					ZoneManager.PaintWater(Zone, 0, 0, -1, -1);
					ZoneManager.FrozenZones.Remove(ZoneID);
					long num9 = ((num > 0L) ? (The.Game.TimeTicks - num) : 0L);
					Zone.Thawed(num9);
					global::UnityEngine.Debug.Log("Thaw complete");
					flag2 = true;
				}
			}
			catch (Exception ex4)
			{
				MessageQueue.AddPlayerMessage("ThawZone exception", 'r', true);
				MetricsManager.LogException("ZoneManager::TryThawZone", ex4, "serialization_error");
				Zone = null;
				flag2 = false;
			}
			finally
			{
				ZoneManager.ProcessingZones.Remove(ZoneID);
				status.Dispose();
			}
			return flag2;
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x00210BDC File Offset: 0x0020EDDC
		public void FreezeZone(Zone Z, DataManager.CacheTransaction transaction)
		{
			Z.FireEvent("ZoneFreezing");
			Loading.Status status = Loading.StartTask("Freezing zone...", "Freezing " + Z.ZoneID, true);
			try
			{
				if (!ZoneManager.FreezingZones.CleanContains(Z.ZoneID))
				{
					GameObject player = The.Player;
					if (((player != null) ? player.GetCurrentZone() : null) == Z)
					{
						MetricsManager.LogError(string.Format("[{0}] Attempting to freeze player zone (LastActive: {1}, LastCached: {2}, LastPlayerPresence: {3}).", new object[]
						{
							XRLCore.CurrentTurn,
							Z.LastActive,
							Z.LastCached,
							Z.LastPlayerPresence
						}));
					}
					else
					{
						SerializationWriter serializationWriter = SerializationWriter.Get();
						try
						{
							this.CachedZones.Remove(Z.ZoneID);
							if (ZoneManager.FreezingZones.CleanContains(Z.ZoneID))
							{
								return;
							}
							ZoneManager.FreezingZones.Add(Z.ZoneID);
							serializationWriter.Start(409, false);
							serializationWriter.Write(123457);
							serializationWriter.Write(Assembly.GetExecutingAssembly().GetName().Version.ToString());
							Zone.Save(serializationWriter, Z);
							serializationWriter.FinalizeWrite();
							byte[] buffer = serializationWriter.Stream.GetBuffer();
							int num = (int)serializationWriter.Stream.Position;
							byte[] array = ArrayPool<byte>.Shared.Rent(num + 4);
							try
							{
								int num2;
								if (!BrotliEncoder.TryCompress(buffer.AsSpan<byte>(0, num), array.AsSpan<byte>(4, num), ref num2, 1, 16))
								{
									throw new InvalidDataException("No encoded data captured.");
								}
								BitConverter.TryWriteBytes(array.AsSpan<byte>(0, 4), num);
								SqliteCommand sqliteCommand = transaction.AddCommand();
								sqliteCommand.CommandText = "REPLACE INTO FrozenZone (ZoneID, Data, FrozenTick) VALUES (@ZoneID, @Data, @FrozenTick);";
								sqliteCommand.Parameters.Add(new SqliteParameter
								{
									ParameterName = "ZoneID",
									Value = Z.ZoneID
								});
								sqliteCommand.Parameters.AddWithValue("FrozenTick", The.Game.TimeTicks);
								sqliteCommand.Parameters.Add("Data", DbType.Binary).Value = array.AsSpan<byte>(0, num2 + 4).ToArray();
								sqliteCommand.ExecuteNonQuery();
							}
							finally
							{
								ArrayPool<byte>.Shared.Return(array, true);
							}
							ZoneManager.FrozenZones.Add(Z.ZoneID);
						}
						catch (Exception ex)
						{
							string text = "ZoneManager::FreezeZone::";
							if (ex is IOException)
							{
								text += "IO::";
							}
							else
							{
								text += "WriteError::";
							}
							ModInfo modInfo;
							StackFrame stackFrame;
							if (ModManager.TryGetStackMod(ex, out modInfo, out stackFrame))
							{
								MethodBase method = stackFrame.GetMethod();
								ModInfo modInfo2 = modInfo;
								string[] array2 = new string[5];
								int num3 = 0;
								Type declaringType = method.DeclaringType;
								array2[num3] = ((declaringType != null) ? declaringType.FullName : null);
								array2[1] = "::";
								array2[2] = method.Name;
								array2[3] = "::";
								int num4 = 4;
								Exception ex2 = ex;
								array2[num4] = ((ex2 != null) ? ex2.ToString() : null);
								modInfo2.Error(string.Concat(array2));
							}
							else
							{
								MetricsManager.LogException(text, ex, "serialization_error");
							}
						}
						finally
						{
							SerializationWriter.Release(serializationWriter);
						}
						ZoneManager.FreezingZones.Remove(Z.ZoneID);
						Z.Release();
						if (Options.CollectEarly)
						{
							ZoneManager.ForceCollect();
						}
					}
				}
			}
			catch (Exception ex3)
			{
				MetricsManager.LogException("ZoneManager::FreezeZone", ex3, "serialization_error");
			}
			finally
			{
				status.Dispose();
				ZoneManager.ForceCollect();
				XRLGame game = The.Game;
				if (game != null)
				{
					game.Clean();
				}
			}
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x00210FA8 File Offset: 0x0020F1A8
		public void CheckCached(bool AllowFreeze = true, bool ForceFreeze = false)
		{
			if (ZoneManager.CheckingCache || The.Game.Transient)
			{
				return;
			}
			ZoneManager.CheckingCache = true;
			try
			{
				int freezabilityTurns = Zone.GetFreezabilityTurns();
				int suspendabilityTurns = Zone.GetSuspendabilityTurns();
				if (AllowFreeze)
				{
					this.Graveyard.ReleaseObjects();
				}
				ZoneManager.FreezableZones.Clear();
				foreach (KeyValuePair<string, Zone> keyValuePair in this.CachedZones)
				{
					string text;
					Zone zone;
					keyValuePair.Deconstruct(out text, out zone);
					Zone zone2 = zone;
					if (!ZoneManager.ProcessingZones.Contains(zone2.ZoneID))
					{
						if (!zone2.Suspended)
						{
							if (this.ActiveZone == zone2 || zone2.GetSuspendability(suspendabilityTurns) != Suspendability.Suspendable)
							{
								continue;
							}
							this.SuspendZone(zone2);
						}
						if (AllowFreeze && zone2.Suspended && zone2.GetFreezability(freezabilityTurns) == Freezability.Freezable)
						{
							ZoneManager.FreezableZones.Add(zone2);
						}
					}
				}
				if ((AllowFreeze && ZoneManager.FreezableZones.Count >= this.GetPendingZoneFreezeThreshold()) || ForceFreeze)
				{
					using (DataManager.CacheTransaction cacheTransaction = DataManager.StartCacheTransaction(false))
					{
						for (int i = ZoneManager.FreezableZones.Count - 1; i >= 0; i--)
						{
							this.FreezeZone(ZoneManager.FreezableZones[i], cacheTransaction);
						}
						cacheTransaction.CommitTransaction();
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error checking zone caching", ex, "game_exception");
			}
			finally
			{
				ZoneManager.CheckingCache = false;
			}
		}

		// Token: 0x0600692A RID: 26922 RVA: 0x00211178 File Offset: 0x0020F378
		private int GetPendingZoneFreezeThreshold()
		{
			if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.LimitedWriteFrequency)))
			{
				return 8;
			}
			return 1;
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x0021118C File Offset: 0x0020F38C
		public void Tick(bool AllowFreeze)
		{
			try
			{
				ZoneManager.Ticker += 1L;
				if (this.ActiveZone != null)
				{
					this.ActiveZone.MarkActive();
					this.ActiveZone.CheckWeather();
				}
				if (Options.DisableZoneCaching2)
				{
					if (ZoneManager.Ticker % 100L == 0L)
					{
						MessageQueue.AddPlayerMessage("&RWARNING: You have the Disable Zone Caching option enabled, this will cause massive memory use over time.", null, true);
					}
				}
				else
				{
					this.CheckCached(AllowFreeze, false);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Freeze zone exception", ex);
			}
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x0021120C File Offset: 0x0020F40C
		public bool IsZoneBuilt(string ZoneID)
		{
			return this.CachedZones.ContainsKey(ZoneID) || ZoneManager.FrozenZones.Contains(ZoneID);
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x00211230 File Offset: 0x0020F430
		public void ClearFrozen()
		{
			foreach (string text in ZoneManager.FrozenZones)
			{
				List<ZoneConnection> list;
				if (this.ZoneConnections.TryGetValue(text, out list))
				{
					list.Clear();
				}
			}
			ZoneManager.FrozenZones.Clear();
			Directory.Delete(The.Game.GetCacheDirectory("ZoneCache"), true);
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x002112B0 File Offset: 0x0020F4B0
		public ZoneBuilderCollection GetBuilderCollection(string ZoneID)
		{
			ZoneBuilderCollection zoneBuilderCollection;
			this.ZoneBuilders.TryGetValue(ZoneID, out zoneBuilderCollection);
			return zoneBuilderCollection;
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x002112D0 File Offset: 0x0020F4D0
		public ZoneBuilderCollection RequireBuilderCollection(string ZoneID)
		{
			ZoneBuilderCollection zoneBuilderCollection;
			if (!this.ZoneBuilders.TryGetValue(ZoneID, out zoneBuilderCollection))
			{
				zoneBuilderCollection = (this.ZoneBuilders[ZoneID] = new ZoneBuilderCollection(ZoneID));
			}
			return zoneBuilderCollection;
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x00211302 File Offset: 0x0020F502
		public void AddZoneBuilder(string ZoneID, int Priority, string Class)
		{
			this.AddZoneBuilderInternal(ZoneID, Priority, ZoneBuilderBlueprint.Get(Class));
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x00211312 File Offset: 0x0020F512
		public void AddZoneBuilder(string ZoneID, int Priority, string Class, string Key1, object Value1)
		{
			this.AddZoneBuilderInternal(ZoneID, Priority, ZoneBuilderBlueprint.Get(Class, Key1, Value1));
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x00211326 File Offset: 0x0020F526
		public void AddZoneBuilder(string ZoneID, int Priority, string Class, string Key1, object Value1, string Key2, object Value2)
		{
			this.AddZoneBuilderInternal(ZoneID, Priority, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2));
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x00211340 File Offset: 0x0020F540
		public void AddZoneBuilder(string ZoneID, int Priority, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3)
		{
			this.AddZoneBuilderInternal(ZoneID, Priority, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3));
		}

		// Token: 0x06006934 RID: 26932 RVA: 0x00211368 File Offset: 0x0020F568
		public void AddZoneBuilder(string ZoneID, int Priority, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4)
		{
			this.AddZoneBuilderInternal(ZoneID, Priority, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4));
		}

		// Token: 0x06006935 RID: 26933 RVA: 0x00211394 File Offset: 0x0020F594
		public void AddZoneBuilder(string ZoneID, int Priority, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5)
		{
			this.AddZoneBuilderInternal(ZoneID, Priority, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5));
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x002113C4 File Offset: 0x0020F5C4
		public void AddZoneBuilder(string ZoneID, int Priority, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5, string Key6, object Value6)
		{
			this.AddZoneBuilderInternal(ZoneID, Priority, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5, Key6, Value6));
		}

		// Token: 0x06006937 RID: 26935 RVA: 0x002113F7 File Offset: 0x0020F5F7
		private void AddZoneBuilderInternal(string ZoneID, int Priority, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(Builder, Priority);
		}

		// Token: 0x06006938 RID: 26936 RVA: 0x00211407 File Offset: 0x0020F607
		public void AddZoneBuilderOverride(string ZoneID, string Builder)
		{
			this.AddZoneBuilderOverrideInternal(ZoneID, ZoneBuilderBlueprint.Get(Builder));
		}

		// Token: 0x06006939 RID: 26937 RVA: 0x00211416 File Offset: 0x0020F616
		private void AddZoneBuilderOverrideInternal(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(Builder, -1000);
		}

		// Token: 0x0600693A RID: 26938 RVA: 0x0021142A File Offset: 0x0020F62A
		public void AddZonePreBuilder(string ZoneID, string Builder)
		{
			this.AddZonePreBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Builder));
		}

		// Token: 0x0600693B RID: 26939 RVA: 0x00211439 File Offset: 0x0020F639
		public void AddZonePreBuilder(string ZoneID, string Class, string Key1, object Value1)
		{
			this.AddZonePreBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1));
		}

		// Token: 0x0600693C RID: 26940 RVA: 0x0021144B File Offset: 0x0020F64B
		public void AddZonePreBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2)
		{
			this.AddZonePreBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2));
		}

		// Token: 0x0600693D RID: 26941 RVA: 0x00211464 File Offset: 0x0020F664
		public void AddZonePreBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3)
		{
			this.AddZonePreBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3));
		}

		// Token: 0x0600693E RID: 26942 RVA: 0x0021148C File Offset: 0x0020F68C
		public void AddZonePreBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4)
		{
			this.AddZonePreBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4));
		}

		// Token: 0x0600693F RID: 26943 RVA: 0x002114B8 File Offset: 0x0020F6B8
		public void AddZonePreBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5)
		{
			this.AddZonePreBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5));
		}

		// Token: 0x06006940 RID: 26944 RVA: 0x002114E8 File Offset: 0x0020F6E8
		public void AddZonePreBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5, string Key6, object Value6)
		{
			this.AddZonePreBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5, Key6, Value6));
		}

		// Token: 0x06006941 RID: 26945 RVA: 0x00211519 File Offset: 0x0020F719
		private void AddZonePreBuilderInternal(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(Builder, 3000);
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x0021152D File Offset: 0x0020F72D
		public void AddZoneMidBuilder(string ZoneID, string Builder)
		{
			this.AddZoneMidBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Builder));
		}

		// Token: 0x06006943 RID: 26947 RVA: 0x0021153C File Offset: 0x0020F73C
		public void AddZoneMidBuilder(string ZoneID, string Class, string Key1, object Value1)
		{
			this.AddZoneMidBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1));
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x0021154E File Offset: 0x0020F74E
		public void AddZoneMidBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2)
		{
			this.AddZoneMidBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2));
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x00211564 File Offset: 0x0020F764
		public void AddZoneMidBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3)
		{
			this.AddZoneMidBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3));
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x0021158C File Offset: 0x0020F78C
		public void AddZoneMidBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4)
		{
			this.AddZoneMidBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4));
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x002115B8 File Offset: 0x0020F7B8
		public void AddZoneMidBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5)
		{
			this.AddZoneMidBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5));
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x002115E8 File Offset: 0x0020F7E8
		public void AddZoneMidBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5, string Key6, object Value6)
		{
			this.AddZoneMidBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5, Key6, Value6));
		}

		// Token: 0x06006949 RID: 26953 RVA: 0x00211619 File Offset: 0x0020F819
		private void AddZoneMidBuilderInternal(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(Builder, 4500);
		}

		// Token: 0x0600694A RID: 26954 RVA: 0x0021162D File Offset: 0x0020F82D
		public void AddZonePostBuilderIfNotAlreadyPresent(string ZoneID, string Builder)
		{
			this.RequireBuilderCollection(ZoneID).AddUnique(ZoneBuilderBlueprint.Get(Builder), 5000);
		}

		// Token: 0x0600694B RID: 26955 RVA: 0x00211647 File Offset: 0x0020F847
		public void AddZonePostBuilder(string ZoneID, string Builder)
		{
			this.AddZonePostBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Builder));
		}

		// Token: 0x0600694C RID: 26956 RVA: 0x00211656 File Offset: 0x0020F856
		public void AddZonePostBuilder(string ZoneID, string Class, string Key1, object Value1)
		{
			this.AddZonePostBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1));
		}

		// Token: 0x0600694D RID: 26957 RVA: 0x00211668 File Offset: 0x0020F868
		public void AddZonePostBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2)
		{
			this.AddZonePostBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2));
		}

		// Token: 0x0600694E RID: 26958 RVA: 0x00211680 File Offset: 0x0020F880
		public void AddZonePostBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3)
		{
			this.AddZonePostBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3));
		}

		// Token: 0x0600694F RID: 26959 RVA: 0x002116A8 File Offset: 0x0020F8A8
		public void AddZonePostBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4)
		{
			this.AddZonePostBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4));
		}

		// Token: 0x06006950 RID: 26960 RVA: 0x002116D4 File Offset: 0x0020F8D4
		public void AddZonePostBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5)
		{
			this.AddZonePostBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5));
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x00211704 File Offset: 0x0020F904
		public void AddZonePostBuilder(string ZoneID, string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5, string Key6, object Value6)
		{
			this.AddZonePostBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Class, Key1, Value1, Key2, Value2, Key3, Value3, Key4, Value4, Key5, Value5, Key6, Value6));
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x00211735 File Offset: 0x0020F935
		private void AddZonePostBuilderInternal(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(Builder, 5000);
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x00211749 File Offset: 0x0020F949
		public void AddZonePostBuilderAtStart(string ZoneID, string Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(ZoneBuilderBlueprint.Get(Builder), 4990);
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x00211762 File Offset: 0x0020F962
		public void AddZonePostBuilderAfterTerrain(string ZoneID, string Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(ZoneBuilderBlueprint.Get(Builder), 6010);
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x0021177B File Offset: 0x0020F97B
		public void AddZonePostBuilderAtStart(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(ZoneBuilderBlueprint.Get(Builder), 4990);
		}

		// Token: 0x06006956 RID: 26966 RVA: 0x00211794 File Offset: 0x0020F994
		[Obsolete]
		public void AddZoneBuilderOverride(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.AddZoneBuilderOverrideInternal(ZoneID, ZoneBuilderBlueprint.Get(Builder));
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x002117A3 File Offset: 0x0020F9A3
		[Obsolete]
		public void AddZonePreBuilder(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.AddZonePreBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Builder));
		}

		// Token: 0x06006958 RID: 26968 RVA: 0x002117B2 File Offset: 0x0020F9B2
		[Obsolete]
		public void AddZoneMidBuilderAtStart(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(ZoneBuilderBlueprint.Get(Builder), 4490);
		}

		// Token: 0x06006959 RID: 26969 RVA: 0x002117CB File Offset: 0x0020F9CB
		[Obsolete]
		public void AddZoneMidBuilder(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.AddZoneMidBuilderInternal(ZoneID, ZoneBuilderBlueprint.Get(Builder));
		}

		// Token: 0x0600695A RID: 26970 RVA: 0x002117DA File Offset: 0x0020F9DA
		[Obsolete]
		public void AddZoneMidBuilderAtStart(string ZoneID, string Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(ZoneBuilderBlueprint.Get(Builder), 4490);
		}

		// Token: 0x0600695B RID: 26971 RVA: 0x002117F3 File Offset: 0x0020F9F3
		[Obsolete]
		public void AddZonePostBuilderAfterTerrain(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(ZoneBuilderBlueprint.Get(Builder), 6010);
		}

		// Token: 0x0600695C RID: 26972 RVA: 0x0021180C File Offset: 0x0020FA0C
		[Obsolete]
		public void AddZonePostBuilder(string ZoneID, ZoneBuilderBlueprint Builder)
		{
			this.RequireBuilderCollection(ZoneID).Add(ZoneBuilderBlueprint.Get(Builder), 5000);
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x00211825 File Offset: 0x0020FA25
		public void ClearZoneBuilders(string ZoneID)
		{
			ZoneBuilderCollection builderCollection = this.GetBuilderCollection(ZoneID);
			if (builderCollection == null)
			{
				return;
			}
			builderCollection.Clear();
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x00211838 File Offset: 0x0020FA38
		public int RemoveZoneBuilders(string ZoneID, Predicate<ZoneBuilderBlueprint> Predicate)
		{
			ZoneBuilderCollection zoneBuilderCollection;
			if (this.ZoneBuilders.TryGetValue(ZoneID, out zoneBuilderCollection))
			{
				return zoneBuilderCollection.RemoveAll(Predicate);
			}
			return 0;
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x00211860 File Offset: 0x0020FA60
		public int RemoveZoneBuilders(string ZoneID, string Class, Predicate<ZoneBuilderBlueprint> Predicate = null)
		{
			ZoneBuilderCollection zoneBuilderCollection;
			if (this.ZoneBuilders.TryGetValue(ZoneID, out zoneBuilderCollection))
			{
				return zoneBuilderCollection.RemoveAll(Class, Predicate);
			}
			return 0;
		}

		// Token: 0x06006960 RID: 26976 RVA: 0x00211887 File Offset: 0x0020FA87
		public void SetZoneColumnProperty(string ZoneID, string Name, object Value)
		{
			ZoneID = ZoneID.Substring(0, ZoneID.LastIndexOf('.'));
			this.SetZoneProperty(ZoneID, Name, Value);
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x002118A3 File Offset: 0x0020FAA3
		public void SetWorldCellProperty(string ZoneID, string Name, object Value)
		{
			ZoneID = ZoneID.Substring(0, ZoneID.UpToNthIndex('.', 3, -1));
			this.SetZoneProperty(ZoneID, Name, Value);
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x002118C4 File Offset: 0x0020FAC4
		public void SetZoneProperty(string ZoneID, string Name, object Value)
		{
			Dictionary<string, object> dictionary;
			if (!this.ZoneProperties.TryGetValue(ZoneID, out dictionary))
			{
				dictionary = new Dictionary<string, object>();
				this.ZoneProperties.Add(ZoneID, dictionary);
			}
			dictionary[Name] = Value;
		}

		// Token: 0x06006963 RID: 26979 RVA: 0x002118FC File Offset: 0x0020FAFC
		public bool HasZoneProperty(string ZoneID, string Name)
		{
			Dictionary<string, object> dictionary;
			return this.ZoneProperties.TryGetValue(ZoneID, out dictionary) && dictionary.ContainsKey(Name);
		}

		// Token: 0x06006964 RID: 26980 RVA: 0x00211924 File Offset: 0x0020FB24
		public bool RemoveZoneProperty(string ZoneID, string Name)
		{
			Dictionary<string, object> dictionary;
			return this.ZoneProperties.TryGetValue(ZoneID, out dictionary) && dictionary.Remove(Name);
		}

		// Token: 0x06006965 RID: 26981 RVA: 0x0021194C File Offset: 0x0020FB4C
		public bool HasZoneColumnProperty(string ZoneID, string Name)
		{
			if (ZoneID != null)
			{
				ZoneID = ZoneID.Substring(0, ZoneID.LastIndexOf('.'));
				Dictionary<string, object> dictionary;
				if (this.ZoneProperties.TryGetValue(ZoneID, out dictionary) && dictionary.ContainsKey(Name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006966 RID: 26982 RVA: 0x0021198C File Offset: 0x0020FB8C
		public bool TryGetZoneColumnProperty<T>(string ZoneID, string Name, out T Value)
		{
			ZoneID = ZoneID.Substring(0, ZoneID.LastIndexOf('.'));
			Dictionary<string, object> dictionary;
			object obj;
			if (this.ZoneProperties.TryGetValue(ZoneID, out dictionary) && dictionary.TryGetValue(Name, out obj))
			{
				Value = (T)((object)obj);
				return true;
			}
			Value = default(T);
			return false;
		}

		// Token: 0x06006967 RID: 26983 RVA: 0x002119DC File Offset: 0x0020FBDC
		public object GetZoneColumnProperty(string ZoneID, string Name, object Default = null)
		{
			if (ZoneID != null)
			{
				ZoneID = ZoneID.Substring(0, ZoneID.LastIndexOf('.'));
				Dictionary<string, object> dictionary;
				object obj;
				if (this.ZoneProperties.TryGetValue(ZoneID, out dictionary) && dictionary.TryGetValue(Name, out obj))
				{
					return obj;
				}
			}
			return Default;
		}

		// Token: 0x06006968 RID: 26984 RVA: 0x00211A1C File Offset: 0x0020FC1C
		public bool TryGetWorldCellProperty<T>(string ZoneID, string Name, out T Value)
		{
			ZoneID = ZoneID.Substring(0, ZoneID.UpToNthIndex('.', 3, -1));
			Dictionary<string, object> dictionary;
			object obj;
			if (this.ZoneProperties.TryGetValue(ZoneID, out dictionary) && dictionary.TryGetValue(Name, out obj))
			{
				Value = (T)((object)obj);
				return true;
			}
			Value = default(T);
			return false;
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x00211A70 File Offset: 0x0020FC70
		public object GetWorldCellProperty(string ZoneID, string Name, object Default = null)
		{
			if (ZoneID != null)
			{
				ZoneID = ZoneID.Substring(0, ZoneID.UpToNthIndex('.', 3, -1));
				Dictionary<string, object> dictionary;
				object obj;
				if (this.ZoneProperties.TryGetValue(ZoneID, out dictionary) && dictionary.TryGetValue(Name, out obj))
				{
					return obj;
				}
			}
			return Default;
		}

		// Token: 0x0600696A RID: 26986 RVA: 0x00211AB4 File Offset: 0x0020FCB4
		public bool TryGetZoneProperty<T>(string ZoneID, string Name, out T Value)
		{
			Dictionary<string, object> dictionary;
			object obj;
			if (this.ZoneProperties.TryGetValue(ZoneID, out dictionary) && dictionary.TryGetValue(Name, out obj))
			{
				Value = (T)((object)obj);
				return true;
			}
			Value = default(T);
			return false;
		}

		// Token: 0x0600696B RID: 26987 RVA: 0x00211AF4 File Offset: 0x0020FCF4
		public object GetZoneProperty(string zID, string Name, bool bClampToLevel30 = false, string defaultvalue = null)
		{
			if (zID != null)
			{
				Dictionary<string, object> dictionary;
				object obj;
				if (this.ZoneProperties.TryGetValue(zID, out dictionary) && dictionary.TryGetValue(Name, out obj))
				{
					return obj;
				}
				string text;
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				if (bClampToLevel30 && zID.Contains(".") && ZoneID.Parse(zID, out text, out num, out num2, out num3, out num4, out num5))
				{
					zID = ZoneID.Assemble(text, num, num2, num3, num4, 29);
					return this.GetZoneProperty(zID, Name, false, defaultvalue);
				}
			}
			return defaultvalue;
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x00211B64 File Offset: 0x0020FD64
		public bool CheckBiomesAllowed(string ZoneID)
		{
			string text;
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			return ZoneID.Parse(ZoneID, out text, out num, out num2, out num3, out num4, out num5) && this.CheckBiomesAllowed(ZoneID, text, num, num2, num3, num4, num5);
		}

		// Token: 0x0600696D RID: 26989 RVA: 0x00211B96 File Offset: 0x0020FD96
		public bool CheckBiomesAllowed(Zone Z)
		{
			return this.CheckBiomesAllowed(Z.ZoneID, Z.ZoneWorld, Z.wX, Z.wY, Z.X, Z.Y, Z.Z);
		}

		// Token: 0x0600696E RID: 26990 RVA: 0x00211BC8 File Offset: 0x0020FDC8
		public bool CheckBiomesAllowed(string ZoneID, string WorldID, int WX, int WY, int X, int Y, int Z)
		{
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(WorldID, WX, WY, X, Y, Z);
			if (zoneBlueprint != null && !zoneBlueprint.HasBiomes)
			{
				return false;
			}
			GameObject terrainObjectForZone = ZoneManager.GetTerrainObjectForZone(WX, WY, WorldID);
			string text;
			return (terrainObjectForZone == null || !terrainObjectForZone.HasTagOrProperty("NoBiomes")) && (!this.TryGetZoneProperty<string>(ZoneID, "NoBiomes", out text) || !(text == "Yes"));
		}

		// Token: 0x0600696F RID: 26991 RVA: 0x00211C30 File Offset: 0x0020FE30
		public List<ZoneConnection> GetZoneConnections(string ZoneID)
		{
			List<ZoneConnection> list;
			if (!this.ZoneConnections.TryGetValue(ZoneID, out list))
			{
				return new List<ZoneConnection>();
			}
			return list;
		}

		// Token: 0x06006970 RID: 26992 RVA: 0x00211C54 File Offset: 0x0020FE54
		public List<ZoneConnection> GetZoneConnectionsCopy(string ZoneID)
		{
			List<ZoneConnection> list;
			if (!this.ZoneConnections.TryGetValue(ZoneID, out list))
			{
				return new List<ZoneConnection>();
			}
			return new List<ZoneConnection>(list);
		}

		// Token: 0x06006971 RID: 26993 RVA: 0x00211C80 File Offset: 0x0020FE80
		public void AddZoneConnection(string ZoneID, string TargetDirection, int X, int Y, string Type, string ConnectionObject = null)
		{
			string zoneFromIDAndDirection = this.GetZoneFromIDAndDirection(ZoneID, TargetDirection);
			if (!this.ZoneConnections.ContainsKey(zoneFromIDAndDirection))
			{
				this.ZoneConnections.Add(zoneFromIDAndDirection, new List<ZoneConnection>());
			}
			ZoneConnection zoneConnection = new ZoneConnection();
			zoneConnection.X = X;
			zoneConnection.Y = Y;
			zoneConnection.Type = Type;
			zoneConnection.Object = ConnectionObject;
			this.ZoneConnections[zoneFromIDAndDirection].Add(zoneConnection);
		}

		// Token: 0x06006972 RID: 26994 RVA: 0x00211CEC File Offset: 0x0020FEEC
		public void RemoveZoneConnection(string ZoneID, string TargetDirection, int X, int Y, string Type, string ConnectionObject = null)
		{
			string zoneFromIDAndDirection = this.GetZoneFromIDAndDirection(ZoneID, TargetDirection);
			List<ZoneConnection> list;
			if (this.ZoneConnections.TryGetValue(zoneFromIDAndDirection, out list))
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					ZoneConnection zoneConnection = list[i];
					if (zoneConnection.X == X && zoneConnection.Y == Y && !(zoneConnection.Type != Type) && !(zoneConnection.Object != ConnectionObject))
					{
						list.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x00211D68 File Offset: 0x0020FF68
		public void RemoveZoneConnection(string ZoneID, string TargetDirection, ZoneConnection Connection)
		{
			string zoneFromIDAndDirection = this.GetZoneFromIDAndDirection(ZoneID, TargetDirection);
			List<ZoneConnection> list;
			if (this.ZoneConnections.TryGetValue(zoneFromIDAndDirection, out list))
			{
				list.Remove(Connection);
			}
		}

		// Token: 0x06006974 RID: 26996 RVA: 0x00211D98 File Offset: 0x0020FF98
		public string GetZoneFromIDAndDirection(string ZoneID, string Direction)
		{
			if (ZoneID.IsNullOrEmpty() || !ZoneID.Contains("."))
			{
				return "";
			}
			string text;
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			ZoneID.Parse(ZoneID, out text, out num, out num2, out num3, out num4, out num5);
			Direction = Direction.ToLower();
			if (Direction == "u")
			{
				num5--;
			}
			if (Direction == "d")
			{
				num5++;
			}
			if (Direction == "n")
			{
				num4--;
			}
			if (Direction == "s")
			{
				num4++;
			}
			if (Direction == "e")
			{
				num3++;
			}
			if (Direction == "w")
			{
				num3--;
			}
			if (Direction == "nw")
			{
				num3--;
				num4--;
			}
			if (Direction == "ne")
			{
				num3++;
				num4--;
			}
			if (Direction == "sw")
			{
				num3--;
				num4++;
			}
			if (Direction == "se")
			{
				num3++;
				num4++;
			}
			if (num3 < 0)
			{
				num3 = Definitions.Width - 1;
				num--;
			}
			if (num3 >= Definitions.Width)
			{
				num3 = 0;
				num++;
			}
			if (num4 < 0)
			{
				num4 = Definitions.Height - 1;
				num2--;
			}
			if (num4 >= Definitions.Width)
			{
				num4 = 0;
				num2++;
			}
			if (num5 < 0)
			{
				num5 = Definitions.Layers - 1;
			}
			if (num5 >= Definitions.Layers)
			{
				num5 = 0;
			}
			return ZoneID.Assemble(text, num, num2, num3, num4, num5);
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x00211F0C File Offset: 0x0021010C
		public bool HasVisitedZone(string zoneid)
		{
			return this.VisitedTime.ContainsKey(zoneid);
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x00211F1A File Offset: 0x0021011A
		public void DeleteZone(Zone Z)
		{
			this.CachedZones.Remove(Z.ZoneID);
			Z.Stale = true;
		}

		// Token: 0x06006977 RID: 26999 RVA: 0x00211F38 File Offset: 0x00210138
		public bool SuspendZone(Zone Zone)
		{
			if (Zone == null || Zone.Suspended)
			{
				return true;
			}
			try
			{
				SuspendingEvent.Send(Zone);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error sending suspension event", ex, "game_exception");
			}
			if (ZoneManager.ProcessingZones.Contains(Zone.ZoneID))
			{
				MetricsManager.LogInfo("Zone by ID '" + Zone.ZoneID + "' was reserved by suspension event, aborting suspend.");
				return false;
			}
			ActionManager actionManager = The.ActionManager;
			RingDeque<GameObject> actionQueue = actionManager.ActionQueue;
			for (int i = actionQueue.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = actionQueue[i];
				if (gameObject != null && gameObject.CurrentZone == Zone)
				{
					actionManager.RemoveActiveObject(i);
				}
			}
			Zone.Suspended = true;
			return true;
		}

		// Token: 0x06006978 RID: 27000 RVA: 0x00211FF4 File Offset: 0x002101F4
		public void SuspendAll()
		{
			foreach (KeyValuePair<string, Zone> keyValuePair in this.CachedZones)
			{
				string text;
				Zone zone;
				keyValuePair.Deconstruct(out text, out zone);
				Zone zone2 = zone;
				if (zone2 != this.ActiveZone)
				{
					this.SuspendZone(zone2);
				}
			}
		}

		// Token: 0x06006979 RID: 27001 RVA: 0x00212060 File Offset: 0x00210260
		public static void ActivateObjects(Zone Zone)
		{
			if (Zone.IsWorldMap())
			{
				foreach (GameObject gameObject in Zone.IterateObjects())
				{
					if (!gameObject.HasPart<TerrainTravel>())
					{
						gameObject.MakeLive();
						gameObject.MakeActive(false);
					}
				}
				return;
			}
			foreach (GameObject gameObject2 in Zone.IterateObjects())
			{
				gameObject2.MakeLive();
				gameObject2.MakeActive(false);
			}
		}

		// Token: 0x0600697A RID: 27002 RVA: 0x002120DC File Offset: 0x002102DC
		public Zone SetActiveZone(string ZoneID)
		{
			if (this.ActiveZone != null && this.ActiveZone.ZoneID == ZoneID)
			{
				return this.ActiveZone;
			}
			Zone zone = this.GetZone(ZoneID);
			if (zone == null)
			{
				MetricsManager.LogError("Attempting to set null active zone by zone id '" + ZoneID + "'.");
				return this.ActiveZone;
			}
			return this.SetActiveZone(zone);
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x00212139 File Offset: 0x00210339
		public void SetCachedZone(Zone Zone)
		{
			Zone.MarkActive();
			ZoneManager.ActivateObjects(Zone);
			Zone.Suspended = false;
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x00212150 File Offset: 0x00210350
		public Zone SetActiveZone(Zone Z)
		{
			Zone activeZone = this.ActiveZone;
			if (Z == activeZone)
			{
				return activeZone;
			}
			try
			{
				XRLCore.ParticleManager.Particles.Clear();
				GameManager.Instance.uiQueue.queueTask(new Action(CombatJuiceManager.finishAll), 0);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("SetActiveZone::FinishJuice", ex, "game_exception");
			}
			this.ActiveZone = Z;
			ZoneManager.ZoneGenerationContext = Z;
			this.LastZoneTransition = XRLCore.Core.Game.Turns;
			string zoneID = this.ActiveZone.ZoneID;
			if (this.ActiveZone != null)
			{
				ZoneManager.zoneGenerationContextTier = this.ActiveZone.NewTier;
				ZoneManager.zoneGenerationContextZoneID = this.ActiveZone.ZoneID;
			}
			if (!XRLCore.Core.Game.bZoned && this.LastZoneTransition > 0L)
			{
				The.Game.bZoned = true;
			}
			if (this.ActiveZone.Z >= 60)
			{
				Achievement.ARCONAUT.Unlock();
			}
			if (!this.VisitedTime.ContainsKey(zoneID))
			{
				if (The.Player != null && zoneID.Contains("."))
				{
					The.Player.FireEvent(Event.New("VisitingNewZone", "ID", zoneID));
				}
				string text = WorldFactory.Factory.ZoneDisplayName(this.ActiveZone.ZoneID);
				int num = Stat.Roll(2, 6);
				if (text.Contains("Kyakukya"))
				{
					if (XRLCore.CurrentTurn - The.Game.GetInt64GameState("LastKyakukyaVisit", 0L) > 1000L)
					{
						JournalAPI.AddAccomplishment("You journeyed to Kyakukya.", "Done trekking through the root-strangled earth, =name= arrived in Kyakukya and was greeted by the village with warmth and reverence. Upon leaving, =name= was named Friend to Oboroqoru.", string.Format("On an expedition down the River Svy, =name= was captured by bandits. {0} languished in captivity for {1} years, eventually escaping to Kyakukya and befriending its mayor, the albino ape called Nuntu.", The.Player.GetPronounProvider(false).Subjective, num), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					}
					The.Game.SetInt64GameState("LastKyakukyaVisit", XRLCore.CurrentTurn);
				}
				if (text.Contains("Omonporch"))
				{
					if (XRLCore.CurrentTurn - The.Game.GetInt64GameState("LastOmonporchVisit", 0L) > 1000L)
					{
						JournalAPI.AddAccomplishment("You journeyed to Omonporch.", "=name= journeyed to the Spindle at Omonporch, voiced a prayer for the Fossilized Saads, and observed a star fall along the planet's axis.", string.Concat(new string[]
						{
							"In =year=, =name= appointed the corrupt administrator Asphodel as earl and minister of Omonporch. There xe mandated the practice of <spice.elements.",
							The.Player.GetMythicDomain(5, null),
							".practices.!random> in ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" name."
						}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					}
					The.Game.SetInt64GameState("LastOmonporchVisit", XRLCore.CurrentTurn);
				}
				if ((text.Contains("Stiltgrounds") || text.Contains("Six Day Stilt")) && The.Game.GetIntGameState("VisitedSixDayStilt", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to the Six Day Stilt.", "=name= trekked through the salt pans, north and west, to the merchant bazaar and grand cathedral of the Six Day Stilt. There, the stiltfolk sang hymns in the sultan's honor.", "After trekking the vacant flats, =name= journeyed to the merchant bazaar and cathedral at the Six Day Stilt. There " + The.Player.GetPronounProvider(false).Subjective + " befriended the Mechanimists and threw an artifact down the Sacred Well.", null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					The.Game.SetIntGameState("VisitedSixDayStilt", 1);
					Achievement.SIX_DAY_STILT.Unlock();
				}
				if (text.Contains("Red Rock") && The.Game.GetIntGameState("VisitedRedrock", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to Red Rock.", "=name= hiked the salted marshes and came upon the ancient gathering place called Red Rock, where a bevy of snapjaws cooked the sultan a satisfying meal.", string.Concat(new string[]
					{
						"Near the location of Red Rock, =name= was captured by baboons. ",
						The.Player.GetPronounProvider(false).Subjective,
						" murdered their leader <spice.elements.",
						The.Player.GetMythicDomain(5, null),
						".murdermethods.!random> and from then on wore a neck ring stained with baboon blood."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					The.Game.SetIntGameState("VisitedRedrock", 1);
				}
				if (text.Contains("rusted archway") && The.Game.GetIntGameState("VisitedRustedArchway", 0) != 1)
				{
					string text2 = HistoricStringExpander.ExpandString("<spice.elements." + The.Player.GetMythicDomain(5, null) + ".nouns.!random>", null);
					JournalAPI.AddAccomplishment("You journeyed to a rusted archway.", "To commemorate the imperial conquests of =name=, a triumphal arch was erected across the fruiting gorges.", string.Concat(new string[]
					{
						"At <spice.time.partsOfDay.!random> under <spice.commonPhrases.strange.!random.article> and rusted sky, the people of the desert canyon saw an image on the horizon that looked like a ",
						text2,
						" under an archway. It was =name=, and after ",
						The.Player.GetPronounProvider(false).Subjective,
						" came and left, the people built a monument to =name= and thenceforth called ",
						The.Player.GetPronounProvider(false).Objective,
						" the Underarch ",
						Grammar.MakeTitleCase(text2),
						"."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					The.Game.SetIntGameState("VisitedRustedArchway", 1);
				}
				if (text.Contains("Rustwell") && The.Game.GetIntGameState("VisitedRustwells", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to the rust wells.", "Hillfolk dug wells deep into the earth and drew spring water to honor the sultan =name= in sacred ritual.", "While journeying across the wavegraph hills, =name= discovered the rust wells. There " + The.Player.GetPronounProvider(false).Subjective + " befriended water barons and sipped from the red iron spring.", null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					The.Game.SetIntGameState("VisitedRustwells", 1);
				}
				if (text.Contains("Grit Gate") && The.Game.GetIntGameState("VisitedGritGate", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to Grit Gate.", "The scholar-sultan =name= visited Grit Gate and lectured the monastic order there on the subject of light refraction.", "While delving deep through moss-skirted steeples of chrome, =name= discovered the ancient Grit Gate. There " + The.Player.GetPronounProvider(false).Subjective + " befriended the Barathrumites and lectured them on light refraction.", null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					The.Game.SetIntGameState("VisitedGritGate", 1);
				}
				if (text.Contains("Golgotha") && The.Game.GetIntGameState("VisitedGolgotha", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to Golgotha.", string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						" AR, =name= ascended the trash chutes of Golgotha, victorious and bathed in slime."
					}), string.Concat(new string[]
					{
						"One auspicious day in the jungle, =name= descended the trash chutes of Golgotha and bathed in viscous slime. From that day forth, ",
						The.Player.GetPronounProvider(false).Subjective,
						" always kept some wet trash on ",
						The.Player.GetPronounProvider(false).PossessiveAdjective,
						" person."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					The.Game.SetIntGameState("VisitedGolgotha", 1);
				}
				if (text.Contains("Bethesda Susa") && The.Game.GetIntGameState("VisitedBethesda", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to Bethesda Susa.", "=name= trekked to the frozen sauna at Bethesda Susa and soaked in the company of trolls.", string.Concat(new string[]
					{
						"In =year=, =name= appointed a corrupt administrator to the frozen sauna at Bethesda Susa. There ",
						Grammar.RandomShePronoun(),
						" mandated association with trolls in ",
						Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
						" name."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					The.Game.SetIntGameState("VisitedBethesda", 1);
				}
				if (text.Contains("Tomb of the Eaters") && The.Game.GetIntGameState("VisitedTomboftheEaters", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to the Tomb of the Eaters.", string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						" AR, =name= trekked to the Tomb of the Eaters and traced a sigil across the Death Gate. O wise sultan!"
					}), string.Concat(new string[]
					{
						"While traveling through the banana grove, =name= trekked to the Tomb of the Eaters and dreamed they emerged at the Court of Sultans and crossed the Death Gate. From that day forth, ",
						The.Player.GetPronounProvider(false).Subjective,
						" always kept the Mark of Death on ",
						The.Player.GetPronounProvider(false).PossessiveAdjective,
						" person."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					The.Game.SetIntGameState("VisitedTomboftheEaters", 1);
				}
				if (text.Contains("Asphalt Mines") && The.Game.GetIntGameState("VisitedAsphaltMines", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to the asphalt mines.", "Strong =name= trekked to the asphalt mines to breathe the tarry vapor and bathe in the black blood of the earth.", "Sometime in =year=, =name= wandered over the high mounts and voyaged to the Asphalt Mines. There " + The.Player.GetPronounProvider(false).Subjective + " befriended no one and instead bathed in the black blood of the earth.", null, "general", MuralCategory.VisitsLocation, MuralWeight.VeryLow, null, -1L, true);
					The.Game.SetIntGameState("VisitedAsphaltMines", 1);
				}
				if (text.Contains("City of Bones") && The.Game.GetIntGameState("VisitedCityofBones", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to the City of Bones.", "Strong =name= trekked to the City of Bones to breathe the tarry vapor and bathe in the black blood of the earth.", "Sometime in =year=, =name= wandered over the high mounts and voyaged to the City of Bones in the Asphalt Mines. There " + The.Player.GetPronounProvider(false).Subjective + " befriended no one and instead bathed in the black blood of the earth.", null, "general", MuralCategory.VisitsLocation, MuralWeight.VeryLow, null, -1L, true);
					The.Game.SetIntGameState("VisitedCityofBones", 1);
				}
				if (text.Contains("Great Sea") && The.Game.GetIntGameState("VisitedGreatSea", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to the Great Asphalt Sea.", "Strong =name= trekked to the Great Asphalt Sea to breathe the tarry vapor and bathe in the black blood of the earth.", "Sometime in =year=, =name= wandered over the high mounts and voyaged to the Great Sea in the Asphalt Mines. There " + The.Player.GetPronounProvider(false).Subjective + " befriended no one and instead bathed in the black blood of the earth.", null, "general", MuralCategory.VisitsLocation, MuralWeight.VeryLow, null, -1L, true);
					The.Game.SetIntGameState("VisitedGreatSea", 1);
				}
				if (text.Contains("Tunnels of Ur") && The.Game.GetIntGameState("VisitedTunnelsUr", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to the Tunnels of Ur.", "Strong =name= trekked to the Tunnels of Ur to breathe the tarry vapor and bathe in the black blood of the earth.", "Sometime in =year=, =name= wandered over the high mounts and voyaged to the Tunnels of Ur in the Asphalt Mines. There " + The.Player.GetPronounProvider(false).Subjective + " befriended no one and instead bathed in the black blood of the earth.", null, "general", MuralCategory.VisitsLocation, MuralWeight.VeryLow, null, -1L, true);
					The.Game.SetIntGameState("VisitedTunnelsUr", 1);
				}
				if (text.Contains("Swilling Vast") && The.Game.GetIntGameState("VisitedSwillingVast", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You journeyed to the Swilling Vast.", "Strong =name= trekked to the Swilling Vast to breathe the tarry vapor and bathe in the black blood of the earth.", "Sometime in =year=, =name= wandered over the high mounts and voyaged to the Swilling Vast in the Asphalt Mines. There " + The.Player.GetPronounProvider(false).Subjective + " befriended no one and instead bathed in the black blood of the earth.", null, "general", MuralCategory.VisitsLocation, MuralWeight.VeryLow, null, -1L, true);
					The.Game.SetIntGameState("VisitedSwillingVast", 1);
				}
			}
			this.VisitedTime[zoneID] = Calendar.TotalTimeTicks;
			if (activeZone != null)
			{
				activeZone.Deactivated();
			}
			this.ActiveZone.Activated();
			ZoneManager.ActivateObjects(this.ActiveZone);
			this.ActiveZone.Suspended = false;
			The.Game.ZoneManager.CheckCached(true, false);
			if (this.ActiveZone.GetZoneWorld() == "JoppaWorld")
			{
				MessageQueue.AddPlayerMessage(WorldFactory.Factory.ZoneDisplayName(zoneID) + ", " + Calendar.GetTime(), 'C', true);
			}
			else
			{
				MessageQueue.AddPlayerMessage(WorldFactory.Factory.ZoneDisplayName(zoneID), 'C', true);
			}
			try
			{
				List<JournalMapNote> mapNotesForZone = JournalAPI.GetMapNotesForZone(this.ActiveZone.ZoneID);
				if (mapNotesForZone.Count > 0)
				{
					string text3 = "Notes: ";
					int num2 = 0;
					for (int i = 0; i < mapNotesForZone.Count; i++)
					{
						if (mapNotesForZone[i].Revealed)
						{
							if (num2 > 0)
							{
								text3 += ", ";
							}
							text3 += mapNotesForZone[i].Text;
							num2++;
						}
					}
					if (num2 > 0)
					{
						MessageQueue.AddPlayerMessage(text3, null, true);
					}
				}
				AutoAct.Interrupt(null, null, null, false);
			}
			catch (Exception ex2)
			{
				global::UnityEngine.Debug.LogError("Exception during zone note retrieval: " + ex2.ToString());
			}
			try
			{
				if ((activeZone != null) & (Z != null))
				{
					ZoneManager.ZoneTransitionCount += Math.Min(activeZone.GetTransitionIntervalTo(Z), Z.GetTransitionIntervalTo(activeZone));
				}
				int autosaveInterval = Options.AutosaveInterval;
				if (autosaveInterval > 0 && ZoneManager.ZoneTransitionCount >= autosaveInterval)
				{
					GameObject player = The.Player;
					if (((player != null) ? player.CurrentCell : null) != null)
					{
						ZoneManager.ZoneTransitionCount = 0;
						AutoSaveCommand.Issue();
					}
				}
			}
			catch (Exception ex3)
			{
				global::UnityEngine.Debug.LogError("Exception during autosave: " + ex3.ToString());
			}
			if (this.ActiveZone != null)
			{
				ZoneManager.PaintWalls(this.ActiveZone, 0, 0, -1, -1);
				ZoneManager.PaintWater(this.ActiveZone, 0, 0, -1, -1);
			}
			this.ProcessGoToPartyLeader();
			return this.ActiveZone;
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x00212CF0 File Offset: 0x00210EF0
		public void CheckEventQueue()
		{
			if (this.ActiveZone != null)
			{
				this.ActiveZone.CheckEventQueue();
			}
		}

		// Token: 0x0600697E RID: 27006 RVA: 0x00212D05 File Offset: 0x00210F05
		public bool IsZoneLive(string ZoneID)
		{
			return ZoneID != null && this.CachedZones.ContainsKey(ZoneID);
		}

		// Token: 0x0600697F RID: 27007 RVA: 0x00212D18 File Offset: 0x00210F18
		public bool IsZoneLive(string World, int wX, int wY, int X, int Y, int Z)
		{
			return this.CachedZones.ContainsKey(ZoneID.Assemble(World, wX, wY, X, Y, Z));
		}

		// Token: 0x06006980 RID: 27008 RVA: 0x00212D34 File Offset: 0x00210F34
		public void RebuildActiveZone(bool Flush = false)
		{
			if (The.Player.CurrentZone != this.ActiveZone)
			{
				return;
			}
			Zone zone = this.ActiveZone;
			List<GameObject> objects = zone.GetObjects((GameObject o) => o.IsPlayerLed());
			Dictionary<GameObject, Location2D> dictionary = new Dictionary<GameObject, Location2D>();
			GameObject player = The.Player;
			dictionary[player] = The.Player.CurrentCell.Location;
			Dictionary<GameObject, Location2D> dictionary2 = dictionary;
			The.Player.CurrentCell.RemoveObject(The.Player, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			foreach (GameObject gameObject in objects)
			{
				try
				{
					dictionary2[gameObject] = gameObject.CurrentCell.Location;
					gameObject.CurrentCell.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				}
				catch
				{
				}
			}
			if (zone.IsWorldMap())
			{
				this.SetActiveZone(The.ZoneManager.GetZone("JoppaWorld.0.0.0.0.0"));
			}
			else
			{
				this.SetActiveZone(The.ZoneManager.ActiveZone.GetZoneWorld());
			}
			this.SuspendZone(zone);
			this.DeleteZone(zone);
			if (Flush)
			{
				The.ZoneManager.CachedZones.Clear();
				The.ZoneManager.ActiveZone = null;
			}
			zone = The.ZoneManager.GetZone(zone.ZoneID);
			The.ZoneManager.SetActiveZone(zone);
			try
			{
				zone.GetCell(dictionary2[The.Player]).AddObject(The.Player, true, true, false, false, false, true, true, null, null, null, null, null, null, null, null);
				The.ZoneManager.ProcessGoToPartyLeader();
			}
			catch
			{
			}
			foreach (GameObject gameObject2 in objects)
			{
				try
				{
					zone.GetCell(dictionary2[gameObject2]).AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x00212F78 File Offset: 0x00211178
		public Zone GetZone(string World, int wX, int wY, int X, int Y, int Z)
		{
			if (World.IsNullOrEmpty())
			{
				return null;
			}
			return this.GetZone(ZoneID.Assemble(World, wX, wY, X, Y, Z));
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x00212F98 File Offset: 0x00211198
		public Zone GetZone(string ZoneID)
		{
			if (ZoneID.IsNullOrEmpty())
			{
				return null;
			}
			Zone zone = null;
			try
			{
				Zone zone2;
				Zone zone3;
				Zone zone4;
				if (GetZoneEvent.TryGetFor(this.Game, ZoneID, out zone2))
				{
					zone = zone2;
				}
				else if (this.CachedZones.TryGetValue(ZoneID, out zone3))
				{
					zone = zone3;
				}
				else if (this.TryThawZone(ZoneID, out zone4))
				{
					zone = zone4;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error getting existing zone '" + ZoneID + "'", ex, "game_exception");
			}
			if (zone == null)
			{
				this.GenerateZone(ZoneID);
				if (!this.CachedZones.TryGetValue(ZoneID, out zone))
				{
					MetricsManager.LogError("Error generating zone '" + ZoneID + "'");
				}
			}
			return zone;
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x00213048 File Offset: 0x00211248
		public bool MoveZone(string FromID, string ToID, bool Overwrite = true)
		{
			Zone zone;
			if (this.CachedZones.TryGetValue(FromID, out zone))
			{
				if (Overwrite)
				{
					this.CachedZones[ToID] = zone;
				}
				else if (this.CachedZones.ContainsKey(ToID))
				{
					return false;
				}
				this.CachedZones.Remove(FromID);
				zone.ZoneID = ToID;
				return true;
			}
			if (!ZoneManager.FrozenZones.Contains(FromID))
			{
				return false;
			}
			string text = The.Game.GetCacheDirectory(Path.Combine("ZoneCache", FromID + ".zone.gz"));
			if (!File.Exists(text))
			{
				text = text.Remove(text.Length - 2, 2);
			}
			if (!File.Exists(text))
			{
				return false;
			}
			string text2 = text.Replace(FromID, ToID);
			if (Overwrite)
			{
				File.Delete(text2);
			}
			else if (ZoneManager.FrozenZones.Contains(ToID))
			{
				return false;
			}
			ZoneManager.FrozenZones.Remove(FromID);
			ZoneManager.FrozenZones.Add(ToID);
			File.Move(text, text2);
			return true;
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x00213134 File Offset: 0x00211334
		public List<CellBlueprint> GetCellBlueprints(string ZoneID)
		{
			List<CellBlueprint> list2;
			try
			{
				List<CellBlueprint> list = new List<CellBlueprint>();
				if (ZoneID.IndexOf('.') == -1)
				{
					list2 = list;
				}
				else
				{
					ReadOnlySpan<char> readOnlySpan;
					ReadOnlySpan<char> readOnlySpan2;
					ReadOnlySpan<char> readOnlySpan3;
					ZoneID.AsDelimitedSpans('.', out readOnlySpan, out readOnlySpan2, out readOnlySpan3);
					string text = new string(readOnlySpan);
					int num = int.Parse(readOnlySpan2, NumberStyles.Integer, null);
					int num2 = int.Parse(readOnlySpan3, NumberStyles.Integer, null);
					list2 = this.GetCellBlueprints(text, num, num2);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Exception on getcellblueprints for: " + ZoneID, ex, "game_exception");
				list2 = new List<CellBlueprint>();
			}
			return list2;
		}

		// Token: 0x06006985 RID: 27013 RVA: 0x002131C8 File Offset: 0x002113C8
		public List<CellBlueprint> GetCellBlueprints(string World, int ParasangX, int ParasangY)
		{
			List<CellBlueprint> list2;
			try
			{
				List<CellBlueprint> list = new List<CellBlueprint>();
				GameObject terrainObjectForZone = ZoneManager.GetTerrainObjectForZone(ParasangX, ParasangY, World);
				if (terrainObjectForZone == null)
				{
					list2 = list;
				}
				else
				{
					WorldBlueprint world = WorldFactory.Factory.getWorld(World);
					if (world == null)
					{
						list2 = list;
					}
					else
					{
						CellBlueprint cellBlueprint;
						if (world.CellBlueprintsByApplication.TryGetValue(terrainObjectForZone.Blueprint, out cellBlueprint))
						{
							list.Add(cellBlueprint);
						}
						string text = ParasangX.ToString() + "." + ParasangY.ToString();
						if (world.CellBlueprintsByApplication.TryGetValue(text, out cellBlueprint))
						{
							list.Add(cellBlueprint);
						}
						list2 = list;
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("GetCellBlueprints", ex, "game_exception");
				list2 = new List<CellBlueprint>();
			}
			return list2;
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x00213284 File Offset: 0x00211484
		public Point3D GetLandingLocation(string World, int ParasangX, int ParasangY)
		{
			foreach (CellBlueprint cellBlueprint in this.GetCellBlueprints(World, ParasangX, ParasangY))
			{
				if (!cellBlueprint.LandingZone.IsNullOrEmpty())
				{
					ReadOnlySpan<char> readOnlySpan;
					ReadOnlySpan<char> readOnlySpan2;
					ReadOnlySpan<char> readOnlySpan3;
					cellBlueprint.LandingZone.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2, out readOnlySpan3);
					int num;
					int num2;
					if (int.TryParse(readOnlySpan, out num) && int.TryParse(readOnlySpan2, out num2))
					{
						int num3;
						if (!int.TryParse(readOnlySpan3, out num3))
						{
							num3 = 10;
						}
						return new Point3D(num, num2, num3);
					}
				}
			}
			return new Point3D(1, 1, 10);
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x00213334 File Offset: 0x00211534
		public void SetZoneDisplayName(string ZoneID, string Name, bool Sync = true)
		{
			this.SetZoneBaseDisplayName(ZoneID, Name, Sync);
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x0021333F File Offset: 0x0021153F
		public ZoneBlueprint GetZoneBlueprint(string ZoneID)
		{
			return this.GetZoneBlueprint(new ZoneRequest(ZoneID));
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x00213350 File Offset: 0x00211550
		public ZoneBlueprint GetZoneBlueprint(string ZoneID, out int ZPos)
		{
			ZoneRequest zoneRequest = new ZoneRequest(ZoneID);
			ZPos = zoneRequest.Z;
			return this.GetZoneBlueprint(zoneRequest);
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x00213374 File Offset: 0x00211574
		public ZoneBlueprint GetZoneBlueprint(ZoneRequest Request)
		{
			if (Request.IsWorldZone)
			{
				return null;
			}
			ref WorldBlueprint ptr = ref Request.World;
			if (ptr == null)
			{
				ptr = WorldFactory.Factory.getWorld(Request.WorldID);
			}
			WorldBlueprint world = Request.World;
			if (world == null)
			{
				return null;
			}
			return world.GetBlueprintFor(Request);
		}

		// Token: 0x0600698B RID: 27019 RVA: 0x002133BC File Offset: 0x002115BC
		public List<ZoneBlueprint> GetZoneBlueprints(ZoneRequest Request)
		{
			if (Request.IsWorldZone)
			{
				return null;
			}
			ref WorldBlueprint ptr = ref Request.World;
			if (ptr == null)
			{
				ptr = WorldFactory.Factory.getWorld(Request.WorldID);
			}
			WorldBlueprint world = Request.World;
			if (world == null)
			{
				return null;
			}
			return world.GetBlueprintsFor(Request);
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x00213403 File Offset: 0x00211603
		public ZoneBlueprint GetZoneBlueprint(string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			return this.GetZoneBlueprint(new ZoneRequest(WorldID, wXPos, wYPos, XPos, YPos, ZPos));
		}

		// Token: 0x0600698D RID: 27021 RVA: 0x00213419 File Offset: 0x00211619
		public ZoneBlueprint GetZoneBlueprint(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			if (ZoneID.IndexOf('.') == -1)
			{
				return null;
			}
			return this.GetZoneBlueprint(WorldID, wXPos, wYPos, XPos, YPos, ZPos);
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x00213438 File Offset: 0x00211638
		public string GetZoneDisplayName(string ZoneID, int ZPos, ZoneBlueprint ZBP, bool WithIndefiniteArticle = false, bool WithDefiniteArticle = false, bool WithStratum = true, bool Mutate = true)
		{
			string zoneBaseDisplayName = this.GetZoneBaseDisplayName(ZoneID, ZBP, Mutate);
			if (zoneBaseDisplayName != null && ZoneID != null && ZoneID.StartsWith("ThinWorld"))
			{
				return zoneBaseDisplayName;
			}
			ZoneManager.SB.Clear();
			if (!zoneBaseDisplayName.IsNullOrEmpty())
			{
				if (WithIndefiniteArticle)
				{
					string zoneIndefiniteArticle = this.GetZoneIndefiniteArticle(ZoneID, ZBP);
					if (zoneIndefiniteArticle == null || zoneIndefiniteArticle == "a")
					{
						if (!this.GetZoneHasProperName(ZoneID, ZBP))
						{
							ZoneManager.SB.Append(Grammar.IndefiniteArticleShouldBeAn(zoneBaseDisplayName) ? "an " : "a ");
						}
					}
					else if (zoneIndefiniteArticle != "")
					{
						ZoneManager.SB.Append(zoneIndefiniteArticle).Append(' ');
					}
				}
				else if (WithDefiniteArticle)
				{
					string zoneDefiniteArticle = this.GetZoneDefiniteArticle(ZoneID, ZBP);
					if (zoneDefiniteArticle == null)
					{
						if (!this.GetZoneHasProperName(ZoneID, ZBP))
						{
							ZoneManager.SB.Append("the ");
						}
					}
					else if (zoneDefiniteArticle == "a")
					{
						if (!this.GetZoneHasProperName(ZoneID, ZBP))
						{
							ZoneManager.SB.Append(Grammar.IndefiniteArticleShouldBeAn(zoneBaseDisplayName) ? "an " : "a ");
						}
					}
					else if (zoneDefiniteArticle != "")
					{
						ZoneManager.SB.Append(zoneDefiniteArticle).Append(' ');
					}
				}
				ZoneManager.SB.Append(zoneBaseDisplayName);
			}
			if (zoneBaseDisplayName.IsNullOrEmpty() || this.GetZoneIncludeContextInZoneDisplay(ZoneID, ZBP))
			{
				string zoneNameContext = this.GetZoneNameContext(ZoneID, ZBP);
				if (zoneNameContext != null)
				{
					if (ZoneManager.SB.Length > 0)
					{
						ZoneManager.SB.Append(", ");
					}
					if (!WithDefiniteArticle && (zoneNameContext.StartsWith("the ") || zoneNameContext.StartsWith("The ")))
					{
						ZoneManager.SB.Append(zoneNameContext, 4, zoneNameContext.Length - 4);
					}
					else if (!WithIndefiniteArticle && (zoneNameContext.StartsWith("an ") || zoneNameContext.StartsWith("An ")))
					{
						ZoneManager.SB.Append(zoneNameContext, 3, zoneNameContext.Length - 3);
					}
					else if (!WithIndefiniteArticle && (zoneNameContext.StartsWith("a ") || zoneNameContext.StartsWith("A ")))
					{
						ZoneManager.SB.Append(zoneNameContext, 2, zoneNameContext.Length - 2);
					}
					else
					{
						ZoneManager.SB.Append(zoneNameContext);
					}
				}
			}
			if (WithStratum && this.GetZoneIncludeStratumInZoneDisplay(ZoneID, ZBP))
			{
				int num = 10 - ZPos;
				if (num == 0)
				{
					ZoneManager.SB.Compound("surface", ", ");
					if (ZBP != null)
					{
						if (ZBP.AnyBuilder((ZoneBuilderBlueprint b) => b.Class == "FlagInside"))
						{
							ZoneManager.SB.Append(" level");
						}
					}
				}
				else
				{
					int num2 = Math.Abs(num);
					ZoneManager.SB.Compound(num2, ", ").Append((num2 == 1) ? " stratum " : " strata ");
					if (num < 0)
					{
						ZoneManager.SB.Append("deep");
					}
					else
					{
						ZoneManager.SB.Append("high");
					}
				}
			}
			return ZoneManager.SB.ToString();
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x00213744 File Offset: 0x00211944
		public void SynchronizeZoneName(string ZoneID, int ZPos, ZoneBlueprint ZBP)
		{
			WorldFactory factory = WorldFactory.Factory;
			if (factory != null)
			{
				factory.UpdateZoneDisplayName(ZoneID, this.GetZoneDisplayName(ZoneID, ZPos, ZBP, false, false, true, true));
			}
			this.NameUpdateTick++;
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x00213780 File Offset: 0x00211980
		public void SynchronizeZoneName(string ZoneID)
		{
			int num;
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, out num);
			this.SynchronizeZoneName(ZoneID, num, zoneBlueprint);
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x002137A0 File Offset: 0x002119A0
		public string GetZoneDisplayName(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos, bool WithIndefiniteArticle = false, bool WithDefiniteArticle = false, bool WithStratum = true, bool Mutate = true)
		{
			return this.GetZoneDisplayName(ZoneID, ZPos, this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos), WithIndefiniteArticle, WithDefiniteArticle, WithStratum, Mutate);
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x002137D0 File Offset: 0x002119D0
		public string GetZoneDisplayName(string ZoneID, bool WithIndefiniteArticle = false, bool WithDefiniteArticle = false, bool WithStratum = true, bool Mutate = true)
		{
			int num;
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, out num);
			return this.GetZoneDisplayName(ZoneID, num, zoneBlueprint, WithIndefiniteArticle, WithDefiniteArticle, WithStratum, Mutate);
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x002137F8 File Offset: 0x002119F8
		public string GetZoneBaseDisplayName(string ZoneID, ZoneBlueprint ZBP, bool Mutate = true)
		{
			string text = The.Game.GetStringGameState("ZoneName_" + ZoneID, null);
			if (text.IsNullOrEmpty())
			{
				if (ZBP == null)
				{
					if (ZoneID.IndexOf('.') == -1)
					{
						text = WorldFactory.Factory.getWorld(ZoneID).DisplayName;
					}
				}
				else
				{
					text = ZBP.Name;
				}
			}
			if (!Mutate || (ZBP != null && !ZBP.HasBiomes))
			{
				return text;
			}
			return BiomeManager.MutateZoneName(text, ZoneID);
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x00213863 File Offset: 0x00211A63
		public string GetZoneBaseDisplayName(string ZoneID, bool Mutate = true)
		{
			return this.GetZoneBaseDisplayName(ZoneID, this.GetZoneBlueprint(ZoneID), Mutate);
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x00213874 File Offset: 0x00211A74
		public void SetZoneBaseDisplayName(string ZoneID, string Name, bool Sync = true)
		{
			The.Game.SetStringGameState("ZoneName_" + ZoneID, Name);
			if (Sync)
			{
				this.SynchronizeZoneName(ZoneID);
				return;
			}
			WorldFactory factory = WorldFactory.Factory;
			if (factory == null)
			{
				return;
			}
			factory.UpdateZoneDisplayName(ZoneID, Name);
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x002138A8 File Offset: 0x00211AA8
		public string GetZoneReferenceDisplayName(string ZoneID, int ZPos, ZoneBlueprint ZBP)
		{
			string text = this.GetZoneBaseDisplayName(ZoneID, ZBP, true);
			if (!text.IsNullOrEmpty())
			{
				if (text.StartsWith("some "))
				{
					text = text.Substring(5);
				}
				if (text.StartsWith("the ") || text.StartsWith("The "))
				{
					text = text.Substring(4);
				}
				else if (text.StartsWith("an ") || text.StartsWith("An "))
				{
					text = text.Substring(3);
				}
				else if (text.StartsWith("a ") || text.StartsWith("A "))
				{
					text = text.Substring(2);
				}
			}
			ZoneManager.SB.Clear();
			if (text.IsNullOrEmpty() || this.GetZoneIncludeContextInZoneDisplay(ZoneID, ZBP))
			{
				string zoneNameContext = this.GetZoneNameContext(ZoneID, ZBP);
				if (zoneNameContext != null)
				{
					if (zoneNameContext.StartsWith("the ") || zoneNameContext.StartsWith("The "))
					{
						ZoneManager.SB.Append(zoneNameContext, 4, zoneNameContext.Length - 4);
					}
					else if (zoneNameContext.StartsWith("an ") || zoneNameContext.StartsWith("An "))
					{
						ZoneManager.SB.Append(zoneNameContext, 3, zoneNameContext.Length - 3);
					}
					else if (zoneNameContext.StartsWith("a ") || zoneNameContext.StartsWith("A "))
					{
						ZoneManager.SB.Append(zoneNameContext, 2, zoneNameContext.Length - 2);
					}
					else
					{
						ZoneManager.SB.Append(zoneNameContext);
					}
				}
			}
			if (!text.IsNullOrEmpty())
			{
				ZoneManager.SB.Compound(text);
			}
			if (this.GetZoneIncludeStratumInZoneDisplay(ZoneID, ZBP))
			{
				int num = 10 - ZPos;
				if (num != 0)
				{
					int num2 = Math.Abs(num);
					ZoneManager.SB.Compound(num2).Append((num2 == 1) ? " stratum " : " strata ");
					if (num < 0)
					{
						ZoneManager.SB.Append("deep");
					}
					else
					{
						ZoneManager.SB.Append("high");
					}
				}
			}
			return ZoneManager.SB.ToString();
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x00213A94 File Offset: 0x00211C94
		public string GetZoneReferenceDisplayName(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			return this.GetZoneReferenceDisplayName(ZoneID, ZPos, this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos));
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x00213ABC File Offset: 0x00211CBC
		public string GetZoneReferenceDisplayName(string ZoneID)
		{
			int num;
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, out num);
			return this.GetZoneReferenceDisplayName(ZoneID, num, zoneBlueprint);
		}

		// Token: 0x06006999 RID: 27033 RVA: 0x00213ADC File Offset: 0x00211CDC
		public void SetZoneNameContext(string ZoneID, string Value, bool Sync = true)
		{
			The.Game.SetStringGameState("ZoneNameContext_" + ZoneID, Value);
			if (Sync)
			{
				this.SynchronizeZoneName(ZoneID);
			}
		}

		// Token: 0x0600699A RID: 27034 RVA: 0x00213B00 File Offset: 0x00211D00
		public string GetZoneNameContext(string ZoneID, ZoneBlueprint ZBP)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneNameContext_" + ZoneID, null);
			if (stringGameState != null)
			{
				if (stringGameState == "")
				{
					return null;
				}
				return stringGameState;
			}
			else
			{
				if (ZBP != null)
				{
					return ZBP.NameContext;
				}
				return null;
			}
		}

		// Token: 0x0600699B RID: 27035 RVA: 0x00213B44 File Offset: 0x00211D44
		public string GetZoneNameContext(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneNameContext_" + ZoneID, null);
			if (stringGameState != null)
			{
				if (stringGameState == "")
				{
					return null;
				}
				return stringGameState;
			}
			else
			{
				ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
				if (zoneBlueprint != null)
				{
					return zoneBlueprint.NameContext;
				}
				return null;
			}
		}

		// Token: 0x0600699C RID: 27036 RVA: 0x00213B9C File Offset: 0x00211D9C
		public string GetZoneNameContext(string ZoneID)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneNameContext_" + ZoneID, null);
			if (stringGameState != null)
			{
				if (stringGameState == "")
				{
					return null;
				}
				return stringGameState;
			}
			else
			{
				ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
				if (zoneBlueprint != null)
				{
					return zoneBlueprint.NameContext;
				}
				return null;
			}
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x00213BE8 File Offset: 0x00211DE8
		public void SetZoneHasProperName(string ZoneID, bool? State)
		{
			if (State == null)
			{
				The.Game.RemoveBooleanGameState("ZoneProperName_" + ZoneID);
			}
			else
			{
				The.Game.SetBooleanGameState("ZoneProperName_" + ZoneID, State.Value);
			}
			this.SynchronizeZoneName(ZoneID);
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x00213C38 File Offset: 0x00211E38
		public bool GetZoneHasProperName(string ZoneID, ZoneBlueprint ZBP)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneProperName_" + ZoneID, out flag))
			{
				return flag;
			}
			if (ZBP != null)
			{
				return ZBP.ProperName;
			}
			return ZoneID.IndexOf('.') == -1;
		}

		// Token: 0x0600699F RID: 27039 RVA: 0x00213C80 File Offset: 0x00211E80
		public bool GetZoneHasProperName(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneProperName_" + ZoneID, out flag))
			{
				return flag;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			return zoneBlueprint != null && zoneBlueprint.ProperName;
		}

		// Token: 0x060069A0 RID: 27040 RVA: 0x00213CCC File Offset: 0x00211ECC
		public bool GetZoneHasProperName(string ZoneID)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneProperName_" + ZoneID, out flag))
			{
				return flag;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			return zoneBlueprint != null && zoneBlueprint.ProperName;
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x00213D0C File Offset: 0x00211F0C
		public void SetZoneIndefiniteArticle(string ZoneID, string Value)
		{
			The.Game.SetStringGameState("ZoneIndefiniteArticle_" + ZoneID, Value);
			this.SynchronizeZoneName(ZoneID);
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x00213D2C File Offset: 0x00211F2C
		public string GetZoneIndefiniteArticle(string ZoneID, ZoneBlueprint ZBP)
		{
			string text = "ZoneIndefiniteArticle_" + ZoneID;
			string stringGameState = The.Game.GetStringGameState(text, null);
			if (stringGameState != null || The.Game.HasStringGameState(text))
			{
				return stringGameState;
			}
			if (ZBP != null)
			{
				return ZBP.IndefiniteArticle;
			}
			return null;
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x00213D70 File Offset: 0x00211F70
		public string GetZoneIndefiniteArticle(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			string text = "ZoneIndefiniteArticle_" + ZoneID;
			string stringGameState = The.Game.GetStringGameState(text, null);
			if (stringGameState != null || The.Game.HasStringGameState(text))
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.IndefiniteArticle;
			}
			return null;
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x00213DC8 File Offset: 0x00211FC8
		public string GetZoneAmbientBed(string ZoneID)
		{
			string text = "ZoneAmbientBed_" + ZoneID;
			string stringGameState = The.Game.GetStringGameState(text, null);
			if (stringGameState != null || The.Game.HasStringGameState(text))
			{
				return stringGameState;
			}
			ZoneRequest zoneRequest = new ZoneRequest(ZoneID);
			if (zoneRequest.IsWorldZone)
			{
				ref WorldBlueprint ptr = ref zoneRequest.World;
				if (ptr == null)
				{
					ptr = WorldFactory.Factory.getWorld(zoneRequest.WorldID);
				}
				WorldBlueprint world = zoneRequest.World;
				if (world == null)
				{
					return null;
				}
				return world.AmbientBed;
			}
			else
			{
				ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(zoneRequest);
				if (zoneBlueprint == null)
				{
					return null;
				}
				return zoneBlueprint.AmbientBed;
			}
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x00213E54 File Offset: 0x00212054
		public string GetZoneAmbientSounds(string ZoneID)
		{
			string text = "ZoneAmbientSounds_" + ZoneID;
			string stringGameState = The.Game.GetStringGameState(text, null);
			if (stringGameState != null || The.Game.HasStringGameState(text))
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.AmbientSounds;
			}
			return null;
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x00213EA0 File Offset: 0x002120A0
		public int GetZoneAmbientVolume(string ZoneID)
		{
			string text = "ZoneAmbientVolume_" + ZoneID;
			int num;
			if (The.Game.IntGameState.TryGetValue(text, out num))
			{
				return num;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			if (zoneBlueprint == null)
			{
				return -1;
			}
			return zoneBlueprint.AmbientVolume;
		}

		// Token: 0x060069A7 RID: 27047 RVA: 0x00213EE4 File Offset: 0x002120E4
		public string GetZoneIndefiniteArticle(string ZoneID)
		{
			string text = "ZoneIndefiniteArticle_" + ZoneID;
			string stringGameState = The.Game.GetStringGameState(text, null);
			if (stringGameState != null || The.Game.HasStringGameState(text))
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.IndefiniteArticle;
			}
			return null;
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x00213F2F File Offset: 0x0021212F
		public void SetZoneDefiniteArticle(string ZoneID, string Value)
		{
			The.Game.SetStringGameState("ZoneDefiniteArticle_" + ZoneID, Value);
			this.SynchronizeZoneName(ZoneID);
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x00213F50 File Offset: 0x00212150
		public string GetZoneDefiniteArticle(string ZoneID, ZoneBlueprint ZBP)
		{
			string text = "ZoneDefiniteArticle_" + ZoneID;
			string stringGameState = The.Game.GetStringGameState(text, null);
			if (stringGameState != null || The.Game.HasStringGameState(text))
			{
				return stringGameState;
			}
			if (ZBP != null)
			{
				return ZBP.DefiniteArticle;
			}
			return null;
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x00213F94 File Offset: 0x00212194
		public string GetZoneDefiniteArticle(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			string text = "ZoneDefiniteArticle_" + ZoneID;
			string stringGameState = The.Game.GetStringGameState(text, null);
			if (stringGameState != null || The.Game.HasStringGameState(text))
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.DefiniteArticle;
			}
			return null;
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x00213FEC File Offset: 0x002121EC
		public string GetZoneDefiniteArticle(string ZoneID)
		{
			string text = "ZoneDefiniteArticle_" + ZoneID;
			string stringGameState = The.Game.GetStringGameState(text, null);
			if (stringGameState != null || The.Game.HasStringGameState(text))
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.DefiniteArticle;
			}
			return null;
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x00214038 File Offset: 0x00212238
		public void SetZoneName(string ZoneID, string Name, string Context = null, string IndefiniteArticle = null, string DefiniteArticle = null, string Article = null, bool Proper = false, bool Sync = true)
		{
			if (Name != null)
			{
				if (Name.StartsWith("the ") || Name.StartsWith("The "))
				{
					Name = Name.Substring(4);
					Article = "the";
					Proper = true;
				}
				else if (Name.StartsWith("an ") || Name.StartsWith("An "))
				{
					Name = Name.Substring(3);
					Article = "a";
					Proper = true;
				}
				else if (Name.StartsWith("a ") || Name.StartsWith("A "))
				{
					Name = Name.Substring(2);
					Article = "a";
					Proper = true;
				}
				else if (Name.StartsWith("some "))
				{
					Name = Name.Substring(5);
					Article = "some";
					Proper = false;
				}
			}
			if (Article != null)
			{
				IndefiniteArticle = Article;
				DefiniteArticle = Article;
			}
			if (Proper)
			{
				if (IndefiniteArticle == null)
				{
					IndefiniteArticle = "";
				}
				if (DefiniteArticle == null)
				{
					DefiniteArticle = "";
				}
			}
			this.SetZoneDisplayName(ZoneID, Name, Sync);
			this.SetZoneNameContext(ZoneID, Context ?? "", Sync);
			this.SetZoneHasProperName(ZoneID, new bool?(Proper));
			this.SetZoneIndefiniteArticle(ZoneID, IndefiniteArticle);
			this.SetZoneDefiniteArticle(ZoneID, DefiniteArticle);
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x0021415C File Offset: 0x0021235C
		public void SetZoneIncludeContextInZoneDisplay(string ZoneID, bool? State)
		{
			if (State == null)
			{
				The.Game.RemoveBooleanGameState("ZoneIncludeContextInZoneDisplay_" + ZoneID);
			}
			else
			{
				The.Game.SetBooleanGameState("ZoneIncludeContextInZoneDisplay_" + ZoneID, State.Value);
			}
			this.SynchronizeZoneName(ZoneID);
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x002141AC File Offset: 0x002123AC
		public bool GetZoneIncludeContextInZoneDisplay(string ZoneID, ZoneBlueprint ZBP)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneIncludeContextInZoneDisplay_" + ZoneID, out flag))
			{
				return flag;
			}
			return ZBP == null || ZBP.IncludeContextInZoneDisplay;
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x002141E4 File Offset: 0x002123E4
		public bool GetZoneIncludeContextInZoneDisplay(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneIncludeContextInZoneDisplay_" + ZoneID, out flag))
			{
				return flag;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			return zoneBlueprint == null || zoneBlueprint.IncludeContextInZoneDisplay;
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x00214230 File Offset: 0x00212430
		public bool GetZoneIncludeContextInZoneDisplay(string ZoneID)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneIncludeContextInZoneDisplay_" + ZoneID, out flag))
			{
				return flag;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			return zoneBlueprint == null || zoneBlueprint.IncludeContextInZoneDisplay;
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x00214270 File Offset: 0x00212470
		public void SetZoneIncludeStratumInZoneDisplay(string ZoneID, bool? State)
		{
			if (State == null)
			{
				The.Game.RemoveBooleanGameState("ZoneIncludeStratumInZoneDisplay_" + ZoneID);
			}
			else
			{
				The.Game.SetBooleanGameState("ZoneIncludeStratumInZoneDisplay_" + ZoneID, State.Value);
			}
			this.SynchronizeZoneName(ZoneID);
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x002142C0 File Offset: 0x002124C0
		public bool GetZoneIncludeStratumInZoneDisplay(string ZoneID, ZoneBlueprint ZBP)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneIncludeStratumInZoneDisplay_" + ZoneID, out flag))
			{
				return flag;
			}
			return ZBP == null || ZBP.IncludeStratumInZoneDisplay;
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x002142F8 File Offset: 0x002124F8
		public bool GetZoneIncludeStratumInZoneDisplay(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneIncludeStratumInZoneDisplay_" + ZoneID, out flag))
			{
				return flag;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			return zoneBlueprint == null || zoneBlueprint.IncludeStratumInZoneDisplay;
		}

		// Token: 0x060069B4 RID: 27060 RVA: 0x00214344 File Offset: 0x00212544
		public bool GetZoneIncludeStratumInZoneDisplay(string ZoneID)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneIncludeStratumInZoneDisplay_" + ZoneID, out flag))
			{
				return flag;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			return zoneBlueprint == null || zoneBlueprint.IncludeContextInZoneDisplay;
		}

		// Token: 0x060069B5 RID: 27061 RVA: 0x00214384 File Offset: 0x00212584
		public void SetZoneNamedByPlayer(string ZoneID, bool State)
		{
			if (State)
			{
				The.Game.SetBooleanGameState("ZoneNamedByPlayer_" + ZoneID, true);
				return;
			}
			The.Game.RemoveBooleanGameState("ZoneNamedByPlayer_" + ZoneID);
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x002143B5 File Offset: 0x002125B5
		public bool GetZoneNamedByPlayer(string ZoneID)
		{
			return The.Game.GetBooleanGameState("ZoneNamedByPlayer_" + ZoneID, false);
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x002143CD File Offset: 0x002125CD
		public void SetZoneHasWeather(string ZoneID, bool? State)
		{
			if (State == null)
			{
				The.Game.RemoveBooleanGameState("ZoneHasWeather_" + ZoneID);
				return;
			}
			The.Game.SetBooleanGameState("ZoneHasWeather_" + ZoneID, State.Value);
		}

		// Token: 0x060069B8 RID: 27064 RVA: 0x0021440A File Offset: 0x0021260A
		public bool GetZoneHasStaticRingGate(string ZoneID, ZoneBlueprint ZBP)
		{
			return ZBP != null && ZBP.HasWeather;
		}

		// Token: 0x060069B9 RID: 27065 RVA: 0x00214418 File Offset: 0x00212618
		public bool GetZoneStaticRingGate(string ZoneID)
		{
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			return zoneBlueprint != null && zoneBlueprint.HasWeather;
		}

		// Token: 0x060069BA RID: 27066 RVA: 0x00214438 File Offset: 0x00212638
		public bool GetZoneHasWeather(string ZoneID, ZoneBlueprint ZBP)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneHasWeather_" + ZoneID, out flag))
			{
				return flag;
			}
			return ZBP != null && ZBP.HasWeather;
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x00214470 File Offset: 0x00212670
		public bool GetZoneHasWeather(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneHasWeather_" + ZoneID, out flag))
			{
				return flag;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			return zoneBlueprint != null && zoneBlueprint.HasWeather;
		}

		// Token: 0x060069BC RID: 27068 RVA: 0x002144BC File Offset: 0x002126BC
		public bool GetZoneHasWeather(string ZoneID)
		{
			bool flag;
			if (XRLCore.Core.Game.TryGetBooleanGameState("ZoneHasWeather_" + ZoneID, out flag))
			{
				return flag;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			return zoneBlueprint != null && zoneBlueprint.HasWeather;
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x002144FC File Offset: 0x002126FC
		public void SetZoneWindSpeed(string ZoneID, string Value)
		{
			The.Game.SetStringGameState("ZoneWindSpeed_" + ZoneID, Value);
		}

		// Token: 0x060069BE RID: 27070 RVA: 0x00214514 File Offset: 0x00212714
		public string GetZoneWindSpeed(string ZoneID, ZoneBlueprint ZBP)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindSpeed_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			if (ZBP != null)
			{
				return ZBP.WindSpeed;
			}
			return null;
		}

		// Token: 0x060069BF RID: 27071 RVA: 0x00214554 File Offset: 0x00212754
		public string GetZoneWindSpeed(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindSpeed_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.WindSpeed;
			}
			return null;
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x002145A4 File Offset: 0x002127A4
		public string GetZoneWindSpeed(string ZoneID)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindSpeed_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.WindSpeed;
			}
			return null;
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x002145E9 File Offset: 0x002127E9
		public void SetZoneWindDirections(string ZoneID, string Value)
		{
			The.Game.SetStringGameState("ZoneWindDirections_" + ZoneID, Value);
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x00214604 File Offset: 0x00212804
		public string GetZoneWindDirections(string ZoneID, ZoneBlueprint ZBP)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindDirections_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			if (ZBP != null)
			{
				return ZBP.WindDirections;
			}
			return null;
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x00214644 File Offset: 0x00212844
		public string GetZoneWindDirections(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindDirections_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.WindDirections;
			}
			return null;
		}

		// Token: 0x060069C4 RID: 27076 RVA: 0x00214694 File Offset: 0x00212894
		public string GetZoneWindDirections(string ZoneID)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindDirections_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.WindDirections;
			}
			return null;
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x002146D9 File Offset: 0x002128D9
		public void SetZoneWindDuration(string ZoneID, string Value)
		{
			The.Game.SetStringGameState("ZoneWindDuration_" + ZoneID, Value);
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x002146F4 File Offset: 0x002128F4
		public string GetZoneWindDuration(string ZoneID, ZoneBlueprint ZBP)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindDuration_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			if (ZBP != null)
			{
				return ZBP.WindDuration;
			}
			return null;
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x00214734 File Offset: 0x00212934
		public string GetZoneWindDuration(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindDuration_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID, WorldID, wXPos, wYPos, XPos, YPos, ZPos);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.WindDuration;
			}
			return null;
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x00214784 File Offset: 0x00212984
		public string GetZoneWindDuration(string ZoneID)
		{
			string stringGameState = The.Game.GetStringGameState("ZoneWindDuration_" + ZoneID, "");
			if (!stringGameState.IsNullOrEmpty())
			{
				return stringGameState;
			}
			ZoneBlueprint zoneBlueprint = this.GetZoneBlueprint(ZoneID);
			if (zoneBlueprint != null)
			{
				return zoneBlueprint.WindDuration;
			}
			return null;
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x002147C9 File Offset: 0x002129C9
		public void SetZoneCurrentWindSpeed(string ZoneID, int Value)
		{
			The.Game.SetIntGameState("ZoneCurrentWindSpeed_" + ZoneID, Value);
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x002147E1 File Offset: 0x002129E1
		public int GetZoneCurrentWindSpeed(string ZoneID, ZoneBlueprint ZBP)
		{
			return this.GetZoneCurrentWindSpeed(ZoneID);
		}

		// Token: 0x060069CB RID: 27083 RVA: 0x002147EA File Offset: 0x002129EA
		public int GetZoneCurrentWindSpeed(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			return this.GetZoneCurrentWindSpeed(ZoneID);
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x002147F3 File Offset: 0x002129F3
		public int GetZoneCurrentWindSpeed(string ZoneID)
		{
			return The.Game.GetIntGameState("ZoneCurrentWindSpeed_" + ZoneID, 0);
		}

		// Token: 0x060069CD RID: 27085 RVA: 0x0021480B File Offset: 0x00212A0B
		public void SetZoneCurrentWindDirection(string ZoneID, string Value)
		{
			The.Game.SetStringGameState("ZoneCurrentWindDirection_" + ZoneID, Value);
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x00214823 File Offset: 0x00212A23
		public string GetZoneCurrentWindDirection(string ZoneID, ZoneBlueprint ZBP)
		{
			return this.GetZoneCurrentWindDirection(ZoneID);
		}

		// Token: 0x060069CF RID: 27087 RVA: 0x0021482C File Offset: 0x00212A2C
		public string GetZoneCurrentWindDirection(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			return this.GetZoneCurrentWindDirection(ZoneID);
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x00214835 File Offset: 0x00212A35
		public string GetZoneCurrentWindDirection(string ZoneID)
		{
			return The.Game.GetStringGameState("ZoneCurrentWindDirection_" + ZoneID, "");
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x00214851 File Offset: 0x00212A51
		public void SetZoneNextWindChange(string ZoneID, long Value)
		{
			The.Game.SetInt64GameState("ZoneNextWindChange_" + ZoneID, Value);
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x00214869 File Offset: 0x00212A69
		public long GetZoneNextWindChange(string ZoneID, ZoneBlueprint ZBP)
		{
			return (long)this.GetZoneCurrentWindSpeed(ZoneID);
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x00214873 File Offset: 0x00212A73
		public long GetZoneNextWindChange(string ZoneID, string WorldID, int wXPos, int wYPos, int XPos, int YPos, int ZPos)
		{
			return (long)this.GetZoneCurrentWindSpeed(ZoneID);
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x0021487D File Offset: 0x00212A7D
		public long GetZoneNextWindChange(string ZoneID)
		{
			return The.Game.GetInt64GameState("ZoneNextWindChange_" + ZoneID, 0L);
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x00214898 File Offset: 0x00212A98
		public GameObject GetOneCreatureFromZone(string ZoneID)
		{
			string text = "LairBosses" + The.Game.ZoneManager.GetZoneTier(ZoneID).ToString();
			int num = 0;
			while (++num < 100)
			{
				string blueprint = PopulationManager.RollOneFrom(text, null, null).Blueprint;
				GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints[blueprint];
				string text2;
				if (gameObjectBlueprint.HasPart("Combat") && gameObjectBlueprint.HasPart("Brain") && (!gameObjectBlueprint.Props.TryGetValue("Role", out text2) || !(text2 == "Minion")) && (!gameObjectBlueprint.Tags.TryGetValue("Role", out text2) || !(text2 == "Minion")) && !gameObjectBlueprint.HasPart("GenericInventoryRestocker"))
				{
					return GameObject.Create(gameObjectBlueprint, 0, 0, null, null, null, null, null);
				}
			}
			return EncountersAPI.GetANonLegendaryCreature(null);
		}

		// Token: 0x060069D6 RID: 27094 RVA: 0x00214980 File Offset: 0x00212B80
		public void AdjustZoneGenerationTierTo(string ZoneID)
		{
			ZoneManager.zoneGenerationContextZoneID = ZoneID;
			ZoneManager.zoneGenerationContextTier = this.GetZoneTier(ZoneID);
		}

		// Token: 0x060069D7 RID: 27095 RVA: 0x00214994 File Offset: 0x00212B94
		public int GetZoneTier(string ZoneID)
		{
			if (string.IsNullOrEmpty(ZoneID) || !ZoneID.Contains("."))
			{
				return 1;
			}
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			ReadOnlySpan<char> readOnlySpan3;
			ReadOnlySpan<char> readOnlySpan4;
			ReadOnlySpan<char> readOnlySpan5;
			ReadOnlySpan<char> readOnlySpan6;
			ZoneID.AsDelimitedSpans('.', out readOnlySpan, out readOnlySpan2, out readOnlySpan3, out readOnlySpan4, out readOnlySpan5, out readOnlySpan6);
			return this.GetZoneTier(new string(readOnlySpan), int.Parse(readOnlySpan2, NumberStyles.Integer, null), int.Parse(readOnlySpan3, NumberStyles.Integer, null), int.Parse(readOnlySpan6, NumberStyles.Integer, null));
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x002149F0 File Offset: 0x00212BF0
		public int GetZoneTier(string world, int wXPos, int wYPos, int ZPos)
		{
			if (string.IsNullOrEmpty(world) || !world.Contains("."))
			{
				return 1;
			}
			Zone zone = this.GetZone(world);
			int num = 1;
			foreach (GameObject gameObject in zone.GetCell(wXPos, wYPos).Objects)
			{
				int num2 = Convert.ToInt32(gameObject.GetTag("RegionTier", "1"));
				if (num2 > num)
				{
					num = num2;
				}
			}
			if (ZPos > 15)
			{
				num = Math.Abs(ZPos - 16) / 5 + 2;
			}
			if (num < 1)
			{
				num = 1;
			}
			if (num > 8)
			{
				num = 8;
			}
			return num;
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x00214AA0 File Offset: 0x00212CA0
		public GameObject GetZoneTerrain(string world, int wXPos, int wYPos)
		{
			return this.GetZone(world).GetCell(wXPos, wYPos).GetFirstObjectWithPart("TerrainTravel");
		}

		// Token: 0x060069DA RID: 27098 RVA: 0x00214ABC File Offset: 0x00212CBC
		private bool GenerateFactoryZone(ZoneRequest Request, out Zone result)
		{
			result = null;
			IZoneFactory zoneFactoryInstance = Request.World.ZoneFactoryInstance;
			if (zoneFactoryInstance == null)
			{
				return false;
			}
			if (!zoneFactoryInstance.CanBuildZone(Request))
			{
				return false;
			}
			Zone zone = zoneFactoryInstance.BuildZone(Request);
			Zone zone2 = zone;
			if (zone2.ZoneID == null)
			{
				zone2.ZoneID = Request.ZoneID;
			}
			this.AddCachedZone(zone);
			BeforeZoneBuiltEvent.Send(zone);
			ZoneBuiltEvent.Send(zone);
			ZoneManager.PaintWalls(zone, 0, 0, -1, -1);
			ZoneManager.PaintWater(zone, 0, 0, -1, -1);
			AfterZoneBuiltEvent.Send(zone);
			zoneFactoryInstance.AfterBuildZone(zone, this);
			result = zone;
			return true;
		}

		// Token: 0x060069DB RID: 27099 RVA: 0x00214B44 File Offset: 0x00212D44
		public void GenerateZone(string ZoneID)
		{
			if (!ZoneManager.ProcessingZones.Add(ZoneID))
			{
				MetricsManager.LogError("Attempting to generate reserved zone.");
				return;
			}
			Event.PinCurrentPool();
			ZonePartCollection zonePartCollection = ((ZoneManager.PartCollections.Count > 0) ? ZoneManager.PartCollections.Pop() : new ZonePartCollection());
			ZoneBuilderCollection zoneBuilderCollection = ((ZoneManager.BuilderCollections.Count > 0) ? ZoneManager.BuilderCollections.Pop() : new ZoneBuilderCollection());
			Loading.Status status = Loading.StartTask("Building zone...", "Building " + ZoneID, true);
			try
			{
				Coach.StartSection("GenerateZone", true);
				int num = 0;
				string text = "<none>";
				ZoneRequest zoneRequest;
				Zone zone;
				ref WorldBlueprint ptr;
				Zone zone2;
				for (;;)
				{
					IL_008D:
					this.CachedObjectsToRemoveAfterZoneBuild = new List<string>();
					Event.ResetToPin();
					zonePartCollection.Clear();
					zoneBuilderCollection.Clear();
					text != "<none>";
					num++;
					bool flag = false;
					if (num >= 20 && num % 5 == 0 && Popup.ShowYesNo("This zone isn't building properly. Do you want to force it to stop and build immediately?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						flag = true;
					}
					zoneRequest = new ZoneRequest(ZoneID);
					foreach (WorldBlueprint worldBlueprint in WorldFactory.Factory.getWorlds())
					{
						if (worldBlueprint.IsMatch(ZoneID))
						{
							zoneRequest.World = worldBlueprint;
							if (this.GenerateFactoryZone(zoneRequest, out zone))
							{
								return;
							}
							break;
						}
					}
					if (zoneRequest.IsWorldZone)
					{
						break;
					}
					ptr = ref zoneRequest.World;
					if (ptr == null)
					{
						ptr = WorldFactory.Factory.getWorld(zoneRequest.WorldID);
					}
					if (!zoneRequest.World.ZoneFactory.IsNullOrEmpty() && this.GenerateFactoryZone(zoneRequest, out zone))
					{
						goto Block_17;
					}
					zone2 = (zoneRequest.Zone = zoneRequest.World.GenerateZone(zoneRequest, 80, 25));
					zone2.ZoneID = ZoneID;
					zone2.BuildTries = num;
					zone2.Tier = this.GetZoneTier(ZoneID);
					this.AddCachedZone(zone2);
					ZoneManager.ZoneGenerationContext = zone2;
					ZoneManager.zoneGenerationContextTier = zone2.NewTier;
					ZoneManager.zoneGenerationContextZoneID = ZoneID;
					bool flag2 = false;
					string text2 = (zoneRequest.Seed = "ZONESEED" + ZoneID);
					string text3 = The.Game.GetWorldSeed(text2).ToString();
					if (num > 30 && !Options.DisableTryLimit)
					{
						goto Block_19;
					}
					Stat.ReseedFrom(text3 + num.ToString(), false);
					MetricsManager.rngCheckpoint(text2 + "startbuild" + num.ToString());
					ZoneBuilderCollection zoneBuilderCollection2;
					if (this.ZoneBuilders.TryGetValue(ZoneID, out zoneBuilderCollection2))
					{
						zoneBuilderCollection.AddRange(zoneBuilderCollection2);
					}
					ZonePartCollection zonePartCollection2;
					if (this.ZoneParts.TryGetValue(ZoneID, out zonePartCollection2))
					{
						zonePartCollection.AddRange(zonePartCollection2);
					}
					foreach (ZoneBlueprint zoneBlueprint in (this.HasZoneProperty(ZoneID, "SkipTerrainBuilders") ? new List<ZoneBlueprint>() : zoneRequest.World.GetBlueprintsFor(zoneRequest)))
					{
						CellBlueprint cell = zoneBlueprint.Cell;
						this.ApplyPropertiesToZone((cell != null) ? cell.Properties : null, zone2);
						this.ApplyPropertiesToZone(zoneBlueprint.Properties, zone2);
						if (!zoneBlueprint.Parts.IsReadOnlyNullOrEmpty<ZonePartBlueprint>())
						{
							zonePartCollection.AddRange(zoneBlueprint.Parts);
						}
						if (!zoneBlueprint.Builders.IsReadOnlyNullOrEmpty<ZoneBuilderBlueprint>())
						{
							zoneBuilderCollection.AddRange(zoneBlueprint.Builders);
						}
						if (!zoneBlueprint.GroundLiquid.IsNullOrEmpty())
						{
							zone2.GroundLiquid = zoneBlueprint.GroundLiquid;
						}
					}
					if (zonePartCollection.ApplyTo(zone2, text2, flag) && zoneBuilderCollection.ApplyTo(zone2, text2, flag))
					{
						if (Options.ShowReachable)
						{
							ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
							for (int i = 0; i < 80; i++)
							{
								for (int j = 0; j < 25; j++)
								{
									scrapBuffer.Goto(i, j);
									if (zone2.IsReachable(i, j))
									{
										scrapBuffer.Write(".", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Write("#", true, false, false, null, -1);
									}
								}
							}
							foreach (ZoneConnection zoneConnection in this.GetZoneConnections(zone2.ZoneID))
							{
								scrapBuffer.Goto(zoneConnection.X + 1, zoneConnection.Y + 1);
								scrapBuffer.Write(zoneConnection.ToString(), true, false, false, null, -1);
							}
							foreach (CachedZoneConnection cachedZoneConnection in zone2.ZoneConnectionCache)
							{
								scrapBuffer.Goto(cachedZoneConnection.X + 1, cachedZoneConnection.Y + 1);
								scrapBuffer.Write(cachedZoneConnection.ToString(), true, false, false, null, -1);
							}
							foreach (ZoneConnection zoneConnection2 in this.GetZoneConnections(zone2.ZoneID))
							{
								scrapBuffer.Goto(zoneConnection2.X, zoneConnection2.Y);
								if (!zone2.IsReachable(zoneConnection2.X, zoneConnection2.Y))
								{
									scrapBuffer.Write("&RX", true, false, false, null, -1);
								}
								else
								{
									scrapBuffer.Write("&GX", true, false, false, null, -1);
								}
							}
							foreach (CachedZoneConnection cachedZoneConnection2 in zone2.ZoneConnectionCache)
							{
								if (cachedZoneConnection2.TargetDirection == "-")
								{
									scrapBuffer.Goto(cachedZoneConnection2.X, cachedZoneConnection2.Y);
									if (!zone2.IsReachable(cachedZoneConnection2.X, cachedZoneConnection2.Y))
									{
										scrapBuffer.Write("&RX", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Write("&GX", true, false, false, null, -1);
									}
								}
							}
							Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
							Keyboard.getch();
						}
						flag2 |= zone2.GetZoneProperty("DisableForcedConnections", "") == "Yes";
						if (!flag2)
						{
							List<Point2D> list = new List<Point2D>();
							for (int k = 0; k < zone2.Height; k++)
							{
								for (int l = 0; l < zone2.Width; l++)
								{
									Cell cell2 = zone2.GetCell(l, k);
									if (cell2.HasObjectWithBlueprint("StairsUp") || cell2.HasObjectWithBlueprint("StairsDown"))
									{
										cell2.ClearWalls();
										list.Add(cell2.Pos2D);
									}
								}
							}
							new ForceConnections()._BuildZone(zone2, list);
						}
						Coach.StartSection("ConnectionChecks", true);
						if (!flag2)
						{
							foreach (ZoneConnection zoneConnection3 in this.GetZoneConnections(zone2.ZoneID))
							{
								if (!zone2.IsReachable(zoneConnection3.X, zoneConnection3.Y) && !flag)
								{
									text = string.Concat(new string[]
									{
										"Connection ZC:",
										zoneConnection3.Type,
										",",
										zoneConnection3.X.ToString(),
										",",
										zoneConnection3.Y.ToString()
									});
									Coach.EndSection();
									goto IL_008D;
								}
							}
							foreach (CachedZoneConnection cachedZoneConnection3 in zone2.ZoneConnectionCache)
							{
								if (cachedZoneConnection3.TargetDirection == "-" && !zone2.IsReachable(cachedZoneConnection3.X, cachedZoneConnection3.Y) && !flag)
								{
									text = string.Concat(new string[]
									{
										"Connection Cached:",
										cachedZoneConnection3.Type,
										",",
										cachedZoneConnection3.X.ToString(),
										",",
										cachedZoneConnection3.Y.ToString(),
										" in cell: ",
										zone2.GetCell(cachedZoneConnection3.X, cachedZoneConnection3.Y).ToString()
									});
									Coach.EndSection();
									goto IL_008D;
								}
							}
							goto IL_0947;
						}
						goto IL_0947;
					}
				}
				ptr = ref zoneRequest.World;
				if (ptr == null)
				{
					ptr = WorldFactory.Factory.getWorld(ZoneID);
				}
				if (!this.GenerateFactoryZone(zoneRequest, out zone))
				{
					Zone zone3 = (zoneRequest.Zone = zoneRequest.World.GenerateZone(zoneRequest, 80, 25));
					zone3.ZoneID = ZoneID;
					this.AddCachedZone(zone3);
					ZoneManager.ZoneGenerationContext = zone;
					ZoneManager.zoneGenerationContextTier = 1;
					ZoneManager.zoneGenerationContextZoneID = ZoneID;
					if (!zoneRequest.World.Map.IsNullOrEmpty())
					{
						new MapBuilder
						{
							ID = zoneRequest.World.Map
						}.BuildZone(zone3);
					}
					zone3.GetCell(0, 0).AddObject("AmbientLight", null, null, null, null);
					zone3.Built = true;
					BeforeZoneBuiltEvent.Send(zone3);
					ZoneBuiltEvent.Send(zone3);
					ZoneManager.PaintWater(zone3, 0, 0, -1, -1);
					AfterZoneBuiltEvent.Send(zone3);
					goto IL_0E59;
				}
				Block_17:
				return;
				Block_19:
				zone2.ClearReachableMap(true);
				MessageQueue.AddPlayerMessage("Zone build failure:" + text, 'R', true);
				goto IL_09D9;
				IL_0947:
				foreach (ZoneConnection zoneConnection4 in this.GetZoneConnectionsCopy(zone2.ZoneID))
				{
					Cell cell3 = zone2.GetCell(zoneConnection4.X, zoneConnection4.Y);
					cell3.ClearWalls();
					if (!zoneConnection4.Object.IsNullOrEmpty() && !cell3.HasObject(zoneConnection4.Object))
					{
						cell3.AddObject(zoneConnection4.Object, null, null, null, null);
					}
				}
				this.CachedObjectsToRemoveAfterZoneBuild.Clear();
				IL_09D9:
				Coach.EndSection();
				zone2.Built = true;
				zone2.WriteZoneConnectionCache();
				if (zone2.IsOutside())
				{
					zone2.GetCell(0, 0).AddObject("DaylightWidget", null, null, null, null);
				}
				BiomeManager.MutateZone(zone2);
				try
				{
					if (this.CachedZones != null)
					{
						List<Point2D> list2 = new List<Point2D>();
						for (int m = 0; m < zone2.Height; m++)
						{
							for (int n = 0; n < zone2.Width; n++)
							{
								Cell cell4 = zone2.GetCell(n, m);
								if (cell4.HasObjectWithBlueprint("StairsUp") || cell4.HasObjectWithBlueprint("StairsDown"))
								{
									cell4.ClearWalls();
									list2.Add(cell4.Pos2D);
								}
							}
						}
						string zoneIDFromDirection = zone2.GetZoneIDFromDirection("U", 1);
						Zone zone4;
						if (zoneIDFromDirection != null && this.CachedZones.TryGetValue(zoneIDFromDirection, out zone4))
						{
							for (int num2 = 0; num2 < zone4.Height; num2++)
							{
								for (int num3 = 0; num3 < zone4.Width; num3++)
								{
									Cell cell5 = zone4.GetCell(num3, num2);
									if (cell5.HasObjectWithBlueprint("OpenShaft"))
									{
										zone2.GetCell(num3, num2).ClearWalls();
										list2.Add(zone2.GetCell(num3, num2).Pos2D);
									}
									if (cell5.HasObjectWithBlueprint("StairsDown"))
									{
										zone2.GetCell(num3, num2).ClearWalls();
										zone2.GetCell(num3, num2).AddObject("StairsUp", null, null, null, null);
										list2.Add(zone2.GetCell(num3, num2).Pos2D);
									}
								}
							}
						}
						string zoneIDFromDirection2 = zone2.GetZoneIDFromDirection("D", 1);
						Zone zone5;
						if (zoneIDFromDirection2 != null && this.CachedZones.TryGetValue(zoneIDFromDirection2, out zone5))
						{
							for (int num4 = 0; num4 < zone5.Height; num4++)
							{
								for (int num5 = 0; num5 < zone5.Width; num5++)
								{
									if (zone5.GetCell(num5, num4).HasObjectWithBlueprint("StairsUp"))
									{
										zone2.GetCell(num5, num4).ClearWalls();
										zone2.GetCell(num5, num4).AddObject("StairsDown", null, null, null, null);
										list2.Add(zone2.GetCell(num5, num4).Pos2D);
									}
								}
							}
						}
						string zoneIDFromDirection3 = zone2.GetZoneIDFromDirection("N", 1);
						Zone zone6;
						if (zoneIDFromDirection3 != null && this.CachedZones.TryGetValue(zoneIDFromDirection3, out zone6))
						{
							for (int num6 = 0; num6 < zone6.Width; num6++)
							{
								Cell cell6 = zone6.GetCell(num6, zone6.Height - 1);
								if (cell6 != null && !cell6.IsSolid())
								{
									zone2.GetCell(num6, 0).ClearWalls();
								}
							}
						}
						string zoneIDFromDirection4 = zone2.GetZoneIDFromDirection("S", 1);
						Zone zone7;
						if (zoneIDFromDirection4 != null && this.CachedZones.TryGetValue(zoneIDFromDirection4, out zone7))
						{
							for (int num7 = 0; num7 < zone7.Width; num7++)
							{
								Cell cell7 = zone7.GetCell(num7, 0);
								if (cell7 != null && !cell7.IsSolid())
								{
									zone2.GetCell(num7, zone2.Height - 1).ClearWalls();
								}
							}
						}
						string zoneIDFromDirection5 = zone2.GetZoneIDFromDirection("E", 1);
						Zone zone8;
						if (zoneIDFromDirection5 != null && this.CachedZones.TryGetValue(zoneIDFromDirection5, out zone8))
						{
							for (int num8 = 0; num8 < zone8.Height; num8++)
							{
								Cell cell8 = zone8.GetCell(0, num8);
								if (cell8 != null && !cell8.IsSolid())
								{
									zone2.GetCell(zone2.Width - 1, num8).ClearWalls();
								}
							}
						}
						string zoneIDFromDirection6 = zone2.GetZoneIDFromDirection("W", 1);
						Zone zone9;
						if (zoneIDFromDirection6 != null && this.CachedZones.TryGetValue(zoneIDFromDirection6, out zone9))
						{
							for (int num9 = 0; num9 < zone9.Height; num9++)
							{
								Cell cell9 = zone9.GetCell(0, num9);
								if (cell9 != null && !cell9.IsSolid())
								{
									zone2.GetCell(0, num9).ClearWalls();
								}
							}
						}
						try
						{
							bool flag2;
							if (!flag2 && zone2.GetZoneProperty("DisableForcedConnections", "") != "Yes")
							{
								new ForceConnections()._BuildZone(zone2, list2);
							}
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("Exception connecting stairs", ex, "game_exception");
						}
					}
				}
				catch
				{
				}
				ZoneManager.SanityCheck(zone2);
				BeforeZoneBuiltEvent.Send(zone2);
				ZoneBuiltEvent.Send(zone2);
				ZoneManager.PaintWalls(zone2, 0, 0, -1, -1);
				ZoneManager.PaintWater(zone2, 0, 0, -1, -1);
				AfterZoneBuiltEvent.Send(zone2);
				ZoneManager.ForceCollect();
				IL_0E59:
				Coach.EndSection();
			}
			catch (Exception ex2)
			{
				if (Popup.ShowYesNo("There was an issue building this zone. Automatically report it to us? " + ex2.ToString(), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
				{
					MetricsManager.LogException("Zone build", ex2, "game_exception");
				}
				this.AddCachedZone(new Zone(80, 25)
				{
					ZoneID = ZoneID,
					BuildTries = 0,
					Tier = 0
				});
			}
			finally
			{
				status.Dispose();
				ZoneManager.ProcessingZones.Remove(ZoneID);
				zoneBuilderCollection.Clear();
				ZoneManager.BuilderCollections.Push(zoneBuilderCollection);
				zonePartCollection.Clear();
				ZoneManager.PartCollections.Push(zonePartCollection);
			}
			Event.ResetToPin();
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x00215B8C File Offset: 0x00213D8C
		public static void SanityCheck(Zone Z)
		{
			try
			{
				for (int i = 0; i < Z.Height; i++)
				{
					for (int j = 0; j < Z.Width; j++)
					{
						Cell cell = Z.Map[j][i];
						if (cell.CountObjectsWithTag("Stairs") > 1 && cell.CountObjectsWithTag("Pit") >= 1)
						{
							foreach (GameObject gameObject in cell.GetObjectsWithTag("Stairs"))
							{
								if (!gameObject.HasTag("Pit"))
								{
									gameObject.Obliterate(null, false, null);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("SanityCheck::NoStairsOnPits", ex, "game_exception");
			}
			try
			{
				for (int k = 0; k < Z.Height; k++)
				{
					for (int l = 0; l < Z.Width; l++)
					{
						Cell cell2 = Z.Map[l][k];
						if (cell2.CountObjectsWithTag("Stairs") > 1)
						{
							foreach (GameObject gameObject2 in cell2.GetObjectsWithTag("Stairs"))
							{
								XRL.World.Parts.StairsDown stairsDown;
								if (gameObject2.TryGetPart<XRL.World.Parts.StairsDown>(out stairsDown) && stairsDown.PullDown)
								{
									gameObject2.Obliterate(null, false, null);
								}
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogException("SanityCheck::NoStairsOnShafts", ex2, "game_exception");
			}
			try
			{
				for (int m = 0; m < Z.Height; m++)
				{
					for (int n = 0; n < Z.Width; n++)
					{
						Cell cell3 = Z.Map[n][m];
						if (cell3.HasWall() && cell3.HasCombatObject(false))
						{
							foreach (GameObject gameObject3 in cell3.GetObjectsWithPart("Combat"))
							{
								if (gameObject3.Brain != null && !gameObject3.Brain.LivesOnWalls && !gameObject3.HasTagOrProperty("IgnoreWallSanityCheck"))
								{
									Cell closestPassableCellFor = cell3.getClosestPassableCellFor(gameObject3);
									gameObject3.DirectMoveTo(closestPassableCellFor, 0, false, false, false, null);
								}
							}
						}
					}
				}
			}
			catch (Exception ex3)
			{
				MetricsManager.LogException("SanityCheck::NoMonstersInWalls", ex3, "game_exception");
			}
			try
			{
				for (int num = 0; num < Z.Height; num++)
				{
					for (int num2 = 0; num2 < Z.Width; num2++)
					{
						Cell cell4 = Z.Map[num2][num];
						if (cell4.HasObjectWithTag("Stairs") && cell4.HasCombatObject(false))
						{
							foreach (GameObject gameObject4 in cell4.GetObjectsWithTag("Combat"))
							{
								if (!gameObject4.HasTagOrProperty("IgnoreStairSanityCheck"))
								{
									Cell closestPassableCellFor2 = cell4.getClosestPassableCellFor(gameObject4);
									gameObject4.DirectMoveTo(closestPassableCellFor2, 0, false, false, false, null);
								}
							}
						}
					}
				}
			}
			catch (Exception ex4)
			{
				MetricsManager.LogException("SanityCheck::NoMonsterOnStairs", ex4, "game_exception");
			}
			try
			{
				for (int num3 = 0; num3 < Z.Height; num3++)
				{
					for (int num4 = 0; num4 < Z.Width; num4++)
					{
						Cell cell5 = Z.Map[num4][num3];
						if (cell5.HasStairs())
						{
							cell5.ClearWalls();
							if (cell5.IsSolid())
							{
								foreach (GameObject gameObject5 in cell5.GetSolidObjects())
								{
									if (gameObject5.CanClear(false, false))
									{
										gameObject5.Obliterate(null, false, null);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex5)
			{
				MetricsManager.LogException("SanityCheck::NoStairsOnSolids", ex5, "game_exception");
			}
			try
			{
				for (int num5 = 0; num5 < Z.Height; num5++)
				{
					for (int num6 = 0; num6 < Z.Width; num6++)
					{
						Cell cell6 = Z.Map[num6][num5];
						if (cell6.HasStairs())
						{
							if (cell6.HasObject(delegate(GameObject o)
							{
								XRL.World.Parts.StairsDown stairsDown2;
								return o.TryGetPart<XRL.World.Parts.StairsDown>(out stairsDown2) && stairsDown2.PullDown;
							}))
							{
								using (List<GameObject>.Enumerator enumerator = cell6.GetObjects((GameObject o) => o.CanFall).GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										enumerator.Current.SystemMoveTo(cell6.getClosestPassableCell((Cell c) => !c.HasObjectWithPart("StairsDown")), null, false, true, false, false);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex6)
			{
				MetricsManager.LogException("SanityCheck::No falling objects on pulldowns", ex6, "game_exception");
			}
			try
			{
				for (int num7 = 0; num7 < Z.Height; num7++)
				{
					for (int num8 = 0; num8 < Z.Width; num8++)
					{
						Cell cell7 = Z.Map[num8][num7];
						if (cell7.HasStairs() && cell7.GetObjectsWithTagOrProperty("NoOverlap").Count > 1)
						{
							List<GameObject> objects = cell7.GetObjects((GameObject o) => o.HasTagOrProperty("NoOverlap"));
							objects.RemoveAt(0);
							using (List<GameObject>.Enumerator enumerator = objects.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									enumerator.Current.SystemMoveTo(cell7.getClosestPassableCell((Cell c) => !c.HasObjectWithTagOrProperty("NoOverlap")), null, false, true, false, false);
								}
							}
						}
					}
				}
			}
			catch (Exception ex7)
			{
				MetricsManager.LogException("SanityCheck::No falling objects on pulldowns", ex7, "game_exception");
			}
		}

		// Token: 0x060069DD RID: 27101 RVA: 0x002162F8 File Offset: 0x002144F8
		public void PaintWalls()
		{
			ZoneManager.PaintWalls(this.ActiveZone, 0, 0, -1, -1);
		}

		// Token: 0x060069DE RID: 27102 RVA: 0x0021630C File Offset: 0x0021450C
		public static void ShatterCrystals(Zone Z)
		{
			if (!Z.Built)
			{
				return;
			}
			int i = 0;
			int num = Z.Height - 2;
			while (i < num)
			{
				int j = 0;
				int num2 = Z.Width - 3;
				while (j < num2)
				{
					int num3 = j / 3 % 2;
					bool flag = false;
					bool flag2 = true;
					GameObject gameObject = null;
					for (int k = 0; k < 3; k++)
					{
						for (int l = 0; l < 2; l++)
						{
							Cell cell = Z.GetCell(j + k, i + l + num3);
							if (cell != null && !cell.HasObject("CrystalWall"))
							{
								flag2 = false;
							}
							else
							{
								flag = true;
								Cell cell2 = Z.GetCell(j + k, i + l + num3);
								gameObject = ((cell2 != null) ? cell2.GetObjects("CrystalWall").FirstOrDefault<GameObject>() : null);
							}
						}
					}
					int num4 = 2;
					if (flag && !flag2)
					{
						for (int m = 0; m < 3; m++)
						{
							for (int n = 0; n < 2; n++)
							{
								GameObject gameObject2 = Z.GetCell(j + m, i + n + num3).GetObjects("CrystalWall").FirstOrDefault<GameObject>();
								GameObject gameObject3 = null;
								if (Stat.Random(1, 100) <= num4)
								{
									gameObject3 = Z.GetCell(j + m, i + n + num3).AddObject("SmallHexCrystal", null, null, null, null);
								}
								if (gameObject2 != null && gameObject3 != null)
								{
									gameObject3.Render.ColorString = gameObject2.Render.ColorString;
									Render render = gameObject3.Render;
									string[] array = gameObject2.Render.ColorString.Split('&', StringSplitOptions.None);
									render.DetailColor = ((array != null) ? array[1] : null);
								}
								else if (gameObject != null && gameObject3 != null)
								{
									gameObject3.Render.ColorString = gameObject.Render.ColorString;
									Render render2 = gameObject3.Render;
									string[] array2 = gameObject.Render.ColorString.Split('&', StringSplitOptions.None);
									render2.DetailColor = ((array2 != null) ? array2[1] : null);
								}
								if (gameObject2 != null)
								{
									Cell currentCell = gameObject2.CurrentCell;
									if (currentCell != null)
									{
										currentCell.RemoveObject(gameObject2, true, true, false, false, false, false, true, null, null, null, null, null, null, null);
									}
								}
								if (gameObject2 != null)
								{
									gameObject2.Obliterate(null, false, null);
								}
							}
						}
					}
					j += 3;
				}
				i += 2;
			}
		}

		// Token: 0x060069DF RID: 27103 RVA: 0x0021653C File Offset: 0x0021473C
		public static void PaintCrystalWalls(Zone Z, int x1 = 0, int y1 = 0, int x2 = -1, int y2 = -1)
		{
			x1 = 0;
			y1 = 0;
			x2 = 80;
			y2 = 25;
			ZoneManager.ShatterCrystals(Z);
			Array.Clear(ZoneManager.WallSingleTrack, 0, ZoneManager.WallSingleTrack.Length);
			Array.Clear(ZoneManager.WallMultiTrack, 0, ZoneManager.WallMultiTrack.Length);
			for (int i = y1 - 1; i <= y2 + 1; i++)
			{
				for (int j = x1 - 1; j <= x2 + 1; j++)
				{
					if (j >= 0 && i >= 0 && j <= Z.Width - 1 && i <= Z.Height - 1)
					{
						Cell cell = Z.GetCell(j, i);
						if (cell != null)
						{
							if (!cell.HasObjectWithPropertyOrTag("PaintedCrystal"))
							{
								ZoneManager.WallSingleTrack[j, i] = null;
								ZoneManager.WallMultiTrack[j, i] = null;
							}
							else
							{
								GameObject firstObjectWithPropertyOrTag = cell.GetFirstObjectWithPropertyOrTag("PaintedCrystal");
								ZoneManager.WallSingleTrack[j, i] = firstObjectWithPropertyOrTag;
								ZoneManager.WallMultiTrack[j, i] = null;
							}
						}
					}
				}
			}
			for (int k = y1 - 1; k <= y2 + 1; k++)
			{
				for (int l = x1 - 1; l <= x2 + 1; l++)
				{
					if (l >= 0 && k >= 0 && l <= Z.Width - 1 && k <= Z.Height - 1 && ZoneManager.WallSingleTrack[l, k] != null)
					{
						Z.GetCell(l, k);
						ZoneManager.PaintCrystalWall(ZoneManager.WallSingleTrack[l, k], l, k, ZoneManager.WallSingleTrack);
					}
				}
			}
		}

		// Token: 0x060069E0 RID: 27104 RVA: 0x002166B8 File Offset: 0x002148B8
		private static void PaintCrystalWall(GameObject obj, int x, int y, GameObject[,] SingleTrack)
		{
			ZoneManager.<>c__DisplayClass254_0 CS$<>8__locals1;
			CS$<>8__locals1.SingleTrack = SingleTrack;
			int num = x % 3;
			int num2 = x / 3 % 2;
			int num3 = (y + num2) % 2;
			bool flag = ZoneManager.<PaintCrystalWall>g__check|254_0(x - 1, y - 1, ref CS$<>8__locals1);
			bool flag2 = ZoneManager.<PaintCrystalWall>g__check|254_0(x, y - 1, ref CS$<>8__locals1);
			bool flag3 = ZoneManager.<PaintCrystalWall>g__check|254_0(x + 1, y, ref CS$<>8__locals1);
			ZoneManager.hb.Length = 0;
			if (num == 0 && num3 == 0)
			{
				ZoneManager.hb.Append("Assets_Content_Textures_Tiles_cryshex_nw_");
				flag = ZoneManager.<PaintCrystalWall>g__check|254_0(x - 1, y + 1, ref CS$<>8__locals1);
				flag2 = ZoneManager.<PaintCrystalWall>g__check|254_0(x - 1, y, ref CS$<>8__locals1);
				flag3 = ZoneManager.<PaintCrystalWall>g__check|254_0(x, y - 1, ref CS$<>8__locals1);
			}
			if (num == 1 && num3 == 0)
			{
				ZoneManager.hb.Append("Assets_Content_Textures_Tiles_cryshex_n_");
				flag = ZoneManager.<PaintCrystalWall>g__check|254_0(x - 2, y - 1, ref CS$<>8__locals1);
				flag2 = ZoneManager.<PaintCrystalWall>g__check|254_0(x, y - 1, ref CS$<>8__locals1);
				flag3 = ZoneManager.<PaintCrystalWall>g__check|254_0(x + 2, y - 1, ref CS$<>8__locals1);
			}
			if (num == 2 && num3 == 0)
			{
				ZoneManager.hb.Append("Assets_Content_Textures_Tiles_cryshex_ne_");
				flag = ZoneManager.<PaintCrystalWall>g__check|254_0(x, y - 1, ref CS$<>8__locals1);
				flag2 = ZoneManager.<PaintCrystalWall>g__check|254_0(x + 1, y, ref CS$<>8__locals1);
				flag3 = ZoneManager.<PaintCrystalWall>g__check|254_0(x + 1, y + 1, ref CS$<>8__locals1);
			}
			if (num == 0 && num3 == 1)
			{
				ZoneManager.hb.Append("Assets_Content_Textures_Tiles_cryshex_sw_");
				flag = ZoneManager.<PaintCrystalWall>g__check|254_0(x, y + 1, ref CS$<>8__locals1);
				flag2 = ZoneManager.<PaintCrystalWall>g__check|254_0(x - 1, y, ref CS$<>8__locals1);
				flag3 = ZoneManager.<PaintCrystalWall>g__check|254_0(x - 1, y - 1, ref CS$<>8__locals1);
			}
			if (num == 1 && num3 == 1)
			{
				ZoneManager.hb.Append("Assets_Content_Textures_Tiles_cryshex_s_");
				flag = ZoneManager.<PaintCrystalWall>g__check|254_0(x + 2, y + 1, ref CS$<>8__locals1);
				flag2 = ZoneManager.<PaintCrystalWall>g__check|254_0(x, y + 1, ref CS$<>8__locals1);
				flag3 = ZoneManager.<PaintCrystalWall>g__check|254_0(x - 2, y + 1, ref CS$<>8__locals1);
			}
			if (num == 2 && num3 == 1)
			{
				ZoneManager.hb.Append("Assets_Content_Textures_Tiles_cryshex_se_");
				flag = ZoneManager.<PaintCrystalWall>g__check|254_0(x + 1, y - 1, ref CS$<>8__locals1);
				flag2 = ZoneManager.<PaintCrystalWall>g__check|254_0(x + 1, y, ref CS$<>8__locals1);
				flag3 = ZoneManager.<PaintCrystalWall>g__check|254_0(x, y + 1, ref CS$<>8__locals1);
			}
			if (!flag2)
			{
				ZoneManager.hb.Append("0.png");
			}
			else if (flag2 && !flag3 && !flag)
			{
				ZoneManager.hb.Append("1.png");
			}
			else if (flag2 && flag && !flag3)
			{
				ZoneManager.hb.Append("2.png");
			}
			else if (flag2 && flag3 && !flag)
			{
				ZoneManager.hb.Append("3.png");
			}
			else
			{
				ZoneManager.hb.Length = 0;
				ZoneManager.hb.Append("Assets_Content_Textures_Tiles_cryshex_full.png");
			}
			obj.Render.Tile = ZoneManager.hb.ToString();
		}

		// Token: 0x060069E1 RID: 27105 RVA: 0x00216928 File Offset: 0x00214B28
		public static void PaintWalls(Zone Z, int x1 = 0, int y1 = 0, int x2 = -1, int y2 = -1)
		{
			if (Z == null)
			{
				return;
			}
			if (x2 == -1)
			{
				x2 = Z.Width - 1;
			}
			if (y2 == -1)
			{
				y2 = Z.Height - 1;
			}
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (x2 > Z.Width - 1)
			{
				x2 = Z.Width - 1;
			}
			if (y2 > Z.Height - 1)
			{
				y2 = Z.Height - 1;
			}
			ZoneManager.PaintCrystalWalls(Z, x1, y1, x2, y2);
			Array.Clear(ZoneManager.WallSingleTrack, 0, ZoneManager.WallSingleTrack.Length);
			Array.Clear(ZoneManager.WallMultiTrack, 0, ZoneManager.WallMultiTrack.Length);
			string text = null;
			List<string> list = null;
			for (int i = y1 - 1; i <= y2 + 1; i++)
			{
				for (int j = x1 - 1; j <= x2 + 1; j++)
				{
					if (j >= 0 && i >= 0 && j <= Z.Width - 1 && i <= Z.Height - 1)
					{
						Cell cell = Z.GetCell(j, i);
						if (cell != null)
						{
							int objectCountWithTagsOrProperties = cell.GetObjectCountWithTagsOrProperties("PaintedWall", "PaintedFence", "PaintWith", new Predicate<GameObject>(CheckPaintabilityEvent.Check));
							if (objectCountWithTagsOrProperties == 0)
							{
								ZoneManager.WallSingleTrack[j, i] = null;
								ZoneManager.WallMultiTrack[j, i] = null;
							}
							else
							{
								if (objectCountWithTagsOrProperties > 1)
								{
									ZoneManager.WallSingleTrack[j, i] = null;
									List<GameObject> list2 = new List<GameObject>(objectCountWithTagsOrProperties);
									cell.GetObjectsWithTagsOrProperties("PaintedWall", "PaintedFence", "PaintWith", new Predicate<GameObject>(CheckPaintabilityEvent.Check), list2);
									ZoneManager.WallMultiTrack[j, i] = list2;
									using (List<GameObject>.Enumerator enumerator = list2.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											GameObject gameObject = enumerator.Current;
											string propertyOrTag = gameObject.GetPropertyOrTag("PaintPart", null);
											if (propertyOrTag != null && propertyOrTag != text)
											{
												if (list == null)
												{
													list = new List<string>(2) { text, propertyOrTag };
												}
												else if (!list.Contains(propertyOrTag))
												{
													list.Add(propertyOrTag);
												}
											}
										}
										goto IL_028B;
									}
								}
								GameObject firstObjectWithTagsOrProperties = cell.GetFirstObjectWithTagsOrProperties("PaintedWall", "PaintedFence", "PaintWith", new Predicate<GameObject>(CheckPaintabilityEvent.Check));
								ZoneManager.WallSingleTrack[j, i] = firstObjectWithTagsOrProperties;
								ZoneManager.WallMultiTrack[j, i] = null;
								string propertyOrTag2 = firstObjectWithTagsOrProperties.GetPropertyOrTag("PaintPart", null);
								if (propertyOrTag2 != null && propertyOrTag2 != text)
								{
									if (list == null)
									{
										list = new List<string>(2) { text, propertyOrTag2 };
									}
									else if (!list.Contains(propertyOrTag2))
									{
										list.Add(propertyOrTag2);
									}
								}
							}
						}
					}
					IL_028B:;
				}
			}
			if (list != null)
			{
				for (int k = y1 - 1; k <= y2 + 1; k++)
				{
					for (int l = x1 - 1; l <= x2 + 1; l++)
					{
						if (l >= 0 && k >= 0 && l <= Z.Width - 1 && k <= Z.Height - 1)
						{
							Cell cell2 = Z.GetCell(l, k);
							if (cell2 != null)
							{
								int objectCountWithPart = cell2.GetObjectCountWithPart(list);
								if (objectCountWithPart > 1)
								{
									List<GameObject> list3 = new List<GameObject>(objectCountWithPart);
									cell2.GetObjectsWithPart(text, list3);
									if (ZoneManager.WallMultiTrack[l, k] != null)
									{
										ZoneManager.WallMultiTrack[l, k].AddRange(list3);
									}
									else if (ZoneManager.WallSingleTrack[l, k] != null)
									{
										ZoneManager.WallMultiTrack[l, k] = new List<GameObject>(objectCountWithPart + 1) { ZoneManager.WallSingleTrack[l, k] };
										ZoneManager.WallMultiTrack[l, k].AddRange(list3);
										ZoneManager.WallSingleTrack[l, k] = null;
									}
									else
									{
										ZoneManager.WallMultiTrack[l, k] = list3;
									}
								}
								else if (objectCountWithPart == 1)
								{
									GameObject firstObjectWithPart = cell2.GetFirstObjectWithPart(list);
									if (ZoneManager.WallMultiTrack[l, k] != null)
									{
										ZoneManager.WallMultiTrack[l, k].Add(firstObjectWithPart);
									}
									else if (ZoneManager.WallSingleTrack[l, k] != null)
									{
										ZoneManager.WallMultiTrack[l, k] = new List<GameObject>(2)
										{
											ZoneManager.WallSingleTrack[l, k],
											firstObjectWithPart
										};
										ZoneManager.WallSingleTrack[l, k] = null;
									}
									else
									{
										ZoneManager.WallSingleTrack[l, k] = firstObjectWithPart;
									}
								}
							}
						}
					}
				}
			}
			else if (text != null)
			{
				for (int m = y1 - 1; m <= y2 + 1; m++)
				{
					for (int n = x1 - 1; n <= x2 + 1; n++)
					{
						if (n >= 0 && m >= 0 && n <= Z.Width - 1 && m <= Z.Height - 1)
						{
							Cell cell3 = Z.GetCell(n, m);
							if (cell3 != null)
							{
								int objectCountWithPart2 = cell3.GetObjectCountWithPart(text);
								if (objectCountWithPart2 > 1)
								{
									List<GameObject> list4 = new List<GameObject>(objectCountWithPart2);
									cell3.GetObjectsWithPart(text, list4);
									if (ZoneManager.WallMultiTrack[n, m] != null)
									{
										ZoneManager.WallMultiTrack[n, m].AddRange(list4);
									}
									else if (ZoneManager.WallSingleTrack[n, m] != null)
									{
										ZoneManager.WallMultiTrack[n, m] = new List<GameObject>(objectCountWithPart2 + 1) { ZoneManager.WallSingleTrack[n, m] };
										ZoneManager.WallMultiTrack[n, m].AddRange(list4);
										ZoneManager.WallSingleTrack[n, m] = null;
									}
									else
									{
										ZoneManager.WallMultiTrack[n, m] = list4;
									}
								}
								else if (objectCountWithPart2 == 1)
								{
									GameObject firstObjectWithPart2 = cell3.GetFirstObjectWithPart(text);
									if (ZoneManager.WallMultiTrack[n, m] != null)
									{
										ZoneManager.WallMultiTrack[n, m].Add(firstObjectWithPart2);
									}
									else if (ZoneManager.WallSingleTrack[n, m] != null)
									{
										ZoneManager.WallMultiTrack[n, m] = new List<GameObject>(2)
										{
											ZoneManager.WallSingleTrack[n, m],
											firstObjectWithPart2
										};
										ZoneManager.WallSingleTrack[n, m] = null;
									}
									else
									{
										ZoneManager.WallSingleTrack[n, m] = firstObjectWithPart2;
									}
								}
							}
						}
					}
				}
			}
			Event.PinCurrentPool();
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
			StringBuilder stringBuilder3 = Event.NewStringBuilder(null);
			for (int num = y1; num <= y2; num++)
			{
				for (int num2 = x1; num2 <= x2; num2++)
				{
					GameObject gameObject2 = ZoneManager.WallSingleTrack[num2, num];
					if (gameObject2 != null)
					{
						ZoneManager.PaintWall(gameObject2, num2, num, stringBuilder, stringBuilder2, stringBuilder3, ZoneManager.WallSingleTrack, ZoneManager.WallMultiTrack);
					}
					List<GameObject> list5 = ZoneManager.WallMultiTrack[num2, num];
					if (list5 != null)
					{
						foreach (GameObject gameObject3 in list5)
						{
							ZoneManager.PaintWall(gameObject3, num2, num, stringBuilder, stringBuilder2, stringBuilder3, ZoneManager.WallSingleTrack, ZoneManager.WallMultiTrack);
						}
					}
				}
			}
			Event.ResetToPin();
		}

		// Token: 0x060069E2 RID: 27106 RVA: 0x00217090 File Offset: 0x00215290
		private static void PaintWall(GameObject obj, int x, int y, StringBuilder f, StringBuilder s, StringBuilder b, GameObject[,] SingleTrack, List<GameObject>[,] MultiTrack)
		{
			string tagOrStringProperty = obj.GetTagOrStringProperty("PaintedWall", null);
			string tagOrStringProperty2 = obj.GetTagOrStringProperty("PaintedFence", null);
			if (tagOrStringProperty.IsNullOrEmpty() && tagOrStringProperty2.IsNullOrEmpty())
			{
				return;
			}
			if (!CheckTileChangeEvent.Check(obj))
			{
				return;
			}
			string tagOrStringProperty3 = obj.GetTagOrStringProperty("PaintPart", null);
			string tagOrStringProperty4 = obj.GetTagOrStringProperty("PaintWith", null);
			s.Length = 0;
			s.Append('-');
			s.Append(ZoneManager.HasWallInDirection(obj, x, y, "N", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false) ? "1" : "0");
			s.Append(ZoneManager.HasWallInDirection(obj, x, y, "NE", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false) ? "1" : "0");
			s.Append(ZoneManager.HasWallInDirection(obj, x, y, "E", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false) ? "1" : "0");
			s.Append(ZoneManager.HasWallInDirection(obj, x, y, "SE", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false) ? "1" : "0");
			s.Append(ZoneManager.HasWallInDirection(obj, x, y, "S", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false) ? "1" : "0");
			s.Append(ZoneManager.HasWallInDirection(obj, x, y, "SW", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false) ? "1" : "0");
			s.Append(ZoneManager.HasWallInDirection(obj, x, y, "W", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false) ? "1" : "0");
			s.Append(ZoneManager.HasWallInDirection(obj, x, y, "NW", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false) ? "1" : "0");
			f.Length = 0;
			f.Append('_');
			if (ZoneManager.HasWallInDirection(obj, x, y, "N", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false))
			{
				f.Append('n');
			}
			if (ZoneManager.HasWallInDirection(obj, x, y, "S", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false))
			{
				f.Append('s');
			}
			if (ZoneManager.HasWallInDirection(obj, x, y, "E", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false))
			{
				f.Append('e');
			}
			if (ZoneManager.HasWallInDirection(obj, x, y, "W", SingleTrack, MultiTrack, tagOrStringProperty3, tagOrStringProperty4, false))
			{
				f.Append('w');
			}
			bool flag = false;
			if (!tagOrStringProperty.IsNullOrEmpty())
			{
				string text = obj.GetTagOrStringProperty("paintedWallSubstring", null);
				if (text == null)
				{
					text = tagOrStringProperty.GetRandomSubstring(',', false, Stat.Rnd2);
				}
				obj.SetStringProperty("paintedWallSubstring", text, false);
				string text2 = null;
				if (obj.HasTag("PaintedCheckerboard"))
				{
					text2 = (((x + y) % 2 == 0) ? "1" : "2");
				}
				b.Length = 0;
				b.Append(obj.GetTagOrStringProperty("PaintedWallAtlas", "Assets_Content_Textures_Tiles_")).Append(text).Append(text2)
					.Append(s)
					.Append(obj.GetTagOrStringProperty("PaintedWallExtension", ".bmp"));
				obj.Render.Tile = b.ToString();
				flag = true;
			}
			if (!tagOrStringProperty2.IsNullOrEmpty())
			{
				string text3 = obj.GetTagOrStringProperty("paintedFenceSubstring", tagOrStringProperty2.GetRandomSubstring(',', false, Stat.Rnd2));
				if (text3 == null)
				{
					text3 = tagOrStringProperty2.GetRandomSubstring(',', false, Stat.Rnd2);
				}
				obj.SetStringProperty("paintedFenceSubstring", text3, false);
				b.Length = 0;
				b.Append(obj.GetTagOrStringProperty("PaintedFenceAtlas", "Assets_Content_Textures_Tiles_")).Append(text3).Append(f)
					.Append(obj.GetTagOrStringProperty("PaintedFenceExtension", ".bmp"));
				obj.Render.Tile = b.ToString();
				flag = true;
			}
			if (flag)
			{
				RepaintedEvent.Send(obj);
			}
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x00217444 File Offset: 0x00215644
		public static bool HasWallInDirection(GameObject obj, int x, int y, string D, GameObject[,] SingleTrack, List<GameObject>[,] MultiTrack, string PaintPartName, string PaintWith, bool bEdgeFlag = false)
		{
			if (PaintWith == "!PitVoid")
			{
				Cell cellFromDirection = obj.CurrentCell.GetCellFromDirection(D, true);
				int num;
				if (cellFromDirection == null)
				{
					num = 0;
				}
				else
				{
					num = (cellFromDirection.HasObject((GameObject o) => o.GetStringProperty("PaintWith", null) == "PitVoid") ? 1 : 0);
				}
				return num == 0;
			}
			bool flag = ZoneManager._HasWallInDirection(obj, x, y, D, SingleTrack, MultiTrack, PaintPartName, PaintWith, bEdgeFlag);
			if (obj.HasTag("PaintedWallInvert"))
			{
				return !flag;
			}
			return flag;
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x002174C4 File Offset: 0x002156C4
		public static bool _HasWallInDirection(GameObject obj, int x, int y, string D, GameObject[,] SingleTrack, List<GameObject>[,] MultiTrack, string PaintPartName, string PaintWith, bool bEdgeFlag = false)
		{
			if (D.Contains("N"))
			{
				y--;
			}
			else if (D.Contains("S"))
			{
				y++;
			}
			if (D.Contains("E"))
			{
				x++;
			}
			else if (D.Contains("W"))
			{
				x--;
			}
			if (y < 0)
			{
				return bEdgeFlag;
			}
			if (x < 0)
			{
				return bEdgeFlag;
			}
			if (x > SingleTrack.GetUpperBound(0))
			{
				return bEdgeFlag;
			}
			if (y > SingleTrack.GetUpperBound(1))
			{
				return bEdgeFlag;
			}
			GameObject gameObject = SingleTrack[x, y];
			if (gameObject != null)
			{
				if (obj.Blueprint == gameObject.Blueprint)
				{
					return true;
				}
				if (!PaintPartName.IsNullOrEmpty() && gameObject.HasPart(PaintPartName))
				{
					return true;
				}
				string tagOrStringProperty = gameObject.GetTagOrStringProperty("PaintWith", null);
				if (tagOrStringProperty == "*")
				{
					return true;
				}
				if (!PaintWith.IsNullOrEmpty() && tagOrStringProperty == PaintWith)
				{
					return true;
				}
			}
			List<GameObject> list = MultiTrack[x, y];
			if (list != null)
			{
				foreach (GameObject gameObject2 in list)
				{
					if (obj.Blueprint == gameObject2.Blueprint)
					{
						return true;
					}
				}
				if (!PaintPartName.IsNullOrEmpty())
				{
					using (List<GameObject>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.HasPart(PaintPartName))
							{
								return true;
							}
						}
					}
				}
				if (!PaintWith.IsNullOrEmpty())
				{
					foreach (GameObject gameObject3 in list)
					{
						string tagOrStringProperty2 = gameObject3.GetTagOrStringProperty("PaintWith", null);
						if (tagOrStringProperty2 == "*")
						{
							return true;
						}
						if (!PaintWith.IsNullOrEmpty() && tagOrStringProperty2 == PaintWith)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x002176E0 File Offset: 0x002158E0
		public static void PaintWater(Zone Z, int x1 = 0, int y1 = 0, int x2 = -1, int y2 = -1)
		{
			if (Z == null)
			{
				return;
			}
			if (x2 == -1)
			{
				x2 = Z.Width - 1;
			}
			if (y2 == -1)
			{
				y2 = Z.Height - 1;
			}
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (x2 > Z.Width - 1)
			{
				x2 = Z.Width - 1;
			}
			if (y2 > Z.Height - 1)
			{
				y2 = Z.Height - 1;
			}
			Array.Clear(ZoneManager.LiquidTrack, 0, ZoneManager.LiquidTrack.Length);
			int num = 0;
			for (int i = y1 - 1; i <= y2 + 1; i++)
			{
				for (int j = x1 - 1; j <= x2 + 1; j++)
				{
					if (j >= 0 && i >= 0 && x2 <= Z.Width - 1 && y2 <= Z.Height - 1)
					{
						Cell cell = Z.GetCell(j, i);
						if (cell != null)
						{
							for (int k = 0; k < cell.Objects.Count; k++)
							{
								if (cell.Objects[k].HasTag("PaintedLiquidAtlas"))
								{
									ZoneManager.LiquidTrack[j, i] = cell.Objects[k];
									num++;
									break;
								}
							}
						}
					}
				}
			}
			if (num > 0)
			{
				int num2 = 0;
				for (int l = y1; l <= y2; l++)
				{
					for (int m = x1; m <= x2; m++)
					{
						GameObject gameObject = ZoneManager.LiquidTrack[m, l];
						if (gameObject != null && CheckTileChangeEvent.Check(gameObject))
						{
							LiquidVolume liquidVolume = gameObject.LiquidVolume;
							BaseLiquid primaryLiquid = liquidVolume.GetPrimaryLiquid();
							if (primaryLiquid != null)
							{
								int paintGroup = primaryLiquid.GetPaintGroup(liquidVolume);
								int num3 = (ZoneManager.LiquidInDirection(m, l, paintGroup, "N", ZoneManager.LiquidTrack, 1) << 7) | (ZoneManager.LiquidInDirection(m, l, paintGroup, "E", ZoneManager.LiquidTrack, 1) << 5) | (ZoneManager.LiquidInDirection(m, l, paintGroup, "S", ZoneManager.LiquidTrack, 1) << 3) | (ZoneManager.LiquidInDirection(m, l, paintGroup, "W", ZoneManager.LiquidTrack, 1) << 1);
								if (liquidVolume.IsWadingDepth())
								{
									if (num3.HasAllBits(160))
									{
										num3 |= ZoneManager.LiquidInDirection(m, l, paintGroup, "NE", ZoneManager.LiquidTrack, 1) << 6;
									}
									if (num3.HasAllBits(40))
									{
										num3 |= ZoneManager.LiquidInDirection(m, l, paintGroup, "SE", ZoneManager.LiquidTrack, 1) << 4;
									}
									if (num3.HasAllBits(10))
									{
										num3 |= ZoneManager.LiquidInDirection(m, l, paintGroup, "SW", ZoneManager.LiquidTrack, 1) << 2;
									}
									if (num3.HasAllBits(130))
									{
										num3 |= ZoneManager.LiquidInDirection(m, l, paintGroup, "NW", ZoneManager.LiquidTrack, 1);
									}
								}
								liquidVolume.Paint(num3);
								if (++num2 >= num)
								{
									return;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060069E6 RID: 27110 RVA: 0x002179B0 File Offset: 0x00215BB0
		private static int LiquidInDirection(int x, int y, int group, string D, GameObject[,] Track, int EdgeValue = 0)
		{
			if (D.Contains("N"))
			{
				y--;
			}
			else if (D.Contains("S"))
			{
				y++;
			}
			if (D.Contains("E"))
			{
				x++;
			}
			else if (D.Contains("W"))
			{
				x--;
			}
			if (y < 0)
			{
				return EdgeValue;
			}
			if (x < 0)
			{
				return EdgeValue;
			}
			if (x > Track.GetUpperBound(0))
			{
				return EdgeValue;
			}
			if (y > Track.GetUpperBound(1))
			{
				return EdgeValue;
			}
			GameObject gameObject = Track[x, y];
			if (gameObject == null)
			{
				return 0;
			}
			if (!gameObject.LiquidVolume.CanPaintWith(group))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060069E7 RID: 27111 RVA: 0x00217A53 File Offset: 0x00215C53
		public void ProcessGoToPartyLeader()
		{
			JoinPartyLeaderCommand.Issue();
		}

		// Token: 0x060069E8 RID: 27112 RVA: 0x00217A5C File Offset: 0x00215C5C
		public static bool ApplyBuilderToZone(string Builder, Zone NewZone)
		{
			string text = "XRL.World.ZoneBuilders." + Builder;
			Type type = ModManager.ResolveType(text, false, false, true);
			if (type == null)
			{
				MetricsManager.LogError("Unknown builder " + text + "!");
				return true;
			}
			object obj = Activator.CreateInstance(type);
			MethodInfo method = type.GetMethod("BuildZone");
			return !(method != null) || (bool)method.Invoke(obj, new object[] { NewZone });
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x00217ADC File Offset: 0x00215CDC
		private void ApplyPropertiesToZone(Dictionary<string, object> Properties, Zone Z)
		{
			if (Properties.IsNullOrEmpty<KeyValuePair<string, object>>())
			{
				return;
			}
			Dictionary<string, object> dictionary;
			if ((dictionary = this.ZoneProperties.GetValue(Z.ZoneID, null)) == null)
			{
				dictionary = (this.ZoneProperties[Z.ZoneID] = new Dictionary<string, object>());
			}
			Dictionary<string, object> dictionary2 = dictionary;
			foreach (KeyValuePair<string, object> keyValuePair in Properties)
			{
				dictionary2[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x00217B70 File Offset: 0x00215D70
		public bool WantEvent(int ID, int Cascade)
		{
			if (this.ActiveZone != null && this.ActiveZone.WantEvent(ID, Cascade))
			{
				return true;
			}
			foreach (Zone zone in this.CachedZones.Values)
			{
				if (zone != this.ActiveZone && zone.WantEvent(ID, Cascade))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x00217BF4 File Offset: 0x00215DF4
		public bool HandleEvent<T>(T E) where T : MinEvent
		{
			if (this.ActiveZone != null && !this.ActiveZone.HandleEvent(E))
			{
				return false;
			}
			foreach (Zone zone in this.CachedZones.Values)
			{
				if (zone != this.ActiveZone && !zone.HandleEvent(E))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060069ED RID: 27117 RVA: 0x00217D2A File Offset: 0x00215F2A
		[CompilerGenerated]
		internal static bool <PaintCrystalWall>g__check|254_0(int x, int y, ref ZoneManager.<>c__DisplayClass254_0 A_2)
		{
			return x < 0 || y < 0 || x >= 80 || y >= 25 || A_2.SingleTrack[x, y] != null;
		}

		// Token: 0x04003060 RID: 12384
		[NonSerialized]
		public XRLGame Game;

		// Token: 0x04003061 RID: 12385
		[NonSerialized]
		public Dictionary<string, Zone> CachedZones = new Dictionary<string, Zone>();

		// Token: 0x04003062 RID: 12386
		[NonSerialized]
		public Zone ActiveZone;

		// Token: 0x04003063 RID: 12387
		[NonSerialized]
		public Graveyard Graveyard = new Graveyard(256);

		// Token: 0x04003064 RID: 12388
		[NonSerialized]
		public long LastZoneTransition;

		// Token: 0x04003065 RID: 12389
		[NonSerialized]
		private Dictionary<string, GameObject> TerrainSamples;

		// Token: 0x04003066 RID: 12390
		[NonSerialized]
		private Dictionary<string, List<ZoneConnection>> ZoneConnections = new Dictionary<string, List<ZoneConnection>>();

		// Token: 0x04003067 RID: 12391
		[NonSerialized]
		public Dictionary<string, GameObject> CachedObjects = new Dictionary<string, GameObject>();

		// Token: 0x04003068 RID: 12392
		[NonSerialized]
		public List<string> CachedObjectsToRemoveAfterZoneBuild = new List<string>();

		// Token: 0x04003069 RID: 12393
		[GameBasedStaticCache(true, false)]
		private static HashSet<string> FrozenZones = new HashSet<string>();

		// Token: 0x0400306A RID: 12394
		[GameBasedStaticCache(true, false)]
		private static List<string> FreezingZones = new List<string>();

		// Token: 0x0400306B RID: 12395
		[NonSerialized]
		public Dictionary<string, Dictionary<string, object>> ZoneProperties = new Dictionary<string, Dictionary<string, object>>();

		// Token: 0x0400306C RID: 12396
		[NonSerialized]
		public Dictionary<string, ZoneBuilderCollection> ZoneBuilders = new Dictionary<string, ZoneBuilderCollection>();

		// Token: 0x0400306D RID: 12397
		[NonSerialized]
		public Dictionary<string, ZonePartCollection> ZoneParts = new Dictionary<string, ZonePartCollection>();

		// Token: 0x0400306E RID: 12398
		public static long Ticker = 0L;

		// Token: 0x0400306F RID: 12399
		[NonSerialized]
		private static List<Zone> FreezableZones = new List<Zone>();

		// Token: 0x04003070 RID: 12400
		[NonSerialized]
		private static bool CheckingCache;

		// Token: 0x04003071 RID: 12401
		[NonSerialized]
		public Dictionary<string, long> VisitedTime = new Dictionary<string, long>();

		// Token: 0x04003072 RID: 12402
		public List<string> PinnedZones = new List<string>();

		// Token: 0x04003073 RID: 12403
		[GameBasedStaticCache(true, false)]
		[NonSerialized]
		public static int ZoneTransitionCount = 0;

		// Token: 0x04003074 RID: 12404
		[NonSerialized]
		private static StringBuilder SB = new StringBuilder();

		// Token: 0x04003075 RID: 12405
		[NonSerialized]
		public int NameUpdateTick;

		// Token: 0x04003076 RID: 12406
		[NonSerialized]
		public static Zone ZoneGenerationContext = null;

		// Token: 0x04003077 RID: 12407
		[NonSerialized]
		public static int zoneGenerationContextTier = 1;

		// Token: 0x04003078 RID: 12408
		[NonSerialized]
		public static string zoneGenerationContextZoneID = "none";

		// Token: 0x04003079 RID: 12409
		[NonSerialized]
		private static Stack<ZonePartCollection> PartCollections = new Stack<ZonePartCollection>();

		// Token: 0x0400307A RID: 12410
		[NonSerialized]
		private static Stack<ZoneBuilderCollection> BuilderCollections = new Stack<ZoneBuilderCollection>();

		// Token: 0x0400307B RID: 12411
		[NonSerialized]
		private static HashSet<string> ProcessingZones = new HashSet<string>();

		// Token: 0x0400307C RID: 12412
		[NonSerialized]
		private static GameObject[,] WallSingleTrack = new GameObject[80, 25];

		// Token: 0x0400307D RID: 12413
		[NonSerialized]
		private static List<GameObject>[,] WallMultiTrack = new List<GameObject>[80, 25];

		// Token: 0x0400307E RID: 12414
		private static StringBuilder hb = new StringBuilder();

		// Token: 0x0400307F RID: 12415
		[NonSerialized]
		private static GameObject[,] LiquidTrack = new GameObject[80, 25];

		// Token: 0x02001AA4 RID: 6820
		private class xyPair
		{
			// Token: 0x0600E88E RID: 59534 RVA: 0x00538DEB File Offset: 0x00536FEB
			public xyPair(int _x, int _y, GameObject _GO)
			{
				this.x = _x;
				this.y = _y;
				this.GO = _GO;
			}

			// Token: 0x04006A1E RID: 27166
			public int x;

			// Token: 0x04006A1F RID: 27167
			public int y;

			// Token: 0x04006A20 RID: 27168
			public GameObject GO;
		}
	}
}
