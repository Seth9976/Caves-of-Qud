using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020005F1 RID: 1521
	[StructLayout(LayoutKind.Sequential)]
	internal class SecChannelBindings
	{
		// Token: 0x04001BD0 RID: 7120
		internal int dwInitiatorAddrType;

		// Token: 0x04001BD1 RID: 7121
		internal int cbInitiatorLength;

		// Token: 0x04001BD2 RID: 7122
		internal int dwInitiatorOffset;

		// Token: 0x04001BD3 RID: 7123
		internal int dwAcceptorAddrType;

		// Token: 0x04001BD4 RID: 7124
		internal int cbAcceptorLength;

		// Token: 0x04001BD5 RID: 7125
		internal int dwAcceptorOffset;

		// Token: 0x04001BD6 RID: 7126
		internal int cbApplicationDataLength;

		// Token: 0x04001BD7 RID: 7127
		internal int dwApplicationDataOffset;
	}
}
