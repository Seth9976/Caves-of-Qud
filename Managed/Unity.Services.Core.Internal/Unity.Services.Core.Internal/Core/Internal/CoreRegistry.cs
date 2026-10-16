using System;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003E RID: 62
	public sealed class CoreRegistry
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002E2B File Offset: 0x0000102B
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00002E32 File Offset: 0x00001032
		public static CoreRegistry Instance { get; internal set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002E3A File Offset: 0x0000103A
		public string InstanceId { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002E42 File Offset: 0x00001042
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00002E4A File Offset: 0x0000104A
		internal ServicesType Type { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002E53 File Offset: 0x00001053
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00002E5B File Offset: 0x0000105B
		internal InitializationOptions Options { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002E64 File Offset: 0x00001064
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00002E6C File Offset: 0x0000106C
		[NotNull]
		internal IPackageRegistry PackageRegistry { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002E75 File Offset: 0x00001075
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00002E7D File Offset: 0x0000107D
		[NotNull]
		internal IComponentRegistry ComponentRegistry { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00002E86 File Offset: 0x00001086
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00002E8E File Offset: 0x0000108E
		[NotNull]
		internal IServiceRegistry ServiceRegistry { get; private set; }

		// Token: 0x0600010E RID: 270 RVA: 0x00002E97 File Offset: 0x00001097
		internal CoreRegistry()
		{
			this.Type = ServicesType.Default;
			this.InstanceId = null;
			this.PackageRegistry = new PackageRegistry(new DependencyTree());
			this.ComponentRegistry = new ComponentRegistry();
			this.ServiceRegistry = new ServiceRegistry();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00002ED3 File Offset: 0x000010D3
		internal CoreRegistry(IPackageRegistry packageRegistry, ServicesType type = ServicesType.Default, string instanceId = null)
		{
			this.Type = type;
			this.InstanceId = instanceId;
			this.PackageRegistry = packageRegistry;
			this.ComponentRegistry = new ComponentRegistry();
			this.ServiceRegistry = new ServiceRegistry();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00002F06 File Offset: 0x00001106
		public CoreRegistration RegisterPackage<TPackage>([NotNull] TPackage package) where TPackage : IInitializablePackage
		{
			return this.PackageRegistry.RegisterPackage<TPackage>(package);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00002F14 File Offset: 0x00001114
		public void RegisterServiceComponent<TComponent>([NotNull] TComponent component) where TComponent : IServiceComponent
		{
			this.ComponentRegistry.RegisterServiceComponent<TComponent>(component);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00002F22 File Offset: 0x00001122
		public TComponent GetServiceComponent<TComponent>() where TComponent : IServiceComponent
		{
			return this.ComponentRegistry.GetServiceComponent<TComponent>();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002F2F File Offset: 0x0000112F
		public bool TryGetServiceComponent<TComponent>(out TComponent component) where TComponent : IServiceComponent
		{
			return this.ComponentRegistry.TryGetServiceComponent<TComponent>(out component);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002F3D File Offset: 0x0000113D
		public void RegisterService<T>([NotNull] T service)
		{
			this.ServiceRegistry.RegisterService<T>(service);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002F4B File Offset: 0x0000114B
		public T GetService<T>()
		{
			return this.ServiceRegistry.GetService<T>();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002F58 File Offset: 0x00001158
		internal void LockComponentRegistration()
		{
			if (this.ComponentRegistry is LockedComponentRegistry)
			{
				return;
			}
			this.ComponentRegistry = new LockedComponentRegistry(this.ComponentRegistry);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002F79 File Offset: 0x00001179
		internal void LockServiceRegistration()
		{
			if (this.ServiceRegistry is LockedServiceRegistry)
			{
				return;
			}
			this.ServiceRegistry = new LockedServiceRegistry(this.ServiceRegistry);
		}
	}
}
