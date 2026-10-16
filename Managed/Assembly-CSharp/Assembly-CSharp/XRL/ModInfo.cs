using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using Cysharp.Text;
using HarmonyLib;
using Kobold;
using Platform.IO;
using Qud.UI;
using RoslynCSharp.Compiler;
using Steamworks;
using UnityEngine;
using XRL.Collections;
using XRL.Rules;
using XRL.UI;

namespace XRL
{
	// Token: 0x02000520 RID: 1312
	[HasModSensitiveStaticCache]
	public class ModInfo : IComparable<ModInfo>
	{
		/// <summary>
		/// Gets or sets a value that determines if this mod is enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if the mod has been approved, enabled, and successfully compiled; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x00111FDD File Offset: 0x001101DD
		// (set) Token: 0x0600381E RID: 14366 RVA: 0x00111FE8 File Offset: 0x001101E8
		public bool IsEnabled
		{
			get
			{
				return this.State == ModState.Enabled;
			}
			set
			{
				this.Settings.Enabled = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x00111FF6 File Offset: 0x001101F6
		[Obsolete]
		public bool IsApproved
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// An enum representation of the mod's current state: lacks approval, failed to compile, enabled, disabled.
		/// </summary>
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06003820 RID: 14368 RVA: 0x00111FF9 File Offset: 0x001101F9
		public ModState State
		{
			get
			{
				if (this.IsScripting && !Options.AllowCSMods)
				{
					return ModState.Disabled;
				}
				if (!this.Settings.Enabled)
				{
					return ModState.Disabled;
				}
				if (this.IsMissingDependency)
				{
					return ModState.MissingDependency;
				}
				if (this.Settings.Failed)
				{
					return ModState.Failed;
				}
				return ModState.Enabled;
			}
		}

		/// <summary><see cref="P:XRL.ModInfo.DisplayTitle" /> stripped of ANSI formatting.</summary>
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x00112035 File Offset: 0x00110235
		public string DisplayTitleStripped
		{
			get
			{
				return ConsoleLib.Console.ColorUtility.StripFormatting(this.DisplayTitle);
			}
		}

		/// <summary>
		/// A display title for the <see cref="T:Qud.UI.ModManagerUI" />.
		/// </summary>
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06003822 RID: 14370 RVA: 0x00112042 File Offset: 0x00110242
		public string DisplayTitle
		{
			get
			{
				return this.Manifest.Title.Coalesce(this.ID);
			}
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x0011205C File Offset: 0x0011025C
		public ModInfo(string Path, string ID = null, ModSource Source = ModSource.Unknown, bool Initialize = false)
		{
			this.Directory = new DirectoryInfo(Path);
			this.Path = this.Directory.FullName;
			this.ID = ID;
			this.Source = Source;
			if (Initialize)
			{
				this.Initialize();
			}
		}

		/// <summary>
		/// Initialize the mod info instance based on configuration and user settings.
		/// </summary>
		// Token: 0x06003824 RID: 14372 RVA: 0x001120E6 File Offset: 0x001102E6
		public void Initialize()
		{
			if (!this.Directory.Exists)
			{
				return;
			}
			this.ReadConfigurations();
			this.LoadSettings();
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x00112104 File Offset: 0x00110304
		public void ResolveDependencies()
		{
			Dictionary<string, ModInfo> modMap = ModManager.ModMap;
			if (!this.Manifest.Dependencies.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.Manifest.Dependencies)
				{
					string text;
					string text2;
					keyValuePair.Deconstruct(out text, out text2);
					string text3 = text;
					string text4 = text2;
					ModInfo modInfo;
					if (modMap.TryGetValue(text3, out modInfo) && modInfo.Manifest.Version.EqualsSemantic(text4))
					{
						global::Logger.buildLog.Info(string.Concat(new string[] { "[Required] ", this.ID, " -> ", text3, " (", text4, "): OK" }));
						uint num;
						if (!this.Dependencies.TryGetValue(modInfo, out num) || num < 10000000U)
						{
							this.Dependencies[modInfo] = 10000000U;
						}
					}
					else
					{
						global::Logger.buildLog.Info(string.Concat(new string[] { "[Required] ", this.ID, " -> ", text3, " (", text4, "): MISSING" }));
						this.IsMissingDependency = true;
					}
				}
			}
			if (!this.Manifest.Directories.IsNullOrEmpty<ModDirectory>())
			{
				foreach (ModDirectory modDirectory in this.Manifest.Directories)
				{
					if (!modDirectory.Dependencies.IsNullOrEmpty<KeyValuePair<string, string>>())
					{
						foreach (KeyValuePair<string, string> keyValuePair in modDirectory.Dependencies)
						{
							string text;
							string text2;
							keyValuePair.Deconstruct(out text2, out text);
							string text5 = text2;
							string text6 = text;
							ModInfo modInfo2;
							if (modMap.TryGetValue(text5, out modInfo2) && modInfo2.Manifest.Version.EqualsSemantic(text6))
							{
								global::Logger.buildLog.Info(string.Concat(new string[] { "[Optional] ", this.ID, " -> ", text5, " (", text6, "): OK" }));
								uint num2;
								if (!this.Dependencies.TryGetValue(modInfo2, out num2) || num2 < 1000U)
								{
									this.Dependencies[modInfo2] = 1000U;
								}
							}
							else
							{
								global::Logger.buildLog.Info(string.Concat(new string[] { "[Optional] ", this.ID, " -> ", text5, " (", text6, "): MISSING" }));
							}
						}
					}
				}
			}
			if (!this.Manifest.LoadBefore.IsNullOrEmpty<string>())
			{
				foreach (string text7 in this.Manifest.LoadBefore)
				{
					ModInfo modInfo3;
					uint num3;
					if (modMap.TryGetValue(text7, out modInfo3) && (!modInfo3.Dependencies.TryGetValue(this, out num3) || num3 < 1U))
					{
						modInfo3.Dependencies[this] = 1U;
					}
				}
			}
			if (!this.Manifest.LoadAfter.IsNullOrEmpty<string>())
			{
				foreach (string text8 in this.Manifest.LoadAfter)
				{
					ModInfo modInfo4;
					uint num4;
					if (modMap.TryGetValue(text8, out modInfo4) && (!this.Dependencies.TryGetValue(modInfo4, out num4) || num4 < 1U))
					{
						this.Dependencies[modInfo4] = 1U;
					}
				}
			}
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x001124EC File Offset: 0x001106EC
		public void AssertDependencies()
		{
			foreach (KeyValuePair<ModInfo, uint> keyValuePair in this.Dependencies)
			{
				ModInfo modInfo;
				uint num;
				keyValuePair.Deconstruct(out modInfo, out num);
				ModInfo modInfo2 = modInfo;
				uint num2 = num;
				if (!modInfo2.Active && num2 >= 10000000U)
				{
					global::Logger.buildLog.Error("Required dependency '" + modInfo2.ID + "' is not loaded");
					this.IsMissingDependency = true;
				}
			}
		}

		/// <summary>
		/// Read mod root configuration files: manifest.json, config.json, workshop.json, modconfig.json.
		/// </summary>
		// Token: 0x06003827 RID: 14375 RVA: 0x00112580 File Offset: 0x00110780
		public void ReadConfigurations()
		{
			foreach (FileInfo fileInfo in this.Directory.EnumerateFiles(null, SearchOption.TopDirectoryOnly))
			{
				try
				{
					this.ReadConfiguration(fileInfo);
				}
				catch (Exception ex)
				{
					this.Error(ex);
				}
			}
			this.ID = Regex.Replace(this.Manifest.ID ?? this.ID, "[^\\w ]", "");
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x00112618 File Offset: 0x00110818
		private void ReadConfiguration(FileInfo File)
		{
			string text = File.Name.ToLower();
			if (!(text == "manifest.json"))
			{
				if (text == "config.json")
				{
					ModManifest modManifest = ModManager.JsonSerializer.Deserialize<ModManifest>(File.FullName);
					if (this.Manifest.ID == null)
					{
						this.Manifest.ID = modManifest.ID;
					}
					this.Warn("Mod using config.json, please convert to manifest.json and check out https://wiki.cavesofqud.com/wiki/Modding:Mod_Configuration for other options to set");
					return;
				}
				if (!(text == "workshop.json"))
				{
					if (!(text == "modconfig.json"))
					{
						return;
					}
					ModManager.JsonSerializer.Populate(File.FullName, this.TextureConfiguration);
				}
				else
				{
					this.WorkshopInfo = ModManager.JsonSerializer.Deserialize<SteamWorkshopInfo>(File.FullName);
					if (this.Manifest.Tags == null)
					{
						this.Manifest.Tags = this.WorkshopInfo.Tags;
					}
					if (this.Manifest.PreviewImage == null)
					{
						this.Manifest.PreviewImage = this.WorkshopInfo.ImagePath;
					}
					if (this.Manifest.Title == null && this.WorkshopInfo.Title != null)
					{
						this.Manifest.Title = ConsoleLib.Console.ColorUtility.EscapeFormatting(this.WorkshopInfo.Title);
						return;
					}
				}
			}
			else
			{
				ModManager.JsonSerializer.Populate(File.FullName, this.Manifest);
				if (this.Manifest.LoadOrder != null)
				{
					this.Warn("Mod defining manual load order, please convert it to use the Dependencies field.");
					return;
				}
			}
		}

		/// <summary>
		/// Load settings by ID from <see cref="F:XRL.ModManager.ModSettingsMap" />.
		/// </summary>
		// Token: 0x06003829 RID: 14377 RVA: 0x00112788 File Offset: 0x00110988
		public void LoadSettings()
		{
			if (!ModManager.ModSettingsMap.TryGetValue(this.ID, out this.Settings))
			{
				this.Settings = new ModSettings();
				ModManager.ModSettingsMap[this.ID] = this.Settings;
			}
			this.Settings.Title = this.DisplayTitle;
		}

		/// <summary>
		/// Check for script and XML files within the mod directory and sort them into their respective lists.
		/// </summary>
		// Token: 0x0600382A RID: 14378 RVA: 0x001127E0 File Offset: 0x001109E0
		public void InitializeFiles()
		{
			this.IsScripting = false;
			string fullName = this.Directory.FullName;
			if (this.Manifest.Directories.IsNullOrEmpty<ModDirectory>())
			{
				global::Logger.buildLog.Info("Loading path: " + Platform.IO.Path.DirectorySeparatorChar.ToString());
				this.InitializeFiles(this.Directory);
				return;
			}
			Rack<DirectoryInfo> rack = new Rack<DirectoryInfo>();
			XRL.Version version = XRLGame.MarketingVersion;
			XRL.Version version2 = XRLGame.CoreVersion;
			Dictionary<string, ModInfo> modMap = ModManager.ModMap;
			foreach (ModDirectory modDirectory in this.Manifest.Directories)
			{
				if (!modDirectory.Paths.IsNullOrEmpty<string>() && version.EqualsSemantic(modDirectory.Version) && version2.EqualsSemantic(modDirectory.Build))
				{
					GameOption.RequiresSpec options = modDirectory.Options;
					if (options == null || options.RequirementsMet)
					{
						if (!modDirectory.Dependencies.IsNullOrEmpty<KeyValuePair<string, string>>())
						{
							bool flag = false;
							foreach (KeyValuePair<string, string> keyValuePair in modDirectory.Dependencies)
							{
								string text;
								string text2;
								keyValuePair.Deconstruct(out text, out text2);
								string text3 = text;
								string text4 = text2;
								ModInfo modInfo;
								if (!modMap.TryGetValue(text3, out modInfo) || !modInfo.Manifest.Version.EqualsSemantic(text4) || !modInfo.IsEnabled || modInfo.LoadOrder > this.LoadOrder)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								goto IL_030A;
							}
						}
						if (!modDirectory.Exclusions.IsNullOrEmpty<KeyValuePair<string, string>>())
						{
							bool flag2 = false;
							foreach (KeyValuePair<string, string> keyValuePair in modDirectory.Exclusions)
							{
								string text;
								string text2;
								keyValuePair.Deconstruct(out text2, out text);
								string text5 = text2;
								string text6 = text;
								ModInfo modInfo2;
								if (modMap.TryGetValue(text5, out modInfo2) && modInfo2.Manifest.Version.EqualsSemantic(text6) && modInfo2.IsEnabled)
								{
									flag2 = true;
									break;
								}
							}
							if (flag2)
							{
								goto IL_030A;
							}
						}
						foreach (string text7 in modDirectory.Paths)
						{
							DirectoryInfo directoryInfo = new DirectoryInfo(Platform.IO.Path.Join(this.Path, text7));
							string fullName2 = directoryInfo.FullName;
							if (directoryInfo.Exists && fullName2.StartsWith(fullName))
							{
								bool flag3 = true;
								foreach (DirectoryInfo directoryInfo2 in rack)
								{
									if (fullName2.StartsWith(directoryInfo2.FullName))
									{
										flag3 = false;
										break;
									}
								}
								if (flag3)
								{
									rack.Add(directoryInfo);
									for (int k = rack.Count - 2; k >= 0; k--)
									{
										if (rack[k].FullName.StartsWith(fullName2))
										{
											rack.RemoveAt(k);
										}
									}
								}
							}
						}
					}
				}
				IL_030A:;
			}
			foreach (DirectoryInfo directoryInfo3 in rack)
			{
				string text8 = Platform.IO.Path.GetRelativePath(fullName, directoryInfo3.FullName);
				if (text8 == ".")
				{
					text8 = Platform.IO.Path.DirectorySeparatorChar.GetString();
				}
				global::Logger.buildLog.Info("Loading path: " + text8);
				this.InitializeFiles(directoryInfo3);
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x00112BB0 File Offset: 0x00110DB0
		private void InitializeFiles(DirectoryInfo Directory)
		{
			foreach (FileSystemInfo fileSystemInfo in Directory.EnumerateFileSystemInfos())
			{
				if ((fileSystemInfo.Attributes & FileAttributes.Hidden) <= FileAttributes.None)
				{
					if ((fileSystemInfo.Attributes & FileAttributes.Directory) > FileAttributes.None)
					{
						this.InitializeFiles((DirectoryInfo)fileSystemInfo);
					}
					else
					{
						FileInfo fileInfo = (FileInfo)fileSystemInfo;
						ModFile modFile = new ModFile(this, fileInfo);
						this.Size += fileInfo.Length;
						this.Files.Add(modFile);
						if (!this.IsScripting && modFile.Type == ModFileType.CSharp)
						{
							this.IsScripting = true;
						}
					}
				}
			}
		}

		/// <summary>
		/// Display a popup to the user with failure information and ask to retry.
		/// Clears <see cref="F:XRL.ModSettings.Failed" /> on success or no errors.
		/// </summary>
		// Token: 0x0600382C RID: 14380 RVA: 0x00112C64 File Offset: 0x00110E64
		public void ConfirmFailure()
		{
			int count = this.Settings.Errors.Count;
			if (count > 0)
			{
				this.LogToClipboard();
				string text = this.DisplayTitle + " - {{R|Errors}}";
				string text2 = string.Join("\n", this.Settings.Errors.Take<string>(3));
				if (count > 3)
				{
					text2 = text2 + "\n(... {{R|+" + (count - 3).ToString() + "}} more)";
				}
				text2 = text2 + "\n\nAutomatically on your clipboard should you wish to forward it to " + (this.Manifest.Author ?? "the mod author") + ".";
				List<QudMenuItem> list = new List<QudMenuItem>(PopupMessage.CancelButton);
				list.Add(new QudMenuItem
				{
					text = "{{W|[R]}} {{y|Retry}}",
					command = "retry",
					hotkey = "R"
				});
				if (this.WorkshopInfo != null)
				{
					list.Add(new QudMenuItem
					{
						text = "{{W|[W]}} {{y|Workshop}}",
						command = "workshop",
						hotkey = "W"
					});
				}
				Popup.WaitNewPopupMessage(text2, list, delegate(QudMenuItem i)
				{
					if (!(i.command == "workshop"))
					{
						if (i.command == "retry")
						{
							this.Settings.Failed = false;
						}
						return;
					}
					SteamWorkshopInfo workshopInfo = this.WorkshopInfo;
					if (workshopInfo == null)
					{
						return;
					}
					workshopInfo.OpenWorkshopPage();
				}, null, text, null, 0, null, null, null, true, true, null, null, false);
				return;
			}
			this.Settings.Failed = false;
		}

		/// <summary>
		/// Display a popup to the user with dependency information
		/// Clears <see cref="F:XRL.ModSettings.Failed" /> on success or no errors.
		/// </summary>
		// Token: 0x0600382D RID: 14381 RVA: 0x00112DAC File Offset: 0x00110FAC
		public void ConfirmDependencies()
		{
			Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder();
			(ref utf16ValueStringBuilder).AppendMultiple(this.DisplayTitle, " is missing one or more dependencies.");
			if (!this.Manifest.Dependencies.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				(ref utf16ValueStringBuilder).Compound("{{W|=== Required ===}}", "\n\n");
				foreach (KeyValuePair<string, string> keyValuePair in this.Manifest.Dependencies)
				{
					string text;
					string text2;
					keyValuePair.Deconstruct(out text, out text2);
					string text3 = text;
					string text4 = text2;
					this.AppendDependencyConfirmation(ref utf16ValueStringBuilder, text3, text4);
				}
			}
			if (!this.Manifest.Directories.IsNullOrEmpty<ModDirectory>())
			{
				bool flag = true;
				foreach (ModDirectory modDirectory in this.Manifest.Directories)
				{
					if (!modDirectory.Dependencies.IsNullOrEmpty<KeyValuePair<string, string>>())
					{
						if (flag)
						{
							(ref utf16ValueStringBuilder).Compound("{{W|=== Optional ===}}", "\n\n");
							flag = false;
						}
						foreach (KeyValuePair<string, string> keyValuePair in modDirectory.Dependencies)
						{
							string text;
							string text2;
							keyValuePair.Deconstruct(out text2, out text);
							string text5 = text2;
							string text6 = text;
							this.AppendDependencyConfirmation(ref utf16ValueStringBuilder, text5, text6);
						}
					}
				}
			}
			if (utf16ValueStringBuilder.Length > 0)
			{
				Popup.WaitNewPopupMessage(utf16ValueStringBuilder.ToString(), null, null, null, "{{W|Dependencies}}", null, 0, null, null, null, true, true, null, null, false);
			}
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x00112F4C File Offset: 0x0011114C
		public void ConfirmUpdate()
		{
			if (this.RemoteVersion == XRL.Version.Zero)
			{
				return;
			}
			if (this.Settings != null)
			{
				this.Settings.UpdateVersion = new XRL.Version?(this.RemoteVersion);
				ModManager.WriteModSettings();
			}
			bool canUpdate = this.Source == ModSource.Steam;
			Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder();
			utf16ValueStringBuilder.Append(this.DisplayTitle);
			utf16ValueStringBuilder.Append(string.Format(" has a new version available: {0}.", this.RemoteVersion));
			if (canUpdate)
			{
				utf16ValueStringBuilder.Append("\n\nDo you want to download it?");
			}
			if (utf16ValueStringBuilder.Length > 0)
			{
				Popup.WaitNewPopupMessage(utf16ValueStringBuilder.ToString(), canUpdate ? PopupMessage.YesNoButton : null, delegate(QudMenuItem i)
				{
					if (canUpdate && i.command == "Yes")
					{
						GameManager.Instance.StartCoroutine(this.DownloadUpdate());
					}
				}, null, "{{W|Update Available}}", null, 0, null, null, null, true, true, null, null, false);
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x00113036 File Offset: 0x00111236
		public IEnumerator DownloadUpdate()
		{
			if (this.Source == ModSource.Steam && this.WorkshopInfo != null)
			{
				PublishedFileId_t id = new PublishedFileId_t(this.WorkshopInfo.WorkshopId);
				try
				{
					this.Directory.Delete();
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Exception deleting workshop directory", ex, "game_exception");
					yield break;
				}
				if (SteamUGC.DownloadItem(id, true))
				{
					using (Loading.StartTask("Updating " + this.DisplayTitleStripped + "...", null, true))
					{
						uint flags = 48U;
						while ((SteamUGC.GetItemState(id) & flags) != 0U)
						{
							yield return new WaitForSeconds(1f);
						}
						this.ReadConfigurations();
						this.HasUpdate = false;
					}
					Loading.Status status = default(Loading.Status);
				}
				id = default(PublishedFileId_t);
			}
			yield break;
			yield break;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x00113048 File Offset: 0x00111248
		private void AppendDependencyConfirmation(ref Utf16ValueStringBuilder SB, string ID, string Version)
		{
			ModInfo modInfo;
			string text;
			string text2;
			if (ModManager.ModMap.TryGetValue(ID, out modInfo))
			{
				if (modInfo.Manifest.Version.EqualsSemantic(Version))
				{
					if (!modInfo.Active)
					{
						text = "Invalid";
						text2 = "R";
					}
					else
					{
						text = "OK";
						text2 = "g";
					}
				}
				else
				{
					text = "Version mismatch";
					text2 = "W";
				}
			}
			else
			{
				text = "Missing";
				text2 = "R";
			}
			(ref SB).AppendMultiple("\n{{", text2, "|", ID);
			(ref SB).AppendMultiple(" (", ConsoleLib.Console.ColorUtility.EscapeFormatting(Version), "): ", text, "}}");
		}

		/// <summary>
		/// Write mod error and warning messages to the clipboard.
		/// </summary>
		// Token: 0x06003831 RID: 14385 RVA: 0x001130E8 File Offset: 0x001112E8
		public void LogToClipboard()
		{
			StringBuilder stringBuilder = Strings.SB.Clear().Append("=== ").Append(this.DisplayTitleStripped);
			if (!this.Manifest.Version.IsZero())
			{
				stringBuilder.Append(" ").Append(this.Manifest.Version.ToString());
			}
			stringBuilder.Append(" Errors ===\n");
			if (this.Settings.Errors.Any<string>())
			{
				stringBuilder.AppendRange(this.Settings.Errors, "\n");
			}
			else
			{
				stringBuilder.Append("None");
			}
			stringBuilder.Append("\n== Warnings ==\n");
			if (this.Settings.Warnings.Any<string>())
			{
				stringBuilder.AppendRange(this.Settings.Warnings, "\n");
			}
			else
			{
				stringBuilder.Append("None");
			}
			ClipboardHelper.SetClipboardData(stringBuilder.ToString());
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x001131DF File Offset: 0x001113DF
		public Sprite GetDefaultSprite()
		{
			return SpriteManager.GetUnitySprite("UI/Achievements/coq.png");
		}

		/// <summary>
		/// Get unity sprite of image defined in manifest.json or workshop.json.
		/// </summary>
		// Token: 0x06003833 RID: 14387 RVA: 0x001131EC File Offset: 0x001113EC
		public Sprite GetSprite()
		{
			string text;
			if ((text = this.Manifest.PreviewImage) == null)
			{
				SteamWorkshopInfo workshopInfo = this.WorkshopInfo;
				text = ((workshopInfo != null) ? workshopInfo.ImagePath : null);
			}
			string text2 = text;
			if (string.IsNullOrEmpty(text2))
			{
				return this.GetDefaultSprite();
			}
			string text3 = Platform.IO.Path.Combine(new string[] { this.Path, text2 });
			if (!text3.StartsWith(this.Path))
			{
				return this.GetDefaultSprite();
			}
			Sprite sprite = null;
			if (ModInfo.spriteByPath.TryGetValue(text3, out sprite))
			{
				return sprite;
			}
			Texture2D texture2D = null;
			if (File.Exists(text3))
			{
				byte[] array = File.ReadAllBytes(text3);
				texture2D = new Texture2D(2, 2, TextureFormat.ARGB32, false);
				texture2D.LoadImage(array);
				texture2D.filterMode = global::UnityEngine.FilterMode.Trilinear;
			}
			if (texture2D != null)
			{
				sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f));
			}
			ModInfo.spriteByPath.Add(text3, sprite);
			return sprite;
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x001132E0 File Offset: 0x001114E0
		public bool TryBuildAssembly(RoslynCSharpCompiler Service, Rack<string> PathList, out Assembly Assembly)
		{
			PathList.Clear();
			foreach (ModFile modFile in this.Files)
			{
				if (modFile.Type == ModFileType.CSharp)
				{
					PathList.Add(modFile.OriginalName);
				}
			}
			string[] array = PathList.ToArray();
			string text = ((array.Length == 1) ? "file" : "files");
			global::Logger.buildLog.Info(string.Format("Compiling {0} {1}...", array.Length, text));
			Service.OutputName = this.ID;
			CompilationResult compilationResult = Service.CompileFromFiles(array, null);
			if (compilationResult.Success)
			{
				global::Logger.buildLog.Info("Success :)");
				Assembly outputAssembly;
				Assembly = (outputAssembly = compilationResult.OutputAssembly);
				this.Assembly = outputAssembly;
				this.Settings.Failed = false;
				if (compilationResult.OutputFile.IsNullOrEmpty())
				{
					Service.ReferenceAssemblies.Add(AssemblyReference.FromImage(compilationResult.OutputAssemblyImage));
				}
				else
				{
					Service.ReferenceAssemblies.Add(AssemblyReference.FromNameOrFile(compilationResult.OutputFile, null));
					global::Logger.buildLog.Info("Location: " + compilationResult.OutputFile);
				}
			}
			else
			{
				global::Logger.buildLog.Info("Failure :(");
				Assembly = null;
				this.Settings.Failed = true;
			}
			if (compilationResult.ErrorCount > 0)
			{
				global::Logger.buildLog.Info("== COMPILER ERRORS ==");
				foreach (CompilationError compilationError in compilationResult.Errors)
				{
					if (compilationError.IsError)
					{
						string text2 = DataManager.SanitizePathForDisplay(compilationError.ToString());
						global::Logger.buildLog.Error(text2);
						this.Error(text2);
					}
				}
			}
			if (compilationResult.WarningCount > 0)
			{
				global::Logger.buildLog.Info("== COMPILER WARNINGS ==");
				foreach (CompilationError compilationError2 in compilationResult.Errors)
				{
					if (compilationError2.IsWarning)
					{
						string text3 = DataManager.SanitizePathForDisplay(compilationError2.ToString());
						global::Logger.buildLog.Info(text3);
						this.Warn(text3);
					}
				}
			}
			this.ApplyHarmonyPatches();
			return compilationResult.Success;
		}

		/// <summary>
		/// Log a warning with mod context.
		/// </summary>
		/// <remarks>Also added to <see cref="F:XRL.ModSettings.Warnings" />.</remarks>
		// Token: 0x06003835 RID: 14389 RVA: 0x0011351C File Offset: 0x0011171C
		public void Warn(object msg)
		{
			MetricsManager.LogModWarning(this, msg);
		}

		/// <summary>
		/// Log an error with mod context.
		/// </summary>
		/// <remarks>Also added to <see cref="F:XRL.ModSettings.Errors" />.</remarks>
		// Token: 0x06003836 RID: 14390 RVA: 0x00113525 File Offset: 0x00111725
		public void Error(object msg)
		{
			MetricsManager.LogModError(this, msg);
		}

		/// <summary>
		/// Apply harmony patches from this mod's assembly, if it has any.
		/// </summary>
		/// <remarks>Uses <see cref="F:XRL.ModInfo.ID" /> as the ID for the <see cref="F:XRL.ModInfo.Harmony" /> instance.</remarks>
		// Token: 0x06003837 RID: 14391 RVA: 0x00113530 File Offset: 0x00111730
		public void ApplyHarmonyPatches()
		{
			try
			{
				if (!(this.Assembly == null))
				{
					if (this.Assembly.GetTypes().Any<Type>((Type x) => x.IsDefined(typeof(HarmonyAttribute), true)))
					{
						global::Logger.buildLog.Info("Applying Harmony patches...");
						this.Harmony = this.Harmony ?? new Harmony(this.ID);
						this.Harmony.PatchAll(this.Assembly);
						global::Logger.buildLog.Info("Success :)");
					}
				}
			}
			catch (Exception ex)
			{
				string text = "Exception applying harmony patches: ";
				Exception ex2 = ex;
				this.Error(text + ((ex2 != null) ? ex2.ToString() : null));
				global::Logger.buildLog.Info("Failure :(");
			}
		}

		/// <summary>
		/// Unapply harmony patches using this mod's <see cref="F:XRL.ModInfo.ID" />.
		/// </summary>
		// Token: 0x06003838 RID: 14392 RVA: 0x00113610 File Offset: 0x00111810
		public void UnapplyHarmonyPatches()
		{
			try
			{
				if (this.Harmony.GetPatchedMethods().Any<MethodBase>())
				{
					global::Logger.buildLog.Info("Unapplying Harmony patches...");
					this.Harmony.UnpatchAll(this.Harmony.Id);
				}
			}
			catch (Exception ex)
			{
				string text = "Exception unapplying harmony patches: ";
				Exception ex2 = ex;
				this.Error(text + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		/// <summary>
		/// Initialize a new <see cref="T:XRL.SteamWorkshopInfo" /> instance with <paramref name="PublishedFileId" />.
		/// </summary>
		// Token: 0x06003839 RID: 14393 RVA: 0x00113688 File Offset: 0x00111888
		public void InitializeWorkshopInfo(ulong PublishedFileId)
		{
			this.WorkshopInfo = new SteamWorkshopInfo();
			this.WorkshopInfo.WorkshopId = PublishedFileId;
			this.SaveWorkshopInfo();
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x001136A7 File Offset: 0x001118A7
		public void SaveWorkshopInfo()
		{
			if (this.WorkshopInfo == null)
			{
				return;
			}
			ModManager.JsonSerializer.Serialize(Platform.IO.Path.Combine(new string[] { this.Path, "workshop.json" }), this.WorkshopInfo);
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x001136E0 File Offset: 0x001118E0
		public int CompareTo(ModInfo Other)
		{
			int num = this.LoadOrder.CompareTo(Other.LoadOrder);
			if (num != 0)
			{
				return num;
			}
			num = this.Manifest.LoadOrder.GetValueOrDefault().CompareTo(Other.Manifest.LoadOrder.GetValueOrDefault());
			if (num != 0)
			{
				return num;
			}
			return string.Compare(this.ID, Other.ID, StringComparison.Ordinal);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x00113744 File Offset: 0x00111944
		public override string ToString()
		{
			return this.ID;
		}

		// Token: 0x04001C33 RID: 7219
		public const uint DEP_WEIGHT_REQUIRED = 10000000U;

		// Token: 0x04001C34 RID: 7220
		public const uint DEP_WEIGHT_OPTIONAL = 1000U;

		// Token: 0x04001C35 RID: 7221
		public const uint DEP_WEIGHT_HINT = 1U;

		/// <summary>The system location of this mod's folder.</summary>
		// Token: 0x04001C36 RID: 7222
		public string Path;

		/// <summary>A unique identifier for this mod.</summary>
		/// <seealso cref="M:XRL.ModManager.RegisterMod(XRL.ModInfo)" />
		// Token: 0x04001C37 RID: 7223
		public string ID = "";

		/// <summary>The load origin of this mod, e.g. the local mods folder or steam workshop.</summary>
		// Token: 0x04001C38 RID: 7224
		public ModSource Source;

		/// <inheritdoc cref="F:XRL.ModInfo.Path" />
		// Token: 0x04001C39 RID: 7225
		public DirectoryInfo Directory;

		/// <summary>The final compiled assembly of all <see cref="!:ScriptFiles" />.</summary>
		// Token: 0x04001C3A RID: 7226
		public Assembly Assembly;

		/// <summary>A harmony instance using <see cref="F:XRL.ModInfo.ID" />.</summary>
		// Token: 0x04001C3B RID: 7227
		public Harmony Harmony;

		/// <summary>A list of all visible files within <see cref="!:FileDirectories" />.</summary>
		// Token: 0x04001C3C RID: 7228
		public Rack<ModFile> Files = new Rack<ModFile>();

		/// <summary>All resolved dependencies mapped with weights.</summary>
		// Token: 0x04001C3D RID: 7229
		public Dictionary<ModInfo, uint> Dependencies = new Dictionary<ModInfo, uint>();

		/// <summary>The load order of this mod, lower values will be prioritized over higher ones.</summary>
		/// <seealso cref="M:XRL.ModInfo.CompareTo(XRL.ModInfo)" />
		// Token: 0x04001C3E RID: 7230
		public int LoadOrder = int.MinValue;

		// Token: 0x04001C3F RID: 7231
		public bool Active;

		/// <value><c>true</c> if this mod contains any <see cref="!:ScriptFiles" />; otherwise, <c>false</c>.</value>
		// Token: 0x04001C40 RID: 7232
		public bool IsScripting;

		/// <value><c>true</c> if this mod is missing a required dependency; otherwise, <c>false</c>.</value>
		// Token: 0x04001C41 RID: 7233
		public bool IsMissingDependency;

		/// <value><c>true</c> if this mod is has a remote version greater than its local manifest; otherwise, <c>false</c>.</value>
		// Token: 0x04001C42 RID: 7234
		public bool HasUpdate;

		/// <summary>The total size in bytes of all visible files within <see cref="F:XRL.ModInfo.Directory" />.</summary>
		// Token: 0x04001C43 RID: 7235
		public long Size;

		/// <summary>Version available for update.</summary>
		// Token: 0x04001C44 RID: 7236
		public XRL.Version RemoteVersion;

		/// <summary>User configured state which is stored in ModSettings.json.</summary>
		/// <seealso cref="M:XRL.ModInfo.LoadSettings" />
		// Token: 0x04001C45 RID: 7237
		public ModSettings Settings;

		/// <summary>A manifest.json read from the mod's root <see cref="F:XRL.ModInfo.Directory" />.</summary>
		/// <seealso cref="M:XRL.ModInfo.ReadConfigurations" />
		// Token: 0x04001C46 RID: 7238
		public ModManifest Manifest = new ModManifest();

		/// <summary>A workshop.json read and written to the mod's root <see cref="F:XRL.ModInfo.Directory" />.</summary>
		/// <seealso cref="M:XRL.ModInfo.ReadConfigurations" />
		// Token: 0x04001C47 RID: 7239
		public SteamWorkshopInfo WorkshopInfo;

		/// <summary>A modconfig.json read from the mod's root <see cref="F:XRL.ModInfo.Directory" />.</summary>
		/// <remarks>This might be obsoleted in the future with a per-file texture configuration like <c>"cute_snapjaw.png.cfg"</c>.</remarks>
		/// <seealso cref="M:XRL.ModInfo.ReadConfigurations" />
		// Token: 0x04001C48 RID: 7240
		public TextureConfiguration TextureConfiguration = new TextureConfiguration();

		// Token: 0x04001C49 RID: 7241
		[ModSensitiveStaticCache(true)]
		private static Dictionary<string, Sprite> spriteByPath = new Dictionary<string, Sprite>();
	}
}
