using System;
using System.Threading;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000037 RID: 55
	public static class UniTaskCancellationExtensions
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00004484 File Offset: 0x00002684
		public static CancellationToken GetCancellationTokenOnDestroy(this MonoBehaviour monoBehaviour)
		{
			return monoBehaviour.destroyCancellationToken;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000448C File Offset: 0x0000268C
		public static CancellationToken GetCancellationTokenOnDestroy(this GameObject gameObject)
		{
			return gameObject.GetAsyncDestroyTrigger().CancellationToken;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000449C File Offset: 0x0000269C
		public static CancellationToken GetCancellationTokenOnDestroy(this Component component)
		{
			MonoBehaviour monoBehaviour = component as MonoBehaviour;
			if (monoBehaviour != null)
			{
				return monoBehaviour.destroyCancellationToken;
			}
			return component.GetAsyncDestroyTrigger().CancellationToken;
		}
	}
}
