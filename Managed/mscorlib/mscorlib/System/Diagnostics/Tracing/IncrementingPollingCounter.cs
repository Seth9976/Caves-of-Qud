using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A00 RID: 2560
	public class IncrementingPollingCounter : DiagnosticCounter
	{
		// Token: 0x06005B4D RID: 23373 RVA: 0x001344A8 File Offset: 0x001326A8
		public IncrementingPollingCounter(string name, EventSource eventSource, Func<double> totalValueProvider)
			: base(name, eventSource)
		{
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x00134978 File Offset: 0x00132B78
		// (set) Token: 0x06005B4F RID: 23375 RVA: 0x00134980 File Offset: 0x00132B80
		public TimeSpan DisplayRateTimeScale { get; set; }
	}
}
