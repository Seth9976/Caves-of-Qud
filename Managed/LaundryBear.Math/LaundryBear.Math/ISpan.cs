using System;

namespace LaundryBear.Math
{
	// Token: 0x02000011 RID: 17
	public interface ISpan<T>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000064 RID: 100
		T Start { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000065 RID: 101
		T Duration { get; }

		// Token: 0x06000066 RID: 102
		bool ContainsPoint(T point);

		// Token: 0x06000067 RID: 103
		bool ContainsSpan(ISpan<T> span);

		// Token: 0x06000068 RID: 104
		bool Overlaps(ISpan<T> span);

		// Token: 0x06000069 RID: 105
		bool StartsBefore(ISpan<T> span);

		// Token: 0x0600006A RID: 106
		bool EndsAfter(ISpan<T> span);
	}
}
