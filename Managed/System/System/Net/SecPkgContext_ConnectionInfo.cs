using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000550 RID: 1360
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_ConnectionInfo
	{
		// Token: 0x06002C28 RID: 11304 RVA: 0x0009623C File Offset: 0x0009443C
		internal unsafe SecPkgContext_ConnectionInfo(byte[] nativeBuffer)
		{
			fixed (byte[] array = nativeBuffer)
			{
				void* ptr;
				if (nativeBuffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = (void*)(&array[0]);
				}
				try
				{
					IntPtr intPtr = new IntPtr(ptr);
					this.Protocol = Marshal.ReadInt32(intPtr);
					this.DataCipherAlg = Marshal.ReadInt32(intPtr, 4);
					this.DataKeySize = Marshal.ReadInt32(intPtr, 8);
					this.DataHashAlg = Marshal.ReadInt32(intPtr, 12);
					this.DataHashKeySize = Marshal.ReadInt32(intPtr, 16);
					this.KeyExchangeAlg = Marshal.ReadInt32(intPtr, 20);
					this.KeyExchKeySize = Marshal.ReadInt32(intPtr, 24);
				}
				catch (OverflowException)
				{
					NetEventSource.Fail(this, "Negative size", ".ctor");
					throw;
				}
			}
		}

		// Token: 0x040017DC RID: 6108
		public readonly int Protocol;

		// Token: 0x040017DD RID: 6109
		public readonly int DataCipherAlg;

		// Token: 0x040017DE RID: 6110
		public readonly int DataKeySize;

		// Token: 0x040017DF RID: 6111
		public readonly int DataHashAlg;

		// Token: 0x040017E0 RID: 6112
		public readonly int DataHashKeySize;

		// Token: 0x040017E1 RID: 6113
		public readonly int KeyExchangeAlg;

		// Token: 0x040017E2 RID: 6114
		public readonly int KeyExchKeySize;
	}
}
