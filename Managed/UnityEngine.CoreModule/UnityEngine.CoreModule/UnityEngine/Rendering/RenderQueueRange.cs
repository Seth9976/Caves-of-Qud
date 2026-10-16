using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200054E RID: 1358
	public struct RenderQueueRange : IEquatable<RenderQueueRange>
	{
		// Token: 0x0600340F RID: 13327 RVA: 0x00066028 File Offset: 0x00064228
		public RenderQueueRange(int lowerBound, int upperBound)
		{
			bool flag = lowerBound < 0 || lowerBound > 5000;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("lowerBound", lowerBound, string.Format("The lower bound must be at least {0} and at most {1}.", 0, 5000));
			}
			bool flag2 = upperBound < 0 || upperBound > 5000;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("upperBound", upperBound, string.Format("The upper bound must be at least {0} and at most {1}.", 0, 5000));
			}
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000660C0 File Offset: 0x000642C0
		public static RenderQueueRange all
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 5000
				};
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000660EC File Offset: 0x000642EC
		public static RenderQueueRange opaque
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 2500
				};
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x00066118 File Offset: 0x00064318
		public static RenderQueueRange transparent
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 2501,
					m_UpperBound = 5000
				};
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x00066148 File Offset: 0x00064348
		// (set) Token: 0x06003414 RID: 13332 RVA: 0x00066160 File Offset: 0x00064360
		public int lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				bool flag = value < 0 || value > 5000;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("The lower bound must be at least {0} and at most {1}.", 0, 5000));
				}
				this.m_LowerBound = value;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000661A8 File Offset: 0x000643A8
		// (set) Token: 0x06003416 RID: 13334 RVA: 0x000661C0 File Offset: 0x000643C0
		public int upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				bool flag = value < 0 || value > 5000;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("The upper bound must be at least {0} and at most {1}.", 0, 5000));
				}
				this.m_UpperBound = value;
			}
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x00066208 File Offset: 0x00064408
		public bool Equals(RenderQueueRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x0006623C File Offset: 0x0006443C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderQueueRange && this.Equals((RenderQueueRange)obj);
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x00066274 File Offset: 0x00064474
		public override int GetHashCode()
		{
			return (this.m_LowerBound * 397) ^ this.m_UpperBound;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x0006629C File Offset: 0x0006449C
		public static bool operator ==(RenderQueueRange left, RenderQueueRange right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000662B8 File Offset: 0x000644B8
		public static bool operator !=(RenderQueueRange left, RenderQueueRange right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001185 RID: 4485
		private int m_LowerBound;

		// Token: 0x04001186 RID: 4486
		private int m_UpperBound;

		// Token: 0x04001187 RID: 4487
		private const int k_MinimumBound = 0;

		// Token: 0x04001188 RID: 4488
		public static readonly int minimumBound = 0;

		// Token: 0x04001189 RID: 4489
		private const int k_MaximumBound = 5000;

		// Token: 0x0400118A RID: 4490
		public static readonly int maximumBound = 5000;
	}
}
