using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200072F RID: 1839
	internal struct Win32_MIB_IPSTATS
	{
		// Token: 0x0400227E RID: 8830
		public int Forwarding;

		// Token: 0x0400227F RID: 8831
		public int DefaultTTL;

		// Token: 0x04002280 RID: 8832
		public uint InReceives;

		// Token: 0x04002281 RID: 8833
		public uint InHdrErrors;

		// Token: 0x04002282 RID: 8834
		public uint InAddrErrors;

		// Token: 0x04002283 RID: 8835
		public uint ForwDatagrams;

		// Token: 0x04002284 RID: 8836
		public uint InUnknownProtos;

		// Token: 0x04002285 RID: 8837
		public uint InDiscards;

		// Token: 0x04002286 RID: 8838
		public uint InDelivers;

		// Token: 0x04002287 RID: 8839
		public uint OutRequests;

		// Token: 0x04002288 RID: 8840
		public uint RoutingDiscards;

		// Token: 0x04002289 RID: 8841
		public uint OutDiscards;

		// Token: 0x0400228A RID: 8842
		public uint OutNoRoutes;

		// Token: 0x0400228B RID: 8843
		public uint ReasmTimeout;

		// Token: 0x0400228C RID: 8844
		public uint ReasmReqds;

		// Token: 0x0400228D RID: 8845
		public uint ReasmOks;

		// Token: 0x0400228E RID: 8846
		public uint ReasmFails;

		// Token: 0x0400228F RID: 8847
		public uint FragOks;

		// Token: 0x04002290 RID: 8848
		public uint FragFails;

		// Token: 0x04002291 RID: 8849
		public uint FragCreates;

		// Token: 0x04002292 RID: 8850
		public int NumIf;

		// Token: 0x04002293 RID: 8851
		public int NumAddr;

		// Token: 0x04002294 RID: 8852
		public int NumRoutes;
	}
}
