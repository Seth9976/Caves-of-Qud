using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000295 RID: 661
	internal sealed class BlobHeap : Heap
	{
		// Token: 0x060010E0 RID: 4320 RVA: 0x000369A3 File Offset: 0x00034BA3
		public BlobHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000369AC File Offset: 0x00034BAC
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

		// Token: 0x060010E2 RID: 4322 RVA: 0x00036A0C File Offset: 0x00034C0C
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
