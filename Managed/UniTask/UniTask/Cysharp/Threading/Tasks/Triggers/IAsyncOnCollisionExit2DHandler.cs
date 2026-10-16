using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000091 RID: 145
	public interface IAsyncOnCollisionExit2DHandler
	{
		// Token: 0x06000457 RID: 1111
		UniTask<Collision2D> OnCollisionExit2DAsync();
	}
}
