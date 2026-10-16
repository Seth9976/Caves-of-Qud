using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000271 RID: 625
	internal class InterfaceImplementationCollection : Collection<InterfaceImplementation>
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x0002F13D File Offset: 0x0002D33D
		internal InterfaceImplementationCollection(TypeDefinition type)
		{
			this.type = type;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002F14C File Offset: 0x0002D34C
		internal InterfaceImplementationCollection(TypeDefinition type, int length)
			: base(length)
		{
			this.type = type;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002F15C File Offset: 0x0002D35C
		protected override void OnAdd(InterfaceImplementation item, int index)
		{
			item.type = this.type;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002F15C File Offset: 0x0002D35C
		protected override void OnInsert(InterfaceImplementation item, int index)
		{
			item.type = this.type;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002F15C File Offset: 0x0002D35C
		protected override void OnSet(InterfaceImplementation item, int index)
		{
			item.type = this.type;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002F16A File Offset: 0x0002D36A
		protected override void OnRemove(InterfaceImplementation item, int index)
		{
			item.type = null;
		}

		// Token: 0x04000549 RID: 1353
		private readonly TypeDefinition type;
	}
}
