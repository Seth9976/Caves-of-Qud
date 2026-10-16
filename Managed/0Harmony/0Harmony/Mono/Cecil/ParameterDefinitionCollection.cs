using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000253 RID: 595
	internal sealed class ParameterDefinitionCollection : Collection<ParameterDefinition>
	{
		// Token: 0x06000E4C RID: 3660 RVA: 0x0002D75C File Offset: 0x0002B95C
		internal ParameterDefinitionCollection(IMethodSignature method)
		{
			this.method = method;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002D76B File Offset: 0x0002B96B
		internal ParameterDefinitionCollection(IMethodSignature method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0002D77B File Offset: 0x0002B97B
		protected override void OnAdd(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0002D790 File Offset: 0x0002B990
		protected override void OnInsert(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
			for (int i = index; i < this.size; i++)
			{
				this.items[i].index = i + 1;
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0002D77B File Offset: 0x0002B97B
		protected override void OnSet(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		protected override void OnRemove(ParameterDefinition item, int index)
		{
			item.method = null;
			item.index = -1;
			for (int i = index + 1; i < this.size; i++)
			{
				this.items[i].index = i - 1;
			}
		}

		// Token: 0x04000494 RID: 1172
		private readonly IMethodSignature method;
	}
}
