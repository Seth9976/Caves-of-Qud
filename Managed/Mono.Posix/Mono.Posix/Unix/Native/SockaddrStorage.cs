using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x0200006D RID: 109
	[CLSCompliant(false)]
	public sealed class SockaddrStorage : Sockaddr, IEquatable<SockaddrStorage>
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000B7B3 File Offset: 0x000099B3
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000B7BB File Offset: 0x000099BB
		public byte[] data { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000B7C4 File Offset: 0x000099C4
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000B7CC File Offset: 0x000099CC
		public long data_len { get; set; }

		// Token: 0x0600046C RID: 1132 RVA: 0x0000B7D5 File Offset: 0x000099D5
		internal override byte[] DynamicData()
		{
			return this.data;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000B7DD File Offset: 0x000099DD
		internal override long GetDynamicLength()
		{
			return this.data_len;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000B7E5 File Offset: 0x000099E5
		internal override void SetDynamicLength(long value)
		{
			this.data_len = value;
		}

		// Token: 0x0600046F RID: 1135
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_SockaddrStorage_get_size", SetLastError = true)]
		private static extern int get_size();

		// Token: 0x06000470 RID: 1136 RVA: 0x0000B7EE File Offset: 0x000099EE
		public SockaddrStorage()
			: base((SockaddrType)32769, UnixAddressFamily.AF_UNSPEC)
		{
			this.data = new byte[SockaddrStorage.default_size];
			this.data_len = 0L;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000B814 File Offset: 0x00009A14
		public SockaddrStorage(int size)
			: base((SockaddrType)32769, UnixAddressFamily.AF_UNSPEC)
		{
			this.data = new byte[size];
			this.data_len = 0L;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000B838 File Offset: 0x00009A38
		public unsafe void SetTo(Sockaddr address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			long nativeSize = address.GetNativeSize();
			if (nativeSize > (long)this.data.Length)
			{
				this.data = new byte[nativeSize];
			}
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			if (!NativeConvert.TryCopy(address, (IntPtr)((void*)ptr)))
			{
				throw new ArgumentException("Failed to convert to native struct", "address");
			}
			array = null;
			this.data_len = nativeSize;
			base.sa_family = address.sa_family;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public unsafe void CopyTo(Sockaddr address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (this.data_len < 0L || this.data_len > (long)this.data.Length)
			{
				throw new ArgumentException("data_len < 0 || data_len > data.Length", "this");
			}
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			if (!NativeConvert.TryCopy((IntPtr)((void*)ptr), this.data_len, address))
			{
				throw new ArgumentException("Failed to convert from native struct", "this");
			}
			array = null;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000B950 File Offset: 0x00009B50
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{{sa_family={0}, data_len={1}, data=(", base.sa_family, this.data_len);
			int num = 0;
			while ((long)num < this.data_len)
			{
				if (num != 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(this.data[num].ToString("x2"));
				num++;
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		public override int GetHashCode()
		{
			int num = 4660;
			int num2 = 0;
			while ((long)num2 < this.data_len)
			{
				num += num2 ^ (int)this.data[num2];
				num2++;
			}
			return num;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000BA0B File Offset: 0x00009C0B
		public override bool Equals(object obj)
		{
			return obj is SockaddrStorage && this.Equals((SockaddrStorage)obj);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000BA24 File Offset: 0x00009C24
		public bool Equals(SockaddrStorage value)
		{
			if (value == null)
			{
				return false;
			}
			if (this.data_len != value.data_len)
			{
				return false;
			}
			int num = 0;
			while ((long)num < this.data_len)
			{
				if (this.data[num] != value.data[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x04000496 RID: 1174
		private static readonly int default_size = SockaddrStorage.get_size();
	}
}
