using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000A8 RID: 168
	[NativeContainer]
	internal struct NativeArrayDispose
	{
		// Token: 0x0600038E RID: 910 RVA: 0x000074CC File Offset: 0x000056CC
		public void Dispose()
		{
			UnsafeUtility.FreeTracked(this.m_Buffer, this.m_AllocatorLabel);
		}

		// Token: 0x0400027F RID: 639
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		// Token: 0x04000280 RID: 640
		internal Allocator m_AllocatorLabel;
	}
}
