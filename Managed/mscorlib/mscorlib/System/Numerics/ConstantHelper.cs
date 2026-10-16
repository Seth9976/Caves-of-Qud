using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x0200094B RID: 2379
	internal class ConstantHelper
	{
		// Token: 0x0600536C RID: 21356 RVA: 0x00105E14 File Offset: 0x00104014
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte GetByteWithAllBitsSet()
		{
			byte b = 0;
			*(&b) = byte.MaxValue;
			return b;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x00105E30 File Offset: 0x00104030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static sbyte GetSByteWithAllBitsSet()
		{
			sbyte b = 0;
			*(&b) = -1;
			return b;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x00105E48 File Offset: 0x00104048
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetUInt16WithAllBitsSet()
		{
			ushort num = 0;
			*(&num) = ushort.MaxValue;
			return num;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00105E64 File Offset: 0x00104064
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static short GetInt16WithAllBitsSet()
		{
			short num = 0;
			*(&num) = -1;
			return num;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x00105E7C File Offset: 0x0010407C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint GetUInt32WithAllBitsSet()
		{
			uint num = 0U;
			*(&num) = uint.MaxValue;
			return num;
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x00105E94 File Offset: 0x00104094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetInt32WithAllBitsSet()
		{
			int num = 0;
			*(&num) = -1;
			return num;
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x00105EAC File Offset: 0x001040AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetUInt64WithAllBitsSet()
		{
			ulong num = 0UL;
			*(&num) = ulong.MaxValue;
			return num;
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x00105EC4 File Offset: 0x001040C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long GetInt64WithAllBitsSet()
		{
			long num = 0L;
			*(&num) = -1L;
			return num;
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x00105EDC File Offset: 0x001040DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float GetSingleWithAllBitsSet()
		{
			float num = 0f;
			*(int*)(&num) = -1;
			return num;
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x00105EF8 File Offset: 0x001040F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double GetDoubleWithAllBitsSet()
		{
			double num = 0.0;
			*(long*)(&num) = -1L;
			return num;
		}
	}
}
