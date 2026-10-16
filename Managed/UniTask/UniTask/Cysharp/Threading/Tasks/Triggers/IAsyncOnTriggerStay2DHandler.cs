using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000DB RID: 219
	public interface IAsyncOnTriggerStay2DHandler
	{
		// Token: 0x0600055A RID: 1370
		UniTask<Collider2D> OnTriggerStay2DAsync();
	}
}
