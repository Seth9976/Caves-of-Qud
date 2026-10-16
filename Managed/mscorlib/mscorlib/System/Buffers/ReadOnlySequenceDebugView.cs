using System;
using System.Diagnostics;

namespace System.Buffers
{
	// Token: 0x02000AEA RID: 2794
	internal sealed class ReadOnlySequenceDebugView<T>
	{
		// Token: 0x06006364 RID: 25444 RVA: 0x0014C9BC File Offset: 0x0014ABBC
		public ReadOnlySequenceDebugView(ReadOnlySequence<T> sequence)
		{
			this._array = (in sequence).ToArray<T>();
			int num = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory in sequence)
			{
				num++;
			}
			ReadOnlyMemory<T>[] array = new ReadOnlyMemory<T>[num];
			int num2 = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory2 in sequence)
			{
				array[num2] = readOnlyMemory2;
				num2++;
			}
			this._segments = new ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments
			{
				Segments = array
			};
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06006365 RID: 25445 RVA: 0x0014CA47 File Offset: 0x0014AC47
		public ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments BufferSegments
		{
			get
			{
				return this._segments;
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06006366 RID: 25446 RVA: 0x0014CA4F File Offset: 0x0014AC4F
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x04003A7B RID: 14971
		private readonly T[] _array;

		// Token: 0x04003A7C RID: 14972
		private readonly ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments _segments;

		// Token: 0x02000AEB RID: 2795
		[DebuggerDisplay("Count: {Segments.Length}", Name = "Segments")]
		public struct ReadOnlySequenceDebugViewSegments
		{
			// Token: 0x17001192 RID: 4498
			// (get) Token: 0x06006367 RID: 25447 RVA: 0x0014CA57 File Offset: 0x0014AC57
			// (set) Token: 0x06006368 RID: 25448 RVA: 0x0014CA5F File Offset: 0x0014AC5F
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public ReadOnlyMemory<T>[] Segments { readonly get; set; }
		}
	}
}
