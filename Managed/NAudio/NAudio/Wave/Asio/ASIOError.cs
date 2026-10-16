using System;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000149 RID: 329
	internal enum ASIOError
	{
		// Token: 0x04000747 RID: 1863
		ASE_OK,
		// Token: 0x04000748 RID: 1864
		ASE_SUCCESS = 1061701536,
		// Token: 0x04000749 RID: 1865
		ASE_NotPresent = -1000,
		// Token: 0x0400074A RID: 1866
		ASE_HWMalfunction,
		// Token: 0x0400074B RID: 1867
		ASE_InvalidParameter,
		// Token: 0x0400074C RID: 1868
		ASE_InvalidMode,
		// Token: 0x0400074D RID: 1869
		ASE_SPNotAdvancing,
		// Token: 0x0400074E RID: 1870
		ASE_NoClock,
		// Token: 0x0400074F RID: 1871
		ASE_NoMemory
	}
}
