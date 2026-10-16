using System;
using System.IO;
using System.Text;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200001D RID: 29
	internal class BitAccess
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003B95 File Offset: 0x00001D95
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00003B9D File Offset: 0x00001D9D
		internal int Position
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003BA6 File Offset: 0x00001DA6
		internal BitAccess(int capacity)
		{
			this.buffer = new byte[capacity];
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003BBA File Offset: 0x00001DBA
		internal void FillBuffer(Stream stream, int capacity)
		{
			this.MinCapacity(capacity);
			stream.Read(this.buffer, 0, capacity);
			this.offset = 0;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003BDC File Offset: 0x00001DDC
		internal void Append(Stream stream, int count)
		{
			int num = this.offset + count;
			if (this.buffer.Length < num)
			{
				byte[] array = new byte[num];
				Array.Copy(this.buffer, array, this.buffer.Length);
				this.buffer = array;
			}
			stream.Read(this.buffer, this.offset, count);
			this.offset += count;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00003C41 File Offset: 0x00001E41
		internal void MinCapacity(int capacity)
		{
			if (this.buffer.Length < capacity)
			{
				this.buffer = new byte[capacity];
			}
			this.offset = 0;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00003C61 File Offset: 0x00001E61
		internal void Align(int alignment)
		{
			while (this.offset % alignment != 0)
			{
				this.offset++;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003C7D File Offset: 0x00001E7D
		internal void ReadInt16(out short value)
		{
			value = (short)((int)(this.buffer[this.offset] & byte.MaxValue) | ((int)this.buffer[this.offset + 1] << 8));
			this.offset += 2;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003CB5 File Offset: 0x00001EB5
		internal void ReadInt8(out sbyte value)
		{
			value = (sbyte)this.buffer[this.offset];
			this.offset++;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003CD8 File Offset: 0x00001ED8
		internal void ReadInt32(out int value)
		{
			value = (int)(this.buffer[this.offset] & byte.MaxValue) | ((int)this.buffer[this.offset + 1] << 8) | ((int)this.buffer[this.offset + 2] << 16) | ((int)this.buffer[this.offset + 3] << 24);
			this.offset += 4;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003D40 File Offset: 0x00001F40
		internal void ReadInt64(out long value)
		{
			value = (long)(((ulong)this.buffer[this.offset] & 255UL) | ((ulong)this.buffer[this.offset + 1] << 8) | ((ulong)this.buffer[this.offset + 2] << 16) | ((ulong)this.buffer[this.offset + 3] << 24) | ((ulong)this.buffer[this.offset + 4] << 32) | ((ulong)this.buffer[this.offset + 5] << 40) | ((ulong)this.buffer[this.offset + 6] << 48) | ((ulong)this.buffer[this.offset + 7] << 56));
			this.offset += 8;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003DFD File Offset: 0x00001FFD
		internal void ReadUInt16(out ushort value)
		{
			value = (ushort)((int)(this.buffer[this.offset] & byte.MaxValue) | ((int)this.buffer[this.offset + 1] << 8));
			this.offset += 2;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003E35 File Offset: 0x00002035
		internal void ReadUInt8(out byte value)
		{
			value = this.buffer[this.offset] & byte.MaxValue;
			this.offset++;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003E5C File Offset: 0x0000205C
		internal void ReadUInt32(out uint value)
		{
			value = (uint)((int)(this.buffer[this.offset] & byte.MaxValue) | ((int)this.buffer[this.offset + 1] << 8) | ((int)this.buffer[this.offset + 2] << 16) | ((int)this.buffer[this.offset + 3] << 24));
			this.offset += 4;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003EC4 File Offset: 0x000020C4
		internal void ReadUInt64(out ulong value)
		{
			value = ((ulong)this.buffer[this.offset] & 255UL) | ((ulong)this.buffer[this.offset + 1] << 8) | ((ulong)this.buffer[this.offset + 2] << 16) | ((ulong)this.buffer[this.offset + 3] << 24) | ((ulong)this.buffer[this.offset + 4] << 32) | ((ulong)this.buffer[this.offset + 5] << 40) | ((ulong)this.buffer[this.offset + 6] << 48) | ((ulong)this.buffer[this.offset + 7] << 56);
			this.offset += 8;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003F84 File Offset: 0x00002184
		internal void ReadInt32(int[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				this.ReadInt32(out values[i]);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003FAC File Offset: 0x000021AC
		internal void ReadUInt32(uint[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				this.ReadUInt32(out values[i]);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003FD4 File Offset: 0x000021D4
		internal void ReadBytes(byte[] bytes)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				int num = i;
				byte[] array = this.buffer;
				int num2 = this.offset;
				this.offset = num2 + 1;
				bytes[num] = array[num2];
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000400C File Offset: 0x0000220C
		internal float ReadFloat()
		{
			float num = BitConverter.ToSingle(this.buffer, this.offset);
			this.offset += 4;
			return num;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000403C File Offset: 0x0000223C
		internal double ReadDouble()
		{
			double num = BitConverter.ToDouble(this.buffer, this.offset);
			this.offset += 8;
			return num;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000406C File Offset: 0x0000226C
		internal decimal ReadDecimal()
		{
			int[] array = new int[4];
			this.ReadInt32(array);
			return new decimal(array[2], array[3], array[1], array[0] < 0, (byte)((array[0] & 16711680) >> 16));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000040A8 File Offset: 0x000022A8
		internal void ReadBString(out string value)
		{
			ushort num;
			this.ReadUInt16(out num);
			value = Encoding.UTF8.GetString(this.buffer, this.offset, (int)num);
			this.offset += (int)num;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000040E4 File Offset: 0x000022E4
		internal string ReadBString(int len)
		{
			string @string = Encoding.UTF8.GetString(this.buffer, this.offset, len);
			this.offset += len;
			return @string;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004118 File Offset: 0x00002318
		internal void ReadCString(out string value)
		{
			int num = 0;
			while (this.offset + num < this.buffer.Length && this.buffer[this.offset + num] != 0)
			{
				num++;
			}
			value = Encoding.UTF8.GetString(this.buffer, this.offset, num);
			this.offset += num + 1;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000417C File Offset: 0x0000237C
		internal void SkipCString(out string value)
		{
			int num = 0;
			while (this.offset + num < this.buffer.Length && this.buffer[this.offset + num] != 0)
			{
				num++;
			}
			this.offset += num + 1;
			value = null;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000041C8 File Offset: 0x000023C8
		internal void ReadGuid(out Guid guid)
		{
			uint num;
			this.ReadUInt32(out num);
			ushort num2;
			this.ReadUInt16(out num2);
			ushort num3;
			this.ReadUInt16(out num3);
			byte b;
			this.ReadUInt8(out b);
			byte b2;
			this.ReadUInt8(out b2);
			byte b3;
			this.ReadUInt8(out b3);
			byte b4;
			this.ReadUInt8(out b4);
			byte b5;
			this.ReadUInt8(out b5);
			byte b6;
			this.ReadUInt8(out b6);
			byte b7;
			this.ReadUInt8(out b7);
			byte b8;
			this.ReadUInt8(out b8);
			guid = new Guid(num, num2, num3, b, b2, b3, b4, b5, b6, b7, b8);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000424C File Offset: 0x0000244C
		internal string ReadString()
		{
			int num = 0;
			while (this.offset + num < this.buffer.Length && this.buffer[this.offset + num] != 0)
			{
				num += 2;
			}
			string @string = Encoding.Unicode.GetString(this.buffer, this.offset, num);
			this.offset += num + 2;
			return @string;
		}

		// Token: 0x04000045 RID: 69
		private byte[] buffer;

		// Token: 0x04000046 RID: 70
		private int offset;
	}
}
