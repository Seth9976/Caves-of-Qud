using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000014 RID: 20
	public static class VectorUtils
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003192 File Offset: 0x00001392
		public static Vector3 MemberwiseDivide(Vector3 numerator, Vector3 divisor)
		{
			numerator.x /= divisor.x;
			numerator.y /= divisor.y;
			numerator.z /= divisor.z;
			return numerator;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031C8 File Offset: 0x000013C8
		public static Vector3 MemberwiseDivide(this Vector3 numerator, float divisor)
		{
			numerator.x /= divisor;
			numerator.y /= divisor;
			numerator.z /= divisor;
			return numerator;
		}
	}
}
