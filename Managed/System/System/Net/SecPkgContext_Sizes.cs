using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200055A RID: 1370
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_Sizes
	{
		// Token: 0x06002C84 RID: 11396 RVA: 0x0009774C File Offset: 0x0009594C
		internal unsafe SecPkgContext_Sizes(byte[] memory)
		{
			fixed (byte[] array = memory)
			{
				void* ptr;
				if (memory == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = (void*)(&array[0]);
				}
				IntPtr intPtr = new IntPtr(ptr);
				checked
				{
					try
					{
						this.cbMaxToken = (int)((uint)Marshal.ReadInt32(intPtr));
						this.cbMaxSignature = (int)((uint)Marshal.ReadInt32(intPtr, 4));
						this.cbBlockSize = (int)((uint)Marshal.ReadInt32(intPtr, 8));
						this.cbSecurityTrailer = (int)((uint)Marshal.ReadInt32(intPtr, 12));
					}
					catch (OverflowException)
					{
						NetEventSource.Fail(this, "Negative size.", ".ctor");
						throw;
					}
				}
			}
		}

		// Token: 0x040017F5 RID: 6133
		public readonly int cbMaxToken;

		// Token: 0x040017F6 RID: 6134
		public readonly int cbMaxSignature;

		// Token: 0x040017F7 RID: 6135
		public readonly int cbBlockSize;

		// Token: 0x040017F8 RID: 6136
		public readonly int cbSecurityTrailer;

		// Token: 0x040017F9 RID: 6137
		public static readonly int SizeOf = Marshal.SizeOf<SecPkgContext_Sizes>();
	}
}
