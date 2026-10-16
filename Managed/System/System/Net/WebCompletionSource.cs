using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006BB RID: 1723
	internal class WebCompletionSource<T>
	{
		// Token: 0x06003763 RID: 14179 RVA: 0x000C260C File Offset: 0x000C080C
		public WebCompletionSource(bool runAsync = true)
		{
			this.completion = new TaskCompletionSource<WebCompletionSource<T>.Result>(runAsync ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000C2627 File Offset: 0x000C0827
		internal WebCompletionSource<T>.Result CurrentResult
		{
			get
			{
				return this.currentResult;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x000C262F File Offset: 0x000C082F
		internal WebCompletionSource<T>.Status CurrentStatus
		{
			get
			{
				WebCompletionSource<T>.Result result = this.currentResult;
				if (result == null)
				{
					return WebCompletionSource<T>.Status.Running;
				}
				return result.Status;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000C2642 File Offset: 0x000C0842
		internal Task Task
		{
			get
			{
				return this.completion.Task;
			}
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000C2650 File Offset: 0x000C0850
		public bool TrySetCompleted(T argument)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(argument);
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000C2684 File Offset: 0x000C0884
		public bool TrySetCompleted()
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Completed, null);
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000C26B6 File Offset: 0x000C08B6
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(new OperationCanceledException());
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000C26C4 File Offset: 0x000C08C4
		public bool TrySetCanceled(OperationCanceledException error)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Canceled, ExceptionDispatchInfo.Capture(error));
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000C26FC File Offset: 0x000C08FC
		public bool TrySetException(Exception error)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Faulted, ExceptionDispatchInfo.Capture(error));
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000C2733 File Offset: 0x000C0933
		public void ThrowOnError()
		{
			if (!this.completion.Task.IsCompleted)
			{
				return;
			}
			ExceptionDispatchInfo error = this.completion.Task.Result.Error;
			if (error == null)
			{
				return;
			}
			error.Throw();
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000C2768 File Offset: 0x000C0968
		public async Task<T> WaitForCompletion()
		{
			WebCompletionSource<T>.Result result = await this.completion.Task.ConfigureAwait(false);
			if (result.Status == WebCompletionSource<T>.Status.Completed)
			{
				return result.Argument;
			}
			result.Error.Throw();
			throw new InvalidOperationException("Should never happen.");
		}

		// Token: 0x0400205F RID: 8287
		private TaskCompletionSource<WebCompletionSource<T>.Result> completion;

		// Token: 0x04002060 RID: 8288
		private WebCompletionSource<T>.Result currentResult;

		// Token: 0x020006BC RID: 1724
		internal enum Status
		{
			// Token: 0x04002062 RID: 8290
			Running,
			// Token: 0x04002063 RID: 8291
			Completed,
			// Token: 0x04002064 RID: 8292
			Canceled,
			// Token: 0x04002065 RID: 8293
			Faulted
		}

		// Token: 0x020006BD RID: 1725
		internal class Result
		{
			// Token: 0x17000B8E RID: 2958
			// (get) Token: 0x0600376E RID: 14190 RVA: 0x000C27AB File Offset: 0x000C09AB
			public WebCompletionSource<T>.Status Status { get; }

			// Token: 0x17000B8F RID: 2959
			// (get) Token: 0x0600376F RID: 14191 RVA: 0x000C27B3 File Offset: 0x000C09B3
			public bool Success
			{
				get
				{
					return this.Status == WebCompletionSource<T>.Status.Completed;
				}
			}

			// Token: 0x17000B90 RID: 2960
			// (get) Token: 0x06003770 RID: 14192 RVA: 0x000C27BE File Offset: 0x000C09BE
			public ExceptionDispatchInfo Error { get; }

			// Token: 0x17000B91 RID: 2961
			// (get) Token: 0x06003771 RID: 14193 RVA: 0x000C27C6 File Offset: 0x000C09C6
			public T Argument { get; }

			// Token: 0x06003772 RID: 14194 RVA: 0x000C27CE File Offset: 0x000C09CE
			public Result(T argument)
			{
				this.Status = WebCompletionSource<T>.Status.Completed;
				this.Argument = argument;
			}

			// Token: 0x06003773 RID: 14195 RVA: 0x000C27E4 File Offset: 0x000C09E4
			public Result(WebCompletionSource<T>.Status state, ExceptionDispatchInfo error)
			{
				this.Status = state;
				this.Error = error;
			}
		}
	}
}
