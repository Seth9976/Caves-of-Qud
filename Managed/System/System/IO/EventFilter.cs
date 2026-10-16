using System;

namespace System.IO
{
	// Token: 0x0200051E RID: 1310
	internal enum EventFilter : short
	{
		// Token: 0x040016AC RID: 5804
		Read = -1,
		// Token: 0x040016AD RID: 5805
		Write = -2,
		// Token: 0x040016AE RID: 5806
		Aio = -3,
		// Token: 0x040016AF RID: 5807
		Vnode = -4,
		// Token: 0x040016B0 RID: 5808
		Proc = -5,
		// Token: 0x040016B1 RID: 5809
		Signal = -6,
		// Token: 0x040016B2 RID: 5810
		Timer = -7,
		// Token: 0x040016B3 RID: 5811
		MachPort = -8,
		// Token: 0x040016B4 RID: 5812
		FS = -9,
		// Token: 0x040016B5 RID: 5813
		User = -10,
		// Token: 0x040016B6 RID: 5814
		VM = -11
	}
}
