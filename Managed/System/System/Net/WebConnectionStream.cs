using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006C6 RID: 1734
	internal abstract class WebConnectionStream : Stream
	{
		// Token: 0x0600379F RID: 14239 RVA: 0x000C38F0 File Offset: 0x000C1AF0
		protected WebConnectionStream(WebConnection cnc, WebOperation operation)
		{
			this.Connection = cnc;
			this.Operation = operation;
			this.Request = operation.Request;
			this.read_timeout = this.Request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000C3945 File Offset: 0x000C1B45
		internal HttpWebRequest Request { get; }

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000C394D File Offset: 0x000C1B4D
		internal WebConnection Connection { get; }

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000C3955 File Offset: 0x000C1B55
		internal WebOperation Operation { get; }

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x000C395D File Offset: 0x000C1B5D
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.Connection.ServicePoint;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x000C396A File Offset: 0x000C1B6A
		// (set) Token: 0x060037A6 RID: 14246 RVA: 0x000C3972 File Offset: 0x000C1B72
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000C398A File Offset: 0x000C1B8A
		// (set) Token: 0x060037A8 RID: 14248 RVA: 0x000C3992 File Offset: 0x000C1B92
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000C39AA File Offset: 0x000C1BAA
		protected Exception GetException(Exception e)
		{
			e = HttpWebRequest.FlattenException(e);
			if (e is WebException)
			{
				return e;
			}
			if (this.Operation.Aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return e;
		}

		// Token: 0x060037AA RID: 14250
		protected abstract bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result);

		// Token: 0x060037AB RID: 14251 RVA: 0x000C39E4 File Offset: 0x000C1BE4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
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
			int num2;
			if (this.TryReadFromBufferedContent(buffer, offset, count, out num2))
			{
				return num2;
			}
			this.Operation.ThrowIfClosedOrDisposed();
			int result;
			try
			{
				result = this.ReadAsync(buffer, offset, count, CancellationToken.None).Result;
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return result;
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000C3A88 File Offset: 0x000C1C88
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cb, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
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
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), cb, state);
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000C3B04 File Offset: 0x000C1D04
		public override int EndRead(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			int num;
			try
			{
				num = TaskToApm.End<int>(r);
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return num;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000C3B44 File Offset: 0x000C1D44
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cb, object state)
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
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), cb, state);
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000C3BC0 File Offset: 0x000C1DC0
		public override void EndWrite(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			try
			{
				TaskToApm.End(r);
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000C3C00 File Offset: 0x000C1E00
		public override void Write(byte[] buffer, int offset, int count)
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
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			try
			{
				base.WriteAsync(buffer, offset, count).Wait();
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000C3C90 File Offset: 0x000C1E90
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000C3CA7 File Offset: 0x000C1EA7
		internal void InternalClose()
		{
			this.disposed = true;
		}

		// Token: 0x060037B4 RID: 14260
		protected abstract void Close_internal(ref bool disposed);

		// Token: 0x060037B5 RID: 14261 RVA: 0x000C3CB0 File Offset: 0x000C1EB0
		public override void Close()
		{
			this.Close_internal(ref this.disposed);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000C3CBE File Offset: 0x000C1EBE
		public override long Seek(long a, SeekOrigin b)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000C3CBE File Offset: 0x000C1EBE
		public override void SetLength(long a)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x000C3CBE File Offset: 0x000C1EBE
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000C3CBE File Offset: 0x000C1EBE
		// (set) Token: 0x060037BB RID: 14267 RVA: 0x000C3CBE File Offset: 0x000C1EBE
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
			set
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		// Token: 0x0400209C RID: 8348
		protected bool closed;

		// Token: 0x0400209D RID: 8349
		private bool disposed;

		// Token: 0x0400209E RID: 8350
		private object locker = new object();

		// Token: 0x0400209F RID: 8351
		private int read_timeout;

		// Token: 0x040020A0 RID: 8352
		private int write_timeout;

		// Token: 0x040020A1 RID: 8353
		internal bool IgnoreIOErrors;
	}
}
