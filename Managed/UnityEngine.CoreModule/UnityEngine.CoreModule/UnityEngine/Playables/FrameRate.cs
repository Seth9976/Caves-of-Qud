using System;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000444 RID: 1092
	[VisibleToOtherModules(new string[] { "UnityEngine.DirectorModule" })]
	[NativeHeader("Runtime/Director/Core/FrameRate.h")]
	[UsedByNativeCode("FrameRate")]
	internal struct FrameRate : IEquatable<FrameRate>
	{
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x0005125B File Offset: 0x0004F45B
		public bool dropFrame
		{
			get
			{
				return this.m_Rate < 0;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x00051266 File Offset: 0x0004F466
		public double rate
		{
			get
			{
				return this.dropFrame ? ((double)(-(double)this.m_Rate) * 0.999000999000999) : ((double)this.m_Rate);
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x0005128B File Offset: 0x0004F48B
		public FrameRate(uint frameRate = 0U, bool drop = false)
		{
			this.m_Rate = (int)((drop ? uint.MaxValue : 1U) * frameRate);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000512A0 File Offset: 0x0004F4A0
		public bool IsValid()
		{
			return this.m_Rate != 0;
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000512BC File Offset: 0x0004F4BC
		public bool Equals(FrameRate other)
		{
			return this.m_Rate == other.m_Rate;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000512DC File Offset: 0x0004F4DC
		public override bool Equals(object obj)
		{
			return obj is FrameRate && this.Equals((FrameRate)obj);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00051305 File Offset: 0x0004F505
		public static bool operator ==(FrameRate a, FrameRate b)
		{
			return a.Equals(b);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x0005130F File Offset: 0x0004F50F
		public static bool operator !=(FrameRate a, FrameRate b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x0005131C File Offset: 0x0004F51C
		public static bool operator <(FrameRate a, FrameRate b)
		{
			return a.rate < b.rate;
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x0005132E File Offset: 0x0004F52E
		public static bool operator <=(FrameRate a, FrameRate b)
		{
			return a.rate <= b.rate;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00051343 File Offset: 0x0004F543
		public static bool operator >(FrameRate a, FrameRate b)
		{
			return a.rate > b.rate;
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x0005132E File Offset: 0x0004F52E
		public static bool operator >=(FrameRate a, FrameRate b)
		{
			return a.rate <= b.rate;
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00051358 File Offset: 0x0004F558
		public override int GetHashCode()
		{
			return this.m_Rate;
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x00051370 File Offset: 0x0004F570
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0005138C File Offset: 0x0004F58C
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000513A8 File Offset: 0x0004F5A8
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = (this.dropFrame ? "F2" : "F0");
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("{0} Fps", new object[] { this.rate.ToString(format, formatProvider) });
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x00051414 File Offset: 0x0004F614
		internal static int FrameRateToInt(FrameRate framerate)
		{
			return framerate.m_Rate;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0005142C File Offset: 0x0004F62C
		internal static FrameRate DoubleToFrameRate(double framerate)
		{
			uint num = (uint)Math.Ceiling(framerate);
			bool flag = num <= 0U;
			FrameRate frameRate;
			if (flag)
			{
				frameRate = new FrameRate(1U, false);
			}
			else
			{
				FrameRate frameRate2 = new FrameRate(num, true);
				bool flag2 = Math.Abs(framerate - frameRate2.rate) < Math.Abs(framerate - num);
				if (flag2)
				{
					frameRate = frameRate2;
				}
				else
				{
					frameRate = new FrameRate(num, false);
				}
			}
			return frameRate;
		}

		// Token: 0x04000C0C RID: 3084
		[Ignore]
		public static readonly FrameRate k_24Fps = new FrameRate(24U, false);

		// Token: 0x04000C0D RID: 3085
		[Ignore]
		public static readonly FrameRate k_23_976Fps = new FrameRate(24U, true);

		// Token: 0x04000C0E RID: 3086
		[Ignore]
		public static readonly FrameRate k_25Fps = new FrameRate(25U, false);

		// Token: 0x04000C0F RID: 3087
		[Ignore]
		public static readonly FrameRate k_30Fps = new FrameRate(30U, false);

		// Token: 0x04000C10 RID: 3088
		[Ignore]
		public static readonly FrameRate k_29_97Fps = new FrameRate(30U, true);

		// Token: 0x04000C11 RID: 3089
		[Ignore]
		public static readonly FrameRate k_50Fps = new FrameRate(50U, false);

		// Token: 0x04000C12 RID: 3090
		[Ignore]
		public static readonly FrameRate k_60Fps = new FrameRate(60U, false);

		// Token: 0x04000C13 RID: 3091
		[Ignore]
		public static readonly FrameRate k_59_94Fps = new FrameRate(60U, true);

		// Token: 0x04000C14 RID: 3092
		[SerializeField]
		private int m_Rate;
	}
}
