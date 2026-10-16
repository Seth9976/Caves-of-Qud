using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000181 RID: 385
	internal static class ImmutableByteArrayInterop
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x00023678 File Offset: 0x00021878
		internal static ImmutableArray<byte> DangerousCreateFromUnderlyingArray([Nullable(2)] ref byte[] array)
		{
			byte[] array2 = array;
			array = null;
			return new ImmutableByteArrayInterop.ByteArrayUnion
			{
				UnderlyingArray = array2
			}.ImmutableArray;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000236A4 File Offset: 0x000218A4
		[return: Nullable(2)]
		internal static byte[] DangerousGetUnderlyingArray(ImmutableArray<byte> array)
		{
			return new ImmutableByteArrayInterop.ByteArrayUnion
			{
				ImmutableArray = array
			}.UnderlyingArray;
		}

		// Token: 0x0200020A RID: 522
		[StructLayout(2)]
		private struct ByteArrayUnion
		{
			// Token: 0x04000BE6 RID: 3046
			[FieldOffset(0)]
			internal byte[] UnderlyingArray;

			// Token: 0x04000BE7 RID: 3047
			[FieldOffset(0)]
			internal ImmutableArray<byte> ImmutableArray;
		}
	}
}
