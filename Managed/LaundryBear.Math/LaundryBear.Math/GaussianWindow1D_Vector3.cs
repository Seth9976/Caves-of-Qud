using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000004 RID: 4
	public class GaussianWindow1D_Vector3 : GaussianWindow1d<Vector3>
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002295 File Offset: 0x00000495
		public GaussianWindow1D_Vector3(float sigma, int maxKernelRadius = 10)
			: base(sigma, maxKernelRadius)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A0 File Offset: 0x000004A0
		protected override Vector3 Compute(int windowPos)
		{
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < base.KernelSize; i++)
			{
				vector += this.m_data[windowPos] * this.m_kernel[i];
				if (++windowPos == base.KernelSize)
				{
					windowPos = 0;
				}
			}
			return vector / this.m_kernelSum;
		}
	}
}
