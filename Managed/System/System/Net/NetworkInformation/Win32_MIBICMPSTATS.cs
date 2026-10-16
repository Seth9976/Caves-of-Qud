using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000737 RID: 1847
	internal struct Win32_MIBICMPSTATS
	{
		// Token: 0x040022A5 RID: 8869
		public uint Msgs;

		// Token: 0x040022A6 RID: 8870
		public uint Errors;

		// Token: 0x040022A7 RID: 8871
		public uint DestUnreachs;

		// Token: 0x040022A8 RID: 8872
		public uint TimeExcds;

		// Token: 0x040022A9 RID: 8873
		public uint ParmProbs;

		// Token: 0x040022AA RID: 8874
		public uint SrcQuenchs;

		// Token: 0x040022AB RID: 8875
		public uint Redirects;

		// Token: 0x040022AC RID: 8876
		public uint Echos;

		// Token: 0x040022AD RID: 8877
		public uint EchoReps;

		// Token: 0x040022AE RID: 8878
		public uint Timestamps;

		// Token: 0x040022AF RID: 8879
		public uint TimestampReps;

		// Token: 0x040022B0 RID: 8880
		public uint AddrMasks;

		// Token: 0x040022B1 RID: 8881
		public uint AddrMaskReps;
	}
}
