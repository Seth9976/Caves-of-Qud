using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000F3 RID: 243
	public interface IAsyncOnMoveHandler
	{
		// Token: 0x060005AE RID: 1454
		UniTask<AxisEventData> OnMoveAsync();
	}
}
