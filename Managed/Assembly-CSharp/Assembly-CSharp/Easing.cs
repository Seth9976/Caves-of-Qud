using System;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public static class Easing
{
	// Token: 0x06000584 RID: 1412 RVA: 0x00024534 File Offset: 0x00022734
	public static float Interpolate(float p, Easing.Functions function)
	{
		switch (function)
		{
		default:
			return Easing.Linear(p);
		case Easing.Functions.QuadraticEaseIn:
			return Easing.QuadraticEaseIn(p);
		case Easing.Functions.QuadraticEaseOut:
			return Easing.QuadraticEaseOut(p);
		case Easing.Functions.QuadraticEaseInOut:
			return Easing.QuadraticEaseInOut(p);
		case Easing.Functions.CubicEaseIn:
			return Easing.CubicEaseIn(p);
		case Easing.Functions.CubicEaseOut:
			return Easing.CubicEaseOut(p);
		case Easing.Functions.CubicEaseInOut:
			return Easing.CubicEaseInOut(p);
		case Easing.Functions.QuarticEaseIn:
			return Easing.QuarticEaseIn(p);
		case Easing.Functions.QuarticEaseOut:
			return Easing.QuarticEaseOut(p);
		case Easing.Functions.QuarticEaseInOut:
			return Easing.QuarticEaseInOut(p);
		case Easing.Functions.QuinticEaseIn:
			return Easing.QuinticEaseIn(p);
		case Easing.Functions.QuinticEaseOut:
			return Easing.QuinticEaseOut(p);
		case Easing.Functions.QuinticEaseInOut:
			return Easing.QuinticEaseInOut(p);
		case Easing.Functions.SineEaseIn:
			return Easing.SineEaseIn(p);
		case Easing.Functions.SineEaseOut:
			return Easing.SineEaseOut(p);
		case Easing.Functions.SineEaseInOut:
			return Easing.SineEaseInOut(p);
		case Easing.Functions.CircularEaseIn:
			return Easing.CircularEaseIn(p);
		case Easing.Functions.CircularEaseOut:
			return Easing.CircularEaseOut(p);
		case Easing.Functions.CircularEaseInOut:
			return Easing.CircularEaseInOut(p);
		case Easing.Functions.ExponentialEaseIn:
			return Easing.ExponentialEaseIn(p);
		case Easing.Functions.ExponentialEaseOut:
			return Easing.ExponentialEaseOut(p);
		case Easing.Functions.ExponentialEaseInOut:
			return Easing.ExponentialEaseInOut(p);
		case Easing.Functions.ElasticEaseIn:
			return Easing.ElasticEaseIn(p);
		case Easing.Functions.ElasticEaseOut:
			return Easing.ElasticEaseOut(p);
		case Easing.Functions.ElasticEaseInOut:
			return Easing.ElasticEaseInOut(p);
		case Easing.Functions.BackEaseIn:
			return Easing.BackEaseIn(p);
		case Easing.Functions.BackEaseOut:
			return Easing.BackEaseOut(p);
		case Easing.Functions.BackEaseInOut:
			return Easing.BackEaseInOut(p);
		case Easing.Functions.BounceEaseIn:
			return Easing.BounceEaseIn(p);
		case Easing.Functions.BounceEaseOut:
			return Easing.BounceEaseOut(p);
		case Easing.Functions.BounceEaseInOut:
			return Easing.BounceEaseInOut(p);
		}
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0002469B File Offset: 0x0002289B
	public static float Linear(float p)
	{
		return p;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0002469E File Offset: 0x0002289E
	public static float QuadraticEaseIn(float p)
	{
		return p * p;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x000246A3 File Offset: 0x000228A3
	public static float QuadraticEaseOut(float p)
	{
		return -(p * (p - 2f));
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x000246AF File Offset: 0x000228AF
	public static float QuadraticEaseInOut(float p)
	{
		if (p < 0.5f)
		{
			return 2f * p * p;
		}
		return -2f * p * p + 4f * p - 1f;
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x000246DA File Offset: 0x000228DA
	public static float CubicEaseIn(float p)
	{
		return p * p * p;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x000246E4 File Offset: 0x000228E4
	public static float CubicEaseOut(float p)
	{
		float num = p - 1f;
		return num * num * num + 1f;
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00024704 File Offset: 0x00022904
	public static float CubicEaseInOut(float p)
	{
		if (p < 0.5f)
		{
			return 4f * p * p * p;
		}
		float num = 2f * p - 2f;
		return 0.5f * num * num * num + 1f;
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00024744 File Offset: 0x00022944
	public static float QuarticEaseIn(float p)
	{
		return p * p * p * p;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00024750 File Offset: 0x00022950
	public static float QuarticEaseOut(float p)
	{
		float num = p - 1f;
		return num * num * num * (1f - p) + 1f;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00024778 File Offset: 0x00022978
	public static float QuarticEaseInOut(float p)
	{
		if (p < 0.5f)
		{
			return 8f * p * p * p * p;
		}
		float num = p - 1f;
		return -8f * num * num * num * num + 1f;
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x000247B6 File Offset: 0x000229B6
	public static float QuinticEaseIn(float p)
	{
		return p * p * p * p * p;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x000247C4 File Offset: 0x000229C4
	public static float QuinticEaseOut(float p)
	{
		float num = p - 1f;
		return num * num * num * num * num + 1f;
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x000247E8 File Offset: 0x000229E8
	public static float QuinticEaseInOut(float p)
	{
		if (p < 0.5f)
		{
			return 16f * p * p * p * p * p;
		}
		float num = 2f * p - 2f;
		return 0.5f * num * num * num * num * num + 1f;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00024830 File Offset: 0x00022A30
	public static float SineEaseIn(float p)
	{
		return Mathf.Sin((p - 1f) * 1.5707964f) + 1f;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0002484A File Offset: 0x00022A4A
	public static float SineEaseOut(float p)
	{
		return Mathf.Sin(p * 1.5707964f);
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00024858 File Offset: 0x00022A58
	public static float SineEaseInOut(float p)
	{
		return 0.5f * (1f - Mathf.Cos(p * 3.1415927f));
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00024872 File Offset: 0x00022A72
	public static float CircularEaseIn(float p)
	{
		return 1f - Mathf.Sqrt(1f - p * p);
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x00024888 File Offset: 0x00022A88
	public static float CircularEaseOut(float p)
	{
		return Mathf.Sqrt((2f - p) * p);
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00024898 File Offset: 0x00022A98
	public static float CircularEaseInOut(float p)
	{
		if (p < 0.5f)
		{
			return 0.5f * (1f - Mathf.Sqrt(1f - 4f * (p * p)));
		}
		return 0.5f * (Mathf.Sqrt(-(2f * p - 3f) * (2f * p - 1f)) + 1f);
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x000248FB File Offset: 0x00022AFB
	public static float ExponentialEaseIn(float p)
	{
		if (p != 0f)
		{
			return Mathf.Pow(2f, 10f * (p - 1f));
		}
		return p;
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0002491E File Offset: 0x00022B1E
	public static float ExponentialEaseOut(float p)
	{
		if (p != 1f)
		{
			return 1f - Mathf.Pow(2f, -10f * p);
		}
		return p;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00024944 File Offset: 0x00022B44
	public static float ExponentialEaseInOut(float p)
	{
		if ((double)p == 0.0 || (double)p == 1.0)
		{
			return p;
		}
		if (p < 0.5f)
		{
			return 0.5f * Mathf.Pow(2f, 20f * p - 10f);
		}
		return -0.5f * Mathf.Pow(2f, -20f * p + 10f) + 1f;
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x000249B6 File Offset: 0x00022BB6
	public static float ElasticEaseIn(float p)
	{
		return Mathf.Sin(20.420353f * p) * Mathf.Pow(2f, 10f * (p - 1f));
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x000249DC File Offset: 0x00022BDC
	public static float ElasticEaseOut(float p)
	{
		return Mathf.Sin(-20.420353f * (p + 1f)) * Mathf.Pow(2f, -10f * p) + 1f;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00024A08 File Offset: 0x00022C08
	public static float ElasticEaseInOut(float p)
	{
		if (p < 0.5f)
		{
			return 0.5f * Mathf.Sin(20.420353f * (2f * p)) * Mathf.Pow(2f, 10f * (2f * p - 1f));
		}
		return 0.5f * (Mathf.Sin(-20.420353f * (2f * p - 1f + 1f)) * Mathf.Pow(2f, -10f * (2f * p - 1f)) + 2f);
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00024A9C File Offset: 0x00022C9C
	public static float BackEaseIn(float p)
	{
		return p * p * p - p * Mathf.Sin(p * 3.1415927f);
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00024AB4 File Offset: 0x00022CB4
	public static float BackEaseOut(float p)
	{
		float num = 1f - p;
		return 1f - (num * num * num - num * Mathf.Sin(num * 3.1415927f));
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00024AE4 File Offset: 0x00022CE4
	public static float BackEaseInOut(float p)
	{
		if (p < 0.5f)
		{
			float num = 2f * p;
			return 0.5f * (num * num * num - num * Mathf.Sin(num * 3.1415927f));
		}
		float num2 = 1f - (2f * p - 1f);
		return 0.5f * (1f - (num2 * num2 * num2 - num2 * Mathf.Sin(num2 * 3.1415927f))) + 0.5f;
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00024B56 File Offset: 0x00022D56
	public static float BounceEaseIn(float p)
	{
		return 1f - Easing.BounceEaseOut(1f - p);
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00024B6C File Offset: 0x00022D6C
	public static float BounceEaseOut(float p)
	{
		if (p < 0.36363637f)
		{
			return 121f * p * p / 16f;
		}
		if (p < 0.72727275f)
		{
			return 9.075f * p * p - 9.9f * p + 3.4f;
		}
		if (p < 0.9f)
		{
			return 12.066482f * p * p - 19.635458f * p + 8.898061f;
		}
		return 10.8f * p * p - 20.52f * p + 10.72f;
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x00024BE8 File Offset: 0x00022DE8
	public static float BounceEaseInOut(float p)
	{
		if (p < 0.5f)
		{
			return 0.5f * Easing.BounceEaseIn(p * 2f);
		}
		return 0.5f * Easing.BounceEaseOut(p * 2f - 1f) + 0.5f;
	}

	// Token: 0x0400043C RID: 1084
	private const float PI = 3.1415927f;

	// Token: 0x0400043D RID: 1085
	private const float HALFPI = 1.5707964f;

	// Token: 0x020015AD RID: 5549
	public enum Functions
	{
		// Token: 0x0400572C RID: 22316
		Linear,
		// Token: 0x0400572D RID: 22317
		QuadraticEaseIn,
		// Token: 0x0400572E RID: 22318
		QuadraticEaseOut,
		// Token: 0x0400572F RID: 22319
		QuadraticEaseInOut,
		// Token: 0x04005730 RID: 22320
		CubicEaseIn,
		// Token: 0x04005731 RID: 22321
		CubicEaseOut,
		// Token: 0x04005732 RID: 22322
		CubicEaseInOut,
		// Token: 0x04005733 RID: 22323
		QuarticEaseIn,
		// Token: 0x04005734 RID: 22324
		QuarticEaseOut,
		// Token: 0x04005735 RID: 22325
		QuarticEaseInOut,
		// Token: 0x04005736 RID: 22326
		QuinticEaseIn,
		// Token: 0x04005737 RID: 22327
		QuinticEaseOut,
		// Token: 0x04005738 RID: 22328
		QuinticEaseInOut,
		// Token: 0x04005739 RID: 22329
		SineEaseIn,
		// Token: 0x0400573A RID: 22330
		SineEaseOut,
		// Token: 0x0400573B RID: 22331
		SineEaseInOut,
		// Token: 0x0400573C RID: 22332
		CircularEaseIn,
		// Token: 0x0400573D RID: 22333
		CircularEaseOut,
		// Token: 0x0400573E RID: 22334
		CircularEaseInOut,
		// Token: 0x0400573F RID: 22335
		ExponentialEaseIn,
		// Token: 0x04005740 RID: 22336
		ExponentialEaseOut,
		// Token: 0x04005741 RID: 22337
		ExponentialEaseInOut,
		// Token: 0x04005742 RID: 22338
		ElasticEaseIn,
		// Token: 0x04005743 RID: 22339
		ElasticEaseOut,
		// Token: 0x04005744 RID: 22340
		ElasticEaseInOut,
		// Token: 0x04005745 RID: 22341
		BackEaseIn,
		// Token: 0x04005746 RID: 22342
		BackEaseOut,
		// Token: 0x04005747 RID: 22343
		BackEaseInOut,
		// Token: 0x04005748 RID: 22344
		BounceEaseIn,
		// Token: 0x04005749 RID: 22345
		BounceEaseOut,
		// Token: 0x0400574A RID: 22346
		BounceEaseInOut
	}
}
