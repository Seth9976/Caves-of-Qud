using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200005F RID: 95
	internal class InterfaceImplementationCollection : Collection<InterfaceImplementation>
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x00007393 File Offset: 0x00005593
		internal InterfaceImplementationCollection(TypeDefinition type)
		{
			this.type = type;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000073A2 File Offset: 0x000055A2
		internal InterfaceImplementationCollection(TypeDefinition type, int length)
			: base(length)
		{
			this.type = type;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000073B2 File Offset: 0x000055B2
		protected override void OnAdd(InterfaceImplementation item, int index)
		{
			item.type = this.type;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000073B2 File Offset: 0x000055B2
		protected override void OnInsert(InterfaceImplementation item, int index)
		{
			item.type = this.type;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000073B2 File Offset: 0x000055B2
		protected override void OnSet(InterfaceImplementation item, int index)
		{
			item.type = this.type;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000073C0 File Offset: 0x000055C0
		protected override void OnRemove(InterfaceImplementation item, int index)
		{
			item.type = null;
		}

		// Token: 0x040000A6 RID: 166
		private readonly TypeDefinition type;
	}
}
