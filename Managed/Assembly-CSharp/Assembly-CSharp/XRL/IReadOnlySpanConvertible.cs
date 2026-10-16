using System;

namespace XRL
{
	// Token: 0x02000513 RID: 1299
	public interface IReadOnlySpanConvertible<T>
	{
		// Token: 0x0600379E RID: 14238
		ReadOnlySpan<T> AsSpan();

		// Token: 0x0600379F RID: 14239
		ReadOnlySpan<T> AsSpan(int Start);

		// Token: 0x060037A0 RID: 14240
		ReadOnlySpan<T> AsSpan(int Start, int Length);
	}
}
