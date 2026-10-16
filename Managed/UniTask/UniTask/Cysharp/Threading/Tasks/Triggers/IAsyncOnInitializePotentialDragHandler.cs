using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F1 RID: 241
	public interface IAsyncOnInitializePotentialDragHandler
	{
		// Token: 0x060005A7 RID: 1447
		UniTask<PointerEventData> OnInitializePotentialDragAsync();
	}
}
