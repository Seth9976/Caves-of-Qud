using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000FF RID: 255
	public interface IAsyncOnScrollHandler
	{
		// Token: 0x060005D8 RID: 1496
		UniTask<PointerEventData> OnScrollAsync();
	}
}
