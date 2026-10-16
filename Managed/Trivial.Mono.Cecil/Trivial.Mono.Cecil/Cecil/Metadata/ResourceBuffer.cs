using System;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E4 RID: 228
	internal sealed class ResourceBuffer : ByteBuffer
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		public ResourceBuffer()
			: base(0)
		{
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001FB00 File Offset: 0x0001DD00
		public uint AddResource(byte[] resource)
		{
			int position = this.position;
			base.WriteInt32(resource.Length);
			base.WriteBytes(resource);
			return (uint)position;
		}
	}
}
