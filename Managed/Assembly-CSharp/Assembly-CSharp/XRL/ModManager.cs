using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cysharp.Text;
using Genkit;
using HarmonyLib;
using Newtonsoft.Json;
using Platform.IO;
using RoslynCSharp;
using RoslynCSharp.Compiler;
using Steamworks;
using UnityEngine;
using XRL.Collections;
using XRL.Core;
using XRL.UI;
using XRL.World;

namespace XRL
{
	// Token: 0x02000524 RID: 1316
	[HasModSensitiveStaticCache]
	public static class ModManager
	{
		// Token: 0x06003840 RID: 14400 RVA: 0x001137C0 File Offset: 0x001119C0
		private static RoslynCSharpCompiler GetCompilerService()
		{
			RoslynCSharpCompiler roslynCompilerService = ScriptDomain.CreateDomain("ModsDomain", true, true, null).RoslynCompilerService;
			roslynCompilerService.GenerateInMemory = !Options.OutputModAssembly;
			roslynCompilerService.OutputDirectory = DataManager.SavePath("ModAssemblies");
			roslynCompilerService.OutputPDBExtension = ".pdb";
			roslynCompilerService.GenerateSymbols = !roslynCompilerService.GenerateInMemory;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().Where<Assembly>(new Func<Assembly, bool>(ModManager.MainAssemblyPredicate)))
			{
				roslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromAssembly(assembly));
			}
			global::System.Version marketingVersion = ModManager.MarketingVersion;
			global::System.Version coreVersion = ModManager.CoreVersion;
			string text = string.Format("VERSION_{0}_{1}", marketingVersion.Major, marketingVersion.Minor);
			string text2 = string.Format("BUILD_{0}_{1}_{2}", coreVersion.Major, coreVersion.Minor, coreVersion.Build);
			roslynCompilerService.DefineSymbols.Add(text);
			roslynCompilerService.DefineSymbols.Add(text2);
			global::Logger.buildLog.Info("Defined symbol: " + text);
			global::Logger.buildLog.Info("Defined symbol: " + text2);
			return roslynCompilerService;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x00113920 File Offset: 0x00111B20
		[Obsolete("Use ModManager.Initialized")]
		public static bool Compiled
		{
			get
			{
				return ModManager.Initialized;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x00113927 File Offset: 0x00111B27
		public static string MarketingPostfix
		{
			get
			{
				return XRLGame.MarketingPostfix;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x0011392E File Offset: 0x00111B2E
		public static global::System.Version MarketingVersion
		{
			get
			{
				return XRLGame.MarketingVersion;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x00113935 File Offset: 0x00111B35
		public static global::System.Version CoreVersion
		{
			get
			{
				return XRLGame.CoreVersion;
			}
		}

		/// <summary>
		/// Yields every type in <see cref="P:XRL.ModManager.ActiveAssemblies" />.
		/// </summary>
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x0011393C File Offset: 0x00111B3C
		public static IEnumerable<Type> ActiveTypes
		{
			get
			{
				foreach (Module module in ModManager.ActiveModules)
				{
					Type[] types = module.GetTypes();
					int num;
					for (int i = 0; i < types.Length; i = num + 1)
					{
						yield return types[i];
						num = i;
					}
					types = null;
				}
				Container<Module>.Enumerator enumerator = default(Container<Module>.Enumerator);
				yield break;
				yield break;
			}
		}

		/// <summary>
		/// Yields the current executing assembly followed by any enabled script mod assemblies in priority order.
		/// </summary>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x00113945 File Offset: 0x00111B45
		public static IEnumerable<Assembly> ActiveAssemblies
		{
			get
			{
				yield return Assembly.GetExecutingAssembly();
				foreach (Assembly assembly in ModManager.ModAssemblies)
				{
					yield return assembly;
				}
				Container<Assembly>.Enumerator enumerator = default(Container<Assembly>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x00113950 File Offset: 0x00111B50
		public static void CheckXRLConflicts()
		{
			ModInfo modInfo = new ModInfo(DataManager.FilePath(), "Base", ModSource.Unknown, false);
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Dictionary<string, object> dictionary = new Dictionary<string, object>(ModManager.ModAssemblies.Count * 32);
			foreach (ModInfo modInfo2 in ModManager.ActiveMods)
			{
				if (modInfo2.Assembly != null)
				{
					Type[] exportedTypes = modInfo2.Assembly.GetExportedTypes();
					for (int i = 0; i < exportedTypes.Length; i++)
					{
						string fullName = exportedTypes[i].FullName;
						if (fullName.StartsWith("XRL."))
						{
							object obj;
							if (!dictionary.TryGetValue(fullName, out obj))
							{
								if (executingAssembly.GetType(fullName) != null)
								{
									dictionary[fullName] = new Rack<ModInfo> { modInfo, modInfo2 };
								}
								else
								{
									dictionary[fullName] = modInfo2;
								}
							}
							else
							{
								Rack<ModInfo> rack = obj as Rack<ModInfo>;
								if (rack != null)
								{
									rack.Add(modInfo2);
								}
								else
								{
									dictionary[fullName] = new Rack<ModInfo>
									{
										(ModInfo)obj,
										modInfo2
									};
								}
							}
						}
					}
				}
			}
			HashSet<ModInfo> hashSet = new HashSet<ModInfo>();
			bool flag = false;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					string text;
					object obj2;
					keyValuePair.Deconstruct(out text, out obj2);
					string text2 = text;
					Rack<ModInfo> rack2 = obj2 as Rack<ModInfo>;
					if (rack2 != null && rack2.Count > 1)
					{
						if (!flag)
						{
							global::Logger.buildLog.Error("==== TYPE CONFLICTS DETECTED ====");
							flag = true;
						}
						utf16ValueStringBuilder.Append(text2);
						utf16ValueStringBuilder.Append(" is defined in: ");
						int j = 0;
						int count = rack2.Count;
						while (j < count)
						{
							hashSet.Add(rack2[j]);
							if (j != 0)
							{
								utf16ValueStringBuilder.Append(", ");
							}
							string displayTitleStripped = rack2[j].DisplayTitleStripped;
							bool flag2 = displayTitleStripped.Contains(',');
							if (flag2)
							{
								utf16ValueStringBuilder.Append('\'');
							}
							utf16ValueStringBuilder.Append(displayTitleStripped);
							if (flag2)
							{
								utf16ValueStringBuilder.Append('\'');
							}
							j++;
						}
						global::Logger.buildLog.Info(utf16ValueStringBuilder.ToString());
						utf16ValueStringBuilder.Clear();
					}
				}
				if (flag)
				{
					hashSet.Remove(modInfo);
					foreach (ModInfo modInfo3 in hashSet)
					{
						if (utf16ValueStringBuilder.Length != 0)
						{
							utf16ValueStringBuilder.Append(", ");
						}
						utf16ValueStringBuilder.Append(modInfo3.DisplayTitleStripped);
					}
					utf16ValueStringBuilder.Insert(0, "Type conflicts detected in mods: ");
					utf16ValueStringBuilder.Append(".\nThis can lead to unexpected casting errors and null references for types that are resolved via their name in XML like parts, skills, and mutations.");
					utf16ValueStringBuilder.Append("\nIt's strongly recommended to rename your type to be unique.");
					utf16ValueStringBuilder.Append("\nSee build log for details of all conflicting types.");
					MetricsManager.LogModError(null, utf16ValueStringBuilder.ToString());
				}
			}
		}

		/// <summary>
		/// Register mod in the manager if a mod with that ID has not already been registered.
		/// </summary>
		/// <returns><c>true</c> if the mod was successfully registered; otherwise, <c>false</c>.</returns>
		// Token: 0x06003848 RID: 14408 RVA: 0x00113CC8 File Offset: 0x00111EC8
		public static bool RegisterMod(ModInfo Mod)
		{
			if (ModManager.ModMap.ContainsKey(Mod.ID))
			{
				Mod.Warn(string.Concat(new string[]
				{
					"A mod with the ID \"",
					Mod.ID,
					"\" already exists in ",
					DataManager.SanitizePathForDisplay(ModManager.ModMap[Mod.ID].Path),
					", skipping."
				}));
				return false;
			}
			ModManager.ModMap[Mod.ID] = Mod;
			ModManager.Mods.Add(Mod);
			if (Mod.WorkshopInfo != null && Mod.WorkshopInfo.WorkshopId != 0UL)
			{
				ModManager.ModWorkshopMap[Mod.WorkshopInfo.WorkshopId] = Mod;
			}
			return true;
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x00113D80 File Offset: 0x00111F80
		public static bool DoesModDefineType(Type T)
		{
			return ModManager.ModAssemblies.Contains(T.Assembly);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x00113D94 File Offset: 0x00111F94
		public static bool DoesModDefineType(string TypeID)
		{
			Type type;
			if (ModManager._typeResolutions.TryGetValue(TypeID, out type))
			{
				return ModManager.ModAssemblies.Contains(type.Assembly);
			}
			return ModManager.ModAssemblies.Any<Assembly>((Assembly x) => x.GetType(TypeID) != null);
		}

		/// <summary>
		/// Attempt to find a type within loaded assemblies.
		/// Mods are searched first according to priority, followed by the main game assembly.
		/// </summary>
		/// <inheritdoc cref="M:XRL.ModManager.ResolveType(System.String,System.String,System.Boolean,System.Boolean,System.Boolean)" />
		// Token: 0x0600384B RID: 14411 RVA: 0x00113DE9 File Offset: 0x00111FE9
		public static Type ResolveType(string TypeID, bool IgnoreCase = false, bool ThrowOnError = false, bool Cache = true)
		{
			return ModManager.ResolveType(null, TypeID, IgnoreCase, ThrowOnError, Cache);
		}

		/// <summary>
		/// Attempt to find a type within loaded assemblies.
		/// Mods are searched first according to priority, followed by the main game assembly.
		/// If not found as Namespace.TypeID it will look for just TypeID also.
		/// </summary>
		/// <param name="Namespace">The default namespace to look in.</param>
		/// <param name="TypeID">The full name of the type, including namespaces.</param>
		/// <param name="IgnoreCase"><c>true</c> to ignore the case of the type name; otherwise, <c>false</c>.</param>
		/// <param name="ThrowOnError">Throw exception if invalid <see cref="!:TypeID" /> is specified.</param>
		/// <param name="Cache">Store the resolved type for this <see cref="!:TypeID" />.</param>
		/// <returns><see cref="T:System.Type" /> if one was found; otherwise, <c>null</c>.</returns>
		// Token: 0x0600384C RID: 14412 RVA: 0x00113DF8 File Offset: 0x00111FF8
		public static Type ResolveType(string Namespace, string TypeID, bool IgnoreCase = false, bool ThrowOnError = false, bool Cache = true)
		{
			if (TypeID.IsNullOrEmpty())
			{
				return null;
			}
			string text = ((Namespace != null) ? (Namespace + "." + TypeID) : TypeID);
			Type type;
			if (ModManager._typeResolutions.TryGetValue(text, out type))
			{
				return type;
			}
			type = Type.GetType(text, false, IgnoreCase);
			if (type == null)
			{
				foreach (Assembly assembly in ModManager.ModAssemblies)
				{
					type = assembly.GetType(text, false, IgnoreCase);
					if (type != null)
					{
						break;
					}
				}
				if (ThrowOnError && type == null)
				{
					throw new TypeLoadException("Could not resolve type '" + text + "' from active assemblies.");
				}
			}
			if (!IgnoreCase && Cache)
			{
				ModManager._typeResolutions[text] = type;
			}
			return type;
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x00113EBC File Offset: 0x001120BC
		public static string ResolveTypeName(Type T)
		{
			string text;
			if (ModManager.typeNames.TryGetValue(T, out text))
			{
				return text;
			}
			ModManager.typeNames.Add(T, T.Name);
			return ModManager.typeNames[T];
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x00113EF8 File Offset: 0x001120F8
		public static void ResetModSensitiveStaticCaches()
		{
			Type typeFromHandle = typeof(ModSensitiveStaticCacheAttribute);
			Type typeFromHandle2 = typeof(ModSensitiveCacheInitAttribute);
			Type typeFromHandle3 = typeof(HasModSensitiveStaticCacheAttribute);
			foreach (FieldInfo fieldInfo in ModManager.GetFieldsWithAttribute(typeFromHandle, typeFromHandle3, false))
			{
				if (fieldInfo.IsStatic)
				{
					try
					{
						bool flag = fieldInfo.FieldType.IsValueType || fieldInfo.GetCustomAttribute<ModSensitiveStaticCacheAttribute>().CreateEmptyInstance;
						fieldInfo.SetValue(null, flag ? Activator.CreateInstance(fieldInfo.FieldType) : null);
					}
					catch (Exception ex)
					{
						MetricsManager.LogAssemblyError(fieldInfo, string.Format("Error initializing {0}.{1}: {2}", fieldInfo.DeclaringType.FullName, fieldInfo.Name, ex));
					}
				}
			}
			foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeFromHandle2, typeFromHandle3, false))
			{
				try
				{
					methodInfo.Invoke(null, new object[0]);
				}
				catch (Exception ex2)
				{
					MetricsManager.LogAssemblyError(methodInfo, string.Format("Error invoking {0}.{1}: {2}", methodInfo.DeclaringType.FullName, methodInfo.Name, ex2));
				}
			}
			XRLCore core = XRLCore.Core;
			if (core == null)
			{
				return;
			}
			core.ReloadUIViews();
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x00114070 File Offset: 0x00112270
		public static void CallAfterGameLoaded()
		{
			foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeof(CallAfterGameLoadedAttribute), typeof(HasCallAfterGameLoadedAttribute), true))
			{
				try
				{
					methodInfo.Invoke(null, Array.Empty<object>());
				}
				catch (Exception ex)
				{
					MetricsManager.LogAssemblyError(methodInfo, string.Format("Error invoking {0}.{1}: {2}", methodInfo.DeclaringType.FullName, methodInfo.Name, ex));
				}
			}
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x00114110 File Offset: 0x00112310
		internal static bool MainAssemblyPredicate(Assembly assembly)
		{
			return !assembly.IsDynamic && !assembly.Location.IsNullOrEmpty() && !assembly.Location.Contains("ModAssemblies") && !assembly.Location.Contains("UIElements") && !assembly.Location.Contains("UnityEditor.") && !assembly.FullName.Contains("ExCSS");
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x00114188 File Offset: 0x00112388
		private static void BuildMods()
		{
			try
			{
				Options.LoadModOptionDefaults();
			}
			catch (Exception ex)
			{
				MetricsManager.LogError(ex);
			}
			global::Logger.buildLog.Info("==== BUILDING MODS ====");
			RoslynCSharpCompiler compilerService = ModManager.GetCompilerService();
			Rack<string> rack = new Rack<string>();
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				global::Logger.buildLog.Info("=== " + modInfo.DisplayTitleStripped.ToUpper() + " ===");
				modInfo.InitializeFiles();
				modInfo.AssertDependencies();
				if (!modInfo.IsEnabled)
				{
					global::Logger.buildLog.Info(string.Format("Skipping, state: {0}", modInfo.State));
				}
				else
				{
					try
					{
						Assembly assembly;
						if (modInfo.IsScripting && modInfo.TryBuildAssembly(compilerService, rack, out assembly))
						{
							ModManager.ModAssemblies.Add(assembly);
							ModManager.ModAssemblyMap[assembly] = modInfo;
							ModManager.ActiveModules.AddRange(assembly.GetModules());
						}
					}
					catch (Exception ex2)
					{
						ModInfo modInfo2 = modInfo;
						string text = "Exception compiling mod assembly: ";
						Exception ex3 = ex2;
						modInfo2.Error(text + ((ex3 != null) ? ex3.ToString() : null));
						modInfo.Settings.Failed = true;
					}
					if (modInfo.IsEnabled)
					{
						modInfo.Active = true;
						ModManager.ActiveMods.Add(modInfo);
						string text2 = "MOD_" + modInfo.ID.ToUpperInvariant().Replace(" ", "_");
						compilerService.DefineSymbols.Add(text2);
						global::Logger.buildLog.Info("Defined symbol: " + text2);
					}
				}
			}
			MinEvent.ResetEvents();
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x00114374 File Offset: 0x00112574
		private static void PrintLoadOrder()
		{
			global::Logger.buildLog.Info("==== FINAL LOAD ORDER ====");
			for (int i = 0; i < ModManager.ActiveMods.Count; i++)
			{
				global::Logger.buildLog.Info(string.Format("{0}: {1}", i + 1, ModManager.ActiveMods[i].ID));
			}
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x001143D1 File Offset: 0x001125D1
		private static bool OnlySupportsEmbeddedMods()
		{
			return false;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x001143D4 File Offset: 0x001125D4
		public static void RefreshModDirectory(string Path, bool Create = false, ModSource Source = ModSource.Local)
		{
			if (Source != ModSource.Embedded && ModManager.OnlySupportsEmbeddedMods())
			{
				return;
			}
			MetricsManager.LogInfo("RefreshModDirectory " + ((Path != null) ? Path : "(null)"));
			Platform.IO.DirectoryInfo directoryInfo = (Create ? Platform.IO.Directory.CreateDirectory(Path) : new Platform.IO.DirectoryInfo(Path));
			if (!directoryInfo.Exists)
			{
				return;
			}
			foreach (Platform.IO.DirectoryInfo directoryInfo2 in directoryInfo.EnumerateDirectories())
			{
				if ((directoryInfo2.Attributes & Platform.IO.FileAttributes.Hidden) <= Platform.IO.FileAttributes.None)
				{
					try
					{
						ModManager.RegisterMod(new ModInfo(directoryInfo2.FullName, directoryInfo2.Name, Source, true));
					}
					catch (Exception ex)
					{
						MetricsManager.LogError("Exception reading local mod directory " + directoryInfo2.Name, ex);
					}
				}
			}
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x001144A8 File Offset: 0x001126A8
		private static void RefreshWorkshopSubscriptions()
		{
			if (!PlatformManager.IsInitialized())
			{
				MetricsManager.LogInfo("Skipping workshop subscription info because steam isn't connected");
				return;
			}
			PublishedFileId_t[] array = new PublishedFileId_t[4096];
			uint subscribedItems = SteamUGC.GetSubscribedItems(array, 4096U);
			MetricsManager.LogInfo("Subscribed workshop items: " + subscribedItems.ToString());
			int num = 0;
			while ((long)num < (long)((ulong)subscribedItems))
			{
				try
				{
					ulong num2;
					string text;
					uint num3;
					if (SteamUGC.GetItemInstallInfo(array[num], out num2, out text, 4096U, out num3))
					{
						if (!global::System.IO.Directory.Exists(text))
						{
							MetricsManager.LogError("Mod directory does not exist: " + text);
						}
						else
						{
							ModManager.RegisterMod(new ModInfo(text, array[num].ToString(), ModSource.Steam, true));
						}
					}
				}
				catch (Exception ex)
				{
					string text2 = "Exception reading workshop mod subscription ";
					PublishedFileId_t publishedFileId_t = array[num];
					MetricsManager.LogError(text2 + publishedFileId_t.ToString(), ex);
				}
				num++;
			}
		}

		/// <summary>
		/// Read current mod settings from ModSettings.json.
		/// </summary>
		/// <param name="Reload">Force reload from disk even if current state in memory (used for undo).</param>
		// Token: 0x06003856 RID: 14422 RVA: 0x0011459C File Offset: 0x0011279C
		public static void ReadModSettings(bool Repopulate = false)
		{
			try
			{
				ModManager.ModSettingsMap.Clear();
				string text = DataManager.LocalPath("ModSettings.json");
				if (Platform.IO.File.Exists(text))
				{
					ModManager.JsonSerializer.Populate(text, ModManager.ModSettingsMap);
				}
				if (Repopulate)
				{
					foreach (ModInfo modInfo in ModManager.Mods)
					{
						modInfo.LoadSettings();
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Failed reading ModSettings.json", ex);
			}
		}

		/// <summary>
		/// Write current mod settings to ModSettings.json.
		/// </summary>
		// Token: 0x06003857 RID: 14423 RVA: 0x00114638 File Offset: 0x00112838
		public static void WriteModSettings()
		{
			if (ModManager.ModSettingsMap == null)
			{
				return;
			}
			string text = DataManager.LocalPath("ModSettings.json");
			ModManager.JsonSerializer.Serialize(text, ModManager.ModSettingsMap);
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x00114668 File Offset: 0x00112868
		public static void Init()
		{
			if (ModManager.Initialized)
			{
				return;
			}
			if (!Application.isPlaying)
			{
				return;
			}
			if (Options.Bag == null)
			{
				MetricsManager.LogError("Mod initialization executed early, options not initialized.");
				return;
			}
			ModManager.ReadModSettings(false);
			try
			{
				ModManager.RefreshModDirectory(DataManager.DLCPath(), false, ModSource.Embedded);
			}
			catch (Exception ex)
			{
				MetricsManager.LogError(ex);
			}
			try
			{
				ModManager.RefreshModDirectory(DataManager.EmbeddedModsPath(), false, ModSource.Embedded);
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError(ex2);
			}
			if (Options.EnableMods)
			{
				try
				{
					ModManager.RefreshModDirectory(DataManager.SavePath("Mods"), true, ModSource.Local);
				}
				catch (Exception ex3)
				{
					MetricsManager.LogError(ex3);
				}
				try
				{
					ModManager.RefreshModDirectory(DataManager.LocalPath("Mods"), true, ModSource.Local);
				}
				catch (Exception ex4)
				{
					MetricsManager.LogError(ex4);
				}
				try
				{
					ModManager.RefreshWorkshopSubscriptions();
				}
				catch (Exception ex5)
				{
					MetricsManager.LogError(ex5);
				}
			}
			try
			{
				ModManager.CheckUpdates();
			}
			catch (Exception ex6)
			{
				MetricsManager.LogError(ex6);
			}
			ModManager.ResolveAllDependencies();
			ModManager.DetermineLoadOrderFast();
			ModManager.BuildMods();
			ModManager.CheckXRLConflicts();
			ModManager.PrintLoadOrder();
			ModManager.Initialized = true;
			ModManager.ResetModSensitiveStaticCaches();
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x00114794 File Offset: 0x00112994
		private static void CheckUpdates()
		{
			if (PlatformManager.IsInitialized())
			{
				PublishedFileId_t[] publishedFileIDs = ModManager.GetPublishedFileIDs();
				UGCQueryHandle_t query = SteamUGC.CreateQueryUGCDetailsRequest(publishedFileIDs, (uint)publishedFileIDs.Length);
				SteamUGC.SetReturnOnlyIDs(query, true);
				SteamUGC.SetReturnKeyValueTags(query, true);
				SteamUGC.SetAllowCachedResponse(query, 300U);
				SteamAPICall_t steamAPICall_t = SteamUGC.SendQueryUGCRequest(query);
				CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(null);
				PlatformManager.Steam.Disposables.Add(callResult);
				callResult.Set(steamAPICall_t, delegate(SteamUGCQueryCompleted_t args, bool _)
				{
					try
					{
						if (args.m_eResult == EResult.k_EResultOK)
						{
							uint num = 0U;
							uint unNumResultsReturned = args.m_unNumResultsReturned;
							while (num < unNumResultsReturned)
							{
								SteamUGCDetails_t steamUGCDetails_t;
								SteamUGC.GetQueryUGCResult(query, num, out steamUGCDetails_t);
								string text;
								SteamUGC.GetQueryUGCKeyValueTag(query, num, "manifest_version", out text, 32U);
								ModInfo modInfo;
								XRL.Version version;
								if (!text.IsNullOrEmpty() && ModManager.ModWorkshopMap.TryGetValue((ulong)steamUGCDetails_t.m_nPublishedFileId, out modInfo) && XRL.Version.TryParse(text, out version))
								{
									modInfo.RemoteVersion = version;
									modInfo.HasUpdate = modInfo.RemoteVersion > modInfo.Manifest.Version;
								}
								num += 1U;
							}
						}
					}
					finally
					{
						SteamUGC.ReleaseQueryUGCRequest(query);
						PlatformManager.Steam.Disposables.Remove(callResult);
						callResult.Dispose();
					}
				});
			}
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x00114838 File Offset: 0x00112A38
		public static void ResolveAllDependencies()
		{
			global::Logger.buildLog.Info("==== RESOLVING DEPENDENCIES ====");
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				modInfo.ResolveDependencies();
			}
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x00114898 File Offset: 0x00112A98
		public static void DetermineLoadOrderFast()
		{
			int num = 1;
			ModManager.Mods.Sort();
			try
			{
				foreach (ModInfo modInfo in ModManager.Mods)
				{
					ModManager.<DetermineLoadOrderFast>g__Visit|50_0(modInfo, ref num);
				}
				ModManager.Mods.Sort();
			}
			catch (Exception)
			{
				foreach (ModInfo modInfo2 in ModManager.Mods)
				{
					modInfo2.LoadOrder = int.MinValue;
				}
				ModManager.DetermineLoadOrder();
			}
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x00114958 File Offset: 0x00112B58
		public static void DetermineLoadOrder()
		{
			WeightedDigraph weightedDigraph = new WeightedDigraph(ModManager.Mods.Count);
			ModManager.Mods.Sort();
			int k = 0;
			int count = ModManager.Mods.Count;
			while (k < count)
			{
				foreach (KeyValuePair<ModInfo, uint> keyValuePair in ModManager.Mods[k].Dependencies)
				{
					ModInfo modInfo;
					uint num;
					keyValuePair.Deconstruct(out modInfo, out num);
					ModInfo modInfo2 = modInfo;
					uint num2 = num;
					int num3 = ModManager.Mods.IndexOf(modInfo2);
					weightedDigraph.AddArc(k, num3, num2);
				}
				k++;
			}
			try
			{
				foreach (RingDeque<int> ringDeque in weightedDigraph.YieldCycles(10000000U))
				{
					ModInfo modInfo3 = ModManager.Mods[ringDeque.First];
					int highlight = ringDeque.Last;
					modInfo3.Error("Circular mod dependency: " + string.Join(" -> ", ringDeque.Select<int, string>(delegate(int i)
					{
						if (i != highlight)
						{
							return ModManager.Mods[i].ID;
						}
						return "*" + ModManager.Mods[i].ID + "*";
					})));
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Graph cycle detection", ex, "game_exception");
			}
			weightedDigraph.Resolve();
			int j = 0;
			int count2 = ModManager.Mods.Count;
			while (j < count2)
			{
				ModManager.Mods[j].LoadOrder = weightedDigraph.GetTarget(j) + 1;
				j++;
			}
			ModManager.Mods.Sort();
		}

		/// <summary>
		/// Get the mod associated with the calling assembly.
		/// </summary>
		/// <returns>The <see cref="T:XRL.ModInfo" /> if one associated with that assembly exists; otherwise, <c>null</c>.</returns>
		// Token: 0x0600385D RID: 14429 RVA: 0x00114B0C File Offset: 0x00112D0C
		public static ModInfo GetMod()
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			ModInfo modInfo;
			if (ModManager.ModAssemblyMap.TryGetValue(callingAssembly, out modInfo))
			{
				return modInfo;
			}
			return null;
		}

		/// <summary>
		/// Get the mod associated with specified ID.
		/// </summary>
		/// <returns>The <see cref="T:XRL.ModInfo" /> if one by that ID exists; otherwise, <c>null</c>.</returns>
		// Token: 0x0600385E RID: 14430 RVA: 0x00114B34 File Offset: 0x00112D34
		public static ModInfo GetMod(string ID)
		{
			if (ID == null)
			{
				return null;
			}
			ModInfo modInfo;
			if (ModManager.ModMap.TryGetValue(ID, out modInfo))
			{
				return modInfo;
			}
			return null;
		}

		/// <summary>
		/// Get the mod associated with specified workshop ID.
		/// </summary>
		/// <returns>The <see cref="T:XRL.ModInfo" /> if one by that workshop ID exists; otherwise, <c>null</c>.</returns>
		// Token: 0x0600385F RID: 14431 RVA: 0x00114B58 File Offset: 0x00112D58
		public static ModInfo GetMod(ulong WorkshopID)
		{
			if (WorkshopID <= 0UL)
			{
				return null;
			}
			ModInfo modInfo;
			if (ModManager.ModWorkshopMap.TryGetValue(WorkshopID, out modInfo))
			{
				return modInfo;
			}
			return null;
		}

		/// <summary>
		/// Get the mod associated with specified assembly.
		/// </summary>
		/// <returns>The <see cref="T:XRL.ModInfo" /> if one defines that assembly; otherwise, <c>null</c>.</returns>
		// Token: 0x06003860 RID: 14432 RVA: 0x00114B80 File Offset: 0x00112D80
		public static ModInfo GetMod(Assembly Assembly)
		{
			if (Assembly == null)
			{
				return null;
			}
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				if (!(modInfo.Assembly != Assembly))
				{
					return modInfo;
				}
			}
			return null;
		}

		/// <summary>
		/// Get the mod associated with a string that may be either
		/// an ID or a string specification of a workshop ID.
		/// </summary>
		/// <returns>The <see cref="T:XRL.ModInfo" /> if one retrievable by either form of ID exists; otherwise, <c>null</c>.</returns>
		// Token: 0x06003861 RID: 14433 RVA: 0x00114BEC File Offset: 0x00112DEC
		public static ModInfo GetModBySpec(string Spec)
		{
			try
			{
				ModInfo mod = ModManager.GetMod(Convert.ToUInt64(Spec));
				if (mod != null)
				{
					return mod;
				}
			}
			catch
			{
			}
			return ModManager.GetMod(Spec);
		}

		/// <summary>
		/// Retrieves whether a mod is loaded that is associated with
		/// a string that may be either an ID or a string specification
		/// of a workshop ID.
		/// </summary>
		// Token: 0x06003862 RID: 14434 RVA: 0x00114C2C File Offset: 0x00112E2C
		public static bool ModLoadedBySpec(string Spec)
		{
			return ModManager.GetModBySpec(Spec) != null;
		}

		/// <summary>Get the first mod encountered in the currently executing stack.</summary>
		// Token: 0x06003863 RID: 14435 RVA: 0x00114C37 File Offset: 0x00112E37
		public static bool TryGetCallingMod(out ModInfo Mod, out StackFrame Frame)
		{
			return ModManager.TryGetStackMod(new StackTrace(1), out Mod, out Frame);
		}

		/// <summary>Get the first mod encountered in the specified exception's stack.</summary>
		// Token: 0x06003864 RID: 14436 RVA: 0x00114C46 File Offset: 0x00112E46
		public static bool TryGetStackMod(Exception Exception, out ModInfo Mod, out StackFrame Frame)
		{
			return ModManager.TryGetStackMod(new StackTrace(Exception), out Mod, out Frame);
		}

		/// <summary>Get the first mod encountered in the specified stack.</summary>
		/// <remarks>Mostly for logging purposes when the ModInfo isn't readily accessible.</remarks>
		// Token: 0x06003865 RID: 14437 RVA: 0x00114C58 File Offset: 0x00112E58
		public static bool TryGetStackMod(StackTrace Trace, out ModInfo Mod, out StackFrame Frame)
		{
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				foreach (StackFrame stackFrame in Trace.GetFrames())
				{
					Assembly assembly = stackFrame.GetMethod().DeclaringType.Assembly;
					ModInfo modInfo;
					if (!(assembly == executingAssembly) && ModManager.ModAssemblyMap.TryGetValue(assembly, out modInfo))
					{
						Mod = modInfo;
						Frame = stackFrame;
						return true;
					}
				}
			}
			catch
			{
			}
			Mod = null;
			Frame = null;
			return false;
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x00114CDC File Offset: 0x00112EDC
		public static string GetModTitle(string ID)
		{
			ModInfo mod = ModManager.GetMod(ID);
			if (mod != null)
			{
				return mod.DisplayTitle;
			}
			ModSettings modSettings;
			if (ModManager.ModSettingsMap.TryGetValue(ID, out modSettings) && !modSettings.Title.IsNullOrEmpty())
			{
				return modSettings.Title;
			}
			return ID;
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x00114D20 File Offset: 0x00112F20
		public static void LogRunningMods()
		{
			string text = "Enabled mods: ";
			if (ModManager.Mods == null || ModManager.Mods.Count == 0)
			{
				text += "None";
			}
			else
			{
				text += string.Join(", ", from m in ModManager.Mods
					where m.IsEnabled
					select m.DisplayTitleStripped);
			}
			MetricsManager.LogInfo(text);
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x00114DB8 File Offset: 0x00112FB8
		public static IEnumerable<string> GetRunningMods()
		{
			if (ModManager.ActiveMods == null)
			{
				yield break;
			}
			foreach (ModInfo modInfo in ModManager.ActiveMods)
			{
				yield return modInfo.ID;
			}
			Container<ModInfo>.Enumerator enumerator = default(Container<ModInfo>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x00114DC1 File Offset: 0x00112FC1
		public static List<string> GetAvailableMods()
		{
			List<string> result = new List<string>();
			ModManager.ForEachMod(delegate(ModInfo mod)
			{
				result.Add(mod.ID);
			}, true);
			return result;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x00114DEC File Offset: 0x00112FEC
		public static bool IsScriptingUndetermined()
		{
			if (Options.Bag == null)
			{
				return false;
			}
			if (!Options.Bag.GetValue("OptionAllowCSMods", null).IsNullOrEmpty())
			{
				return false;
			}
			using (Container<ModInfo>.Enumerator enumerator = ModManager.Mods.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsScripting)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x00114E68 File Offset: 0x00113068
		public static bool IsAnyModFailed()
		{
			using (Container<ModInfo>.Enumerator enumerator = ModManager.Mods.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.State == ModState.Failed)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x00114EC4 File Offset: 0x001130C4
		public static bool IsAnyModMissingDependency()
		{
			using (Container<ModInfo>.Enumerator enumerator = ModManager.Mods.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.State == ModState.MissingDependency)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x00114F20 File Offset: 0x00113120
		public static bool HasActiveScripts()
		{
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				if (modInfo.Active && modInfo.IsScripting)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x00114F84 File Offset: 0x00113184
		public static void ForEachMod(Action<ModInfo> ModAction, bool IncludeDisabled = false)
		{
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				if (modInfo.Active || IncludeDisabled)
				{
					try
					{
						ModAction(modInfo);
					}
					catch (Exception ex)
					{
						modInfo.Error(DataManager.SanitizePathForDisplay(ex.ToString()));
					}
				}
			}
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x00115004 File Offset: 0x00113204
		public static void ForEveryFile(Action<string, ModInfo> FileAction, bool IncludeDisabled = false)
		{
			ModManager.ForEachMod(delegate(ModInfo mod)
			{
				if (!mod.Directory.Exists)
				{
					return;
				}
				foreach (Platform.IO.FileInfo fileInfo in mod.Directory.EnumerateFiles(null, Platform.IO.SearchOption.TopDirectoryOnly))
				{
					FileAction(fileInfo.FullName, mod);
				}
			}, IncludeDisabled);
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x00115023 File Offset: 0x00113223
		public static void ForEveryFileRecursive(Action<string, ModInfo> FileAction, string SearchPattern = "*.*", bool IncludeDisabled = false)
		{
			ModManager.ForEachMod(delegate(ModInfo mod)
			{
				if (!mod.Directory.Exists)
				{
					return;
				}
				foreach (string text in Platform.IO.Directory.EnumerateFiles(mod.Path, SearchPattern, Platform.IO.SearchOption.AllDirectories))
				{
					FileAction(text, mod);
				}
			}, IncludeDisabled);
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x0011504C File Offset: 0x0011324C
		public static void ForEachFile(string FileName, Action<string> FileAction, bool IncludeDisabled = false, bool Recursive = false)
		{
			ModManager.ForEachFile(FileName, delegate(string f, ModInfo i)
			{
				FileAction(f);
			}, IncludeDisabled, Recursive);
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x0011507C File Offset: 0x0011327C
		public static void ForEachFile(string FileName, Action<string, ModInfo> FileAction, bool IncludeDisabled = false, bool Recursive = false)
		{
			string text = FileName.ToLower();
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				if (modInfo.Active || IncludeDisabled)
				{
					foreach (ModFile modFile in modInfo.Files)
					{
						if ((Recursive ? modFile.Name : modFile.RelativeName) == text)
						{
							try
							{
								FileAction(modFile.OriginalName, modInfo);
								if (!Recursive)
								{
									break;
								}
							}
							catch (Exception ex)
							{
								modInfo.Error(DataManager.SanitizePathForDisplay(string.Concat(new string[]
								{
									modInfo.Path,
									"/",
									FileName,
									": ",
									ex.ToString()
								})));
							}
						}
					}
				}
			}
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x001151A0 File Offset: 0x001133A0
		public static void ForEachFileIn(string Subdirectory, Action<string, ModInfo> FileAction, bool bIncludeBase = false, bool bIncludeDisabled = false)
		{
			if (bIncludeBase)
			{
				ModManager._ForEachFileIn(DataManager.FilePath(Subdirectory), FileAction, null);
			}
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				if (bIncludeDisabled || modInfo.Active)
				{
					ModManager._ForEachFileIn(Platform.IO.Path.Combine(new string[] { modInfo.Path, Subdirectory }), FileAction, modInfo);
				}
			}
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x00115228 File Offset: 0x00113428
		private static void _ForEachFileIn(string Subdirectory, Action<string, ModInfo> FileAction, ModInfo mod)
		{
			if (!Platform.IO.Directory.Exists(Subdirectory))
			{
				return;
			}
			foreach (string text in Platform.IO.Directory.EnumerateFiles(Subdirectory))
			{
				try
				{
					FileAction(text, mod);
				}
				catch (Exception ex)
				{
					mod.Error(DataManager.SanitizePathForDisplay(Subdirectory + ": " + ex.ToString()));
				}
			}
			string[] directories = Platform.IO.Directory.GetDirectories(Subdirectory);
			for (int i = 0; i < directories.Length; i++)
			{
				ModManager._ForEachFileIn(directories[i], FileAction, mod);
			}
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x001152D0 File Offset: 0x001134D0
		public static object CreateInstance(string className)
		{
			Type type = ModManager.ResolveType(className, false, false, true);
			if (type == null)
			{
				throw new TypeLoadException("No class with name \"" + className + "\" could be found. A full name including namespaces is required.");
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x001152FF File Offset: 0x001134FF
		public static T CreateInstance<T>(string className) where T : class
		{
			return ModManager.CreateInstance(className) as T;
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x00115311 File Offset: 0x00113511
		public static T CreateInstance<T>(Type type) where T : class
		{
			return Activator.CreateInstance(type) as T;
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x00115324 File Offset: 0x00113524
		public static IEnumerable<Type> GetClassesWithAttribute(Type attributeToSearchFor, Type classFilterAttribute = null)
		{
			List<Type> list = new List<Type>();
			Func<Type, bool> <>9__1;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (assembly.FullName.Contains("Assembly-CSharp"))
				{
					List<Type> list2 = list;
					IEnumerable<Type> enumerable = assembly.GetTypes().Concat<Type>(assembly.GetTypes().SelectMany<Type, Type>((Type type) => type.GetNestedTypes()));
					Func<Type, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (Type m) => m.IsDefined(attributeToSearchFor, false) && m.IsClass);
					}
					list2.AddRange(enumerable.Where<Type>(func));
				}
			}
			return list;
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x001153DC File Offset: 0x001135DC
		public static List<T> GetInstancesWithAttribute<T>(Type attributeType) where T : class
		{
			List<T> list = new List<T>();
			foreach (Type type in ModManager.GetTypesWithAttribute(attributeType, true))
			{
				T t = Activator.CreateInstance(type) as T;
				if (t != null)
				{
					list.Add(t);
				}
			}
			return list;
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x00115450 File Offset: 0x00113650
		public static List<Type> GetTypesWithAttribute(Type AttributeType, bool Cache = true)
		{
			List<Type> list;
			if (Cache && ModManager._typesWithAttribute.TryGetValue(AttributeType, out list))
			{
				return list;
			}
			list = new List<Type>(32);
			foreach (Type type in ModManager.ActiveTypes)
			{
				if (type.IsDefined(AttributeType, true))
				{
					list.Add(type);
				}
			}
			if (Cache)
			{
				ModManager._typesWithAttribute[AttributeType] = list;
				list.TrimExcess();
			}
			return list;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x001154D8 File Offset: 0x001136D8
		public static List<MethodInfo> GetMethodsWithAttribute(Type AttributeType, Type ClassFilterType = null, bool Cache = true)
		{
			List<MethodInfo> list;
			if (ModManager._methodsWithAttribute.TryGetValue(new ValueTuple<Type, Type>(AttributeType, ClassFilterType), out list))
			{
				return list;
			}
			list = new List<MethodInfo>(128);
			IEnumerable<Type> enumerable;
			if (!(ClassFilterType == null))
			{
				IEnumerable<Type> typesWithAttribute = ModManager.GetTypesWithAttribute(ClassFilterType, Cache);
				enumerable = typesWithAttribute;
			}
			else
			{
				enumerable = ModManager.ActiveTypes;
			}
			foreach (Type type in enumerable)
			{
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.IsDefined(AttributeType, false))
					{
						list.Add(methodInfo);
					}
				}
			}
			if (Cache)
			{
				ModManager._methodsWithAttribute.Add(new ValueTuple<Type, Type>(AttributeType, ClassFilterType), list);
				list.TrimExcess();
			}
			return list;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x001155A4 File Offset: 0x001137A4
		public static List<FieldInfo> GetFieldsWithAttribute(Type AttributeType, Type ClassFilterType = null, bool Cache = true)
		{
			List<FieldInfo> list;
			if (Cache && ModManager._fieldsWithAttribute.TryGetValue(new ValueTuple<Type, Type>(AttributeType, ClassFilterType), out list))
			{
				return list;
			}
			list = new List<FieldInfo>(128);
			IEnumerable<Type> enumerable;
			if (!(ClassFilterType == null))
			{
				IEnumerable<Type> typesWithAttribute = ModManager.GetTypesWithAttribute(ClassFilterType, Cache);
				enumerable = typesWithAttribute;
			}
			else
			{
				enumerable = ModManager.ActiveTypes;
			}
			foreach (Type type in enumerable)
			{
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (fieldInfo.IsDefined(AttributeType, false))
					{
						list.Add(fieldInfo);
					}
				}
			}
			if (Cache)
			{
				ModManager._fieldsWithAttribute.Add(new ValueTuple<Type, Type>(AttributeType, ClassFilterType), list);
				list.TrimExcess();
			}
			return list;
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x00115670 File Offset: 0x00113870
		public static List<Type> GetTypesAssignableFrom(Type AssignableType, bool Cache = true)
		{
			List<Type> list;
			if (Cache && ModManager._assignableTypes.TryGetValue(AssignableType, out list))
			{
				return list;
			}
			list = new List<Type>(64);
			ModManager.GetTypesAssignableFrom(AssignableType, list);
			if (Cache)
			{
				ModManager._assignableTypes.Add(AssignableType, list);
				list.TrimExcess();
			}
			return list;
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x001156B8 File Offset: 0x001138B8
		public static void GetTypesAssignableFrom(Type AssignableType, List<Type> Result)
		{
			Result.Clear();
			foreach (Type type in ModManager.ActiveTypes)
			{
				if (AssignableType.IsAssignableFrom(type))
				{
					Result.Add(type);
				}
			}
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x00115714 File Offset: 0x00113914
		public static PublishedFileId_t[] GetPublishedFileIDs()
		{
			uint num = 0U;
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				if (modInfo.WorkshopInfo != null && modInfo.WorkshopInfo.WorkshopId != 0UL)
				{
					num += 1U;
				}
			}
			PublishedFileId_t[] array = new PublishedFileId_t[num];
			int num2 = 0;
			foreach (ModInfo modInfo2 in ModManager.Mods)
			{
				if (modInfo2.WorkshopInfo != null && modInfo2.WorkshopInfo.WorkshopId != 0UL)
				{
					array[num2++] = new PublishedFileId_t(modInfo2.WorkshopInfo.WorkshopId);
				}
			}
			return array;
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x001158C8 File Offset: 0x00113AC8
		[CompilerGenerated]
		internal static void <DetermineLoadOrderFast>g__Visit|50_0(ModInfo Mod, ref int Order)
		{
			if (Mod.LoadOrder == -2147483648)
			{
				Mod.LoadOrder = int.MaxValue;
				foreach (KeyValuePair<ModInfo, uint> keyValuePair in Mod.Dependencies)
				{
					ModInfo modInfo;
					uint num;
					keyValuePair.Deconstruct(out modInfo, out num);
					ModManager.<DetermineLoadOrderFast>g__Visit|50_0(modInfo, ref Order);
				}
				int num2 = Order;
				Order = num2 + 1;
				Mod.LoadOrder = num2;
				return;
			}
			if (Mod.LoadOrder == 2147483647)
			{
				throw new Exception("Cycle detected.");
			}
		}

		// Token: 0x04001C58 RID: 7256
		public static Rack<ModInfo> Mods = new Rack<ModInfo>();

		// Token: 0x04001C59 RID: 7257
		public static Rack<ModInfo> ActiveMods = new Rack<ModInfo>();

		// Token: 0x04001C5A RID: 7258
		public static Rack<Module> ActiveModules = new Rack<Module>(Assembly.GetExecutingAssembly().GetModules());

		// Token: 0x04001C5B RID: 7259
		public static Dictionary<string, ModInfo> ModMap = new Dictionary<string, ModInfo>();

		// Token: 0x04001C5C RID: 7260
		public static Dictionary<string, ModSettings> ModSettingsMap = new Dictionary<string, ModSettings>();

		// Token: 0x04001C5D RID: 7261
		public static Dictionary<Assembly, ModInfo> ModAssemblyMap = new Dictionary<Assembly, ModInfo>();

		// Token: 0x04001C5E RID: 7262
		public static Dictionary<ulong, ModInfo> ModWorkshopMap = new Dictionary<ulong, ModInfo>();

		/// <summary>
		/// Get enabled script mod assemblies in priority order.
		/// </summary>
		// Token: 0x04001C5F RID: 7263
		public static Rack<Assembly> ModAssemblies = new Rack<Assembly>();

		// Token: 0x04001C60 RID: 7264
		public static bool Initialized = false;

		// Token: 0x04001C61 RID: 7265
		public static JsonSerializer JsonSerializer = new JsonSerializer
		{
			Formatting = Formatting.Indented,
			NullValueHandling = NullValueHandling.Ignore
		};

		// Token: 0x04001C62 RID: 7266
		[ModSensitiveStaticCache(true)]
		private static Dictionary<Type, List<Type>> _typesWithAttribute = new Dictionary<Type, List<Type>>();

		// Token: 0x04001C63 RID: 7267
		[ModSensitiveStaticCache(true)]
		private static Dictionary<ValueTuple<Type, Type>, List<FieldInfo>> _fieldsWithAttribute = new Dictionary<ValueTuple<Type, Type>, List<FieldInfo>>();

		// Token: 0x04001C64 RID: 7268
		[ModSensitiveStaticCache(true)]
		private static Dictionary<ValueTuple<Type, Type>, List<MethodInfo>> _methodsWithAttribute = new Dictionary<ValueTuple<Type, Type>, List<MethodInfo>>();

		// Token: 0x04001C65 RID: 7269
		[ModSensitiveStaticCache(true)]
		private static Dictionary<Type, List<Type>> _assignableTypes = new Dictionary<Type, List<Type>>();

		// Token: 0x04001C66 RID: 7270
		[ModSensitiveStaticCache(true)]
		private static Dictionary<string, Type> _typeResolutions = new Dictionary<string, Type>();

		// Token: 0x04001C67 RID: 7271
		public static Dictionary<Type, string> typeNames = new Dictionary<Type, string>();

		// Token: 0x04001C68 RID: 7272
		private static Harmony harmony = new Harmony("com.freeholdgames.cavesofqud");

		// Token: 0x04001C69 RID: 7273
		public const BindingFlags ATTRIBUTE_FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
