using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000671 RID: 1649
	internal class ChunkedInputStream : RequestStream
	{
		// Token: 0x060033EC RID: 13292 RVA: 0x000B4D8C File Offset: 0x000B2F8C
		public ChunkedInputStream(HttpListenerContext context, Stream stream, byte[] buffer, int offset, int length)
			: base(stream, buffer, offset, length)
		{
			this.context = context;
			WebHeaderCollection webHeaderCollection = (WebHeaderCollection)context.Request.Headers;
			this.decoder = new MonoChunkParser(webHeaderCollection);
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000B4DC9 File Offset: 0x000B2FC9
		// (set) Token: 0x060033EE RID: 13294 RVA: 0x000B4DD1 File Offset: 0x000B2FD1
		public MonoChunkParser Decoder
		{
			get
			{
				return this.decoder;
			}
			set
			{
				this.decoder = value;
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000B4DDC File Offset: 0x000B2FDC
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			IAsyncResult asyncResult = this.BeginRead(buffer, offset, count, null, null);
			return this.EndRead(asyncResult);
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000B4DFC File Offset: 0x000B2FFC
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (count < 0 || offset > num - count)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
			httpStreamAsyncResult.Callback = cback;
			httpStreamAsyncResult.State = state;
			if (this.no_more_data)
			{
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			int num2 = this.decoder.Read(buffer, offset, count);
			offset += num2;
			count -= num2;
			if (count == 0)
			{
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (!this.decoder.WantMore)
			{
				this.no_more_data = num2 == 0;
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			httpStreamAsyncResult.Buffer = new byte[8192];
			httpStreamAsyncResult.Offset = 0;
			httpStreamAsyncResult.Count = 8192;
			ChunkedInputStream.ReadBufferState readBufferState = new ChunkedInputStream.ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
			readBufferState.InitialCount += num2;
			base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, new AsyncCallback(this.OnRead), readBufferState);
			return httpStreamAsyncResult;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000B4F34 File Offset: 0x000B3134
		private void OnRead(IAsyncResult base_ares)
		{
			ChunkedInputStream.ReadBufferState readBufferState = (ChunkedInputStream.ReadBufferState)base_ares.AsyncState;
			HttpStreamAsyncResult ares = readBufferState.Ares;
			try
			{
				int num = base.EndRead(base_ares);
				this.decoder.Write(ares.Buffer, ares.Offset, num);
				num = this.decoder.Read(readBufferState.Buffer, readBufferState.Offset, readBufferState.Count);
				readBufferState.Offset += num;
				readBufferState.Count -= num;
				if (readBufferState.Count == 0 || !this.decoder.WantMore || num == 0)
				{
					this.no_more_data = !this.decoder.WantMore && num == 0;
					ares.Count = readBufferState.InitialCount - readBufferState.Count;
					ares.Complete();
				}
				else
				{
					ares.Offset = 0;
					ares.Count = Math.Min(8192, this.decoder.ChunkLeft + 6);
					base.BeginRead(ares.Buffer, ares.Offset, ares.Count, new AsyncCallback(this.OnRead), readBufferState);
				}
			}
			catch (Exception ex)
			{
				this.context.Connection.SendError(ex.Message, 400);
				ares.Complete(ex);
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000B507C File Offset: 0x000B327C
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			HttpStreamAsyncResult httpStreamAsyncResult = ares as HttpStreamAsyncResult;
			if (ares == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "ares");
			}
			if (!ares.IsCompleted)
			{
				ares.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.Error != null)
			{
				throw new HttpListenerException(400, "I/O operation aborted: " + httpStreamAsyncResult.Error.Message);
			}
			return httpStreamAsyncResult.Count;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000B50FE File Offset: 0x000B32FE
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				base.Close();
			}
		}

		// Token: 0x04001E88 RID: 7816
		private bool disposed;

		// Token: 0x04001E89 RID: 7817
		private MonoChunkParser decoder;

		// Token: 0x04001E8A RID: 7818
		private HttpListenerContext context;

		// Token: 0x04001E8B RID: 7819
		private bool no_more_data;

		// Token: 0x02000672 RID: 1650
		private class ReadBufferState
		{
			// Token: 0x060033F4 RID: 13300 RVA: 0x000B5115 File Offset: 0x000B3315
			public ReadBufferState(byte[] buffer, int offset, int count, HttpStreamAsyncResult ares)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
				this.InitialCount = count;
				this.Ares = ares;
			}

			// Token: 0x04001E8C RID: 7820
			public byte[] Buffer;

			// Token: 0x04001E8D RID: 7821
			public int Offset;

			// Token: 0x04001E8E RID: 7822
			public int Count;

			// Token: 0x04001E8F RID: 7823
			public int InitialCount;

			// Token: 0x04001E90 RID: 7824
			public HttpStreamAsyncResult Ares;
		}
	}
}
