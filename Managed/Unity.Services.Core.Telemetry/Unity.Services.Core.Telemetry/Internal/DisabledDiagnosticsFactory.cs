using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000006 RID: 6
	internal class DisabledDiagnosticsFactory : IDiagnosticsFactory, IServiceComponent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002107 File Offset: 0x00000307
		IReadOnlyDictionary<string, string> IDiagnosticsFactory.CommonTags { get; } = new Dictionary<string, string>();

		// Token: 0x0600000C RID: 12 RVA: 0x0000210F File Offset: 0x0000030F
		IDiagnostics IDiagnosticsFactory.Create(string packageName)
		{
			return new DisabledDiagnostics();
		}
	}
}
