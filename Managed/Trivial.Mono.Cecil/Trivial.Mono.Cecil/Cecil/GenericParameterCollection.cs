using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000046 RID: 70
	internal sealed class GenericParameterCollection : Collection<GenericParameter>
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00006552 File Offset: 0x00004752
		internal GenericParameterCollection(IGenericParameterProvider owner)
		{
			this.owner = owner;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006561 File Offset: 0x00004761
		internal GenericParameterCollection(IGenericParameterProvider owner, int capacity)
			: base(capacity)
		{
			this.owner = owner;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00006571 File Offset: 0x00004771
		protected override void OnAdd(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000657C File Offset: 0x0000477C
		protected override void OnInsert(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
			for (int i = index; i < this.size; i++)
			{
				this.items[i].position = i + 1;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006571 File Offset: 0x00004771
		protected override void OnSet(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000065B2 File Offset: 0x000047B2
		private void UpdateGenericParameter(GenericParameter item, int index)
		{
			item.owner = this.owner;
			item.position = index;
			item.type = this.owner.GenericParameterType;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000065D8 File Offset: 0x000047D8
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

		// Token: 0x0400009C RID: 156
		private readonly IGenericParameterProvider owner;
	}
}
