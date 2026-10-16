using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000019 RID: 25
	[NullableContext(2)]
	public interface IResettableBufferWriter<T> : IBufferWriter<T>
	{
		// Token: 0x060000EB RID: 235
		void Reset();
	}
}
