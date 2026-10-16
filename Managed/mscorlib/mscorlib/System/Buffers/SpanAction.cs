using System;

namespace System.Buffers
{
	// Token: 0x02000ACF RID: 2767
	// (Invoke) Token: 0x060062C9 RID: 25289
	public delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);
}
