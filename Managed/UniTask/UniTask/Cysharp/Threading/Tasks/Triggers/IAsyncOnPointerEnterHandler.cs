using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F9 RID: 249
	public interface IAsyncOnPointerEnterHandler
	{
		// Token: 0x060005C3 RID: 1475
		UniTask<PointerEventData> OnPointerEnterAsync();
	}
}
