using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006D0 RID: 1744
	internal class WebRequestStream : WebConnectionStream
	{
		// Token: 0x06003814 RID: 14356 RVA: 0x000C545C File Offset: 0x000C365C
		public WebRequestStream(WebConnection connection, WebOperation operation, Stream stream, WebConnectionTunnel tunnel)
			: base(connection, operation)
		{
			this.InnerStream = stream;
			this.allowBuffering = operation.Request.InternalAllowBuffering;
			this.sendChunked = operation.Request.SendChunked && operation.WriteBuffer == null;
			if (!this.sendChunked && this.allowBuffering && operation.WriteBuffer == null)
			{
				this.writeBuffer = new MemoryStream();
			}
			this.KeepAlive = base.Request.KeepAlive;
			if (((tunnel != null) ? tunnel.ProxyVersion : null) != null && ((tunnel != null) ? tunnel.ProxyVersion : null) != HttpVersion.Version11)
			{
				this.KeepAlive = false;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x000C5513 File Offset: 0x000C3713
		internal Stream InnerStream { get; }

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000C551B File Offset: 0x000C371B
		public bool KeepAlive { get; }

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003817 RID: 14359 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x000C5523 File Offset: 0x000C3723
		// (set) Token: 0x0600381A RID: 14362 RVA: 0x000C552B File Offset: 0x000C372B
		internal bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
			set
			{
				this.sendChunked = value;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x000C5534 File Offset: 0x000C3734
		internal bool HasWriteBuffer
		{
			get
			{
				return base.Operation.WriteBuffer != null || this.writeBuffer != null;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x000C554E File Offset: 0x000C374E
		internal int WriteBufferLength
		{
			get
			{
				if (base.Operation.WriteBuffer != null)
				{
					return base.Operation.WriteBuffer.Size;
				}
				if (this.writeBuffer != null)
				{
					return (int)this.writeBuffer.Length;
				}
				return -1;
			}
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000C5584 File Offset: 0x000C3784
		internal BufferOffsetSize GetWriteBuffer()
		{
			if (base.Operation.WriteBuffer != null)
			{
				return base.Operation.WriteBuffer;
			}
			if (this.writeBuffer == null || this.writeBuffer.Length == 0L)
			{
				return null;
			}
			return new BufferOffsetSize(this.writeBuffer.GetBuffer(), 0, (int)this.writeBuffer.Length, false);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000C55E0 File Offset: 0x000C37E0
		private async Task FinishWriting(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.completeRequestWritten, 1, 0) == 0)
			{
				try
				{
					base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
					if (this.sendChunked)
					{
						await this.WriteChunkTrailer_inner(cancellationToken).ConfigureAwait(false);
					}
				}
				catch (Exception ex)
				{
					base.Operation.CompleteRequestWritten(this, ex);
					throw;
				}
				finally
				{
				}
				base.Operation.CompleteRequestWritten(this, null);
			}
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000C562C File Offset: 0x000C382C
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				throw new InvalidOperationException();
			}
			WebCompletionSource webCompletionSource = new WebCompletionSource();
			if (Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, webCompletionSource, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."));
			}
			return this.WriteAsyncInner(buffer, offset, count, webCompletionSource, cancellationToken);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000C56D8 File Offset: 0x000C38D8
		private async Task WriteAsyncInner(byte[] buffer, int offset, int size, WebCompletionSource completion, CancellationToken cancellationToken)
		{
			try
			{
				await this.ProcessWrite(buffer, offset, size, cancellationToken).ConfigureAwait(false);
				if (base.Request.ContentLength > 0L && this.totalWritten == base.Request.ContentLength)
				{
					await this.FinishWriting(cancellationToken);
				}
				this.pendingWrite = null;
				completion.TrySetCompleted();
			}
			catch (Exception ex)
			{
				this.KillBuffer();
				this.closed = true;
				ExceptionDispatchInfo exceptionDispatchInfo = base.Operation.CheckDisposed(cancellationToken);
				if (exceptionDispatchInfo != null)
				{
					ex = exceptionDispatchInfo.SourceException;
				}
				else if (ex is SocketException)
				{
					ex = new IOException("Error writing request", ex);
				}
				base.Operation.CompleteRequestWritten(this, ex);
				this.pendingWrite = null;
				completion.TrySetException(ex);
				if (exceptionDispatchInfo != null)
				{
					exceptionDispatchInfo.Throw();
				}
				throw;
			}
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000C5748 File Offset: 0x000C3948
		private async Task ProcessWrite(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (this.sendChunked)
			{
				this.requestWritten = true;
				string text = string.Format("{0:X}\r\n", size);
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				int num = 2 + size + bytes.Length;
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
				Buffer.BlockCopy(buffer, offset, array, bytes.Length, size);
				Buffer.BlockCopy(WebRequestStream.crlf, 0, array, bytes.Length + size, WebRequestStream.crlf.Length);
				if (this.allowBuffering)
				{
					if (this.writeBuffer == null)
					{
						this.writeBuffer = new MemoryStream();
					}
					this.writeBuffer.Write(buffer, offset, size);
				}
				this.totalWritten += (long)size;
				buffer = array;
				offset = 0;
				size = num;
			}
			else
			{
				this.CheckWriteOverflow(base.Request.ContentLength, this.totalWritten, (long)size);
				if (this.allowBuffering)
				{
					if (this.writeBuffer == null)
					{
						this.writeBuffer = new MemoryStream();
					}
					this.writeBuffer.Write(buffer, offset, size);
					this.totalWritten += (long)size;
					if (base.Request.ContentLength <= 0L || this.totalWritten < base.Request.ContentLength)
					{
						return;
					}
					this.requestWritten = true;
					buffer = this.writeBuffer.GetBuffer();
					offset = 0;
					size = (int)this.totalWritten;
				}
				else
				{
					this.totalWritten += (long)size;
				}
			}
			await this.InnerStream.WriteAsync(buffer, offset, size, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000C57AC File Offset: 0x000C39AC
		private void CheckWriteOverflow(long contentLength, long totalWritten, long size)
		{
			if (contentLength == -1L)
			{
				return;
			}
			long num = contentLength - totalWritten;
			if (size > num)
			{
				this.KillBuffer();
				this.closed = true;
				ProtocolViolationException ex = new ProtocolViolationException("The number of bytes to be written is greater than the specified ContentLength.");
				base.Operation.CompleteRequestWritten(this, ex);
				throw ex;
			}
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000C57F0 File Offset: 0x000C39F0
		internal async Task Initialize(CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				if (base.Operation.IsNtlmChallenge)
				{
					base.Request.InternalContentLength = 0L;
				}
				else
				{
					base.Request.InternalContentLength = (long)base.Operation.WriteBuffer.Size;
				}
			}
			await this.SetHeadersAsync(false, cancellationToken).ConfigureAwait(false);
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null && !base.Operation.IsNtlmChallenge)
			{
				await this.WriteRequestAsync(cancellationToken);
				this.Close();
			}
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000C583C File Offset: 0x000C3A3C
		private async Task SetHeadersAsync(bool setInternalLength, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (!this.headersSent)
			{
				string method = base.Request.Method;
				bool flag = method == "GET" || method == "CONNECT" || method == "HEAD" || method == "TRACE";
				bool flag2 = method == "PROPFIND" || method == "PROPPATCH" || method == "MKCOL" || method == "COPY" || method == "MOVE" || method == "LOCK" || method == "UNLOCK";
				if (base.Operation.IsNtlmChallenge)
				{
					flag = true;
				}
				if (setInternalLength && !flag && this.HasWriteBuffer)
				{
					base.Request.InternalContentLength = (long)this.WriteBufferLength;
				}
				bool flag3 = !flag && (!this.HasWriteBuffer || base.Request.ContentLength > -1L);
				if (this.sendChunked || flag3 || flag || flag2)
				{
					this.headersSent = true;
					this.headers = base.Request.GetRequestHeaders();
					try
					{
						await this.InnerStream.WriteAsync(this.headers, 0, this.headers.Length, cancellationToken).ConfigureAwait(false);
						long contentLength = base.Request.ContentLength;
						if (!this.sendChunked && contentLength == 0L)
						{
							this.requestWritten = true;
						}
					}
					catch (Exception ex)
					{
						if (ex is WebException || ex is OperationCanceledException)
						{
							throw;
						}
						throw new WebException("Error writing headers", WebExceptionStatus.SendFailure, WebExceptionInternalStatus.RequestFatal, ex);
					}
				}
			}
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000C5890 File Offset: 0x000C3A90
		internal async Task WriteRequestAsync(CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (!this.requestWritten)
			{
				this.requestWritten = true;
				if (!this.sendChunked && this.HasWriteBuffer)
				{
					BufferOffsetSize buffer = this.GetWriteBuffer();
					if (buffer != null && !base.Operation.IsNtlmChallenge && base.Request.ContentLength != -1L && base.Request.ContentLength < (long)buffer.Size)
					{
						this.closed = true;
						WebException ex = new WebException("Specified Content-Length is less than the number of bytes to write", null, WebExceptionStatus.ServerProtocolViolation, null);
						base.Operation.CompleteRequestWritten(this, ex);
						throw ex;
					}
					await this.SetHeadersAsync(true, cancellationToken).ConfigureAwait(false);
					base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
					if (buffer != null && buffer.Size > 0)
					{
						await this.InnerStream.WriteAsync(buffer.Buffer, 0, buffer.Size, cancellationToken);
					}
					await this.FinishWriting(cancellationToken);
				}
			}
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000C58DC File Offset: 0x000C3ADC
		private async Task WriteChunkTrailer_inner(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.chunkTrailerWritten, 1, 0) == 0)
			{
				base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
				byte[] bytes = Encoding.ASCII.GetBytes("0\r\n\r\n");
				await this.InnerStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000C5928 File Offset: 0x000C3B28
		private async Task WriteChunkTrailer()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			try
			{
				cts.CancelAfter(this.WriteTimeout);
				Task timeoutTask = Task.Delay(this.WriteTimeout, cts.Token);
				ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					WebCompletionSource webCompletionSource = new WebCompletionSource();
					WebCompletionSource webCompletionSource2 = Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, webCompletionSource, null);
					if (webCompletionSource2 == null)
					{
						goto IL_010D;
					}
					Task<object> task = webCompletionSource2.WaitForCompletion();
					configuredTaskAwaiter = Task.WhenAny(new Task[] { timeoutTask, task }).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() != timeoutTask);
				throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
				IL_010D:
				await this.WriteChunkTrailer_inner(cts.Token).ConfigureAwait(false);
				timeoutTask = null;
			}
			catch
			{
			}
			finally
			{
				this.pendingWrite = null;
				cts.Cancel();
				cts.Dispose();
			}
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000C596B File Offset: 0x000C3B6B
		internal void KillBuffer()
		{
			this.writeBuffer = null;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000C5974 File Offset: 0x000C3B74
		public override Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return Task.FromException<int>(new NotSupportedException("The stream does not support reading."));
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x00011EB3 File Offset: 0x000100B3
		protected override bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000C5988 File Offset: 0x000C3B88
		protected override void Close_internal(ref bool disposed)
		{
			if (disposed)
			{
				return;
			}
			disposed = true;
			if (this.sendChunked)
			{
				this.WriteChunkTrailer().Wait();
				return;
			}
			if (!this.allowBuffering || this.requestWritten)
			{
				base.Operation.CompleteRequestWritten(this, null);
				return;
			}
			long contentLength = base.Request.ContentLength;
			if (!this.sendChunked && !base.Operation.IsNtlmChallenge && contentLength != -1L && this.totalWritten != contentLength)
			{
				IOException ex = new IOException("Cannot close the stream until all bytes are written");
				this.closed = true;
				disposed = true;
				WebException ex2 = new WebException("Request was cancelled.", WebExceptionStatus.RequestCanceled, WebExceptionInternalStatus.RequestFatal, ex);
				base.Operation.CompleteRequestWritten(this, ex2);
				throw ex2;
			}
			disposed = true;
			base.Operation.CompleteRequestWritten(this, null);
		}

		// Token: 0x040020F0 RID: 8432
		private static byte[] crlf = new byte[] { 13, 10 };

		// Token: 0x040020F1 RID: 8433
		private MemoryStream writeBuffer;

		// Token: 0x040020F2 RID: 8434
		private bool requestWritten;

		// Token: 0x040020F3 RID: 8435
		private bool allowBuffering;

		// Token: 0x040020F4 RID: 8436
		private bool sendChunked;

		// Token: 0x040020F5 RID: 8437
		private WebCompletionSource pendingWrite;

		// Token: 0x040020F6 RID: 8438
		private long totalWritten;

		// Token: 0x040020F7 RID: 8439
		private byte[] headers;

		// Token: 0x040020F8 RID: 8440
		private bool headersSent;

		// Token: 0x040020F9 RID: 8441
		private int completeRequestWritten;

		// Token: 0x040020FA RID: 8442
		private int chunkTrailerWritten;

		// Token: 0x040020FB RID: 8443
		internal readonly string ME;
	}
}
