using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000219 RID: 537
	[NativeHeader("Runtime/Math/PerlinNoise.h")]
	[NativeHeader("Runtime/Math/FloatConversion.h")]
	[NativeHeader("Runtime/Math/ColorSpaceConversion.h")]
	[Il2CppEagerStaticClassConstruction]
	public struct Mathf
	{
		// Token: 0x06001CB8 RID: 7352
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GammaToLinearSpace(float value);

		// Token: 0x06001CB9 RID: 7353
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float LinearToGammaSpace(float value);

		// Token: 0x06001CBA RID: 7354 RVA: 0x00035B40 File Offset: 0x00033D40
		[FreeFunction(IsThreadSafe = true)]
		public static Color CorrelatedColorTemperatureToRGB(float kelvin)
		{
			Color color;
			Mathf.CorrelatedColorTemperatureToRGB_Injected(kelvin, out color);
			return color;
		}

		// Token: 0x06001CBB RID: 7355
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort FloatToHalf(float val);

		// Token: 0x06001CBC RID: 7356
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float HalfToFloat(ushort val);

		// Token: 0x06001CBD RID: 7357
		[FreeFunction("PerlinNoise::NoiseNormalized", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float PerlinNoise(float x, float y);

		// Token: 0x06001CBE RID: 7358
		[FreeFunction("PerlinNoise::NoiseNormalized", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float PerlinNoise1D(float x);

		// Token: 0x06001CBF RID: 7359 RVA: 0x00035B58 File Offset: 0x00033D58
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00035B74 File Offset: 0x00033D74
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00035B90 File Offset: 0x00033D90
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00035BAC File Offset: 0x00033DAC
		public static float Asin(float f)
		{
			return (float)Math.Asin((double)f);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00035BC8 File Offset: 0x00033DC8
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00035BE4 File Offset: 0x00033DE4
		public static float Atan(float f)
		{
			return (float)Math.Atan((double)f);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00035C00 File Offset: 0x00033E00
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00035C1C File Offset: 0x00033E1C
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00035C38 File Offset: 0x00033E38
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00035C50 File Offset: 0x00033E50
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00035C68 File Offset: 0x00033E68
		public static float Min(float a, float b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00035C84 File Offset: 0x00033E84
		public static float Min(params float[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] < num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00035CDC File Offset: 0x00033EDC
		public static int Min(int a, int b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00035CF8 File Offset: 0x00033EF8
		public static int Min(params int[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			int num2;
			if (flag)
			{
				num2 = 0;
			}
			else
			{
				int num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] < num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00035D4C File Offset: 0x00033F4C
		public static float Max(float a, float b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00035D68 File Offset: 0x00033F68
		public static float Max(params float[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] > num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00035DC0 File Offset: 0x00033FC0
		public static int Max(int a, int b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00035DDC File Offset: 0x00033FDC
		public static int Max(params int[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			int num2;
			if (flag)
			{
				num2 = 0;
			}
			else
			{
				int num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] > num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00035E30 File Offset: 0x00034030
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00035E4C File Offset: 0x0003404C
		public static float Exp(float power)
		{
			return (float)Math.Exp((double)power);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00035E68 File Offset: 0x00034068
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00035E84 File Offset: 0x00034084
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00035EA0 File Offset: 0x000340A0
		public static float Log10(float f)
		{
			return (float)Math.Log10((double)f);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00035EBC File Offset: 0x000340BC
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00035ED8 File Offset: 0x000340D8
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00035EF4 File Offset: 0x000340F4
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00035F10 File Offset: 0x00034110
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00035F2C File Offset: 0x0003412C
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00035F48 File Offset: 0x00034148
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00035F64 File Offset: 0x00034164
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sign(float f)
		{
			return (f >= 0f) ? 1f : (-1f);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00035F8C File Offset: 0x0003418C
		public static float Clamp(float value, float min, float max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00035FB8 File Offset: 0x000341B8
		public static int Clamp(int value, int min, int max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00035FE4 File Offset: 0x000341E4
		public static float Clamp01(float value)
		{
			bool flag = value < 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = value > 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					num = value;
				}
			}
			return num;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00036020 File Offset: 0x00034220
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Mathf.Clamp01(t);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00036040 File Offset: 0x00034240
		public static float LerpUnclamped(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0003605C File Offset: 0x0003425C
		public static float LerpAngle(float a, float b, float t)
		{
			float num = Mathf.Repeat(b - a, 360f);
			bool flag = num > 180f;
			if (flag)
			{
				num -= 360f;
			}
			return a + num * Mathf.Clamp01(t);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0003609C File Offset: 0x0003429C
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			bool flag = Mathf.Abs(target - current) <= maxDelta;
			float num;
			if (flag)
			{
				num = target;
			}
			else
			{
				num = current + Mathf.Sign(target - current) * maxDelta;
			}
			return num;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000360D0 File Offset: 0x000342D0
		public static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			float num = Mathf.DeltaAngle(current, target);
			bool flag = -maxDelta < num && num < maxDelta;
			float num2;
			if (flag)
			{
				num2 = target;
			}
			else
			{
				target = current + num;
				num2 = Mathf.MoveTowards(current, target, maxDelta);
			}
			return num2;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0003610C File Offset: 0x0003430C
		public static float SmoothStep(float from, float to, float t)
		{
			t = Mathf.Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0003614C File Offset: 0x0003434C
		public static float Gamma(float value, float absmax, float gamma)
		{
			bool flag = value < 0f;
			float num = Mathf.Abs(value);
			bool flag2 = num > absmax;
			float num2;
			if (flag2)
			{
				num2 = (flag ? (-num) : num);
			}
			else
			{
				float num3 = Mathf.Pow(num / absmax, gamma) * absmax;
				num2 = (flag ? (-num3) : num3);
			}
			return num2;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00036198 File Offset: 0x00034398
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000361DC File Offset: 0x000343DC
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00036200 File Offset: 0x00034400
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0003622C File Offset: 0x0003442C
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			bool flag = num5 - current > 0f == num8 > num5;
			if (flag)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000362E8 File Offset: 0x000344E8
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0003630C File Offset: 0x0003450C
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00036338 File Offset: 0x00034538
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00036364 File Offset: 0x00034564
		public static float Repeat(float t, float length)
		{
			return Mathf.Clamp(t - Mathf.Floor(t / length) * length, 0f, length);
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00036390 File Offset: 0x00034590
		public static float PingPong(float t, float length)
		{
			t = Mathf.Repeat(t, length * 2f);
			return length - Mathf.Abs(t - length);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x000363BC File Offset: 0x000345BC
		public static float InverseLerp(float a, float b, float value)
		{
			bool flag = a != b;
			float num;
			if (flag)
			{
				num = Mathf.Clamp01((value - a) / (b - a));
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x000363F0 File Offset: 0x000345F0
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 360f);
			bool flag = num > 180f;
			if (flag)
			{
				num -= 360f;
			}
			return num;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00036428 File Offset: 0x00034628
		internal static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool flag = num5 == 0f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				result.x = p1.x + num8 * num;
				result.y = p1.y + num8 * num2;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000364E4 File Offset: 0x000346E4
		internal static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool flag = num5 == 0f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				bool flag3 = num8 < 0f || num8 > 1f;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					float num9 = (num6 * num2 - num7 * num) / num5;
					bool flag4 = num9 < 0f || num9 > 1f;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						result.x = p1.x + num8 * num;
						result.y = p1.y + num8 * num2;
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000365F4 File Offset: 0x000347F4
		internal static long RandomToLong(Random r)
		{
			byte[] array = new byte[8];
			r.NextBytes(array);
			return (long)(BitConverter.ToUInt64(array, 0) & 9223372036854775807UL);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00036628 File Offset: 0x00034828
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static float ClampToFloat(double value)
		{
			bool flag = double.IsPositiveInfinity(value);
			float num;
			if (flag)
			{
				num = float.PositiveInfinity;
			}
			else
			{
				bool flag2 = double.IsNegativeInfinity(value);
				if (flag2)
				{
					num = float.NegativeInfinity;
				}
				else
				{
					bool flag3 = value < -3.4028234663852886E+38;
					if (flag3)
					{
						num = float.MinValue;
					}
					else
					{
						bool flag4 = value > 3.4028234663852886E+38;
						if (flag4)
						{
							num = float.MaxValue;
						}
						else
						{
							num = (float)value;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x00036694 File Offset: 0x00034894
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
		internal static int ClampToInt(long value)
		{
			bool flag = value < -2147483648L;
			int num;
			if (flag)
			{
				num = int.MinValue;
			}
			else
			{
				bool flag2 = value > 2147483647L;
				if (flag2)
				{
					num = int.MaxValue;
				}
				else
				{
					num = (int)value;
				}
			}
			return num;
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000366D4 File Offset: 0x000348D4
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static uint ClampToUInt(long value)
		{
			bool flag = value < 0L;
			uint num;
			if (flag)
			{
				num = 0U;
			}
			else
			{
				bool flag2 = value > (long)((ulong)(-1));
				if (flag2)
				{
					num = uint.MaxValue;
				}
				else
				{
					num = (uint)value;
				}
			}
			return num;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x00036704 File Offset: 0x00034904
		internal static float RoundToMultipleOf(float value, float roundingValue)
		{
			bool flag = roundingValue == 0f;
			float num;
			if (flag)
			{
				num = value;
			}
			else
			{
				num = Mathf.Round(value / roundingValue) * roundingValue;
			}
			return num;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00036730 File Offset: 0x00034930
		internal static float GetClosestPowerOfTen(float positiveNumber)
		{
			bool flag = positiveNumber <= 0f;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = Mathf.Pow(10f, (float)Mathf.RoundToInt(Mathf.Log10(positiveNumber)));
			}
			return num;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00036770 File Offset: 0x00034970
		internal static int GetNumberOfDecimalsForMinimumDifference(float minDifference)
		{
			return Mathf.Clamp(-Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(minDifference))), 0, 15);
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0003679C File Offset: 0x0003499C
		internal static int GetNumberOfDecimalsForMinimumDifference(double minDifference)
		{
			return (int)Math.Max(0.0, -Math.Floor(Math.Log10(Math.Abs(minDifference))));
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000367D0 File Offset: 0x000349D0
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static float RoundBasedOnMinimumDifference(float valueToRound, float minDifference)
		{
			bool flag = minDifference == 0f;
			float num;
			if (flag)
			{
				num = Mathf.DiscardLeastSignificantDecimal(valueToRound);
			}
			else
			{
				num = (float)Math.Round((double)valueToRound, Mathf.GetNumberOfDecimalsForMinimumDifference(minDifference), MidpointRounding.AwayFromZero);
			}
			return num;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00036808 File Offset: 0x00034A08
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static double RoundBasedOnMinimumDifference(double valueToRound, double minDifference)
		{
			bool flag = minDifference == 0.0;
			double num;
			if (flag)
			{
				num = Mathf.DiscardLeastSignificantDecimal(valueToRound);
			}
			else
			{
				num = Math.Round(valueToRound, Mathf.GetNumberOfDecimalsForMinimumDifference(minDifference), MidpointRounding.AwayFromZero);
			}
			return num;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00036840 File Offset: 0x00034A40
		internal static float DiscardLeastSignificantDecimal(float v)
		{
			int num = Mathf.Clamp((int)(5f - Mathf.Log10(Mathf.Abs(v))), 0, 15);
			return (float)Math.Round((double)v, num, MidpointRounding.AwayFromZero);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00036878 File Offset: 0x00034A78
		internal static double DiscardLeastSignificantDecimal(double v)
		{
			int num = Math.Max(0, (int)(5.0 - Math.Log10(Math.Abs(v))));
			double num2;
			try
			{
				num2 = Math.Round(v, num);
			}
			catch (ArgumentOutOfRangeException)
			{
				num2 = 0.0;
			}
			return num2;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000368D0 File Offset: 0x00034AD0
		public static int NextPowerOfTwo(int value)
		{
			value--;
			value |= value >> 16;
			value |= value >> 8;
			value |= value >> 4;
			value |= value >> 2;
			value |= value >> 1;
			return value + 1;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00036910 File Offset: 0x00034B10
		public static int ClosestPowerOfTwo(int value)
		{
			int num = Mathf.NextPowerOfTwo(value);
			int num2 = num >> 1;
			bool flag = value - num2 < num - value;
			int num3;
			if (flag)
			{
				num3 = num2;
			}
			else
			{
				num3 = num;
			}
			return num3;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00036940 File Offset: 0x00034B40
		public static bool IsPowerOfTwo(int value)
		{
			return (value & (value - 1)) == 0;
		}

		// Token: 0x06001D04 RID: 7428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CorrelatedColorTemperatureToRGB_Injected(float kelvin, out Color ret);

		// Token: 0x040008A6 RID: 2214
		public const float PI = 3.1415927f;

		// Token: 0x040008A7 RID: 2215
		public const float Infinity = float.PositiveInfinity;

		// Token: 0x040008A8 RID: 2216
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x040008A9 RID: 2217
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x040008AA RID: 2218
		public const float Rad2Deg = 57.29578f;

		// Token: 0x040008AB RID: 2219
		internal const int kMaxDecimals = 15;

		// Token: 0x040008AC RID: 2220
		public static readonly float Epsilon = (MathfInternal.IsFlushToZeroEnabled ? MathfInternal.FloatMinNormal : MathfInternal.FloatMinDenormal);
	}
}
