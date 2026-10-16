using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000011 RID: 17
	public static class Packsize
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003400 File Offset: 0x00001600
		public static bool Test()
		{
			int num = Marshal.SizeOf(typeof(Packsize.ValvePackingSentinel_t));
			int num2 = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
			return num == 32 && num2 == 616;
		}

		// Token: 0x04000028 RID: 40
		public const int value = 8;

		// Token: 0x020001F1 RID: 497
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct ValvePackingSentinel_t
		{
			// Token: 0x04000B5C RID: 2908
			private uint m_u32;

			// Token: 0x04000B5D RID: 2909
			private ulong m_u64;

			// Token: 0x04000B5E RID: 2910
			private ushort m_u16;

			// Token: 0x04000B5F RID: 2911
			private double m_d;
		}
	}
}
