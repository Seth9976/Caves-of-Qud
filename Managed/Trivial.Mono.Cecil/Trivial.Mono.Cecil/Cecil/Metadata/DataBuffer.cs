using System;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000DC RID: 220
	internal sealed class DataBuffer : ByteBuffer
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		public DataBuffer()
			: base(0)
		{
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001F9DC File Offset: 0x0001DBDC
		public uint AddData(byte[] data)
		{
			int position = this.position;
			base.WriteBytes(data);
			return (uint)position;
		}
	}
}
