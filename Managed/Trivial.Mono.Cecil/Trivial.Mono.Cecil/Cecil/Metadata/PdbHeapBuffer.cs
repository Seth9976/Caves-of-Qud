using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E3 RID: 227
	internal sealed class PdbHeapBuffer : HeapBuffer
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000025EF File Offset: 0x000007EF
		public override bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		public PdbHeapBuffer()
			: base(0)
		{
		}
	}
}
