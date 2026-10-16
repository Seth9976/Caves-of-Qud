using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003A RID: 58
	internal interface IComponentRegistry
	{
		// Token: 0x060000EB RID: 235
		void RegisterServiceComponent<TComponent>([NotNull] TComponent component) where TComponent : IServiceComponent;

		// Token: 0x060000EC RID: 236
		TComponent GetServiceComponent<TComponent>() where TComponent : IServiceComponent;

		// Token: 0x060000ED RID: 237
		bool TryGetServiceComponent<TComponent>(out TComponent component) where TComponent : IServiceComponent;

		// Token: 0x060000EE RID: 238
		void ResetProvidedComponents(IDictionary<int, IServiceComponent> componentTypeHashToInstance);
	}
}
