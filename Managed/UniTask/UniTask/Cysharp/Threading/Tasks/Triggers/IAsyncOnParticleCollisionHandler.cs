using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000B5 RID: 181
	public interface IAsyncOnParticleCollisionHandler
	{
		// Token: 0x060004D5 RID: 1237
		UniTask<GameObject> OnParticleCollisionAsync();
	}
}
