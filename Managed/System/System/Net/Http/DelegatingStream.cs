using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000817 RID: 2071
	internal abstract class DelegatingStream : Stream
	{
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x000E4C7F File Offset: 0x000E2E7F
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x000E4C8C File Offset: 0x000E2E8C
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x000E4C99 File Offset: 0x000E2E99
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x000E4CA6 File Offset: 0x000E2EA6
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x000E4CB3 File Offset: 0x000E2EB3
		// (set) Token: 0x0600423D RID: 16957 RVA: 0x000E4CC0 File Offset: 0x000E2EC0
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x000E4CCE File Offset: 0x000E2ECE
		// (set) Token: 0x0600423F RID: 16959 RVA: 0x000E4CDB File Offset: 0x000E2EDB
		public override int ReadTimeout
		{
			get
			{
				return this._innerStream.ReadTimeout;
			}
			set
			{
				this._innerStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x000E4CE9 File Offset: 0x000E2EE9
		public override bool CanTimeout
		{
			get
			{
				return this._innerStream.CanTimeout;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x000E4CF6 File Offset: 0x000E2EF6
		// (set) Token: 0x06004242 RID: 16962 RVA: 0x000E4D03 File Offset: 0x000E2F03
		public override int WriteTimeout
		{
			get
			{
				return this._innerStream.WriteTimeout;
			}
			set
			{
				this._innerStream.WriteTimeout = value;
			}
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000E4D11 File Offset: 0x000E2F11
		protected DelegatingStream(Stream innerStream)
		{
			this._innerStream = innerStream;
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000E4D20 File Offset: 0x000E2F20
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._innerStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000E4D37 File Offset: 0x000E2F37
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x000E4D46 File Offset: 0x000E2F46
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000E4D56 File Offset: 0x000E2F56
		public override int Read(Span<byte> buffer)
		{
			return this._innerStream.Read(buffer);
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x000E4D64 File Offset: 0x000E2F64
		public override int ReadByte()
		{
			return this._innerStream.ReadByte();
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x000E4D71 File Offset: 0x000E2F71
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x000E4D83 File Offset: 0x000E2F83
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._innerStream.ReadAsync(buffer, cancellationToken);
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x000E4D92 File Offset: 0x000E2F92
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x000E4DA6 File Offset: 0x000E2FA6
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._innerStream.EndRead(asyncResult);
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x000E4DB4 File Offset: 0x000E2FB4
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000E4DC1 File Offset: 0x000E2FC1
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._innerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x000E4DCF File Offset: 0x000E2FCF
		public override void SetLength(long value)
		{
			this._innerStream.SetLength(value);
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x000E4DDD File Offset: 0x000E2FDD
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x000E4DED File Offset: 0x000E2FED
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this._innerStream.Write(buffer);
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x000E4DFB File Offset: 0x000E2FFB
		public override void WriteByte(byte value)
		{
			this._innerStream.WriteByte(value);
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x000E4E09 File Offset: 0x000E3009
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._innerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x000E4E1B File Offset: 0x000E301B
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._innerStream.WriteAsync(buffer, cancellationToken);
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x000E4E2A File Offset: 0x000E302A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000E4E3E File Offset: 0x000E303E
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._innerStream.EndWrite(asyncResult);
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000E4E4C File Offset: 0x000E304C
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			return this._innerStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x040027C1 RID: 10177
		private readonly Stream _innerStream;
	}
}
