using System;
using System.Collections;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000020 RID: 32
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00003071 File Offset: 0x00001271
		private static IEnumerator Start(T tweenInfo)
		{
			if (!tweenInfo.ValidTarget())
			{
				yield break;
			}
			float elapsedTime = 0f;
			while (elapsedTime < tweenInfo.duration)
			{
				elapsedTime += (tweenInfo.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
				float num = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(num);
				yield return null;
			}
			tweenInfo.TweenValue(1f);
			yield break;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003080 File Offset: 0x00001280
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000308C File Offset: 0x0000128C
		public void StartTween(T info)
		{
			if (this.m_CoroutineContainer == null)
			{
				Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
				return;
			}
			this.StopTween();
			if (!this.m_CoroutineContainer.gameObject.activeInHierarchy)
			{
				info.TweenValue(1f);
				return;
			}
			this.m_Tween = TweenRunner<T>.Start(info);
			this.m_CoroutineContainer.StartCoroutine(this.m_Tween);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000030FB File Offset: 0x000012FB
		public void StopTween()
		{
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
		}

		// Token: 0x04000080 RID: 128
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x04000081 RID: 129
		protected IEnumerator m_Tween;
	}
}
