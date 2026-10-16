using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000097 RID: 151
	public interface IAsyncOnControllerColliderHitHandler
	{
		// Token: 0x0600046C RID: 1132
		UniTask<ControllerColliderHit> OnControllerColliderHitAsync();
	}
}
