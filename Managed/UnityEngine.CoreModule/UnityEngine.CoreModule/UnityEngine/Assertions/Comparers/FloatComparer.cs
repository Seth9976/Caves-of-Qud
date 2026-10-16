using System;
using System.Collections.Generic;

namespace UnityEngine.Assertions.Comparers
{
	// Token: 0x0200046F RID: 1135
	public class FloatComparer : IEqualityComparer<float>
	{
		// Token: 0x06002CD8 RID: 11480 RVA: 0x00054D1E File Offset: 0x00052F1E
		public FloatComparer()
			: this(1E-05f, false)
		{
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x00054D2E File Offset: 0x00052F2E
		public FloatComparer(bool relative)
			: this(1E-05f, relative)
		{
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x00054D3E File Offset: 0x00052F3E
		public FloatComparer(float error)
			: this(error, false)
		{
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x00054D4A File Offset: 0x00052F4A
		public FloatComparer(float error, bool relative)
		{
			this.m_Error = error;
			this.m_Relative = relative;
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x00054D64 File Offset: 0x00052F64
		public bool Equals(float a, float b)
		{
			return this.m_Relative ? FloatComparer.AreEqualRelative(a, b, this.m_Error) : FloatComparer.AreEqual(a, b, this.m_Error);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x00054D9C File Offset: 0x00052F9C
		public int GetHashCode(float obj)
		{
			return base.GetHashCode();
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x00054DB4 File Offset: 0x00052FB4
		public static bool AreEqual(float expected, float actual, float error)
		{
			return Math.Abs(actual - expected) <= error;
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x00054DD4 File Offset: 0x00052FD4
		public static bool AreEqualRelative(float expected, float actual, float error)
		{
			bool flag = expected == actual;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				float num = Math.Abs(expected);
				float num2 = Math.Abs(actual);
				float num3 = Math.Abs((actual - expected) / ((num > num2) ? num : num2));
				flag2 = num3 <= error;
			}
			return flag2;
		}

		// Token: 0x04000C59 RID: 3161
		private readonly float m_Error;

		// Token: 0x04000C5A RID: 3162
		private readonly bool m_Relative;

		// Token: 0x04000C5B RID: 3163
		public static readonly FloatComparer s_ComparerWithDefaultTolerance = new FloatComparer(1E-05f);

		// Token: 0x04000C5C RID: 3164
		public const float kEpsilon = 1E-05f;
	}
}
