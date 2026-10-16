using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000070 RID: 112
	[Map("struct sockaddr_in6")]
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SockaddrIn6 : Sockaddr, IEquatable<SockaddrIn6>
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000BDC9 File Offset: 0x00009FC9
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		public UnixAddressFamily sin6_family
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

		// Token: 0x06000498 RID: 1176 RVA: 0x0000BDDA File Offset: 0x00009FDA
		public SockaddrIn6()
			: base(SockaddrType.SockaddrIn6, UnixAddressFamily.AF_INET6)
		{
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000BDE8 File Offset: 0x00009FE8
		public override string ToString()
		{
			return string.Format("{{sin6_family={0}, sin6_port=htons({1}), sin6_flowinfo={2}, sin6_addr={3}, sin6_scope_id={4}}}", new object[]
			{
				base.sa_family,
				Syscall.ntohs(this.sin6_port),
				this.sin6_flowinfo,
				this.sin6_addr,
				this.sin6_scope_id
			});
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000BE50 File Offset: 0x0000A050
		public new static SockaddrIn6 FromSockaddrStorage(SockaddrStorage storage)
		{
			SockaddrIn6 sockaddrIn = new SockaddrIn6();
			storage.CopyTo(sockaddrIn);
			return sockaddrIn;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public override int GetHashCode()
		{
			return this.sin6_family.GetHashCode() ^ this.sin6_port.GetHashCode() ^ this.sin6_flowinfo.GetHashCode() ^ this.sin6_addr.GetHashCode() ^ this.sin6_scope_id.GetHashCode();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000BEC3 File Offset: 0x0000A0C3
		public override bool Equals(object obj)
		{
			return obj is SockaddrIn6 && this.Equals((SockaddrIn6)obj);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public bool Equals(SockaddrIn6 value)
		{
			return value != null && (this.sin6_family == value.sin6_family && this.sin6_port == value.sin6_port && this.sin6_flowinfo == value.sin6_flowinfo && this.sin6_addr.Equals(value.sin6_addr)) && this.sin6_scope_id == value.sin6_scope_id;
		}

		// Token: 0x0400049C RID: 1180
		public ushort sin6_port;

		// Token: 0x0400049D RID: 1181
		public uint sin6_flowinfo;

		// Token: 0x0400049E RID: 1182
		public In6Addr sin6_addr;

		// Token: 0x0400049F RID: 1183
		public uint sin6_scope_id;
	}
}
