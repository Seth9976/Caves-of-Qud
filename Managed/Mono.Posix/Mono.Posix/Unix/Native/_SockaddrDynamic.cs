using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200006C RID: 108
	[Map]
	internal struct _SockaddrDynamic
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x0000B708 File Offset: 0x00009908
		public unsafe _SockaddrDynamic(Sockaddr address, byte* data, bool useMaxLength)
		{
			if (data == null)
			{
				this = default(_SockaddrDynamic);
				return;
			}
			byte[] array = address.DynamicData();
			this.type = address.type & (SockaddrType)(-32769);
			this.sa_family = address.sa_family;
			this.data = data;
			if (useMaxLength)
			{
				this.len = (long)array.Length;
				return;
			}
			this.len = address.GetDynamicLength();
			if (this.len < 0L || this.len > (long)array.Length)
			{
				throw new ArgumentException("len < 0 || len > dynData.Length", "address");
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000B78E File Offset: 0x0000998E
		public void Update(Sockaddr address)
		{
			if (this.data == null)
			{
				return;
			}
			address.sa_family = this.sa_family;
			address.SetDynamicLength(this.len);
		}

		// Token: 0x04000490 RID: 1168
		public SockaddrType type;

		// Token: 0x04000491 RID: 1169
		public UnixAddressFamily sa_family;

		// Token: 0x04000492 RID: 1170
		public unsafe byte* data;

		// Token: 0x04000493 RID: 1171
		public long len;
	}
}
