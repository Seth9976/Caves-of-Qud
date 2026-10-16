using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x0200023E RID: 574
	internal class AsyncStreamReader : IDisposable
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x0004B574 File Offset: 0x00049774
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding)
			: this(process, stream, callback, encoding, 1024)
		{
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0004B586 File Offset: 0x00049786
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.Init(process, stream, callback, encoding, bufferSize);
			this.messageQueue = new Queue();
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004B5B4 File Offset: 0x000497B4
		private void Init(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.process = process;
			this.stream = stream;
			this.encoding = encoding;
			this.userCallBack = callback;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.cancelOperation = false;
			this.eofEvent = new ManualResetEvent(false);
			this.sb = null;
			this.bLastCarriageReturn = false;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0004B649 File Offset: 0x00049849
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0004B652 File Offset: 0x00049852
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0004B664 File Offset: 0x00049864
		protected virtual void Dispose(bool disposing)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				if (disposing && this.stream != null)
				{
					if (this.asyncReadResult != null && !this.asyncReadResult.IsCompleted && this.stream is FileStream)
					{
						SafeHandle safeFileHandle = ((FileStream)this.stream).SafeFileHandle;
						MonoIOError monoIOError;
						while (!this.asyncReadResult.IsCompleted && (MonoIO.Cancel(safeFileHandle, out monoIOError) || monoIOError != MonoIOError.ERROR_NOT_SUPPORTED))
						{
							this.asyncReadResult.AsyncWaitHandle.WaitOne(200);
						}
					}
					this.stream.Close();
				}
				if (this.stream != null)
				{
					this.stream = null;
					this.encoding = null;
					this.decoder = null;
					this.byteBuffer = null;
					this.charBuffer = null;
				}
				if (this.eofEvent != null)
				{
					this.eofEvent.Close();
					this.eofEvent = null;
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0004B760 File Offset: 0x00049960
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0004B768 File Offset: 0x00049968
		public virtual Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0004B770 File Offset: 0x00049970
		internal void BeginReadLine()
		{
			if (this.cancelOperation)
			{
				this.cancelOperation = false;
			}
			if (this.sb == null)
			{
				this.sb = new StringBuilder(1024);
				this.asyncReadResult = this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
				return;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0004B7D8 File Offset: 0x000499D8
		internal void CancelOperation()
		{
			this.cancelOperation = true;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0004B7E4 File Offset: 0x000499E4
		private void ReadBuffer(IAsyncResult ar)
		{
			int num;
			try
			{
				object obj = this.syncObject;
				lock (obj)
				{
					this.asyncReadResult = null;
					if (this.stream == null)
					{
						num = 0;
					}
					else
					{
						num = this.stream.EndRead(ar);
					}
				}
			}
			catch (IOException)
			{
				num = 0;
			}
			catch (OperationCanceledException)
			{
				num = 0;
			}
			for (;;)
			{
				object obj;
				if (num == 0)
				{
					Queue queue = this.messageQueue;
					lock (queue)
					{
						if (this.sb.Length != 0)
						{
							this.messageQueue.Enqueue(this.sb.ToString());
							this.sb.Length = 0;
						}
						this.messageQueue.Enqueue(null);
					}
					try
					{
						this.FlushMessageQueue();
						break;
					}
					finally
					{
						obj = this.syncObject;
						lock (obj)
						{
							if (this.eofEvent != null)
							{
								try
								{
									this.eofEvent.Set();
								}
								catch (ObjectDisposedException)
								{
								}
							}
						}
					}
				}
				obj = this.syncObject;
				lock (obj)
				{
					if (this.decoder == null)
					{
						num = 0;
						continue;
					}
					int chars = this.decoder.GetChars(this.byteBuffer, 0, num, this.charBuffer, 0);
					this.sb.Append(this.charBuffer, 0, chars);
				}
				this.GetLinesFromStringBuilder();
				obj = this.syncObject;
				lock (obj)
				{
					if (this.stream == null)
					{
						num = 0;
						continue;
					}
					this.asyncReadResult = this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
				}
				break;
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0004BA08 File Offset: 0x00049C08
		private void GetLinesFromStringBuilder()
		{
			int i = this.currentLinePos;
			int num = 0;
			int length = this.sb.Length;
			if (this.bLastCarriageReturn && length > 0 && this.sb[0] == '\n')
			{
				i = 1;
				num = 1;
				this.bLastCarriageReturn = false;
			}
			while (i < length)
			{
				char c = this.sb[i];
				if (c == '\r' || c == '\n')
				{
					string text = this.sb.ToString(num, i - num);
					num = i + 1;
					if (c == '\r' && num < length && this.sb[num] == '\n')
					{
						num++;
						i++;
					}
					Queue queue = this.messageQueue;
					lock (queue)
					{
						this.messageQueue.Enqueue(text);
					}
				}
				i++;
			}
			if (this.sb[length - 1] == '\r')
			{
				this.bLastCarriageReturn = true;
			}
			if (num < length)
			{
				if (num == 0)
				{
					this.currentLinePos = i;
				}
				else
				{
					this.sb.Remove(0, num);
					this.currentLinePos = 0;
				}
			}
			else
			{
				this.sb.Length = 0;
				this.currentLinePos = 0;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0004BB50 File Offset: 0x00049D50
		private void FlushMessageQueue()
		{
			while (this.messageQueue.Count > 0)
			{
				Queue queue = this.messageQueue;
				lock (queue)
				{
					if (this.messageQueue.Count > 0)
					{
						string text = (string)this.messageQueue.Dequeue();
						if (!this.cancelOperation)
						{
							this.userCallBack(text);
						}
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0004BBCC File Offset: 0x00049DCC
		internal void WaitUtilEOF()
		{
			if (this.eofEvent != null)
			{
				this.eofEvent.WaitOne();
				this.eofEvent.Close();
				this.eofEvent = null;
			}
		}

		// Token: 0x04000A3D RID: 2621
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04000A3E RID: 2622
		private const int MinBufferSize = 128;

		// Token: 0x04000A3F RID: 2623
		private Stream stream;

		// Token: 0x04000A40 RID: 2624
		private Encoding encoding;

		// Token: 0x04000A41 RID: 2625
		private Decoder decoder;

		// Token: 0x04000A42 RID: 2626
		private byte[] byteBuffer;

		// Token: 0x04000A43 RID: 2627
		private char[] charBuffer;

		// Token: 0x04000A44 RID: 2628
		private int _maxCharsPerBuffer;

		// Token: 0x04000A45 RID: 2629
		private Process process;

		// Token: 0x04000A46 RID: 2630
		private UserCallBack userCallBack;

		// Token: 0x04000A47 RID: 2631
		private bool cancelOperation;

		// Token: 0x04000A48 RID: 2632
		private ManualResetEvent eofEvent;

		// Token: 0x04000A49 RID: 2633
		private Queue messageQueue;

		// Token: 0x04000A4A RID: 2634
		private StringBuilder sb;

		// Token: 0x04000A4B RID: 2635
		private bool bLastCarriageReturn;

		// Token: 0x04000A4C RID: 2636
		private int currentLinePos;

		// Token: 0x04000A4D RID: 2637
		private object syncObject = new object();

		// Token: 0x04000A4E RID: 2638
		private IAsyncResult asyncReadResult;
	}
}
