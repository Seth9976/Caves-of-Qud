using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200008B RID: 139
	public interface IAsyncOnCollisionEnterHandler
	{
		// Token: 0x06000442 RID: 1090
		UniTask<Collision> OnCollisionEnterAsync();
	}
}
