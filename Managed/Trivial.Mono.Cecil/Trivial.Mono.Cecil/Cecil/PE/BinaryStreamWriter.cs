using System;
using System.IO;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000CF RID: 207
	internal class BinaryStreamWriter : BinaryWriter
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001C6A2 File Offset: 0x0001A8A2
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
		public int Position
		{
			get
			{
				return (int)this.BaseStream.Position;
			}
			set
			{
				this.BaseStream.Position = (long)value;
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001C6BF File Offset: 0x0001A8BF
		public BinaryStreamWriter(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		public void WriteByte(byte value)
		{
			this.Write(value);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001C6D1 File Offset: 0x0001A8D1
		public void WriteUInt16(ushort value)
		{
			this.Write(value);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001C6DA File Offset: 0x0001A8DA
		public void WriteInt16(short value)
		{
			this.Write(value);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001C6E3 File Offset: 0x0001A8E3
		public void WriteUInt32(uint value)
		{
			this.Write(value);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001C6EC File Offset: 0x0001A8EC
		public void WriteInt32(int value)
		{
			this.Write(value);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001C6F5 File Offset: 0x0001A8F5
		public void WriteUInt64(ulong value)
		{
			this.Write(value);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001C6FE File Offset: 0x0001A8FE
		public void WriteBytes(byte[] bytes)
		{
			this.Write(bytes);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001C707 File Offset: 0x0001A907
		public void WriteDataDirectory(DataDirectory directory)
		{
			this.Write(directory.VirtualAddress);
			this.Write(directory.Size);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001C721 File Offset: 0x0001A921
		public void WriteBuffer(ByteBuffer buffer)
		{
			this.Write(buffer.buffer, 0, buffer.length);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001C736 File Offset: 0x0001A936
		protected void Advance(int bytes)
		{
			this.BaseStream.Seek((long)bytes, 1);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001C748 File Offset: 0x0001A948
		public void Align(int align)
		{
			align--;
			int position = this.Position;
			int num = ((position + align) & ~align) - position;
			for (int i = 0; i < num; i++)
			{
				this.WriteByte(0);
			}
		}
	}
}
