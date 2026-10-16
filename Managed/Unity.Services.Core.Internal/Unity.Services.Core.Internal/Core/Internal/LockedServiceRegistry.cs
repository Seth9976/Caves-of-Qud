using System;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000050 RID: 80
	internal class LockedServiceRegistry : IServiceRegistry
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000039D3 File Offset: 0x00001BD3
		[NotNull]
		internal IServiceRegistry Registry { get; }

		// Token: 0x06000158 RID: 344 RVA: 0x000039DB File Offset: 0x00001BDB
		public LockedServiceRegistry([NotNull] IServiceRegistry registryToLock)
		{
			this.Registry = registryToLock;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000039EA File Offset: 0x00001BEA
		public void RegisterService<T>(T service)
		{
			throw new InvalidOperationException("Service registration has been locked. Make sure to register service services before all packages have finished initializing.");
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000039F6 File Offset: 0x00001BF6
		public T GetService<T>()
		{
			return this.Registry.GetService<T>();
		}

		// Token: 0x04000055 RID: 85
		private const string k_ErrorMessage = "Service registration has been locked. Make sure to register service services before all packages have finished initializing.";
	}
}
