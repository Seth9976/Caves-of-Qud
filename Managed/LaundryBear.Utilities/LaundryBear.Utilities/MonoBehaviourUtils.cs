using System;
using System.Collections;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000006 RID: 6
	public static class MonoBehaviourUtils
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022C4 File Offset: 0x000004C4
		public static bool TryDestroyComponent<T>(this Transform transform) where T : global::UnityEngine.Object
		{
			T component = transform.GetComponent<T>();
			if (component != null)
			{
				global::UnityEngine.Object.Destroy(component);
				return true;
			}
			return false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022F4 File Offset: 0x000004F4
		public static Coroutine ExecuteAfterTime(this MonoBehaviour behaviour, float time, Func<float> timeFunc, Action onComplete)
		{
			return behaviour.StartCoroutine(MonoBehaviourUtils.ExecuteAfterCoroutine(time, timeFunc, onComplete));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002304 File Offset: 0x00000504
		public static Coroutine ExecuteAfterTime(this MonoBehaviour behaviour, float time, Action onComplete)
		{
			return behaviour.StartCoroutine(MonoBehaviourUtils.ExecuteAfterCoroutine(time, () => Time.deltaTime, onComplete));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002332 File Offset: 0x00000532
		public static Coroutine ExecuteOverTime(this MonoBehaviour behaviour, float time, Func<float> timeFunc, Action<float> onUpdate)
		{
			return behaviour.StartCoroutine(MonoBehaviourUtils.ExecuteOverCoroutine(time, timeFunc, onUpdate));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002342 File Offset: 0x00000542
		public static Coroutine ExecuteOverTime(this MonoBehaviour behaviour, float time, Action<float> onUpdate)
		{
			return behaviour.StartCoroutine(MonoBehaviourUtils.ExecuteOverCoroutine(time, () => Time.deltaTime, onUpdate));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002370 File Offset: 0x00000570
		private static IEnumerator ExecuteAfterCoroutine(float time, Func<float> timeFunc, Action onComplete)
		{
			float timer = 0f;
			while (timer < time)
			{
				timer = Mathf.Clamp(timer + timeFunc(), 0f, time);
				yield return null;
			}
			onComplete();
			yield break;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000238D File Offset: 0x0000058D
		private static IEnumerator ExecuteOverCoroutine(float time, Func<float> timeFunc, Action<float> onUpdate)
		{
			float timer = 0f;
			while (timer < time)
			{
				timer = Mathf.Clamp(timer + timeFunc(), 0f, time);
				onUpdate(timer / time);
				yield return null;
			}
			yield break;
		}
	}
}
