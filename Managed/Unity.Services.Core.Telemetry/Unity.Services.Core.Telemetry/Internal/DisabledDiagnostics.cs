using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000005 RID: 5
	internal class DisabledDiagnostics : IDiagnostics
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020FD File Offset: 0x000002FD
		void IDiagnostics.SendDiagnostic(string name, string message, IDictionary<string, string> tags)
		{
		}
	}
}
