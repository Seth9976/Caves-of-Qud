using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003F RID: 63
	internal class CoreRegistryInitializer
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00002F9A File Offset: 0x0000119A
		public CoreRegistryInitializer([NotNull] CoreRegistry registry, [NotNull] List<int> sortedPackageTypeHashes)
		{
			this.m_Registry = registry;
			this.m_SortedPackageTypeHashes = sortedPackageTypeHashes;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00002FB0 File Offset: 0x000011B0
		public async Task<List<PackageInitializationInfo>> InitializeRegistryAsync()
		{
			CoreRegistryInitializer.<>c__DisplayClass3_0 CS$<>8__locals1 = new CoreRegistryInitializer.<>c__DisplayClass3_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.packagesInitInfos = new List<PackageInitializationInfo>(this.m_SortedPackageTypeHashes.Count);
			List<PackageInitializationInfo> list;
			if (this.m_SortedPackageTypeHashes.Count <= 0)
			{
				list = CS$<>8__locals1.packagesInitInfos;
			}
			else
			{
				CS$<>8__locals1.dependencyTree = this.m_Registry.PackageRegistry.Tree;
				if (CS$<>8__locals1.dependencyTree == null)
				{
					NullReferenceException ex = new NullReferenceException("Registry requires a valid dependency tree to be initialized.");
					throw new ServicesInitializationException("Registry is in an invalid state (dependency tree is null) and can't be initialized.", ex);
				}
				this.m_Registry.ComponentRegistry.ResetProvidedComponents(CS$<>8__locals1.dependencyTree.ComponentTypeHashToInstance);
				CS$<>8__locals1.failureReasons = new List<Exception>(this.m_SortedPackageTypeHashes.Count);
				CS$<>8__locals1.stopwatch = new Stopwatch();
				for (int i = 0; i < this.m_SortedPackageTypeHashes.Count; i++)
				{
					IInitializablePackage initializablePackage = CS$<>8__locals1.<InitializeRegistryAsync>g__GetPackageAt|1(i);
					await CS$<>8__locals1.<InitializeRegistryAsync>g__TryInitializePackageAsync|0(initializablePackage);
				}
				if (CS$<>8__locals1.failureReasons.Count > 0)
				{
					CS$<>8__locals1.<InitializeRegistryAsync>g__Fail|3();
				}
				list = CS$<>8__locals1.packagesInitInfos;
			}
			return list;
		}

		// Token: 0x04000045 RID: 69
		[NotNull]
		private readonly CoreRegistry m_Registry;

		// Token: 0x04000046 RID: 70
		[NotNull]
		private readonly List<int> m_SortedPackageTypeHashes;
	}
}
