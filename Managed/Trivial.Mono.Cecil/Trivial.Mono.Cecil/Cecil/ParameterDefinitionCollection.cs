using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200009A RID: 154
	internal sealed class ParameterDefinitionCollection : Collection<ParameterDefinition>
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0001470D File Offset: 0x0001290D
		internal ParameterDefinitionCollection(IMethodSignature method)
		{
			this.method = method;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001471C File Offset: 0x0001291C
		internal ParameterDefinitionCollection(IMethodSignature method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001472C File Offset: 0x0001292C
		protected override void OnAdd(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00014744 File Offset: 0x00012944
		protected override void OnInsert(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
			for (int i = index; i < this.size; i++)
			{
				this.items[i].index = i + 1;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001472C File Offset: 0x0001292C
		protected override void OnSet(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00014788 File Offset: 0x00012988
		protected override void OnRemove(ParameterDefinition item, int index)
		{
			item.method = null;
			item.index = -1;
			for (int i = index + 1; i < this.size; i++)
			{
				this.items[i].index = i - 1;
			}
		}

		// Token: 0x04000216 RID: 534
		private readonly IMethodSignature method;
	}
}
