using System;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000D0 RID: 208
	internal class ByteBuffer
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0001C77D File Offset: 0x0001A97D
		public ByteBuffer()
		{
			this.buffer = Empty<byte>.Array;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001C790 File Offset: 0x0001A990
		public ByteBuffer(int length)
		{
			this.buffer = new byte[length];
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		public ByteBuffer(byte[] buffer)
		{
			this.buffer = buffer ?? Empty<byte>.Array;
			this.length = this.buffer.Length;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001C7CA File Offset: 0x0001A9CA
		public void Advance(int length)
		{
			this.position += length;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001C7DC File Offset: 0x0001A9DC
		public byte ReadByte()
		{
			byte[] array = this.buffer;
			int num = this.position;
			this.position = num + 1;
			return array[num];
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001C801 File Offset: 0x0001AA01
		public sbyte ReadSByte()
		{
			return (sbyte)this.ReadByte();
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001C80C File Offset: 0x0001AA0C
		public byte[] ReadBytes(int length)
		{
			byte[] array = new byte[length];
			Buffer.BlockCopy(this.buffer, this.position, array, 0, length);
			this.position += length;
			return array;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001C844 File Offset: 0x0001AA44
		public ushort ReadUInt16()
		{
			ushort num = (ushort)((int)this.buffer[this.position] | ((int)this.buffer[this.position + 1] << 8));
			this.position += 2;
			return num;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001C881 File Offset: 0x0001AA81
		public short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001C88C File Offset: 0x0001AA8C
		public uint ReadUInt32()
		{
			int num = (int)this.buffer[this.position] | ((int)this.buffer[this.position + 1] << 8) | ((int)this.buffer[this.position + 2] << 16) | ((int)this.buffer[this.position + 3] << 24);
			this.position += 4;
			return (uint)num;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001C8EE File Offset: 0x0001AAEE
		public int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001C8F8 File Offset: 0x0001AAF8
		public ulong ReadUInt64()
		{
			uint num = this.ReadUInt32();
			return ((ulong)this.ReadUInt32() << 32) | (ulong)num;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001C919 File Offset: 0x0001AB19
		public long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001C924 File Offset: 0x0001AB24
		public uint ReadCompressedUInt32()
		{
			byte b = this.ReadByte();
			if ((b & 128) == 0)
			{
				return (uint)b;
			}
			if ((b & 64) == 0)
			{
				return (((uint)b & 4294967167U) << 8) | (uint)this.ReadByte();
			}
			return (uint)((((int)b & -193) << 24) | ((int)this.ReadByte() << 16) | ((int)this.ReadByte() << 8) | (int)this.ReadByte());
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001C980 File Offset: 0x0001AB80
		public int ReadCompressedInt32()
		{
			byte b = this.buffer[this.position];
			uint num = this.ReadCompressedUInt32();
			int num2 = (int)num >> 1;
			if ((num & 1U) == 0U)
			{
				return num2;
			}
			int num3 = (int)(b & 192);
			if (num3 == 0 || num3 == 64)
			{
				return num2 - 64;
			}
			if (num3 != 128)
			{
				return num2 - 268435456;
			}
			return num2 - 8192;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		public float ReadSingle()
		{
			if (!BitConverter.IsLittleEndian)
			{
				byte[] array = this.ReadBytes(4);
				Array.Reverse(array);
				return BitConverter.ToSingle(array, 0);
			}
			float num = BitConverter.ToSingle(this.buffer, this.position);
			this.position += 4;
			return num;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001CA28 File Offset: 0x0001AC28
		public double ReadDouble()
		{
			if (!BitConverter.IsLittleEndian)
			{
				byte[] array = this.ReadBytes(8);
				Array.Reverse(array);
				return BitConverter.ToDouble(array, 0);
			}
			double num = BitConverter.ToDouble(this.buffer, this.position);
			this.position += 8;
			return num;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001CA74 File Offset: 0x0001AC74
		public void WriteByte(byte value)
		{
			if (this.position == this.buffer.Length)
			{
				this.Grow(1);
			}
			byte[] array = this.buffer;
			int num = this.position;
			this.position = num + 1;
			array[num] = value;
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000178C3 File Offset: 0x00015AC3
		public void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001CACC File Offset: 0x0001ACCC
		public void WriteUInt16(ushort value)
		{
			if (this.position + 2 > this.buffer.Length)
			{
				this.Grow(2);
			}
			byte[] array = this.buffer;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array2[num] = (byte)(value >> 8);
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001CB42 File Offset: 0x0001AD42
		public void WriteInt16(short value)
		{
			this.WriteUInt16((ushort)value);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		public void WriteUInt32(uint value)
		{
			if (this.position + 4 > this.buffer.Length)
			{
				this.Grow(4);
			}
			byte[] array = this.buffer;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array4[num] = (byte)(value >> 24);
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		public void WriteInt32(int value)
		{
			this.WriteUInt32((uint)value);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001CC08 File Offset: 0x0001AE08
		public void WriteUInt64(ulong value)
		{
			if (this.position + 8 > this.buffer.Length)
			{
				this.Grow(8);
			}
			byte[] array = this.buffer;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array4[num] = (byte)(value >> 24);
			byte[] array5 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array5[num] = (byte)(value >> 32);
			byte[] array6 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array6[num] = (byte)(value >> 40);
			byte[] array7 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array7[num] = (byte)(value >> 48);
			byte[] array8 = this.buffer;
			num = this.position;
			this.position = num + 1;
			array8[num] = (byte)(value >> 56);
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001CD2C File Offset: 0x0001AF2C
		public void WriteInt64(long value)
		{
			this.WriteUInt64((ulong)value);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001CD38 File Offset: 0x0001AF38
		public void WriteCompressedUInt32(uint value)
		{
			if (value < 128U)
			{
				this.WriteByte((byte)value);
				return;
			}
			if (value < 16384U)
			{
				this.WriteByte((byte)(128U | (value >> 8)));
				this.WriteByte((byte)(value & 255U));
				return;
			}
			this.WriteByte((byte)((value >> 24) | 192U));
			this.WriteByte((byte)((value >> 16) & 255U));
			this.WriteByte((byte)((value >> 8) & 255U));
			this.WriteByte((byte)(value & 255U));
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		public void WriteCompressedInt32(int value)
		{
			if (value >= 0)
			{
				this.WriteCompressedUInt32((uint)((uint)value << 1));
				return;
			}
			if (value > -64)
			{
				value = 64 + value;
			}
			else if (value >= -8192)
			{
				value = 8192 + value;
			}
			else if (value >= -536870912)
			{
				value = 536870912 + value;
			}
			this.WriteCompressedUInt32((uint)((value << 1) | 1));
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001CE18 File Offset: 0x0001B018
		public void WriteBytes(byte[] bytes)
		{
			int num = bytes.Length;
			if (this.position + num > this.buffer.Length)
			{
				this.Grow(num);
			}
			Buffer.BlockCopy(bytes, 0, this.buffer, this.position, num);
			this.position += num;
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001CE80 File Offset: 0x0001B080
		public void WriteBytes(int length)
		{
			if (this.position + length > this.buffer.Length)
			{
				this.Grow(length);
			}
			this.position += length;
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001CED0 File Offset: 0x0001B0D0
		public void WriteBytes(ByteBuffer buffer)
		{
			if (this.position + buffer.length > this.buffer.Length)
			{
				this.Grow(buffer.length);
			}
			Buffer.BlockCopy(buffer.buffer, 0, this.buffer, this.position, buffer.length);
			this.position += buffer.length;
			if (this.position > this.length)
			{
				this.length = this.position;
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001CF4C File Offset: 0x0001B14C
		public void WriteSingle(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			this.WriteBytes(bytes);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001CF74 File Offset: 0x0001B174
		public void WriteDouble(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			this.WriteBytes(bytes);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001CF9C File Offset: 0x0001B19C
		private void Grow(int desired)
		{
			byte[] array = this.buffer;
			int num = array.Length;
			byte[] array2 = new byte[Math.Max(num + desired, num * 2)];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			this.buffer = array2;
		}

		// Token: 0x0400032F RID: 815
		internal byte[] buffer;

		// Token: 0x04000330 RID: 816
		internal int length;

		// Token: 0x04000331 RID: 817
		internal int position;
	}
}
