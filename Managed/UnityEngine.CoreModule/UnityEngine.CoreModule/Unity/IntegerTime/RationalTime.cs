using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IntegerTime
{
	// Token: 0x0200005E RID: 94
	[NativeHeader("Runtime/Input/RationalTime.h")]
	[Serializable]
	public struct RationalTime
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00003A28 File Offset: 0x00001C28
		public RationalTime(long count, RationalTime.TicksPerSecond ticks)
		{
			this.m_Count = count;
			this.m_TicksPerSecond = ticks;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003A39 File Offset: 0x00001C39
		public long Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003A41 File Offset: 0x00001C41
		public RationalTime.TicksPerSecond Ticks
		{
			get
			{
				return this.m_TicksPerSecond;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003A4C File Offset: 0x00001C4C
		[FreeFunction("IntegerTime::RationalTime::FromDouble", IsFreeFunction = true, ThrowsException = true)]
		public static RationalTime FromDouble(double t, RationalTime.TicksPerSecond ticksPerSecond)
		{
			RationalTime rationalTime;
			RationalTime.FromDouble_Injected(t, ref ticksPerSecond, out rationalTime);
			return rationalTime;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003A64 File Offset: 0x00001C64
		public static explicit operator DiscreteTime(RationalTime t)
		{
			return DiscreteTime.FromTicks(t.Convert(RationalTime.TicksPerSecond.DiscreteTimeRate).Count);
		}

		// Token: 0x0600016E RID: 366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromDouble_Injected(double t, [In] ref RationalTime.TicksPerSecond ticksPerSecond, out RationalTime ret);

		// Token: 0x04000148 RID: 328
		[SerializeField]
		private long m_Count;

		// Token: 0x04000149 RID: 329
		[SerializeField]
		private RationalTime.TicksPerSecond m_TicksPerSecond;

		// Token: 0x0200005F RID: 95
		[Serializable]
		public struct TicksPerSecond : IEquatable<RationalTime.TicksPerSecond>
		{
			// Token: 0x0600016F RID: 367 RVA: 0x00003A8E File Offset: 0x00001C8E
			public TicksPerSecond(uint num, uint den = 1U)
			{
				this.m_Numerator = num;
				this.m_Denominator = den;
				RationalTime.TicksPerSecond.Simplify(ref this.m_Numerator, ref this.m_Denominator);
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000170 RID: 368 RVA: 0x00003AB1 File Offset: 0x00001CB1
			public readonly uint Numerator
			{
				get
				{
					return this.m_Numerator;
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000171 RID: 369 RVA: 0x00003AB9 File Offset: 0x00001CB9
			public readonly uint Denominator
			{
				get
				{
					return this.m_Denominator;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000172 RID: 370 RVA: 0x00003AC1 File Offset: 0x00001CC1
			public readonly bool Valid
			{
				get
				{
					return RationalTime.TicksPerSecond.IsValid(this);
				}
			}

			// Token: 0x06000173 RID: 371 RVA: 0x00003AD0 File Offset: 0x00001CD0
			public readonly bool Equals(RationalTime.TicksPerSecond rhs)
			{
				return this.m_Numerator == rhs.m_Numerator && this.m_Denominator == rhs.m_Denominator;
			}

			// Token: 0x06000174 RID: 372 RVA: 0x00003B04 File Offset: 0x00001D04
			public override readonly bool Equals(object rhs)
			{
				bool flag;
				if (rhs is RationalTime.TicksPerSecond)
				{
					RationalTime.TicksPerSecond ticksPerSecond = (RationalTime.TicksPerSecond)rhs;
					flag = this.Equals(ticksPerSecond);
				}
				else
				{
					flag = false;
				}
				return flag;
			}

			// Token: 0x06000175 RID: 373 RVA: 0x00003B30 File Offset: 0x00001D30
			public override readonly int GetHashCode()
			{
				return HashCode.Combine<uint, uint>(this.m_Numerator, this.m_Denominator);
			}

			// Token: 0x06000176 RID: 374 RVA: 0x00003B54 File Offset: 0x00001D54
			private static void Simplify(ref uint num, ref uint den)
			{
				bool flag = den > 1U && num > 0U;
				if (flag)
				{
					uint num2 = RationalTime.TicksPerSecond.Gcd(num, den);
					num /= num2;
					den /= num2;
				}
			}

			// Token: 0x06000177 RID: 375 RVA: 0x00003B8C File Offset: 0x00001D8C
			private static uint Gcd(uint a, uint b)
			{
				for (;;)
				{
					bool flag = a == 0U;
					if (flag)
					{
						break;
					}
					b %= a;
					bool flag2 = b == 0U;
					if (flag2)
					{
						goto Block_2;
					}
					a %= b;
				}
				return b;
				Block_2:
				return a;
			}

			// Token: 0x06000178 RID: 376 RVA: 0x00003BC4 File Offset: 0x00001DC4
			[FreeFunction("IntegerTime::TicksPerSecond::IsValid", IsFreeFunction = true, ThrowsException = false)]
			private static bool IsValid(RationalTime.TicksPerSecond tps)
			{
				return RationalTime.TicksPerSecond.IsValid_Injected(ref tps);
			}

			// Token: 0x0600017A RID: 378
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool IsValid_Injected([In] ref RationalTime.TicksPerSecond tps);

			// Token: 0x0400014A RID: 330
			private const uint k_DefaultTicksPerSecond = 141120000U;

			// Token: 0x0400014B RID: 331
			[SerializeField]
			private uint m_Numerator;

			// Token: 0x0400014C RID: 332
			[SerializeField]
			private uint m_Denominator;

			// Token: 0x0400014D RID: 333
			public static readonly RationalTime.TicksPerSecond DefaultTicksPerSecond = new RationalTime.TicksPerSecond(141120000U, 1U);

			// Token: 0x0400014E RID: 334
			public static readonly RationalTime.TicksPerSecond TicksPerSecond24 = new RationalTime.TicksPerSecond(24U, 1U);

			// Token: 0x0400014F RID: 335
			public static readonly RationalTime.TicksPerSecond TicksPerSecond25 = new RationalTime.TicksPerSecond(25U, 1U);

			// Token: 0x04000150 RID: 336
			public static readonly RationalTime.TicksPerSecond TicksPerSecond30 = new RationalTime.TicksPerSecond(30U, 1U);

			// Token: 0x04000151 RID: 337
			public static readonly RationalTime.TicksPerSecond TicksPerSecond50 = new RationalTime.TicksPerSecond(50U, 1U);

			// Token: 0x04000152 RID: 338
			public static readonly RationalTime.TicksPerSecond TicksPerSecond60 = new RationalTime.TicksPerSecond(60U, 1U);

			// Token: 0x04000153 RID: 339
			public static readonly RationalTime.TicksPerSecond TicksPerSecond120 = new RationalTime.TicksPerSecond(120U, 1U);

			// Token: 0x04000154 RID: 340
			public static readonly RationalTime.TicksPerSecond TicksPerSecond2397 = new RationalTime.TicksPerSecond(24000U, 1001U);

			// Token: 0x04000155 RID: 341
			public static readonly RationalTime.TicksPerSecond TicksPerSecond2425 = new RationalTime.TicksPerSecond(25000U, 1001U);

			// Token: 0x04000156 RID: 342
			public static readonly RationalTime.TicksPerSecond TicksPerSecond2997 = new RationalTime.TicksPerSecond(30000U, 1001U);

			// Token: 0x04000157 RID: 343
			public static readonly RationalTime.TicksPerSecond TicksPerSecond5994 = new RationalTime.TicksPerSecond(60000U, 1001U);

			// Token: 0x04000158 RID: 344
			public static readonly RationalTime.TicksPerSecond TicksPerSecond11988 = new RationalTime.TicksPerSecond(120000U, 1001U);

			// Token: 0x04000159 RID: 345
			internal static readonly RationalTime.TicksPerSecond DiscreteTimeRate = new RationalTime.TicksPerSecond(141120000U, 1U);
		}
	}
}
