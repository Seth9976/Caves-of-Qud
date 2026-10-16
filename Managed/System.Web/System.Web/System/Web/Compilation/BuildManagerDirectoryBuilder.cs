using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000636 RID: 1590
	internal sealed class BuildManagerDirectoryBuilder
	{
		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x000B981A File Offset: 0x000B7A1A
		private CompilationSection CompilationSection
		{
			get
			{
				if (this.compilationSection == null)
				{
					this.compilationSection = WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
				}
				return this.compilationSection;
			}
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x000B983F File Offset: 0x000B7A3F
		public BuildManagerDirectoryBuilder(VirtualPath virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			this.vpp = HostingEnvironment.VirtualPathProvider;
			this.virtualPath = virtualPath;
			this.virtualPathDirectory = VirtualPathUtility.GetDirectory(virtualPath.Absolute);
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x000B9878 File Offset: 0x000B7A78
		public List<BuildProviderGroup> Build(bool single)
		{
			if (StrUtils.StartsWith(this.virtualPath.AppRelative, "~/App_Themes/"))
			{
				ThemeDirectoryBuildProvider themeDirectoryBuildProvider = new ThemeDirectoryBuildProvider();
				themeDirectoryBuildProvider.SetVirtualPath(this.virtualPath);
				return this.GetSingleBuildProviderGroup(themeDirectoryBuildProvider);
			}
			CompilationSection compilationSection = this.CompilationSection;
			BuildProviderCollection buildProviderCollection = ((compilationSection != null) ? compilationSection.BuildProviders : null);
			if (buildProviderCollection == null || buildProviderCollection.Count == 0)
			{
				return null;
			}
			if (this.virtualPath.IsFake)
			{
				BuildProvider buildProvider = BuildManagerDirectoryBuilder.GetBuildProvider(this.virtualPath, buildProviderCollection);
				if (buildProvider == null)
				{
					return null;
				}
				return this.GetSingleBuildProviderGroup(buildProvider);
			}
			else
			{
				if (single)
				{
					this.AddVirtualFile(this.GetVirtualFile(this.virtualPath.Absolute), buildProviderCollection);
				}
				else
				{
					Dictionary<string, bool> dictionary = new Dictionary<string, bool>(RuntimeHelpers.StringEqualityComparer);
					this.AddVirtualDir(this.GetVirtualDirectory(this.virtualPath.Absolute), buildProviderCollection, dictionary);
					if (this.buildProviders == null || this.buildProviders.Count == 0)
					{
						this.AddVirtualFile(this.GetVirtualFile(this.virtualPath.Absolute), buildProviderCollection);
					}
				}
				if (this.buildProviders == null || this.buildProviders.Count == 0)
				{
					return null;
				}
				List<BuildProviderGroup> list = new List<BuildProviderGroup>();
				foreach (BuildProvider buildProvider2 in this.buildProviders.Values)
				{
					this.AssignToGroup(buildProvider2, list);
				}
				if (list == null || list.Count == 0)
				{
					list = null;
					return null;
				}
				list.Reverse();
				return list;
			}
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x000B99F8 File Offset: 0x000B7BF8
		private bool AddBuildProvider(BuildProvider buildProvider)
		{
			if (this.buildProviders == null)
			{
				this.buildProviders = new Dictionary<string, BuildProvider>(RuntimeHelpers.StringEqualityComparer);
			}
			string text = buildProvider.VirtualPath;
			if (this.buildProviders.ContainsKey(text))
			{
				return false;
			}
			this.buildProviders.Add(text, buildProvider);
			return true;
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x000B9A44 File Offset: 0x000B7C44
		private void AddVirtualDir(VirtualDirectory vdir, BuildProviderCollection bpcoll, Dictionary<string, bool> cache)
		{
			if (vdir == null)
			{
				return;
			}
			List<string> list = new List<string>();
			foreach (object obj in vdir.Files)
			{
				string text = ((VirtualFile)obj).VirtualPath;
				if (!BuildManager.IgnoreVirtualPath(text))
				{
					BuildProvider buildProvider = BuildManagerDirectoryBuilder.GetBuildProvider(text, bpcoll);
					if (buildProvider != null && this.AddBuildProvider(buildProvider))
					{
						IDictionary<string, bool> dictionary = buildProvider.ExtractDependencies();
						if (dictionary != null)
						{
							list.Clear();
							foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
							{
								string key = keyValuePair.Key;
								string directory = VirtualPathUtility.GetDirectory(key);
								if (!cache.ContainsKey(directory))
								{
									cache.Add(directory, true);
									this.AddVirtualDir(this.GetVirtualDirectory(key), bpcoll, cache);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x000B9B4C File Offset: 0x000B7D4C
		private void AddVirtualFile(VirtualFile file, BuildProviderCollection bpcoll)
		{
			if (file == null || BuildManager.IgnoreVirtualPath(file.VirtualPath))
			{
				return;
			}
			BuildProvider buildProvider = BuildManagerDirectoryBuilder.GetBuildProvider(file.VirtualPath, bpcoll);
			if (buildProvider == null)
			{
				return;
			}
			this.AddBuildProvider(buildProvider);
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x000B9B84 File Offset: 0x000B7D84
		private List<BuildProviderGroup> GetSingleBuildProviderGroup(BuildProvider bp)
		{
			List<BuildProviderGroup> list = new List<BuildProviderGroup>();
			BuildProviderGroup buildProviderGroup = new BuildProviderGroup();
			buildProviderGroup.AddProvider(bp);
			list.Add(buildProviderGroup);
			return list;
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x000B9BAA File Offset: 0x000B7DAA
		private VirtualDirectory GetVirtualDirectory(string virtualPath)
		{
			if (!this.vpp.DirectoryExists(VirtualPathUtility.GetDirectory(virtualPath)))
			{
				return null;
			}
			return this.vpp.GetDirectory(virtualPath);
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000B9BCD File Offset: 0x000B7DCD
		private VirtualFile GetVirtualFile(string virtualPath)
		{
			if (!this.vpp.FileExists(virtualPath))
			{
				return null;
			}
			return this.vpp.GetFile(virtualPath);
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x000B9BEC File Offset: 0x000B7DEC
		private Type GetBuildProviderCodeDomType(BuildProvider bp)
		{
			CompilerType compilerType = bp.CodeCompilerType;
			if (compilerType == null)
			{
				string text = bp.LanguageName;
				if (string.IsNullOrEmpty(text))
				{
					text = this.CompilationSection.DefaultLanguage;
				}
				compilerType = BuildManager.GetDefaultCompilerTypeForLanguage(text, this.CompilationSection, false);
			}
			Type type = ((compilerType != null) ? compilerType.CodeDomProviderType : null);
			if (type == null)
			{
				throw new HttpException("Unable to determine code compilation language provider for virtual path '" + bp.VirtualPath + "'.");
			}
			return type;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x000B9C5C File Offset: 0x000B7E5C
		private void AssignToGroup(BuildProvider buildProvider, List<BuildProviderGroup> groups)
		{
			if (this.IsDependencyCycle(buildProvider))
			{
				throw new HttpException("Dependency cycles are not suppported: " + buildProvider.VirtualPath);
			}
			BuildProviderGroup buildProviderGroup = null;
			string directory = VirtualPathUtility.GetDirectory(buildProvider.VirtualPath);
			if (BuildManager.HasCachedItemNoLock(buildProvider.VirtualPath))
			{
				return;
			}
			StringComparison stringComparison = RuntimeHelpers.StringComparison;
			if (buildProvider is ApplicationFileBuildProvider || buildProvider is ThemeDirectoryBuildProvider)
			{
				buildProviderGroup = new BuildProviderGroup();
				buildProviderGroup.Standalone = true;
				this.InsertGroup(buildProviderGroup, groups);
			}
			else
			{
				Type buildProviderCodeDomType = this.GetBuildProviderCodeDomType(buildProvider);
				foreach (BuildProviderGroup buildProviderGroup2 in groups)
				{
					if (!buildProviderGroup2.Standalone)
					{
						if (buildProviderGroup2.Count == 0)
						{
							buildProviderGroup = buildProviderGroup2;
							break;
						}
						bool flag = true;
						foreach (BuildProvider buildProvider2 in buildProviderGroup2)
						{
							if (this.IsDependency(buildProvider, buildProvider2))
							{
								flag = false;
								break;
							}
							if (string.Compare(directory, VirtualPathUtility.GetDirectory(buildProvider2.VirtualPath), stringComparison) != 0)
							{
								flag = false;
								break;
							}
							if (buildProviderCodeDomType != null)
							{
								Type buildProviderCodeDomType2 = this.GetBuildProviderCodeDomType(buildProvider2);
								if (buildProviderCodeDomType2 != null && buildProviderCodeDomType2 != buildProviderCodeDomType)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							buildProviderGroup = buildProviderGroup2;
							break;
						}
					}
				}
				if (buildProviderGroup == null)
				{
					buildProviderGroup = new BuildProviderGroup();
					this.InsertGroup(buildProviderGroup, groups);
				}
			}
			buildProviderGroup.AddProvider(buildProvider);
			if (string.Compare(directory, this.virtualPathDirectory, stringComparison) == 0)
			{
				buildProviderGroup.Master = true;
			}
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x000B9E04 File Offset: 0x000B8004
		private void InsertGroup(BuildProviderGroup group, List<BuildProviderGroup> groups)
		{
			if (group.Application)
			{
				groups.Insert(groups.Count - 1, group);
				return;
			}
			int num;
			if (group.Standalone)
			{
				num = groups.FindLastIndex(new Predicate<BuildProviderGroup>(BuildManagerDirectoryBuilder.SkipApplicationGroup));
			}
			else
			{
				num = groups.FindLastIndex(new Predicate<BuildProviderGroup>(BuildManagerDirectoryBuilder.SkipStandaloneGroups));
			}
			if (num == -1)
			{
				groups.Add(group);
				return;
			}
			groups.Insert((num == 0) ? 0 : (num - 1), group);
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x000B9E75 File Offset: 0x000B8075
		private static bool SkipStandaloneGroups(BuildProviderGroup group)
		{
			return group != null && group.Standalone;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x000B9E82 File Offset: 0x000B8082
		private static bool SkipApplicationGroup(BuildProviderGroup group)
		{
			return group != null && group.Application;
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x000B9E90 File Offset: 0x000B8090
		private bool IsDependency(BuildProvider bp1, BuildProvider bp2)
		{
			IDictionary<string, bool> dictionary = bp1.ExtractDependencies();
			if (dictionary == null)
			{
				return false;
			}
			if (dictionary.ContainsKey(bp2.VirtualPath))
			{
				return true;
			}
			foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
			{
				BuildProvider buildProvider;
				if (this.buildProviders.TryGetValue(keyValuePair.Key, out buildProvider) && this.IsDependency(buildProvider, bp2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000B9F18 File Offset: 0x000B8118
		private bool IsDependencyCycle(BuildProvider buildProvider)
		{
			return this.IsDependencyCycle(new Dictionary<BuildProvider, bool> { { buildProvider, true } }, buildProvider.ExtractDependencies());
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x000B9F40 File Offset: 0x000B8140
		private bool IsDependencyCycle(Dictionary<BuildProvider, bool> cache, IDictionary<string, bool> deps)
		{
			if (deps == null)
			{
				return false;
			}
			foreach (KeyValuePair<string, bool> keyValuePair in deps)
			{
				BuildProvider buildProvider;
				if (this.buildProviders.TryGetValue(keyValuePair.Key, out buildProvider))
				{
					if (cache.ContainsKey(buildProvider))
					{
						return true;
					}
					cache.Add(buildProvider, true);
					if (this.IsDependencyCycle(cache, buildProvider.ExtractDependencies()))
					{
						return true;
					}
					cache.Remove(buildProvider);
				}
			}
			return false;
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x000B9FD0 File Offset: 0x000B81D0
		public static BuildProvider GetBuildProvider(string virtualPath, BuildProviderCollection coll)
		{
			return BuildManagerDirectoryBuilder.GetBuildProvider(new VirtualPath(virtualPath), coll);
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x000B9FE0 File Offset: 0x000B81E0
		public static BuildProvider GetBuildProvider(VirtualPath virtualPath, BuildProviderCollection coll)
		{
			if (virtualPath == null || string.IsNullOrEmpty(virtualPath.Original) || coll == null)
			{
				return null;
			}
			string extension = virtualPath.Extension;
			BuildProvider buildProvider = coll.GetProviderInstanceForExtension(extension);
			if (buildProvider == null)
			{
				if (string.Compare(extension, ".asax", StringComparison.OrdinalIgnoreCase) == 0)
				{
					buildProvider = new ApplicationFileBuildProvider();
				}
				else if (StrUtils.StartsWith(virtualPath.AppRelative, "~/App_Themes/"))
				{
					buildProvider = new ThemeDirectoryBuildProvider();
				}
				if (buildProvider != null)
				{
					buildProvider.SetVirtualPath(virtualPath);
				}
				return buildProvider;
			}
			object[] customAttributes = buildProvider.GetType().GetCustomAttributes(typeof(BuildProviderAppliesToAttribute), true);
			if (customAttributes != null && customAttributes.Length != 0 && (((BuildProviderAppliesToAttribute)customAttributes[0]).AppliesTo & BuildProviderAppliesTo.Web) == (BuildProviderAppliesTo)0)
			{
				return null;
			}
			buildProvider.SetVirtualPath(virtualPath);
			return buildProvider;
		}

		// Token: 0x040024A7 RID: 9383
		private readonly VirtualPath virtualPath;

		// Token: 0x040024A8 RID: 9384
		private readonly string virtualPathDirectory;

		// Token: 0x040024A9 RID: 9385
		private CompilationSection compilationSection;

		// Token: 0x040024AA RID: 9386
		private Dictionary<string, BuildProvider> buildProviders;

		// Token: 0x040024AB RID: 9387
		private VirtualPathProvider vpp;

		// Token: 0x02000637 RID: 1591
		private sealed class BuildProviderItem
		{
			// Token: 0x0600448E RID: 17550 RVA: 0x000BA087 File Offset: 0x000B8287
			public BuildProviderItem(BuildProvider bp, int listIndex, int parentIndex)
			{
				this.Provider = bp;
				this.ListIndex = listIndex;
				this.ParentIndex = parentIndex;
			}

			// Token: 0x040024AC RID: 9388
			public BuildProvider Provider;

			// Token: 0x040024AD RID: 9389
			public int ListIndex;

			// Token: 0x040024AE RID: 9390
			public int ParentIndex;
		}
	}
}
