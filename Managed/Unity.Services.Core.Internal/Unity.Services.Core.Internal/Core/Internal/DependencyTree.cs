using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000040 RID: 64
	internal class DependencyTree
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00002FF3 File Offset: 0x000011F3
		internal DependencyTree()
			: this(new Dictionary<int, IInitializablePackage>(), new Dictionary<int, int>(), new Dictionary<int, List<int>>(), new Dictionary<int, IServiceComponent>())
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000300F File Offset: 0x0000120F
		internal DependencyTree(Dictionary<int, IInitializablePackage> packageToInstance, Dictionary<int, int> componentToPackage, Dictionary<int, List<int>> packageToComponentDependencies, Dictionary<int, IServiceComponent> componentToInstance)
		{
			this.PackageTypeHashToInstance = packageToInstance;
			this.ComponentTypeHashToPackageTypeHash = componentToPackage;
			this.PackageTypeHashToComponentTypeHashDependencies = packageToComponentDependencies;
			this.ComponentTypeHashToInstance = componentToInstance;
		}

		// Token: 0x04000047 RID: 71
		public readonly Dictionary<int, IInitializablePackage> PackageTypeHashToInstance;

		// Token: 0x04000048 RID: 72
		public readonly Dictionary<int, int> ComponentTypeHashToPackageTypeHash;

		// Token: 0x04000049 RID: 73
		public readonly Dictionary<int, List<int>> PackageTypeHashToComponentTypeHashDependencies;

		// Token: 0x0400004A RID: 74
		public readonly Dictionary<int, IServiceComponent> ComponentTypeHashToInstance;
	}
}
