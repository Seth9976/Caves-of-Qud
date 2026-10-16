using System;
using System.Runtime.Versioning;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000002 RID: 2
	public static class Unsafe
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static T Read<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E4 File Offset: 0x000002E4
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static T ReadUnaligned<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020FC File Offset: 0x000002FC
		[NonVersionable]
		[MethodImpl(256)]
		public static T ReadUnaligned<T>(ref byte source)
		{
			return source;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002114 File Offset: 0x00000314
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void Write<T>(void* destination, T value)
		{
			*(T*)destination = value;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002128 File Offset: 0x00000328
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void WriteUnaligned<T>(void* destination, T value)
		{
			*(T*)destination = value;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002140 File Offset: 0x00000340
		[NonVersionable]
		[MethodImpl(256)]
		public static void WriteUnaligned<T>(ref byte destination, T value)
		{
			destination = value;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002158 File Offset: 0x00000358
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void Copy<T>(void* destination, ref T source)
		{
			*(T*)destination = source;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002174 File Offset: 0x00000374
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void Copy<T>(ref T destination, void* source)
		{
			destination = *(T*)source;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void* AsPointer<T>(ref T value)
		{
			return (void*)(&value);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A0 File Offset: 0x000003A0
		[NonVersionable]
		[MethodImpl(256)]
		public static void SkipInit<T>(out T value)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B0 File Offset: 0x000003B0
		[NonVersionable]
		[MethodImpl(256)]
		public static int SizeOf<T>()
		{
			return sizeof(T);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021C4 File Offset: 0x000003C4
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void CopyBlock(void* destination, void* source, uint byteCount)
		{
			cpblk(destination, source, byteCount);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D8 File Offset: 0x000003D8
		[NonVersionable]
		[MethodImpl(256)]
		public static void CopyBlock(ref byte destination, ref byte source, uint byteCount)
		{
			cpblk(ref destination, ref source, byteCount);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021EC File Offset: 0x000003EC
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void CopyBlockUnaligned(void* destination, void* source, uint byteCount)
		{
			cpblk(destination, source, byteCount);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
		[NonVersionable]
		[MethodImpl(256)]
		public static void CopyBlockUnaligned(ref byte destination, ref byte source, uint byteCount)
		{
			cpblk(ref destination, ref source, byteCount);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000221C File Offset: 0x0000041C
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void InitBlock(void* startAddress, byte value, uint byteCount)
		{
			initblk(startAddress, value, byteCount);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002230 File Offset: 0x00000430
		[NonVersionable]
		[MethodImpl(256)]
		public static void InitBlock(ref byte startAddress, byte value, uint byteCount)
		{
			initblk(ref startAddress, value, byteCount);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002244 File Offset: 0x00000444
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
		{
			initblk(startAddress, value, byteCount);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000225C File Offset: 0x0000045C
		[NonVersionable]
		[MethodImpl(256)]
		public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
		{
			initblk(ref startAddress, value, byteCount);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002274 File Offset: 0x00000474
		[NonVersionable]
		[MethodImpl(256)]
		public static T As<T>(object o) where T : class
		{
			return o;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002284 File Offset: 0x00000484
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static ref T AsRef<T>(void* source)
		{
			return ref *(T*)source;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002294 File Offset: 0x00000494
		[NonVersionable]
		[MethodImpl(256)]
		public static ref T AsRef<T>(in T source)
		{
			return ref source;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022A4 File Offset: 0x000004A4
		[NonVersionable]
		[MethodImpl(256)]
		public static ref TTo As<TFrom, TTo>(ref TFrom source)
		{
			return ref source;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022B4 File Offset: 0x000004B4
		[MethodImpl(256)]
		public static ref T Unbox<T>(object box) where T : ValueType
		{
			return ref (T)box;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022C8 File Offset: 0x000004C8
		[NonVersionable]
		[MethodImpl(256)]
		public static ref T Add<T>(ref T source, int elementOffset)
		{
			return (ref source) + (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022E0 File Offset: 0x000004E0
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void* Add<T>(void* source, int elementOffset)
		{
			return (void*)((byte*)source + (IntPtr)elementOffset * (IntPtr)sizeof(T));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022F8 File Offset: 0x000004F8
		[NonVersionable]
		[MethodImpl(256)]
		public static ref T Add<T>(ref T source, IntPtr elementOffset)
		{
			return (ref source) + elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002310 File Offset: 0x00000510
		[NonVersionable]
		[MethodImpl(256)]
		public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
		{
			return (ref source) + byteOffset;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002320 File Offset: 0x00000520
		[NonVersionable]
		[MethodImpl(256)]
		public static ref T Subtract<T>(ref T source, int elementOffset)
		{
			return (ref source) - (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002338 File Offset: 0x00000538
		[NonVersionable]
		[MethodImpl(256)]
		public unsafe static void* Subtract<T>(void* source, int elementOffset)
		{
			return (void*)((byte*)source - (IntPtr)elementOffset * (IntPtr)sizeof(T));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002350 File Offset: 0x00000550
		[NonVersionable]
		[MethodImpl(256)]
		public static ref T Subtract<T>(ref T source, IntPtr elementOffset)
		{
			return (ref source) - elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002368 File Offset: 0x00000568
		[NonVersionable]
		[MethodImpl(256)]
		public static ref T SubtractByteOffset<T>(ref T source, IntPtr byteOffset)
		{
			return (ref source) - byteOffset;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002378 File Offset: 0x00000578
		[NonVersionable]
		[MethodImpl(256)]
		public static IntPtr ByteOffset<T>(ref T origin, ref T target)
		{
			return (ref target) - (ref origin);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002388 File Offset: 0x00000588
		[NonVersionable]
		[MethodImpl(256)]
		public static bool AreSame<T>(ref T left, ref T right)
		{
			return (ref left) == (ref right);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000239C File Offset: 0x0000059C
		[NonVersionable]
		[MethodImpl(256)]
		public static bool IsAddressGreaterThan<T>(ref T left, ref T right)
		{
			return (ref left) != (ref right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023B0 File Offset: 0x000005B0
		[NonVersionable]
		[MethodImpl(256)]
		public static bool IsAddressLessThan<T>(ref T left, ref T right)
		{
			return (ref left) < (ref right);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023C4 File Offset: 0x000005C4
		[NonVersionable]
		[MethodImpl(256)]
		public static bool IsNullRef<T>(ref T source)
		{
			return (ref source) == (UIntPtr)0;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023D8 File Offset: 0x000005D8
		[NonVersionable]
		[MethodImpl(256)]
		public static ref T NullRef<T>()
		{
			return (UIntPtr)0;
		}
	}
}
