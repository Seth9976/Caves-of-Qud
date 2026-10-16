using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000188 RID: 392
	internal sealed class ReadOnlyUnmanagedMemoryStream : Stream
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x000245F3 File Offset: 0x000227F3
		public unsafe ReadOnlyUnmanagedMemoryStream(byte* data, int length)
		{
			this._data = data;
			this._length = length;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002460C File Offset: 0x0002280C
		public unsafe override int ReadByte()
		{
			if (this._position >= this._length)
			{
				return -1;
			}
			int data = this._data;
			int position = this._position;
			this._position = position + 1;
			return (int)(*(data + position));
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00024644 File Offset: 0x00022844
		[NullableContext(1)]
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._length - this._position);
			Marshal.Copy((IntPtr)((void*)(this._data + this._position)), buffer, offset, num);
			this._position += num;
			return num;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002468E File Offset: 0x0002288E
		public override void Flush()
		{
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00024690 File Offset: 0x00022890
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00024693 File Offset: 0x00022893
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00024696 File Offset: 0x00022896
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00024699 File Offset: 0x00022899
		public override long Length
		{
			get
			{
				return (long)this._length;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x000246A2 File Offset: 0x000228A2
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x000246AB File Offset: 0x000228AB
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

		// Token: 0x06000C11 RID: 3089 RVA: 0x000246B8 File Offset: 0x000228B8
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
						num = offset + unchecked((long)this._length);
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
				if (num2 < 0L || num2 > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
			}
			this._position = (int)num2;
			return num2;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00024744 File Offset: 0x00022944
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002474B File Offset: 0x0002294B
		[NullableContext(1)]
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400096E RID: 2414
		private unsafe readonly byte* _data;

		// Token: 0x0400096F RID: 2415
		private readonly int _length;

		// Token: 0x04000970 RID: 2416
		private int _position;
	}
}
