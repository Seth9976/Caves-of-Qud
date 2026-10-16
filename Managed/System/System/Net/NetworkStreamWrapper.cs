using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000598 RID: 1432
	internal class NetworkStreamWrapper : Stream
	{
		// Token: 0x06002E75 RID: 11893 RVA: 0x000A0AC6 File Offset: 0x0009ECC6
		internal NetworkStreamWrapper(TcpClient client)
		{
			this._client = client;
			this._networkStream = client.GetStream();
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000A0AE1 File Offset: 0x0009ECE1
		protected bool UsingSecureStream
		{
			get
			{
				return this._networkStream is TlsStream;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000A0AF1 File Offset: 0x0009ECF1
		internal IPAddress ServerAddress
		{
			get
			{
				return ((IPEndPoint)this.Socket.RemoteEndPoint).Address;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000A0B08 File Offset: 0x0009ED08
		internal Socket Socket
		{
			get
			{
				return this._client.Client;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x000A0B15 File Offset: 0x0009ED15
		// (set) Token: 0x06002E7A RID: 11898 RVA: 0x000A0B1D File Offset: 0x0009ED1D
		internal NetworkStream NetworkStream
		{
			get
			{
				return this._networkStream;
			}
			set
			{
				this._networkStream = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x000A0B26 File Offset: 0x0009ED26
		public override bool CanRead
		{
			get
			{
				return this._networkStream.CanRead;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x000A0B33 File Offset: 0x0009ED33
		public override bool CanSeek
		{
			get
			{
				return this._networkStream.CanSeek;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000A0B40 File Offset: 0x0009ED40
		public override bool CanWrite
		{
			get
			{
				return this._networkStream.CanWrite;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000A0B4D File Offset: 0x0009ED4D
		public override bool CanTimeout
		{
			get
			{
				return this._networkStream.CanTimeout;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000A0B5A File Offset: 0x0009ED5A
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000A0B67 File Offset: 0x0009ED67
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

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000A0B75 File Offset: 0x0009ED75
		// (set) Token: 0x06002E82 RID: 11906 RVA: 0x000A0B82 File Offset: 0x0009ED82
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

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000A0B90 File Offset: 0x0009ED90
		public override long Length
		{
			get
			{
				return this._networkStream.Length;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000A0B9D File Offset: 0x0009ED9D
		// (set) Token: 0x06002E85 RID: 11909 RVA: 0x000A0BAA File Offset: 0x0009EDAA
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

		// Token: 0x06002E86 RID: 11910 RVA: 0x000A0BB8 File Offset: 0x0009EDB8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._networkStream.Seek(offset, origin);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000A0BC7 File Offset: 0x0009EDC7
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this._networkStream.Read(buffer, offset, size);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000A0BD7 File Offset: 0x0009EDD7
		public override void Write(byte[] buffer, int offset, int size)
		{
			this._networkStream.Write(buffer, offset, size);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000A0BE8 File Offset: 0x0009EDE8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseSocket();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000A0C18 File Offset: 0x0009EE18
		internal void CloseSocket()
		{
			this._networkStream.Close();
			this._client.Dispose();
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000A0C30 File Offset: 0x0009EE30
		public void Close(int timeout)
		{
			this._networkStream.Close(timeout);
			this._client.Dispose();
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000A0C49 File Offset: 0x0009EE49
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._networkStream.BeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000A0C5D File Offset: 0x0009EE5D
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._networkStream.EndRead(asyncResult);
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000A0C6B File Offset: 0x0009EE6B
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._networkStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000A0C7D File Offset: 0x0009EE7D
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._networkStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000A0C91 File Offset: 0x0009EE91
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._networkStream.EndWrite(asyncResult);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000A0C9F File Offset: 0x0009EE9F
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._networkStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000A0CB1 File Offset: 0x0009EEB1
		public override void Flush()
		{
			this._networkStream.Flush();
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000A0CBE File Offset: 0x0009EEBE
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._networkStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000A0CCC File Offset: 0x0009EECC
		public override void SetLength(long value)
		{
			this._networkStream.SetLength(value);
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000A0CDA File Offset: 0x0009EEDA
		internal void SetSocketTimeoutOption(int timeout)
		{
			this._networkStream.ReadTimeout = timeout;
			this._networkStream.WriteTimeout = timeout;
		}

		// Token: 0x040019C6 RID: 6598
		private TcpClient _client;

		// Token: 0x040019C7 RID: 6599
		private NetworkStream _networkStream;
	}
}
