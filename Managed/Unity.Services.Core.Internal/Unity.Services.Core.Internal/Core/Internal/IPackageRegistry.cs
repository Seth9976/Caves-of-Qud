using System;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200004C RID: 76
	internal interface IPackageRegistry
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013F RID: 319
		// (set) Token: 0x06000140 RID: 320
		[CanBeNull]
		DependencyTree Tree { get; set; }

		// Token: 0x06000141 RID: 321
		CoreRegistration RegisterPackage<TPackage>([NotNull] TPackage package) where TPackage : IInitializablePackage;

		// Token: 0x06000142 RID: 322
		void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent;

		// Token: 0x06000143 RID: 323
		void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent;

		// Token: 0x06000144 RID: 324
		void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : IServiceComponent;
	}
}
