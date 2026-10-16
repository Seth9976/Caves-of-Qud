using System;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200004F RID: 79
	internal interface IServiceRegistry
	{
		// Token: 0x06000155 RID: 341
		void RegisterService<T>([NotNull] T service);

		// Token: 0x06000156 RID: 342
		T GetService<T>();
	}
}
