using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000107 RID: 263
	public class NormalizeProcessor : InputProcessor<float>
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x0004AA44 File Offset: 0x00048C44
		public override float Process(float value, InputControl control)
		{
			return NormalizeProcessor.Normalize(value, this.min, this.max, this.zero);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0004AA60 File Offset: 0x00048C60
		public static float Normalize(float value, float min, float max, float zero)
		{
			if (zero < min)
			{
				zero = min;
			}
			if (Mathf.Approximately(value, min))
			{
				if (min < zero)
				{
					return -1f;
				}
				return 0f;
			}
			else
			{
				float num = (value - min) / (max - min);
				if (min < zero)
				{
					return 2f * num - 1f;
				}
				return num;
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0004AAA8 File Offset: 0x00048CA8
		internal static float Denormalize(float value, float min, float max, float zero)
		{
			if (zero < min)
			{
				zero = min;
			}
			if (min >= zero)
			{
				return min + (max - min) * value;
			}
			if (value < 0f)
			{
				return min + (zero - min) * (value * -1f);
			}
			return zero + (max - zero) * value;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0004AADA File Offset: 0x00048CDA
		public override string ToString()
		{
			return string.Format("Normalize(min={0},max={1},zero={2})", this.min, this.max, this.zero);
		}

		// Token: 0x0400065D RID: 1629
		public float min;

		// Token: 0x0400065E RID: 1630
		public float max;

		// Token: 0x0400065F RID: 1631
		public float zero;
	}
}
