using System;

namespace Unity.Collections
{
	// Token: 0x020000AC RID: 172
	public static class NativeSliceExtensions
	{
		// Token: 0x06000395 RID: 917 RVA: 0x000075CC File Offset: 0x000057CC
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray) where T : struct
		{
			return new NativeSlice<T>(thisArray);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000075E4 File Offset: 0x000057E4
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray, int start) where T : struct
		{
			return new NativeSlice<T>(thisArray, start);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00007600 File Offset: 0x00005800
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray, int start, int length) where T : struct
		{
			return new NativeSlice<T>(thisArray, start, length);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000761C File Offset: 0x0000581C
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice) where T : struct
		{
			return thisSlice;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00007630 File Offset: 0x00005830
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice, int start) where T : struct
		{
			return new NativeSlice<T>(thisSlice, start);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000764C File Offset: 0x0000584C
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice, int start, int length) where T : struct
		{
			return new NativeSlice<T>(thisSlice, start, length);
		}
	}
}
