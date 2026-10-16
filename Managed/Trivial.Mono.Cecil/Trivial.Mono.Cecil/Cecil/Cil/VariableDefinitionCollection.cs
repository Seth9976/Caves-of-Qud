using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000134 RID: 308
	internal sealed class VariableDefinitionCollection : Collection<VariableDefinition>
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x00025C5C File Offset: 0x00023E5C
		internal VariableDefinitionCollection()
		{
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00025C64 File Offset: 0x00023E64
		internal VariableDefinitionCollection(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00025C6D File Offset: 0x00023E6D
		protected override void OnAdd(VariableDefinition item, int index)
		{
			item.index = index;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00025C78 File Offset: 0x00023E78
		protected override void OnInsert(VariableDefinition item, int index)
		{
			item.index = index;
			for (int i = index; i < this.size; i++)
			{
				this.items[i].index = i + 1;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00025C6D File Offset: 0x00023E6D
		protected override void OnSet(VariableDefinition item, int index)
		{
			item.index = index;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00025CB0 File Offset: 0x00023EB0
		protected override void OnRemove(VariableDefinition item, int index)
		{
			item.index = -1;
			for (int i = index + 1; i < this.size; i++)
			{
				this.items[i].index = i - 1;
			}
		}
	}
}
