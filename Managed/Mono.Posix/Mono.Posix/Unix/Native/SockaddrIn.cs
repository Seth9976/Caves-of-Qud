using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x0200006F RID: 111
	[Map("struct sockaddr_in")]
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SockaddrIn : Sockaddr, IEquatable<SockaddrIn>
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000BCD2 File Offset: 0x00009ED2
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000BCDA File Offset: 0x00009EDA
		public UnixAddressFamily sin_family
		{
			get
			{
				return base.sa_family;
			}
			set
			{
				base.sa_family = value;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000BCE3 File Offset: 0x00009EE3
		public SockaddrIn()
			: base(SockaddrType.SockaddrIn, UnixAddressFamily.AF_INET)
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000BCED File Offset: 0x00009EED
		public override string ToString()
		{
			return string.Format("{{sin_family={0}, sin_port=htons({1}), sin_addr={2}}}", base.sa_family, Syscall.ntohs(this.sin_port), this.sin_addr);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000BD20 File Offset: 0x00009F20
		public new static SockaddrIn FromSockaddrStorage(SockaddrStorage storage)
		{
			SockaddrIn sockaddrIn = new SockaddrIn();
			storage.CopyTo(sockaddrIn);
			return sockaddrIn;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public override int GetHashCode()
		{
			return this.sin_family.GetHashCode() ^ this.sin_port.GetHashCode() ^ this.sin_addr.GetHashCode();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000BD7B File Offset: 0x00009F7B
		public override bool Equals(object obj)
		{
			return obj is SockaddrIn && this.Equals((SockaddrIn)obj);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000BD93 File Offset: 0x00009F93
		public bool Equals(SockaddrIn value)
		{
			return value != null && (this.sin_family == value.sin_family && this.sin_port == value.sin_port) && this.sin_addr.Equals(value.sin_addr);
		}

		// Token: 0x0400049A RID: 1178
		public ushort sin_port;

		// Token: 0x0400049B RID: 1179
		public InAddr sin_addr;
	}
}
