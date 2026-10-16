using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000DE RID: 222
	internal sealed class GuidHeap : Heap
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x0001F8A7 File Offset: 0x0001DAA7
		public GuidHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001F9F8 File Offset: 0x0001DBF8
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
