using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000298 RID: 664
	internal sealed class DataBuffer : ByteBuffer
	{
		// Token: 0x060010FA RID: 4346 RVA: 0x00036E7B File Offset: 0x0003507B
		public DataBuffer()
			: base(0)
		{
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00036E9C File Offset: 0x0003509C
		public uint AddData(byte[] data)
		{
			uint position = (uint)this.position;
			base.WriteBytes(data);
			return position;
		}
	}
}
