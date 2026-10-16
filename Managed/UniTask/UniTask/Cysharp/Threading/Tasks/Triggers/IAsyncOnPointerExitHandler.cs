using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000FB RID: 251
	public interface IAsyncOnPointerExitHandler
	{
		// Token: 0x060005CA RID: 1482
		UniTask<PointerEventData> OnPointerExitAsync();
	}
}
