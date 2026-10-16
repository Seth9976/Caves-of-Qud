using System;
using System.IO;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008D RID: 141
	internal class UnixConnection
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		public UnixConnection(HostConnectionPool pool, ReusableUnixClient client)
		{
			this._pool = pool;
			this._client = client;
			this._stream = new BufferedStream(client.GetStream());
			this._controlTime = DateTime.UtcNow;
			this._buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000F215 File Offset: 0x0000D415
		public Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0000F21D File Offset: 0x0000D41D
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0000F225 File Offset: 0x0000D425
		public DateTime ControlTime
		{
			get
			{
				return this._controlTime;
			}
			set
			{
				this._controlTime = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0000F22E File Offset: 0x0000D42E
		public bool IsAlive
		{
			get
			{
				return this._client.IsAlive;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0000F23B File Offset: 0x0000D43B
		public byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000F243 File Offset: 0x0000D443
		public void Release()
		{
			this._pool.ReleaseConnection(this);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000F251 File Offset: 0x0000D451
		public void Close()
		{
			this._client.Close();
		}

		// Token: 0x040004D4 RID: 1236
		private DateTime _controlTime;

		// Token: 0x040004D5 RID: 1237
		private Stream _stream;

		// Token: 0x040004D6 RID: 1238
		private ReusableUnixClient _client;

		// Token: 0x040004D7 RID: 1239
		private HostConnectionPool _pool;

		// Token: 0x040004D8 RID: 1240
		private byte[] _buffer;
	}
}
