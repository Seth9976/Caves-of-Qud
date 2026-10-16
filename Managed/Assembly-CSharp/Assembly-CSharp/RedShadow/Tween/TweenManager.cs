using System;
using System.Collections;
using UnityEngine;

namespace RedShadow.Tween
{
	// Token: 0x020001E5 RID: 485
	public class TweenManager : MonoBehaviour
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x00069873 File Offset: 0x00067A73
		protected void Awake()
		{
			TweenManager.Instance = this;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0006987B File Offset: 0x00067A7B
		public static void delayedCall(float time, Action action)
		{
			TweenManager.Instance.StartCoroutine(TweenManager.Instance.delayedCall_co(time, action));
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00069894 File Offset: 0x00067A94
		private IEnumerator delayedCall_co(float time, Action action)
		{
			yield return new WaitForSeconds(time);
			action();
			yield break;
		}

		// Token: 0x04000B98 RID: 2968
		public static TweenManager Instance;
	}
}
