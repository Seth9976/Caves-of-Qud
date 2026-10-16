using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000748 RID: 1864
	internal struct Win32_MIB_IFROW
	{
		// Token: 0x0400232C RID: 9004
		private const int MAX_INTERFACE_NAME_LEN = 256;

		// Token: 0x0400232D RID: 9005
		private const int MAXLEN_PHYSADDR = 8;

		// Token: 0x0400232E RID: 9006
		private const int MAXLEN_IFDESCR = 256;

		// Token: 0x0400232F RID: 9007
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public char[] Name;

		// Token: 0x04002330 RID: 9008
		public int Index;

		// Token: 0x04002331 RID: 9009
		public NetworkInterfaceType Type;

		// Token: 0x04002332 RID: 9010
		public int Mtu;

		// Token: 0x04002333 RID: 9011
		public uint Speed;

		// Token: 0x04002334 RID: 9012
		public int PhysAddrLen;

		// Token: 0x04002335 RID: 9013
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysAddr;

		// Token: 0x04002336 RID: 9014
		public uint AdminStatus;

		// Token: 0x04002337 RID: 9015
		public uint OperStatus;

		// Token: 0x04002338 RID: 9016
		public uint LastChange;

		// Token: 0x04002339 RID: 9017
		public int InOctets;

		// Token: 0x0400233A RID: 9018
		public int InUcastPkts;

		// Token: 0x0400233B RID: 9019
		public int InNUcastPkts;

		// Token: 0x0400233C RID: 9020
		public int InDiscards;

		// Token: 0x0400233D RID: 9021
		public int InErrors;

		// Token: 0x0400233E RID: 9022
		public int InUnknownProtos;

		// Token: 0x0400233F RID: 9023
		public int OutOctets;

		// Token: 0x04002340 RID: 9024
		public int OutUcastPkts;

		// Token: 0x04002341 RID: 9025
		public int OutNUcastPkts;

		// Token: 0x04002342 RID: 9026
		public int OutDiscards;

		// Token: 0x04002343 RID: 9027
		public int OutErrors;

		// Token: 0x04002344 RID: 9028
		public int OutQLen;

		// Token: 0x04002345 RID: 9029
		public int DescrLen;

		// Token: 0x04002346 RID: 9030
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] Descr;
	}
}
