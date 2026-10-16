using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020009FF RID: 2559
	public class IncrementingEventCounter : DiagnosticCounter
	{
		// Token: 0x06005B49 RID: 23369 RVA: 0x001344A8 File Offset: 0x001326A8
		public IncrementingEventCounter(string name, EventSource eventSource)
			: base(name, eventSource)
		{
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Increment(double increment = 1.0)
		{
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x00134967 File Offset: 0x00132B67
		// (set) Token: 0x06005B4C RID: 23372 RVA: 0x0013496F File Offset: 0x00132B6F
		public TimeSpan DisplayRateTimeScale { get; set; }
	}
}
