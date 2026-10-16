using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200022E RID: 558
	internal sealed class MemberDefinitionCollection<T> : Collection<T> where T : IMemberDefinition
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x000298E0 File Offset: 0x00027AE0
		internal MemberDefinitionCollection(TypeDefinition container)
		{
			this.container = container;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000298EF File Offset: 0x00027AEF
		internal MemberDefinitionCollection(TypeDefinition container, int capacity)
			: base(capacity)
		{
			this.container = container;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000298FF File Offset: 0x00027AFF
		protected override void OnAdd(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000298FF File Offset: 0x00027AFF
		protected sealed override void OnSet(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000298FF File Offset: 0x00027AFF
		protected sealed override void OnInsert(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00029908 File Offset: 0x00027B08
		protected sealed override void OnRemove(T item, int index)
		{
			MemberDefinitionCollection<T>.Detach(item);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00029910 File Offset: 0x00027B10
		protected sealed override void OnClear()
		{
			foreach (T t in this)
			{
				MemberDefinitionCollection<T>.Detach(t);
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002995C File Offset: 0x00027B5C
		private void Attach(T element)
		{
			if (element.DeclaringType == this.container)
			{
				return;
			}
			if (element.DeclaringType != null)
			{
				throw new ArgumentException("Member already attached");
			}
			element.DeclaringType = this.container;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000299AC File Offset: 0x00027BAC
		private static void Detach(T element)
		{
			element.DeclaringType = null;
		}

		// Token: 0x04000368 RID: 872
		private TypeDefinition container;
	}
}
