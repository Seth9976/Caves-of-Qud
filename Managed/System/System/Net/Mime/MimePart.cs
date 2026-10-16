using System;
using System.IO;
using System.Net.Mail;
using System.Runtime.ExceptionServices;

namespace System.Net.Mime
{
	// Token: 0x020007E1 RID: 2017
	internal class MimePart : MimeBasePart, IDisposable
	{
		// Token: 0x0600407A RID: 16506 RVA: 0x000DC6BC File Offset: 0x000DA8BC
		internal MimePart()
		{
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x000DC6C4 File Offset: 0x000DA8C4
		public void Dispose()
		{
			if (this._stream != null)
			{
				this._stream.Close();
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000DC6D9 File Offset: 0x000DA8D9
		internal Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x000DC6E1 File Offset: 0x000DA8E1
		// (set) Token: 0x0600407E RID: 16510 RVA: 0x000DC6E9 File Offset: 0x000DA8E9
		internal ContentDisposition ContentDisposition
		{
			get
			{
				return this._contentDisposition;
			}
			set
			{
				this._contentDisposition = value;
				if (value == null)
				{
					((HeaderCollection)base.Headers).InternalRemove(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition));
					return;
				}
				this._contentDisposition.PersistIfNeeded((HeaderCollection)base.Headers, true);
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x000DC724 File Offset: 0x000DA924
		// (set) Token: 0x06004080 RID: 16512 RVA: 0x000DC784 File Offset: 0x000DA984
		internal TransferEncoding TransferEncoding
		{
			get
			{
				string text = base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)];
				if (text.Equals("base64", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.Base64;
				}
				if (text.Equals("quoted-printable", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.QuotedPrintable;
				}
				if (text.Equals("7bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.SevenBit;
				}
				if (text.Equals("8bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.EightBit;
				}
				return TransferEncoding.Unknown;
			}
			set
			{
				if (value == TransferEncoding.Base64)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "base64";
					return;
				}
				if (value == TransferEncoding.QuotedPrintable)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "quoted-printable";
					return;
				}
				if (value == TransferEncoding.SevenBit)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "7bit";
					return;
				}
				if (value == TransferEncoding.EightBit)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "8bit";
					return;
				}
				throw new NotSupportedException(SR.Format("The MIME transfer encoding '{0}' is not supported.", value));
			}
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000DC814 File Offset: 0x000DAA14
		internal void SetContent(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this._streamSet)
			{
				this._stream.Close();
				this._stream = null;
				this._streamSet = false;
			}
			this._stream = stream;
			this._streamSet = true;
			this._streamUsedOnce = false;
			this.TransferEncoding = TransferEncoding.Base64;
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000DC86C File Offset: 0x000DAA6C
		internal void SetContent(Stream stream, string name, string mimeType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (mimeType != null && mimeType != string.Empty)
			{
				this._contentType = new ContentType(mimeType);
			}
			if (name != null && name != string.Empty)
			{
				base.ContentType.Name = name;
			}
			this.SetContent(stream);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000DC8C6 File Offset: 0x000DAAC6
		internal void SetContent(Stream stream, ContentType contentType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._contentType = contentType;
			this.SetContent(stream);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000DC8E4 File Offset: 0x000DAAE4
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			if (mimePartContext._completed)
			{
				ExceptionDispatchInfo.Throw(e);
			}
			try
			{
				if (mimePartContext._outputStream != null)
				{
					mimePartContext._outputStream.Close();
				}
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext._completed = true;
			mimePartContext._result.InvokeCallback(e);
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000DC950 File Offset: 0x000DAB50
		internal void ReadCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.ReadCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x000DC99C File Offset: 0x000DAB9C
		internal void ReadCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			mimePartContext._bytesLeft = this.Stream.EndRead(result);
			if (mimePartContext._bytesLeft > 0)
			{
				IAsyncResult asyncResult = mimePartContext._outputStream.BeginWrite(mimePartContext._buffer, 0, mimePartContext._bytesLeft, this._writeCallback, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.WriteCallbackHandler(asyncResult);
					return;
				}
			}
			else
			{
				this.Complete(result, null);
			}
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x000DCA08 File Offset: 0x000DAC08
		internal void WriteCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.WriteCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x000DCA54 File Offset: 0x000DAC54
		internal void WriteCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			mimePartContext._outputStream.EndWrite(result);
			IAsyncResult asyncResult = this.Stream.BeginRead(mimePartContext._buffer, 0, mimePartContext._buffer.Length, this._readCallback, mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadCallbackHandler(asyncResult);
			}
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x000DCAAC File Offset: 0x000DACAC
		internal Stream GetEncodedStream(Stream stream)
		{
			Stream stream2 = stream;
			if (this.TransferEncoding == TransferEncoding.Base64)
			{
				stream2 = new Base64Stream(stream2, new Base64WriteStateInfo());
			}
			else if (this.TransferEncoding == TransferEncoding.QuotedPrintable)
			{
				stream2 = new QuotedPrintableStream(stream2, true);
			}
			else if (this.TransferEncoding == TransferEncoding.SevenBit || this.TransferEncoding == TransferEncoding.EightBit)
			{
				stream2 = new EightBitStream(stream2);
			}
			return stream2;
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000DCB00 File Offset: 0x000DAD00
		internal void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			Stream stream = mimePartContext._writer.EndGetContentStream(result);
			mimePartContext._outputStream = this.GetEncodedStream(stream);
			this._readCallback = new AsyncCallback(this.ReadCallback);
			this._writeCallback = new AsyncCallback(this.WriteCallback);
			IAsyncResult asyncResult = this.Stream.BeginRead(mimePartContext._buffer, 0, mimePartContext._buffer.Length, this._readCallback, mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadCallbackHandler(asyncResult);
			}
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x000DCB88 File Offset: 0x000DAD88
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x000DCBD4 File Offset: 0x000DADD4
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult mimePartAsyncResult = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimePart.MimePartContext mimePartContext = new MimePart.MimePartContext(writer, mimePartAsyncResult);
			this.ResetStream();
			this._streamUsedOnce = true;
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return mimePartAsyncResult;
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x000DCC38 File Offset: 0x000DAE38
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			if (this.Stream != null)
			{
				byte[] array = new byte[17408];
				base.PrepareHeaders(allowUnicode);
				writer.WriteHeaders(base.Headers, allowUnicode);
				Stream stream = writer.GetContentStream();
				stream = this.GetEncodedStream(stream);
				this.ResetStream();
				this._streamUsedOnce = true;
				int num;
				while ((num = this.Stream.Read(array, 0, 17408)) > 0)
				{
					stream.Write(array, 0, num);
				}
				stream.Close();
			}
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x000DCCB0 File Offset: 0x000DAEB0
		internal void ResetStream()
		{
			if (!this._streamUsedOnce)
			{
				return;
			}
			if (this.Stream.CanSeek)
			{
				this.Stream.Seek(0L, SeekOrigin.Begin);
				this._streamUsedOnce = false;
				return;
			}
			throw new InvalidOperationException("One of the streams has already been used and can't be reset to the origin.");
		}

		// Token: 0x040026BE RID: 9918
		private Stream _stream;

		// Token: 0x040026BF RID: 9919
		private bool _streamSet;

		// Token: 0x040026C0 RID: 9920
		private bool _streamUsedOnce;

		// Token: 0x040026C1 RID: 9921
		private AsyncCallback _readCallback;

		// Token: 0x040026C2 RID: 9922
		private AsyncCallback _writeCallback;

		// Token: 0x040026C3 RID: 9923
		private const int maxBufferSize = 17408;

		// Token: 0x020007E2 RID: 2018
		internal class MimePartContext
		{
			// Token: 0x0600408F RID: 16527 RVA: 0x000DCCE9 File Offset: 0x000DAEE9
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result)
			{
				this._writer = writer;
				this._result = result;
				this._buffer = new byte[17408];
			}

			// Token: 0x040026C4 RID: 9924
			internal Stream _outputStream;

			// Token: 0x040026C5 RID: 9925
			internal LazyAsyncResult _result;

			// Token: 0x040026C6 RID: 9926
			internal int _bytesLeft;

			// Token: 0x040026C7 RID: 9927
			internal BaseWriter _writer;

			// Token: 0x040026C8 RID: 9928
			internal byte[] _buffer;

			// Token: 0x040026C9 RID: 9929
			internal bool _completed;

			// Token: 0x040026CA RID: 9930
			internal bool _completedSynchronously = true;
		}
	}
}
