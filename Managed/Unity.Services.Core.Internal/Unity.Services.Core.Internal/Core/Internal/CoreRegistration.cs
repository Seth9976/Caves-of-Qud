using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003D RID: 61
	public readonly struct CoreRegistration
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00002DD0 File Offset: 0x00000FD0
		internal CoreRegistration(IPackageRegistry registry, int packageHash)
		{
			this.m_Registry = registry;
			this.m_PackageHash = packageHash;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public CoreRegistration DependsOn<T>() where T : IServiceComponent
		{
			this.m_Registry.RegisterDependency<T>(this.m_PackageHash);
			return this;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public CoreRegistration OptionallyDependsOn<T>() where T : IServiceComponent
		{
			this.m_Registry.RegisterOptionalDependency<T>(this.m_PackageHash);
			return this;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002E12 File Offset: 0x00001012
		public CoreRegistration ProvidesComponent<T>() where T : IServiceComponent
		{
			this.m_Registry.RegisterProvision<T>(this.m_PackageHash);
			return this;
		}

		// Token: 0x0400003C RID: 60
		private readonly IPackageRegistry m_Registry;

		// Token: 0x0400003D RID: 61
		private readonly int m_PackageHash;
	}
}
