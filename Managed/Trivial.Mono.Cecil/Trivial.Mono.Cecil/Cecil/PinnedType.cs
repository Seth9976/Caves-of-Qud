using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200009D RID: 157
	public sealed class PinnedType : TypeSpecification
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsPinned
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000148A3 File Offset: 0x00012AA3
		public PinnedType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.Pinned;
		}
	}
}
