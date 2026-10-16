using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;
using System.Runtime.ExceptionServices;

namespace System.Net.Mime
{
	// Token: 0x020007CF RID: 1999
	internal abstract class BaseWriter
	{
		// Token: 0x06003FF8 RID: 16376 RVA: 0x000DA624 File Offset: 0x000D8824
		protected BaseWriter(Stream stream, bool shouldEncodeLeadingDots)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._stream = stream;
			this._shouldEncodeLeadingDots = shouldEncodeLeadingDots;
			this._onCloseHandler = new EventHandler(this.OnClose);
			this._bufferBuilder = new BufferBuilder();
			this._lineLength = 76;
		}

		// Token: 0x06003FF9 RID: 16377
		internal abstract void WriteHeaders(NameValueCollection headers, bool allowUnicode);

		// Token: 0x06003FFA RID: 16378 RVA: 0x000DA67C File Offset: 0x000D887C
		internal void WriteHeader(string name, string value, bool allowUnicode)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this._isInContent)
			{
				throw new InvalidOperationException("This operation cannot be performed while in content.");
			}
			this.CheckBoundary();
			this._bufferBuilder.Append(name);
			this._bufferBuilder.Append(": ");
			this.WriteAndFold(value, name.Length + 2, allowUnicode);
			this._bufferBuilder.Append(BaseWriter.s_crlf);
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000DA6FC File Offset: 0x000D88FC
		private void WriteAndFold(string value, int charsAlreadyOnLine, bool allowUnicode)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < value.Length; i++)
			{
				if (MailBnfHelper.IsFWSAt(value, i))
				{
					i += 2;
					this._bufferBuilder.Append(value, num2, i - num2, allowUnicode);
					num2 = i;
					num = i;
					charsAlreadyOnLine = 0;
				}
				else if (i - num2 > this._lineLength - charsAlreadyOnLine && num != num2)
				{
					this._bufferBuilder.Append(value, num2, num - num2, allowUnicode);
					this._bufferBuilder.Append(BaseWriter.s_crlf);
					num2 = num;
					charsAlreadyOnLine = 0;
				}
				else if (value[i] == MailBnfHelper.Space || value[i] == MailBnfHelper.Tab)
				{
					num = i;
				}
			}
			if (value.Length - num2 > 0)
			{
				this._bufferBuilder.Append(value, num2, value.Length - num2, allowUnicode);
			}
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000DA7C3 File Offset: 0x000D89C3
		internal Stream GetContentStream()
		{
			return this.GetContentStream(null);
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000DA7CC File Offset: 0x000D89CC
		private Stream GetContentStream(MultiAsyncResult multiResult)
		{
			if (this._isInContent)
			{
				throw new InvalidOperationException("This operation cannot be performed while in content.");
			}
			this._isInContent = true;
			this.CheckBoundary();
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			this.Flush(multiResult);
			ClosableStream closableStream = new ClosableStream(new EightBitStream(this._stream, this._shouldEncodeLeadingDots), this._onCloseHandler);
			this._contentStream = closableStream;
			return closableStream;
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000DA838 File Offset: 0x000D8A38
		internal IAsyncResult BeginGetContentStream(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			Stream contentStream = this.GetContentStream(multiAsyncResult);
			if (!(multiAsyncResult.Result is Exception))
			{
				multiAsyncResult.Result = contentStream;
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000DA874 File Offset: 0x000D8A74
		internal Stream EndGetContentStream(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			Exception ex = obj as Exception;
			if (ex != null)
			{
				ExceptionDispatchInfo.Throw(ex);
			}
			return (Stream)obj;
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000DA89C File Offset: 0x000D8A9C
		protected void Flush(MultiAsyncResult multiResult)
		{
			if (this._bufferBuilder.Length > 0)
			{
				if (multiResult != null)
				{
					multiResult.Enter();
					IAsyncResult asyncResult = this._stream.BeginWrite(this._bufferBuilder.GetBuffer(), 0, this._bufferBuilder.Length, BaseWriter.s_onWrite, multiResult);
					if (asyncResult.CompletedSynchronously)
					{
						this._stream.EndWrite(asyncResult);
						multiResult.Leave();
					}
				}
				else
				{
					this._stream.Write(this._bufferBuilder.GetBuffer(), 0, this._bufferBuilder.Length);
				}
				this._bufferBuilder.Reset();
			}
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000DA934 File Offset: 0x000D8B34
		protected static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				BaseWriter baseWriter = (BaseWriter)multiAsyncResult.Context;
				try
				{
					baseWriter._stream.EndWrite(result);
					multiAsyncResult.Leave();
				}
				catch (Exception ex)
				{
					multiAsyncResult.Leave(ex);
				}
			}
		}

		// Token: 0x06004002 RID: 16386
		internal abstract void Close();

		// Token: 0x06004003 RID: 16387
		protected abstract void OnClose(object sender, EventArgs args);

		// Token: 0x06004004 RID: 16388 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void CheckBoundary()
		{
		}

		// Token: 0x04002673 RID: 9843
		private const int DefaultLineLength = 76;

		// Token: 0x04002674 RID: 9844
		private static readonly AsyncCallback s_onWrite = new AsyncCallback(BaseWriter.OnWrite);

		// Token: 0x04002675 RID: 9845
		protected static readonly byte[] s_crlf = new byte[] { 13, 10 };

		// Token: 0x04002676 RID: 9846
		protected readonly BufferBuilder _bufferBuilder;

		// Token: 0x04002677 RID: 9847
		protected readonly Stream _stream;

		// Token: 0x04002678 RID: 9848
		private readonly EventHandler _onCloseHandler;

		// Token: 0x04002679 RID: 9849
		private readonly bool _shouldEncodeLeadingDots;

		// Token: 0x0400267A RID: 9850
		private int _lineLength;

		// Token: 0x0400267B RID: 9851
		protected Stream _contentStream;

		// Token: 0x0400267C RID: 9852
		protected bool _isInContent;
	}
}
