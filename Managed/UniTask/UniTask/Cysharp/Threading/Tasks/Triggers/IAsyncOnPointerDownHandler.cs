using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F7 RID: 247
	public interface IAsyncOnPointerDownHandler
	{
		// Token: 0x060005BC RID: 1468
		UniTask<PointerEventData> OnPointerDownAsync();
	}
}
