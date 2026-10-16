using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.QupKit
{
	// Token: 0x020003E2 RID: 994
	public static class Ease
	{
		// Token: 0x06002548 RID: 9544 RVA: 0x000BDE0C File Offset: 0x000BC00C
		public static void Start(MonoBehaviour o, float start, float end, float t, UnityAction<float> update, UnityAction complete, Ease.Type type)
		{
			if (Ease.Routines.ContainsKey(o) && Ease.Routines[o] != null)
			{
				o.StopCoroutine(Ease.Routines[o]);
				Ease.Routines[o] = null;
			}
			else
			{
				Ease.Routines.Add(o, null);
			}
			o.StopAllCoroutines();
			Ease.Routines.Add(o, o.StartCoroutine(Ease.TweenCoroutine(o, start, end, t, update, complete, type)));
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000BDE84 File Offset: 0x000BC084
		private static IEnumerator TweenCoroutine(MonoBehaviour o, float start, float end, float t, UnityAction<float> update, UnityAction complete, Ease.Type type)
		{
			float i = 0f;
			while (i <= 1f)
			{
				i += Time.deltaTime / t;
				update(Ease._types[type](start, end, i));
				yield return null;
			}
			if (complete != null)
			{
				complete();
			}
			Ease.Routines.Remove(o);
			yield break;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000BDEC0 File Offset: 0x000BC0C0
		private static float Hermite(float start, float end, float t)
		{
			return Mathf.Lerp(start, end, t * t * (3f - 2f * t));
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000BDEDA File Offset: 0x000BC0DA
		private static float Sinerp(float start, float end, float t)
		{
			return Mathf.Lerp(start, end, Mathf.Sin(t * 3.1415927f * 0.5f));
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000BDEF5 File Offset: 0x000BC0F5
		private static float Coserp(float start, float end, float t)
		{
			return Mathf.Lerp(start, end, 1f - Mathf.Cos(t * 3.1415927f * 0.5f));
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000BDF18 File Offset: 0x000BC118
		private static float Spring(float start, float end, float t)
		{
			t = Mathf.Clamp01(t);
			t = (Mathf.Sin(t * 3.1415927f * (0.2f + 2.5f * t * t * t)) * Mathf.Pow(1f - t, 2.2f) + t) * (1f + 1.2f * (1f - t));
			return start + (end - start) * t;
		}

		// Token: 0x0400129B RID: 4763
		private static Dictionary<MonoBehaviour, Coroutine> Routines = new Dictionary<MonoBehaviour, Coroutine>();

		// Token: 0x0400129C RID: 4764
		private static Dictionary<Ease.Type, Ease.EaseHandler> _types = new Dictionary<Ease.Type, Ease.EaseHandler>
		{
			{
				Ease.Type.Linear,
				new Ease.EaseHandler(Mathf.Lerp)
			},
			{
				Ease.Type.Hermite,
				new Ease.EaseHandler(Ease.Hermite)
			},
			{
				Ease.Type.Sinerp,
				new Ease.EaseHandler(Ease.Sinerp)
			},
			{
				Ease.Type.Coserp,
				new Ease.EaseHandler(Ease.Coserp)
			},
			{
				Ease.Type.Spring,
				new Ease.EaseHandler(Ease.Spring)
			}
		};

		// Token: 0x020017AB RID: 6059
		public enum Type
		{
			// Token: 0x04005F39 RID: 24377
			Linear,
			// Token: 0x04005F3A RID: 24378
			Hermite,
			// Token: 0x04005F3B RID: 24379
			Sinerp,
			// Token: 0x04005F3C RID: 24380
			Coserp,
			// Token: 0x04005F3D RID: 24381
			Spring
		}

		// Token: 0x020017AC RID: 6060
		// (Invoke) Token: 0x0600DE40 RID: 56896
		private delegate float EaseHandler(float start, float end, float t);
	}
}
