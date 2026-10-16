using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000016 RID: 22
	public interface IDiagnosticsFactory : IServiceComponent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002A RID: 42
		IReadOnlyDictionary<string, string> CommonTags { get; }

		// Token: 0x0600002B RID: 43
		IDiagnostics Create(string packageName);
	}
}
