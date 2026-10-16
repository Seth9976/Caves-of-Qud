using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200000C RID: 12
	internal static class MathEx
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002A28 File Offset: 0x00000C28
		public static uint DivRem(uint a, uint b, out uint result)
		{
			uint num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A44 File Offset: 0x00000C44
		public static ulong DivRem(ulong a, ulong b, out ulong result)
		{
			ulong num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A60 File Offset: 0x00000C60
		public static int DivRem(int a, int b, out int result)
		{
			int num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A7C File Offset: 0x00000C7C
		public static long DivRem(long a, long b, out long result)
		{
			long num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A95 File Offset: 0x00000C95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte Clamp(byte value, byte min, byte max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<byte>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002AAF File Offset: 0x00000CAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Clamp(decimal value, decimal min, decimal max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<decimal>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AD8 File Offset: 0x00000CD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Clamp(double value, double min, double max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<double>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AF2 File Offset: 0x00000CF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short Clamp(short value, short min, short max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<short>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B0C File Offset: 0x00000D0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Clamp(int value, int min, int max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<int>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B26 File Offset: 0x00000D26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long Clamp(long value, long min, long max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<long>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B40 File Offset: 0x00000D40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte Clamp(sbyte value, sbyte min, sbyte max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<sbyte>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B5A File Offset: 0x00000D5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float value, float min, float max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<float>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B74 File Offset: 0x00000D74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort Clamp(ushort value, ushort min, ushort max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<ushort>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B8E File Offset: 0x00000D8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Clamp(uint value, uint min, uint max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<uint>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002BA8 File Offset: 0x00000DA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Clamp(ulong value, ulong min, ulong max)
		{
			if (min > max)
			{
				MathEx.ThrowMinMaxException<ulong>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BC2 File Offset: 0x00000DC2
		[NullableContext(1)]
		private static void ThrowMinMaxException<[Nullable(2)] T>(T min, T max)
		{
			throw new ArgumentException(string.Format("Argument_MinMaxValue, min:{0} max:{1}", min, max));
		}
	}
}
