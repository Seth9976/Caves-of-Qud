using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	// Token: 0x020006AF RID: 1711
	internal class ResponseStream : Stream
	{
		// Token: 0x060036A4 RID: 13988 RVA: 0x000BFB47 File Offset: 0x000BDD47
		internal ResponseStream(Stream stream, HttpListenerResponse response, bool ignore_errors)
		{
			this.response = response;
			this.ignore_errors = ignore_errors;
			this.stream = stream;
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060036A6 RID: 13990 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x060036AA RID: 13994 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000BFB64 File Offset: 0x000BDD64
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				MemoryStream headers = this.GetHeaders(true);
				bool sendChunked = this.response.SendChunked;
				if (this.stream.CanWrite)
				{
					try
					{
						if (headers != null)
						{
							long position = headers.Position;
							if (sendChunked && !this.trailer_sent)
							{
								byte[] array = ResponseStream.GetChunkSizeBytes(0, true);
								headers.Position = headers.Length;
								headers.Write(array, 0, array.Length);
							}
							this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
							this.trailer_sent = true;
						}
						else if (sendChunked && !this.trailer_sent)
						{
							byte[] array = ResponseStream.GetChunkSizeBytes(0, true);
							this.InternalWrite(array, 0, array.Length);
							this.trailer_sent = true;
						}
					}
					catch (IOException)
					{
					}
				}
				this.response.Close();
			}
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000BFC40 File Offset: 0x000BDE40
		private MemoryStream GetHeaders(bool closing)
		{
			object headers_lock = this.response.headers_lock;
			MemoryStream memoryStream;
			lock (headers_lock)
			{
				if (this.response.HeadersSent)
				{
					memoryStream = null;
				}
				else
				{
					MemoryStream memoryStream2 = new MemoryStream();
					this.response.SendHeaders(closing, memoryStream2);
					memoryStream = memoryStream2;
				}
			}
			return memoryStream;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000BFCA8 File Offset: 0x000BDEA8
		private static byte[] GetChunkSizeBytes(int size, bool final)
		{
			string text = string.Format("{0:x}\r\n{1}", size, final ? "\r\n" : "");
			return Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000BFCE0 File Offset: 0x000BDEE0
		internal void InternalWrite(byte[] buffer, int offset, int count)
		{
			if (this.ignore_errors)
			{
				try
				{
					this.stream.Write(buffer, offset, count);
					return;
				}
				catch
				{
					return;
				}
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000BFD28 File Offset: 0x000BDF28
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (count == 0)
			{
				return;
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(array, 0, array.Length);
				}
				int num = Math.Min(count, 16384 - (int)headers.Position + (int)position);
				headers.Write(buffer, offset, num);
				count -= num;
				offset += num;
				this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
				headers.SetLength(0L);
				headers.Capacity = 0;
			}
			else if (sendChunked)
			{
				byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(array, 0, array.Length);
			}
			if (count > 0)
			{
				this.InternalWrite(buffer, offset, count);
			}
			if (sendChunked)
			{
				this.InternalWrite(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000BFE20 File Offset: 0x000BE020
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(array, 0, array.Length);
				}
				headers.Write(buffer, offset, count);
				buffer = headers.GetBuffer();
				offset = (int)position;
				count = (int)(headers.Position - position);
			}
			else if (sendChunked)
			{
				byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(array, 0, array.Length);
			}
			return this.stream.BeginWrite(buffer, offset, count, cback, state);
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000BFED4 File Offset: 0x000BE0D4
		public override void EndWrite(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (this.ignore_errors)
			{
				try
				{
					this.stream.EndWrite(ares);
					if (this.response.SendChunked)
					{
						this.stream.Write(ResponseStream.crlf, 0, 2);
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.stream.EndWrite(ares);
			if (this.response.SendChunked)
			{
				this.stream.Write(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000044FA File Offset: 0x000026FA
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000044FA File Offset: 0x000026FA
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000044FA File Offset: 0x000026FA
		public override int EndRead(IAsyncResult ares)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001FFF RID: 8191
		private HttpListenerResponse response;

		// Token: 0x04002000 RID: 8192
		private bool ignore_errors;

		// Token: 0x04002001 RID: 8193
		private bool disposed;

		// Token: 0x04002002 RID: 8194
		private bool trailer_sent;

		// Token: 0x04002003 RID: 8195
		private Stream stream;

		// Token: 0x04002004 RID: 8196
		private static byte[] crlf = new byte[] { 13, 10 };
	}
}
