using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct PEBinaryReader
	{
		// Token: 0x06000195 RID: 405 RVA: 0x000053A9 File Offset: 0x000035A9
		public PEBinaryReader(Stream stream, int size)
		{
			this._startOffset = stream.Position;
			this._maxOffset = this._startOffset + (long)size;
			this._reader = new BinaryReader(stream, Encoding.UTF8, true);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000053D8 File Offset: 0x000035D8
		public int CurrentOffset
		{
			get
			{
				return (int)(this._reader.BaseStream.Position - this._startOffset);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000053F2 File Offset: 0x000035F2
		public void Seek(int offset)
		{
			this.CheckBounds(this._startOffset, offset);
			this._reader.BaseStream.Seek((long)offset, 0);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005415 File Offset: 0x00003615
		public byte[] ReadBytes(int count)
		{
			this.CheckBounds(this._reader.BaseStream.Position, count);
			return this._reader.ReadBytes(count);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000543A File Offset: 0x0000363A
		public byte ReadByte()
		{
			this.CheckBounds(1U);
			return this._reader.ReadByte();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000544E File Offset: 0x0000364E
		public short ReadInt16()
		{
			this.CheckBounds(2U);
			return this._reader.ReadInt16();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005462 File Offset: 0x00003662
		public ushort ReadUInt16()
		{
			this.CheckBounds(2U);
			return this._reader.ReadUInt16();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005476 File Offset: 0x00003676
		public int ReadInt32()
		{
			this.CheckBounds(4U);
			return this._reader.ReadInt32();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000548A File Offset: 0x0000368A
		public uint ReadUInt32()
		{
			this.CheckBounds(4U);
			return this._reader.ReadUInt32();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000549E File Offset: 0x0000369E
		public ulong ReadUInt64()
		{
			this.CheckBounds(8U);
			return this._reader.ReadUInt64();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000054B4 File Offset: 0x000036B4
		public string ReadNullPaddedUTF8(int byteCount)
		{
			byte[] array = this.ReadBytes(byteCount);
			int num = 0;
			for (int i = array.Length; i > 0; i--)
			{
				if (array[i - 1] != 0)
				{
					num = i;
					break;
				}
			}
			return Encoding.UTF8.GetString(array, 0, num);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000054F1 File Offset: 0x000036F1
		private void CheckBounds(uint count)
		{
			if (this._reader.BaseStream.Position + (long)((ulong)count) > this._maxOffset)
			{
				Throw.ImageTooSmall();
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005513 File Offset: 0x00003713
		private void CheckBounds(long startPosition, int count)
		{
			if (startPosition + (long)((ulong)count) > this._maxOffset)
			{
				Throw.ImageTooSmallOrContainsInvalidOffsetOrCount();
			}
		}

		// Token: 0x040000DF RID: 223
		private readonly long _startOffset;

		// Token: 0x040000E0 RID: 224
		private readonly long _maxOffset;

		// Token: 0x040000E1 RID: 225
		private readonly BinaryReader _reader;
	}
}
