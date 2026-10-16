using System;

namespace System.Buffers
{
	// Token: 0x02000AE2 RID: 2786
	public interface IBufferWriter<T>
	{
		// Token: 0x06006319 RID: 25369
		void Advance(int count);

		// Token: 0x0600631A RID: 25370
		Memory<T> GetMemory(int sizeHint = 0);

		// Token: 0x0600631B RID: 25371
		Span<T> GetSpan(int sizeHint = 0);
	}
}
