using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200001D RID: 29
	internal struct DiscreteTime : IComparable
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00007612 File Offset: 0x00005812
		public static double tickValue
		{
			get
			{
				return 1E-12;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000761D File Offset: 0x0000581D
		public DiscreteTime(DiscreteTime time)
		{
			this.m_DiscreteTime = time.m_DiscreteTime;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000762B File Offset: 0x0000582B
		private DiscreteTime(long time)
		{
			this.m_DiscreteTime = time;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007634 File Offset: 0x00005834
		public DiscreteTime(double time)
		{
			this.m_DiscreteTime = DiscreteTime.DoubleToDiscreteTime(time);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007642 File Offset: 0x00005842
		public DiscreteTime(float time)
		{
			this.m_DiscreteTime = DiscreteTime.FloatToDiscreteTime(time);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007650 File Offset: 0x00005850
		public DiscreteTime(int time)
		{
			this.m_DiscreteTime = DiscreteTime.IntToDiscreteTime(time);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000765E File Offset: 0x0000585E
		public DiscreteTime(int frame, double fps)
		{
			this.m_DiscreteTime = DiscreteTime.DoubleToDiscreteTime((double)frame * fps);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000766F File Offset: 0x0000586F
		public DiscreteTime OneTickBefore()
		{
			return new DiscreteTime(this.m_DiscreteTime - 1L);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000767F File Offset: 0x0000587F
		public DiscreteTime OneTickAfter()
		{
			return new DiscreteTime(this.m_DiscreteTime + 1L);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000768F File Offset: 0x0000588F
		public long GetTick()
		{
			return this.m_DiscreteTime;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007697 File Offset: 0x00005897
		public static DiscreteTime FromTicks(long ticks)
		{
			return new DiscreteTime(ticks);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000076A0 File Offset: 0x000058A0
		public int CompareTo(object obj)
		{
			if (obj is DiscreteTime)
			{
				return this.m_DiscreteTime.CompareTo(((DiscreteTime)obj).m_DiscreteTime);
			}
			return 1;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000076D0 File Offset: 0x000058D0
		public bool Equals(DiscreteTime other)
		{
			return this.m_DiscreteTime == other.m_DiscreteTime;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000076E0 File Offset: 0x000058E0
		public override bool Equals(object obj)
		{
			return obj is DiscreteTime && this.Equals((DiscreteTime)obj);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000076F8 File Offset: 0x000058F8
		private static long DoubleToDiscreteTime(double time)
		{
			double num = time / 1E-12 + 0.5;
			if (num < 9.223372036854776E+18 && num > -9.223372036854776E+18)
			{
				return (long)num;
			}
			throw new ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007740 File Offset: 0x00005940
		private static long FloatToDiscreteTime(float time)
		{
			float num = time / 1E-12f + 0.5f;
			if (num < 9.223372E+18f && num > -9.223372E+18f)
			{
				return (long)num;
			}
			throw new ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007778 File Offset: 0x00005978
		private static long IntToDiscreteTime(int time)
		{
			return DiscreteTime.DoubleToDiscreteTime((double)time);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007781 File Offset: 0x00005981
		private static double ToDouble(long time)
		{
			return (double)time * 1E-12;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000778F File Offset: 0x0000598F
		private static float ToFloat(long time)
		{
			return (float)DiscreteTime.ToDouble(time);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007798 File Offset: 0x00005998
		public static explicit operator double(DiscreteTime b)
		{
			return DiscreteTime.ToDouble(b.m_DiscreteTime);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000077A5 File Offset: 0x000059A5
		public static explicit operator float(DiscreteTime b)
		{
			return DiscreteTime.ToFloat(b.m_DiscreteTime);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000077B2 File Offset: 0x000059B2
		public static explicit operator long(DiscreteTime b)
		{
			return b.m_DiscreteTime;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000077BA File Offset: 0x000059BA
		public static explicit operator DiscreteTime(double time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000077C2 File Offset: 0x000059C2
		public static explicit operator DiscreteTime(float time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000077CA File Offset: 0x000059CA
		public static implicit operator DiscreteTime(int time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000077D2 File Offset: 0x000059D2
		public static explicit operator DiscreteTime(long time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000077DA File Offset: 0x000059DA
		public static bool operator ==(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime == rhs.m_DiscreteTime;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000077EA File Offset: 0x000059EA
		public static bool operator !=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000077F6 File Offset: 0x000059F6
		public static bool operator >(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime > rhs.m_DiscreteTime;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007806 File Offset: 0x00005A06
		public static bool operator <(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime < rhs.m_DiscreteTime;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007816 File Offset: 0x00005A16
		public static bool operator <=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime <= rhs.m_DiscreteTime;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007829 File Offset: 0x00005A29
		public static bool operator >=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime >= rhs.m_DiscreteTime;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000783C File Offset: 0x00005A3C
		public static DiscreteTime operator +(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(lhs.m_DiscreteTime + rhs.m_DiscreteTime);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007850 File Offset: 0x00005A50
		public static DiscreteTime operator -(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(lhs.m_DiscreteTime - rhs.m_DiscreteTime);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007864 File Offset: 0x00005A64
		public override string ToString()
		{
			return this.m_DiscreteTime.ToString();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007880 File Offset: 0x00005A80
		public override int GetHashCode()
		{
			return this.m_DiscreteTime.GetHashCode();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000789B File Offset: 0x00005A9B
		public static DiscreteTime Min(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(Math.Min(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000078B3 File Offset: 0x00005AB3
		public static DiscreteTime Max(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(Math.Max(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000078CB File Offset: 0x00005ACB
		public static double SnapToNearestTick(double time)
		{
			return DiscreteTime.ToDouble(DiscreteTime.DoubleToDiscreteTime(time));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000078D8 File Offset: 0x00005AD8
		public static float SnapToNearestTick(float time)
		{
			return DiscreteTime.ToFloat(DiscreteTime.FloatToDiscreteTime(time));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000078E5 File Offset: 0x00005AE5
		public static long GetNearestTick(double time)
		{
			return DiscreteTime.DoubleToDiscreteTime(time);
		}

		// Token: 0x040000AE RID: 174
		private const double k_Tick = 1E-12;

		// Token: 0x040000AF RID: 175
		public static readonly DiscreteTime kMaxTime = new DiscreteTime(long.MaxValue);

		// Token: 0x040000B0 RID: 176
		private readonly long m_DiscreteTime;
	}
}
