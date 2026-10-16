using System;
using System.IO;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000012 RID: 18
	internal sealed class MyBinaryWriter : BinaryWriter
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00004BBA File Offset: 0x00002DBA
		public MyBinaryWriter(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004BC3 File Offset: 0x00002DC3
		public void WriteLeb128(int value)
		{
			base.Write7BitEncodedInt(value);
		}
	}
}
