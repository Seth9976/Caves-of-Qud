using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000014 RID: 20
	public interface IDiagnostics
	{
		// Token: 0x06000027 RID: 39
		void SendDiagnostic(string name, string message, IDictionary<string, string> tags = null);
	}
}
