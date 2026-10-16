using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007E3 RID: 2019
	internal class MimeWriter : BaseWriter
	{
		// Token: 0x06004090 RID: 16528 RVA: 0x000DCD16 File Offset: 0x000DAF16
		internal MimeWriter(Stream stream, string boundary)
			: base(stream, false)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			this._boundaryBytes = Encoding.ASCII.GetBytes(boundary);
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x000DCD48 File Offset: 0x000DAF48
		internal override void WriteHeaders(NameValueCollection headers, bool allowUnicode)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string text = (string)obj;
				base.WriteHeader(text, headers[text], allowUnicode);
			}
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x000DCDB4 File Offset: 0x000DAFB4
		internal IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			this.Close(multiAsyncResult);
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x000DCDD8 File Offset: 0x000DAFD8
		internal void EndClose(IAsyncResult result)
		{
			MultiAsyncResult.End(result);
			this._stream.Close();
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x000DCDEC File Offset: 0x000DAFEC
		internal override void Close()
		{
			this.Close(null);
			this._stream.Close();
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x000DCE00 File Offset: 0x000DB000
		private void Close(MultiAsyncResult multiResult)
		{
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
			this._bufferBuilder.Append(this._boundaryBytes);
			this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			base.Flush(multiResult);
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x000DCE65 File Offset: 0x000DB065
		protected override void OnClose(object sender, EventArgs args)
		{
			if (this._contentStream != sender)
			{
				return;
			}
			this._contentStream.Flush();
			this._contentStream = null;
			this._writeBoundary = true;
			this._isInContent = false;
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x000DCE94 File Offset: 0x000DB094
		protected override void CheckBoundary()
		{
			if (this._writeBoundary)
			{
				this._bufferBuilder.Append(BaseWriter.s_crlf);
				this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
				this._bufferBuilder.Append(this._boundaryBytes);
				this._bufferBuilder.Append(BaseWriter.s_crlf);
				this._writeBoundary = false;
			}
		}

		// Token: 0x040026CB RID: 9931
		private static byte[] s_DASHDASH = new byte[] { 45, 45 };

		// Token: 0x040026CC RID: 9932
		private byte[] _boundaryBytes;

		// Token: 0x040026CD RID: 9933
		private bool _writeBoundary = true;
	}
}
