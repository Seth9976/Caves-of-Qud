using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000063 RID: 99
	[Map]
	public struct In6Addr : IEquatable<In6Addr>
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0000AB4C File Offset: 0x00008D4C
		public unsafe In6Addr(byte[] buffer)
		{
			if (buffer.Length != 16)
			{
				throw new ArgumentException("buffer.Length != 16", "buffer");
			}
			this.addr0 = (this.addr1 = 0UL);
			fixed (ulong* ptr = &this.addr0)
			{
				ulong* ptr2 = ptr;
				Marshal.Copy(buffer, 0, (IntPtr)((void*)ptr2), 16);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public unsafe void CopyFrom(byte[] source, int startIndex)
		{
			fixed (ulong* ptr = &this.addr0)
			{
				ulong* ptr2 = ptr;
				Marshal.Copy(source, startIndex, (IntPtr)((void*)ptr2), 16);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public unsafe void CopyTo(byte[] destination, int startIndex)
		{
			fixed (ulong* ptr = &this.addr0)
			{
				Marshal.Copy((IntPtr)((void*)ptr), destination, startIndex, 16);
			}
		}

		// Token: 0x1700008E RID: 142
		public unsafe byte this[int index]
		{
			get
			{
				if (index < 0 || index >= 16)
				{
					throw new ArgumentOutOfRangeException("index", "index < 0 || index >= 16");
				}
				fixed (ulong* ptr = &this.addr0)
				{
					return ((byte*)ptr)[index];
				}
			}
			set
			{
				if (index < 0 || index >= 16)
				{
					throw new ArgumentOutOfRangeException("index", "index < 0 || index >= 16");
				}
				fixed (ulong* ptr = &this.addr0)
				{
					((byte*)ptr)[index] = value;
				}
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000AC5E File Offset: 0x00008E5E
		public override string ToString()
		{
			return NativeConvert.ToIPAddress(this).ToString();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000AC70 File Offset: 0x00008E70
		public override int GetHashCode()
		{
			return this.addr0.GetHashCode() ^ this.addr1.GetHashCode();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000AC89 File Offset: 0x00008E89
		public override bool Equals(object obj)
		{
			return obj is In6Addr && this.Equals((In6Addr)obj);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		public bool Equals(In6Addr value)
		{
			return this.addr0 == value.addr0 && this.addr1 == value.addr1;
		}

		// Token: 0x04000468 RID: 1128
		private ulong addr0;

		// Token: 0x04000469 RID: 1129
		private ulong addr1;
	}
}
