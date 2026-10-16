using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006CB RID: 1739
	internal class WebOperation
	{
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x000C467A File Offset: 0x000C287A
		public HttpWebRequest Request { get; }

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000C4682 File Offset: 0x000C2882
		// (set) Token: 0x060037D8 RID: 14296 RVA: 0x000C468A File Offset: 0x000C288A
		public WebConnection Connection { get; private set; }

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x000C4693 File Offset: 0x000C2893
		// (set) Token: 0x060037DA RID: 14298 RVA: 0x000C469B File Offset: 0x000C289B
		public ServicePoint ServicePoint { get; private set; }

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060037DB RID: 14299 RVA: 0x000C46A4 File Offset: 0x000C28A4
		public BufferOffsetSize WriteBuffer { get; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060037DC RID: 14300 RVA: 0x000C46AC File Offset: 0x000C28AC
		public bool IsNtlmChallenge { get; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060037DD RID: 14301 RVA: 0x00002F6A File Offset: 0x0000116A
		internal string ME
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000C46B4 File Offset: 0x000C28B4
		public WebOperation(HttpWebRequest request, BufferOffsetSize writeBuffer, bool isNtlmChallenge, CancellationToken cancellationToken)
		{
			this.Request = request;
			this.WriteBuffer = writeBuffer;
			this.IsNtlmChallenge = isNtlmChallenge;
			this.cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			this.requestTask = new WebCompletionSource<WebRequestStream>(true);
			this.requestWrittenTask = new WebCompletionSource<WebRequestStream>(true);
			this.responseTask = new WebCompletionSource<WebResponseStream>(true);
			this.finishedTask = new WebCompletionSource<ValueTuple<bool, WebOperation>>(true);
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000C4719 File Offset: 0x000C2919
		public bool Aborted
		{
			get
			{
				return this.disposedInfo != null || this.Request.Aborted || (this.cts != null && this.cts.IsCancellationRequested);
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x000C474A File Offset: 0x000C294A
		public bool Closed
		{
			get
			{
				return this.Aborted || this.closedInfo != null;
			}
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000C475F File Offset: 0x000C295F
		public void Abort()
		{
			if (!this.SetDisposed(ref this.disposedInfo).Item2)
			{
				return;
			}
			CancellationTokenSource cancellationTokenSource = this.cts;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this.SetCanceled();
			this.Close();
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000C4794 File Offset: 0x000C2994
		public void Close()
		{
			if (!this.SetDisposed(ref this.closedInfo).Item2)
			{
				return;
			}
			WebRequestStream webRequestStream = Interlocked.Exchange<WebRequestStream>(ref this.writeStream, null);
			if (webRequestStream != null)
			{
				try
				{
					webRequestStream.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000C47E0 File Offset: 0x000C29E0
		private void SetCanceled()
		{
			OperationCanceledException ex = new OperationCanceledException();
			this.requestTask.TrySetCanceled(ex);
			this.requestWrittenTask.TrySetCanceled(ex);
			this.responseTask.TrySetCanceled(ex);
			this.Finish(false, ex);
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000C4822 File Offset: 0x000C2A22
		private void SetError(Exception error)
		{
			this.requestTask.TrySetException(error);
			this.requestWrittenTask.TrySetException(error);
			this.responseTask.TrySetException(error);
			this.Finish(false, error);
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000C4854 File Offset: 0x000C2A54
		private ValueTuple<ExceptionDispatchInfo, bool> SetDisposed(ref ExceptionDispatchInfo field)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new WebException(SR.GetString("The request was canceled"), WebExceptionStatus.RequestCanceled));
			ExceptionDispatchInfo exceptionDispatchInfo2 = Interlocked.CompareExchange<ExceptionDispatchInfo>(ref field, exceptionDispatchInfo, null);
			return new ValueTuple<ExceptionDispatchInfo, bool>(exceptionDispatchInfo2 ?? exceptionDispatchInfo, exceptionDispatchInfo2 == null);
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000C488F File Offset: 0x000C2A8F
		internal ExceptionDispatchInfo CheckDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				return this.CheckThrowDisposed(false, ref this.disposedInfo);
			}
			return null;
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000C48B1 File Offset: 0x000C2AB1
		internal void ThrowIfDisposed()
		{
			this.ThrowIfDisposed(CancellationToken.None);
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000C48BE File Offset: 0x000C2ABE
		internal void ThrowIfDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				this.CheckThrowDisposed(true, ref this.disposedInfo);
			}
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000C48DF File Offset: 0x000C2ADF
		internal void ThrowIfClosedOrDisposed()
		{
			this.ThrowIfClosedOrDisposed(CancellationToken.None);
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000C48EC File Offset: 0x000C2AEC
		internal void ThrowIfClosedOrDisposed(CancellationToken cancellationToken)
		{
			if (this.Closed || cancellationToken.IsCancellationRequested)
			{
				this.CheckThrowDisposed(true, ref this.closedInfo);
			}
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000C4910 File Offset: 0x000C2B10
		private ExceptionDispatchInfo CheckThrowDisposed(bool throwIt, ref ExceptionDispatchInfo field)
		{
			ValueTuple<ExceptionDispatchInfo, bool> valueTuple = this.SetDisposed(ref field);
			ExceptionDispatchInfo item = valueTuple.Item1;
			if (valueTuple.Item2)
			{
				CancellationTokenSource cancellationTokenSource = this.cts;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
			}
			if (throwIt)
			{
				item.Throw();
			}
			return item;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000C4950 File Offset: 0x000C2B50
		internal void RegisterRequest(ServicePoint servicePoint, WebConnection connection)
		{
			if (servicePoint == null)
			{
				throw new ArgumentNullException("servicePoint");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			lock (this)
			{
				if (Interlocked.CompareExchange(ref this.requestSent, 1, 0) != 0)
				{
					throw new InvalidOperationException("Invalid nested call.");
				}
				this.ServicePoint = servicePoint;
				this.Connection = connection;
			}
			this.cts.Token.Register(delegate
			{
				this.Request.FinishedReading = true;
				this.SetDisposed(ref this.disposedInfo);
			});
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000C49EC File Offset: 0x000C2BEC
		public void SetPriorityRequest(WebOperation operation)
		{
			lock (this)
			{
				if (this.requestSent != 1 || this.ServicePoint == null || this.finished != 0)
				{
					throw new InvalidOperationException("Should never happen.");
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.priorityRequest, operation, null) != null)
				{
					throw new InvalidOperationException("Invalid nested request.");
				}
			}
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000C4A60 File Offset: 0x000C2C60
		public async Task<Stream> GetRequestStream()
		{
			return await this.requestTask.WaitForCompletion().ConfigureAwait(false);
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000C4AA3 File Offset: 0x000C2CA3
		internal Task<WebRequestStream> GetRequestStreamInternal()
		{
			return this.requestTask.WaitForCompletion();
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000C4AB0 File Offset: 0x000C2CB0
		public Task WaitUntilRequestWritten()
		{
			return this.requestWrittenTask.WaitForCompletion();
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x000C4ABD File Offset: 0x000C2CBD
		public WebRequestStream WriteStream
		{
			get
			{
				this.ThrowIfDisposed();
				return this.writeStream;
			}
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000C4ACB File Offset: 0x000C2CCB
		public Task<WebResponseStream> GetResponseStream()
		{
			return this.responseTask.WaitForCompletion();
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x000C4AD8 File Offset: 0x000C2CD8
		internal WebCompletionSource<ValueTuple<bool, WebOperation>> Finished
		{
			get
			{
				return this.finishedTask;
			}
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000C4AE0 File Offset: 0x000C2CE0
		internal async void Run()
		{
			try
			{
				this.ThrowIfClosedOrDisposed();
				WebRequestStream webRequestStream = await this.Connection.InitConnection(this, this.cts.Token).ConfigureAwait(false);
				WebRequestStream requestStream = webRequestStream;
				this.ThrowIfClosedOrDisposed();
				this.writeStream = requestStream;
				await requestStream.Initialize(this.cts.Token).ConfigureAwait(false);
				this.ThrowIfClosedOrDisposed();
				this.requestTask.TrySetCompleted(requestStream);
				WebResponseStream stream = new WebResponseStream(requestStream);
				this.responseStream = stream;
				await stream.InitReadAsync(this.cts.Token).ConfigureAwait(false);
				this.responseTask.TrySetCompleted(stream);
				requestStream = null;
				stream = null;
			}
			catch (OperationCanceledException)
			{
				this.SetCanceled();
			}
			catch (Exception ex)
			{
				this.SetError(ex);
			}
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000C4B17 File Offset: 0x000C2D17
		internal void CompleteRequestWritten(WebRequestStream stream, Exception error = null)
		{
			if (error != null)
			{
				this.SetError(error);
				return;
			}
			this.requestWrittenTask.TrySetCompleted(stream);
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000C4B34 File Offset: 0x000C2D34
		internal void Finish(bool ok, Exception error = null)
		{
			if (Interlocked.CompareExchange(ref this.finished, 1, 0) != 0)
			{
				return;
			}
			WebResponseStream webResponseStream;
			WebOperation webOperation;
			lock (this)
			{
				webResponseStream = Interlocked.Exchange<WebResponseStream>(ref this.responseStream, null);
				webOperation = Interlocked.Exchange<WebOperation>(ref this.priorityRequest, null);
				this.Request.FinishedReading = true;
			}
			if (error != null)
			{
				if (webOperation != null)
				{
					webOperation.SetError(error);
				}
				this.finishedTask.TrySetException(error);
				return;
			}
			bool flag2 = !this.Aborted && ok && webResponseStream != null && webResponseStream.KeepAlive;
			if (webOperation != null && webOperation.Aborted)
			{
				webOperation = null;
				flag2 = false;
			}
			this.finishedTask.TrySetCompleted(new ValueTuple<bool, WebOperation>(flag2, webOperation));
		}

		// Token: 0x040020CC RID: 8396
		internal readonly int ID;

		// Token: 0x040020CD RID: 8397
		private CancellationTokenSource cts;

		// Token: 0x040020CE RID: 8398
		private WebCompletionSource<WebRequestStream> requestTask;

		// Token: 0x040020CF RID: 8399
		private WebCompletionSource<WebRequestStream> requestWrittenTask;

		// Token: 0x040020D0 RID: 8400
		private WebCompletionSource<WebResponseStream> responseTask;

		// Token: 0x040020D1 RID: 8401
		private WebCompletionSource<ValueTuple<bool, WebOperation>> finishedTask;

		// Token: 0x040020D2 RID: 8402
		private WebRequestStream writeStream;

		// Token: 0x040020D3 RID: 8403
		private WebResponseStream responseStream;

		// Token: 0x040020D4 RID: 8404
		private ExceptionDispatchInfo disposedInfo;

		// Token: 0x040020D5 RID: 8405
		private ExceptionDispatchInfo closedInfo;

		// Token: 0x040020D6 RID: 8406
		private WebOperation priorityRequest;

		// Token: 0x040020D7 RID: 8407
		private int requestSent;

		// Token: 0x040020D8 RID: 8408
		private int finished;
	}
}
