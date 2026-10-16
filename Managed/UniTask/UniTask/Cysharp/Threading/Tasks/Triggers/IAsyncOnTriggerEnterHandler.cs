using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000D1 RID: 209
	public interface IAsyncOnTriggerEnterHandler
	{
		// Token: 0x06000537 RID: 1335
		UniTask<Collider> OnTriggerEnterAsync();
	}
}
