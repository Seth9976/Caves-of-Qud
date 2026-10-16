using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000075 RID: 117
	internal abstract class MetadataTable
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000475 RID: 1141
		public abstract int Length { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000F9E9 File Offset: 0x0000DBE9
		public bool IsLarge
		{
			get
			{
				return this.Length > 65535;
			}
		}

		// Token: 0x06000477 RID: 1143
		public abstract void Write(TableHeapBuffer buffer);

		// Token: 0x06000478 RID: 1144
		public abstract void Sort();
	}
}
