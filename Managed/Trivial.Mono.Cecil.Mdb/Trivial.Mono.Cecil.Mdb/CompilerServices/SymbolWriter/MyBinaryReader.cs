using System;
using System.IO;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000011 RID: 17
	internal class MyBinaryReader : BinaryReader
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00004B6C File Offset: 0x00002D6C
		public MyBinaryReader(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004B75 File Offset: 0x00002D75
		public int ReadLeb128()
		{
			return base.Read7BitEncodedInt();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004B80 File Offset: 0x00002D80
		public string ReadString(int offset)
		{
			long position = this.BaseStream.Position;
			this.BaseStream.Position = (long)offset;
			string text = this.ReadString();
			this.BaseStream.Position = position;
			return text;
		}
	}
}
