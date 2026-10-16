using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.IntegerTime
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public struct DiscreteTime : IEquatable<DiscreteTime>, IFormattable, IComparable<DiscreteTime>
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00003392 File Offset: 0x00001592
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DiscreteTime(DiscreteTime x)
		{
			this.Value = x.Value;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000033A1 File Offset: 0x000015A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DiscreteTime(float v)
		{
			this.Value = (long)Math.Round((double)v * 141120000.0);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000033BC File Offset: 0x000015BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DiscreteTime(double v)
		{
			this.Value = (long)Math.Round(v * 141120000.0);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000033D6 File Offset: 0x000015D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DiscreteTime(long v)
		{
			this.Value = v * 141120000L;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000033E7 File Offset: 0x000015E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DiscreteTime(int v)
		{
			this.Value = (long)v * 141120000L;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000033F9 File Offset: 0x000015F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private DiscreteTime(long v, int _)
		{
			this.Value = v;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00003404 File Offset: 0x00001604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime FromTicks(long v)
		{
			return new DiscreteTime(v, 0);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00003420 File Offset: 0x00001620
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator DiscreteTime(float v)
		{
			return new DiscreteTime(v);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00003438 File Offset: 0x00001638
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator DiscreteTime(double v)
		{
			return new DiscreteTime(v);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00003450 File Offset: 0x00001650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float(DiscreteTime d)
		{
			return (float)((double)d.Value / 141120000.0);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00003474 File Offset: 0x00001674
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double(DiscreteTime d)
		{
			return (double)d.Value / 141120000.0;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00003498 File Offset: 0x00001698
		public static implicit operator RationalTime(DiscreteTime t)
		{
			return new RationalTime(t.Value, RationalTime.TicksPerSecond.DiscreteTimeRate);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000034BC File Offset: 0x000016BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000034DC File Offset: 0x000016DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value != rhs.Value;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003500 File Offset: 0x00001700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value < rhs.Value;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003520 File Offset: 0x00001720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value > rhs.Value;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003540 File Offset: 0x00001740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value <= rhs.Value;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003564 File Offset: 0x00001764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value >= rhs.Value;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00003588 File Offset: 0x00001788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator +(DiscreteTime lhs, DiscreteTime rhs)
		{
			return DiscreteTime.FromTicks(lhs.Value + rhs.Value);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000035AC File Offset: 0x000017AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator -(DiscreteTime lhs, DiscreteTime rhs)
		{
			return DiscreteTime.FromTicks(lhs.Value - rhs.Value);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000035D0 File Offset: 0x000017D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator *(DiscreteTime lhs, long rhs)
		{
			return DiscreteTime.FromTicks(lhs.Value * rhs);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000035F0 File Offset: 0x000017F0
		public static DiscreteTime operator *(DiscreteTime lhs, double s)
		{
			double num2;
			double num = DiscreteTime.Modf(s, out num2);
			long num3 = lhs.Value * (long)num2;
			bool flag = Math.Abs(num) >= 7.0861678004535145E-09;
			if (flag)
			{
				int num4 = DiscreteTime.Lzcnt(Math.Abs(lhs.Value)) - 1;
				long num5 = 1L << (num4 & 31);
				bool flag2 = num4 >= DiscreteTime.TicksPerSecondBits;
				if (flag2)
				{
					num5 = 141120000L;
				}
				else
				{
					bool flag3 = num4 >= DiscreteTime.NonPow2TpsBits;
					if (flag3)
					{
						num5 = (long)(275625UL << ((num4 - DiscreteTime.NonPow2TpsBits) & 31));
					}
				}
				long num6 = (long)Math.Round((double)num5 / num);
				num3 += lhs.Value * num5 / num6;
			}
			return DiscreteTime.FromTicks(num3);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000036B4 File Offset: 0x000018B4
		private static double Modf(double x, out double i)
		{
			i = Math.Truncate(x);
			return x - i;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000036D4 File Offset: 0x000018D4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static int Lzcnt(long x)
		{
			bool flag = x == 0L;
			int num;
			if (flag)
			{
				num = 64;
			}
			else
			{
				uint num2 = (uint)(x >> 32);
				uint num3 = ((num2 != 0U) ? num2 : ((uint)x));
				int num4 = ((num2 != 0U) ? 1054 : 1086);
				DiscreteTime.LongDoubleUnion longDoubleUnion;
				longDoubleUnion.doubleValue = 0.0;
				longDoubleUnion.longValue = (long)(4841369599423283200UL + (ulong)num3);
				longDoubleUnion.doubleValue -= 4503599627370496.0;
				num = num4 - (int)(longDoubleUnion.longValue >> 52);
			}
			return num;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000375C File Offset: 0x0000195C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator *(DiscreteTime lhs, float s)
		{
			return lhs * (double)s;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003778 File Offset: 0x00001978
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator /(DiscreteTime lhs, double s)
		{
			return lhs * (1.0 / s);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000379C File Offset: 0x0000199C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator /(DiscreteTime lhs, long s)
		{
			return DiscreteTime.FromTicks(lhs.Value / s);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000037BC File Offset: 0x000019BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator %(DiscreteTime lhs, DiscreteTime rhs)
		{
			return DiscreteTime.FromTicks(lhs.Value % rhs.Value);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000037E0 File Offset: 0x000019E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator -(DiscreteTime lhs)
		{
			return DiscreteTime.FromTicks(-lhs.Value);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00003800 File Offset: 0x00001A00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(DiscreteTime rhs)
		{
			return this.Value == rhs.Value;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003820 File Offset: 0x00001A20
		public override readonly bool Equals(object o)
		{
			return this.Equals((DiscreteTime)o);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003840 File Offset: 0x00001A40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003860 File Offset: 0x00001A60
		public override readonly string ToString()
		{
			return ((double)this).ToString();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003888 File Offset: 0x00001A88
		public readonly string ToString(string format, IFormatProvider formatProvider)
		{
			return ((double)this).ToString(format, formatProvider);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000038B0 File Offset: 0x00001AB0
		public readonly int CompareTo(DiscreteTime other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x0400011F RID: 287
		[SerializeField]
		public long Value;

		// Token: 0x04000120 RID: 288
		public static readonly DiscreteTime Zero = default(DiscreteTime);

		// Token: 0x04000121 RID: 289
		public static readonly DiscreteTime MinValue = new DiscreteTime(long.MinValue, 0);

		// Token: 0x04000122 RID: 290
		public static readonly DiscreteTime MaxValue = new DiscreteTime(long.MaxValue, 0);

		// Token: 0x04000123 RID: 291
		private const int Pow2Exp = 9;

		// Token: 0x04000124 RID: 292
		private const uint Pow2Tps = 512U;

		// Token: 0x04000125 RID: 293
		private const uint NonPow2Tps = 275625U;

		// Token: 0x04000126 RID: 294
		private static readonly int TicksPerSecondBits = (int)Mathf.Ceil(Mathf.Log(141120000f, 2f));

		// Token: 0x04000127 RID: 295
		private static readonly int NonPow2TpsBits = (int)Mathf.Ceil(Mathf.Log(275625f, 2f));

		// Token: 0x04000128 RID: 296
		public const uint TicksPerSecond = 141120000U;

		// Token: 0x04000129 RID: 297
		public const double Tick = 7.0861678004535145E-09;

		// Token: 0x0400012A RID: 298
		public const long MaxValueSeconds = 65358361939L;

		// Token: 0x0400012B RID: 299
		public const long MinValueSeconds = -65358361939L;

		// Token: 0x0400012C RID: 300
		public const uint Tick5Fps = 28224000U;

		// Token: 0x0400012D RID: 301
		public const uint Tick10Fps = 14112000U;

		// Token: 0x0400012E RID: 302
		public const uint Tick12Fps = 11760000U;

		// Token: 0x0400012F RID: 303
		public const uint Tick15Fps = 9408000U;

		// Token: 0x04000130 RID: 304
		public const uint Tick2397Fps = 5885880U;

		// Token: 0x04000131 RID: 305
		public const uint Tick24Fps = 5880000U;

		// Token: 0x04000132 RID: 306
		public const uint Tick25Fps = 5644800U;

		// Token: 0x04000133 RID: 307
		public const uint Tick2997Fps = 4708704U;

		// Token: 0x04000134 RID: 308
		public const uint Tick30Fps = 4704000U;

		// Token: 0x04000135 RID: 309
		public const uint Tick48Fps = 2940000U;

		// Token: 0x04000136 RID: 310
		public const uint Tick50Fps = 2822400U;

		// Token: 0x04000137 RID: 311
		public const uint Tick5995Fps = 2354352U;

		// Token: 0x04000138 RID: 312
		public const uint Tick60Fps = 2352000U;

		// Token: 0x04000139 RID: 313
		public const uint Tick90Fps = 1568000U;

		// Token: 0x0400013A RID: 314
		public const uint Tick11988Fps = 1177176U;

		// Token: 0x0400013B RID: 315
		public const uint Tick120Fps = 1176000U;

		// Token: 0x0400013C RID: 316
		public const uint Tick240Fps = 588000U;

		// Token: 0x0400013D RID: 317
		public const uint Tick1000Fps = 141120U;

		// Token: 0x0400013E RID: 318
		public const uint Tick8Khz = 17640U;

		// Token: 0x0400013F RID: 319
		public const uint Tick16Khz = 8820U;

		// Token: 0x04000140 RID: 320
		public const uint Tick22Khz = 6400U;

		// Token: 0x04000141 RID: 321
		public const uint Tick44Khz = 3200U;

		// Token: 0x04000142 RID: 322
		public const uint Tick48Khz = 2940U;

		// Token: 0x04000143 RID: 323
		public const uint Tick88Khz = 1600U;

		// Token: 0x04000144 RID: 324
		public const uint Tick96Khz = 1470U;

		// Token: 0x04000145 RID: 325
		public const uint Tick192Khz = 735U;

		// Token: 0x0200005C RID: 92
		[StructLayout(LayoutKind.Explicit)]
		private struct LongDoubleUnion
		{
			// Token: 0x04000146 RID: 326
			[FieldOffset(0)]
			public long longValue;

			// Token: 0x04000147 RID: 327
			[FieldOffset(0)]
			public double doubleValue;
		}
	}
}
