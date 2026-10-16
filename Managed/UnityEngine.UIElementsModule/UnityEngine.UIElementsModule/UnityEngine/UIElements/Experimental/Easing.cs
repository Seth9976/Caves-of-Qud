using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020006FF RID: 1791
	public static class Easing
	{
		// Token: 0x06003396 RID: 13206 RVA: 0x000C9D6C File Offset: 0x000C7F6C
		public static float Step(float t)
		{
			return (float)((t < 0.5f) ? 0 : 1);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000C9D8C File Offset: 0x000C7F8C
		public static float Linear(float t)
		{
			return t;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000C9DA0 File Offset: 0x000C7FA0
		public static float InSine(float t)
		{
			return Mathf.Sin(1.5707964f * (t - 1f)) + 1f;
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000C9DCC File Offset: 0x000C7FCC
		public static float OutSine(float t)
		{
			return Mathf.Sin(t * 1.5707964f);
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000C9DEC File Offset: 0x000C7FEC
		public static float InOutSine(float t)
		{
			return (Mathf.Sin(3.1415927f * (t - 0.5f)) + 1f) * 0.5f;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000C9E1C File Offset: 0x000C801C
		public static float InQuad(float t)
		{
			return t * t;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000C9E34 File Offset: 0x000C8034
		public static float OutQuad(float t)
		{
			return t * (2f - t);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000C9E50 File Offset: 0x000C8050
		public static float InOutQuad(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = t * t * 0.5f;
			}
			else
			{
				num = -0.5f * ((t - 1f) * (t - 3f) - 1f);
			}
			return num;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000C9EA0 File Offset: 0x000C80A0
		public static float InCubic(float t)
		{
			return Easing.InPower(t, 3);
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000C9EBC File Offset: 0x000C80BC
		public static float OutCubic(float t)
		{
			return Easing.OutPower(t, 3);
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000C9ED8 File Offset: 0x000C80D8
		public static float InOutCubic(float t)
		{
			return Easing.InOutPower(t, 3);
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000C9EF4 File Offset: 0x000C80F4
		public static float InPower(float t, int power)
		{
			return Mathf.Pow(t, (float)power);
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000C9F10 File Offset: 0x000C8110
		public static float OutPower(float t, int power)
		{
			int num = ((power % 2 == 0) ? (-1) : 1);
			return (float)num * (Mathf.Pow(t - 1f, (float)power) + (float)num);
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000C9F44 File Offset: 0x000C8144
		public static float InOutPower(float t, int power)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = Easing.InPower(t, power) * 0.5f;
			}
			else
			{
				int num2 = ((power % 2 == 0) ? (-1) : 1);
				num = (float)num2 * 0.5f * (Mathf.Pow(t - 2f, (float)power) + (float)(num2 * 2));
			}
			return num;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000C9FA4 File Offset: 0x000C81A4
		public static float InBounce(float t)
		{
			return 1f - Easing.OutBounce(1f - t);
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000C9FC8 File Offset: 0x000C81C8
		public static float OutBounce(float t)
		{
			bool flag = t < 0.36363637f;
			float num;
			if (flag)
			{
				num = 7.5625f * t * t;
			}
			else
			{
				bool flag2 = t < 0.72727275f;
				if (flag2)
				{
					float num2;
					t = (num2 = t - 0.54545456f);
					num = 7.5625f * num2 * t + 0.75f;
				}
				else
				{
					bool flag3 = t < 0.90909094f;
					if (flag3)
					{
						float num3;
						t = (num3 = t - 0.8181818f);
						num = 7.5625f * num3 * t + 0.9375f;
					}
					else
					{
						float num4;
						t = (num4 = t - 0.95454544f);
						num = 7.5625f * num4 * t + 0.984375f;
					}
				}
			}
			return num;
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000CA068 File Offset: 0x000C8268
		public static float InOutBounce(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InBounce(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutBounce((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000CA0BC File Offset: 0x000C82BC
		public static float InElastic(float t)
		{
			bool flag = t == 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					float num2 = 0.3f;
					float num3 = num2 / 4f;
					float num4 = Mathf.Pow(2f, 10f * (t -= 1f));
					num = -(num4 * Mathf.Sin((t - num3) * 6.2831855f / num2));
				}
			}
			return num;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000CA138 File Offset: 0x000C8338
		public static float OutElastic(float t)
		{
			bool flag = t == 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = t == 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					float num2 = 0.3f;
					float num3 = num2 / 4f;
					num = Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - num3) * 6.2831855f / num2) + 1f;
				}
			}
			return num;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000CA1AC File Offset: 0x000C83AC
		public static float InOutElastic(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InElastic(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutElastic((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000CA200 File Offset: 0x000C8400
		public static float InBack(float t)
		{
			float num = 1.70158f;
			return t * t * ((num + 1f) * t - num);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000CA228 File Offset: 0x000C8428
		public static float OutBack(float t)
		{
			return 1f - Easing.InBack(1f - t);
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000CA24C File Offset: 0x000C844C
		public static float InOutBack(float t)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InBack(t * 2f) * 0.5f;
			}
			else
			{
				num = Easing.OutBack((t - 0.5f) * 2f) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000CA2A0 File Offset: 0x000C84A0
		public static float InBack(float t, float s)
		{
			return t * t * ((s + 1f) * t - s);
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000CA2C4 File Offset: 0x000C84C4
		public static float OutBack(float t, float s)
		{
			return 1f - Easing.InBack(1f - t, s);
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000CA2EC File Offset: 0x000C84EC
		public static float InOutBack(float t, float s)
		{
			bool flag = t < 0.5f;
			float num;
			if (flag)
			{
				num = Easing.InBack(t * 2f, s) * 0.5f;
			}
			else
			{
				num = Easing.OutBack((t - 0.5f) * 2f, s) * 0.5f + 0.5f;
			}
			return num;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000CA344 File Offset: 0x000C8544
		public static float InCirc(float t)
		{
			return -(Mathf.Sqrt(1f - t * t) - 1f);
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000CA36C File Offset: 0x000C856C
		public static float OutCirc(float t)
		{
			t -= 1f;
			return Mathf.Sqrt(1f - t * t);
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000CA398 File Offset: 0x000C8598
		public static float InOutCirc(float t)
		{
			t *= 2f;
			bool flag = t < 1f;
			float num;
			if (flag)
			{
				num = -0.5f * (Mathf.Sqrt(1f - t * t) - 1f);
			}
			else
			{
				t -= 2f;
				num = 0.5f * (Mathf.Sqrt(1f - t * t) + 1f);
			}
			return num;
		}

		// Token: 0x04001A8A RID: 6794
		private const float HalfPi = 1.5707964f;
	}
}
