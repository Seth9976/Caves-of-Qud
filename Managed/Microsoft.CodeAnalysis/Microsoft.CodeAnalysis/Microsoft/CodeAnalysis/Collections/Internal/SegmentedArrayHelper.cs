using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004B5 RID: 1205
	internal static class SegmentedArrayHelper
	{
		// Token: 0x06003037 RID: 12343 RVA: 0x00089F95 File Offset: 0x00088195
		[NullableContext(2)]
		[MethodImpl(768)]
		internal static int GetSegmentSize<T>()
		{
			if (Unsafe.SizeOf<T>() == Unsafe.SizeOf<object>())
			{
				return SegmentedArrayHelper.ReferenceTypeSegmentHelper.SegmentSize;
			}
			return SegmentedArrayHelper.ValueTypeSegmentHelper<T>.SegmentSize;
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x00089FAE File Offset: 0x000881AE
		[NullableContext(2)]
		[MethodImpl(768)]
		internal static int GetSegmentShift<T>()
		{
			if (Unsafe.SizeOf<T>() == Unsafe.SizeOf<object>())
			{
				return SegmentedArrayHelper.ReferenceTypeSegmentHelper.SegmentShift;
			}
			return SegmentedArrayHelper.ValueTypeSegmentHelper<T>.SegmentShift;
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x00089FC7 File Offset: 0x000881C7
		[NullableContext(2)]
		[MethodImpl(768)]
		internal static int GetOffsetMask<T>()
		{
			if (Unsafe.SizeOf<T>() == Unsafe.SizeOf<object>())
			{
				return SegmentedArrayHelper.ReferenceTypeSegmentHelper.OffsetMask;
			}
			return SegmentedArrayHelper.ValueTypeSegmentHelper<T>.OffsetMask;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x00089FE0 File Offset: 0x000881E0
		private static int CalculateSegmentSize(int elementSize)
		{
			int num = 2;
			while (SegmentedArrayHelper.<CalculateSegmentSize>g__ArraySize|5_0(elementSize, num << 1) < 85000)
			{
				num <<= 1;
			}
			return num;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0008A008 File Offset: 0x00088208
		private static int CalculateSegmentShift(int segmentSize)
		{
			int num = 0;
			while ((segmentSize >>= 1) != 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0008A026 File Offset: 0x00088226
		private static int CalculateOffsetMask(int segmentSize)
		{
			return segmentSize - 1;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0008A02B File Offset: 0x0008822B
		[CompilerGenerated]
		internal static int <CalculateSegmentSize>g__ArraySize|5_0(int elementSize, int segmentSize)
		{
			return 2 * IntPtr.Size + elementSize * segmentSize;
		}

		// Token: 0x040012A9 RID: 4777
		internal const int IntrosortSizeThreshold = 16;

		// Token: 0x040012AA RID: 4778
		internal const MethodImplOptions FastPathMethodImplOptions = 768;

		// Token: 0x02000760 RID: 1888
		internal static class TestAccessor
		{
			// Token: 0x06003E22 RID: 15906 RVA: 0x000AF4E8 File Offset: 0x000AD6E8
			public static int CalculateSegmentSize(int elementSize)
			{
				return SegmentedArrayHelper.CalculateSegmentSize(elementSize);
			}

			// Token: 0x06003E23 RID: 15907 RVA: 0x000AF4F0 File Offset: 0x000AD6F0
			public static int CalculateSegmentShift(int elementSize)
			{
				return SegmentedArrayHelper.CalculateSegmentShift(elementSize);
			}

			// Token: 0x06003E24 RID: 15908 RVA: 0x000AF4F8 File Offset: 0x000AD6F8
			public static int CalculateOffsetMask(int elementSize)
			{
				return SegmentedArrayHelper.CalculateOffsetMask(elementSize);
			}
		}

		// Token: 0x02000761 RID: 1889
		private static class ReferenceTypeSegmentHelper
		{
			// Token: 0x04001D2C RID: 7468
			public static readonly int SegmentSize = SegmentedArrayHelper.CalculateSegmentSize(Unsafe.SizeOf<object>());

			// Token: 0x04001D2D RID: 7469
			public static readonly int SegmentShift = SegmentedArrayHelper.CalculateSegmentShift(SegmentedArrayHelper.ReferenceTypeSegmentHelper.SegmentSize);

			// Token: 0x04001D2E RID: 7470
			public static readonly int OffsetMask = SegmentedArrayHelper.CalculateOffsetMask(SegmentedArrayHelper.ReferenceTypeSegmentHelper.SegmentSize);
		}

		// Token: 0x02000762 RID: 1890
		private static class ValueTypeSegmentHelper<[Nullable(2)] T>
		{
			// Token: 0x04001D2F RID: 7471
			public static readonly int SegmentSize = SegmentedArrayHelper.CalculateSegmentSize(Unsafe.SizeOf<T>());

			// Token: 0x04001D30 RID: 7472
			public static readonly int SegmentShift = SegmentedArrayHelper.CalculateSegmentShift(SegmentedArrayHelper.ValueTypeSegmentHelper<T>.SegmentSize);

			// Token: 0x04001D31 RID: 7473
			public static readonly int OffsetMask = SegmentedArrayHelper.CalculateOffsetMask(SegmentedArrayHelper.ValueTypeSegmentHelper<T>.SegmentSize);
		}
	}
}
