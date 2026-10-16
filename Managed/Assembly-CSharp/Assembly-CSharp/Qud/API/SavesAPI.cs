using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Cysharp.Text;
using Platform.IO;
using Qud.UI;
using XRL;
using XRL.UI;

namespace Qud.API
{
	// Token: 0x0200040E RID: 1038
	public static class SavesAPI
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000CC576 File Offset: 0x000CA776
		public static List<string> SavedGamePaths
		{
			get
			{
				List<string> list;
				if ((list = SavesAPI._SavedGamePaths) == null)
				{
					List<string> list2 = new List<string>();
					list2.Add(DataManager.SyncedPath("Saves"));
					list2.Add(DataManager.SavePath("Saves"));
					list = list2;
					SavesAPI._SavedGamePaths = list2;
				}
				return list;
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000CC5AC File Offset: 0x000CA7AC
		private static long GetDirectorySize(string p)
		{
			IEnumerable<string> enumerable = Platform.IO.Directory.EnumerateFiles(p);
			long num = 0L;
			foreach (string text in enumerable)
			{
				Platform.IO.FileInfo fileInfo = new Platform.IO.FileInfo(text);
				num += fileInfo.Length;
			}
			return num;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000CC604 File Offset: 0x000CA804
		public static async Task<SaveGameInfo> ReadSaveJson(string dirPath, string filePath)
		{
			SaveGameJSON json = null;
			try
			{
				SaveGameJSON saveGameJSON = await Platform.IO.File.ReadAllJsonAsync<SaveGameJSON>(filePath, null);
				json = saveGameJSON;
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Loading save json " + filePath, ex);
			}
			SaveGameInfo saveGameInfo;
			if (json == null)
			{
				saveGameInfo = new SaveGameInfo
				{
					Name = "&RCorrupt info file",
					Size = "Total size: " + (SavesAPI.GetDirectorySize(dirPath) / 1000000L).ToString() + "mb",
					Info = "",
					Directory = dirPath
				};
			}
			else
			{
				SaveGameInfo saveGameInfo2 = new SaveGameInfo
				{
					json = json,
					Directory = dirPath,
					Size = "Total size: " + (SavesAPI.GetDirectorySize(dirPath) / 1000000L).ToString() + "mb",
					ID = json.ID,
					Version = json.GameVersion,
					Name = json.Name,
					Description = string.Format("Level {0} {1} [{2}]", json.Level, json.GenoSubType, json.GameMode),
					Info = string.Format("{0}, {1} turn {2}", json.Location, json.InGameTime, json.Turn),
					SaveTime = json.SaveTime,
					ModsEnabled = json.ModsEnabled
				};
				if (json.SaveVersion < 395 || json.SaveVersion > 409)
				{
					saveGameInfo2.Name = "{{R|Older Version (" + json.GameVersion + ")}} " + saveGameInfo2.Name;
				}
				saveGameInfo = saveGameInfo2;
			}
			return saveGameInfo;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000CC650 File Offset: 0x000CA850
		public static bool HasSavedGameInfo()
		{
			foreach (string text in SavesAPI.SavedGamePaths)
			{
				if (Platform.IO.Directory.Exists(text))
				{
					EnumerateDirectoriesResult enumerateDirectoriesResult = Folder.EnumerateDirectories(text);
					if (!enumerateDirectoriesResult.WasSuccessful())
					{
						return false;
					}
					foreach (string text2 in enumerateDirectoriesResult.directories)
					{
						foreach (string text3 in SavesAPI.InfoFileNames)
						{
							if (Platform.IO.File.Exists(Platform.IO.Path.Combine(new string[] { text2, text3 })))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000CC728 File Offset: 0x000CA928
		private static void FatalSaveError(Exception e, string path)
		{
			MetricsManager.LogError("Error checking for save files", e);
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				if (e is UnauthorizedAccessException || e is IOException)
				{
					utf16ValueStringBuilder.AppendLine("There was a permission error while trying to access your save directory.");
					utf16ValueStringBuilder.AppendLine();
					utf16ValueStringBuilder.AppendLine(ColorUtility.EscapeFormatting(e.Message));
				}
				else
				{
					utf16ValueStringBuilder.AppendLine("There was an error while trying to access your save directory.");
					utf16ValueStringBuilder.AppendLine();
					utf16ValueStringBuilder.Append("Directory: ");
					utf16ValueStringBuilder.AppendLine(ColorUtility.EscapeFormatting(path));
				}
				utf16ValueStringBuilder.AppendLine();
				utf16ValueStringBuilder.AppendLine("Caves of Qud will exit now since we cannot save games. Please check your directory’s permissions.");
				Popup.WaitNewPopupMessage(utf16ValueStringBuilder.ToString(), new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "Quit",
						hotkey = "Accept,Cancel",
						command = "Cancel"
					}
				}, null, null, "Error reading save location.", null, 0, null, null, null, true, true, null, null, false);
				GameManager.Instance.uiQueue.queueTask(new Action(GameManager.Instance.Quit), 0);
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000CC858 File Offset: 0x000CAA58
		public static async Task<List<SaveGameInfo>> GetSavedGameInfo()
		{
			List<SaveGameInfo> result = new List<SaveGameInfo>();
			foreach (string path in SavesAPI.SavedGamePaths)
			{
				try
				{
					if (!Platform.IO.Directory.Exists(path))
					{
						continue;
					}
					EnumerateDirectoriesResult enumerateDirectoriesResult = await Folder.EnumerateDirectoriesAsync(path);
					enumerateDirectoriesResult.LogErrorIfFailed();
					string[] array = enumerateDirectoriesResult.directories;
					for (int i = 0; i < array.Length; i++)
					{
						SaveGameInfo saveGameInfo = await SavesAPI.GetDirectoryInfo(array[i]);
						if (saveGameInfo != null)
						{
							result.Add(saveGameInfo);
						}
					}
					array = null;
				}
				catch (Exception ex)
				{
					SavesAPI.FatalSaveError(ex, path);
				}
				path = null;
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			result.Sort(new Comparison<SaveGameInfo>(SavesAPI.SortGameByDate));
			return result;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000CC894 File Offset: 0x000CAA94
		private static async Task<SaveGameInfo> GetDirectoryInfo(string dirPath)
		{
			try
			{
				if (Platform.IO.Path.GetFileNameWithoutExtension(dirPath).EqualsNoCase("mods") || Platform.IO.Path.GetFileNameWithoutExtension(dirPath).EqualsNoCase("textures"))
				{
					return null;
				}
				foreach (string text in SavesAPI.InfoFileNames)
				{
					string path = Platform.IO.Path.Combine(new string[] { dirPath, text });
					TaskAwaiter<bool> taskAwaiter = Platform.IO.File.ExistsAsync(path).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						return await SavesAPI.ReadSaveJson(dirPath, path);
					}
					path = null;
				}
				string[] array = null;
				if (!Platform.IO.Directory.EnumerateFiles(dirPath).Any<string>((string file) => !file.EndsWith("Cache.db")))
				{
					try
					{
						Platform.IO.Directory.Delete(dirPath, true);
						goto IL_01F1;
					}
					catch (Exception ex)
					{
						MetricsManager.LogWarning(ex);
						goto IL_01F1;
					}
				}
				MetricsManager.LogWarning("Weird save directory with no .json file present: " + DataManager.SanitizePathForDisplay(dirPath));
				IL_01F1:;
			}
			catch (ThreadInterruptedException ex2)
			{
				throw ex2;
			}
			catch (Exception ex3)
			{
				MetricsManager.LogWarning(ex3.ToString());
			}
			return null;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000CC8D8 File Offset: 0x000CAAD8
		private static int SortGameByDate(SaveGameInfo I1, SaveGameInfo I2)
		{
			int num;
			try
			{
				if (string.IsNullOrEmpty(I1.SaveTime) || !I1.SaveTime.Contains(" at "))
				{
					num = 1;
				}
				else if (string.IsNullOrEmpty(I2.SaveTime) || !I2.SaveTime.Contains(" at "))
				{
					num = -1;
				}
				else
				{
					string text = I1.SaveTime.Substring(0, I1.SaveTime.IndexOf(" at "));
					string text2 = I1.SaveTime.Substring(I1.SaveTime.IndexOf(" at ") + 4);
					string text3 = I2.SaveTime.Substring(0, I2.SaveTime.IndexOf(" at "));
					string text4 = I2.SaveTime.Substring(I2.SaveTime.IndexOf(" at ") + 4);
					DateTime dateTime = DateTime.Parse(text + " " + text2);
					num = DateTime.Parse(text3 + " " + text4).CompareTo(dateTime);
				}
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x0400137B RID: 4987
		private static List<string> _SavedGamePaths;

		// Token: 0x0400137C RID: 4988
		private static readonly string[] InfoFileNames = new string[] { "Primary.json", "Primary.sav.json" };
	}
}
