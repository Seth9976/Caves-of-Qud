using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000E7 RID: 231
	public interface IAsyncOnCancelHandler
	{
		// Token: 0x06000584 RID: 1412
		UniTask<BaseEventData> OnCancelAsync();
	}
}
