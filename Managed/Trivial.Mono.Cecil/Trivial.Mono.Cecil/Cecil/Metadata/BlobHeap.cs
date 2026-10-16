using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000D9 RID: 217
	internal sealed class BlobHeap : Heap
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x0001F8A7 File Offset: 0x0001DAA7
		public BlobHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		public byte[] Read(uint index)
		{
			if (index == 0U || (ulong)index > (ulong)((long)(this.data.Length - 1)))
			{
				return Empty<byte>.Array;
			}
			int num = (int)index;
			int num2 = (int)this.data.ReadCompressedUInt32(ref num);
			if (num2 > this.data.Length - num)
			{
				return Empty<byte>.Array;
			}
			byte[] array = new byte[num2];
			Buffer.BlockCopy(this.data, num, array, 0, num2);
			return array;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001F910 File Offset: 0x0001DB10
		public void GetView(uint signature, out byte[] buffer, out int index, out int length)
		{
			if (signature == 0U || (ulong)signature > (ulong)((long)(this.data.Length - 1)))
			{
				buffer = null;
				index = (length = 0);
				return;
			}
			buffer = this.data;
			index = (int)signature;
			length = (int)buffer.ReadCompressedUInt32(ref index);
		}
	}
}
