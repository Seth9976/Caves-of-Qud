using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000D5 RID: 213
	public interface IAsyncOnTriggerExitHandler
	{
		// Token: 0x06000545 RID: 1349
		UniTask<Collider> OnTriggerExitAsync();
	}
}
