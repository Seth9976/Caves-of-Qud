using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000004 RID: 4
	internal class DiagnosticsFactory : IDiagnosticsFactory, IServiceComponent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020DB File Offset: 0x000002DB
		public IReadOnlyDictionary<string, string> CommonTags { get; } = new Dictionary<string, string>();

		// Token: 0x06000007 RID: 7 RVA: 0x000020E3 File Offset: 0x000002E3
		public IDiagnostics Create(string packageName)
		{
			return new Diagnostics();
		}
	}
}
