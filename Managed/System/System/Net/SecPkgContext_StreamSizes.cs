using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200055B RID: 1371
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_StreamSizes
	{
		// Token: 0x06002C86 RID: 11398 RVA: 0x000977EC File Offset: 0x000959EC
		internal unsafe SecPkgContext_StreamSizes(byte[] memory)
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
						this.cbHeader = (int)((uint)Marshal.ReadInt32(intPtr));
						this.cbTrailer = (int)((uint)Marshal.ReadInt32(intPtr, 4));
						this.cbMaximumMessage = (int)((uint)Marshal.ReadInt32(intPtr, 8));
						this.cBuffers = (int)((uint)Marshal.ReadInt32(intPtr, 12));
						this.cbBlockSize = (int)((uint)Marshal.ReadInt32(intPtr, 16));
					}
					catch (OverflowException)
					{
						NetEventSource.Fail(this, "Negative size.", ".ctor");
						throw;
					}
				}
			}
		}

		// Token: 0x040017FA RID: 6138
		public int cbHeader;

		// Token: 0x040017FB RID: 6139
		public int cbTrailer;

		// Token: 0x040017FC RID: 6140
		public int cbMaximumMessage;

		// Token: 0x040017FD RID: 6141
		public int cBuffers;

		// Token: 0x040017FE RID: 6142
		public int cbBlockSize;

		// Token: 0x040017FF RID: 6143
		public static readonly int SizeOf = Marshal.SizeOf<SecPkgContext_StreamSizes>();
	}
}
