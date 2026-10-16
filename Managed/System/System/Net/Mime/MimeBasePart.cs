using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007DC RID: 2012
	internal class MimeBasePart
	{
		// Token: 0x06004057 RID: 16471 RVA: 0x0000219B File Offset: 0x0000039B
		internal MimeBasePart()
		{
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x000DBD7D File Offset: 0x000D9F7D
		internal static bool ShouldUseBase64Encoding(Encoding encoding)
		{
			return encoding == Encoding.Unicode || encoding == Encoding.UTF8 || encoding == Encoding.UTF32 || encoding == Encoding.BigEndianUnicode;
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000DBDA1 File Offset: 0x000D9FA1
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding)
		{
			return MimeBasePart.EncodeHeaderValue(value, encoding, base64Encoding, 0);
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x000DBDAC File Offset: 0x000D9FAC
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding, int headerLength)
		{
			if (MimeBasePart.IsAscii(value, false))
			{
				return value;
			}
			if (encoding == null)
			{
				encoding = Encoding.GetEncoding("utf-8");
			}
			IEncodableStream encoderForHeader = new EncodedStreamFactory().GetEncoderForHeader(encoding, base64Encoding, headerLength);
			byte[] bytes = encoding.GetBytes(value);
			encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
			return encoderForHeader.GetEncodedString();
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x000DBDFC File Offset: 0x000D9FFC
		internal static string DecodeHeaderValue(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			string text = string.Empty;
			string[] array = value.Split(MimeBasePart.s_headerValueSplitChars, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(MimeBasePart.s_questionMarkSplitChars);
				if (array2.Length != 5 || array2[0] != "=" || array2[4] != "=")
				{
					return value;
				}
				string text2 = array2[1];
				bool flag = array2[2] == "B";
				byte[] bytes = Encoding.ASCII.GetBytes(array2[3]);
				int num = new EncodedStreamFactory().GetEncoderForHeader(Encoding.GetEncoding(text2), flag, 0).DecodeBytes(bytes, 0, bytes.Length);
				Encoding encoding = Encoding.GetEncoding(text2);
				text += encoding.GetString(bytes, 0, num);
			}
			return text;
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x000DBED4 File Offset: 0x000DA0D4
		internal static Encoding DecodeEncoding(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			string[] array = value.Split(MimeBasePart.s_decodeEncodingSplitChars);
			if (array.Length < 5 || array[0] != "=" || array[4] != "=")
			{
				return null;
			}
			return Encoding.GetEncoding(array[1]);
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x000DBF28 File Offset: 0x000DA128
		internal static bool IsAscii(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (char c in value)
			{
				if (c > '\u007f')
				{
					return false;
				}
				if (!permitCROrLF && (c == '\r' || c == '\n'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x000DBF75 File Offset: 0x000DA175
		// (set) Token: 0x0600405F RID: 16479 RVA: 0x000DBF88 File Offset: 0x000DA188
		internal string ContentID
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentID));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)] = value;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000DBFB6 File Offset: 0x000DA1B6
		// (set) Token: 0x06004061 RID: 16481 RVA: 0x000DBFC9 File Offset: 0x000DA1C9
		internal string ContentLocation
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentLocation));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)] = value;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x000DBFF8 File Offset: 0x000DA1F8
		internal NameValueCollection Headers
		{
			get
			{
				if (this._headers == null)
				{
					this._headers = new HeaderCollection();
				}
				if (this._contentType == null)
				{
					this._contentType = new ContentType();
				}
				this._contentType.PersistIfNeeded(this._headers, false);
				if (this._contentDisposition != null)
				{
					this._contentDisposition.PersistIfNeeded(this._headers, false);
				}
				return this._headers;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x000DC060 File Offset: 0x000DA260
		// (set) Token: 0x06004064 RID: 16484 RVA: 0x000DC085 File Offset: 0x000DA285
		internal ContentType ContentType
		{
			get
			{
				ContentType contentType;
				if ((contentType = this._contentType) == null)
				{
					contentType = (this._contentType = new ContentType());
				}
				return contentType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._contentType = value;
				this._contentType.PersistIfNeeded((HeaderCollection)this.Headers, true);
			}
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000DC0B4 File Offset: 0x000DA2B4
		internal void PrepareHeaders(bool allowUnicode)
		{
			this._contentType.PersistIfNeeded((HeaderCollection)this.Headers, false);
			this._headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this._contentType.Encode(allowUnicode));
			if (this._contentDisposition != null)
			{
				this._contentDisposition.PersistIfNeeded((HeaderCollection)this.Headers, false);
				this._headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this._contentDisposition.Encode(allowUnicode));
			}
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x0000829A File Offset: 0x0000649A
		internal virtual void Send(BaseWriter writer, bool allowUnicode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x0000829A File Offset: 0x0000649A
		internal virtual IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000DC134 File Offset: 0x000DA334
		internal void EndSend(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as MimeBasePart.MimePartAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException("The IAsyncResult object was not returned from the corresponding asynchronous method on this class.", "asyncResult");
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.Format("{0} can only be called once for each asynchronous operation.", "EndSend"));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			lazyAsyncResult.EndCalled = true;
			if (lazyAsyncResult.Result is Exception)
			{
				throw (Exception)lazyAsyncResult.Result;
			}
		}

		// Token: 0x040026A7 RID: 9895
		internal const string DefaultCharSet = "utf-8";

		// Token: 0x040026A8 RID: 9896
		private static readonly char[] s_decodeEncodingSplitChars = new char[] { '?', '\r', '\n' };

		// Token: 0x040026A9 RID: 9897
		protected ContentType _contentType;

		// Token: 0x040026AA RID: 9898
		protected ContentDisposition _contentDisposition;

		// Token: 0x040026AB RID: 9899
		private HeaderCollection _headers;

		// Token: 0x040026AC RID: 9900
		private static readonly char[] s_headerValueSplitChars = new char[] { '\r', '\n', ' ' };

		// Token: 0x040026AD RID: 9901
		private static readonly char[] s_questionMarkSplitChars = new char[] { '?' };

		// Token: 0x020007DD RID: 2013
		internal class MimePartAsyncResult : LazyAsyncResult
		{
			// Token: 0x0600406A RID: 16490 RVA: 0x000DC1F4 File Offset: 0x000DA3F4
			internal MimePartAsyncResult(MimeBasePart part, object state, AsyncCallback callback)
				: base(part, state, callback)
			{
			}
		}
	}
}
