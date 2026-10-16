using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200006B RID: 107
	internal sealed class MemberDefinitionCollection<T> : Collection<T> where T : IMemberDefinition
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00007660 File Offset: 0x00005860
		internal MemberDefinitionCollection(TypeDefinition container)
		{
			this.container = container;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000766F File Offset: 0x0000586F
		internal MemberDefinitionCollection(TypeDefinition container, int capacity)
			: base(capacity)
		{
			this.container = container;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000767F File Offset: 0x0000587F
		protected override void OnAdd(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000767F File Offset: 0x0000587F
		protected sealed override void OnSet(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000767F File Offset: 0x0000587F
		protected sealed override void OnInsert(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007688 File Offset: 0x00005888
		protected sealed override void OnRemove(T item, int index)
		{
			MemberDefinitionCollection<T>.Detach(item);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007690 File Offset: 0x00005890
		protected sealed override void OnClear()
		{
			foreach (T t in this)
			{
				MemberDefinitionCollection<T>.Detach(t);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000076DC File Offset: 0x000058DC
		private void Attach(T element)
		{
			if (element.DeclaringType != this.container)
			{
				if (element.DeclaringType != null)
				{
					throw new ArgumentException("Member already attached");
				}
				element.DeclaringType = this.container;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000772B File Offset: 0x0000592B
		private static void Detach(T element)
		{
			element.DeclaringType = null;
		}

		// Token: 0x040000AE RID: 174
		private TypeDefinition container;
	}
}
