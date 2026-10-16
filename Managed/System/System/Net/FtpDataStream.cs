using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Net
{
	// Token: 0x0200058D RID: 1421
	internal class FtpDataStream : Stream, ICloseEx
	{
		// Token: 0x06002DF3 RID: 11763 RVA: 0x0009E5D8 File Offset: 0x0009C7D8
		internal FtpDataStream(NetworkStream networkStream, FtpWebRequest request, TriState writeOnly)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, ".ctor");
			}
			this._readable = true;
			this._writeable = true;
			if (writeOnly == TriState.True)
			{
				this._readable = false;
			}
			else if (writeOnly == TriState.False)
			{
				this._writeable = false;
			}
			this._networkStream = networkStream;
			this._request = request;
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x0009E634 File Offset: 0x0009C834
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((ICloseEx)this).CloseEx(CloseExState.Normal);
				}
				else
				{
					((ICloseEx)this).CloseEx(CloseExState.Abort | CloseExState.Silent);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0009E670 File Offset: 0x0009C870
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("state = {0}", new object[] { closeState }), "CloseEx");
			}
			lock (this)
			{
				if (this._closing)
				{
					return;
				}
				this._closing = true;
				this._writeable = false;
				this._readable = false;
			}
			try
			{
				try
				{
					if ((closeState & CloseExState.Abort) == CloseExState.Normal)
					{
						this._networkStream.Close(-1);
					}
					else
					{
						this._networkStream.Close(0);
					}
				}
				finally
				{
					this._request.DataStreamClosed(closeState);
				}
			}
			catch (Exception ex)
			{
				bool flag2 = true;
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					FtpWebResponse ftpWebResponse = ex2.Response as FtpWebResponse;
					if (ftpWebResponse != null && !this._isFullyRead && ftpWebResponse.StatusCode == FtpStatusCode.ConnectionClosed)
					{
						flag2 = false;
					}
				}
				if (flag2 && (closeState & CloseExState.Silent) == CloseExState.Normal)
				{
					throw;
				}
			}
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0009E77C File Offset: 0x0009C97C
		private void CheckError()
		{
			if (this._request.Aborted)
			{
				throw ExceptionHelper.RequestAbortedException;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x0009E791 File Offset: 0x0009C991
		public override bool CanRead
		{
			get
			{
				return this._readable;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x0009E799 File Offset: 0x0009C999
		public override bool CanSeek
		{
			get
			{
				return this._networkStream.CanSeek;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x0009E7A6 File Offset: 0x0009C9A6
		public override bool CanWrite
		{
			get
			{
				return this._writeable;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x0009E7AE File Offset: 0x0009C9AE
		public override long Length
		{
			get
			{
				return this._networkStream.Length;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x0009E7BB File Offset: 0x0009C9BB
		// (set) Token: 0x06002DFC RID: 11772 RVA: 0x0009E7C8 File Offset: 0x0009C9C8
		public override long Position
		{
			get
			{
				return this._networkStream.Position;
			}
			set
			{
				this._networkStream.Position = value;
			}
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckError();
			long num;
			try
			{
				num = this._networkStream.Seek(offset, origin);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x0009E818 File Offset: 0x0009CA18
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = this._networkStream.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			if (num == 0)
			{
				this._isFullyRead = true;
				this.Close();
			}
			return num;
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x0009E868 File Offset: 0x0009CA68
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				this._networkStream.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x0009E8A4 File Offset: 0x0009CAA4
		private void AsyncReadCallback(IAsyncResult ar)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar.AsyncState;
			try
			{
				try
				{
					int num = this._networkStream.EndRead(ar);
					if (num == 0)
					{
						this._isFullyRead = true;
						this.Close();
					}
					lazyAsyncResult.InvokeCallback(num);
				}
				catch (Exception ex)
				{
					if (!lazyAsyncResult.IsCompleted)
					{
						lazyAsyncResult.InvokeCallback(ex);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x0009E91C File Offset: 0x0009CB1C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			try
			{
				this._networkStream.BeginRead(buffer, offset, size, new AsyncCallback(this.AsyncReadCallback), lazyAsyncResult);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return lazyAsyncResult;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x0009E974 File Offset: 0x0009CB74
		public override int EndRead(IAsyncResult ar)
		{
			int num;
			try
			{
				object obj = ((LazyAsyncResult)ar).InternalWaitForCompletion();
				Exception ex = obj as Exception;
				if (ex != null)
				{
					ExceptionDispatchInfo.Throw(ex);
				}
				num = (int)obj;
			}
			finally
			{
				this.CheckError();
			}
			return num;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0009E9BC File Offset: 0x0009CBBC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this._networkStream.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x0009EA00 File Offset: 0x0009CC00
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				this._networkStream.EndWrite(asyncResult);
			}
			finally
			{
				this.CheckError();
			}
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0009EA34 File Offset: 0x0009CC34
		public override void Flush()
		{
			this._networkStream.Flush();
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x0009EA41 File Offset: 0x0009CC41
		public override void SetLength(long value)
		{
			this._networkStream.SetLength(value);
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x0009EA4F File Offset: 0x0009CC4F
		public override bool CanTimeout
		{
			get
			{
				return this._networkStream.CanTimeout;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x0009EA5C File Offset: 0x0009CC5C
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x0009EA69 File Offset: 0x0009CC69
		public override int ReadTimeout
		{
			get
			{
				return this._networkStream.ReadTimeout;
			}
			set
			{
				this._networkStream.ReadTimeout = value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x0009EA77 File Offset: 0x0009CC77
		// (set) Token: 0x06002E0B RID: 11787 RVA: 0x0009EA84 File Offset: 0x0009CC84
		public override int WriteTimeout
		{
			get
			{
				return this._networkStream.WriteTimeout;
			}
			set
			{
				this._networkStream.WriteTimeout = value;
			}
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0009EA92 File Offset: 0x0009CC92
		internal void SetSocketTimeoutOption(int timeout)
		{
			this._networkStream.ReadTimeout = timeout;
			this._networkStream.WriteTimeout = timeout;
		}

		// Token: 0x04001964 RID: 6500
		private FtpWebRequest _request;

		// Token: 0x04001965 RID: 6501
		private NetworkStream _networkStream;

		// Token: 0x04001966 RID: 6502
		private bool _writeable;

		// Token: 0x04001967 RID: 6503
		private bool _readable;

		// Token: 0x04001968 RID: 6504
		private bool _isFullyRead;

		// Token: 0x04001969 RID: 6505
		private bool _closing;

		// Token: 0x0400196A RID: 6506
		private const int DefaultCloseTimeout = -1;
	}
}
