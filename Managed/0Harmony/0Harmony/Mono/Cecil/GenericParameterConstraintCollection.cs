using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200020F RID: 527
	internal class GenericParameterConstraintCollection : Collection<GenericParameterConstraint>
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x00028195 File Offset: 0x00026395
		internal GenericParameterConstraintCollection(GenericParameter genericParameter)
		{
			this.generic_parameter = genericParameter;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000281A4 File Offset: 0x000263A4
		internal GenericParameterConstraintCollection(GenericParameter genericParameter, int length)
			: base(length)
		{
			this.generic_parameter = genericParameter;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000281B4 File Offset: 0x000263B4
		protected override void OnAdd(GenericParameterConstraint item, int index)
		{
			item.generic_parameter = this.generic_parameter;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000281B4 File Offset: 0x000263B4
		protected override void OnInsert(GenericParameterConstraint item, int index)
		{
			item.generic_parameter = this.generic_parameter;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000281B4 File Offset: 0x000263B4
		protected override void OnSet(GenericParameterConstraint item, int index)
		{
			item.generic_parameter = this.generic_parameter;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000281C2 File Offset: 0x000263C2
		protected override void OnRemove(GenericParameterConstraint item, int index)
		{
			item.generic_parameter = null;
		}

		// Token: 0x0400033D RID: 829
		private readonly GenericParameter generic_parameter;
	}
}
