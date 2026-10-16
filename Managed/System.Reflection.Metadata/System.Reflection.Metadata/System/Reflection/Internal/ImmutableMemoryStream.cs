using System;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000182 RID: 386
	internal sealed class ImmutableMemoryStream : Stream
	{
		// Token: 0x06000BBE RID: 3006 RVA: 0x000236C7 File Offset: 0x000218C7
		internal ImmutableMemoryStream(ImmutableArray<byte> array)
		{
			this._array = array;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000236D6 File Offset: 0x000218D6
		public ImmutableArray<byte> GetBuffer()
		{
			return this._array;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x000236DE File Offset: 0x000218DE
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x000236E1 File Offset: 0x000218E1
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x000236E4 File Offset: 0x000218E4
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x000236E8 File Offset: 0x000218E8
		public override long Length
		{
			get
			{
				return (long)this._array.Length;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00023704 File Offset: 0x00021904
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x00023710 File Offset: 0x00021910
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

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00023747 File Offset: 0x00021947
		public override void Flush()
		{
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002374C File Offset: 0x0002194C
		[NullableContext(1)]
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._array.Length - this._position);
			this._array.CopyTo(this._position, buffer, offset, num);
			this._position += num;
			return num;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002379C File Offset: 0x0002199C
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num2;
			checked
			{
				try
				{
					long num;
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
					num2 = num;
				}
				catch (OverflowException)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
			}
			if (num2 < 0L || num2 >= (long)this._array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this._position = (int)num2;
			return num2;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00023838 File Offset: 0x00021A38
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002383F File Offset: 0x00021A3F
		[NullableContext(1)]
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000965 RID: 2405
		private readonly ImmutableArray<byte> _array;

		// Token: 0x04000966 RID: 2406
		private int _position;
	}
}
