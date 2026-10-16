using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000027 RID: 39
	public struct RealTimeSignum : IEquatable<RealTimeSignum>
	{
		// Token: 0x0600035B RID: 859 RVA: 0x00009478 File Offset: 0x00007678
		public RealTimeSignum(int offset)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("Offset cannot be negative");
			}
			if (offset > RealTimeSignum.MaxOffset)
			{
				throw new ArgumentOutOfRangeException("Offset greater than maximum supported SIGRT");
			}
			this.rt_offset = offset;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000094A3 File Offset: 0x000076A3
		public int Offset
		{
			get
			{
				return this.rt_offset;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000094AB File Offset: 0x000076AB
		public override int GetHashCode()
		{
			return this.rt_offset.GetHashCode();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000094B8 File Offset: 0x000076B8
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && this.Equals((RealTimeSignum)obj);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000094E8 File Offset: 0x000076E8
		public bool Equals(RealTimeSignum value)
		{
			return this.Offset == value.Offset;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000094F9 File Offset: 0x000076F9
		public static bool operator ==(RealTimeSignum lhs, RealTimeSignum rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009503 File Offset: 0x00007703
		public static bool operator !=(RealTimeSignum lhs, RealTimeSignum rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400009F RID: 159
		private int rt_offset;

		// Token: 0x040000A0 RID: 160
		private static readonly int MaxOffset = UnixSignal.GetSIGRTMAX() - UnixSignal.GetSIGRTMIN() - 1;

		// Token: 0x040000A1 RID: 161
		public static readonly RealTimeSignum MinValue = new RealTimeSignum(0);

		// Token: 0x040000A2 RID: 162
		public static readonly RealTimeSignum MaxValue = new RealTimeSignum(RealTimeSignum.MaxOffset);
	}
}
