using System;
using System.Runtime.InteropServices;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200010F RID: 271
	public class FloatingPointNumerics
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x0001412C File Offset: 0x0001232C
		public static bool AreAlmostEqualUlps(float left, float right, int maxUlps)
		{
			FloatingPointNumerics.FloatIntUnion floatIntUnion = default(FloatingPointNumerics.FloatIntUnion);
			FloatingPointNumerics.FloatIntUnion floatIntUnion2 = default(FloatingPointNumerics.FloatIntUnion);
			floatIntUnion.Float = left;
			floatIntUnion2.Float = right;
			uint num = floatIntUnion.UInt >> 31;
			uint num2 = floatIntUnion2.UInt >> 31;
			uint num3 = (2147483648U - floatIntUnion.UInt) & num;
			floatIntUnion.UInt = num3 | (floatIntUnion.UInt & ~num);
			uint num4 = (2147483648U - floatIntUnion2.UInt) & num2;
			floatIntUnion2.UInt = num4 | (floatIntUnion2.UInt & ~num2);
			return (num == num2 || (Math.Abs(floatIntUnion.Int) <= maxUlps && Math.Abs(floatIntUnion2.Int) <= maxUlps)) && Math.Abs(floatIntUnion.Int - floatIntUnion2.Int) <= maxUlps;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000141F0 File Offset: 0x000123F0
		public static bool AreAlmostEqualUlps(double left, double right, long maxUlps)
		{
			FloatingPointNumerics.DoubleLongUnion doubleLongUnion = default(FloatingPointNumerics.DoubleLongUnion);
			FloatingPointNumerics.DoubleLongUnion doubleLongUnion2 = default(FloatingPointNumerics.DoubleLongUnion);
			doubleLongUnion.Double = left;
			doubleLongUnion2.Double = right;
			ulong num = doubleLongUnion.ULong >> 63;
			ulong num2 = doubleLongUnion2.ULong >> 63;
			ulong num3 = (9223372036854775808UL - doubleLongUnion.ULong) & num;
			doubleLongUnion.ULong = num3 | (doubleLongUnion.ULong & ~num);
			ulong num4 = (9223372036854775808UL - doubleLongUnion2.ULong) & num2;
			doubleLongUnion2.ULong = num4 | (doubleLongUnion2.ULong & ~num2);
			return (num == num2 || (Math.Abs(doubleLongUnion.Long) <= maxUlps && Math.Abs(doubleLongUnion2.Long) <= maxUlps)) && Math.Abs(doubleLongUnion.Long - doubleLongUnion2.Long) <= maxUlps;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000142BC File Offset: 0x000124BC
		public static int ReinterpretAsInt(float value)
		{
			return new FloatingPointNumerics.FloatIntUnion
			{
				Float = value
			}.Int;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000142E0 File Offset: 0x000124E0
		public static long ReinterpretAsLong(double value)
		{
			return new FloatingPointNumerics.DoubleLongUnion
			{
				Double = value
			}.Long;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00014304 File Offset: 0x00012504
		public static float ReinterpretAsFloat(int value)
		{
			return new FloatingPointNumerics.FloatIntUnion
			{
				Int = value
			}.Float;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00014328 File Offset: 0x00012528
		public static double ReinterpretAsDouble(long value)
		{
			return new FloatingPointNumerics.DoubleLongUnion
			{
				Long = value
			}.Double;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00002050 File Offset: 0x00000250
		private FloatingPointNumerics()
		{
		}

		// Token: 0x020001A7 RID: 423
		[StructLayout(LayoutKind.Explicit)]
		private struct FloatIntUnion
		{
			// Token: 0x040002B6 RID: 694
			[FieldOffset(0)]
			public float Float;

			// Token: 0x040002B7 RID: 695
			[FieldOffset(0)]
			public int Int;

			// Token: 0x040002B8 RID: 696
			[FieldOffset(0)]
			public uint UInt;
		}

		// Token: 0x020001A8 RID: 424
		[StructLayout(LayoutKind.Explicit)]
		private struct DoubleLongUnion
		{
			// Token: 0x040002B9 RID: 697
			[FieldOffset(0)]
			public double Double;

			// Token: 0x040002BA RID: 698
			[FieldOffset(0)]
			public long Long;

			// Token: 0x040002BB RID: 699
			[FieldOffset(0)]
			public ulong ULong;
		}
	}
}
