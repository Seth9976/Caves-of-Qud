using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002EE RID: 750
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class SourceTextWriter : TextWriter
	{
		// Token: 0x06002027 RID: 8231
		public abstract SourceText ToSourceText();

		// Token: 0x06002028 RID: 8232 RVA: 0x0006372D File Offset: 0x0006192D
		public static SourceTextWriter Create([Nullable(2)] Encoding encoding, SourceHashAlgorithm checksumAlgorithm, int length)
		{
			if (length < 40960)
			{
				return new StringTextWriter(encoding, checksumAlgorithm, length);
			}
			return new LargeTextWriter(encoding, checksumAlgorithm, length);
		}
	}
}
