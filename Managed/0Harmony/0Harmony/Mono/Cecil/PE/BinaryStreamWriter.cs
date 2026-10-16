using System;
using System.IO;

namespace Mono.Cecil.PE
{
	// Token: 0x0200028A RID: 650
	internal class BinaryStreamWriter : BinaryWriter
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00033702 File Offset: 0x00031902
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x00033710 File Offset: 0x00031910
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

		// Token: 0x06001047 RID: 4167 RVA: 0x0003371F File Offset: 0x0003191F
		public BinaryStreamWriter(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00033728 File Offset: 0x00031928
		public void WriteByte(byte value)
		{
			this.Write(value);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00033731 File Offset: 0x00031931
		public void WriteUInt16(ushort value)
		{
			this.Write(value);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0003373A File Offset: 0x0003193A
		public void WriteInt16(short value)
		{
			this.Write(value);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00033743 File Offset: 0x00031943
		public void WriteUInt32(uint value)
		{
			this.Write(value);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003374C File Offset: 0x0003194C
		public void WriteInt32(int value)
		{
			this.Write(value);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00033755 File Offset: 0x00031955
		public void WriteUInt64(ulong value)
		{
			this.Write(value);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003375E File Offset: 0x0003195E
		public void WriteBytes(byte[] bytes)
		{
			this.Write(bytes);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00033767 File Offset: 0x00031967
		public void WriteDataDirectory(DataDirectory directory)
		{
			this.Write(directory.VirtualAddress);
			this.Write(directory.Size);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00033781 File Offset: 0x00031981
		public void WriteBuffer(ByteBuffer buffer)
		{
			this.Write(buffer.buffer, 0, buffer.length);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00033796 File Offset: 0x00031996
		protected void Advance(int bytes)
		{
			this.BaseStream.Seek((long)bytes, SeekOrigin.Current);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000337A8 File Offset: 0x000319A8
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
