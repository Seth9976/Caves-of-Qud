using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Device.Internal
{
	// Token: 0x02000022 RID: 34
	public interface IInstallationId : IServiceComponent
	{
		// Token: 0x06000062 RID: 98
		string GetOrCreateIdentifier();
	}
}
