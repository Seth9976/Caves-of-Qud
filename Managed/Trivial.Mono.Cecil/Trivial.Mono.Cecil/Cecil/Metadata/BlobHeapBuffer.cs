using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000DA RID: 218
	internal sealed class BlobHeapBuffer : HeapBuffer
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001F952 File Offset: 0x0001DB52
		public override bool IsEmpty
		{
			get
			{
				return this.length <= 1;
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001F960 File Offset: 0x0001DB60
		public BlobHeapBuffer()
			: base(1)
		{
			base.WriteByte(0);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001F980 File Offset: 0x0001DB80
		public uint GetBlobIndex(ByteBuffer blob)
		{
			uint position;
			if (this.blobs.TryGetValue(blob, ref position))
			{
				return position;
			}
			position = (uint)this.position;
			this.WriteBlob(blob);
			this.blobs.Add(blob, position);
			return position;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001F9BB File Offset: 0x0001DBBB
		private void WriteBlob(ByteBuffer blob)
		{
			base.WriteCompressedUInt32((uint)blob.length);
			base.WriteBytes(blob);
		}

		// Token: 0x0400037A RID: 890
		private readonly Dictionary<ByteBuffer, uint> blobs = new Dictionary<ByteBuffer, uint>(new ByteBufferEqualityComparer());
	}
}
