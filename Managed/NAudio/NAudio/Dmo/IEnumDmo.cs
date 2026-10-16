using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	// Token: 0x020001FF RID: 511
	[Guid("2c3cd98a-2bfa-4a53-9c27-5249ba64ba0f")]
	[InterfaceType(1)]
	internal interface IEnumDmo
	{
		// Token: 0x06000B8B RID: 2955
		int Next(int itemsToFetch, out Guid clsid, out IntPtr name, out int itemsFetched);

		// Token: 0x06000B8C RID: 2956
		int Skip(int itemsToSkip);

		// Token: 0x06000B8D RID: 2957
		int Reset();

		// Token: 0x06000B8E RID: 2958
		int Clone(out IEnumDmo enumPointer);
	}
}
