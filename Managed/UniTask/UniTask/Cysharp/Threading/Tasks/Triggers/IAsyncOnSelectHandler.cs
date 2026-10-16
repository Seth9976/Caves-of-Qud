using System;
using UnityEngine.EventSystems;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000101 RID: 257
	public interface IAsyncOnSelectHandler
	{
		// Token: 0x060005DF RID: 1503
		UniTask<BaseEventData> OnSelectAsync();
	}
}
