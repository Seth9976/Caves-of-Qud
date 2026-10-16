using System;
using System.Collections.Generic;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200029D RID: 669
	internal sealed class BlobHeapBuffer : HeapBuffer
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00036F35 File Offset: 0x00035135
		public override bool IsEmpty
		{
			get
			{
				return this.length <= 1;
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0003713F File Offset: 0x0003533F
		public BlobHeapBuffer()
			: base(1)
		{
			base.WriteByte(0);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00037160 File Offset: 0x00035360
		public uint GetBlobIndex(ByteBuffer blob)
		{
			uint position;
			if (this.blobs.TryGetValue(blob, out position))
			{
				return position;
			}
			position = (uint)this.position;
			this.WriteBlob(blob);
			this.blobs.Add(blob, position);
			return position;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003719B File Offset: 0x0003539B
		private void WriteBlob(ByteBuffer blob)
		{
			base.WriteCompressedUInt32((uint)blob.length);
			base.WriteBytes(blob);
		}

		// Token: 0x0400064D RID: 1613
		private readonly Dictionary<ByteBuffer, uint> blobs = new Dictionary<ByteBuffer, uint>(new ByteBufferEqualityComparer());
	}
}
