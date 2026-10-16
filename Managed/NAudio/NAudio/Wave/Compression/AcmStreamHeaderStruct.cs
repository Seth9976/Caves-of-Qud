using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200016F RID: 367
	[StructLayout(0, Size = 128)]
	internal class AcmStreamHeaderStruct
	{
		// Token: 0x04000818 RID: 2072
		public int cbStruct;

		// Token: 0x04000819 RID: 2073
		public AcmStreamHeaderStatusFlags fdwStatus;

		// Token: 0x0400081A RID: 2074
		public IntPtr userData;

		// Token: 0x0400081B RID: 2075
		public IntPtr sourceBufferPointer;

		// Token: 0x0400081C RID: 2076
		public int sourceBufferLength;

		// Token: 0x0400081D RID: 2077
		public int sourceBufferLengthUsed;

		// Token: 0x0400081E RID: 2078
		public IntPtr sourceUserData;

		// Token: 0x0400081F RID: 2079
		public IntPtr destBufferPointer;

		// Token: 0x04000820 RID: 2080
		public int destBufferLength;

		// Token: 0x04000821 RID: 2081
		public int destBufferLengthUsed;

		// Token: 0x04000822 RID: 2082
		public IntPtr destUserData;
	}
}
