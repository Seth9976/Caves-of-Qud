using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000255 RID: 597
	internal sealed class PinnedType : TypeSpecification
	{
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00018318 File Offset: 0x00016518
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x000189AE File Offset: 0x00016BAE
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsPinned
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0002D881 File Offset: 0x0002BA81
		public PinnedType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.Pinned;
		}
	}
}
