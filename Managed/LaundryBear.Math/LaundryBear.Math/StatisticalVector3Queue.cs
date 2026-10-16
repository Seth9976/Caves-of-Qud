using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x0200000C RID: 12
	public class StatisticalVector3Queue : StatisticalQueue<Vector3>
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002ADD File Offset: 0x00000CDD
		public StatisticalVector3Queue(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public StatisticalVector3Queue(int capacity, IEnumerable<Vector3> startingValues)
			: base(capacity, startingValues)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public override Vector3 GetMax()
		{
			if (base.Count > 0)
			{
				Vector3 vector = this.m_list[0];
				for (int i = 1; i < base.Count; i++)
				{
					if (vector.sqrMagnitude > this.m_list[i].sqrMagnitude)
					{
						vector = this.m_list[i];
					}
				}
				return vector;
			}
			throw new InvalidOperationException("The statistical queue must have at least one value before executing GetMax().");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B5C File Offset: 0x00000D5C
		public override Vector3 GetMin()
		{
			if (base.Count > 0)
			{
				Vector3 vector = this.m_list[0];
				for (int i = 1; i < base.Count; i++)
				{
					if (vector.sqrMagnitude < this.m_list[i].sqrMagnitude)
					{
						vector = this.m_list[i];
					}
				}
				return vector;
			}
			throw new InvalidOperationException("The statistical queue must have at least one value before executing GetMin().");
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public override Vector3 GetAverage()
		{
			if (base.Count > 0)
			{
				Vector3 vector = Vector3.zero;
				for (int i = 0; i < base.Count; i++)
				{
					vector += this.m_list[i];
				}
				return vector / (float)base.Count;
			}
			throw new InvalidOperationException("The statistical queue must have at least one value before executing GetAverage().");
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C20 File Offset: 0x00000E20
		public override Vector3 GetInstantPartialDerivative()
		{
			if (base.Count > 1)
			{
				return this.m_list[base.Count - 1] - this.m_list[base.Count - 2];
			}
			throw new InvalidOperationException("The statistical queue must have at least two values before executing GetInstancePartialDerivative().");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C6C File Offset: 0x00000E6C
		public override Vector3 GetAveragePartialDerivative()
		{
			if (base.Count > 1)
			{
				Vector3 vector = Vector3.zero;
				for (int i = 1; i < base.Count; i++)
				{
					vector += this.m_list[i] - this.m_list[i - 1];
				}
				return vector / (float)(base.Count - 1);
			}
			throw new InvalidOperationException("The statistical queue must have at least two values before executing GetAveragePartialDerivative().");
		}
	}
}
