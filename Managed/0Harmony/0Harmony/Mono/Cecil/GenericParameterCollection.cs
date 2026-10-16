using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200020D RID: 525
	internal sealed class GenericParameterCollection : Collection<GenericParameter>
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x00027FDC File Offset: 0x000261DC
		internal GenericParameterCollection(IGenericParameterProvider owner)
		{
			this.owner = owner;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00027FEB File Offset: 0x000261EB
		internal GenericParameterCollection(IGenericParameterProvider owner, int capacity)
			: base(capacity)
		{
			this.owner = owner;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00027FFB File Offset: 0x000261FB
		protected override void OnAdd(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00028008 File Offset: 0x00026208
		protected override void OnInsert(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
			for (int i = index; i < this.size; i++)
			{
				this.items[i].position = i + 1;
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00027FFB File Offset: 0x000261FB
		protected override void OnSet(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002803E File Offset: 0x0002623E
		private void UpdateGenericParameter(GenericParameter item, int index)
		{
			item.owner = this.owner;
			item.position = index;
			item.type = this.owner.GenericParameterType;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00028064 File Offset: 0x00026264
		protected override void OnRemove(GenericParameter item, int index)
		{
			item.owner = null;
			item.position = -1;
			item.type = GenericParameterType.Type;
			for (int i = index + 1; i < this.size; i++)
			{
				this.items[i].position = i - 1;
			}
		}

		// Token: 0x04000338 RID: 824
		private readonly IGenericParameterProvider owner;
	}
}
