using System;

namespace System.Web
{
	// Token: 0x020000DF RID: 223
	internal sealed class ControlTraceData
	{
		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001F79E File Offset: 0x0001D99E
		public ControlTraceData(string controlId, Type type, int renderSize, int viewstateSize, int controlstateSize, int depth)
		{
			this.ControlId = controlId;
			this.Type = type;
			this.RenderSize = renderSize;
			this.ViewstateSize = viewstateSize;
			this.Depth = depth;
			this.ControlstateSize = controlstateSize;
		}

		// Token: 0x040010D2 RID: 4306
		public string ControlId;

		// Token: 0x040010D3 RID: 4307
		public Type Type;

		// Token: 0x040010D4 RID: 4308
		public int RenderSize;

		// Token: 0x040010D5 RID: 4309
		public int ViewstateSize;

		// Token: 0x040010D6 RID: 4310
		public int Depth;

		// Token: 0x040010D7 RID: 4311
		public int ControlstateSize;
	}
}
