using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020009FE RID: 2558
	public abstract class DiagnosticCounter : IDisposable
	{
		// Token: 0x06005B3F RID: 23359 RVA: 0x0000259F File Offset: 0x0000079F
		internal DiagnosticCounter(string name, EventSource eventSource)
		{
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x0000259F File Offset: 0x0000079F
		internal DiagnosticCounter()
		{
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x00134935 File Offset: 0x00132B35
		// (set) Token: 0x06005B42 RID: 23362 RVA: 0x0013493D File Offset: 0x00132B3D
		public string DisplayName { get; set; }

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x00134946 File Offset: 0x00132B46
		// (set) Token: 0x06005B44 RID: 23364 RVA: 0x0013494E File Offset: 0x00132B4E
		public string DisplayUnits { get; set; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06005B45 RID: 23365 RVA: 0x00134957 File Offset: 0x00132B57
		public EventSource EventSource { get; }

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0013495F File Offset: 0x00132B5F
		public string Name { get; }

		// Token: 0x06005B47 RID: 23367 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void AddMetadata(string key, string value)
		{
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dispose()
		{
		}
	}
}
