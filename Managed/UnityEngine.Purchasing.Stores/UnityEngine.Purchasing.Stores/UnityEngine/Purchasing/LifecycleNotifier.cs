using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200006B RID: 107
	internal class LifecycleNotifier : MonoBehaviour
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00007EAF File Offset: 0x000060AF
		private void OnDestroy()
		{
			Action onDestroyCallback = this.OnDestroyCallback;
			if (onDestroyCallback == null)
			{
				return;
			}
			onDestroyCallback();
		}

		// Token: 0x04000117 RID: 279
		public Action OnDestroyCallback;
	}
}
