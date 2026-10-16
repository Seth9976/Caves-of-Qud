using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x020001C1 RID: 449
	internal abstract class MetadataTable
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008F8 RID: 2296
		public abstract int Length { get; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00020C5E File Offset: 0x0001EE5E
		public bool IsLarge
		{
			get
			{
				return this.Length > 65535;
			}
		}

		// Token: 0x060008FA RID: 2298
		public abstract void Write(TableHeapBuffer buffer);

		// Token: 0x060008FB RID: 2299
		public abstract void Sort();
	}
}
