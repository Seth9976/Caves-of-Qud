using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000D9 RID: 217
	public interface IAsyncOnTriggerStayHandler
	{
		// Token: 0x06000553 RID: 1363
		UniTask<Collider> OnTriggerStayAsync();
	}
}
