using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedShadow.Tween
{
	// Token: 0x020001DF RID: 479
	public class Tween<T> : CustomYieldInstruction
	{
		// Token: 0x0600113A RID: 4410 RVA: 0x000684AA File Offset: 0x000666AA
		private static Vector3 Identity(Vector3 v)
		{
			return v;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000684AD File Offset: 0x000666AD
		private static Vector3 TransformDotPosition(Transform t)
		{
			return t.position;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000684B5 File Offset: 0x000666B5
		private static IEnumerable<float> NewCounter(int start, int end, int step)
		{
			for (int i = start; i <= end; i += step)
			{
				yield return (float)i;
			}
			yield break;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000684D3 File Offset: 0x000666D3
		private static IEnumerable<float> NewTimer(float duration)
		{
			float elapsedTime = 0f;
			while (elapsedTime < duration)
			{
				yield return elapsedTime;
				elapsedTime += Time.deltaTime;
				if (elapsedTime >= duration)
				{
					yield return elapsedTime;
				}
			}
			yield break;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000684E4 File Offset: 0x000666E4
		public static IEnumerable<Vector3> NewBezier(Tween<T>.EaseFunction ease, Transform[] nodes, float duration)
		{
			IEnumerable<float> enumerable = Tween<T>.NewTimer(duration);
			return Tween<T>.NewBezier<Transform>(ease, nodes, new Tween<T>.ToVector3<Transform>(Tween<T>.TransformDotPosition), duration, enumerable);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00068510 File Offset: 0x00066710
		public static IEnumerable<Vector3> NewBezier(Tween<T>.EaseFunction ease, Transform[] nodes, int slices)
		{
			IEnumerable<float> enumerable = Tween<T>.NewCounter(0, slices + 1, 1);
			return Tween<T>.NewBezier<Transform>(ease, nodes, new Tween<T>.ToVector3<Transform>(Tween<T>.TransformDotPosition), (float)(slices + 1), enumerable);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00068540 File Offset: 0x00066740
		public static IEnumerable<Vector3> NewBezier(Tween<T>.EaseFunction ease, Vector3[] points, float duration)
		{
			IEnumerable<float> enumerable = Tween<T>.NewTimer(duration);
			return Tween<T>.NewBezier<Vector3>(ease, points, new Tween<T>.ToVector3<Vector3>(Tween<T>.Identity), duration, enumerable);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0006856C File Offset: 0x0006676C
		public static IEnumerable<Vector3> NewBezier(Tween<T>.EaseFunction ease, Vector3[] points, int slices)
		{
			IEnumerable<float> enumerable = Tween<T>.NewCounter(0, slices + 1, 1);
			return Tween<T>.NewBezier<Vector3>(ease, points, new Tween<T>.ToVector3<Vector3>(Tween<T>.Identity), (float)(slices + 1), enumerable);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0006859C File Offset: 0x0006679C
		private static IEnumerable<Vector3> NewBezier<T2>(Tween<T>.EaseFunction ease, IList nodes, Tween<T>.ToVector3<T2> toVector3, float maxStep, IEnumerable<float> steps)
		{
			if (nodes.Count >= 2)
			{
				Vector3[] points = new Vector3[nodes.Count];
				foreach (float num in steps)
				{
					for (int i = 0; i < nodes.Count; i++)
					{
						points[i] = toVector3((T2)((object)nodes[i]));
					}
					yield return Tween<T>.Bezier(ease, points, num, maxStep);
				}
				IEnumerator<float> enumerator = null;
				points = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000685CC File Offset: 0x000667CC
		private static Vector3 Bezier(Tween<T>.EaseFunction ease, Vector3[] points, float elapsedTime, float duration)
		{
			for (int i = points.Length - 1; i > 0; i--)
			{
				for (int j = 0; j < i; j++)
				{
					points[j].x = ease(points[j].x, points[j + 1].x - points[j].x, elapsedTime, duration);
					points[j].y = ease(points[j].y, points[j + 1].y - points[j].y, elapsedTime, duration);
					points[j].z = ease(points[j].z, points[j + 1].z - points[j].z, elapsedTime, duration);
				}
			}
			return points[0];
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000686B9 File Offset: 0x000668B9
		public static IEnumerable<Vector3> NewCatmullRom(Transform[] nodes, int slices, bool loop)
		{
			return Tween<T>.NewCatmullRom<Transform>(nodes, new Tween<T>.ToVector3<Transform>(Tween<T>.TransformDotPosition), slices, loop);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000686CF File Offset: 0x000668CF
		public static IEnumerable<Vector3> NewCatmullRom(Vector3[] points, int slices, bool loop)
		{
			return Tween<T>.NewCatmullRom<Vector3>(points, new Tween<T>.ToVector3<Vector3>(Tween<T>.Identity), slices, loop);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000686E5 File Offset: 0x000668E5
		private static IEnumerable<Vector3> NewCatmullRom<T2>(IList nodes, Tween<T>.ToVector3<T2> toVector3, int slices, bool loop)
		{
			if (nodes.Count >= 2)
			{
				yield return toVector3((T2)((object)nodes[0]));
				int last = nodes.Count - 1;
				int current = 0;
				while (loop || current < last)
				{
					if (loop && current > last)
					{
						current = 0;
					}
					int previous = ((current == 0) ? (loop ? last : current) : (current - 1));
					int start = current;
					int end = ((current == last) ? (loop ? 0 : current) : (current + 1));
					int next = ((end == last) ? (loop ? 0 : end) : (end + 1));
					int stepCount = slices + 1;
					int num;
					for (int step = 1; step <= stepCount; step = num + 1)
					{
						yield return Tween<T>.CatmullRom(toVector3((T2)((object)nodes[previous])), toVector3((T2)((object)nodes[start])), toVector3((T2)((object)nodes[end])), toVector3((T2)((object)nodes[next])), (float)step, (float)stepCount);
						num = step;
					}
					num = current;
					current = num + 1;
				}
			}
			yield break;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0006870C File Offset: 0x0006690C
		private static Vector3 CatmullRom(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime, float duration)
		{
			float num = elapsedTime / duration;
			float num2 = num * num;
			float num3 = num2 * num;
			return previous * (-0.5f * num3 + num2 - 0.5f * num) + start * (1.5f * num3 + -2.5f * num2 + 1f) + end * (-1.5f * num3 + 2f * num2 + 0.5f * num) + next * (0.5f * num3 - 0.5f * num2);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0006879A File Offset: 0x0006699A
		public static float interp(EaseType type, float elapsedTime, float duration)
		{
			if (Tween<T>.EaseFunctionMapping.Count == 0)
			{
				Tween<T>.initializeEaseTypes();
			}
			return Tween<T>.EaseFunctionMapping[type](0f, 1f, elapsedTime, duration);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000687C9 File Offset: 0x000669C9
		public static Tween<T>.EaseFunction getEaseFunction(EaseType type)
		{
			if (Tween<T>.EaseFunctionMapping.Count == 0)
			{
				Tween<T>.initializeEaseTypes();
			}
			return Tween<T>.EaseFunctionMapping[type];
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000687E8 File Offset: 0x000669E8
		private static void initializeEaseTypes()
		{
			Tween<T>.EaseFunctionMapping[EaseType.Linear] = new Tween<T>.EaseFunction(Tween<T>.linear);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInQuad] = new Tween<T>.EaseFunction(Tween<T>.easeInQuad);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutQuad] = new Tween<T>.EaseFunction(Tween<T>.easeOutQuad);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutQuad] = new Tween<T>.EaseFunction(Tween<T>.easeInOutQuad);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInCubic] = new Tween<T>.EaseFunction(Tween<T>.easeInCubic);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutCubic] = new Tween<T>.EaseFunction(Tween<T>.easeOutCubic);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutCubic] = new Tween<T>.EaseFunction(Tween<T>.easeInOutCubic);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInQuart] = new Tween<T>.EaseFunction(Tween<T>.easeInQuart);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutQuart] = new Tween<T>.EaseFunction(Tween<T>.easeOutQuart);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutQuart] = new Tween<T>.EaseFunction(Tween<T>.easeInOutQuart);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInQuint] = new Tween<T>.EaseFunction(Tween<T>.easeInQuint);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutQuint] = new Tween<T>.EaseFunction(Tween<T>.easeOutQuint);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutQuint] = new Tween<T>.EaseFunction(Tween<T>.easeInOutQuint);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInSine] = new Tween<T>.EaseFunction(Tween<T>.easeInSine);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutSine] = new Tween<T>.EaseFunction(Tween<T>.easeOutSine);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutSine] = new Tween<T>.EaseFunction(Tween<T>.easeInOutSine);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInExpo] = new Tween<T>.EaseFunction(Tween<T>.easeInExpo);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutExpo] = new Tween<T>.EaseFunction(Tween<T>.easeOutExpo);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutExpo] = new Tween<T>.EaseFunction(Tween<T>.easeInOutExpo);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInCirc] = new Tween<T>.EaseFunction(Tween<T>.easeInCirc);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutCirc] = new Tween<T>.EaseFunction(Tween<T>.easeOutCirc);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutCirc] = new Tween<T>.EaseFunction(Tween<T>.easeInOutCirc);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInElastic] = new Tween<T>.EaseFunction(Tween<T>.easeInElastic);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutElastic] = new Tween<T>.EaseFunction(Tween<T>.easeOutElastic);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutElastic] = new Tween<T>.EaseFunction(Tween<T>.easeInOutElastic);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInBounce] = new Tween<T>.EaseFunction(Tween<T>.easeInBounce);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutBounce] = new Tween<T>.EaseFunction(Tween<T>.easeOutBounce);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutBounce] = new Tween<T>.EaseFunction(Tween<T>.easeInOutBounce);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInBack] = new Tween<T>.EaseFunction(Tween<T>.easeInBack);
			Tween<T>.EaseFunctionMapping[EaseType.EaseOutBack] = new Tween<T>.EaseFunction(Tween<T>.easeOutBack);
			Tween<T>.EaseFunctionMapping[EaseType.EaseInOutBack] = new Tween<T>.EaseFunction(Tween<T>.easeInOutBack);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00068AD4 File Offset: 0x00066CD4
		private static float linear(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			return distance * (elapsedTime / duration) + start;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00068AE4 File Offset: 0x00066CE4
		private static float easeInQuad(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			return distance * elapsedTime * elapsedTime + start;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00068AFD File Offset: 0x00066CFD
		private static float easeOutQuad(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			return -distance * elapsedTime * (elapsedTime - 2f) + start;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00068B20 File Offset: 0x00066D20
		private static float easeInOutQuad(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 2f : (elapsedTime / (duration / 2f)));
			if (elapsedTime < 1f)
			{
				return distance / 2f * elapsedTime * elapsedTime + start;
			}
			elapsedTime -= 1f;
			return -distance / 2f * (elapsedTime * (elapsedTime - 2f) - 1f) + start;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00068B7C File Offset: 0x00066D7C
		private static float easeInCubic(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			return distance * elapsedTime * elapsedTime * elapsedTime + start;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00068B97 File Offset: 0x00066D97
		private static float easeOutCubic(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			elapsedTime -= 1f;
			return distance * (elapsedTime * elapsedTime * elapsedTime + 1f) + start;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00068BC4 File Offset: 0x00066DC4
		private static float easeInOutCubic(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 2f : (elapsedTime / (duration / 2f)));
			if (elapsedTime < 1f)
			{
				return distance / 2f * elapsedTime * elapsedTime * elapsedTime + start;
			}
			elapsedTime -= 2f;
			return distance / 2f * (elapsedTime * elapsedTime * elapsedTime + 2f) + start;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00068C1D File Offset: 0x00066E1D
		private static float easeInQuart(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			return distance * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00068C3A File Offset: 0x00066E3A
		private static float easeOutQuart(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			elapsedTime -= 1f;
			return -distance * (elapsedTime * elapsedTime * elapsedTime * elapsedTime - 1f) + start;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00068C68 File Offset: 0x00066E68
		private static float easeInOutQuart(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 2f : (elapsedTime / (duration / 2f)));
			if (elapsedTime < 1f)
			{
				return distance / 2f * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
			}
			elapsedTime -= 2f;
			return -distance / 2f * (elapsedTime * elapsedTime * elapsedTime * elapsedTime - 2f) + start;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00068CC6 File Offset: 0x00066EC6
		private static float easeInQuint(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			return distance * elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00068CE5 File Offset: 0x00066EE5
		private static float easeOutQuint(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			elapsedTime -= 1f;
			return distance * (elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + 1f) + start;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00068D14 File Offset: 0x00066F14
		private static float easeInOutQuint(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 2f : (elapsedTime / (duration / 2f)));
			if (elapsedTime < 1f)
			{
				return distance / 2f * elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
			}
			elapsedTime -= 2f;
			return distance / 2f * (elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + 2f) + start;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00068D75 File Offset: 0x00066F75
		private static float easeInSine(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			return -distance * Mathf.Cos(elapsedTime / duration * 1.5707964f) + distance + start;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00068D93 File Offset: 0x00066F93
		private static float easeOutSine(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			return distance * Mathf.Sin(elapsedTime / duration * 1.5707964f) + start;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00068DAE File Offset: 0x00066FAE
		private static float easeInOutSine(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			return -distance / 2f * (Mathf.Cos(3.1415927f * elapsedTime / duration) - 1f) + start;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00068DD6 File Offset: 0x00066FD6
		private static float easeInExpo(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			return distance * Mathf.Pow(2f, 10f * (elapsedTime / duration - 1f)) + start;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00068DFC File Offset: 0x00066FFC
		private static float easeOutExpo(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			return distance * (-Mathf.Pow(2f, -10f * elapsedTime / duration) + 1f) + start;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00068E24 File Offset: 0x00067024
		private static float easeInOutExpo(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 2f : (elapsedTime / (duration / 2f)));
			if (elapsedTime < 1f)
			{
				return distance / 2f * Mathf.Pow(2f, 10f * (elapsedTime - 1f)) + start;
			}
			elapsedTime -= 1f;
			return distance / 2f * (-Mathf.Pow(2f, -10f * elapsedTime) + 2f) + start;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00068E9C File Offset: 0x0006709C
		private static float easeInCirc(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			return -distance * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) - 1f) + start;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00068EC7 File Offset: 0x000670C7
		private static float easeOutCirc(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 1f : (elapsedTime / duration));
			elapsedTime -= 1f;
			return distance * Mathf.Sqrt(1f - elapsedTime * elapsedTime) + start;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00068EF4 File Offset: 0x000670F4
		private static float easeInOutCirc(float start, float distance, float elapsedTime, float duration)
		{
			elapsedTime = ((elapsedTime > duration) ? 2f : (elapsedTime / (duration / 2f)));
			if (elapsedTime < 1f)
			{
				return -distance / 2f * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) - 1f) + start;
			}
			elapsedTime -= 2f;
			return distance / 2f * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) + 1f) + start;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00068F68 File Offset: 0x00067168
		private static float easeOutElastic(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			if ((elapsedTime /= duration) == 1f)
			{
				return start + distance;
			}
			float num = duration * 0.3f;
			float num2 = num / 4f;
			return distance * Mathf.Pow(2f, -10f * elapsedTime) * Mathf.Sin((elapsedTime * duration - num2) * 6.2831855f / num) + distance + start;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00068FC8 File Offset: 0x000671C8
		private static float easeInElastic(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			if ((elapsedTime /= duration) == 1f)
			{
				return start + distance;
			}
			float num = duration * 0.3f;
			float num2 = num / 4f;
			return -(distance * Mathf.Pow(2f, 10f * (elapsedTime -= 1f)) * Mathf.Sin((elapsedTime * duration - num2) * 6.2831855f / num)) + start;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00069030 File Offset: 0x00067230
		private static float easeInOutElastic(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			if ((elapsedTime /= duration / 2f) == 2f)
			{
				return start + distance;
			}
			float num = duration * 0.45000002f;
			float num2 = num / 4f;
			if (elapsedTime < 1f)
			{
				return -0.5f * (distance * Mathf.Pow(2f, 10f * (elapsedTime -= 1f)) * Mathf.Sin((elapsedTime * duration - num2) * 6.2831855f / num)) + start;
			}
			return distance * Mathf.Pow(2f, -10f * (elapsedTime -= 1f)) * Mathf.Sin((elapsedTime * duration - num2) * 6.2831855f / num) * 0.5f + distance + start;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000690E4 File Offset: 0x000672E4
		private static float easeOutBounce(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			if ((elapsedTime /= duration) < 0.36363637f)
			{
				return distance * (7.5625f * elapsedTime * elapsedTime) + start;
			}
			if (elapsedTime < 0.72727275f)
			{
				return distance * (7.5625f * (elapsedTime -= 0.54545456f) * elapsedTime + 0.75f) + start;
			}
			if (elapsedTime < 0.90909094f)
			{
				return distance * (7.5625f * (elapsedTime -= 0.8181818f) * elapsedTime + 0.9375f) + start;
			}
			return distance * (7.5625f * (elapsedTime -= 0.95454544f) * elapsedTime + 0.984375f) + start;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00069179 File Offset: 0x00067379
		private static float easeInBounce(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			return distance - Tween<T>.easeOutBounce(0f, distance, duration - elapsedTime, duration) + start;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00069198 File Offset: 0x00067398
		private static float easeInOutBounce(float start, float distance, float elapsedTime, float duration)
		{
			if (elapsedTime > duration)
			{
				elapsedTime = duration;
			}
			if (elapsedTime < duration / 2f)
			{
				return Tween<T>.easeInBounce(0f, distance, elapsedTime * 2f, duration) * 0.5f + start;
			}
			return Tween<T>.easeOutBounce(0f, distance, elapsedTime * 2f - duration, duration) * 0.5f + distance * 0.5f + start;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000691F7 File Offset: 0x000673F7
		private static float easeOutBack(float start, float distance, float elapsedTime, float duration)
		{
			return distance * ((elapsedTime = elapsedTime / duration - 1f) * elapsedTime * (2.70158f * elapsedTime + 1.70158f) + 1f) + start;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0006921F File Offset: 0x0006741F
		private static float easeInBack(float start, float distance, float elapsedTime, float duration)
		{
			return distance * (elapsedTime /= duration) * elapsedTime * (2.70158f * elapsedTime - 1.70158f) + start;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0006923C File Offset: 0x0006743C
		private static float easeInOutBack(float start, float distance, float elapsedTime, float duration)
		{
			float num = 1.70158f;
			if ((elapsedTime /= duration / 2f) < 1f)
			{
				return distance / 2f * (elapsedTime * elapsedTime * (((num *= 1.525f) + 1f) * elapsedTime - num)) + start;
			}
			return distance / 2f * ((elapsedTime -= 2f) * elapsedTime * (((num *= 1.525f) + 1f) * elapsedTime + num) + 2f) + start;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000692B4 File Offset: 0x000674B4
		public float Progress
		{
			get
			{
				return this.getProgress();
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000692BC File Offset: 0x000674BC
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x000692C4 File Offset: 0x000674C4
		public Status Status { get; private set; }

		// Token: 0x0600116D RID: 4461 RVA: 0x000692D0 File Offset: 0x000674D0
		public Tween(T startValue, Action<object> setter, T endValue)
		{
			this._setter = setter;
			this._startValue = startValue;
			this._endValue = endValue;
			if (typeof(T) == typeof(Vector3))
			{
				this._updater = new Vector3Updater(this._setter, startValue, endValue);
			}
			else if (typeof(T) == typeof(Color))
			{
				this._updater = new ColorUpdater(this._setter, startValue, endValue);
			}
			else
			{
				if (!(typeof(T) == typeof(float)))
				{
					throw new NotImplementedException("Missing Tween Updater implementation");
				}
				this._updater = new FloatUpdater(this._setter, startValue, endValue);
			}
			this.reset();
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000693C9 File Offset: 0x000675C9
		public IEnumerator start()
		{
			this.reset();
			this.Status = Status.Playing;
			yield return this;
			yield break;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000693D8 File Offset: 0x000675D8
		public void stop()
		{
			if (this.Status == Status.Playing || this.Status == Status.Paused)
			{
				this.Status = Status.Stopped;
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000693F3 File Offset: 0x000675F3
		public void pause()
		{
			if (this.Status == Status.Playing)
			{
				this.Status = Status.Paused;
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00069405 File Offset: 0x00067605
		public void resume()
		{
			if (this.Status == Status.Paused)
			{
				this.Status = Status.Playing;
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00069418 File Offset: 0x00067618
		public void reset()
		{
			this._currentLoop = 0f;
			this._startTime = Time.time;
			this._currentTime = Time.time;
			this._currentDirection = this._direction;
			this._setter((this._currentDirection == Direction.Forward) ? this._startValue : this._endValue);
			this._easeFunction = Tween<T>.getEaseFunction(this._easeType);
			this.Status = Status.Initialized;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00069490 File Offset: 0x00067690
		public bool update()
		{
			if (this.Status == Status.Paused)
			{
				return true;
			}
			if (this.Status == Status.Stopped)
			{
				return false;
			}
			if (this.Status == Status.Initialized)
			{
				this.start();
			}
			this._currentTime += Time.deltaTime;
			while (this._currentTime - this._startTime >= this._duration)
			{
				this._currentLoop += 1f;
				if (this._currentLoop >= (float)this._loopCount)
				{
					this.Status = Status.Finished;
					this.updateValue(1f);
					return false;
				}
				this._startTime += this._duration;
				if (this._endMode == EndMode.PingPong)
				{
					this._currentDirection = ((this._direction == Direction.Forward) ? Direction.Reverse : Direction.Forward);
				}
			}
			float num = (this._currentTime - this._startTime) / this._duration;
			this.updateValue(num);
			return true;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0006956C File Offset: 0x0006776C
		private void updateValue(float progress)
		{
			if (this.Status != Status.Finished)
			{
				float num = this._easeFunction(0f, 1f, progress * this._duration, this._duration);
				if (this._currentDirection == Direction.Reverse)
				{
					num = 1f - num;
				}
				this._updater.update(num);
				return;
			}
			switch (this._endMode)
			{
			case EndMode.Stop:
			case EndMode.PingPong:
			{
				T t = ((this._currentDirection == Direction.Forward) ? this._endValue : this._startValue);
				this._setter(t);
				return;
			}
			case EndMode.Reset:
			{
				T t2 = ((this._currentDirection == Direction.Forward) ? this._startValue : this._endValue);
				this._setter(t2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00069630 File Offset: 0x00067830
		private float getProgress()
		{
			float num = this._duration * this._currentLoop + (this._currentTime - this._startTime);
			float num2 = this._duration * (float)this._loopCount;
			return num / num2;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00069669 File Offset: 0x00067869
		public override bool keepWaiting
		{
			get
			{
				return this.update();
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00069671 File Offset: 0x00067871
		public Tween<T> loopCount(int count)
		{
			this._loopCount = count;
			return this;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0006967B File Offset: 0x0006787B
		public Tween<T> endMode(EndMode endMode)
		{
			this._endMode = endMode;
			return this;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00069685 File Offset: 0x00067885
		public Tween<T> direction(Direction direction)
		{
			this._direction = direction;
			return this;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0006968F File Offset: 0x0006788F
		public Tween<T> duration(float duration)
		{
			this._duration = duration;
			return this;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00069699 File Offset: 0x00067899
		public Tween<T> easeType(EaseType easeType)
		{
			this._easeType = easeType;
			return this;
		}

		// Token: 0x04000B5B RID: 2907
		private static readonly Dictionary<EaseType, Tween<T>.EaseFunction> EaseFunctionMapping = new Dictionary<EaseType, Tween<T>.EaseFunction>();

		// Token: 0x04000B5D RID: 2909
		private int _loopCount = 1;

		// Token: 0x04000B5E RID: 2910
		private EndMode _endMode;

		// Token: 0x04000B5F RID: 2911
		private Direction _direction;

		// Token: 0x04000B60 RID: 2912
		private float _duration = 1f;

		// Token: 0x04000B61 RID: 2913
		private EaseType _easeType;

		// Token: 0x04000B62 RID: 2914
		private readonly T _startValue;

		// Token: 0x04000B63 RID: 2915
		private readonly T _endValue;

		// Token: 0x04000B64 RID: 2916
		private float _startTime;

		// Token: 0x04000B65 RID: 2917
		private float _currentTime;

		// Token: 0x04000B66 RID: 2918
		private Direction _currentDirection;

		// Token: 0x04000B67 RID: 2919
		private float _currentLoop;

		// Token: 0x04000B68 RID: 2920
		private Tween<T>.EaseFunction _easeFunction;

		// Token: 0x04000B69 RID: 2921
		private readonly Action<object> _setter;

		// Token: 0x04000B6A RID: 2922
		private readonly IUpdater _updater;

		// Token: 0x0200163A RID: 5690
		// (Invoke) Token: 0x0600D757 RID: 55127
		public delegate Vector3 ToVector3<T2>(T2 v);

		// Token: 0x0200163B RID: 5691
		// (Invoke) Token: 0x0600D75B RID: 55131
		public delegate float EaseFunction(float start, float distance, float elapsedTime, float duration);
	}
}
