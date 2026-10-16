using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000006 RID: 6
	public class GaussianWindow1D_CameraRotation : GaussianWindow1d<Vector2>
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002406 File Offset: 0x00000606
		public GaussianWindow1D_CameraRotation(float sigma, int maxKernelRadius = 10)
			: base(sigma, maxKernelRadius)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002410 File Offset: 0x00000610
		protected override Vector2 Compute(int windowPos)
		{
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = this.m_data[this.m_currentPos];
			for (int i = 0; i < base.KernelSize; i++)
			{
				Vector2 vector3 = this.m_data[windowPos] - vector2;
				if (vector3.y > 180f)
				{
					vector3.y -= 360f;
				}
				if (vector3.y < -180f)
				{
					vector3.y += 360f;
				}
				vector += vector3 * this.m_kernel[i];
				if (++windowPos == base.KernelSize)
				{
					windowPos = 0;
				}
			}
			return vector2 + vector / this.m_kernelSum;
		}
	}
}
