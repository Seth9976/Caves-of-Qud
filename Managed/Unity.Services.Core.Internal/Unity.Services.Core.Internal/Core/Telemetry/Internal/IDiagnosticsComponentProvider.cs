using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000015 RID: 21
	internal interface IDiagnosticsComponentProvider
	{
		// Token: 0x06000028 RID: 40
		Task<IDiagnosticsFactory> CreateDiagnosticsComponents();

		// Token: 0x06000029 RID: 41
		Task<string> GetSerializedProjectConfigurationAsync();
	}
}
