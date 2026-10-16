using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000062 RID: 98
	[Map]
	[CLSCompliant(false)]
	public struct InAddr : IEquatable<InAddr>
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public unsafe InAddr(byte b0, byte b1, byte b2, byte b3)
		{
			this.s_addr = 0U;
			fixed (uint* ptr = &this.s_addr)
			{
				byte* ptr2 = (byte*)ptr;
				*ptr2 = b0;
				ptr2[1] = b1;
				ptr2[2] = b2;
				ptr2[3] = b3;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000AA04 File Offset: 0x00008C04
		public unsafe InAddr(byte[] buffer)
		{
			if (buffer.Length != 4)
			{
				throw new ArgumentException("buffer.Length != 4", "buffer");
			}
			this.s_addr = 0U;
			fixed (uint* ptr = &this.s_addr)
			{
				uint* ptr2 = ptr;
				Marshal.Copy(buffer, 0, (IntPtr)((void*)ptr2), 4);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public unsafe void CopyFrom(byte[] source, int startIndex)
		{
			fixed (uint* ptr = &this.s_addr)
			{
				uint* ptr2 = ptr;
				Marshal.Copy(source, startIndex, (IntPtr)((void*)ptr2), 4);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000AA74 File Offset: 0x00008C74
		public unsafe void CopyTo(byte[] destination, int startIndex)
		{
			fixed (uint* ptr = &this.s_addr)
			{
				Marshal.Copy((IntPtr)((void*)ptr), destination, startIndex, 4);
			}
		}

		// Token: 0x1700008D RID: 141
		public unsafe byte this[int index]
		{
			get
			{
				if (index < 0 || index >= 4)
				{
					throw new ArgumentOutOfRangeException("index", "index < 0 || index >= 4");
				}
				fixed (uint* ptr = &this.s_addr)
				{
					return ((byte*)ptr)[index];
				}
			}
			set
			{
				if (index < 0 || index >= 4)
				{
					throw new ArgumentOutOfRangeException("index", "index < 0 || index >= 4");
				}
				fixed (uint* ptr = &this.s_addr)
				{
					((byte*)ptr)[index] = value;
				}
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000AB05 File Offset: 0x00008D05
		public override string ToString()
		{
			return NativeConvert.ToIPAddress(this).ToString();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000AB17 File Offset: 0x00008D17
		public override int GetHashCode()
		{
			return this.s_addr.GetHashCode();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000AB24 File Offset: 0x00008D24
		public override bool Equals(object obj)
		{
			return obj is InAddr && this.Equals((InAddr)obj);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000AB3C File Offset: 0x00008D3C
		public bool Equals(InAddr value)
		{
			return this.s_addr == value.s_addr;
		}

		// Token: 0x04000467 RID: 1127
		public uint s_addr;
	}
}
