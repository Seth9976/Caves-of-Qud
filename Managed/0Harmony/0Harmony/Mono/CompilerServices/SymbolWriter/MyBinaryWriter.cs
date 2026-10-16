using System;
using System.IO;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x020002FC RID: 764
	internal sealed class MyBinaryWriter : BinaryWriter
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x0003371F File Offset: 0x0003191F
		public MyBinaryWriter(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0003D833 File Offset: 0x0003BA33
		public void WriteLeb128(int value)
		{
			base.Write7BitEncodedInt(value);
		}
	}
}
