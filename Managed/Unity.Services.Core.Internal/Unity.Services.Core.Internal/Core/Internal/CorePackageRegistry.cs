using System;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003C RID: 60
	public sealed class CorePackageRegistry
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002D5A File Offset: 0x00000F5A
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00002D61 File Offset: 0x00000F61
		public static CorePackageRegistry Instance { get; internal set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002D69 File Offset: 0x00000F69
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00002D71 File Offset: 0x00000F71
		internal IPackageRegistry Registry { get; set; }

		// Token: 0x060000F9 RID: 249 RVA: 0x00002D7A File Offset: 0x00000F7A
		internal CorePackageRegistry()
		{
			this.Registry = new PackageRegistry(new DependencyTree());
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002D92 File Offset: 0x00000F92
		internal CorePackageRegistry(IPackageRegistry registry)
		{
			this.Registry = registry;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002DA1 File Offset: 0x00000FA1
		public CoreRegistration Register<TPackage>([NotNull] TPackage package) where TPackage : IInitializablePackage
		{
			return this.Registry.RegisterPackage<TPackage>(package);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002DAF File Offset: 0x00000FAF
		internal void Lock()
		{
			if (this.Registry is LockedPackageRegistry)
			{
				return;
			}
			this.Registry = new LockedPackageRegistry(this.Registry);
		}
	}
}
