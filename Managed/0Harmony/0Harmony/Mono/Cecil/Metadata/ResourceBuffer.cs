using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000297 RID: 663
	internal sealed class ResourceBuffer : ByteBuffer
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x00036E7B File Offset: 0x0003507B
		public ResourceBuffer()
			: base(0)
		{
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00036E84 File Offset: 0x00035084
		public uint AddResource(byte[] resource)
		{
			uint position = (uint)this.position;
			base.WriteInt32(resource.Length);
			base.WriteBytes(resource);
			return position;
		}
	}
}
