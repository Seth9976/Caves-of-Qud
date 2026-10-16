using System;

namespace System.Buffers
{
	// Token: 0x02000AD7 RID: 2775
	public interface IPinnable
	{
		// Token: 0x060062E8 RID: 25320
		MemoryHandle Pin(int elementIndex);

		// Token: 0x060062E9 RID: 25321
		void Unpin();
	}
}
