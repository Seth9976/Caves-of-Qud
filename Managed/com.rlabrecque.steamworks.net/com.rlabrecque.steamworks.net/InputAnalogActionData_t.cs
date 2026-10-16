using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000161 RID: 353
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct InputAnalogActionData_t
	{
		// Token: 0x04000A35 RID: 2613
		public EInputSourceMode eMode;

		// Token: 0x04000A36 RID: 2614
		public float x;

		// Token: 0x04000A37 RID: 2615
		public float y;

		// Token: 0x04000A38 RID: 2616
		public byte bActive;
	}
}
