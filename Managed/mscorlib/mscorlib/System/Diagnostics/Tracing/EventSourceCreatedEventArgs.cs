using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020009FA RID: 2554
	public class EventSourceCreatedEventArgs : EventArgs
	{
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x00134875 File Offset: 0x00132A75
		// (set) Token: 0x06005B20 RID: 23328 RVA: 0x0013487D File Offset: 0x00132A7D
		public EventSource EventSource { get; internal set; }
	}
}
