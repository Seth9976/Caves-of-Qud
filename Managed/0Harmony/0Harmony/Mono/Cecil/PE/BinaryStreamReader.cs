using System;
using System.IO;

namespace Mono.Cecil.PE
{
	// Token: 0x02000289 RID: 649
	internal class BinaryStreamReader : BinaryReader
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003366F File Offset: 0x0003186F
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x0003367D File Offset: 0x0003187D
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0003368C File Offset: 0x0003188C
		public int Length
		{
			get
			{
				return (int)this.BaseStream.Length;
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003369A File Offset: 0x0003189A
		public BinaryStreamReader(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000336A3 File Offset: 0x000318A3
		public void Advance(int bytes)
		{
			this.BaseStream.Seek((long)bytes, SeekOrigin.Current);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000336B4 File Offset: 0x000318B4
		public void MoveTo(uint position)
		{
			this.BaseStream.Seek((long)((ulong)position), SeekOrigin.Begin);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000336C8 File Offset: 0x000318C8
		public void Align(int align)
		{
			align--;
			int position = this.Position;
			this.Advance(((position + align) & ~align) - position);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000336EF File Offset: 0x000318EF
		public DataDirectory ReadDataDirectory()
		{
			return new DataDirectory(this.ReadUInt32(), this.ReadUInt32());
		}
	}
}
