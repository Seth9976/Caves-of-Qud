using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000195 RID: 405
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ReadOnlyUnmanagedMemoryStream : Stream
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x00030026 File Offset: 0x0002E226
		public ReadOnlyUnmanagedMemoryStream(object memoryOwner, IntPtr data, int length)
		{
			this._memoryOwner = memoryOwner;
			this._data = data;
			this._length = length;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00030044 File Offset: 0x0002E244
		public unsafe override int ReadByte()
		{
			if (this._position == this._length)
			{
				return -1;
			}
			int num = (void*)this._data;
			int position = this._position;
			this._position = position + 1;
			return (int)(*(num + position));
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00030080 File Offset: 0x0002E280
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._length - this._position);
			Marshal.Copy(this._data + this._position, buffer, offset, num);
			this._position += num;
			return num;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000300C9 File Offset: 0x0002E2C9
		public override void Flush()
		{
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000300CB File Offset: 0x0002E2CB
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000300CE File Offset: 0x0002E2CE
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000300D1 File Offset: 0x0002E2D1
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000300D4 File Offset: 0x0002E2D4
		public override long Length
		{
			get
			{
				return (long)this._length;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000300DD File Offset: 0x0002E2DD
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x000300E6 File Offset: 0x0002E2E6
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				this.Seek(value, 0);
			}
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000300F4 File Offset: 0x0002E2F4
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
						num = offset + unchecked((long)this._length);
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
			if (num < 0L || num >= (long)this._length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this._position = (int)num;
			return num;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00030180 File Offset: 0x0002E380
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00030187 File Offset: 0x0002E387
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000507 RID: 1287
		private readonly object _memoryOwner;

		// Token: 0x04000508 RID: 1288
		private readonly IntPtr _data;

		// Token: 0x04000509 RID: 1289
		private readonly int _length;

		// Token: 0x0400050A RID: 1290
		private int _position;
	}
}
