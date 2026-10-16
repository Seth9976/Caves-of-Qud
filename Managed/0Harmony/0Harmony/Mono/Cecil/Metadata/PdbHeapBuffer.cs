using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200029F RID: 671
	internal sealed class PdbHeapBuffer : HeapBuffer
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00018318 File Offset: 0x00016518
		public override bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003726B File Offset: 0x0003546B
		public PdbHeapBuffer()
			: base(0)
		{
		}
	}
}
