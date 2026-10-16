using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002E RID: 46
	internal static class AsyncOperationExtensions
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x0000280C File Offset: 0x00000A0C
		public static AsyncOperationAwaiter GetAwaiter(this IAsyncOperation self)
		{
			return new AsyncOperationAwaiter(self);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002814 File Offset: 0x00000A14
		public static Task AsTask(this IAsyncOperation self)
		{
			AsyncOperationExtensions.<>c__DisplayClass1_0 CS$<>8__locals1 = new AsyncOperationExtensions.<>c__DisplayClass1_0();
			if (self.Status == AsyncOperationStatus.Succeeded)
			{
				return Task.CompletedTask;
			}
			CS$<>8__locals1.taskCompletionSource = new TaskCompletionSource<object>();
			if (self.IsDone)
			{
				CS$<>8__locals1.<AsTask>g__CompleteTask|0(self);
			}
			else
			{
				self.Completed += CS$<>8__locals1.<AsTask>g__CompleteTask|0;
			}
			return CS$<>8__locals1.taskCompletionSource.Task;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000286F File Offset: 0x00000A6F
		public static AsyncOperationAwaiter<T> GetAwaiter<T>(this IAsyncOperation<T> self)
		{
			return new AsyncOperationAwaiter<T>(self);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002878 File Offset: 0x00000A78
		public static Task<T> AsTask<T>(this IAsyncOperation<T> self)
		{
			AsyncOperationExtensions.<>c__DisplayClass3_0<T> CS$<>8__locals1 = new AsyncOperationExtensions.<>c__DisplayClass3_0<T>();
			CS$<>8__locals1.taskCompletionSource = new TaskCompletionSource<T>();
			if (self.IsDone)
			{
				CS$<>8__locals1.<AsTask>g__CompleteTask|0(self);
			}
			else
			{
				self.Completed += CS$<>8__locals1.<AsTask>g__CompleteTask|0;
			}
			return CS$<>8__locals1.taskCompletionSource.Task;
		}
	}
}
