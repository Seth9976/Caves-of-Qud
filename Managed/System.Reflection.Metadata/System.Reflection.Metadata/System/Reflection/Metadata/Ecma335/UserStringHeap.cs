using System;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000119 RID: 281
	internal readonly struct UserStringHeap
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x0001D913 File Offset: 0x0001BB13
		public UserStringHeap(MemoryBlock block)
		{
			this.Block = block;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001D91C File Offset: 0x0001BB1C
		[NullableContext(1)]
		internal string GetString(UserStringHandle handle)
		{
			int num;
			int num2;
			if (!this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out num, out num2))
			{
				return string.Empty;
			}
			return this.Block.PeekUtf16(num, num2 & -2);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001D958 File Offset: 0x0001BB58
		internal UserStringHandle GetNextHandle(UserStringHandle handle)
		{
			int num;
			int num2;
			if (!this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out num, out num2))
			{
				return default(UserStringHandle);
			}
			int num3 = num + num2;
			if (num3 >= this.Block.Length)
			{
				return default(UserStringHandle);
			}
			return UserStringHandle.FromOffset(num3);
		}

		// Token: 0x04000662 RID: 1634
		internal readonly MemoryBlock Block;
	}
}
