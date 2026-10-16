using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A2 RID: 674
	internal sealed class GuidHeap : Heap
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x000369A3 File Offset: 0x00034BA3
		public GuidHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00037274 File Offset: 0x00035474
		public Guid Read(uint index)
		{
			if (index == 0U || (ulong)(index - 1U + 16U) > (ulong)((long)this.data.Length))
			{
				return default(Guid);
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(this.data, (int)((index - 1U) * 16U), array, 0, 16);
			return new Guid(array);
		}
	}
}
