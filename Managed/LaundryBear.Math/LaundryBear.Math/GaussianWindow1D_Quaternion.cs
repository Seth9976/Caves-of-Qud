using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000005 RID: 5
	public class GaussianWindow1D_Quaternion : GaussianWindow1d<Quaternion>
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002301 File Offset: 0x00000501
		public GaussianWindow1D_Quaternion(float sigma, int maxKernelRadius = 10)
			: base(sigma, maxKernelRadius)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000230C File Offset: 0x0000050C
		protected override Quaternion Compute(int windowPos)
		{
			Quaternion quaternion = new Quaternion(0f, 0f, 0f, 0f);
			Quaternion quaternion2 = this.m_data[this.m_currentPos];
			Quaternion quaternion3 = Quaternion.Inverse(quaternion2);
			for (int i = 0; i < base.KernelSize; i++)
			{
				float num = this.m_kernel[i];
				Quaternion quaternion4 = quaternion3 * this.m_data[windowPos];
				if (Quaternion.Dot(Quaternion.identity, quaternion4) < 0f)
				{
					num = -num;
				}
				quaternion.x += quaternion4.x * num;
				quaternion.y += quaternion4.y * num;
				quaternion.z += quaternion4.z * num;
				quaternion.w += quaternion4.w * num;
				if (++windowPos == base.KernelSize)
				{
					windowPos = 0;
				}
			}
			return quaternion2 * quaternion;
		}
	}
}
