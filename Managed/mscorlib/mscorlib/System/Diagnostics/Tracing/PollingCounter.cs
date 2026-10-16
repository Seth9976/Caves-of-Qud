using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A01 RID: 2561
	public class PollingCounter : DiagnosticCounter
	{
		// Token: 0x06005B50 RID: 23376 RVA: 0x001344A8 File Offset: 0x001326A8
		public PollingCounter(string name, EventSource eventSource, Func<double> metricProvider)
			: base(name, eventSource)
		{
		}
	}
}
