using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020009EF RID: 2543
	public class EventCounter : DiagnosticCounter
	{
		// Token: 0x06005ABC RID: 23228 RVA: 0x001344A8 File Offset: 0x001326A8
		public EventCounter(string name, EventSource eventSource)
			: base(name, eventSource)
		{
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void WriteMetric(float value)
		{
		}

		// Token: 0x06005ABE RID: 23230 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void WriteMetric(double value)
		{
		}
	}
}
