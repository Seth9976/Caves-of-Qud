using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000781 RID: 1921
	internal static class AddrofIntrinsics
	{
		// Token: 0x0600447D RID: 17533 RVA: 0x000E3B12 File Offset: 0x000E1D12
		internal static IntPtr AddrOf<T>(T ftn)
		{
			return Marshal.GetFunctionPointerForDelegate<T>(ftn);
		}
	}
}
