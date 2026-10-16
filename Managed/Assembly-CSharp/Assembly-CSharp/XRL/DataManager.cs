using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using LaundryBear;
using Mono.Data.Sqlite;
using Platform.IO;
using UnityEngine;
using XRL.Core;

namespace XRL
{
	// Token: 0x020004D7 RID: 1239
	[HasModSensitiveStaticCache]
	[HasGameBasedStaticCache]
	public static class DataManager
	{
		// Token: 0x0600350E RID: 13582 RVA: 0x00100357 File Offset: 0x000FE557
		[GameBasedCacheInit]
		public static void ResetCacheConnection()
		{
			if (DataManager.Connection == null)
			{
				return;
			}
			DataManager.Connection.Close();
			DataManager.Connection.Dispose();
			DataManager.Connection = null;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x0010037B File Offset: 0x000FE57B
		public static void CloseCacheConnection()
		{
			if (DataManager.Connection == null)
			{
				return;
			}
			DataManager.Connection.Close();
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x00100390 File Offset: 0x000FE590
		public static bool HasMigration(string Migration)
		{
			bool hasRows;
			using (SqliteCommand sqliteCommand = DataManager.Connection.CreateCommand())
			{
				sqliteCommand.CommandText = "SELECT Migration FROM Migrations WHERE Migration = @Migration";
				sqliteCommand.Parameters.AddWithValue("Migration", Migration);
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					hasRows = sqliteDataReader.HasRows;
				}
			}
			return hasRows;
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x00100408 File Offset: 0x000FE608
		public static SqliteCommand CreateMigrationUpgradeTransactionCommand(string Migration, string CommandRaw)
		{
			SqliteCommand sqliteCommand = DataManager.Connection.CreateCommand();
			sqliteCommand.CommandText = "BEGIN; " + CommandRaw + "; INSERT INTO Migrations (Migration) VALUES (@Migration); COMMIT;";
			sqliteCommand.Parameters.AddWithValue("Migration", Migration);
			return sqliteCommand;
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x0010043C File Offset: 0x000FE63C
		public static void CheckSqlMigrations(bool initialCreate)
		{
			if (initialCreate)
			{
				using (SqliteCommand sqliteCommand = DataManager.CreateMigrationUpgradeTransactionCommand("FrozenZone_FrozenTick", "\r\n                    CREATE TABLE IF NOT EXISTS 'FrozenZone' (\r\n                        'ID' INTEGER PRIMARY KEY AUTOINCREMENT,\r\n                        'ZoneID' TEXT UNIQUE NOT NULL,\r\n                        'FrozenTick' INTEGER,\r\n                        'Data' BLOB NOT NULL\r\n                    );\r\n                    CREATE UNIQUE INDEX IF NOT EXISTS ZoneIDIndex ON FrozenZone (ZoneID);\r\n                "))
				{
					sqliteCommand.ExecuteNonQuery();
					MetricsManager.LogInfo("Creating Cache.db with FrozenZone_FrozenTick");
					return;
				}
			}
			if (!DataManager.HasMigration("FrozenZone_FrozenTick"))
			{
				using (SqliteCommand sqliteCommand2 = DataManager.CreateMigrationUpgradeTransactionCommand("FrozenZone_FrozenTick", "ALTER TABLE 'FrozenZone' ADD 'FrozenTick' INTEGER"))
				{
					sqliteCommand2.ExecuteNonQuery();
					MetricsManager.LogInfo("Upgrading Cache.db with FrozenZone_FrozenTick");
				}
			}
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x001004D0 File Offset: 0x000FE6D0
		public static SqliteConnection RequireCacheConnection()
		{
			if (DataManager.Connection == null)
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					throw new NullReferenceException("Game is not initialized.");
				}
				string cacheDirectory = game.GetCacheDirectory("Cache.db");
				bool flag = !Platform.IO.File.Exists(cacheDirectory);
				DataManager.Connection = new SqliteConnection("Data Source=" + cacheDirectory);
				DataManager.Connection.Open();
				using (SqliteCommand sqliteCommand = DataManager.Connection.CreateCommand())
				{
					sqliteCommand.CommandText = "BEGIN;CREATE TABLE IF NOT EXISTS 'Migrations' (   'ID' INTEGER PRIMARY KEY AUTOINCREMENT,   'Migration' TEXT UNIQUE NOT NULL );CREATE UNIQUE INDEX IF NOT EXISTS MigrationIndex ON Migrations (Migration); COMMIT;";
					sqliteCommand.ExecuteNonQuery();
				}
				DataManager.CheckSqlMigrations(flag);
			}
			else if (DataManager.Connection.State == ConnectionState.Closed)
			{
				DataManager.Connection.Open();
			}
			return DataManager.Connection;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x00100588 File Offset: 0x000FE788
		public static DataManager.CacheOperation StartCacheOperation(bool Exclusive = false)
		{
			if (Exclusive)
			{
				return new DataManager.CacheOperation(DataManager.ConnectionLock.TakeWriteLock().Normalize());
			}
			return new DataManager.CacheOperation(DataManager.ConnectionLock.TakeReadLock().Normalize());
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x001005C8 File Offset: 0x000FE7C8
		public static async Task<DataManager.CacheOperation> StartCacheOperationAsync(bool Exclusive = false)
		{
			DataManager.CacheOperation cacheOperation;
			if (Exclusive)
			{
				TaskAwaiter<Latcher.WriteLock> taskAwaiter = DataManager.ConnectionLock.TakeWriteLockAsync(default(CancellationToken)).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<Latcher.WriteLock> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<Latcher.WriteLock>);
				}
				cacheOperation = new DataManager.CacheOperation(taskAwaiter.GetResult().Normalize());
			}
			else
			{
				TaskAwaiter<Latcher.ReadLock> taskAwaiter3 = DataManager.ConnectionLock.TakeReadLockAsync(default(CancellationToken)).GetAwaiter();
				if (!taskAwaiter3.IsCompleted)
				{
					await taskAwaiter3;
					TaskAwaiter<Latcher.ReadLock> taskAwaiter4;
					taskAwaiter3 = taskAwaiter4;
					taskAwaiter4 = default(TaskAwaiter<Latcher.ReadLock>);
				}
				cacheOperation = new DataManager.CacheOperation(taskAwaiter3.GetResult().Normalize());
			}
			return cacheOperation;
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x0010060C File Offset: 0x000FE80C
		public static DataManager.CacheTransaction StartCacheTransaction(bool Exclusive = false)
		{
			if (Exclusive)
			{
				return new DataManager.CacheTransaction(DataManager.ConnectionLock.TakeWriteLock().Normalize());
			}
			return new DataManager.CacheTransaction(DataManager.ConnectionLock.TakeReadLock().Normalize());
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x0010064B File Offset: 0x000FE84B
		public static void Shutdown()
		{
			DataManager.ResetCacheConnection();
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x00100654 File Offset: 0x000FE854
		public static async Task PerformFileLocationChangesAsAppropriate()
		{
			ServiceLocator.GetServiceOrThrow<PlatformService>();
			global::UnityEngine.Debug.Log("Performing File Location Copies...");
			await DataManager.<PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(DataManager.FilePath("UserPrefs.json"), DataManager.SyncedPath("UserPrefs.json"));
			global::UnityEngine.Debug.Log("Performing File Location Upgrades...");
			await DataManager.<PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(DataManager.SavePath("UserPrefs.json"), DataManager.SyncedPath("UserPrefs.json"));
			await DataManager.<PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(DataManager.SavePath("Achievements.json"), DataManager.SyncedPath("Achievements.json"));
			await DataManager.<PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(DataManager.SavePath("HighScores.json"), DataManager.SyncedPath("HighScores.json"));
			await DataManager.<PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(DataManager.SavePath("PlayerOptions.json"), DataManager.SyncedPath("PlayerOptions.json"));
			await DataManager.<PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(DataManager.SavePath("ModSettings.json"), DataManager.SyncedPath("ModSettings.json"));
			await DataManager.<PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(DataManager.SavePath("BuildLibrary.json"), DataManager.SyncedPath("BuildLibrary.json"));
			EnumerateFilesResult enumerateFilesResult = await Folder.EnumerateFilesShallowAsync(DataManager.SavePath(string.Empty));
			foreach (string text in enumerateFilesResult.files)
			{
				if (text.EndsWith("Keymap.json") || text.EndsWith("Keymap2.json"))
				{
					await DataManager.<PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(text, DataManager.SyncedPath(Platform.IO.Path.GetFileName(text)));
				}
			}
			string[] array = null;
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x00100690 File Offset: 0x000FE890
		public static void preloadContents(string path)
		{
			try
			{
				string text = Platform.IO.Path.GetFileName(path).ToLower();
				if (!DataManager.contents.ContainsKey(text))
				{
					string text2 = Platform.IO.Path.Combine(new string[]
					{
						Platform.IO.Path.Combine(new string[]
						{
							Application.streamingAssetsPath,
							"Base"
						}),
						path
					});
					WWW www = new WWW(text2);
					while (!www.isDone)
					{
						Thread.Sleep(0);
					}
					DataManager.contents.Add(text, www.text);
					MetricsManager.LogInfo("loaded " + text2 + " length " + www.text.Length.ToString());
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("error loading " + path, ex);
			}
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x0010075C File Offset: 0x000FE95C
		public static Stream GenerateStreamFromString(string s)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(s);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x00100780 File Offset: 0x000FE980
		public static XmlDataHelper GetXMLStream(string FileName, ModInfo modInfo)
		{
			string text = Platform.IO.Path.GetFileName(FileName).ToLower();
			if (DataManager.contents.ContainsKey(text))
			{
				return new XmlDataHelper(DataManager.GenerateStreamFromString(DataManager.contents[text]), modInfo);
			}
			return new XmlDataHelper(DataManager.FilePath(FileName), modInfo);
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x001007CC File Offset: 0x000FE9CC
		public static XmlTextReader GetStreamingAssetsXMLStream(string FileName)
		{
			string text = Platform.IO.Path.GetFileName(FileName).ToLower();
			if (DataManager.contents.ContainsKey(text))
			{
				return new XmlTextReader(DataManager.GenerateStreamFromString(DataManager.contents[text]));
			}
			return new XmlTextReader(DataManager.FilePath(FileName));
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x00100814 File Offset: 0x000FEA14
		public static StreamReader GetStreamingAssetsStreamReader(string FileName)
		{
			string text = Platform.IO.Path.GetFileName(FileName).ToLower();
			if (DataManager.contents.ContainsKey(text))
			{
				return new StreamReader(DataManager.GenerateStreamFromString(DataManager.contents[text]));
			}
			return new StreamReader(DataManager.FilePath(FileName));
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x0010085B File Offset: 0x000FEA5B
		public static string EmbeddedModsPath()
		{
			if (string.IsNullOrWhiteSpace(XRLCore.EmbeddedModsPath))
			{
				XRLCore.EmbeddedModsPath = Platform.IO.Path.Combine(new string[]
				{
					Application.streamingAssetsPath,
					"Mods"
				});
			}
			return XRLCore.EmbeddedModsPath;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x0010088E File Offset: 0x000FEA8E
		public static string OSXDLCPath()
		{
			if (string.IsNullOrWhiteSpace(XRLCore.OSXDLCPath))
			{
				XRLCore.OSXDLCPath = Platform.IO.Path.Combine(new string[]
				{
					Application.dataPath,
					"../../CoQ_Data/StreamingAssets/DLC"
				});
			}
			return XRLCore.OSXDLCPath;
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x001008C4 File Offset: 0x000FEAC4
		public static string OSXDLCPath(string file)
		{
			if (string.IsNullOrWhiteSpace(XRLCore.OSXDLCPath))
			{
				XRLCore.OSXDLCPath = Platform.IO.Path.Combine(new string[]
				{
					Application.dataPath,
					"../../CoQ_Data/StreamingAssets/DLC"
				});
			}
			return Platform.IO.Path.Combine(new string[]
			{
				XRLCore.OSXDLCPath,
				file
			});
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x00100914 File Offset: 0x000FEB14
		public static string DLCPath()
		{
			if (string.IsNullOrWhiteSpace(XRLCore.DLCPath))
			{
				XRLCore.DLCPath = Platform.IO.Path.Combine(new string[]
				{
					Application.streamingAssetsPath,
					"DLC"
				});
			}
			return XRLCore.DLCPath;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x00100948 File Offset: 0x000FEB48
		public static string DLCPath(string file)
		{
			if (string.IsNullOrWhiteSpace(XRLCore.DLCPath))
			{
				XRLCore.DLCPath = Platform.IO.Path.Combine(new string[]
				{
					Application.streamingAssetsPath,
					"DLC"
				});
			}
			return Platform.IO.Path.Combine(new string[]
			{
				XRLCore.DLCPath,
				file
			});
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x00100998 File Offset: 0x000FEB98
		public static string FilePath()
		{
			if (string.IsNullOrWhiteSpace(XRLCore.DataPath))
			{
				XRLCore.DataPath = Platform.IO.Path.Combine(new string[]
				{
					Application.streamingAssetsPath,
					"Base"
				});
			}
			return XRLCore.DataPath;
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x001009CC File Offset: 0x000FEBCC
		public static string FilePath(string FileName)
		{
			if (string.IsNullOrWhiteSpace(XRLCore.DataPath))
			{
				XRLCore.DataPath = Platform.IO.Path.Combine(new string[]
				{
					Application.streamingAssetsPath,
					"Base"
				});
			}
			return Platform.IO.Path.Combine(new string[]
			{
				XRLCore.DataPath,
				FileName
			});
		}

		/// <returns> A read/write-able path that can be used with System.IO </returns>
		/// <remarks> If you want to write portable I/O, prefer Platform.Path.RW, which must be used with Platform.IO or IStorage</remarks>
		// Token: 0x06003525 RID: 13605 RVA: 0x00100A1C File Offset: 0x000FEC1C
		public static string SavePath(string FileName)
		{
			return Platform.IO.Path.Combine(new string[]
			{
				XRLCore.SavePath,
				FileName
			});
		}

		/// <returns> A read/write-able path that can be used with System.IO </returns>
		/// <remarks> If you want to write portable I/O, prefer Platform.Path.RWLocal, which must be used with Platform.IO or IStorage</remarks>
		// Token: 0x06003526 RID: 13606 RVA: 0x00100A35 File Offset: 0x000FEC35
		public static string LocalPath(string FileName)
		{
			return Platform.IO.Path.Combine(new string[]
			{
				XRLCore.LocalPath,
				FileName
			});
		}

		/// <returns> A read/write-able path that is saved(/synced) to the cloud that can be used with System.IO </returns>
		/// <remarks> If you want to write portable I/O, consider Platform.Path.RWSynced, which must be used with Platform.IO or IStorage</remarks>
		// Token: 0x06003527 RID: 13607 RVA: 0x00100A4E File Offset: 0x000FEC4E
		public static string SyncedPath(string FileName)
		{
			return Platform.IO.Path.Combine(new string[]
			{
				XRLCore.SyncedPath,
				FileName
			});
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x00100A67 File Offset: 0x000FEC67
		[Conditional("DEBUGGING_IO")]
		private static void LogIfDebuggingIO(string msg)
		{
			global::UnityEngine.Debug.Log(msg);
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x00100A70 File Offset: 0x000FEC70
		public static string SanitizePathForDisplay(string Source)
		{
			string text = DataManager.SavePath("");
			if (text.IsNullOrEmpty())
			{
				return "<...>";
			}
			string text2 = Source.Replace(text, "<...>/CavesOfQud");
			text = DataManager.SavePath("").Replace("/", "\\");
			return Regex.Replace(text2.Replace(text, "<...>"), "^(.+)[/\\\\]steamapps[/\\\\]", "<...>/steamapps/").Replace('\\', '/');
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x00100ADF File Offset: 0x000FECDF
		[ModSensitiveCacheInit]
		private static void ClearXMLCache()
		{
			if (DataManager.XMLCacheInitialized && GameManager.AwakeComplete)
			{
				DataManager.XMLFilesByRoot.Clear();
				DataManager.XMLCacheInitialized = false;
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x00100B00 File Offset: 0x000FED00
		private static void CheckXMLInitialize()
		{
			if (DataManager.XMLCacheInitialized)
			{
				return;
			}
			if (DataManager.XMLFilesByRoot.IsNullOrEmpty<KeyValuePair<string, List<DataFile>>>())
			{
				global::System.IO.FileInfo[] files = new global::System.IO.DirectoryInfo(DataManager.FilePath("")).GetFiles("*.xml", global::System.IO.SearchOption.AllDirectories);
				for (int i = 0; i < files.Length; i++)
				{
					DataManager.CacheFile(files[i].FullName, null);
				}
				DataManager.SortXMLRoots();
			}
			if (ModManager.Initialized)
			{
				foreach (ModInfo modInfo in ModManager.ActiveMods)
				{
					foreach (ModFile modFile in modInfo.Files)
					{
						if (modFile.Type == ModFileType.XML)
						{
							DataManager.CacheFile(modFile.OriginalName, modInfo);
						}
					}
				}
				DataManager.SortXMLRoots();
				DataManager.PrintXMLRoots();
				DataManager.XMLCacheInitialized = true;
			}
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x00100C0C File Offset: 0x000FEE0C
		private static void SortXMLRoots()
		{
			foreach (KeyValuePair<string, List<DataFile>> keyValuePair in DataManager.XMLFilesByRoot)
			{
				keyValuePair.Value.Sort();
			}
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x00100C64 File Offset: 0x000FEE64
		private static void PrintXMLRoots()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("Found {0} XML roots;", DataManager.XMLFilesByRoot.Count));
			foreach (KeyValuePair<string, List<DataFile>> keyValuePair in DataManager.XMLFilesByRoot)
			{
				string text;
				List<DataFile> list;
				keyValuePair.Deconstruct(out text, out list);
				string text2 = text;
				List<DataFile> list2 = list;
				int num = 0;
				int num2 = 0;
				using (List<DataFile>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.IsBase)
						{
							num++;
						}
						else
						{
							num2++;
						}
					}
				}
				stringBuilder.Append("\n - ").AppendUpper(text2).Append(": ");
				if (num != 0)
				{
					stringBuilder.AppendThings(num, "base file", "base files");
				}
				if (num2 != 0)
				{
					if (num != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendThings(num2, "mod file", "mod files");
				}
			}
			MetricsManager.LogInfo(stringBuilder.ToString());
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x00100DA0 File Offset: 0x000FEFA0
		private static void CacheFile(string Path, ModInfo Mod = null)
		{
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(Path))
				{
					while (xmlReader.Read())
					{
						if (!xmlReader.Name.IsNullOrEmpty() && !(xmlReader.Name == "xml"))
						{
							List<DataFile> list;
							if (!DataManager.XMLFilesByRoot.TryGetValue(xmlReader.Name, out list))
							{
								list = (DataManager.XMLFilesByRoot[xmlReader.Name] = new List<DataFile>());
							}
							int num2;
							int num = (int.TryParse(xmlReader.GetAttribute("LoadPriority"), out num2) ? num2 : 0);
							list.Add(new DataFile
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
				MetricsManager.LogPotentialModError(Mod, string.Concat(new string[]
				{
					Path,
					": ",
					ex.Message,
					"\n",
					ex.StackTrace.ToString()
				}));
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x00100EB8 File Offset: 0x000FF0B8
		public static void DeleteSaveDirectory(string Path)
		{
			if (Path.IsNullOrEmpty())
			{
				return;
			}
			using (DataManager.StartCacheOperation(true))
			{
				DataManager.ResetCacheConnection();
				int num = 0;
				for (;;)
				{
					try
					{
						Platform.IO.Directory.Delete(Path, true);
						break;
					}
					catch (Exception ex)
					{
						if (num++ >= 20)
						{
							MetricsManager.LogException("Error deleting saved game", ex, "game_exception");
							break;
						}
						Thread.Sleep(50);
					}
				}
			}
		}

		/// <summary>Get list of XML files with root element within both base and mod directories.</summary>
		// Token: 0x06003530 RID: 13616 RVA: 0x00100F38 File Offset: 0x000FF138
		public static List<DataFile> GetXMLFilesWithRoot(string Root)
		{
			DataManager.CheckXMLInitialize();
			return DataManager.XMLFilesByRoot.GetValue(Root, null) ?? new List<DataFile>();
		}

		/// <summary>Yield XmlDataHelper streams for each file in <see cref="M:XRL.DataManager.GetXMLFilesWithRoot(System.String)" />.</summary>
		/// <remarks>Stream is disposed on next enumeration.</remarks>
		// Token: 0x06003531 RID: 13617 RVA: 0x00100F54 File Offset: 0x000FF154
		public static IEnumerable<XmlDataHelper> YieldXMLStreamsWithRoot(string Root, bool IncludeBase = true, bool IncludeMods = true)
		{
			foreach (DataFile dataFile in DataManager.GetXMLFilesWithRoot(Root))
			{
				if ((IncludeBase || !dataFile.IsBase) && (IncludeMods || !dataFile.IsMod))
				{
					using (XmlDataHelper stream = DataManager.GetXMLStream(dataFile, dataFile.Mod))
					{
						yield return stream;
					}
					XmlDataHelper stream = null;
				}
			}
			List<DataFile>.Enumerator enumerator = default(List<DataFile>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x00100F74 File Offset: 0x000FF174
		public static void Open(string Path)
		{
			Path = global::System.IO.Path.GetFullPath(Path);
			try
			{
				if (Process.Start("open", "\"" + Path + "\"") != null)
				{
					return;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				if (Process.Start("xdg-open", "\"" + Path + "\"") != null)
				{
					return;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				Process.Start("explorer", "\"" + Path + "\"");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x00101044 File Offset: 0x000FF244
		[CompilerGenerated]
		internal static async Task <PerformFileLocationChangesAsAppropriate>g__copyIfSrcExistsAndDstDoesnt|15_0(string srcPath, string dstPath)
		{
			TaskAwaiter<bool> taskAwaiter = Platform.IO.File.ExistsAsync(srcPath).GetAwaiter();
			TaskAwaiter<bool> taskAwaiter2;
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			if (taskAwaiter.GetResult())
			{
				taskAwaiter = Platform.IO.File.ExistsAsync(dstPath).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (!taskAwaiter.GetResult())
				{
					PlatformIOResult platformIOResult = await Blob.CopyAsync(srcPath, dstPath, false);
					if (platformIOResult.WasSuccessful())
					{
						global::UnityEngine.Debug.Log(string.Concat(new string[] { "File Upgrade: Copying \"", srcPath, "\" to \"", dstPath, "\". Not deleting old file in case some old mods still use the original locations." }));
					}
					else
					{
						global::UnityEngine.Debug.LogWarning(platformIOResult.ToString());
					}
				}
			}
		}

		// Token: 0x04001AD5 RID: 6869
		public static Dictionary<string, string> contents = new Dictionary<string, string>();

		// Token: 0x04001AD6 RID: 6870
		private static SqliteConnection Connection;

		// Token: 0x04001AD7 RID: 6871
		private static Latcher ConnectionLock = new Latcher();

		// Token: 0x04001AD8 RID: 6872
		public const string STEAM_PATH_REGEX = "^(.+)[/\\\\]steamapps[/\\\\]";

		// Token: 0x04001AD9 RID: 6873
		private static Dictionary<string, List<DataFile>> XMLFilesByRoot = new Dictionary<string, List<DataFile>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001ADA RID: 6874
		private static bool XMLCacheInitialized;

		// Token: 0x02001919 RID: 6425
		public struct CacheOperation : IDisposable
		{
			// Token: 0x17000E50 RID: 3664
			// (get) Token: 0x0600E250 RID: 57936 RVA: 0x00521E80 File Offset: 0x00520080
			public SqliteCommand Command
			{
				get
				{
					SqliteCommand sqliteCommand;
					if ((sqliteCommand = this._Command) == null)
					{
						sqliteCommand = (this._Command = DataManager.RequireCacheConnection().CreateCommand());
					}
					return sqliteCommand;
				}
			}

			// Token: 0x0600E251 RID: 57937 RVA: 0x00521EAA File Offset: 0x005200AA
			public CacheOperation(Latcher.Lock Lock)
			{
				this._Command = null;
				this.Lock = Lock;
			}

			// Token: 0x0600E252 RID: 57938 RVA: 0x00521EBA File Offset: 0x005200BA
			public void Dispose()
			{
				if (this._Command != null)
				{
					this._Command.Dispose();
					this._Command = null;
				}
				this.Lock.Release();
			}

			// Token: 0x040064AE RID: 25774
			private SqliteCommand _Command;

			// Token: 0x040064AF RID: 25775
			private Latcher.Lock Lock;
		}

		// Token: 0x0200191A RID: 6426
		public struct CacheTransaction : IDisposable
		{
			// Token: 0x0600E253 RID: 57939 RVA: 0x00521EE1 File Offset: 0x005200E1
			public CacheTransaction(Latcher.Lock Lock)
			{
				this._Transaction = DataManager.RequireCacheConnection().BeginTransaction();
				this._Commands = new List<SqliteCommand>();
				this.Lock = Lock;
				this._Committed = false;
				this._Rolledback = false;
			}

			// Token: 0x0600E254 RID: 57940 RVA: 0x00521F14 File Offset: 0x00520114
			public SqliteCommand AddCommand()
			{
				if (this._Committed || this._Rolledback)
				{
					throw new SqliteException("Cannot add additional commands to a committed or rolled back transaction.");
				}
				SqliteCommand sqliteCommand = DataManager.Connection.CreateCommand();
				sqliteCommand.Transaction = this._Transaction;
				this._Commands.Add(sqliteCommand);
				return sqliteCommand;
			}

			// Token: 0x0600E255 RID: 57941 RVA: 0x00521F60 File Offset: 0x00520160
			public void CommitTransaction()
			{
				if (this._Committed || this._Rolledback)
				{
					throw new SqliteException("Cannot commit a transaction that has already been committed or roleld back.");
				}
				this._Transaction.Commit();
				this._Committed = true;
			}

			// Token: 0x0600E256 RID: 57942 RVA: 0x00521F8F File Offset: 0x0052018F
			public void Rollback()
			{
				if (this._Committed || this._Rolledback)
				{
					throw new SqliteException("Cannot roll back a transaction that has already been committed or roleld back.");
				}
				this._Transaction.Rollback();
				this._Rolledback = true;
			}

			// Token: 0x0600E257 RID: 57943 RVA: 0x00521FC0 File Offset: 0x005201C0
			public void Dispose()
			{
				if (!this._Committed && !this._Rolledback)
				{
					this.CommitTransaction();
				}
				this._Transaction.Dispose();
				this._Transaction = null;
				for (int i = this._Commands.Count - 1; i >= 0; i--)
				{
					global::System.ComponentModel.Component component = this._Commands[i];
					this._Commands.RemoveAt(i);
					component.Dispose();
				}
				this._Commands = null;
				this.Lock.Release();
			}

			// Token: 0x040064B0 RID: 25776
			private List<SqliteCommand> _Commands;

			// Token: 0x040064B1 RID: 25777
			private SqliteTransaction _Transaction;

			// Token: 0x040064B2 RID: 25778
			private Latcher.Lock Lock;

			// Token: 0x040064B3 RID: 25779
			private bool _Committed;

			// Token: 0x040064B4 RID: 25780
			private bool _Rolledback;
		}
	}
}
