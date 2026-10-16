using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004B6 RID: 1206
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct SegmentedArraySegment<[Nullable(2)] T>
	{
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x0008A038 File Offset: 0x00088238
		[Nullable(new byte[] { 0, 1 })]
		public SegmentedArray<T> Array
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x0008A040 File Offset: 0x00088240
		public int Start { get; }

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x0008A048 File Offset: 0x00088248
		public int Length { get; }

		// Token: 0x06003041 RID: 12353 RVA: 0x0008A050 File Offset: 0x00088250
		public SegmentedArraySegment([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int start, int length)
		{
			this.Array = array;
			this.Start = start;
			this.Length = length;
		}

		// Token: 0x17000DCA RID: 3530
		public ref T this[int index]
		{
			get
			{
				if (index >= this.Length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				return this.Array[index + this.Start];
			}
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x0008A099 File Offset: 0x00088299
		[return: Nullable(new byte[] { 0, 1 })]
		public SegmentedArraySegment<T> Slice(int start)
		{
			if (start >= this.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new SegmentedArraySegment<T>(this.Array, this.Start + start, this.Length - start);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x0008A0C4 File Offset: 0x000882C4
		[return: Nullable(new byte[] { 0, 1 })]
		public SegmentedArraySegment<T> Slice(int start, int length)
		{
			if ((ulong)start + (ulong)length > (ulong)this.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new SegmentedArraySegment<T>(this.Array, this.Start + start, length);
		}
	}
}
