using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000EF RID: 239
	public interface IAsyncOnEndDragHandler
	{
		// Token: 0x060005A0 RID: 1440
		UniTask<PointerEventData> OnEndDragAsync();
	}
}
