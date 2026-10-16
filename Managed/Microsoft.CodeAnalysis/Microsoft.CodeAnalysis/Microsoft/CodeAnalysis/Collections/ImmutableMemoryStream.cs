using System;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x0200049C RID: 1180
	internal sealed class ImmutableMemoryStream : Stream
	{
		// Token: 0x06002E9E RID: 11934 RVA: 0x000837A2 File Offset: 0x000819A2
		internal ImmutableMemoryStream(ImmutableArray<byte> array)
		{
			this._array = array;
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000837B1 File Offset: 0x000819B1
		public ImmutableArray<byte> GetBuffer()
		{
			return this._array;
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x000837B9 File Offset: 0x000819B9
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000837BC File Offset: 0x000819BC
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000837BF File Offset: 0x000819BF
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000837C4 File Offset: 0x000819C4
		public override long Length
		{
			get
			{
				return (long)this._array.Length;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x000837E0 File Offset: 0x000819E0
		// (set) Token: 0x06002EA5 RID: 11941 RVA: 0x000837EC File Offset: 0x000819EC
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				if (value < 0L || value >= (long)this._array.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._position = (int)value;
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x00083823 File Offset: 0x00081A23
		public override void Flush()
		{
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x00083828 File Offset: 0x00081A28
		[NullableContext(1)]
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._array.Length - this._position);
			this._array.CopyTo(this._position, buffer, offset, num);
			this._position += num;
			return num;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x00083878 File Offset: 0x00081A78
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			checked
			{
				try
				{
					switch (origin)
					{
					case 0:
						num = offset;
						break;
					case 1:
						num = offset + unchecked((long)this._position);
						break;
					case 2:
						num = offset + unchecked((long)this._array.Length);
						break;
					default:
						throw new ArgumentOutOfRangeException("origin");
					}
				}
				catch (OverflowException)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
			}
			if (num < 0L || num >= (long)this._array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this._position = (int)num;
			return num;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x00083914 File Offset: 0x00081B14
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x0008391B File Offset: 0x00081B1B
		[NullableContext(1)]
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001279 RID: 4729
		private readonly ImmutableArray<byte> _array;

		// Token: 0x0400127A RID: 4730
		private int _position;
	}
}
