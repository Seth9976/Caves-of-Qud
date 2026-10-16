using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200004E RID: 78
	internal class PackageRegistry : IPackageRegistry
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003872 File Offset: 0x00001A72
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000387A File Offset: 0x00001A7A
		public DependencyTree Tree { get; set; }

		// Token: 0x0600014F RID: 335 RVA: 0x00003883 File Offset: 0x00001A83
		public PackageRegistry([CanBeNull] DependencyTree tree)
		{
			this.Tree = tree;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003894 File Offset: 0x00001A94
		public CoreRegistration RegisterPackage<TPackage>(TPackage package) where TPackage : IInitializablePackage
		{
			int hashCode = typeof(TPackage).GetHashCode();
			this.Tree.PackageTypeHashToInstance[hashCode] = package;
			this.Tree.PackageTypeHashToComponentTypeHashDependencies[hashCode] = new List<int>();
			return new CoreRegistration(this, hashCode);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000038E8 File Offset: 0x00001AE8
		public void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			Type typeFromHandle = typeof(TComponent);
			int hashCode = typeFromHandle.GetHashCode();
			this.Tree.ComponentTypeHashToInstance[hashCode] = new MissingComponent(typeFromHandle);
			this.AddComponentDependencyToPackage(hashCode, packageTypeHash);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003928 File Offset: 0x00001B28
		public void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			int hashCode = typeof(TComponent).GetHashCode();
			if (!this.Tree.ComponentTypeHashToInstance.ContainsKey(hashCode))
			{
				this.Tree.ComponentTypeHashToInstance[hashCode] = null;
			}
			this.AddComponentDependencyToPackage(hashCode, packageTypeHash);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00003974 File Offset: 0x00001B74
		public void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			int hashCode = typeof(TComponent).GetHashCode();
			this.Tree.ComponentTypeHashToPackageTypeHash[hashCode] = packageTypeHash;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000039A4 File Offset: 0x00001BA4
		private void AddComponentDependencyToPackage(int componentTypeHash, int packageTypeHash)
		{
			List<int> list = this.Tree.PackageTypeHashToComponentTypeHashDependencies[packageTypeHash];
			if (!list.Contains(componentTypeHash))
			{
				list.Add(componentTypeHash);
			}
		}
	}
}
