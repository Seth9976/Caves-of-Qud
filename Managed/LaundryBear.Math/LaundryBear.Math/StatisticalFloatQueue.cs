using System;
using System.Collections.Generic;

namespace LaundryBear.Math
{
	// Token: 0x0200000B RID: 11
	public class StatisticalFloatQueue : StatisticalQueue<float>
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000291F File Offset: 0x00000B1F
		public StatisticalFloatQueue(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002928 File Offset: 0x00000B28
		public StatisticalFloatQueue(int capacity, IEnumerable<float> startingValues)
			: base(capacity, startingValues)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002934 File Offset: 0x00000B34
		public override float GetMax()
		{
			if (base.Count > 0)
			{
				float num = this.m_list[0];
				for (int i = 1; i < base.Count; i++)
				{
					if (num > this.m_list[i])
					{
						num = this.m_list[i];
					}
				}
				return num;
			}
			throw new InvalidOperationException("The statistical queue must have at least one value before executing GetMax().");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002990 File Offset: 0x00000B90
		public override float GetMin()
		{
			if (base.Count > 0)
			{
				float num = this.m_list[0];
				for (int i = 1; i < base.Count; i++)
				{
					if (num < this.m_list[i])
					{
						num = this.m_list[i];
					}
				}
				return num;
			}
			throw new InvalidOperationException("The statistical queue must have at least one value before executing GetMin().");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029EC File Offset: 0x00000BEC
		public override float GetAverage()
		{
			if (base.Count > 0)
			{
				float num = 0f;
				for (int i = 0; i < base.Count; i++)
				{
					num += this.m_list[i];
				}
				return num / (float)base.Count;
			}
			throw new InvalidOperationException("The statistical queue must have at least one value before executing GetAverage().");
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A3C File Offset: 0x00000C3C
		public override float GetInstantPartialDerivative()
		{
			if (base.Count > 1)
			{
				return this.m_list[base.Count - 1] - this.m_list[base.Count - 2];
			}
			throw new InvalidOperationException("The statistical queue must have at least two values before executing GetInstancePartialDerivative().");
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A7C File Offset: 0x00000C7C
		public override float GetAveragePartialDerivative()
		{
			if (base.Count > 1)
			{
				float num = 0f;
				for (int i = 1; i < base.Count; i++)
				{
					num += this.m_list[i] - this.m_list[i - 1];
				}
				return num / (float)(base.Count - 1);
			}
			throw new InvalidOperationException("The statistical queue must have at least two values before executing GetAveragePartialDerivative().");
		}
	}
}
