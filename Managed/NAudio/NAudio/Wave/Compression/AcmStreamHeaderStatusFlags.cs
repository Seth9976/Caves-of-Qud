using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200016E RID: 366
	[Flags]
	internal enum AcmStreamHeaderStatusFlags
	{
		// Token: 0x04000815 RID: 2069
		Done = 65536,
		// Token: 0x04000816 RID: 2070
		Prepared = 131072,
		// Token: 0x04000817 RID: 2071
		InQueue = 1048576
	}
}
