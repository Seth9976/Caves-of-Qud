using System;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200004D RID: 77
	internal class LockedPackageRegistry : IPackageRegistry
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003810 File Offset: 0x00001A10
		[NotNull]
		internal IPackageRegistry Registry { get; }

		// Token: 0x06000146 RID: 326 RVA: 0x00003818 File Offset: 0x00001A18
		public LockedPackageRegistry([NotNull] IPackageRegistry registryToLock)
		{
			this.Registry = registryToLock;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003827 File Offset: 0x00001A27
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00003834 File Offset: 0x00001A34
		public DependencyTree Tree
		{
			get
			{
				return this.Registry.Tree;
			}
			set
			{
				this.Registry.Tree = value;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00003842 File Offset: 0x00001A42
		public CoreRegistration RegisterPackage<TPackage>(TPackage package) where TPackage : IInitializablePackage
		{
			throw new InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000384E File Offset: 0x00001A4E
		public void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			throw new InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000385A File Offset: 0x00001A5A
		public void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			throw new InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00003866 File Offset: 0x00001A66
		public void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			throw new InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		// Token: 0x04000052 RID: 82
		private const string k_ErrorMessage = "Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].";
	}
}
