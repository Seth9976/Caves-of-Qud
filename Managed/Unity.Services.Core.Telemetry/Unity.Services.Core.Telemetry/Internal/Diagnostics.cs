using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000003 RID: 3
	internal class Diagnostics : IDiagnostics
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		internal IDictionary<string, string> PackageTags { get; } = new Dictionary<string, string>();

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public void SendDiagnostic(string name, string message, IDictionary<string, string> tags = null)
		{
		}
	}
}
