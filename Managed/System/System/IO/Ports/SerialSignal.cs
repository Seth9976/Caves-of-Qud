using System;

namespace System.IO.Ports
{
	// Token: 0x0200053B RID: 1339
	internal enum SerialSignal
	{
		// Token: 0x04001763 RID: 5987
		None,
		// Token: 0x04001764 RID: 5988
		Cd,
		// Token: 0x04001765 RID: 5989
		Cts,
		// Token: 0x04001766 RID: 5990
		Dsr = 4,
		// Token: 0x04001767 RID: 5991
		Dtr = 8,
		// Token: 0x04001768 RID: 5992
		Rts = 16
	}
}
