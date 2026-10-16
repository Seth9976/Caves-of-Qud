using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000E9 RID: 233
	public interface IAsyncOnDeselectHandler
	{
		// Token: 0x0600058B RID: 1419
		UniTask<BaseEventData> OnDeselectAsync();
	}
}
