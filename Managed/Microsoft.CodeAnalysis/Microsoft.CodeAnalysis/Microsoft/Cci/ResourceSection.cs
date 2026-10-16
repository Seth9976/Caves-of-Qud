using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000B7 RID: 183
	[NullableContext(1)]
	[Nullable(0)]
	internal class ResourceSection
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x0001602E File Offset: 0x0001422E
		internal ResourceSection(byte[] sectionBytes, uint[] relocations)
		{
			this.SectionBytes = sectionBytes;
			this.Relocations = relocations;
		}

		// Token: 0x040001EA RID: 490
		internal readonly byte[] SectionBytes;

		// Token: 0x040001EB RID: 491
		internal readonly uint[] Relocations;
	}
}
