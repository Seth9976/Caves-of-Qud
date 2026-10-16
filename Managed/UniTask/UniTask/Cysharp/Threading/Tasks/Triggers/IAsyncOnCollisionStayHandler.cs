using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000093 RID: 147
	public interface IAsyncOnCollisionStayHandler
	{
		// Token: 0x0600045E RID: 1118
		UniTask<Collision> OnCollisionStayAsync();
	}
}
