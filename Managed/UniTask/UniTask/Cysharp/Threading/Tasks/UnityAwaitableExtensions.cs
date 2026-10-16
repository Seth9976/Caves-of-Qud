using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000069 RID: 105
	public static class UnityAwaitableExtensions
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000A438 File Offset: 0x00008638
		public static async UniTask AsUniTask(this Awaitable awaitable)
		{
			await awaitable;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A47C File Offset: 0x0000867C
		public static async UniTask<T> AsUniTask<T>(this Awaitable<T> awaitable)
		{
			return await awaitable;
		}
	}
}
