using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000D7 RID: 215
	public interface IAsyncOnTriggerExit2DHandler
	{
		// Token: 0x0600054C RID: 1356
		UniTask<Collider2D> OnTriggerExit2DAsync();
	}
}
