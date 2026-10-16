using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Security
{
	// Token: 0x02000851 RID: 2129
	internal class SecurityBuffer
	{
		// Token: 0x060043A2 RID: 17314 RVA: 0x000EBE70 File Offset: 0x000EA070
		public SecurityBuffer(byte[] data, int offset, int size, SecurityBufferType tokentype)
		{
			if (offset < 0 || offset > ((data == null) ? 0 : data.Length))
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'offset' out of range.  [{0}]", new object[] { offset }), ".ctor");
			}
			if (size < 0 || size > ((data == null) ? 0 : (data.Length - offset)))
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[] { size }), ".ctor");
			}
			this.offset = ((data == null || offset < 0) ? 0 : Math.Min(offset, data.Length));
			this.size = ((data == null || size < 0) ? 0 : Math.Min(size, data.Length - this.offset));
			this.type = tokentype;
			this.token = ((size == 0) ? null : data);
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000EBF3B File Offset: 0x000EA13B
		public SecurityBuffer(byte[] data, SecurityBufferType tokentype)
		{
			this.size = ((data == null) ? 0 : data.Length);
			this.type = tokentype;
			this.token = ((this.size == 0) ? null : data);
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000EBF6C File Offset: 0x000EA16C
		public SecurityBuffer(int size, SecurityBufferType tokentype)
		{
			if (size < 0)
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("'size' out of range.  [{0}]", new object[] { size }), ".ctor");
			}
			this.size = size;
			this.type = tokentype;
			this.token = ((size == 0) ? null : new byte[size]);
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000EBFC7 File Offset: 0x000EA1C7
		public SecurityBuffer(ChannelBinding binding)
		{
			this.size = ((binding == null) ? 0 : binding.Size);
			this.type = SecurityBufferType.SECBUFFER_CHANNEL_BINDINGS;
			this.unmanagedToken = binding;
		}

		// Token: 0x040028F5 RID: 10485
		public int size;

		// Token: 0x040028F6 RID: 10486
		public SecurityBufferType type;

		// Token: 0x040028F7 RID: 10487
		public byte[] token;

		// Token: 0x040028F8 RID: 10488
		public SafeHandle unmanagedToken;

		// Token: 0x040028F9 RID: 10489
		public int offset;
	}
}
