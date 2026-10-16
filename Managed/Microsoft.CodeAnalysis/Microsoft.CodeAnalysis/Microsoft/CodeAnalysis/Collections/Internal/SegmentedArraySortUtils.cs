using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004AD RID: 1197
	internal static class SegmentedArraySortUtils
	{
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x00089BA2 File Offset: 0x00087DA2
		private unsafe static ReadOnlySpan<byte> Log2DeBruijn
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.4BCD43D478B9229AB7A13406353712C7944B60348C36B4D0E6B789D10F697652), 32);
			}
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x00089BB0 File Offset: 0x00087DB0
		[NullableContext(1)]
		public unsafe static int MoveNansToFront<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values)
		{
			int num = 0;
			for (int i = 0; i < keys.Length; i++)
			{
				if ((typeof(TKey) == typeof(double) && double.IsNaN((double)((object)(*keys[i])))) || (typeof(TKey) == typeof(float) && float.IsNaN((float)((object)(*keys[i])))) || (typeof(TKey) == typeof(float) && float.IsNaN((float)((object)(*keys[i])))))
				{
					TKey tkey = *keys[num];
					*keys[num] = *keys[i];
					*keys[i] = tkey;
					if (i < values.Length)
					{
						TValue tvalue = *values[num];
						*values[num] = *values[i];
						*values[i] = tvalue;
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x00089D00 File Offset: 0x00087F00
		public static int Log2(uint value)
		{
			return SegmentedArraySortUtils.Log2SoftwareFallback(value);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x00089D08 File Offset: 0x00087F08
		private unsafe static int Log2SoftwareFallback(uint value)
		{
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return (int)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(SegmentedArraySortUtils.Log2DeBruijn), (IntPtr)((int)(value * 130329821U >> 27))));
		}
	}
}
