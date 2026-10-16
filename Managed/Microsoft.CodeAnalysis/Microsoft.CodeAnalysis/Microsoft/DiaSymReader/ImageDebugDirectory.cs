using System;
using System.Runtime.InteropServices;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000FF RID: 255
	[StructLayout(0, Pack = 1)]
	internal struct ImageDebugDirectory
	{
		// Token: 0x04000270 RID: 624
		internal int Characteristics;

		// Token: 0x04000271 RID: 625
		internal int TimeDateStamp;

		// Token: 0x04000272 RID: 626
		internal short MajorVersion;

		// Token: 0x04000273 RID: 627
		internal short MinorVersion;

		// Token: 0x04000274 RID: 628
		internal int Type;

		// Token: 0x04000275 RID: 629
		internal int SizeOfData;

		// Token: 0x04000276 RID: 630
		internal int AddressOfRawData;

		// Token: 0x04000277 RID: 631
		internal int PointerToRawData;
	}
}
