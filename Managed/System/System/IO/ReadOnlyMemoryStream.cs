using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020004F9 RID: 1273
	internal sealed class ReadOnlyMemoryStream : Stream
	{
		// Token: 0x06002991 RID: 10641 RVA: 0x0008EE95 File Offset: 0x0008D095
		public ReadOnlyMemoryStream(ReadOnlyMemory<byte> content)
		{
			this._content = content;
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002993 RID: 10643 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002995 RID: 10645 RVA: 0x0008EEA4 File Offset: 0x0008D0A4
		public override long Length
		{
			get
			{
				return (long)this._content.Length;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x0008EEB2 File Offset: 0x0008D0B2
		// (set) Token: 0x06002997 RID: 10647 RVA: 0x0008EEBB File Offset: 0x0008D0BB
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				if (value < 0L || value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._position = (int)value;
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0008EEE0 File Offset: 0x0008D0E0
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			if (origin != SeekOrigin.Begin)
			{
				if (origin != SeekOrigin.Current)
				{
					if (origin != SeekOrigin.End)
					{
						throw new ArgumentOutOfRangeException("origin");
					}
					num = (long)this._content.Length + offset;
				}
				else
				{
					num = (long)this._position + offset;
				}
			}
			else
			{
				num = offset;
			}
			long num2 = num;
			if (num2 > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (num2 < 0L)
			{
				throw new IOException("An attempt was made to move the position before the beginning of the stream.");
			}
			this._position = (int)num2;
			return (long)this._position;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x0008EF54 File Offset: 0x0008D154
		public unsafe override int ReadByte()
		{
			ReadOnlySpan<byte> span = this._content.Span;
			if (this._position >= span.Length)
			{
				return -1;
			}
			int position = this._position;
			this._position = position + 1;
			return (int)(*span[position]);
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x0008EF97 File Offset: 0x0008D197
		public override int Read(byte[] buffer, int offset, int count)
		{
			ReadOnlyMemoryStream.ValidateReadArrayArguments(buffer, offset, count);
			return this.Read(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x0008EFB0 File Offset: 0x0008D1B0
		public override int Read(Span<byte> buffer)
		{
			int num = this._content.Length - this._position;
			if (num <= 0 || buffer.Length == 0)
			{
				return 0;
			}
			if (num <= buffer.Length)
			{
				this._content.Span.Slice(this._position).CopyTo(buffer);
				this._position = this._content.Length;
				return num;
			}
			this._content.Span.Slice(this._position, buffer.Length).CopyTo(buffer);
			this._position += buffer.Length;
			return buffer.Length;
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x0008F062 File Offset: 0x0008D262
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			ReadOnlyMemoryStream.ValidateReadArrayArguments(buffer, offset, count);
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.FromResult<int>(this.Read(new Span<byte>(buffer, offset, count)));
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x0008F090 File Offset: 0x0008D290
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(this.Read(buffer.Span));
			}
			return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x0008F0B9 File Offset: 0x0008D2B9
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(base.ReadAsync(buffer, offset, count), callback, state);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0008F0CD File Offset: 0x0008D2CD
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x0008F0D8 File Offset: 0x0008D2D8
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (this._content.Length > this._position)
			{
				destination.Write(this._content.Span.Slice(this._position));
			}
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x0008F120 File Offset: 0x0008D320
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (this._content.Length <= this._position)
			{
				return Task.CompletedTask;
			}
			return destination.WriteAsync(this._content.Slice(this._position), cancellationToken).AsTask();
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x0008EAEB File Offset: 0x0008CCEB
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000044FA File Offset: 0x000026FA
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x0008F16E File Offset: 0x0008D36E
		private static void ValidateReadArrayArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || buffer.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x04001618 RID: 5656
		private readonly ReadOnlyMemory<byte> _content;

		// Token: 0x04001619 RID: 5657
		private int _position;
	}
}
