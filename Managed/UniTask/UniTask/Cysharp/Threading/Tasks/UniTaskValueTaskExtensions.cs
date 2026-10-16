using System;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000038 RID: 56
	public static class UniTaskValueTaskExtensions
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x000044C5 File Offset: 0x000026C5
		public static ValueTask AsValueTask(this UniTask task)
		{
			return in task;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000044CD File Offset: 0x000026CD
		public static ValueTask<T> AsValueTask<T>(this UniTask<T> task)
		{
			return in task;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000044D8 File Offset: 0x000026D8
		public static async UniTask<T> AsUniTask<T>(this ValueTask<T> task)
		{
			return await task;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000451C File Offset: 0x0000271C
		public static async UniTask AsUniTask(this ValueTask task)
		{
			await task;
		}
	}
}
