using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000095 RID: 149
	public interface IAsyncOnCollisionStay2DHandler
	{
		// Token: 0x06000465 RID: 1125
		UniTask<Collision2D> OnCollisionStay2DAsync();
	}
}
