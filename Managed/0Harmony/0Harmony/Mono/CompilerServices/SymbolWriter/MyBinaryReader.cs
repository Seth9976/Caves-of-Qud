using System;
using System.IO;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x020002FD RID: 765
	internal class MyBinaryReader : BinaryReader
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x0003369A File Offset: 0x0003189A
		public MyBinaryReader(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0003D83C File Offset: 0x0003BA3C
		public int ReadLeb128()
		{
			return base.Read7BitEncodedInt();
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0003D844 File Offset: 0x0003BA44
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
